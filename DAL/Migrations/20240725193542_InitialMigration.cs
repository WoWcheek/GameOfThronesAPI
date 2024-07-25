using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PetTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExistedFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    ExistedTo = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_LocationTypes_LocationTypeId",
                        column: x => x.LocationTypeId,
                        principalTable: "LocationTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Motto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CrestPicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Houses_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlsoKnownAs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PetsId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characters_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PetTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_Characters_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pets_PetTypes_PetTypeId",
                        column: x => x.PetTypeId,
                        principalTable: "PetTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1fc39009-6e3f-4415-89e5-1db92c9f45ee"), "Male" },
                    { new Guid("21d3630a-4d0a-4d19-b41a-d4715ef7200e"), "Other" },
                    { new Guid("b83f5072-0370-423f-90e5-c959b3f8e93c"), "Female" }
                });

            migrationBuilder.InsertData(
                table: "LocationTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0b25bad1-8ccd-4538-92ab-8223e409633b"), "Estate" },
                    { new Guid("6e32994a-b21e-43db-8070-83c7be8e29e0"), "Region" },
                    { new Guid("a48783bc-f035-4104-a104-9f3c94d6c777"), "Continent" }
                });

            migrationBuilder.InsertData(
                table: "PetTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("724ea49b-2ebf-422e-b2d0-5754b6c3bc56"), "Dragon" },
                    { new Guid("819f58c8-c2bb-457f-9df8-9594f707b722"), "Direwolf" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_GenderId",
                table: "Characters",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_HouseId",
                table: "Characters",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Houses_LocationId",
                table: "Houses",
                column: "LocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_LocationTypeId",
                table: "Locations",
                column: "LocationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_GenderId",
                table: "Pets",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_OwnerId",
                table: "Pets",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_PetTypeId",
                table: "Pets",
                column: "PetTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "PetTypes");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "Houses");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "LocationTypes");
        }
    }
}
