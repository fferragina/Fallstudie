#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using HsrOrderApp.SharedLibraries.SharedEnums;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses
{
    public class GetSuppliersRequest : RequestType
    {
        [DataMember]
        public SupplierSearchType SearchType { get; set; }

        [DataMember]
        public string Name { get; set; }
        }

}