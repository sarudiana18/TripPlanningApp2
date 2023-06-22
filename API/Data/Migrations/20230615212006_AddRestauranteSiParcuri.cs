using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRestauranteSiParcuri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AspNetUsers_AppUserId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Hoteluri_HotelId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Reviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ParcId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Reviews",
                type: "int",
                nullable: true);

            // migrationBuilder.AddColumn<string>(
            //     name: "Titlu",
            //     table: "Reviews",
            //     type: "nvarchar(max)",
            //     nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Photos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ParcId",
                table: "Photos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Photos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Parcuri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcuri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcuri_cities_CityId",
                        column: x => x.CityId,
                        principalTable: "cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restaurante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Specific = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restaurante_cities_CityId",
                        column: x => x.CityId,
                        principalTable: "cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ParcId",
                table: "Reviews",
                column: "ParcId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RestaurantId",
                table: "Reviews",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ParcId",
                table: "Photos",
                column: "ParcId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_RestaurantId",
                table: "Photos",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcuri_CityId",
                table: "Parcuri",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurante_CityId",
                table: "Restaurante",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AspNetUsers_AppUserId",
                table: "Photos",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Parcuri_ParcId",
                table: "Photos",
                column: "ParcId",
                principalTable: "Parcuri",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Restaurante_RestaurantId",
                table: "Photos",
                column: "RestaurantId",
                principalTable: "Restaurante",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Hoteluri_HotelId",
                table: "Reviews",
                column: "HotelId",
                principalTable: "Hoteluri",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Parcuri_ParcId",
                table: "Reviews",
                column: "ParcId",
                principalTable: "Parcuri",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Restaurante_RestaurantId",
                table: "Reviews",
                column: "RestaurantId",
                principalTable: "Restaurante",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AspNetUsers_AppUserId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Parcuri_ParcId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Restaurante_RestaurantId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Hoteluri_HotelId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Parcuri_ParcId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Restaurante_RestaurantId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "Parcuri");

            migrationBuilder.DropTable(
                name: "Restaurante");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ParcId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_RestaurantId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Photos_ParcId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_RestaurantId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "ParcId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Titlu",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ParcId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Photos");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Photos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AspNetUsers_AppUserId",
                table: "Photos",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Hoteluri_HotelId",
                table: "Reviews",
                column: "HotelId",
                principalTable: "Hoteluri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
