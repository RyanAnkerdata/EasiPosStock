using EasiPosStock.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace EasiPosStock.Permissions;

public class EasiPosStockPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(EasiPosStockPermissions.GroupName);

        myGroup.AddPermission(EasiPosStockPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(EasiPosStockPermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(EasiPosStockPermissions.MyPermission1, L("Permission:MyPermission1"));

        var branchPermission = myGroup.AddPermission(EasiPosStockPermissions.Branches.Default, L("Permission:Branches"));
        branchPermission.AddChild(EasiPosStockPermissions.Branches.Create, L("Permission:Create"));
        branchPermission.AddChild(EasiPosStockPermissions.Branches.Edit, L("Permission:Edit"));
        branchPermission.AddChild(EasiPosStockPermissions.Branches.Delete, L("Permission:Delete"));

        var costCentrePermission = myGroup.AddPermission(EasiPosStockPermissions.CostCentres.Default, L("Permission:CostCentres"));
        costCentrePermission.AddChild(EasiPosStockPermissions.CostCentres.Create, L("Permission:Create"));
        costCentrePermission.AddChild(EasiPosStockPermissions.CostCentres.Edit, L("Permission:Edit"));
        costCentrePermission.AddChild(EasiPosStockPermissions.CostCentres.Delete, L("Permission:Delete"));

        var productPermission = myGroup.AddPermission(EasiPosStockPermissions.Products.Default, L("Permission:Products"));
        productPermission.AddChild(EasiPosStockPermissions.Products.Create, L("Permission:Create"));
        productPermission.AddChild(EasiPosStockPermissions.Products.Edit, L("Permission:Edit"));
        productPermission.AddChild(EasiPosStockPermissions.Products.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EasiPosStockResource>(name);
    }
}