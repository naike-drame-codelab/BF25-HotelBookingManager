using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Login",
                columns: new[] { "LoginId", "Password", "Username" },
                values: new object[] { -1, new byte[] { 212, 4, 85, 159, 96, 46, 171, 111, 214, 2, 172, 118, 128, 218, 203, 250, 173, 209, 54, 48, 51, 94, 149, 31, 9, 122, 243, 144, 14, 157, 225, 118, 182, 219, 40, 81, 47, 46, 0, 11, 157, 4, 251, 165, 19, 62, 139, 28, 110, 141, 245, 157, 179, 168, 171, 157, 96, 190, 75, 151, 204, 158, 129, 219 }, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Login",
                keyColumn: "LoginId",
                keyValue: -1);
        }
    }
}
