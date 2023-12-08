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

namespace EasiPosStock.Products
{
    public abstract class EfCoreProductRepositoryBase : EfCoreRepository<EasiPosStockDbContext, Product, Guid>
    {
        public EfCoreProductRepositoryBase(IDbContextProvider<EasiPosStockDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<List<Product>> GetListByBranchIdAsync(
           Guid branchId,
           string? sorting = null,
           int maxResultCount = int.MaxValue,
           int skipCount = 0,
           CancellationToken cancellationToken = default)
        {
            var query = (await GetQueryableAsync()).Where(x => x.BranchId == branchId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ProductConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountByBranchIdAsync(Guid branchId, CancellationToken cancellationToken = default)
        {
            return await (await GetQueryableAsync()).Where(x => x.BranchId == branchId).CountAsync(cancellationToken);
        }

        public virtual async Task<List<Product>> GetListAsync(
            string? filterText = null,
            string? productName = null,
            string? productCode = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, productName, productCode);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ProductConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? productName = null,
            string? productCode = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, productName, productCode);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Product> ApplyFilter(
            IQueryable<Product> query,
            string? filterText = null,
            string? productName = null,
            string? productCode = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ProductName!.Contains(filterText!) || e.ProductCode!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(productName), e => e.ProductName.Contains(productName))
                    .WhereIf(!string.IsNullOrWhiteSpace(productCode), e => e.ProductCode.Contains(productCode));
        }
    }
}