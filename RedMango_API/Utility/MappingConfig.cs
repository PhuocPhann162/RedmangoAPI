using AutoMapper;
using RedMango_API.Models;
using RedMango_API.Models.Dto;

namespace RedMango_API.Utility
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<OrderHeader, OrderHeaderCreateDTO>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
