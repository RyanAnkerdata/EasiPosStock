using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace EasiPosStock.Products
{
    public abstract class ProductUpdateDtoBase
    {
        public string? ProductName { get; set; }
        public string? ProductCode { get; set; }

    }
}