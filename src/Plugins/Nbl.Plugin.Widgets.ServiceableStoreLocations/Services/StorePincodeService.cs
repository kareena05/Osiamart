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
            var existingStorePincodeLocation = GetStoreLocationByStoreId(model.StoreId);
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
            //newVendorLocation.CreatedAt = DateTime.Now;
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

            var existingPincodes = existingStoreLocation.Pincode.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct();
            var mergedPincodes = pincodes.Union(existingPincodes).Distinct().ToArray();
            existingStoreLocation.Pincode = string.Join(",", mergedPincodes);
            //existingStoreLocation.UpdatedAt = DateTime.Now;

            await _storePincodeRepository.UpdateAsync(existingStoreLocation);

            return false;
        }

        public async Task<StorePincodeMapping> GetStoreLocationByStoreId(int storeId)
        {

            var data = (from vl in _storePincodeRepository.Table
                        where vl.StoreId == storeId
                        && vl.Status
                        select vl).FirstOrDefault();
            return data;
        }
    }
}
