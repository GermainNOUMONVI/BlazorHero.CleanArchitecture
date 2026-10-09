using BlazorHero.CleanArchitecture.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Domain.Entities.Catalog
{
    public class Command : AuditableEntity<int>
    {
        public string ClientEmail { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string DeliveryAddress { get; set; }
        public virtual List<CommandDetails> Details { get; }
    }
}
