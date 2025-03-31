using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtVista.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class gallery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtworkGalleries_Artworks_ArtworkID",
                table: "ArtworkGalleries");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtworkGalleries_Galleries_GalleryID",
                table: "ArtworkGalleries");

            migrationBuilder.DropForeignKey(
                name: "FK_Galleries_Artists_ArtistID",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ArtworkGalleries");

            migrationBuilder.RenameColumn(
                name: "ArtistID",
                table: "Galleries",
                newName: "ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_Galleries_ArtistID",
                table: "Galleries",
                newName: "IX_Galleries_ArtistId");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Galleries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Galleries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtworkGalleries_Artworks_ArtworkID",
                table: "ArtworkGalleries",
                column: "ArtworkID",
                principalTable: "Artworks",
                principalColumn: "ArtworkID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtworkGalleries_Galleries_GalleryID",
                table: "ArtworkGalleries",
                column: "GalleryID",
                principalTable: "Galleries",
                principalColumn: "GalleryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Galleries_Artists_ArtistId",
                table: "Galleries",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "ArtistID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtworkGalleries_Artworks_ArtworkID",
                table: "ArtworkGalleries");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtworkGalleries_Galleries_GalleryID",
                table: "ArtworkGalleries");

            migrationBuilder.DropForeignKey(
                name: "FK_Galleries_Artists_ArtistId",
                table: "Galleries");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "Galleries",
                newName: "ArtistID");

            migrationBuilder.RenameIndex(
                name: "IX_Galleries_ArtistId",
                table: "Galleries",
                newName: "IX_Galleries_ArtistID");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Galleries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Galleries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ArtworkGalleries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtworkGalleries_Artworks_ArtworkID",
                table: "ArtworkGalleries",
                column: "ArtworkID",
                principalTable: "Artworks",
                principalColumn: "ArtworkID");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtworkGalleries_Galleries_GalleryID",
                table: "ArtworkGalleries",
                column: "GalleryID",
                principalTable: "Galleries",
                principalColumn: "GalleryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Galleries_Artists_ArtistID",
                table: "Galleries",
                column: "ArtistID",
                principalTable: "Artists",
                principalColumn: "ArtistID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
