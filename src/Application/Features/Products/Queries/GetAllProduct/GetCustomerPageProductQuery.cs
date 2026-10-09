using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.Catalog;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using LazyCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Products.Queries.GetAllProduct
{
    public class GetCustomerPageProductQuery : IRequest<Result<List<GetAllProductResponse>>>
    {
    }

    public class GetCustomerPageProductQueryHandler : IRequestHandler<GetCustomerPageProductQuery, Result<List<GetAllProductResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetCustomerPageProductQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache appCache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = appCache;
        }

        public async Task<Result<List<GetAllProductResponse>>> Handle(GetCustomerPageProductQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<Product>>> products = () => _unitOfWork.Repository<Product>().GetAllAsync();
            var productList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllProductCacheKey, products);
            var domain = "https://localhost:44390/";
            var mappedProduct = _mapper.Map<List<GetAllProductResponse>>(productList);
            foreach(var product in mappedProduct)
            {
                product.ImageDataURL = domain + product.ImageDataURL;
            }
            return await Result<List<GetAllProductResponse>>.SuccessAsync(mappedProduct);
        }
    }
}
