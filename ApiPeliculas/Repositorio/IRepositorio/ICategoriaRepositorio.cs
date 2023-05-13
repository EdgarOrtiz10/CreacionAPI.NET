using ApiPeliculas.Modelos;

namespace ApiPeliculas.Repositorio.IRepositorio
{
    public interface ICategoriaRepositorio
    {
        // Método para obtener todas las categorías
        ICollection<Categoria> GetCategorias();

        // Método para obtener una categoría por su ID
        Categoria GetCategoria(int categoriaId);

        // Método para verificar si existe una categoría por su nombre
        bool ExisteCategoria(string nombre);

        // Método para verificar si existe una categoría por su ID
        bool ExisteCategoria(int id);

        // Método para crear una nueva categoría
        bool CrearCategoria(Categoria categoria);

        // Método para actualizar una categoría existente
        bool ActualizarCategoria(Categoria categoria);

        // Método para borrar una categoría existente
        bool BorrarCategoria(Categoria categoria);

        // Método para guardar los cambios realizados en el repositorio
        bool Guardar();
    }
}
