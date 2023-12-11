using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasiPosStock.Products
{
    public partial interface IProductsAppService : IApplicationService
    {

        Task<PagedResultDto<ProductDto>> GetListAsync(GetProductsInput input);

        Task<ProductDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ProductDto> CreateAsync(ProductCreateDto input);

        Task<ProductDto> UpdateAsync(Guid id, ProductUpdateDto input);
    }
}