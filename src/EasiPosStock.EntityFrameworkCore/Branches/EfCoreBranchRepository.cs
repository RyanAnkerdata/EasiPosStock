using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using EasiPosStock.EntityFrameworkCore;

namespace EasiPosStock.Branches
{
    public abstract class EfCoreBranchRepositoryBase : EfCoreRepository<EasiPosStockDbContext, Branch, Guid>
    {
        public EfCoreBranchRepositoryBase(IDbContextProvider<EasiPosStockDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<List<Branch>> GetListAsync(
            string? filterText = null,
            string? branchName = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, branchName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? BranchConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? branchName = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, branchName);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Branch> ApplyFilter(
            IQueryable<Branch> query,
            string? filterText = null,
            string? branchName = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.BranchName!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(branchName), e => e.BranchName.Contains(branchName));
        }
    }
}