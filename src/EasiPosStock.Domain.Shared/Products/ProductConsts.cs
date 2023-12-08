namespace EasiPosStock.Products
{
    public static class ProductConsts
    {
        private const string DefaultSorting = "{0}ProductName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Product." : string.Empty);
        }

    }
}