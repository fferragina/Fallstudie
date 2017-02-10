#region

using System;
using System.Data;
using System.Linq;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.BL.DomainModel.SpecialCases;
using HsrOrderApp.DAL.Data.Repositories;
using HsrOrderApp.DAL.Providers.EntityFramework.Repositories.Adapters;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

#endregion

namespace HsrOrderApp.DAL.Providers.EntityFramework.Repositories
{
    public class SupplierRepository : RepositoryBase, ISupplierRepository
    {
        public SupplierRepository(HsrOrderAppEntities db)
            : base(db)
        {
        }

        public SupplierRepository(string connectionString) : base(connectionString)
        {
        }

        public SupplierRepository() : base()
        {
        }

        public int SaveSupplier(HsrOrderApp.BL.DomainModel.Supplier supplier)
        {
            try
            {
                string setname = "SuppliersSet";
                Supplier dbSupplier;

                bool isNew = false;
                if (supplier.SupplierId == default(int) || supplier.SupplierId <= 0)
                {
                    isNew = true;
                    dbSupplier = new Supplier();
                }
                else
                {
                    dbSupplier = new Supplier() {SupplierId = supplier.SupplierId, Version = supplier.Version.ToTimestamp()};
                    dbSupplier.EntityKey = db.CreateEntityKey(setname, dbSupplier);
                    db.AttachTo(setname, dbSupplier);
                }

                dbSupplier.Name = supplier.Name;
                if (isNew)
                {
                    db.AddToSuppliersSet(dbSupplier);
                }
                db.SaveChanges();

                supplier.SupplierId = dbSupplier.SupplierId;
                return dbSupplier.SupplierId;
            }
            catch (OptimisticConcurrencyException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return default(int);
            }
        }

        public void DeleteSupplier(int id)
        {
            Supplier cu = db.SuppliersSet.First(c => c.SupplierId == id);
            if (cu != null)
            {
                db.DeleteObject(cu);
                db.SaveChanges();
            }
        }

        public int SaveAddress(HsrOrderApp.BL.DomainModel.Address address, HsrOrderApp.BL.DomainModel.Supplier forThisSupplier)
        {
            AddressRepository rep = new AddressRepository(db);
            Address dbAddress = rep.SaveAddressInternal(address);
            if (address.IsNew)
            {
                Supplier supplier = db.SuppliersSet.First(c => c.SupplierId == forThisSupplier.SupplierId);
                supplier.Addresses.Add(dbAddress);
                db.SaveChanges();
            }
            return dbAddress.AddressId;
        }

        public void DeleteAddress(int id)
        {
            AddressRepository rep = new AddressRepository(db);
            rep.DeleteAddress(id);
        }

        public IQueryable<BL.DomainModel.Supplier> GetAll()
        {
            throw new NotImplementedException();
        }

        public BL.DomainModel.Supplier GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}