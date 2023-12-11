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
using EasiPosStock.Branches;
using EasiPosStock.Permissions;
using EasiPosStock.Shared;
using EasiPosStock.CostCentres; 


namespace EasiPosStock.Blazor.Pages
{
    public partial class Branches
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        protected bool ShowAdvancedFilters { get; set; }
        private IReadOnlyList<BranchDto> BranchList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateBranch { get; set; }
        private bool CanEditBranch { get; set; }
        private bool CanDeleteBranch { get; set; }
        private BranchCreateDto NewBranch { get; set; }
        private Validations NewBranchValidations { get; set; } = new();
        private BranchUpdateDto EditingBranch { get; set; }
        private Validations EditingBranchValidations { get; set; } = new();
        private Guid EditingBranchId { get; set; }
        private Modal CreateBranchModal { get; set; } = new();
        private Modal EditBranchModal { get; set; } = new();
        private GetBranchesInput Filter { get; set; }
        private DataGridEntityActionsColumn<BranchDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "branch-create-tab";
        protected string SelectedEditTab = "branch-edit-tab";
        private BranchDto? SelectedBranch;
        
        
                #region Child Entities
        
                #region CostCentres

                private bool CanListCostCentre { get; set; }
                private bool CanCreateCostCentre { get; set; }
                private bool CanEditCostCentre { get; set; }
                private bool CanDeleteCostCentre { get; set; }
                private CostCentreCreateDto NewCostCentre { get; set; }
                private Dictionary<Guid, DataGrid<CostCentreDto>> CostCentreDataGrids { get; set; } = new();
                private int CostCentrePageSize { get; } = 5;
                private DataGridEntityActionsColumn<CostCentreDto> CostCentreEntityActionsColumns { get; set; } = new();
                private Validations NewCostCentreValidations { get; set; } = new();
                private Modal CreateCostCentreModal { get; set; } = new();
                private Guid EditingCostCentreId { get; set; }
                private CostCentreUpdateDto EditingCostCentre { get; set; }
                private Validations EditingCostCentreValidations { get; set; } = new();
                private Modal EditCostCentreModal { get; set; } = new();
    
            
                #endregion
        
        #endregion
        
        public Branches()
        {
            NewBranch = new BranchCreateDto();
            EditingBranch = new BranchUpdateDto();
            Filter = new GetBranchesInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            BranchList = new List<BranchDto>();
            
            NewCostCentre = new CostCentreCreateDto();
EditingCostCentre = new CostCentreUpdateDto();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
            
        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:Branches"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () =>{ await DownloadAsExcelAsync(); }, IconName.Download);
            
            Toolbar.AddButton(L["NewBranch"], async () =>
            {
                await OpenCreateBranchModalAsync();
            }, IconName.Add, requiredPolicyName: EasiPosStockPermissions.Branches.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateBranch = await AuthorizationService
                .IsGrantedAsync(EasiPosStockPermissions.Branches.Create);
            CanEditBranch = await AuthorizationService
                            .IsGrantedAsync(EasiPosStockPermissions.Branches.Edit);
            CanDeleteBranch = await AuthorizationService
                            .IsGrantedAsync(EasiPosStockPermissions.Branches.Delete);
                            
            
            #region CostCentres
            CanListCostCentre = await AuthorizationService
                .IsGrantedAsync(EasiPosStockPermissions.CostCentres.Default);
            CanCreateCostCentre = await AuthorizationService
                .IsGrantedAsync(EasiPosStockPermissions.CostCentres.Create);
            CanEditCostCentre = await AuthorizationService
                .IsGrantedAsync(EasiPosStockPermissions.CostCentres.Edit);
            CanDeleteCostCentre = await AuthorizationService
                .IsGrantedAsync(EasiPosStockPermissions.CostCentres.Delete);
            #endregion                
        }

        private async Task GetBranchesAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await BranchesAppService.GetListAsync(Filter);
            BranchList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetBranchesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private  async Task DownloadAsExcelAsync()
        {
            var token = (await BranchesAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("EasiPosStock") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/app/branches/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}&BranchName={Filter.BranchName}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<BranchDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetBranchesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateBranchModalAsync()
        {
            NewBranch = new BranchCreateDto{
                
                
            };
            await NewBranchValidations.ClearAll();
            await CreateBranchModal.Show();
        }

        private async Task CloseCreateBranchModalAsync()
        {
            NewBranch = new BranchCreateDto{
                
                
            };
            await CreateBranchModal.Hide();
        }

        private async Task OpenEditBranchModalAsync(BranchDto input)
        {
            var branch = await BranchesAppService.GetAsync(input.Id);
            
            EditingBranchId = branch.Id;
            EditingBranch = ObjectMapper.Map<BranchDto, BranchUpdateDto>(branch);
            await EditingBranchValidations.ClearAll();
            await EditBranchModal.Show();
        }

        private async Task DeleteBranchAsync(BranchDto input)
        {
            await BranchesAppService.DeleteAsync(input.Id);
            await GetBranchesAsync();
        }

        private async Task CreateBranchAsync()
        {
            try
            {
                if (await NewBranchValidations.ValidateAll() == false)
                {
                    return;
                }

                await BranchesAppService.CreateAsync(NewBranch);
                await GetBranchesAsync();
                await CloseCreateBranchModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditBranchModalAsync()
        {
            await EditBranchModal.Hide();
        }

        private async Task UpdateBranchAsync()
        {
            try
            {
                if (await EditingBranchValidations.ValidateAll() == false)
                {
                    return;
                }

                await BranchesAppService.UpdateAsync(EditingBranchId, EditingBranch);
                await GetBranchesAsync();
                await EditBranchModal.Hide();                
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

        protected virtual async Task OnBranchNameChangedAsync(string? branchName)
        {
            Filter.BranchName = branchName;
            await SearchAsync();
        }
        


    private bool ShouldShowDetailRow()
    {
        return CanListCostCentre;
    }
    
    public string SelectedChildTab { get; set; } = "costcentre-tab";
        
    private Task OnSelectedChildTabChanged(string name)
    {
        SelectedChildTab = name;
    
        return Task.CompletedTask;
    }


        #region CostCentres
        
        private async Task OnCostCentreDataGridReadAsync(DataGridReadDataEventArgs<CostCentreDto> e, Guid branchId)
        {
            var sorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");

            var currentPage = e.Page;
            await SetCostCentresAsync(branchId, currentPage, sorting: sorting);
            await InvokeAsync(StateHasChanged);
        }
        
        private async Task SetCostCentresAsync(Guid branchId, int currentPage = 1, string? sorting = null)
        {
            var branch = BranchList.FirstOrDefault(x => x.Id == branchId);
            if(branch == null)
            {
                return;
            }

            var costCentres = await CostCentresAppService.GetListByBranchIdAsync(new GetCostCentreListInput 
            {
                BranchId = branchId,
                MaxResultCount = CostCentrePageSize,
                SkipCount = (currentPage - 1) * CostCentrePageSize,
                Sorting = sorting
            });

            branch.CostCentres = costCentres.Items.ToList();

            var costCentreDataGrid = CostCentreDataGrids[branchId];
            
            costCentreDataGrid.CurrentPage = currentPage;
            costCentreDataGrid.TotalItems = (int)costCentres.TotalCount;
        }
        
        private async Task OpenEditCostCentreModalAsync(CostCentreDto input)
        {
            var costCentre = await CostCentresAppService.GetAsync(input.Id);

            EditingCostCentreId = costCentre.Id;
            EditingCostCentre = ObjectMapper.Map<CostCentreDto, CostCentreUpdateDto>(costCentre);
            await EditingCostCentreValidations.ClearAll();
            await EditCostCentreModal.Show();
        }
        
        private async Task CloseEditCostCentreModalAsync()
        {
            await EditCostCentreModal.Hide();
        }
        
        private async Task UpdateCostCentreAsync()
        {
            try
            {
                if (await EditingCostCentreValidations.ValidateAll() == false)
                {
                    return;
                }

                await CostCentresAppService.UpdateAsync(EditingCostCentreId, EditingCostCentre);
                await SetCostCentresAsync(EditingCostCentre.BranchId);
                await EditCostCentreModal.Hide();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
        
        private async Task DeleteCostCentreAsync(CostCentreDto input)
        {
            await CostCentresAppService.DeleteAsync(input.Id);
            await SetCostCentresAsync(input.BranchId);
        }
        
        private async Task OpenCreateCostCentreModalAsync(Guid branchId)
        {
            NewCostCentre = new CostCentreCreateDto
            {
                BranchId = branchId
            };

            await NewCostCentreValidations.ClearAll();
            await CreateCostCentreModal.Show();
        }
        
        private async Task CloseCreateCostCentreModalAsync()
        {
            NewCostCentre = new CostCentreCreateDto();

            await CreateCostCentreModal.Hide();
        }
        
        private async Task CreateCostCentreAsync()
        {
            try
            {
                if (await NewCostCentreValidations.ValidateAll() == false)
                {
                    return;
                }

                await CostCentresAppService.CreateAsync(NewCostCentre);
                await SetCostCentresAsync(NewCostCentre.BranchId);
                await CloseCreateCostCentreModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
        
        
        
        #endregion
    }
}
