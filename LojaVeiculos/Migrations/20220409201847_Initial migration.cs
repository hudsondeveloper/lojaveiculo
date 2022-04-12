using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaVeiculos.Migrations
{
    public partial class Initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Marca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marca", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proprietario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proprietario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Renavam = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Anofabricacao = table.Column<int>(type: "int", nullable: false),
                    AnoModelo = table.Column<int>(type: "int", nullable: false),
                    Quilometragem = table.Column<double>(type: "float", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false),
                    StatusVeiculo = table.Column<int>(type: "int", nullable: false),
                    ProprietarioID = table.Column<int>(type: "int", nullable: false),
                    MarcaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veiculo_Marca_MarcaID",
                        column: x => x.MarcaID,
                        principalTable: "Marca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Veiculo_Proprietario_ProprietarioID",
                        column: x => x.ProprietarioID,
                        principalTable: "Proprietario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Marca_Nome",
                table: "Marca",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proprietario_Documento",
                table: "Proprietario",
                column: "Documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_MarcaID",
                table: "Veiculo",
                column: "MarcaID");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_ProprietarioID",
                table: "Veiculo",
                column: "ProprietarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_Renavam",
                table: "Veiculo",
                column: "Renavam",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Veiculo");

            migrationBuilder.DropTable(
                name: "Marca");

            migrationBuilder.DropTable(
                name: "Proprietario");
        }
    }
}
