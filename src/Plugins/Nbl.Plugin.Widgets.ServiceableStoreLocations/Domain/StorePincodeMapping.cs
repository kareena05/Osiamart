using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;

namespace Nbl.Plugin.Widgets.ServiceableStoreLocations.Domain
{
    public class StorePincodeMapping : BaseEntity
    {
        public int StoreId { get; set; }
        public string Pincode { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? IsDeleted { get; set; } 
    }
}
