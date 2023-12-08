using Volo.Abp.Application.Dtos;
using System;

namespace EasiPosStock.Products
{
    public class GetProductListInput : PagedAndSortedResultRequestDto
    {
        public Guid BranchId { get; set; }
    }
}