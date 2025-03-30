using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtVista.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artworks",
                columns: table => new
                {
                    ArtworkID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArtistID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artworks", x => x.ArtworkID);
                    table.ForeignKey(
                        name: "FK_Artworks_Artists_ArtistID",
                        column: x => x.ArtistID,
                        principalTable: "Artists",
                        principalColumn: "ArtistID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Galleries",
                columns: table => new
                {
                    GalleryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtistID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.GalleryID);
                    table.ForeignKey(
                        name: "FK_Galleries_Artists_ArtistID",
                        column: x => x.ArtistID,
                        principalTable: "Artists",
                        principalColumn: "ArtistID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteArtworks",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ArtworkID = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteArtworks", x => new { x.UserId, x.ArtworkID });
                    table.ForeignKey(
                        name: "FK_FavoriteArtworks_Artworks_ArtworkID",
                        column: x => x.ArtworkID,
                        principalTable: "Artworks",
                        principalColumn: "ArtworkID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtworkGalleries",
                columns: table => new
                {
                    ArtworkID = table.Column<int>(type: "int", nullable: false),
                    GalleryID = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtworkGalleries", x => new { x.ArtworkID, x.GalleryID });
                    table.ForeignKey(
                        name: "FK_ArtworkGalleries_Artworks_ArtworkID",
                        column: x => x.ArtworkID,
                        principalTable: "Artworks",
                        principalColumn: "ArtworkID");
                    table.ForeignKey(
                        name: "FK_ArtworkGalleries_Galleries_GalleryID",
                        column: x => x.GalleryID,
                        principalTable: "Galleries",
                        principalColumn: "GalleryID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtworkGalleries_GalleryID",
                table: "ArtworkGalleries",
                column: "GalleryID");

            migrationBuilder.CreateIndex(
                name: "IX_Artworks_ArtistID",
                table: "Artworks",
                column: "ArtistID");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteArtworks_ArtworkID",
                table: "FavoriteArtworks",
                column: "ArtworkID");

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_ArtistID",
                table: "Galleries",
                column: "ArtistID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtworkGalleries");

            migrationBuilder.DropTable(
                name: "FavoriteArtworks");

            migrationBuilder.DropTable(
                name: "Galleries");

            migrationBuilder.DropTable(
                name: "Artworks");
        }
    }
}
