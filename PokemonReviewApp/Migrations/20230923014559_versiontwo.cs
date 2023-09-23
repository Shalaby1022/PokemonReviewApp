using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class versiontwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonCategory_Categories_CategoryId",
                table: "PokemonCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonCategory_Pokemons_PokemonId",
                table: "PokemonCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonOwner_Owners_OwnerId",
                table: "PokemonOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonOwner_Pokemons_PokemonId",
                table: "PokemonOwner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonOwner",
                table: "PokemonOwner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonCategory",
                table: "PokemonCategory");

            migrationBuilder.RenameTable(
                name: "PokemonOwner",
                newName: "PokemonOwners");

            migrationBuilder.RenameTable(
                name: "PokemonCategory",
                newName: "pokemonCategories");

            migrationBuilder.RenameIndex(
                name: "IX_PokemonOwner_OwnerId",
                table: "PokemonOwners",
                newName: "IX_PokemonOwners_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_PokemonCategory_CategoryId",
                table: "pokemonCategories",
                newName: "IX_pokemonCategories_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonOwners",
                table: "PokemonOwners",
                columns: new[] { "PokemonId", "OwnerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_pokemonCategories",
                table: "pokemonCategories",
                columns: new[] { "PokemonId", "CategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_pokemonCategories_Categories_CategoryId",
                table: "pokemonCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pokemonCategories_Pokemons_PokemonId",
                table: "pokemonCategories",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonOwners_Owners_OwnerId",
                table: "PokemonOwners",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonOwners_Pokemons_PokemonId",
                table: "PokemonOwners",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pokemonCategories_Categories_CategoryId",
                table: "pokemonCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_pokemonCategories_Pokemons_PokemonId",
                table: "pokemonCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonOwners_Owners_OwnerId",
                table: "PokemonOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonOwners_Pokemons_PokemonId",
                table: "PokemonOwners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonOwners",
                table: "PokemonOwners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pokemonCategories",
                table: "pokemonCategories");

            migrationBuilder.RenameTable(
                name: "PokemonOwners",
                newName: "PokemonOwner");

            migrationBuilder.RenameTable(
                name: "pokemonCategories",
                newName: "PokemonCategory");

            migrationBuilder.RenameIndex(
                name: "IX_PokemonOwners_OwnerId",
                table: "PokemonOwner",
                newName: "IX_PokemonOwner_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_pokemonCategories_CategoryId",
                table: "PokemonCategory",
                newName: "IX_PokemonCategory_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonOwner",
                table: "PokemonOwner",
                columns: new[] { "PokemonId", "OwnerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonCategory",
                table: "PokemonCategory",
                columns: new[] { "PokemonId", "CategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonCategory_Categories_CategoryId",
                table: "PokemonCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonCategory_Pokemons_PokemonId",
                table: "PokemonCategory",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonOwner_Owners_OwnerId",
                table: "PokemonOwner",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonOwner_Pokemons_PokemonId",
                table: "PokemonOwner",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
