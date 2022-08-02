using AutoMapper;
using Shopping_Product_API.Data.ValueObjects;
using Shopping_Product_API.Model;

namespace Shopping_Product_API.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductVO, Product>();
                config.CreateMap<Product, ProductVO>();

            });
            return mappingConfig;
        }
    }
}
