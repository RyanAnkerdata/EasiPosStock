using EasiPosStock.Products;
using EasiPosStock.CostCentres;
using EasiPosStock.Branches;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;
using Volo.Saas.Editions;
using Volo.Saas.Tenants;
using Volo.Abp.Gdpr;
using Volo.Abp.OpenIddict.EntityFrameworkCore;

namespace EasiPosStock.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityProDbContext))]
[ReplaceDbContext(typeof(ISaasDbContext))]
[ConnectionStringName("Default")]
public class EasiPosStockDbContext :
    AbpDbContext<EasiPosStockDbContext>,
    IIdentityProDbContext,
    ISaasDbContext
{
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<CostCentre> CostCentres { get; set; } = null!;
    public DbSet<Branch> Branches { get; set; } = null!;
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // SaaS
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Edition> Editions { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public EasiPosStockDbContext(DbContextOptions<EasiPosStockDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentityPro();
        builder.ConfigureOpenIddictPro();
        builder.ConfigureFeatureManagement();
        builder.ConfigureLanguageManagement();
        builder.ConfigureSaas();
        builder.ConfigureTextTemplateManagement();
        builder.ConfigureBlobStoring();
        builder.ConfigureGdpr();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(EasiPosStockConsts.DbTablePrefix + "YourEntities", EasiPosStockConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        builder.Entity<CostCentre>(b =>
    {
        b.ToTable(EasiPosStockConsts.DbTablePrefix + "CostCentres", EasiPosStockConsts.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(CostCentre.TenantId));
        b.Property(x => x.CostCentreReference).HasColumnName(nameof(CostCentre.CostCentreReference));
        b.Property(x => x.CostCentreName).HasColumnName(nameof(CostCentre.CostCentreName));
        b.Property(x => x.IsDisabled).HasColumnName(nameof(CostCentre.IsDisabled));
        b.HasOne<Branch>().WithMany(x => x.CostCentres).HasForeignKey(x => x.BranchId).IsRequired().OnDelete(DeleteBehavior.NoAction);
    });

        builder.Entity<Branch>(b =>
    {
        b.ToTable(EasiPosStockConsts.DbTablePrefix + "Branches", EasiPosStockConsts.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(Branch.TenantId));
        b.Property(x => x.BranchName).HasColumnName(nameof(Branch.BranchName)).IsRequired();
        b.HasMany(x => x.CostCentres).WithOne().HasForeignKey(x => x.BranchId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        b.HasMany(x => x.Products).WithOne().HasForeignKey(x => x.BranchId).IsRequired().OnDelete(DeleteBehavior.NoAction);
    });
        builder.Entity<Product>(b =>
    {
        b.ToTable(EasiPosStockConsts.DbTablePrefix + "Products", EasiPosStockConsts.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(Product.TenantId));
        b.Property(x => x.ProductName).HasColumnName(nameof(Product.ProductName));
        b.Property(x => x.ProductCode).HasColumnName(nameof(Product.ProductCode));
        b.HasOne<Branch>().WithMany(x => x.Products).HasForeignKey(x => x.BranchId).IsRequired().OnDelete(DeleteBehavior.NoAction);
    });
    }
}