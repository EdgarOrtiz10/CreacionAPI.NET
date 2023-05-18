using ApiPeliculas.Modelos;
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

        [HttpGet ("{categoriaId:int})", Name = "GetCategoria")] // Atributo que indica que este método responde a las solicitudes HTTP GET
        [ProducesResponseType(StatusCodes.Status403Forbidden)] // Atributo que indica el tipo de respuesta HTTP que se produce cuando se deniega el acceso a este método
        [ProducesResponseType(StatusCodes.Status200OK)] // Atributo que indica el tipo de respuesta HTTP que se produce cuando la solicitud es exitosa
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Atributo que indica el tipo de respuesta HTTP que se produce cuando la solicitud es incorrecta
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Atributo que indica el tipo de respuesta HTTP que se produce cuando no se encuentra el recurso
        public IActionResult GetCategorias(int categoriaId)
        {
            var itemCategoria = _ctRepo.GetCategoria(categoriaId); // Obtiene la categoría del repositorio según el identificador proporcionado
            if (itemCategoria == null)
            {
                return NotFound(); // Devuelve una respuesta HTTP 404 Not Found si no se encuentra la categoría
            }
            var itemCategoriaDto = _mapper.Map<CategoriaDto>(itemCategoria); // Mapea la categoría a su correspondiente DTO de categoría
            return Ok(itemCategoriaDto); // Devuelve el DTO de categoría en la respuesta HTTP con código de estado 200 OK
        }

        [HttpPost] // Atributo que indica que este método responde a una solicitud HTTP POST
        [ProducesResponseType(201, Type = typeof(CategoriaDto))] // Define el tipo de respuesta HTTP 201 y el tipo de dato devuelto es CategoriaDto
        [ProducesResponseType(StatusCodes.Status201Created)] // Define el tipo de respuesta HTTP 201 Created
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Define el tipo de respuesta HTTP 400 Bad Request
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Define el tipo de respuesta HTTP 500 Internal Server Error
        public IActionResult CrearCategoria([FromBody] CrearCategoriaDto crearCategoriaDto) // Método público que crea una categoría, recibe un objeto CrearCategoriaDto en el cuerpo de la solicitud
        {
            if (!ModelState.IsValid) // Verifica si el modelo recibido es válido según las validaciones definidas en el modelo
            {
                return BadRequest(); // Si el modelo no es válido, retorna una respuesta HTTP 400 Bad Request
            }
            if (crearCategoriaDto == null) // Verifica si el objeto CrearCategoriaDto recibido es nulo
            {
                return BadRequest(ModelState); // Si es nulo, retorna una respuesta HTTP 400 Bad Request y agrega el estado del modelo actual
            }
            if (_ctRepo.ExisteCategoria(crearCategoriaDto.Nombre)) // Verifica si la categoría ya existe en el repositorio
            {
                ModelState.AddModelError("", "La categoria ya existe"); // Agrega un error de modelo indicando que la categoría ya existe
                return StatusCode(404, ModelState); // Retorna una respuesta HTTP 404 Not Found y agrega el estado del modelo actual
            }
            var categoria = _mapper.Map<Categoria>(crearCategoriaDto); // Mapea el objeto CrearCategoriaDto a un objeto de tipo Categoria utilizando AutoMapper
            if (!_ctRepo.CrearCategoria(categoria)) // Intenta crear la categoría en el repositorio
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {categoria.Nombre}"); // Agrega un error de modelo indicando que hubo un problema al guardar la categoría
                return StatusCode(500, ModelState); // Retorna una respuesta HTTP 500 Internal Server Error y agrega el estado del modelo actual
            }
            return CreatedAtRoute("GetCategoria", new { categoriaId = categoria.Id }, categoria); // Retorna una respuesta HTTP 201 Created con la categoría creada en el cuerpo de la respuesta y la ubicación de la categoría en el encabezado de la respuesta
        }

    }
}
