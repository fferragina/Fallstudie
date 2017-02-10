#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Base;
using HsrOrderApp.SharedLibraries.SharedEnums;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO
{
    public class SupplierDTO : DTOParentObject
    {
        private string _name;
        private string _accountnumber;
        private string _creditrating;
        private bool?  _preferredsupplierflag;
        private bool?  _activeflag;
        private string _purchasingwebserviceurl;

        public SupplierDTO()
        {
            this.Name = string.Empty;
            this.AccountNumber = string.Empty;
            this.CreditRating = string.Empty;
            this.PreferredSupplierFlag = false;
            this.ActiveFlag = false;
            this.PurchasingWebServiceUrl = string.Empty;
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    this._name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string AccountNumber
        {
            get { return _accountnumber; }
            set
            {
                if (value != _accountnumber)
                {
                    this._accountnumber = value;
                    OnPropertyChanged(() => AccountNumber);
                }
            }
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string CreditRating
        {
            get { return _creditrating; }
            set
            {
                if (value != _creditrating)
                {
                    this._creditrating = value;
                    OnPropertyChanged(() => CreditRating);
                }
            }
        }

        [DataMember]
        public bool? PreferredSupplierFlag
        {
            get { return _preferredsupplierflag; }
            set
            {
                if (value != _preferredsupplierflag)
                {
                    this._preferredsupplierflag = value;
                    OnPropertyChanged(() => PreferredSupplierFlag);
                }
            }
        }

        [DataMember]
        public bool? ActiveFlag
        {
            get { return _activeflag; }
            set
            {
                if (value != _activeflag)
                {
                    this._activeflag = value;
                    OnPropertyChanged(() => ActiveFlag);
                }
            }
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string PurchasingWebServiceUrl
        {
            get { return _purchasingwebserviceurl; }
            set
            {
                if (value != _purchasingwebserviceurl)
                {
                    this._purchasingwebserviceurl = value;
                    OnPropertyChanged(() => PurchasingWebServiceUrl);
                }
            }
        }
    }
}