#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    public class GetSupplierResponse : ResponseType
    {
        public GetSupplierResponse()
        {
            this.Supplier = new SupplierDTO();
        }

        [DataMember]
        [ObjectValidator]
        public SupplierDTO Supplier { get; set; }
    }
}