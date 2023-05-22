using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Modelos.Dtos
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string NombreUsuario { get; set; } // Define unad propiedad pública llamada MyProperty de tipo cadena de caracteres

        [Required(ErrorMessage = "El password es obligatorio")]
        public string Password { get; set; }
    }
}
