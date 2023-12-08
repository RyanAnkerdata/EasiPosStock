using Volo.Abp.Application.Dtos;
using System;

namespace EasiPosStock.CostCentres
{
    public class GetCostCentreListInput : PagedAndSortedResultRequestDto
    {
        public Guid BranchId { get; set; }
    }
}