using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using EasiPosStock.Permissions;
using EasiPosStock.Products;

namespace EasiPosStock.Products
{
    [RemoteService(IsEnabled = false)]
    [Authorize(EasiPosStockPermissions.Products.Default)]
    public abstract class ProductsAppServiceBase : ApplicationService
    {

        protected IProductRepository _productRepository;
        protected ProductManager _productManager;

        public ProductsAppServiceBase(IProductRepository productRepository, ProductManager productManager)
        {

            _productRepository = productRepository;
            _productManager = productManager;
        }

        public virtual async Task<PagedResultDto<ProductDto>> GetListAsync(GetProductsInput input)
        {
            var totalCount = await _productRepository.GetCountAsync(input.FilterText, input.ProductName, input.ProductCode);
            var items = await _productRepository.GetListAsync(input.FilterText, input.ProductName, input.ProductCode, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<ProductDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Product>, List<ProductDto>>(items)
            };
        }

        public virtual async Task<ProductDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Product, ProductDto>(await _productRepository.GetAsync(id));
        }

        [Authorize(EasiPosStockPermissions.Products.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _productRepository.DeleteAsync(id);
        }

        [Authorize(EasiPosStockPermissions.Products.Create)]
        public virtual async Task<ProductDto> CreateAsync(ProductCreateDto input)
        {

            var product = await _productManager.CreateAsync(
            input.ProductName, input.ProductCode
            );

            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        [Authorize(EasiPosStockPermissions.Products.Edit)]
        public virtual async Task<ProductDto> UpdateAsync(Guid id, ProductUpdateDto input)
        {

            var product = await _productManager.UpdateAsync(
            id,
            input.ProductName, input.ProductCode
            );

            return ObjectMapper.Map<Product, ProductDto>(product);
        }
    }
}