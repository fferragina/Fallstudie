#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.DomainModel.HelperObjects;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.BL.DomainModel
{
    public class Supplier : DomainObject
    {
        public Supplier()
        {
            this.SupplierId = default(int);
            this.Name = string.Empty;
            this.AccountNumber = string.Empty;
            this.Addresses = new List<Address>().AsQueryable();
            this.Products = new List<Product>().AsQueryable();
            this.CreditRating = string.Empty;
            this.PreferredSupplierFlag = false;
            this.ActiveFlag = false;
            this.PurchasingWebServiceUrl = string.Empty;
        }

        public int SupplierId { get; set; }

        [StringLengthValidator(1, 50)]
        public string Name { get; set; }

        [StringLengthValidator(1, 50)]
        public string AccountNumber { get; set; }

        [StringLengthValidator(1, 50)]
        public string PurchasingWebServiceUrl { get; set; }

        public IQueryable<Address> Addresses { get; set; }
        public IQueryable<Product> Products { get; set; }

        public string CreditRating { get; set; } 

        public bool? PreferredSupplierFlag { get; set; }

        public bool? ActiveFlag { get; set; }
       
    }
}