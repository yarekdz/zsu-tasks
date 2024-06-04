using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasks.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MainInfo_Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MainInfo_Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    MainInfo_Category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MainInfo_Priority = table.Column<int>(type: "integer", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssigneeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Estimation_EstimatedStartDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Estimation_EstimatedEndDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Stats_CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Stats_StartedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Stats_CompletionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Stats_VerifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Stats_ApprovedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Stats_ReleasedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Person_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Person_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_Email",
                table: "Person",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssigneeId",
                table: "Tasks",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
