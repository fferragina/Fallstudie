#region

using System;
using System.Data.Linq;
using System.Linq;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.BL.DomainModel.SpecialCases;
using HsrOrderApp.DAL.Data.Repositories;
using HsrOrderApp.DAL.Providers.LinqToSql.Adapters;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

#endregion

namespace HsrOrderApp.DAL.Providers.LinqToSql.Repositories
{
    public class SupplierRepository : RepositoryBase, ISupplierRepository
    {
        public SupplierRepository(HsrOrderAppDataContext db) : base(db)
        {
        }

        public SupplierRepository(string connectionString) : base(connectionString)
        {
        }

        
        public int SaveSupplier(HsrOrderApp.BL.DomainModel.Supplier supplier)
        {
            try
            {
                Supplier dbSupplier = new Supplier();
                bool isNew = false;
                if (supplier.SupplierId == default(int) || supplier.SupplierId <= 0)
                    isNew = true;

                dbSupplier.SupplierId = supplier.SupplierId;
                dbSupplier.Version = supplier.Version.ToTimestamp();
                dbSupplier.Name = supplier.Name;
                dbSupplier.ActiveFlag = supplier.ActiveFlag;
                dbSupplier.PreferredSupplierFlag = supplier.PreferredSupplierFlag;
                dbSupplier.AccountNumber = supplier.AccountNumber;
                dbSupplier.CreditRating = supplier.CreditRating;
                dbSupplier.PurchasingWebServiceURL = supplier.PurchasingWebServiceUrl;

                if (isNew)
                    db.Suppliers.InsertOnSubmit(dbSupplier);
                else
                    db.Suppliers.Attach(dbSupplier, true);
                db.SubmitChanges();
                supplier.SupplierId = dbSupplier.SupplierId;
                return dbSupplier.SupplierId;
            }
            catch (ChangeConflictException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return default(int);
            }
        }

        public void DeleteSupplier(int id)
        {
            Supplier cu = db.Suppliers.FirstOrDefault(c => c.SupplierId == id);
            if (cu != null)
            {
                db.Suppliers.DeleteOnSubmit(cu);
                db.SubmitChanges();
            }
        }

        public int SaveAddress(HsrOrderApp.BL.DomainModel.Address address, HsrOrderApp.BL.DomainModel.Supplier forThisSupplier)
        {
            AddressRepository rep = new AddressRepository(db);
            int addressid = rep.SaveAddress(address);
            if (address.IsNew)
            {
                SupplierAddress ca = new SupplierAddress();
                ca.AddressId = addressid;
                ca.SupplierId = forThisSupplier.SupplierId;
                db.SupplierAddresses.InsertOnSubmit(ca);
                db.SubmitChanges();
            }
            return addressid;
        }

        public void DeleteAddress(int id)
        {
            AddressRepository rep = new AddressRepository(db);
            rep.DeleteAddress(id);
        }

        public IQueryable<BL.DomainModel.Supplier> GetAll()
        {
            var suppliers = from p in this.db.Suppliers
                           select SupplierAdapter.AdaptSupplier(p);
            return suppliers;
        }

        public BL.DomainModel.Supplier GetById(int id)
        {
            try
            {
                var suppliers = from p in this.db.Suppliers
                               where p.SupplierId == id
                               select SupplierAdapter.AdaptSupplier(p);

                return suppliers.First();
            }
            catch (ArgumentNullException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return new MissingSupplier();
            }
        }
    }
}