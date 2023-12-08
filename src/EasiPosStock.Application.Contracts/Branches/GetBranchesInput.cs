using Volo.Abp.Application.Dtos;
using System;

namespace EasiPosStock.Branches
{
    public abstract class GetBranchesInputBase : PagedAndSortedResultRequestDto
    {

        public string? FilterText { get; set; }

        public string? BranchName { get; set; }

        public GetBranchesInputBase()
        {

        }
    }
}