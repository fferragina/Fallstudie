using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.Mvc.Controllers.Base;
using HsrOrderApp.UI.Mvc.Helpers;
using HsrOrderApp.UI.Mvc.Models;
using HsrOrderApp.UI.Mvc.Resources;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HsrOrderApp.UI.Mvc.Controllers
{
    [CustomAuthorize(RequiredPermissions = new UserPermission[] { UserPermission.ADMIN, UserPermission.STAFF })]
    public class SupplierController : HsrOrderAppController
    {
        // GET: Supplier
        public ActionResult Index()
        {
            var vm = new SupplierListViewModel();
            vm.DisplayName = Strings.SupplierViewModel_DisplayName;
            vm.Items = Service.GetAllSuppliers().ToList();
            vm.SelectedItem = vm.Items.FirstOrDefault();

            // Finish Action
            return View(vm);
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int id)
        {
            SupplierDTO item = Service.GetSupplierById(id);
            return DisplayDetails(item);
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            SupplierDTO item = new SupplierDTO();
            return DisplayDetails(item);
        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(SupplierViewModel vmChanged)
        {
            SupplierViewModel vm = DisplayDetails(vmChanged);
            return StoreEntity(vm);
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            SupplierDTO item = Service.GetSupplierById(id);
            return DisplayDetails(item);
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(SupplierViewModel vmChanged)
        {
            SupplierViewModel vm = DisplayDetails(vmChanged);
            return StoreEntity(vm);
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Service.DeleteSupplier(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex);
            }
            return RedirectToAction("Index");
        }

        protected SupplierViewModel DisplayDetails(SupplierViewModel vmChanged)
        {
            var vm = new SupplierViewModel();
            vm.DisplayName = Strings.SupplierViewModel_DisplayName;
            vm.Model = vmChanged.Model;

            return vm;
        }
        protected ActionResult DisplayDetails(SupplierDTO item)
        {
            var vm = GetViewModelFromTempData<SupplierViewModel>() ?? new SupplierViewModel();
            vm.DisplayName = Strings.SupplierViewModel_DisplayName;
            vm.Model = item;

            // Finish Action
            StoreViewModelToTempData(vm);
            return View(vm);
        }

        protected ActionResult StoreEntity(SupplierViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.StoreSupplier(vm.Model);

                    // Finish Action and go back to Index
                    StoreViewModelToTempData(vm);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex);
            }

            // Finish Action without saving
            StoreViewModelToTempData(vm);
            return View(vm);
        }
    }
}
