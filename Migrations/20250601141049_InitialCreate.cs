using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmptyBlazorApp1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "current_project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_current_project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "internship_direction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_internship_direction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    gender = table.Column<int>(type: "integer", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    internship_direction_id = table.Column<Guid>(type: "uuid", nullable: false),
                    current_project_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_current_project_current_project_id",
                        column: x => x.current_project_id,
                        principalTable: "current_project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_internship_direction_internship_direction_id",
                        column: x => x.internship_direction_id,
                        principalTable: "internship_direction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_current_project_name",
                table: "current_project",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_internship_direction_name",
                table: "internship_direction",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_current_project_id",
                table: "users",
                column: "current_project_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_internship_direction_id",
                table: "users",
                column: "internship_direction_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "current_project");

            migrationBuilder.DropTable(
                name: "internship_direction");
        }
    }
}
