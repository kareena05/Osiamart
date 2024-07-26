using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Models;

namespace Nbl.Plugin.Widgets.ServiceableStoreLocations.Models
{
    public record StorePincodeMappingModel : BaseNopEntityModel
    {
        public int StoreId { get; set; }
        public string? StoreName { get; set; } = string.Empty;
        public string Pincode { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
