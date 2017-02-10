#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.BL.DTOAdapters.Helper;
using HsrOrderApp.SharedLibraries.DTO;

#endregion

namespace HsrOrderApp.BL.DtoAdapters
{
    public class SupplierAdapter
    {
        #region SuppliersToDTO

        public static IList<SupplierDTO> SuppliersToDtos(IQueryable<Supplier> suppliers)
        {
            IQueryable<SupplierDTO> supplierDTOs = from p in suppliers
                                                 select SupplierToDto(p);
            return supplierDTOs.ToList();
        }

        public static SupplierDTO SupplierToDto(Supplier p)
        {
            SupplierDTO dto = new SupplierDTO()
                                 {
                                     Id = p.SupplierId,
                                     Name = p.Name,
                                     AccountNumber = p.AccountNumber,
                                     CreditRating = p.CreditRating,
                                     PreferredSupplierFlag = p.PreferredSupplierFlag,
                                     ActiveFlag = p.ActiveFlag,
                                     PurchasingWebServiceUrl = p.PurchasingWebServiceUrl,
                                     Version = p.Version
                                 };

            return dto;
        }

        #endregion

        #region DTOToSupplier

        public static Supplier DtoToSupplier(SupplierDTO dto)
        {
            Supplier supplier = new Supplier()
                                  {
                                      SupplierId = dto.Id,
                                      Name = dto.Name,
                                      AccountNumber = dto.AccountNumber,
                                      CreditRating = dto.CreditRating,
                                      PreferredSupplierFlag = dto.PreferredSupplierFlag,
                                      ActiveFlag = dto.ActiveFlag,
                                      PurchasingWebServiceUrl = dto.PurchasingWebServiceUrl,
                                      Version = dto.Version
                                  };
            ValidationHelper.Validate(supplier);
            return supplier;
        }

        #endregion
    }
}