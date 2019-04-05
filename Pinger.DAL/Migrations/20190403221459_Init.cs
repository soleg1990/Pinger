using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pinger.DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SiteOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Host = table.Column<string>(nullable: true),
                    PingFrequency = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteOptions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteOptions");
        }
    }
}
