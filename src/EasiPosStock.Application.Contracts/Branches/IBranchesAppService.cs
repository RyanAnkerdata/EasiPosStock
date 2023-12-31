using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using EasiPosStock.Shared;

namespace EasiPosStock.Branches
{
    public partial interface IBranchesAppService : IApplicationService
    {

        Task<PagedResultDto<BranchDto>> GetListAsync(GetBranchesInput input);

        Task<BranchDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<BranchDto> CreateAsync(BranchCreateDto input);

        Task<BranchDto> UpdateAsync(Guid id, BranchUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(BranchExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}