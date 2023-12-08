namespace EasiPosStock.Branches
{
    public static class BranchConsts
    {
        private const string DefaultSorting = "{0}BranchName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Branch." : string.Empty);
        }

    }
}