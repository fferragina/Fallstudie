#region

using System.Linq;
using System.Transactions;
using HsrOrderApp.BL.BusinessComponents.DependencyInjection;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.DAL.Data.Repositories;
using HsrOrderApp.SharedLibraries.SharedEnums;
using System.Collections.Generic;

#endregion

namespace HsrOrderApp.BL.BusinessComponents
{
    public class SupplierBusinessComponent
    {
        private ISupplierRepository rep;

        public SupplierBusinessComponent()
        {
        }

        public SupplierBusinessComponent(ISupplierRepository unitOfWork)
        {
            this.rep = unitOfWork;
        }

        #region CRUD Operations

        public Supplier GetSupplierById(int supplierId)
        {
            Supplier supplier = rep.GetById(supplierId);
            return supplier;
        }

        public IQueryable<Supplier> GetSuppliersByCriteria(SupplierSearchType searchType, string Name)
        {
            IQueryable<Supplier> products = null;

            switch (searchType)
            {
                case SupplierSearchType.None:
                    products = rep.GetAll();
                    break;
                case SupplierSearchType.ByName:
                    products = rep.GetAll().Where(cu => cu.Name == Name);
                    break;
            }

            return products;
        }


        public int StoreSupplier(Supplier supplier)
        {
            int supplierId = default(int);
            using (TransactionScope transaction = new TransactionScope())
            {
                supplierId = rep.SaveSupplier(supplier);
                transaction.Complete();
            }

            return supplierId;
        }

        public void DeleteSupplier(int supplierId)
        {
            rep.DeleteSupplier(supplierId);
        }

        #endregion

        public ISupplierRepository Repository
        {
            get { return this.rep; }
            set { this.rep = value; }
        }

    }
}