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
    public class EfCoreProductRepository : EfCoreProductRepositoryBase, IProductRepository
    {
        public EfCoreProductRepository(IDbContextProvider<EasiPosStockDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}