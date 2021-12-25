using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dignite.Abp.NotificationCenter.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotificationName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 1048576, nullable: false),
                    DataTypeName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EntityTypeName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Severity = table.Column<byte>(type: "tinyint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiNotificationSubscriptions",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotificationName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    EntityTypeName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiNotificationSubscriptions", x => new { x.UserId, x.NotificationName, x.EntityTypeName, x.EntityId });
                });

            migrationBuilder.CreateTable(
                name: "DiUserNotifications",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiUserNotifications", x => new { x.UserId, x.NotificationId });
                    table.ForeignKey(
                        name: "FK_DiUserNotifications_DiNotifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "DiNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiNotifications_CreationTime_Id",
                table: "DiNotifications",
                columns: new[] { "CreationTime", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_DiNotificationSubscriptions_CreationTime_UserId",
                table: "DiNotificationSubscriptions",
                columns: new[] { "CreationTime", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_DiUserNotifications_NotificationId",
                table: "DiUserNotifications",
                column: "NotificationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiUserNotifications_UserId",
                table: "DiUserNotifications",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiNotificationSubscriptions");

            migrationBuilder.DropTable(
                name: "DiUserNotifications");

            migrationBuilder.DropTable(
                name: "DiNotifications");
        }
    }
}
