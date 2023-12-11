using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using EasiPosStock.CostCentres;

using Volo.Abp;

namespace EasiPosStock.Branches
{
    public abstract class BranchBase : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string BranchName { get; set; }

        public ICollection<CostCentre> CostCentres { get; private set; }

        protected BranchBase()
        {

        }

        public BranchBase(Guid id, string branchName)
        {

            Id = id;
            Check.NotNull(branchName, nameof(branchName));
            BranchName = branchName;
            CostCentres = new Collection<CostCentre>();
        }

    }
}