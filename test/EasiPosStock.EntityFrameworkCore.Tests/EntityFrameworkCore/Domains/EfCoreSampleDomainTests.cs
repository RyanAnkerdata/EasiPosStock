using EasiPosStock.Samples;
using Xunit;

namespace EasiPosStock.EntityFrameworkCore.Domains;

[Collection(EasiPosStockTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<EasiPosStockEntityFrameworkCoreTestModule>
{

}
