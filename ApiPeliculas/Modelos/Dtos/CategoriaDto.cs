using System.ComponentModel.DataAnnotations;  // Importar el espacio de nombres System.ComponentModel.DataAnnotations

namespace ApiPeliculas.Modelos.Dtos
{
    public class CategoriaDto
    {
        //Este código también es parte de una API de películas y la clase CategoriaDto se utiliza para representar una categoría en la API.
        //Contiene información sobre el ID de la categoría y el nombre de la misma, con validaciones para asegurarse de que el nombre no esté
        //vacío y no exceda los 60 caracteres.
        public int Id { get; set; }  // Define una propiedad pública llamada Id de tipo entero

        [Required(ErrorMessage = "El nombre es obligatorio")]  // Indica que la siguiente propiedad es obligatoria
        [MaxLength(60, ErrorMessage = "El número máximo de caracteres es de 60!")]  // Establece la longitud máxima permitida para la propiedad
        public string Nombre { get; set; }  // Define una propiedad pública llamada Nombre de tipo cadena de caracteres
    }
}
