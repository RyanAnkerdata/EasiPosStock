using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace EasiPosStock.CostCentres
{
    public abstract class CostCentreManagerBase : DomainService
    {
        protected ICostCentreRepository _costCentreRepository;

        public CostCentreManagerBase(ICostCentreRepository costCentreRepository)
        {
            _costCentreRepository = costCentreRepository;
        }

        public virtual async Task<CostCentre> CreateAsync(
        Guid branchId, bool isDisabled, string? costCentreReference = null, string? costCentreName = null)
        {

            var costCentre = new CostCentre(
             GuidGenerator.Create(),
             branchId, isDisabled, costCentreReference, costCentreName
             );

            return await _costCentreRepository.InsertAsync(costCentre);
        }

        public virtual async Task<CostCentre> UpdateAsync(
            Guid id,
            Guid branchId, bool isDisabled, string? costCentreReference = null, string? costCentreName = null
        )
        {

            var costCentre = await _costCentreRepository.GetAsync(id);

            costCentre.BranchId = branchId;
            costCentre.IsDisabled = isDisabled;
            costCentre.CostCentreReference = costCentreReference;
            costCentre.CostCentreName = costCentreName;

            return await _costCentreRepository.UpdateAsync(costCentre);
        }

    }
}