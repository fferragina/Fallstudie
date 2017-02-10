using HsrOrderApp.SharedLibraries.DTO;
using System.Collections.Generic;

namespace HsrOrderApp.UI.Mvc.Models
{
    public class SupplierListViewModel : ListViewModelBase<SupplierDTO>
    {
        public SupplierListViewModel() { }
        public SupplierListViewModel(List<SupplierDTO> list) { Items = list; }
    }
    public class SupplierViewModel : DetailViewModelBase<SupplierDTO>
    {
        public SupplierViewModel() { }
        public SupplierViewModel(SupplierDTO model, bool isNew) : base(model, isNew) { }
    }
}