using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasiPosStock.Branches
{
    public partial interface IBranchRepository : IRepository<Branch, Guid>
    {
        Task<List<Branch>> GetListAsync(
            string? filterText = null,
            string? branchName = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? branchName = null,
            CancellationToken cancellationToken = default);
    }
}