using System;
using System.Collections.Generic;
using EasiPosStock.CostCentres;
using EasiPosStock.Products;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace EasiPosStock.Branches
{
    public abstract class BranchDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string BranchName { get; set; } = null!;

        public string ConcurrencyStamp { get; set; } = null!;

        public List<CostCentreDto> CostCentres { get; set; } = new();
        public List<ProductDto> Products { get; set; } = new();
    }
}