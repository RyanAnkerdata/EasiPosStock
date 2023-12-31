using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using Volo.Abp.BlazoriseUI.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using EasiPosStock.Products;
using EasiPosStock.Permissions;
using EasiPosStock.Shared;


namespace EasiPosStock.Blazor.Pages
{
    public partial class Products
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        protected bool ShowAdvancedFilters { get; set; }
        private IReadOnlyList<ProductDto> ProductList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateProduct { get; set; }
        private bool CanEditProduct { get; set; }
        private bool CanDeleteProduct { get; set; }
        private ProductCreateDto NewProduct { get; set; }
        private Validations NewProductValidations { get; set; } = new();
        private ProductUpdateDto EditingProduct { get; set; }
        private Validations EditingProductValidations { get; set; } = new();
        private Guid EditingProductId { get; set; }
        private Modal CreateProductModal { get; set; } = new();
        private Modal EditProductModal { get; set; } = new();
        private GetProductsInput Filter { get; set; }
        private DataGridEntityActionsColumn<ProductDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "product-create-tab";
        protected string SelectedEditTab = "product-edit-tab";
        private ProductDto? SelectedProduct;
        
        
        
        
        public Products()
        {
            NewProduct = new ProductCreateDto();
            EditingProduct = new ProductUpdateDto();
            Filter = new GetProductsInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            ProductList = new List<ProductDto>();
            
            
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
            
        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:Products"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            
            
            Toolbar.AddButton(L["NewProduct"], async () =>
            {
                await OpenCreateProductModalAsync();
            }, IconName.Add, requiredPolicyName: EasiPosStockPermissions.Products.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateProduct = await AuthorizationService
                .IsGrantedAsync(EasiPosStockPermissions.Products.Create);
            CanEditProduct = await AuthorizationService
                            .IsGrantedAsync(EasiPosStockPermissions.Products.Edit);
            CanDeleteProduct = await AuthorizationService
                            .IsGrantedAsync(EasiPosStockPermissions.Products.Delete);
                            
                            
        }

        private async Task GetProductsAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await ProductsAppService.GetListAsync(Filter);
            ProductList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetProductsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<ProductDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetProductsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateProductModalAsync()
        {
            NewProduct = new ProductCreateDto{
                
                
            };
            await NewProductValidations.ClearAll();
            await CreateProductModal.Show();
        }

        private async Task CloseCreateProductModalAsync()
        {
            NewProduct = new ProductCreateDto{
                
                
            };
            await CreateProductModal.Hide();
        }

        private async Task OpenEditProductModalAsync(ProductDto input)
        {
            var product = await ProductsAppService.GetAsync(input.Id);
            
            EditingProductId = product.Id;
            EditingProduct = ObjectMapper.Map<ProductDto, ProductUpdateDto>(product);
            await EditingProductValidations.ClearAll();
            await EditProductModal.Show();
        }

        private async Task DeleteProductAsync(ProductDto input)
        {
            await ProductsAppService.DeleteAsync(input.Id);
            await GetProductsAsync();
        }

        private async Task CreateProductAsync()
        {
            try
            {
                if (await NewProductValidations.ValidateAll() == false)
                {
                    return;
                }

                await ProductsAppService.CreateAsync(NewProduct);
                await GetProductsAsync();
                await CloseCreateProductModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditProductModalAsync()
        {
            await EditProductModal.Hide();
        }

        private async Task UpdateProductAsync()
        {
            try
            {
                if (await EditingProductValidations.ValidateAll() == false)
                {
                    return;
                }

                await ProductsAppService.UpdateAsync(EditingProductId, EditingProduct);
                await GetProductsAsync();
                await EditProductModal.Hide();                
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private void OnSelectedCreateTabChanged(string name)
        {
            SelectedCreateTab = name;
        }

        private void OnSelectedEditTabChanged(string name)
        {
            SelectedEditTab = name;
        }

        protected virtual async Task OnProductNameChangedAsync(string? productName)
        {
            Filter.ProductName = productName;
            await SearchAsync();
        }
        protected virtual async Task OnProductCodeChangedAsync(string? productCode)
        {
            Filter.ProductCode = productCode;
            await SearchAsync();
        }
        





    }
}
