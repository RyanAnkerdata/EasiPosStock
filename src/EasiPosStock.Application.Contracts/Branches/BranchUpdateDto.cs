using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace EasiPosStock.Branches
{
    public abstract class BranchUpdateDtoBase : IHasConcurrencyStamp
    {
        [Required(AllowEmptyStrings = true)]
        public string BranchName { get; set; } = null!;

        public string ConcurrencyStamp { get; set; } = null!;
    }
}