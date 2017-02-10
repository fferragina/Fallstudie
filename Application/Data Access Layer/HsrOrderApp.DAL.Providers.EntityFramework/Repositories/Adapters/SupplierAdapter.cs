#region

using System.Data.Objects.DataClasses;

#endregion

namespace HsrOrderApp.DAL.Providers.EntityFramework.Repositories.Adapters
{
    internal static class SupplierAdapter
    {
        internal static BL.DomainModel.Supplier AdaptSupplier(EntityReference<Supplier> s)
        {
            if (s.Value == null)
                return null;
            return AdaptSupplier(s.Value);
        }
       
        internal static BL.DomainModel.Supplier AdaptSupplier(Supplier c)
        {
            BL.DomainModel.Supplier supplier = new BL.DomainModel.Supplier()
                                                   {
                                                       SupplierId = c.SupplierId,
                                                       Name = c.Name,
                                                       CreditRating = c.CreditRating,
                                                       PreferredSupplierFlag = c.PreferredSupplierFlag,
                                                       ActiveFlag = c.ActiveFlag,
                                                       PurchasingWebServiceUrl = c.PurchasingWebServiceURL,
                                                       Version = c.Version.ToUlong(),
                                                   };
            return supplier;
        }
    }
}