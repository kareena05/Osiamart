using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nbl.Plugin.Widgets.ServiceableStoreLocations.Services;
using Nop.Services.Stores;

namespace Nbl.Plugin.Widgets.ServiceableStoreLocations.Components
{
    
    public class LocationPopUpPublicComponent : ViewComponent
    {
        private readonly IStorePincodeService _storePincodeService;
        private readonly IStoreService _storeService;

        public LocationPopUpPublicComponent(IStorePincodeService storePincodeService, IStoreService storeService)
        {
            _storePincodeService = storePincodeService;
            _storeService = storeService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _storePincodeService.GetStoresWithPincodesAsync();
            return View("~/Plugins/Widgets.ServiceableStoreLocations/Views/Component/AllStoresView.cshtml", model);
        }
    }
}
