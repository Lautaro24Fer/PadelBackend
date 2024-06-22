using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PadelBackend.Migrations
{
    /// <inheritdoc />
    public partial class dbup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Racket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<float>(type: "real", maxLength: 20, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Racket",
                columns: new[] { "Id", "Category", "Description", "Image", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 2, "esta es una descripcion de prueba", "esta es una url", "Racketa 1", 12.19f },
                    { 2, 0, "esta es una descripcion de prueba 2", "esta es una url 2", "Racketa 2", 1922.19f }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Name", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, "lautaro@gmail.com", "Lautaro Fernandez", "12345678", "lauta" },
                    { 2, "lautaro1@gmail.com", "Lautaro Fernandez", "12345678", "lauta2" },
                    { 3, "lautaro2@gmail.com", "Lautaro Fernandez", "12345678", "lauta3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Racket");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
