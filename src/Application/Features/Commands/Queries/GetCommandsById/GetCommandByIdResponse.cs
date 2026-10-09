using BlazorHero.CleanArchitecture.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Commands.Queries.GetCommandsById
{
    public class GetCommandByIdResponse
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string DeliveryAddress { get; set; }
        public List<CommandDetailsResponse> ProductsList { get; set; }
    }

    public class CommandDetailsResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string ImageDataURL { get; set; }
    }
}
