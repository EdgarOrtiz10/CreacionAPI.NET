using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Modelos // Define el espacio de nombres ApiPeliculas.Modelos
{
    public class Usuario // Define una clase llamada Categoria
    {
        [Key] // Indica que la siguiente propiedad es clave primaria
        public int Id { get; set; } // Define una propiedad pública llamada Id de tipo 0entero

        [Required] // Indica que la siguiente propiedad es obligatoria
        public string NombreUsuario { get; set; } // Define una propiedad pública llamada MyProperty de tipo cadena de caracteres
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

    }
}
