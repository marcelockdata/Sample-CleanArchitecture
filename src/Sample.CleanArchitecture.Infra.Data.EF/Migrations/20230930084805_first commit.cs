using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sample.CleanArchitecture.Infra.Data.EF.Migrations;

/// <inheritdoc />
public partial class firstcommit : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Product",
            columns: table => new
            {
                ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                Description = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: false),
                Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                IsActive = table.Column<bool>(type: "boolean", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Product", x => x.ProductId);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Product");
    }
}
