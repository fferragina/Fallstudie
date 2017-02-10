#region

using System.Collections.Generic;
using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    public class GetSuppliersResponse : ResponseType
    {
        public GetSuppliersResponse()
        {
            this.Suppliers = new List<SupplierDTO>();
        }

        [DataMember]
        [ObjectCollectionValidator(typeof (SupplierDTO))]
        public IList<SupplierDTO> Suppliers { get; set; }
    }
}