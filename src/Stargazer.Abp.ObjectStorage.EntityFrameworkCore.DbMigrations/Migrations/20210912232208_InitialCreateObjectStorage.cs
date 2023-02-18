using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stargazer.Abp.ObjectStorage.EntityFrameworkCore.DbMigrations.Migrations
{
    public partial class InitialCreateObjectStorage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ObjectData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ObjectType = table.Column<string>(type: "text", nullable: true),
                    ObjectExtension = table.Column<string>(type: "text", nullable: true),
                    ObjectHash = table.Column<string>(type: "text", nullable: true),
                    ObjectPath = table.Column<string>(type: "text", nullable: true),
                    ObjectSize = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObjectData");
        }
    }
}
