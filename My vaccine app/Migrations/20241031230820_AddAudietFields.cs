using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My_vaccine_app.Migrations
{
    /// <inheritdoc />
    public partial class AddAudietFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Vaccines",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Vaccines",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "VaccineRecords",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "VaccineRecords",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "VaccineCategories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "VaccineCategories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "FamilyGroups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "FamilyGroups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Dependents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Dependents",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Allergies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Allergies",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "VaccineRecords");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "VaccineRecords");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "VaccineCategories");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "VaccineCategories");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "FamilyGroups");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "FamilyGroups");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Dependents");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Dependents");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Allergies");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Allergies");
        }
    }
}
