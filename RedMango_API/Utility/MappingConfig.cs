using AutoMapper;
using RedMango_API.Models;
using RedMango_API.Models.Dto;
using RedMango_API.Models.DTO;

namespace RedMango_API.Utility
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                // MenuItem Map
                config.CreateMap<MenuItem, MenuItemCreateDTO>().ReverseMap();

                // Order Map
                config.CreateMap<OrderHeader, OrderHeaderCreateDTO>().ReverseMap();
                config.CreateMap<OrderDetails, OrderDetailsCreateDTO>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
