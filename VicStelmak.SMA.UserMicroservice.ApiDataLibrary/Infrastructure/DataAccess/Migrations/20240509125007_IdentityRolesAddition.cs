using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VicStelmak.SMA.UserMicroservice.ApiDataLibrary.Infrastructure.DataAccess.Migrations
{
    public partial class IdentityRolesAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2a6d8851-0c10-4273-a8e3-c103b7883097", "1258d8df-d317-443e-a063-9d9bc024ae09", "Customer", "CUSTOMER" },
                    { "98a94e3a-732a-4e5a-a932-d3e5f97e6303", "c28bad6b-4d19-4e9c-a195-c80a0de231ae", "Administrator", "ADMINISTRATOR" },
                    { "a7ea9e42-84a2-42d2-b53f-1a3c34d51cb2", "6fa70c14-b591-4e90-ab28-a78700902bc9", "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7f335875-a73d-3773-a7c7-937a53fd7330",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a5002430-a6a7-4cdb-bb3c-219b45c47f31", "AQAAAAEAACcQAAAAEKCypkncB9CMcdyzAz2+gH26t+Dq6aNHYJxYcpN8kLtxrNYk8eQnIy10DczhCLL2sA==", "1143ee44-2335-432e-ba05-4805b2694386" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a6d8851-0c10-4273-a8e3-c103b7883097");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98a94e3a-732a-4e5a-a932-d3e5f97e6303");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a7ea9e42-84a2-42d2-b53f-1a3c34d51cb2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7f335875-a73d-3773-a7c7-937a53fd7330",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3eac1653-d504-40c1-bd8f-976c4a86528a", "AQAAAAEAACcQAAAAEKneEBD4lkZqgf4vBan1qhJFAE7fvea5vdbDyt0CWVUI24c3YAxL9rvIW1Xb8UIP0A==", "2a149c0d-fc5c-4993-a232-9887a1189273" });
        }
    }
}
