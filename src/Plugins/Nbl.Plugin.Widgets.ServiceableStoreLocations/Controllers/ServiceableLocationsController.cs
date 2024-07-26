using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Framework;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nbl.Plugin.Widgets.ServiceableStoreLocations.Services;
using Nbl.Plugin.Widgets.ServiceableStoreLocations.Models;
using Nop.Services.Stores;

namespace Nbl.Plugin.Widgets.ServiceableStoreLocations.Controllers
{
   

    [AutoValidateAntiforgeryToken]
    [AuthorizeAdmin] //confirms access to the admin panel
    [Area(AreaNames.Admin)] //specifies the area containing a controller or action
    public class ServiceableLocationsController : BasePluginController
    {
        private readonly IStorePincodeService _storePincodeService;
        private readonly IStoreService _storeService;

        public ServiceableLocationsController(IStorePincodeService storePincodeService,IStoreService storeService)
        {
            _storePincodeService = storePincodeService;
            _storeService = storeService;
        }
        #region Methods
        public async Task<IActionResult> Configure()
        {
            var model = new StorePincodeMappingListModel
            {
                StorePincodeMappings = await _storePincodeService.GetAllStorePincodeMappingsAsync(),
                AvailableStores = (await _storeService.GetAllStoresAsync()).Select(store => new SelectListItem
                {
                    Text = store.Name,
                    Value = store.Id.ToString()
                }).ToList()
            };
            return View("~/Plugins/Widgets.ServiceableStoreLocations/Views/Configure.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetStorePincode(int id)
        {
            var storePincode = await _storePincodeService.GetStoreLocationById(id);
            if (storePincode == null)
            {
                return NotFound();
            }

            var model = new StorePincodeMappingModel
            {
                Id = storePincode.Id,
                StoreId = storePincode.StoreId,
                Pincode = storePincode.Pincode,
                Status = storePincode.Status
            };

            return Json(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddStorePincode(StorePincodeMappingModel model)
        {

            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    var msg = $"Property: {state.Key}, Error: {error.ErrorMessage}";
                    Console.WriteLine($"Property: {state.Key}, Error: {error.ErrorMessage}");
                }
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _storePincodeService.AddVendorLocationsAsync(model);
            return RedirectToAction("Configure");
        }
        [HttpGet]
        public async Task<IActionResult> EditStorePincode(int id)
        {
            var storePincode = await _storePincodeService.GetStoreLocationById(id);
            if (storePincode == null)
            {
                return NotFound();
            }

            var model = new StorePincodeMappingModel
            {
                Id = storePincode.Id,
                StoreId = storePincode.StoreId,
                StoreName = (await _storeService.GetStoreByIdAsync(storePincode.StoreId)).Name??"",
                Pincode = storePincode.Pincode,
                Status = storePincode.Status
            };

            //model.AvailableStores = (await _storeService.GetAllStoresAsync()).Select(store => new SelectListItem
            //{
            //    Text = store.Name,
            //    Value = store.Id.ToString()
            //}).ToList();

            return View("~/Plugins/Widgets.ServiceableStoreLocations/Views/EditStorePincode.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditStorePincode(StorePincodeMappingModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        var msg = $"Property: {state.Key}, Error: {error.ErrorMessage}";
                        Console.WriteLine(msg); // Log the error details
                    }
                }

                return BadRequest(ModelState);
            }

            await _storePincodeService.UpdateStoreLocationsAsync(model);
            return RedirectToAction("Configure");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStorePincode(int id)
        {
            await _storePincodeService.DeleteStoreLocationAsync(id);
            return RedirectToAction("Configure");
        }
        #endregion
    }
}
