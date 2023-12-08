using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace EasiPosStock.Branches
{
    public abstract class BranchCreateDtoBase
    {
        [Required(AllowEmptyStrings = true)]
        public string BranchName { get; set; } = null!;
    }
}