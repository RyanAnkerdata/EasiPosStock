using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasiPosStock.Products
{
    public partial interface IProductRepository : IRepository<Product, Guid>
    {
        Task<List<Product>> GetListByBranchIdAsync(
    Guid branchId,
    string? sorting = null,
    int maxResultCount = int.MaxValue,
    int skipCount = 0,
    CancellationToken cancellationToken = default
);

        Task<long> GetCountByBranchIdAsync(Guid branchId, CancellationToken cancellationToken = default);

        Task<List<Product>> GetListAsync(
                    string? filterText = null,
                    string? productName = null,
                    string? productCode = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? productName = null,
            string? productCode = null,
            CancellationToken cancellationToken = default);
    }
}