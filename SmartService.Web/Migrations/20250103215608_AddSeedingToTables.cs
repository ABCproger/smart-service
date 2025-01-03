using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartService.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedingToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "process_equipments",
                columns: new[] { "code", "area", "name" },
                values: new object[,]
                {
                    { "EQP001", 200.0, "High-Speed Conveyor" },
                    { "EQP002", 150.0, "Industrial Robot Arm" }
                });

            migrationBuilder.InsertData(
                table: "production_facilities",
                columns: new[] { "code", "name", "standard_area" },
                values: new object[,]
                {
                    { "FAC001", "Main Production Facility", 5000.0 },
                    { "FAC002", "Secondary Facility", 3000.0 }
                });

            migrationBuilder.InsertData(
                table: "equipment_placement_contracts",
                columns: new[] { "id", "equipment_units", "process_equipment_code", "production_facility_code" },
                values: new object[,]
                {
                    { 1, 10, "EQP001", "FAC001" },
                    { 2, 5, "EQP002", "FAC002" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "equipment_placement_contracts",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "equipment_placement_contracts",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "process_equipments",
                keyColumn: "code",
                keyValue: "EQP001");

            migrationBuilder.DeleteData(
                table: "process_equipments",
                keyColumn: "code",
                keyValue: "EQP002");

            migrationBuilder.DeleteData(
                table: "production_facilities",
                keyColumn: "code",
                keyValue: "FAC001");

            migrationBuilder.DeleteData(
                table: "production_facilities",
                keyColumn: "code",
                keyValue: "FAC002");
        }
    }
}
