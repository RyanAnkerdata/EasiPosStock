using Xunit;

namespace EasiPosStock.EntityFrameworkCore;

[CollectionDefinition(EasiPosStockTestConsts.CollectionDefinitionName)]
public class EasiPosStockEntityFrameworkCoreCollection : ICollectionFixture<EasiPosStockEntityFrameworkCoreFixture>
{

}
