using EasiPosStock.Samples;
using Xunit;

namespace EasiPosStock.EntityFrameworkCore.Applications;

[Collection(EasiPosStockTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<EasiPosStockEntityFrameworkCoreTestModule>
{

}
