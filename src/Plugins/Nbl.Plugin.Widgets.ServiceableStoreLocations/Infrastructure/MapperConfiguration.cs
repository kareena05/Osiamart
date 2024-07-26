using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Nbl.Plugin.Widgets.ServiceableStoreLocations.Domain;
using Nbl.Plugin.Widgets.ServiceableStoreLocations.Models;
using Nop.Core.Infrastructure.Mapper;

namespace Nbl.Plugin.Widgets.ServiceableStoreLocations.Infrastructure
{
    public class MapperConfiguration : Profile, IOrderedMapperProfile
    {
        #region Ctor

        public MapperConfiguration()
        {
            CreateMap<StorePincodeMappingModel, StorePincodeMapping>().ReverseMap();

        }

        #endregion

        #region Properties

        /// <summary>
        /// Order of this mapper implementation
        /// </summary>
        public int Order => 1;

        #endregion
    }
}
