using BlazorHero.CleanArchitecture.Application.Features.Commands.Commands.AddCommandCommand;
using BlazorHero.CleanArchitecture.Application.Features.Commands.Queries.GetAllCommands;
using BlazorHero.CleanArchitecture.Application.Features.Commands.Queries.GetCommandsById;
using BlazorHero.CleanArchitecture.Application.Features.Products.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Application.Features.Products.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Application.Requests.Catalog;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Catalog.Command
{
    public interface ICommandManager : IManager
    {
        Task<IResult<List<GetAllCommandsResponse>>> GetCommandsAsync();
        Task<IResult<GetCommandByIdResponse>> GetCommandByIdAsync(int id);

        Task<IResult<int>> SaveAsync(AddEditCommandCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}
