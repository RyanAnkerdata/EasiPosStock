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

namespace EasiPosStock.CostCentres
{
    public abstract class EfCoreCostCentreRepositoryBase : EfCoreRepository<EasiPosStockDbContext, CostCentre, Guid>
    {
        public EfCoreCostCentreRepositoryBase(IDbContextProvider<EasiPosStockDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<List<CostCentre>> GetListByBranchIdAsync(
           Guid branchId,
           string? sorting = null,
           int maxResultCount = int.MaxValue,
           int skipCount = 0,
           CancellationToken cancellationToken = default)
        {
            var query = (await GetQueryableAsync()).Where(x => x.BranchId == branchId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CostCentreConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountByBranchIdAsync(Guid branchId, CancellationToken cancellationToken = default)
        {
            return await (await GetQueryableAsync()).Where(x => x.BranchId == branchId).CountAsync(cancellationToken);
        }

        public virtual async Task<List<CostCentre>> GetListAsync(
            string? filterText = null,
            string? costCentreReference = null,
            string? costCentreName = null,
            bool? isDisabled = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, costCentreReference, costCentreName, isDisabled);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CostCentreConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? costCentreReference = null,
            string? costCentreName = null,
            bool? isDisabled = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, costCentreReference, costCentreName, isDisabled);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<CostCentre> ApplyFilter(
            IQueryable<CostCentre> query,
            string? filterText = null,
            string? costCentreReference = null,
            string? costCentreName = null,
            bool? isDisabled = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.CostCentreReference!.Contains(filterText!) || e.CostCentreName!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(costCentreReference), e => e.CostCentreReference.Contains(costCentreReference))
                    .WhereIf(!string.IsNullOrWhiteSpace(costCentreName), e => e.CostCentreName.Contains(costCentreName))
                    .WhereIf(isDisabled.HasValue, e => e.IsDisabled == isDisabled);
        }
    }
}