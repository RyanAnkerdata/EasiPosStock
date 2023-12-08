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
    public abstract class CostCentreBase : Entity<Guid>, IMultiTenant
    {
        public virtual Guid BranchId { get; set; }

        public virtual Guid? TenantId { get; set; }

        [CanBeNull]
        public virtual string? CostCentreReference { get; set; }

        [CanBeNull]
        public virtual string? CostCentreName { get; set; }

        public virtual bool IsDisabled { get; set; }

        protected CostCentreBase()
        {

        }

        public CostCentreBase(Guid id, Guid branchId, bool isDisabled, string? costCentreReference = null, string? costCentreName = null)
        {

            Id = id;
            BranchId = branchId;
            IsDisabled = isDisabled;
            CostCentreReference = costCentreReference;
            CostCentreName = costCentreName;
        }

    }
}