using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    BeaconID = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Kind = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.BeaconID);
                });

            migrationBuilder.CreateTable(
                name: "TrackingInfos",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BatchID = table.Column<Guid>(nullable: false),
                    BeaconID = table.Column<string>(nullable: true),
                    FrameEndTime = table.Column<DateTime>(nullable: false),
                    FrameStartTime = table.Column<DateTime>(nullable: false),
                    GeoReversedAddress = table.Column<string>(nullable: true),
                    GpsCoordinates = table.Column<string>(nullable: true),
                    MaxProximityInFrame = table.Column<int>(nullable: false),
                    MaxProximityTime = table.Column<DateTime>(nullable: false),
                    MinProximityInFrame = table.Column<int>(nullable: false),
                    MinProxmityTime = table.Column<int>(nullable: false),
                    ProximityAtFrameEnd = table.Column<int>(nullable: false),
                    ProximityAtFrameStart = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingInfos", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "TrackingInfos");
        }
    }
}
