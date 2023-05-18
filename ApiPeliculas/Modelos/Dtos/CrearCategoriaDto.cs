using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Modelos.Dtos
{
    public class CrearCategoriaDto
    {
        //Este código es parte de una API de películas y el objetivo de esta clase es representar un DTO
        //(Objeto de Transferencia de Datos) utilizado para crear una nueva categoría en la API.

        //Esta validación es importante sino se crea vacia el nombre de la categoria 
        [Required(ErrorMessage = "El nombre es obligatorio")] // Indica que la siguiente propiedad es obligatoria
        [MaxLength(100, ErrorMessage = "El numero maximo de caracteres es de 100!")]
        public string Nombre { get; set; } // Define una propiedad pública llamada MyProperty de tipo cadena de caracteres
    }
}
