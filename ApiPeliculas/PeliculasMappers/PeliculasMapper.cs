using ApiPeliculas.Modelos;  // Importar el espacio de nombres ApiPeliculas.Modelos
using ApiPeliculas.Modelos.Dtos;  // Importar el espacio de nombres ApiPeliculas.Modelos.Dtos
using AutoMapper;  // Importar el espacio de nombres AutoMapper

namespace ApiPeliculas.PeliculasMapper
{
    //En resumen, este código define una clase llamada PeliculasMapper que hereda de la clase Profile proporcionada por AutoMapper.
    //En el constructor de la clase, se configuran los mapeos entre las clases Categoria, CategoriaDto y CrearCategoriaDto, permitiendo
    //la conversión entre objetos de dominio y objetos utilizados para la transferencia de datos. Esto facilita el proceso de mapeo y
    //transformación de datos en la API de películas.
    public class PeliculasMapper : Profile //Se define la clase PeliculasMapper que hereda de la clase Profile proporcionada por AutoMapper.
    {
        public PeliculasMapper()
        {
            CreateMap<Categoria, CategoriaDto>().ReverseMap();  // Crear un mapeo bidireccional entre Categoria y CategoriaDto
            CreateMap<Categoria, CrearCategoriaDto>().ReverseMap();  // Crear un mapeo bidireccional entre Categoria y CrearCategoriaDto
            CreateMap<Pelicula, PeliculasDto>().ReverseMap(); // Crear un mapeo bidireccional entre Pelicula y PeliculaDto
        }
    }
}
