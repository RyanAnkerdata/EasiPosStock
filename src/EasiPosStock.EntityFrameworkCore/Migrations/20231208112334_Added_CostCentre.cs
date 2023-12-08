using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasiPosStock.Migrations
{
    /// <inheritdoc />
    public partial class Added_CostCentre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppCostCentres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CostCentreReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostCentreName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCostCentres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCostCentres_AppBranches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "AppBranches",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCostCentres_BranchId",
                table: "AppCostCentres",
                column: "BranchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCostCentres");
        }
    }
}
