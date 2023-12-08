using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasiPosStock.CostCentres
{
    public partial interface ICostCentresAppService : IApplicationService
    {
        Task<PagedResultDto<CostCentreDto>> GetListByBranchIdAsync(GetCostCentreListInput input);

        Task<PagedResultDto<CostCentreDto>> GetListAsync(GetCostCentresInput input);

        Task<CostCentreDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<CostCentreDto> CreateAsync(CostCentreCreateDto input);

        Task<CostCentreDto> UpdateAsync(Guid id, CostCentreUpdateDto input);
    }
}