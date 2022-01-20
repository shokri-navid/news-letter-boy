using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsLetterBoy.Repository.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewsLetters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsLetters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewsLetterId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "NewsLetters",
                columns: new[] { "Id", "CreationDate", "Description", "IsDeleted", "ModifyDate", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 1, 20, 21, 49, 14, 376, DateTimeKind.Local).AddTicks(9815), "About information technologies an data science", false, null, "Tech news" },
                    { 2, new DateTime(2022, 1, 20, 21, 49, 14, 389, DateTimeKind.Local).AddTicks(2393), "About Science and academic research", false, null, "Science news" },
                    { 3, new DateTime(2022, 1, 20, 21, 49, 14, 389, DateTimeKind.Local).AddTicks(2454), "About cryptocurrencies and economy", false, null, "Financial news" },
                    { 4, new DateTime(2022, 1, 20, 21, 49, 14, 389, DateTimeKind.Local).AddTicks(2461), "About Sport and fitness around the world", false, null, "Sport news" },
                    { 5, new DateTime(2022, 1, 20, 21, 49, 14, 389, DateTimeKind.Local).AddTicks(2465), "About industrial and engineering", false, null, "Technology news" },
                    { 6, new DateTime(2022, 1, 20, 21, 49, 14, 389, DateTimeKind.Local).AddTicks(2479), "About politics ", false, null, "Political news" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsLetters");

            migrationBuilder.DropTable(
                name: "Subscriptions");
        }
    }
}
