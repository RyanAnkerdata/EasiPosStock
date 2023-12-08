using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace EasiPosStock.CostCentres
{
    public abstract class CostCentreCreateDtoBase
    {
        public Guid BranchId { get; set; }
        public string? CostCentreReference { get; set; }
        public string? CostCentreName { get; set; }
        public bool IsDisabled { get; set; }
    }
}