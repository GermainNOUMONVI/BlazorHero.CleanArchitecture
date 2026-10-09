using BlazorHero.CleanArchitecture.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Domain.Entities.Catalog
{
    public class CommandDetails : AuditableEntity<int>
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int CommandId { get; set; }
        public virtual Command Command { get; set; }
        public virtual Product Product { get; set; }
    }
}
