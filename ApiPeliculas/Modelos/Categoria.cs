using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Modelos // Define el espacio de nombres ApiPeliculas.Modelos
{
    public class Categoria // Define una clase llamada Categoria
    {
        [Key] // Indica que la siguiente propiedad es clave primaria
        public int Id { get; set; } // Define una propiedad pública llamada Id de tipo entero

        [Required] // Indica que la siguiente propiedad es obligatoria
        public string MyProperty { get; set; } // Define una propiedad pública llamada MyProperty de tipo cadena de caracteres

        public DateTime FechaCreacion { get; set; } // Define una propiedad pública llamada FechaCreacion de tipo DateTime
    }
}
