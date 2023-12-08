using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace EasiPosStock.Branches
{
    public abstract class BranchesAppServiceTests<TStartupModule> : EasiPosStockApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly IBranchesAppService _branchesAppService;
        private readonly IRepository<Branch, Guid> _branchRepository;

        public BranchesAppServiceTests()
        {
            _branchesAppService = GetRequiredService<IBranchesAppService>();
            _branchRepository = GetRequiredService<IRepository<Branch, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _branchesAppService.GetListAsync(new GetBranchesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("c6a842ff-38ec-4671-beef-a70931e8a7f4")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("a432a0c2-1634-4b66-92af-be87edb985ae")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _branchesAppService.GetAsync(Guid.Parse("c6a842ff-38ec-4671-beef-a70931e8a7f4"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("c6a842ff-38ec-4671-beef-a70931e8a7f4"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new BranchCreateDto
            {
                BranchName = "c86cce080c934f2fab1eede6ba1a6c05d84115a2c42b489e88ca93e5a1703"
            };

            // Act
            var serviceResult = await _branchesAppService.CreateAsync(input);

            // Assert
            var result = await _branchRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.BranchName.ShouldBe("c86cce080c934f2fab1eede6ba1a6c05d84115a2c42b489e88ca93e5a1703");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new BranchUpdateDto()
            {
                BranchName = "cacbaa0f4e894809b8d"
            };

            // Act
            var serviceResult = await _branchesAppService.UpdateAsync(Guid.Parse("c6a842ff-38ec-4671-beef-a70931e8a7f4"), input);

            // Assert
            var result = await _branchRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.BranchName.ShouldBe("cacbaa0f4e894809b8d");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _branchesAppService.DeleteAsync(Guid.Parse("c6a842ff-38ec-4671-beef-a70931e8a7f4"));

            // Assert
            var result = await _branchRepository.FindAsync(c => c.Id == Guid.Parse("c6a842ff-38ec-4671-beef-a70931e8a7f4"));

            result.ShouldBeNull();
        }
    }
}