using Volo.Abp.Application.Dtos;
using System;

namespace EasiPosStock.CostCentres
{
    public abstract class GetCostCentresInputBase : PagedAndSortedResultRequestDto
    {
        public Guid? BranchId { get; set; }

        public string? FilterText { get; set; }

        public string? CostCentreReference { get; set; }
        public string? CostCentreName { get; set; }
        public bool? IsDisabled { get; set; }

        public GetCostCentresInputBase()
        {

        }
    }
}