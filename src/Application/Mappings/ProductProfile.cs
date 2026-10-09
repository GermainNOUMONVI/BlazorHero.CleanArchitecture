using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Features.Products.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Application.Features.Products.Queries.GetAllProduct;
using BlazorHero.CleanArchitecture.Domain.Entities.Catalog;

namespace BlazorHero.CleanArchitecture.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddEditProductCommand, Product>().ReverseMap();
            CreateMap<Product, GetAllProductResponse>().ReverseMap();
        }
    }
}