using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LinqToDB.Common;
using Nbl.Plugin.Widgets.ServiceableStoreLocations.Domain;
using Nbl.Plugin.Widgets.ServiceableStoreLocations.Models;
using Nop.Data;
using Nop.Services.Stores;

namespace Nbl.Plugin.Widgets.ServiceableStoreLocations.Services
{
    public class StorePincodeService : IStorePincodeService
    {
        private readonly IRepository<StorePincodeMapping> _storePincodeRepository;
        private readonly IStoreService _storeService;
        private readonly IMapper _mapper;

        public StorePincodeService(IRepository<StorePincodeMapping> storePincodeRepository,
            IStoreService storeService,
            IMapper mapper)
        {
            _storePincodeRepository = storePincodeRepository;
            _storeService = storeService;
            _mapper = mapper;
        }
        public async Task<bool> AddVendorLocationsAsync(StorePincodeMappingModel model)
        {
            var store = await _storeService.GetStoreByIdAsync(model.StoreId);
            if (store == null)
            {
                return false;
            }
            var existingStorePincodeLocation = await GetStoreLocationByStoreId(model.StoreId);
            if (existingStorePincodeLocation != null)
            {
                return false;

            }
            var pincodes = model.Pincode.Split(",");
            pincodes.RemoveDuplicates();
            if (!pincodes.Any())
            {
                return false;
            }
            var newVendorLocation = _mapper.Map<StorePincodeMapping>(model);
            newVendorLocation.CreatedAt = DateTime.Now;
            await _storePincodeRepository.InsertAsync(newVendorLocation);
            return true;
        }
        public async Task<bool> UpdateStoreLocationsAsync(StorePincodeMappingModel model)
        {
            var store = await _storeService.GetStoreByIdAsync(model.StoreId);
            if (store == null)
            {
                return false;
            }

            var existingStoreLocation = await GetStoreLocationByStoreId(model.StoreId);
            if (existingStoreLocation == null)
            {
                return false;
            }

            var pincodes = model.Pincode.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct();

            if (!pincodes.Any())
            {
                return false;
            }

            //var existingPincodes = existingStoreLocation.Pincode.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct();
            //var mergedPincodes = pincodes.Union(existingPincodes).Distinct().ToArray();
            var mergedPincodes = pincodes.Distinct().ToArray();
            existingStoreLocation.Pincode = string.Join(",", mergedPincodes);
            existingStoreLocation.Status = model.Status;
            existingStoreLocation.UpdatedAt = DateTime.Now;

            await _storePincodeRepository.UpdateAsync(existingStoreLocation);

            return false;
        }

        public async Task<StorePincodeMapping> GetStoreLocationByStoreId(int storeId)
        {

            var data = (from vl in _storePincodeRepository.Table
                        where vl.StoreId == storeId
                        && (vl.IsDeleted == null
                        || vl.IsDeleted == false)
                        select vl).FirstOrDefault();
            return data;
        }
        public async Task<StorePincodeMapping> GetStoreLocationById(int Id)
        {

            var data = (from vl in _storePincodeRepository.Table
                        where vl.Id == Id
                        && (vl.IsDeleted == null
                        || vl.IsDeleted == false)
                        select vl).FirstOrDefault();
            return data;
        }

        public async Task<List<StorePincodeMappingModel>> GetAllStorePincodeMappingsAsync()
        {

            var data = await _storePincodeRepository.Table.Where(x => x.IsDeleted == null || x.IsDeleted == false).ToListAsync();
            var response = _mapper.Map<List<StorePincodeMappingModel>>(data);
            foreach (var mapping in response)
            {
                var store = await _storeService.GetStoreByIdAsync(mapping.StoreId);
                mapping.StoreName = store?.Name??""; // Set the StoreName property
            }
            return response;
        }
        public async Task DeleteStoreLocationAsync(int id)
        {
            var storePincodeMapping = await _storePincodeRepository.GetByIdAsync(id);
            if (storePincodeMapping != null)
            {
                storePincodeMapping.IsDeleted = true;
                storePincodeMapping.DeletedAt = DateTime.Now;
                await _storePincodeRepository.UpdateAsync(storePincodeMapping);
            }
        }
        public async Task<List<StoreDetailsModel>> GetStoresWithPincodesAsync()
        {
            var stores = await _storeService.GetAllStoresAsync();
            var storePincodes = await GetAllStorePincodeMappingsAsync();

            var storePincodeDict = storePincodes.ToDictionary(sp => sp.StoreId, sp => sp.Pincode);

            var combinedStores = stores.Select(store => new StoreDetailsModel
            {
                StoreId = store.Id,
                StoreName = store.Name,
                StoreUrl = store.Url, 
                Pincode = storePincodeDict.TryGetValue(store.Id, out var pincode) ? pincode : null
            }).ToList();

            return combinedStores;
        }

    }
}
