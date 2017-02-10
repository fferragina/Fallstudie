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

        public IQueryable<Supplier> GetSuppliersByCriteria(SupplierSearchType searchType, string name)
        {
            IQueryable<Supplier> suppliers = null;

            switch (searchType)
            {
                case SupplierSearchType.None:
                    suppliers = rep.GetAll();
                    break;
                case SupplierSearchType.ByName:
                    suppliers = rep.GetAll().Where(cu => cu.Name == name);
                    break;
            }

            return suppliers;
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