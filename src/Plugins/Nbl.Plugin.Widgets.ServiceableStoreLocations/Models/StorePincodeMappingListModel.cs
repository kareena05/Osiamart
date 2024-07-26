using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Nbl.Plugin.Widgets.ServiceableStoreLocations.Models
{
    public class StorePincodeMappingListModel
    {
        public List<StorePincodeMappingModel> StorePincodeMappings { get; set; }
        public List<SelectListItem> AvailableStores { get; set; }
    }
}
