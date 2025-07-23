using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL_Core.Migrations
{
    /// <inheritdoc />
    public partial class DelTriggerEntityChangeCommandAndThresholdEnities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Triggers");

            migrationBuilder.DropColumn(
                name: "Action",
                table: "Thresholds");

            migrationBuilder.RenameColumn(
                name: "ThresholdName",
                table: "Thresholds",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "Condition",
                table: "Thresholds",
                newName: "ThresholdCondition");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "CommandValue",
                table: "Commands",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommandValue",
                table: "Commands");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Thresholds",
                newName: "ThresholdName");

            migrationBuilder.RenameColumn(
                name: "ThresholdCondition",
                table: "Thresholds",
                newName: "Condition");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "Thresholds",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Triggers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TriggerCondition = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TriggerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triggers", x => x.Id);
                });
        }
    }
}
