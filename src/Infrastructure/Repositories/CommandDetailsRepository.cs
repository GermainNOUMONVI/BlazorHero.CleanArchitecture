using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.Catalog;
using BlazorHero.CleanArchitecture.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Infrastructure.Repositories
{
    public class CommandDetailsRepository : ICommandDetailsRepository
    {
        private readonly IRepositoryAsync<CommandDetails, int> _detailsRepository;
        private readonly IRepositoryAsync<Product, int> _productRepository;
        private readonly BlazorHeroContext _context;

        public CommandDetailsRepository(IRepositoryAsync<CommandDetails, int> repository, IRepositoryAsync<Product, int> productRepository, BlazorHeroContext context)
        {
            _detailsRepository = repository;
            _productRepository = productRepository;
            _context = context;
        }

        public async Task<List<CommandDetails>> GetCommandDetails(int id)
        {
            var commandDetails = await _detailsRepository.GetAllAsync();
            foreach(CommandDetails detalis in commandDetails)
            {
                detalis.Product = await _productRepository.GetByIdAsync(id);
            }
            return (await _detailsRepository.GetAllAsync()).Where(cmd => cmd.CommandId == id).ToList();
        }

        public async void AddRangeCommandDetails(List<CommandDetails> details)
        {
            _context.Set<CommandDetails>().AddRange(details);
            await _context.SaveChangesAsync();
        }
        
        public async void DeleteRangeCommandDetails(List<CommandDetails> details)
        {
            _context.Set<CommandDetails>().RemoveRange(details);
            await _context.SaveChangesAsync();
        }
        
        /*public async void DeleteRangeCommandDetails(int id)
        {
            var details = await GetCommandDetails(id);
            _context.Set<CommandDetails>().RemoveRange(details);
            await _context.SaveChangesAsync();
        }*/
    }
}
