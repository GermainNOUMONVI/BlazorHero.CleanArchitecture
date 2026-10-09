using BlazorHero.CleanArchitecture.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Interfaces.Repositories
{
    public interface ICommandDetailsRepository
    {
        public Task<List<CommandDetails>> GetCommandDetails(int id);
        public void AddRangeCommandDetails(List<CommandDetails> details);
        public void DeleteRangeCommandDetails(List<CommandDetails> details);
        //public void DeleteRangeCommandDetails(int id);
    }
}
