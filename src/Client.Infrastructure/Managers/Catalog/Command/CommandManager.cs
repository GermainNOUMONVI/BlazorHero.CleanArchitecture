using BlazorHero.CleanArchitecture.Application.Features.Commands.Commands.AddCommandCommand;
using BlazorHero.CleanArchitecture.Application.Features.Commands.Queries.GetAllCommands;
using BlazorHero.CleanArchitecture.Application.Features.Commands.Queries.GetCommandsById;
using BlazorHero.CleanArchitecture.Application.Features.Products.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Application.Requests.Catalog;
using BlazorHero.CleanArchitecture.Application.Requests.Identity;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Extensions;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Catalog.Command
{
    public class CommandManager : ICommandManager
    {
        private readonly HttpClient _httpClient;

        public CommandManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.CommandsEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            throw new NotImplementedException();
        }

        public async Task<IResult<GetCommandByIdResponse>> GetCommandByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{Routes.CommandsEndpoints.Update}/{id}");
            return await response.ToResult<GetCommandByIdResponse>();
            throw new NotImplementedException();
        }

        public async Task<IResult<List<GetAllCommandsResponse>>> GetCommandsAsync()
        {
            var response = await _httpClient.GetAsync(Routes.CommandsEndpoints.GetAll);
            return await response.ToResult<List<GetAllCommandsResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditCommandCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.CommandsEndpoints.Create, request);
            return await response.ToResult<int>();
        }
    }
}
