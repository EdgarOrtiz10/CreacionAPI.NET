using ApiPeliculas.Modelos.Dtos; // Importa el namespace de los DTOs de categoría
using ApiPeliculas.Repositorio.IRepositorio; // Importa el namespace de la interfaz del repositorio de categoría
using AutoMapper; // Importa el namespace de AutoMapper
using Microsoft.AspNetCore.Mvc; // Importa el namespace de los atributos y clases relacionados con ASP.NET Core MVC

namespace ApiPeliculas.Controllers
{
    //este código implementa un controlador de API que proporciona un endpoint para obtener todas las categorías de películas.
    //Utiliza un repositorio de categorías y AutoMapper para obtener y mapear las categorías a DTOs, que luego se devuelven en
    //la respuesta HTTP.


    [ApiController] // Atributo que indica que este controlador es un controlador de API
    [Route("api/categorias")] // Ruta base para las rutas de este controlador
    public class CategoriasController : ControllerBase // Clase del controlador de categorías que hereda de ControllerBase
    {
        private readonly ICategoriaRepositorio _ctRepo; // Campo para almacenar la instancia del repositorio de categoría
        private readonly IMapper _mapper; // Campo para almacenar la instancia de AutoMapper

        public CategoriasController(ICategoriaRepositorio ctRepo, IMapper mapper) //define el constructor de CategoriasController que acepta dos dependencias (ctRepo y mapper) y permite la inyección de las implementaciones concretas de esas dependencias al crear una instancia del controlador.
        {
            _ctRepo = ctRepo; // Inicializa el campo del repositorio de categoría con la instancia proporcionada
            _mapper = mapper; // Inicializa el campo de AutoMapper con la instancia proporcionada
        }

        [HttpGet] // Atributo que indica que este método responde a las solicitudes HTTP GET
        [ProducesResponseType(StatusCodes.Status403Forbidden)] // Atributo que indica el tipo de respuesta HTTP que se produce cuando se deniega el acceso a este método
        [ProducesResponseType(StatusCodes.Status200OK)] // Atributo que indica el tipo de respuesta HTTP que se produce cuando la solicitud es exitosa
        public IActionResult GetCategorias() //declara un método público llamado GetCategorias() que devuelve un objeto de tipo IActionResult.
        {
            var listaCategorias = _ctRepo.GetCategorias(); // Obtiene la lista de categorías del repositorio

            var listaCategoriasDto = new List<CategoriaDto>(); // Crea una nueva lista para almacenar los DTOs de categoría

            foreach (var lista in listaCategorias)
            {
                listaCategoriasDto.Add(_mapper.Map<CategoriaDto>(lista)); // Mapea cada categoría del repositorio a su correspondiente DTO y la agrega a la lista de DTOs de categoría
            }
            return Ok(listaCategoriasDto); // Devuelve la lista de DTOs de categoría en la respuesta HTTP con código de estado 200 OK
        }
    }
}
