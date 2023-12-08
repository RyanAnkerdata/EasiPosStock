using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using EasiPosStock.Branches;
using EasiPosStock.EntityFrameworkCore;
using Xunit;

namespace EasiPosStock.EntityFrameworkCore.Domains.Branches
{
    public class BranchRepositoryTests : EasiPosStockEntityFrameworkCoreTestBase
    {
        private readonly IBranchRepository _branchRepository;

        public BranchRepositoryTests()
        {
            _branchRepository = GetRequiredService<IBranchRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _branchRepository.GetListAsync(
                    branchName: "ad7af65d056847549f2e7c0048d1794f905af93160484498b71abdb89c83ad78987ca4c630474598b62c86c6f22a221d"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("c6a842ff-38ec-4671-beef-a70931e8a7f4"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _branchRepository.GetCountAsync(
                    branchName: "3c064ad4258b4f368ae20d05eddb1fd5016c7450710e45f2b6cd12a79426885229d58"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}