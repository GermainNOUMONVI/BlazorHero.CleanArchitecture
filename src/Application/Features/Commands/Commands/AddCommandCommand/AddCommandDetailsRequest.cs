using BlazorHero.CleanArchitecture.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Commands.Commands.AddCommandCommand
{
    public class AddCommandRequest
    {
        public string ClientEmail { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string DeliveryAddress { get; set; }
    }
    /*public class AddCommandDetailsRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }*/
}
