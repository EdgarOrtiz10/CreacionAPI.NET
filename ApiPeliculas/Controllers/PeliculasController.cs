using ApiPeliculas.Modelos;
using ApiPeliculas.Modelos.Dtos;
using ApiPeliculas.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.Intrinsics.X86;

namespace ApiPeliculas.Controllers
{
    [Route("api/peliculas")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly IPeliculaRepositorio _pelRepo; // Campo para almacenar la instancia del repositorio de pelicula
        private readonly IMapper _mapper; // Campo para almacenar la instancia de AutoMapper

        public PeliculasController(IPeliculaRepositorio pelRepo, IMapper mapper) //define el constructor de PeliculasController que acepta dos dependencias (ctRepo y mapper) y permite la inyección de las implementaciones concretas de esas dependencias al crear una instancia del controlador.
        {
            _pelRepo = pelRepo; // Inicializa el campo del repositorio de pelicula con la instancia proporcionada
            _mapper = mapper; // Inicializa el campo de AutoMapper con la instancia proporcionada
        }

        [HttpGet] // Atributo que indica que este método responde a las solicitudes HTTP GET
        [ProducesResponseType(StatusCodes.Status403Forbidden)] // Atributo que indica el tipo de respuesta HTTP que se produce cuando se deniega el acceso a este método
        [ProducesResponseType(StatusCodes.Status200OK)] // Atributo que indica el tipo de respuesta HTTP que se produce cuando la solicitud es exitosa
        public IActionResult GetPeliculas() //declara un método público llamado GetPeliculas() que devuelve un objeto de tipo IActionResult.
        {
            var listaPeliculas = _pelRepo.GetPeliculas(); // Obtiene la lista de películas del repositorio
            var listaPeliculaDto = new List<PeliculaDto>(); // Crea una nueva lista para almacenar los DTOs de película

            foreach (var lista in listaPeliculas)
            {
                listaPeliculaDto.Add(_mapper.Map<PeliculaDto>(lista)); // Mapea cada película del repositorio a su correspondiente DTO y la agrega a la lista de DTOs de película
            }
            return Ok(listaPeliculaDto); // Devuelve la lista de DTOs de película en la respuesta HTTP con código de estado 200 OK
        }

        [HttpGet("{peliculaId:int})", Name = "GetPelicula")] // Atributo que indica que este método responde a las solicitudes HTTP GET
        [ProducesResponseType(StatusCodes.Status403Forbidden)] // Atributo que indica el tipo de respuesta HTTP que se produce cuando se deniega el acceso a este método
        [ProducesResponseType(StatusCodes.Status200OK)] // Atributo que indica el tipo de respuesta HTTP que se produce cuando la solicitud es exitosa
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Atributo que indica el tipo de respuesta HTTP que se produce cuando la solicitud es incorrecta
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Atributo que indica el tipo de respuesta HTTP que se produce cuando no se encuentra el recurso
        public IActionResult GetPelicula(int peliculaId)
        {
            var itemPelicula = _pelRepo.GetPelicula(peliculaId); // Obtiene la película del repositorio según el identificador proporcionado
            if (itemPelicula == null)
            {
                return NotFound(); // Devuelve una respuesta HTTP 404 Not Found si no se encuentra la película
            }
            var itemPeliculaDto = _mapper.Map<PeliculaDto>(itemPelicula); // Mapea la película a su correspondiente DTO de película
            return Ok(itemPeliculaDto); // Devuelve el DTO de película en la respuesta HTTP con código de estado 200 OK
        }

        [HttpPost] // Atributo que indica que este método responde a una solicitud HTTP POST
        [ProducesResponseType(201, Type = typeof(PeliculaDto))] // Define el tipo de respuesta HTTP 201 y el tipo de dato devuelto es PeliculaDto
        [ProducesResponseType(StatusCodes.Status201Created)] // Define el tipo de respuesta HTTP 201 Created
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Define el tipo de respuesta HTTP 400 Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Define el tipo de respuesta HTTP 500 Internal Server Error
        public IActionResult CrearPelicula([FromBody] PeliculaDto peliculaDto) // Método público que crea una película, recibe un objeto CrearPeliculaDto en el cuerpo de la solicitud
        {
            if (!ModelState.IsValid) // Verifica si el modelo recibido es válido según las validaciones definidas en el modelo
            {
                return BadRequest(); // Si el modelo no es válido, retorna una respuesta HTTP 400 Bad Request
            }
            if (peliculaDto == null) // Verifica si el objeto CrearPeliculaDto recibido es nulo
            {
                return BadRequest(ModelState); // Si es nulo, retorna una respuesta HTTP 400 Bad Request y agrega el estado del modelo actual
            }
            if (_pelRepo.ExistePelicula(peliculaDto.Nombre)) // Verifica si la película ya existe en el repositorio
            {
                ModelState.AddModelError("", "La pelicula ya existe"); // Agrega un error de modelo indicando que la película ya existe
                return StatusCode(404, ModelState); // Retorna una respuesta HTTP 404 Not Found y agrega el estado del modelo actual
            }
            var pelicula = _mapper.Map<Pelicula>(peliculaDto); // Mapea el objeto CrearPeliculaDto a un objeto de tipo Pelicula utilizando AutoMapper
            if (!_pelRepo.CrearPelicula(pelicula)) // Intenta crear la película en el repositorio
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {pelicula.Nombre}"); // Agrega un error de modelo indicando que hubo un problema al guardar la película
                return StatusCode(500, ModelState); // Retorna una respuesta HTTP 500 Internal Server Error y agrega el estado del modelo actual
            }
            return CreatedAtRoute("GetPelicula", new { peliculaId = pelicula.Id }, pelicula); // Retorna una respuesta HTTP 201 Created con la película creada en el cuerpo de la respuesta y la ubicación de la película en el encabezado de la respuesta

        }

        [HttpPatch("{peliculaId:int}", Name = "ActualizarPatchPelicula")] // Atributo que indica que este método responde a una solicitud HTTP PATCH con una variable de ruta "peliculaId" de tipo entero y establece el nombre de ruta como "ActualizarPatchPelicula"
        [ProducesResponseType(StatusCodes.Status204NoContent)] // Define el tipo de respuesta HTTP 204 No Content
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Define el tipo de respuesta HTTP 400 Bad Request
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Define el tipo de respuesta HTTP 500 Internal Server Error
        public IActionResult ActualizarPatchPelicula(int peliculaId, [FromBody] PeliculaDto peliculaDto) // Método público para actualizar una película utilizando HTTP PATCH, recibe el identificador de película en la ruta y un objeto PeliculaDto en el cuerpo de la solicitud
        {
            if (!ModelState.IsValid) // Verifica si el modelo recibido es válido según las validaciones definidas en el modelo
            {
                return BadRequest(); // Si el modelo no es válido, retorna una respuesta HTTP 400 Bad Request
            }
            if (peliculaDto == null || peliculaId != peliculaDto.Id) // Verifica si el objeto PeliculaDto recibido es nulo o si el identificador de película en el objeto no coincide con el identificador en la ruta
            {
                return BadRequest(ModelState); // Si es nulo o los identificadores no coinciden, retorna una respuesta HTTP 400 Bad Request y agrega el estado del modelo actual
            }

            var pelicula = _mapper.Map<Pelicula>(peliculaDto); // Mapea el objeto PeliculaDto a un objeto de tipo Pelicula utilizando AutoMapper

            if (!_pelRepo.ActualizarPelicula(pelicula)) // Intenta actualizar la película en el repositorio
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {pelicula.Nombre}"); // Agrega un error de modelo indicando que hubo un problema al actualizar la película
                return StatusCode(500, ModelState); // Retorna una respuesta HTTP 500 Internal Server Error y agrega el estado del modelo actual
            }
            return NoContent(); // Retorna una respuesta HTTP 204 No Content
        }


        [HttpDelete("{peliculaId:int}", Name = "BorrarPelicula")] // Atributo que indica que este método responde a una solicitud HTTP DELETE con una variable de ruta "peliculaId" de tipo entero y establece el nombre de ruta como "BorrarPelicula"
        [ProducesResponseType(StatusCodes.Status204NoContent)] // Define el tipo de respuesta HTTP 204 No Content
        [ProducesResponseType(StatusCodes.Status403Forbidden)] // Define el tipo de respuesta HTTP 403 Forbidden
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Define el tipo de respuesta HTTP 404 Not Found
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Define el tipo de respuesta HTTP 400 Bad Request
        public IActionResult BorrarPelicula(int peliculaId) // Método público para borrar una película utilizando HTTP DELETE, recibe el identificador de película en la ruta
        {
            if (!_pelRepo.ExistePelicula(peliculaId)) // Verifica si la película existe en el repositorio
            {
                return NotFound(); // Si la película no existe, retorna una respuesta HTTP 404 Not Found
            }
            var pelicula = _pelRepo.GetPelicula(peliculaId); // Obtiene la película del repositorio utilizando el identificador
            if (!_pelRepo.BorrarPelicula(pelicula)) // Intenta borrar la película del repositorio
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {pelicula.Nombre}"); // Agrega un error de modelo indicando que hubo un problema al borrar la película
                return StatusCode(500, ModelState); // Retorna una respuesta HTTP 500 Internal Server Error y agrega el estado del modelo actual
            }
            return NoContent(); // Retorna una respuesta HTTP 204 No Content
        }


        //configura la ruta relativa del endpoint para obtener películas
        //en una categoría específica utilizando el identificador de la categoría como parte de la URL.
        [HttpGet("GetPeliculasEnCategoria/{categoriaId:int}")]
        public IActionResult GetPeliculasEnCategoria(int categoriaId)
        {
            // Obtiene la lista de películas correspondientes a una categoría específica
            var listaPeliculas = _pelRepo.GetPeliculasEnCategoria(categoriaId);

            // Verifica si la lista de películas es nula
            if (listaPeliculas == null)
            {
                // Retorna una respuesta HTTP 404 Not Found si la lista de películas es nula
                return NotFound();
            }

            // Crea una lista de objetos PeliculaDto para almacenar las películas mapeadas
            var itemPelicula = new List<PeliculaDto>();

            // Itera a través de cada elemento en la lista de películas obtenida
            foreach (var item in listaPeliculas)
            {
                // Mapea cada objeto Pelicula a un objeto PeliculaDto y lo agrega a la lista itemPelicula
                itemPelicula.Add(_mapper.Map<PeliculaDto>(item));
            }

            // Retorna una respuesta HTTP 200 OK con la lista de películas mapeadas (itemPelicula)
            return Ok(itemPelicula);
        }


        [HttpGet("Buscar")]
        public IActionResult Buscar(string nombre)
        {
            //se utiliza para manejar excepciones y proporcionar una forma de controlar posibles errores durante la ejecución
            //del código dentro de ese bloque.
            try
            {
                // Llama al método BuscarPelicula en _pelRepo para buscar películas por nombre
                var resultado = _pelRepo.BuscarPelicula(nombre.Trim());

                // Verifica si se encontraron resultados en la búsqueda
                if (resultado.Any())
                {
                    // Retorna una respuesta HTTP 200 OK con el resultado de la búsqueda
                    return Ok(resultado);
                }

                // Retorna una respuesta HTTP 404 Not Found si no se encontraron resultados en la búsqueda
                return NotFound();
            }
            catch (Exception)
            {
                // Retorna una respuesta HTTP 500 Internal Server Error si ocurre una excepción durante la búsqueda
                return StatusCode(StatusCodes.Status500InternalServerError, "Error recuperando datos");
            }
        }

    }
}
