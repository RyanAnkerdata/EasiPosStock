using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace EasiPosStock.Branches
{
    public abstract class BranchManagerBase : DomainService
    {
        protected IBranchRepository _branchRepository;

        public BranchManagerBase(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public virtual async Task<Branch> CreateAsync(
        string branchName)
        {
            Check.NotNullOrWhiteSpace(branchName, nameof(branchName));

            var branch = new Branch(
             GuidGenerator.Create(),
             branchName
             );

            return await _branchRepository.InsertAsync(branch);
        }

        public virtual async Task<Branch> UpdateAsync(
            Guid id,
            string branchName, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(branchName, nameof(branchName));

            var branch = await _branchRepository.GetAsync(id);

            branch.BranchName = branchName;

            branch.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _branchRepository.UpdateAsync(branch);
        }

    }
}