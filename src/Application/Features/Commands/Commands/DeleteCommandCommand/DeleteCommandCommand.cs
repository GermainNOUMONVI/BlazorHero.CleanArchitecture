using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.Catalog;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Commands.Commands.DeleteCommandCommand
{
    public class DeleteCommandCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteCommandCommandHandler : IRequestHandler<DeleteCommandCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly ICommandDetailsRepository _commandDetailsRepository;
        private readonly IStringLocalizer<DeleteCommandCommandHandler> _localizer;

        public DeleteCommandCommandHandler(IUnitOfWork<int> unitOfWork, ICommandDetailsRepository commandDetailsRepository, IStringLocalizer<DeleteCommandCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _commandDetailsRepository = commandDetailsRepository;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteCommandCommand request, CancellationToken cancellationToken)
        {
            var command = await _unitOfWork.Repository<Command>().GetByIdAsync(request.Id);
            if (command != null)
            {
                var details = await _commandDetailsRepository.GetCommandDetails(command.Id);
                await _unitOfWork.Repository<Command>().DeleteAsync(command);
                _commandDetailsRepository.DeleteRangeCommandDetails(details);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllCommandsCacheKey);
                return await Result<int>.SuccessAsync(request.Id, _localizer["Command Deleted"]);
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Command not found"]);
            }
        }
    }
}
