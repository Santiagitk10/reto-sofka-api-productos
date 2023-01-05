using AutoMapper;
using DB;
using reto_sofka_api_productos.DTOs;

namespace reto_sofka_api_productos.Helpers
{
    public class AutoMappingProfiles : Profile
    {

        public AutoMappingProfiles() 
        {
        
            CreateMap<CreateProductDTO, Product>().ReverseMap();
            CreateMap<GetProductDTO, Product>().ReverseMap();
            CreateMap<CreatePurchaseDTO, Purchase>().ReverseMap();

        }

    }
}
