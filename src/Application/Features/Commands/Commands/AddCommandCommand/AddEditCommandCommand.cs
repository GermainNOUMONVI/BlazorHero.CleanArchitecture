using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Features.Commands.Queries.GetCommandsById;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.Catalog;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Commands.Commands.AddCommandCommand
{
    public class AddEditCommandCommand : IRequest<Result<int>>
    {
        public Command Command { get; set; } = new();
        public List<CommandDetailsResponse> CommandDetails { get; set; } = new();
    }

    public class AddEditCommandCommandHandler : IRequestHandler<AddEditCommandCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICommandDetailsRepository _commandDetailsRepository;
        private readonly IStringLocalizer<AddEditCommandCommandHandler> _localizer;

        public AddEditCommandCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, ICommandDetailsRepository commandDetailsRepository, IStringLocalizer<AddEditCommandCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _commandDetailsRepository = commandDetailsRepository;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditCommandCommand request, CancellationToken cancellationToken)
        {
            void AddDetails(int commandId)
            {
                var commandDetails = _mapper.Map<List<CommandDetails>>(request.CommandDetails);
                foreach (CommandDetails details in commandDetails)
                {
                    details.CommandId = commandId;
                }
                //_commandDetailsRepository.AddRangeCommandDetails(commandDetails);
            }
            var command = request.Command;
            if (command.Id == 0)
            {
                await _unitOfWork.Repository<Command>().AddAsync(command);
                var commandDetails = _mapper.Map<List<CommandDetails>>(request.CommandDetails);
                AddDetails(command.Id);
                return await Result<int>.SuccessAsync(command.Id);
            }
            else
            {
                var contextCommand = await _unitOfWork.Repository<Command>().GetByIdAsync(command.Id);
                if (contextCommand != null)
                {
                    _mapper.Map(command, contextCommand);
                    await _unitOfWork.Repository<Command>().UpdateAsync(contextCommand);
                    var commandCurrentDetails = await _commandDetailsRepository.GetCommandDetails(command.Id);
                    _commandDetailsRepository.DeleteRangeCommandDetails(commandCurrentDetails);
                    AddDetails(command.Id);
                    return await Result<int>.SuccessAsync(command.Id, _localizer["Command Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Command not found"]);
                }
            }
        }
    }
}
