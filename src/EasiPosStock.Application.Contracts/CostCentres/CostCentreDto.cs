using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;

namespace EasiPosStock.CostCentres
{
    public abstract class CostCentreDtoBase : EntityDto<Guid>
    {
        public Guid BranchId { get; set; }
        public string? CostCentreReference { get; set; }
        public string? CostCentreName { get; set; }
        public bool IsDisabled { get; set; }

    }
}