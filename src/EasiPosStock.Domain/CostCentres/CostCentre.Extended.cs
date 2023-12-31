using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace EasiPosStock.CostCentres
{
    public class CostCentre : CostCentreBase
    {
        //<suite-custom-code-autogenerated>
        protected CostCentre()
        {

        }

        public CostCentre(Guid id, Guid branchId, bool isDisabled, string? costCentreReference = null, string? costCentreName = null)
            : base(id, branchId, isDisabled, costCentreReference, costCentreName)
        {
        }
        //</suite-custom-code-autogenerated>

        //Write your custom code...
    }
}