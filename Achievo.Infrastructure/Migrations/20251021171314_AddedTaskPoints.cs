using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Achievo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedTaskPoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTasks_Reference_TaskTemplateId",
                table: "UserTasks");

            migrationBuilder.DropTable(
                name: "Reference");

            migrationBuilder.DropIndex(
                name: "IX_UserTasks_TaskTemplateId",
                table: "UserTasks");

            migrationBuilder.AlterColumn<Guid>(
                name: "TaskTemplateId",
                table: "UserTasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "UserTasks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AssignedUserId",
                table: "UserTasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "AssignedUserName",
                table: "UserTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaskTemplateName",
                table: "UserTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserTask_AssignedUserId",
                table: "UserTasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserTask_AssignedUserName",
                table: "UserTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "pointsAwarded",
                table: "UserTasks",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EstimatedDuration",
                table: "TaskTemplates",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<int>(
                name: "Difficulty",
                table: "TaskTemplates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "UserPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPoints", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "Ind_AssignedUserId_Status_AssignedAt",
                table: "UserTasks",
                columns: new[] { "UserTask_AssignedUserId", "Status", "AssignedAt" });

            migrationBuilder.CreateIndex(
                name: "Ind_DueAt",
                table: "UserTasks",
                column: "DueAt");

            migrationBuilder.CreateIndex(
                name: "Ind_UserId",
                table: "Users",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Ind_UserPoint",
                table: "Users",
                column: "TotalPoints");

            migrationBuilder.CreateIndex(
                name: "Ind_UserId_CreatedAt",
                table: "UserPoints",
                columns: new[] { "UserId", "CreatedAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPoints");

            migrationBuilder.DropIndex(
                name: "Ind_AssignedUserId_Status_AssignedAt",
                table: "UserTasks");

            migrationBuilder.DropIndex(
                name: "Ind_DueAt",
                table: "UserTasks");

            migrationBuilder.DropIndex(
                name: "Ind_UserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "Ind_UserPoint",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "UserTasks");

            migrationBuilder.DropColumn(
                name: "AssignedUserName",
                table: "UserTasks");

            migrationBuilder.DropColumn(
                name: "TaskTemplateName",
                table: "UserTasks");

            migrationBuilder.DropColumn(
                name: "UserTask_AssignedUserId",
                table: "UserTasks");

            migrationBuilder.DropColumn(
                name: "UserTask_AssignedUserName",
                table: "UserTasks");

            migrationBuilder.DropColumn(
                name: "pointsAwarded",
                table: "UserTasks");

            migrationBuilder.AlterColumn<Guid>(
                name: "TaskTemplateId",
                table: "UserTasks",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "UserTasks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EstimatedDuration",
                table: "TaskTemplates",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Difficulty",
                table: "TaskTemplates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Reference",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reference_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTasks_TaskTemplateId",
                table: "UserTasks",
                column: "TaskTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Reference_UserId",
                table: "Reference",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasks_Reference_TaskTemplateId",
                table: "UserTasks",
                column: "TaskTemplateId",
                principalTable: "Reference",
                principalColumn: "Id");
        }
    }
}
