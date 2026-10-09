using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.Catalog;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Commands.Queries.GetCommandsById
{
    public class GetCommandByIdQuery : IRequest<Result<GetCommandByIdResponse>>
    {
        public int Id { get; set; }
    }

    public class GetCommandByIdQueryHandler : IRequestHandler<GetCommandByIdQuery, Result<GetCommandByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICommandDetailsRepository _commandDetailsRepository;

        public GetCommandByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, ICommandDetailsRepository commandDetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _commandDetailsRepository = commandDetailsRepository;
        }

        public async Task<Result<GetCommandByIdResponse>> Handle(GetCommandByIdQuery request, CancellationToken cancellationToken)
        {
            var command = await _unitOfWork.Repository<Command>().GetByIdAsync(request.Id);
            var mappedCommand = _mapper.Map<GetCommandByIdResponse>(command);
            var commandDetails = _commandDetailsRepository.GetCommandDetails(request.Id);
            var mappedCommandDetails= await _mapper.Map<Task<List<CommandDetailsResponse>>>(commandDetails);
            mappedCommand.ProductsList = mappedCommandDetails;
            return await Result<GetCommandByIdResponse>.SuccessAsync(mappedCommand);
        }
    }
}
