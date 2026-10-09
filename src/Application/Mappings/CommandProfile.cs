using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Features.Commands.Commands.AddCommandCommand;
using BlazorHero.CleanArchitecture.Application.Features.Commands.Queries.GetAllCommands;
using BlazorHero.CleanArchitecture.Application.Features.Commands.Queries.GetCommandsById;
using BlazorHero.CleanArchitecture.Domain.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Mappings
{
    public class CommandProfile : Profile
    {
        public CommandProfile()
        {
            CreateMap<AddCommandRequest, Command>();
            CreateMap<Command, Command>();
            //CreateMap<AddCommandDetailsRequest, CommandDetails>();
            CreateMap<CommandDetailsResponse, CommandDetails>();
            CreateMap<Command, GetAllCommandsResponse>();
            CreateMap<CommandDetails, CommandDetailsResponse>().ForMember(nameof(CommandDetailsResponse.ProductName), opt => opt.MapFrom(c => c.Product.Name));
        }
    }
}
