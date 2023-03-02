using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace McTours.DataAccess.Migrations
{
    public partial class AllConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passenger",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IdentityNumber = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Gender = table.Column<short>(type: "smallint", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passenger", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleMake",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleMake", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    VehicleMakeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleModel_VehicleMake_VehicleMakeId",
                        column: x => x.VehicleMakeId,
                        principalTable: "VehicleMake",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VehicleDefinition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleModelId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<short>(type: "smallint", nullable: false),
                    SeatingType = table.Column<int>(type: "int", nullable: false),
                    LineCount = table.Column<int>(type: "int", nullable: false),
                    FuelType = table.Column<int>(type: "int", nullable: false),
                    Utilities = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleDefinition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleDefinition_VehicleModel_VehicleModelId",
                        column: x => x.VehicleModelId,
                        principalTable: "VehicleModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleDefinitionId = table.Column<int>(type: "int", nullable: false),
                    PlateNumber = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(8)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicle_VehicleDefinition_VehicleDefinitionId",
                        column: x => x.VehicleDefinitionId,
                        principalTable: "VehicleDefinition",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BusTrip",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    DepartureCityId = table.Column<int>(type: "int", nullable: false),
                    ArrivalCityId = table.Column<int>(type: "int", nullable: false),
                    EstimatedDuration = table.Column<int>(type: "int", nullable: false),
                    TicketPrice = table.Column<decimal>(type: "money", nullable: false),
                    BreakTimeDuration = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusTrip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusTrip_City_ArrivalCityId",
                        column: x => x.ArrivalCityId,
                        principalTable: "City",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BusTrip_City_DepartureCityId",
                        column: x => x.DepartureCityId,
                        principalTable: "City",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BusTrip_Vehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicle",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusTripId = table.Column<int>(type: "int", nullable: false),
                    PassengerId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SeatNumber = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_BusTrip_BusTripId",
                        column: x => x.BusTripId,
                        principalTable: "BusTrip",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ticket_Passenger_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passenger",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Adana" },
                    { 2, "Adıyaman" },
                    { 3, "Afyonkarahisar" },
                    { 4, "Ağrı" },
                    { 5, "Amasya" },
                    { 6, "Ankara" },
                    { 7, "Antalya" },
                    { 8, "Artvin" },
                    { 9, "Aydın" },
                    { 10, "Balıkesir" },
                    { 11, "Bilecik	" },
                    { 12, "Bingöl" },
                    { 13, "Bitlis" },
                    { 14, "Bolu" },
                    { 15, "Burdur" },
                    { 16, "Bursa" },
                    { 17, "Çanakkale" },
                    { 18, "Çankırı" },
                    { 19, "Çorum" },
                    { 20, "Denizli	" },
                    { 21, "Diyarbakır	" },
                    { 22, "Edirne	" },
                    { 23, "Elazığ	" },
                    { 24, "Erzincan	" },
                    { 25, "Erzurum	" },
                    { 26, "Eskişehir	" },
                    { 27, "Gaziantep	" },
                    { 28, "Giresun	" },
                    { 29, "Gümüşhane	" },
                    { 30, "Hakkari	" },
                    { 31, "Hatay	" },
                    { 32, "Isparta	" },
                    { 33, "Mersin	" },
                    { 34, "İstanbul	" },
                    { 35, "İzmir	" },
                    { 36, "Kars	" },
                    { 37, "Kastamonu	" },
                    { 38, "Kayseri	" },
                    { 39, "Kırklareli	" },
                    { 40, "Kırşehir	" },
                    { 41, "Kocaeli	" },
                    { 42, "Konya	" }
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 43, "Kütahya	" },
                    { 44, "Malatya	" },
                    { 45, "Manisa	" },
                    { 46, "Kahramanmaraş	" },
                    { 47, "Mardin	" },
                    { 48, "Muğla	" },
                    { 49, "Muş	" },
                    { 50, "Nevşehir	" },
                    { 51, "Niğde	" },
                    { 52, "Ordu	" },
                    { 53, "Rize	" },
                    { 54, "Sakarya	" },
                    { 55, "Samsun	" },
                    { 56, "Siirt	" },
                    { 57, "Sinop	" },
                    { 58, "Sivas	" },
                    { 59, "Tekirdağ	" },
                    { 60, "Tokat	" },
                    { 61, "Trabzon	" },
                    { 62, "Tunceli	" },
                    { 63, "Şanlıurfa	" },
                    { 64, "Uşak	" },
                    { 65, "Van	" },
                    { 66, "Yozgat	" },
                    { 67, "Zonguldak	" },
                    { 68, "Aksaray	" },
                    { 69, "Bayburt	" },
                    { 70, "Karaman	" },
                    { 71, "Kırıkkale	" },
                    { 72, "Batman	" },
                    { 73, "Şırnak	" },
                    { 74, "Bartın	" },
                    { 75, "Ardahan	" },
                    { 76, "Iğdır	" },
                    { 77, "Yalova	" },
                    { 78, "Karabük	" },
                    { 79, "Kilis	" },
                    { 80, "Osmaniye	" },
                    { 81, "Düzce	" }
                });

            migrationBuilder.InsertData(
                table: "Passenger",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "Gender", "IdentityNumber", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(1994, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Matias", (short)1, "3648951235", "Delgada" },
                    { 2, new DateTime(1997, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rodrigo", (short)2, "45698535684", "Tello" },
                    { 3, new DateTime(2022, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mehmet", (short)1, "15468532651", "Kaya" }
                });

            migrationBuilder.InsertData(
                table: "VehicleMake",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Mercedes" },
                    { 2, "Man" },
                    { 3, "Neoplan" },
                    { 4, "Setra" },
                    { 5, "Isuzu" },
                    { 6, "Temsa" }
                });

            migrationBuilder.InsertData(
                table: "VehicleModel",
                columns: new[] { "Id", "Name", "VehicleMakeId" },
                values: new object[,]
                {
                    { 1, "Travego", 1 },
                    { 2, "Tourismo", 1 },
                    { 3, "Novociti", 5 },
                    { 4, "Fortuna", 2 },
                    { 5, "Lions", 2 },
                    { 6, "Cityliner", 3 },
                    { 7, "Tourliner", 3 },
                    { 8, "S Serisi", 4 },
                    { 9, "Maraton", 6 },
                    { 10, "Safir", 6 }
                });

            migrationBuilder.InsertData(
                table: "VehicleDefinition",
                columns: new[] { "Id", "FuelType", "LineCount", "SeatingType", "Utilities", "VehicleModelId", "Year" },
                values: new object[,]
                {
                    { 1, 1, 10, 2, 9, 1, (short)2023 },
                    { 2, 3, 11, 3, 6, 2, (short)2021 },
                    { 3, 1, 12, 2, 1, 3, (short)2020 },
                    { 4, 2, 10, 1, 2, 4, (short)2019 },
                    { 5, 2, 11, 2, 5, 5, (short)2022 },
                    { 6, 3, 12, 3, 10, 6, (short)2021 }
                });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "Id", "PlateNumber", "RegistrationDate", "RegistrationNumber", "VehicleDefinitionId" },
                values: new object[,]
                {
                    { 1, "34TJ0350", new DateTime(2020, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "AA658549", 1 },
                    { 2, "34CRN470", new DateTime(2021, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "AB123478", 2 },
                    { 3, "53OZD47", new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "CD854621", 3 },
                    { 4, "21ABC737", new DateTime(2022, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "DE854746", 4 },
                    { 5, "47ZHR939", new DateTime(2019, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "EE217634", 5 }
                });

            migrationBuilder.InsertData(
                table: "BusTrip",
                columns: new[] { "Id", "ArrivalCityId", "BreakTimeDuration", "Date", "DepartureCityId", "EstimatedDuration", "TicketPrice", "VehicleId" },
                values: new object[] { 1, 2, 15, new DateTime(2023, 2, 18, 10, 0, 0, 0, DateTimeKind.Unspecified), 3, 240, 250m, 2 });

            migrationBuilder.InsertData(
                table: "BusTrip",
                columns: new[] { "Id", "ArrivalCityId", "BreakTimeDuration", "Date", "DepartureCityId", "EstimatedDuration", "TicketPrice", "VehicleId" },
                values: new object[] { 2, 7, 20, new DateTime(2022, 12, 31, 12, 30, 0, 0, DateTimeKind.Unspecified), 4, 420, 350m, 4 });

            migrationBuilder.InsertData(
                table: "BusTrip",
                columns: new[] { "Id", "ArrivalCityId", "BreakTimeDuration", "Date", "DepartureCityId", "EstimatedDuration", "TicketPrice", "VehicleId" },
                values: new object[] { 3, 6, 25, new DateTime(2023, 3, 1, 17, 45, 0, 0, DateTimeKind.Unspecified), 1, 360, 550m, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_BusTrip_ArrivalCityId",
                table: "BusTrip",
                column: "ArrivalCityId");

            migrationBuilder.CreateIndex(
                name: "IX_BusTrip_DepartureCityId",
                table: "BusTrip",
                column: "DepartureCityId");

            migrationBuilder.CreateIndex(
                name: "IX_BusTrip_VehicleId",
                table: "BusTrip",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Passenger_IdentityNumber",
                table: "Passenger",
                column: "IdentityNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_BusTripId",
                table: "Ticket",
                column: "BusTripId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_PassengerId",
                table: "Ticket",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_VehicleDefinitionId",
                table: "Vehicle",
                column: "VehicleDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDefinition_VehicleModelId",
                table: "VehicleDefinition",
                column: "VehicleModelId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModel_VehicleMakeId",
                table: "VehicleModel",
                column: "VehicleMakeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "BusTrip");

            migrationBuilder.DropTable(
                name: "Passenger");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropTable(
                name: "VehicleDefinition");

            migrationBuilder.DropTable(
                name: "VehicleModel");

            migrationBuilder.DropTable(
                name: "VehicleMake");
        }
    }
}
