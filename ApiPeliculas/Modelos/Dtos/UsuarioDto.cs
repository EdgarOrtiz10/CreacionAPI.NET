using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Modelos.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; } // Define una propiedad pública llamada Id de tipo 0entero

        public string NombreUsuario { get; set; } // Define una propiedad pública llamada MyProperty de tipo cadena de caracteres
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
