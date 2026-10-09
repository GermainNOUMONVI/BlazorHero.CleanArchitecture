using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class CommandUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClientName",
                table: "Commands",
                newName: "ClientEmail");

            migrationBuilder.CreateTable(
                name: "CommandDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CommandId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommandDetails_Commands_CommandId",
                        column: x => x.CommandId,
                        principalTable: "Commands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommandDetails_CommandId",
                table: "CommandDetails",
                column: "CommandId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandDetails_ProductId",
                table: "CommandDetails",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandDetails");

            migrationBuilder.RenameColumn(
                name: "ClientEmail",
                table: "Commands",
                newName: "ClientName");
        }
    }
}
