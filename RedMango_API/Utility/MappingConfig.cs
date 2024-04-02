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
                config.CreateMap<MenuItem, MenuItemUpdateDTO>().ReverseMap();

                // Order Map
                config.CreateMap<OrderHeader, OrderHeaderCreateDTO>().ReverseMap();
                config.CreateMap<OrderDetails, OrderDetailsCreateDTO>().ReverseMap();

                // Coupon Map 
                config.CreateMap<Coupon, CouponCreateDTO>().ReverseMap();
                config.CreateMap<Coupon, CouponUpdateDTO>().ReverseMap();

                // Review Map
                config.CreateMap<Review, CreateReviewDTO>().ReverseMap();   
            });
         
            return mappingConfig;
        }
    }
}
