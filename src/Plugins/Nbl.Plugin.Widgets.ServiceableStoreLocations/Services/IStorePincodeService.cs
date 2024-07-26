using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nbl.Plugin.Widgets.ServiceableStoreLocations.Domain;
using Nbl.Plugin.Widgets.ServiceableStoreLocations.Models;

namespace Nbl.Plugin.Widgets.ServiceableStoreLocations.Services
{
    public interface IStorePincodeService
    {
        Task<bool> AddVendorLocationsAsync(StorePincodeMappingModel model);
        Task<bool> UpdateStoreLocationsAsync(StorePincodeMappingModel model);
        Task<StorePincodeMapping> GetStoreLocationByStoreId(int storeId);
        Task<StorePincodeMapping> GetStoreLocationById(int Id);
        Task<List<StorePincodeMappingModel>> GetAllStorePincodeMappingsAsync();
        Task DeleteStoreLocationAsync(int id);
        Task<List<StoreDetailsModel>> GetStoresWithPincodesAsync();

    }
}
