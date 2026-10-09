using BlazorHero.CleanArchitecture.Application.Specifications.Base;
using BlazorHero.CleanArchitecture.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Specifications.Catalog
{
    public class CommandFilterSpecification : HeroSpecification<Command>
    {
        public CommandFilterSpecification(string searchingString)
        {
            if (!string.IsNullOrEmpty(searchingString))
            {
                //Criteria = c => c.ClientName.Contains(searchingString) || c.Description.Contains(searchingString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}
