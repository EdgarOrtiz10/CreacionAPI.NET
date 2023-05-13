using System; // Importa el espacio de nombres System
using Microsoft.EntityFrameworkCore.Migrations; // Importa el espacio de nombres Microsoft.EntityFrameworkCore.Migrations

#nullable disable

namespace ApiPeliculas.Migrations // Define el espacio de nombres ApiPeliculas.Migrations
{
    /// <inheritdoc />
    public partial class CreacionTablaCategoria : Migration // Define una clase llamada CreacionTablaCategoria que hereda de Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            // Método que aplica los cambios necesarios en la base de datos para reflejar una migración específica
            // El parámetro 'migrationBuilder' permite definir las operaciones de esquema y datos necesarias para la migración hacia adelante
            // Aquí se deben incluir las instrucciones para crear nuevas tablas, modificar tablas existentes, agregar o eliminar columnas, entre otras operaciones relacionadas con el esquema de la base de datos
            
            migrationBuilder.CreateTable( // Crea una nueva tabla en la base de datos
                name: "Categoria", // Nombre de la tabla
                columns: table => new // Define las columnas de la tabla
                {
                    Id = table.Column<int>(type: "int", nullable: false) // Columna 'Id' de tipo entero no nulo
                        .Annotation("SqlServer:Identity", "1, 1"), // Configuración adicional de la columna 'Id'con llave autoincremental
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false), // Columna 'Nombre' de tipo cadena de caracteres no nulo
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false) // Columna 'FechaCreacion' de tipo DateTime no nulo
                },
                constraints: table => // Define las restricciones de la tabla
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id); // Define la clave primaria de la tabla
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable( // Elimina la tabla de la base de datos
                name: "Categoria"); // Nombre de la tabla a eliminar
        }
    }
}
