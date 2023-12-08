using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using EasiPosStock.Products;

namespace EasiPosStock.Controllers.Products
{
    [RemoteService]
    [Area("app")]
    [ControllerName("Product")]
    [Route("api/app/products")]

    public abstract class ProductControllerBase : AbpController
    {
        protected IProductsAppService _productsAppService;

        public ProductControllerBase(IProductsAppService productsAppService)
        {
            _productsAppService = productsAppService;
        }

        [HttpGet]
        [Route("by-branch")]
        public virtual Task<PagedResultDto<ProductDto>> GetListByBranchIdAsync(GetProductListInput input)
        {
            return _productsAppService.GetListByBranchIdAsync(input);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<ProductDto>> GetListAsync(GetProductsInput input)
        {
            return _productsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ProductDto> GetAsync(Guid id)
        {
            return _productsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<ProductDto> CreateAsync(ProductCreateDto input)
        {
            return _productsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ProductDto> UpdateAsync(Guid id, ProductUpdateDto input)
        {
            return _productsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _productsAppService.DeleteAsync(id);
        }
    }
}