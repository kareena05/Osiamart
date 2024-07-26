using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nbl.Plugin.Widgets.ServiceableStoreLocations.Components;
using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nbl.Plugin.Widgets.ServiceableStoreLocations
{
    public class ServiceableStoreLocationsPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly IWebHelper _webHelper;
        private readonly ILocalizationService _localizationService;

        public ServiceableStoreLocationsPlugin(IWebHelper webHelper, ILocalizationService localizationService)
        {
            _webHelper = webHelper;
            _localizationService = localizationService;
        }
        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/ServiceableLocations/Configure";
        }

        /// <summary>

        public bool HideInWidgetList => false;
        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string>
            {
                //AdminWidgetZones.ProductListButtons,
                //AdminWidgetZones.OrderShipmentAddButtons,
                //PublicWidgetZones.HeaderSelectors,
                //AdminWidgetZones.ProductDetailsBlock,
                //PublicWidgetZones.HeaderAfter,
                PublicWidgetZones.HeaderSelectors,
                PublicWidgetZones.BodyEndHtmlTagBefore,
                //PublicWidgetZones.ContentAfter
            });
        }

        public Type GetWidgetViewComponent(string widgetZone)
        {
            if (widgetZone.Equals(PublicWidgetZones.HeaderSelectors))
            {
                return typeof(ChooseLocationPublicComponent);
            }
            else if (widgetZone.Equals(PublicWidgetZones.BodyEndHtmlTagBefore))
            {
                return typeof(LocationPopUpPublicComponent);
            }
            return null;
            //  if (
            //    widgetZone.Equals(AdminWidgetZones.ProductListButtons)
            //    )
            //  {
            //      return typeof(AdminBulkImportViewComponent);
            //  }
            //  else if (widgetZone.Equals(AdminWidgetZones.OrderShipmentAddButtons))
            //  {
            //      return typeof(AdminShipmentWDMS);
            //  }

            //  //Location wise pricing 
            //  else if (
            //widgetZone.Equals(AdminWidgetZones.ProductDetailsBlock)
            //)
            //  {
            //      return typeof(LocationWisePricingAdminViewComponent);
            //  }
            //  else if (widgetZone.Equals(PublicWidgetZones.HeaderSelectors))
            //  {
            //      return typeof(WidgetsLocationWisePricingPublicViewComponent);
            //  }
            //  else if (widgetZone.Equals(PublicWidgetZones.ContentAfter))
            //  {
            //      return typeof(FindLocationPopUpViewComponent);
            //  }
            //  return typeof(WidgetsLocationWisePricingPublicViewComponent);
            // location wise pricing ends
        }

        public override async Task InstallAsync()
        {
            //await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            //{
            //    ["Admin.Catalog.Products.LocationWisepricing.Fields.Id"] = "Id",
            //    ["Admin.Catalog.Products.LocationWisepricing.Fields.Price"] = "Price",
            //    ["Admin.Catalog.Products.LocationWisepricing.Fields.Pincode"] = "PinCode"
            //});
            await base.InstallAsync();
        }

        public override async Task UninstallAsync()
        {
            await base.UninstallAsync();
        }

    }
}