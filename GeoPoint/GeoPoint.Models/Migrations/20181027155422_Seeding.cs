using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeoPoint.Models.Migrations
{
    public partial class Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Friends",
                columns: new[] { "Id", "FriendId", "UserId", "isPending" },
                values: new object[,]
                {
                    { "973a5570-eb9c-4506-9bd3-32373a23a6fe", "1025c6a2-5b02-425e-b17a-ad59be26ece7", "5e780f46-64a3-4b03-b3c4-2839711f9451", false },
                    { "c13cb356-8553-4fbe-8ada-52b6050ad347", "6fb0905f-0a90-4f08-85a8-b13e8d956c4b", "5e780f46-64a3-4b03-b3c4-2839711f9451", true },
                    { "c99ac9a6-1b72-47ad-b91a-d62db2a9c71e", "5e780f46-64a3-4b03-b3c4-2839711f9451", "1025c6a2-5b02-425e-b17a-ad59be26ece7", false },
                    { "9a5c6a4c-bad5-4cf6-bc74-2aa8156ed2f7", "6fb0905f-0a90-4f08-85a8-b13e8d956c4b", "1025c6a2-5b02-425e-b17a-ad59be26ece7", true }
                });

            migrationBuilder.InsertData(
                table: "Scores",
                columns: new[] { "Id", "Map", "Score", "TimeStamp", "UserId" },
                values: new object[,]
                {
                    { "755f79d9-9d53-4af3-bbd8-5658c407e4fc", "ZA", 7, new DateTime(2018, 10, 27, 17, 54, 22, 289, DateTimeKind.Local), "6fb0905f-0a90-4f08-85a8-b13e8d956c4b" },
                    { "2935d37a-6dce-4223-a954-3a2a3d5b41fc", "ZA", 5, new DateTime(2018, 10, 27, 17, 54, 22, 289, DateTimeKind.Local), "1025c6a2-5b02-425e-b17a-ad59be26ece7" },
                    { "d93b1e5a-cd05-45f6-958e-35fb2ce8394e", "ZA", 3, new DateTime(2018, 10, 27, 17, 54, 22, 289, DateTimeKind.Local), "5e780f46-64a3-4b03-b3c4-2839711f9451" },
                    { "3ec59051-bdfd-42ce-a7d5-1826da3e6519", "AF", 6, new DateTime(2018, 10, 27, 17, 54, 22, 289, DateTimeKind.Local), "6fb0905f-0a90-4f08-85a8-b13e8d956c4b" },
                    { "d1ca6ba3-899b-4c83-89fb-847ef6bd63ad", "AF", 6, new DateTime(2018, 10, 27, 17, 54, 22, 289, DateTimeKind.Local), "1025c6a2-5b02-425e-b17a-ad59be26ece7" },
                    { "258c583f-6005-49d6-8c3e-0d85bf944175", "AF", 7, new DateTime(2018, 10, 27, 17, 54, 22, 289, DateTimeKind.Local), "5e780f46-64a3-4b03-b3c4-2839711f9451" },
                    { "1f80d95c-d77d-4790-b62d-30b8204e81bc", "NA", 8, new DateTime(2018, 10, 27, 17, 54, 22, 289, DateTimeKind.Local), "6fb0905f-0a90-4f08-85a8-b13e8d956c4b" },
                    { "3323c0c9-dd3a-43fa-a1e5-b3379770de4b", "NA", 14, new DateTime(2018, 10, 27, 17, 54, 22, 289, DateTimeKind.Local), "1025c6a2-5b02-425e-b17a-ad59be26ece7" },
                    { "69f59a9f-a906-42db-9c51-9f5cda834092", "NA", 5, new DateTime(2018, 10, 27, 17, 54, 22, 289, DateTimeKind.Local), "5e780f46-64a3-4b03-b3c4-2839711f9451" },
                    { "8c9f1b31-ea99-47e0-82c1-886fbd9d915d", "EU", 10, new DateTime(2018, 10, 27, 17, 54, 22, 289, DateTimeKind.Local), "6fb0905f-0a90-4f08-85a8-b13e8d956c4b" },
                    { "bd1ce265-6703-4985-967b-1021bfb48728", "EU", 4, new DateTime(2018, 10, 27, 17, 54, 22, 289, DateTimeKind.Local), "1025c6a2-5b02-425e-b17a-ad59be26ece7" },
                    { "fba5a66c-da4a-479d-aba9-634ff1c49d5b", "EU", 8, new DateTime(2018, 10, 27, 17, 54, 22, 287, DateTimeKind.Local), "5e780f46-64a3-4b03-b3c4-2839711f9451" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsAdmin", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "5e780f46-64a3-4b03-b3c4-2839711f9451", 0, "fb0bbc71-a159-4669-9ed0-760f96520c86", null, false, true, false, null, null, null, "rune", null, false, null, false, "RuneClaeys" },
                    { "1025c6a2-5b02-425e-b17a-ad59be26ece7", 0, "9856ecda-604a-44c8-afce-0e3b203f58fa", null, false, true, false, null, null, null, "miguel", null, false, null, false, "MiguelMargodt" },
                    { "6fb0905f-0a90-4f08-85a8-b13e8d956c4b", 0, "39c4eda7-62ad-4900-a859-28bee78db2a9", null, false, false, false, null, null, null, "bram", null, false, null, false, "BramVuylseke" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Friends",
                keyColumn: "Id",
                keyValue: "973a5570-eb9c-4506-9bd3-32373a23a6fe");

            migrationBuilder.DeleteData(
                table: "Friends",
                keyColumn: "Id",
                keyValue: "9a5c6a4c-bad5-4cf6-bc74-2aa8156ed2f7");

            migrationBuilder.DeleteData(
                table: "Friends",
                keyColumn: "Id",
                keyValue: "c13cb356-8553-4fbe-8ada-52b6050ad347");

            migrationBuilder.DeleteData(
                table: "Friends",
                keyColumn: "Id",
                keyValue: "c99ac9a6-1b72-47ad-b91a-d62db2a9c71e");

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: "1f80d95c-d77d-4790-b62d-30b8204e81bc");

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: "258c583f-6005-49d6-8c3e-0d85bf944175");

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: "2935d37a-6dce-4223-a954-3a2a3d5b41fc");

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: "3323c0c9-dd3a-43fa-a1e5-b3379770de4b");

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: "3ec59051-bdfd-42ce-a7d5-1826da3e6519");

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: "69f59a9f-a906-42db-9c51-9f5cda834092");

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: "755f79d9-9d53-4af3-bbd8-5658c407e4fc");

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: "8c9f1b31-ea99-47e0-82c1-886fbd9d915d");

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: "bd1ce265-6703-4985-967b-1021bfb48728");

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: "d1ca6ba3-899b-4c83-89fb-847ef6bd63ad");

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: "d93b1e5a-cd05-45f6-958e-35fb2ce8394e");

            migrationBuilder.DeleteData(
                table: "Scores",
                keyColumn: "Id",
                keyValue: "fba5a66c-da4a-479d-aba9-634ff1c49d5b");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "1025c6a2-5b02-425e-b17a-ad59be26ece7");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "5e780f46-64a3-4b03-b3c4-2839711f9451");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "6fb0905f-0a90-4f08-85a8-b13e8d956c4b");
        }
    }
}
