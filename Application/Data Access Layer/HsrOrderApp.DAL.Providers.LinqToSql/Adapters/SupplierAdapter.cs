#region

using System.Data.Linq;
using HsrOrderApp.SharedLibraries.SharedEnums;

#endregion

namespace HsrOrderApp.DAL.Providers.LinqToSql.Adapters
{
    internal static class SupplierAdapter
    {
        internal static BL.DomainModel.Supplier AdaptProduct(Supplier s)
        {
            BL.DomainModel.Supplier supplier = new BL.DomainModel.Supplier()
                                                 {
                                                     SupplierId = s.SupplierId,
                                                     Name = s.Name,
                                                     CreditRating = s.CreditRating,
                                                     PreferredSupplierFlag = s.PreferredSupplierFlag,
                                                     ActiveFlag = s.ActiveFlag,
                                                     PurchasingWebServiceUrl = s.PurchasingWebServiceURL,
                                                     Version = s.Version.ToUlong(),
                                                 };
            return supplier;
        }
    }
}