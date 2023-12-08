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
using EasiPosStock.CostCentres;

namespace EasiPosStock.CostCentres
{
    [RemoteService(IsEnabled = false)]
    [Authorize(EasiPosStockPermissions.CostCentres.Default)]
    public abstract class CostCentresAppServiceBase : ApplicationService
    {

        protected ICostCentreRepository _costCentreRepository;
        protected CostCentreManager _costCentreManager;

        public CostCentresAppServiceBase(ICostCentreRepository costCentreRepository, CostCentreManager costCentreManager)
        {

            _costCentreRepository = costCentreRepository;
            _costCentreManager = costCentreManager;
        }

        public virtual async Task<PagedResultDto<CostCentreDto>> GetListByBranchIdAsync(GetCostCentreListInput input)
        {
            var costCentres = await _costCentreRepository.GetListByBranchIdAsync(
                input.BranchId,
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount);

            return new PagedResultDto<CostCentreDto>
            {
                TotalCount = await _costCentreRepository.GetCountByBranchIdAsync(input.BranchId),
                Items = ObjectMapper.Map<List<CostCentre>, List<CostCentreDto>>(costCentres)
            };
        }

        public virtual async Task<PagedResultDto<CostCentreDto>> GetListAsync(GetCostCentresInput input)
        {
            var totalCount = await _costCentreRepository.GetCountAsync(input.FilterText, input.CostCentreReference, input.CostCentreName, input.IsDisabled);
            var items = await _costCentreRepository.GetListAsync(input.FilterText, input.CostCentreReference, input.CostCentreName, input.IsDisabled, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CostCentreDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CostCentre>, List<CostCentreDto>>(items)
            };
        }

        public virtual async Task<CostCentreDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CostCentre, CostCentreDto>(await _costCentreRepository.GetAsync(id));
        }

        [Authorize(EasiPosStockPermissions.CostCentres.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _costCentreRepository.DeleteAsync(id);
        }

        [Authorize(EasiPosStockPermissions.CostCentres.Create)]
        public virtual async Task<CostCentreDto> CreateAsync(CostCentreCreateDto input)
        {

            var costCentre = await _costCentreManager.CreateAsync(input.BranchId,
            input.IsDisabled, input.CostCentreReference, input.CostCentreName
            );

            return ObjectMapper.Map<CostCentre, CostCentreDto>(costCentre);
        }

        [Authorize(EasiPosStockPermissions.CostCentres.Edit)]
        public virtual async Task<CostCentreDto> UpdateAsync(Guid id, CostCentreUpdateDto input)
        {

            var costCentre = await _costCentreManager.UpdateAsync(
            id, input.BranchId,
            input.IsDisabled, input.CostCentreReference, input.CostCentreName
            );

            return ObjectMapper.Map<CostCentre, CostCentreDto>(costCentre);
        }
    }
}