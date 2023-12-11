using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace EasiPosStock.Products
{
    public abstract class ProductBase : Entity<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [CanBeNull]
        public virtual string? ProductName { get; set; }

        [CanBeNull]
        public virtual string? ProductCode { get; set; }

        protected ProductBase()
        {

        }

        public ProductBase(Guid id, string? productName = null, string? productCode = null)
        {

            Id = id;
            ProductName = productName;
            ProductCode = productCode;
        }

    }
}