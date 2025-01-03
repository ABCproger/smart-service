using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartService.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "process_equipments",
                columns: table => new
                {
                    code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    area = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_process_equipments", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "production_facilities",
                columns: table => new
                {
                    code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    standard_area = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_production_facilities", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "equipment_placement_contracts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    production_facility_code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    process_equipment_code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    equipment_units = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_equipment_placement_contracts", x => x.id);
                    table.ForeignKey(
                        name: "fk_equipment_placement_contracts_process_equipments_process_equipment_code",
                        column: x => x.process_equipment_code,
                        principalTable: "process_equipments",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_equipment_placement_contracts_production_facilities_production_facility_code",
                        column: x => x.production_facility_code,
                        principalTable: "production_facilities",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_equipment_placement_contracts_process_equipment_code",
                table: "equipment_placement_contracts",
                column: "process_equipment_code");

            migrationBuilder.CreateIndex(
                name: "ix_equipment_placement_contracts_production_facility_code",
                table: "equipment_placement_contracts",
                column: "production_facility_code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "equipment_placement_contracts");

            migrationBuilder.DropTable(
                name: "process_equipments");

            migrationBuilder.DropTable(
                name: "production_facilities");
        }
    }
}
