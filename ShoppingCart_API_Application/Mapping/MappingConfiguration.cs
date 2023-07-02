using AutoMapper;
using ShoppingCart_API_Application.Models;
using ShoppingCart_API_Application.Models.DTO.product;
using ShoppingCart_API_Application.Models.DTO.productnumber;

namespace ShoppingCart_API_Application.Mapping
{
    public class MappingConfiguration:Profile
    {
        public MappingConfiguration()
        {
            CreateMap<Product,ProductDTO>().ReverseMap();
            CreateMap<ProductDTO,Product>().ReverseMap();
        
            CreateMap<Product,ProductCreateDTO>().ReverseMap();
            CreateMap<Product,ProductUpdateDTO>().ReverseMap();


            CreateMap<ProductNumber, ProductNumberDTO>().ReverseMap();
            CreateMap<ProductNumberDTO, ProductNumber>().ReverseMap();

            CreateMap<ProductNumber,ProductNumberCreateDTO>().ReverseMap(); 
            CreateMap<ProductNumber,ProductNumberUpdateDTO>().ReverseMap(); 
        }
    }
}
