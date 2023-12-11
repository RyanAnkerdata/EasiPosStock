using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasiPosStock.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Product_23121112172546 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProducts_AppBranches_BranchId",
                table: "AppProducts");

            migrationBuilder.DropIndex(
                name: "IX_AppProducts_BranchId",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "AppProducts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                table: "AppProducts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppProducts_BranchId",
                table: "AppProducts",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProducts_AppBranches_BranchId",
                table: "AppProducts",
                column: "BranchId",
                principalTable: "AppBranches",
                principalColumn: "Id");
        }
    }
}
