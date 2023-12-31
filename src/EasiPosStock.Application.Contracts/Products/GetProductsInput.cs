using Volo.Abp.Application.Dtos;
using System;

namespace EasiPosStock.Products
{
    public abstract class GetProductsInputBase : PagedAndSortedResultRequestDto
    {

        public string? FilterText { get; set; }

        public string? ProductName { get; set; }
        public string? ProductCode { get; set; }

        public GetProductsInputBase()
        {

        }
    }
}