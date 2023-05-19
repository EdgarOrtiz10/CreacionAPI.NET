using ApiPeliculas.Modelos;

namespace ApiPeliculas.Repositorio.IRepositorio
{
    public interface IPeliculaRepositorio
    {
        // Método para obtener todas las peliculas
        ICollection<Pelicula> GetPeliculas();

        // Método para obtener una pelicula por su ID
        Pelicula GetPelicula(int peliculaId);

        // Método para verificar si existe una pelicula por su nombre
        bool ExistePelicula(string nombre);

        // Método para verificar si existe una pelicula por su ID
        bool ExistePelicula(int id);

        // Método para crear una nueva pelicula
        bool CrearPelicula(Pelicula pelicula);

        // Método para actualizar una pelicula existente
        bool ActualizarPelicula(Pelicula pelicula);

        // Método para borrar una pelicula existente
        bool BorrarPelicula(Pelicula pelicula);


        //Metodos para buscar peliculas en categoria y buscar pelicula por nombre
        ICollection<Pelicula> GetPeliculasEnCategoria(int catId);
        ICollection<Pelicula> BuscarPelicula(string nombre);


        // Método para guardar los cambios realizados en el repositorio
        bool Guardar();
    }
}
