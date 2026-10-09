
using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Features.Products.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.Catalog;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using LazyCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Commands.Queries.GetAllCommands
{
    public class GetAllCommandsQuery : IRequest<Result<List<GetAllCommandsResponse>>>
    {
        /*public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; }*/

        public GetAllCommandsQuery(/*int pageNumber, int pageSize, string searchString, string orderBy*/)
        {
            /*PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
            if(!string.IsNullOrWhiteSpace(searchString) )
            {
                this.OrderBy = orderBy.Split(',');
            }*/
        }
    }

    public class GetAllCommandsQueryHandler : IRequestHandler<GetAllCommandsQuery, Result<List<GetAllCommandsResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IAppCache _cache;
        private readonly IMapper _mapper;

        public GetAllCommandsQueryHandler(IUnitOfWork<int> unitOfWork, IAppCache cache, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
            _mapper = mapper;
        }
        public async Task<Result<List<GetAllCommandsResponse>>> Handle(GetAllCommandsQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<Command>>> getAllCammands = () => _unitOfWork.Repository<Command>().GetAllAsync();
            var commandList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllCommandsCacheKey, getAllCammands);
            var mappedCommands = _mapper.Map<List<GetAllCommandsResponse>>(commandList);
            return await Result<List<GetAllCommandsResponse>>.SuccessAsync(mappedCommands);
        }
    }
}
