using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using EasiPosStock.Branches;

namespace EasiPosStock.Branches
{
    public class BranchesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IBranchRepository _branchRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public BranchesDataSeedContributor(IBranchRepository branchRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _branchRepository = branchRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _branchRepository.InsertAsync(new Branch
            (
                id: Guid.Parse("c6a842ff-38ec-4671-beef-a70931e8a7f4"),
                branchName: "ad7af65d056847549f2e7c0048d1794f905af93160484498b71abdb89c83ad78987ca4c630474598b62c86c6f22a221d"
            ));

            await _branchRepository.InsertAsync(new Branch
            (
                id: Guid.Parse("a432a0c2-1634-4b66-92af-be87edb985ae"),
                branchName: "3c064ad4258b4f368ae20d05eddb1fd5016c7450710e45f2b6cd12a79426885229d58"
            ));

            await _unitOfWorkManager!.Current!.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}