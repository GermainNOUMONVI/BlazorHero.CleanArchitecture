
namespace BlazorHero.CleanArchitecture.Application.Features.Products.Queries.GetAllProduct
{
    public class GetAllProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public string Brand { get; set; }
        public string ImageDataURL { get; set; }
    }
}
