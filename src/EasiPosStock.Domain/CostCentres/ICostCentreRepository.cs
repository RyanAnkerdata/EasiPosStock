using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasiPosStock.CostCentres
{
    public partial interface ICostCentreRepository : IRepository<CostCentre, Guid>
    {
        Task<List<CostCentre>> GetListByBranchIdAsync(
    Guid branchId,
    string? sorting = null,
    int maxResultCount = int.MaxValue,
    int skipCount = 0,
    CancellationToken cancellationToken = default
);

        Task<long> GetCountByBranchIdAsync(Guid branchId, CancellationToken cancellationToken = default);

        Task<List<CostCentre>> GetListAsync(
                    string? filterText = null,
                    string? costCentreReference = null,
                    string? costCentreName = null,
                    bool? isDisabled = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? costCentreReference = null,
            string? costCentreName = null,
            bool? isDisabled = null,
            CancellationToken cancellationToken = default);
    }
}