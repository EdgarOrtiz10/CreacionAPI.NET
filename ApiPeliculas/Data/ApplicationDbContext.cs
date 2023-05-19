using ApiPeliculas.Modelos; // Importa el espacio de nombres ApiPeliculas.Modelos
using Microsoft.EntityFrameworkCore; // Importa el espacio de nombres Microsoft.EntityFrameworkCore

namespace ApiPeliculas.Data // Define el espacio de nombres ApiPeliculas.Data
{
    public class ApplicationDbContext : DbContext // Define una clase llamada ApplicationDbContext que hereda de DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) // Constructor de la clase ApplicationDbContext que recibe DbContextOptions<ApplicationDbContext> como argumento y llama al constructor base de DbContext

        {
        }

        // Agregar los modelos aquí
        public DbSet<Categoria> Categoria { get; set; } 
        public DbSet<Pelicula> Pelicula { get; set; }
    }
}
