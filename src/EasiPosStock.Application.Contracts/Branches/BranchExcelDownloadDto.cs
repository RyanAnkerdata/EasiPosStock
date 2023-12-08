using Volo.Abp.Application.Dtos;
using System;

namespace EasiPosStock.Branches
{
    public abstract class BranchExcelDownloadDtoBase
    {
        public string DownloadToken { get; set; } = null!;

        public string? FilterText { get; set; }

        public string? BranchName { get; set; }

        public BranchExcelDownloadDtoBase()
        {

        }
    }
}