using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Framework;

namespace Nbl.Plugin.Widgets.ServiceableStoreLocations.Controllers
{
   

    [AutoValidateAntiforgeryToken]
    [AuthorizeAdmin] //confirms access to the admin panel
    [Area(AreaNames.Admin)] //specifies the area containing a controller or action
    public class ServiceableLocationsController : BasePluginController
    {
        #region Methods
        public async Task<IActionResult> Configure()
        {
            var model = new StorePincodeMappingListModel
            {
                StorePincodeMappings = (await _storePincodeService.GetAllStorePincodeMappingsAsync()).ToList(),
                AvailableStores = (await _storeService.GetAllStoresAsync()).Select(store => new SelectListItem
                {
                    Text = store.Name,
                    Value = store.Id.ToString()
                }).ToList()
            };
            return View("~/Plugins/Widgets.ServiceableStoreLocations/Views/Configure.cshtml");
        }
        #endregion
    }
}
