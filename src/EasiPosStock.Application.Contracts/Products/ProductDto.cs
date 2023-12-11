using System;
using System.Collections.Generic;

using Volo.Abp.Application.Dtos;

namespace EasiPosStock.Products
{
    public abstract class ProductDtoBase : EntityDto<Guid>
    {
        public string? ProductName { get; set; }
        public string? ProductCode { get; set; }

    }
}