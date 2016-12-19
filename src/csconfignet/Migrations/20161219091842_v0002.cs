using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace csconfignet.Migrations
{
    public partial class v0002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ViewConfig",
                table: "Attributes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewConfig",
                table: "Attributes");
        }
    }
}
