using BlazorHero.CleanArchitecture.Application.Features.Commands.Commands.AddCommandCommand;
using BlazorHero.CleanArchitecture.Application.Features.Commands.Commands.DeleteCommandCommand;
using BlazorHero.CleanArchitecture.Application.Features.Commands.Queries.GetAllCommands;
using BlazorHero.CleanArchitecture.Application.Features.Commands.Queries.GetCommandsById;
using BlazorHero.CleanArchitecture.Domain.Entities.Catalog;
using BlazorHero.CleanArchitecture.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Server.Controllers.v1.Catalog
{
    public class CommandsController : BaseApiController<Command>
    {
        [Authorize(Policy = Permissions.Commands.View)]
        [HttpGet]
        public async Task<IActionResult> GetAllCommands()
        {
            var commands = await _mediator.Send(new GetAllCommandsQuery());
            return Ok(commands);
        }

        [Authorize(Policy = Permissions.Commands.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommandById(int id)
        {
            var command = await _mediator.Send(new GetCommandByIdQuery());
            return Ok(command);
        }

        [Authorize(Policy = Permissions.Commands.Create)]
        [HttpPost]
        public async Task<IActionResult> PostCommand(AddEditCommandCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [Authorize(Policy =  Permissions.Commands.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommand(int id)
        {
            return Ok(await _mediator.Send(new DeleteCommandCommand { Id = id }));
        }
    }
}
