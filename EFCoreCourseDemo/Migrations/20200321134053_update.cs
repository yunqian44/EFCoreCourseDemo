using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreCourseDemo.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "EntityFramework Core 3.1.1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "EntityFramework Core");
        }
    }
}
