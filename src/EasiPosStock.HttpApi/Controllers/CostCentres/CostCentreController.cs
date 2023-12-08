using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using EasiPosStock.CostCentres;

namespace EasiPosStock.Controllers.CostCentres
{
    [RemoteService]
    [Area("app")]
    [ControllerName("CostCentre")]
    [Route("api/app/cost-centres")]

    public abstract class CostCentreControllerBase : AbpController
    {
        protected ICostCentresAppService _costCentresAppService;

        public CostCentreControllerBase(ICostCentresAppService costCentresAppService)
        {
            _costCentresAppService = costCentresAppService;
        }

        [HttpGet]
        [Route("by-branch")]
        public virtual Task<PagedResultDto<CostCentreDto>> GetListByBranchIdAsync(GetCostCentreListInput input)
        {
            return _costCentresAppService.GetListByBranchIdAsync(input);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<CostCentreDto>> GetListAsync(GetCostCentresInput input)
        {
            return _costCentresAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CostCentreDto> GetAsync(Guid id)
        {
            return _costCentresAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<CostCentreDto> CreateAsync(CostCentreCreateDto input)
        {
            return _costCentresAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CostCentreDto> UpdateAsync(Guid id, CostCentreUpdateDto input)
        {
            return _costCentresAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _costCentresAppService.DeleteAsync(id);
        }
    }
}