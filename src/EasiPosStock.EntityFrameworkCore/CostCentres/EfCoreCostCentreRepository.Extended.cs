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
    public class EfCoreCostCentreRepository : EfCoreCostCentreRepositoryBase, ICostCentreRepository
    {
        public EfCoreCostCentreRepository(IDbContextProvider<EasiPosStockDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}