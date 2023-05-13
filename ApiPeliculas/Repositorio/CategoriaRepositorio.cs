using ApiPeliculas.Data; // Importar el espacio de nombres (namespace) ApiPeliculas.Data
using ApiPeliculas.Modelos; // Importar el espacio de nombres ApiPeliculas.Modelos
using ApiPeliculas.Repositorio.IRepositorio; // Importar el espacio de nombres ApiPeliculas.Repositorio.IRepositorio

namespace ApiPeliculas.Repositorio // Declarar el espacio de nombres ApiPeliculas.Repositorio
{
    public class CategoriaRepositorio : ICategoriaRepositorio // Declarar la clase CategoriaRepositorio que implementa la interfaz ICategoriaRepositorio
    {
        private readonly ApplicationDbContext _bd; // Declarar una variable de solo lectura llamada _bd del tipo ApplicationDbContext

        public CategoriaRepositorio(ApplicationDbContext bd) // Declarar el constructor de la clase que recibe un parámetro del tipo ApplicationDbContext llamado bd
        {
            _bd = bd; // Asignar el valor del parámetro bd a la variable _bd
        }

        public bool ActualizarCategoria(Categoria categoria) // Declarar el método ActualizarCategoria que recibe un parámetro del tipo Categoria y devuelve un valor booleano
        {
            categoria.FechaCreacion = DateTime.Now; // Establecer la propiedad FechaCreacion de la categoría como la fecha y hora actuales
            _bd.Categoria.Update(categoria); // Actualizar la categoría en el contexto de base de datos _bd
            return Guardar(); // Llamar al método Guardar y devolver el valor que retorna
        }

        public bool BorrarCategoria(Categoria categoria) // Declarar el método BorrarCategoria que recibe un parámetro del tipo Categoria y devuelve un valor booleano
        {
            _bd.Categoria.Remove(categoria); // Eliminar la categoría del contexto de base de datos _bd
            return Guardar(); // Llamar al método Guardar y devolver el valor que retorna
        }

        public bool CrearCategoria(Categoria categoria) // Declarar el método CrearCategoria que recibe un parámetro del tipo Categoria y devuelve un valor booleano
        {
            categoria.FechaCreacion = DateTime.Now; // Establecer la propiedad FechaCreacion de la categoría como la fecha y hora actuales
            _bd.Categoria.Add(categoria); // Agregar la categoría al contexto de base de datos _bd
            return Guardar(); // Llamar al método Guardar y devolver el valor que retorna
        }

        public bool ExisteCategoria(string nombre) // Declarar el método ExisteCategoria que recibe un parámetro de tipo string llamado nombre y devuelve un valor booleano
        {
            bool valor = _bd.Categoria.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim()); // Verificar si existe una categoría en el contexto de base de datos _bd cuyo nombre coincida con el nombre proporcionado
            return valor; // Devolver el valor de la variable valor
        }

        public bool ExisteCategoria(int id) // Declarar el método ExisteCategoria que recibe un parámetro de tipo int llamado id y devuelve un valor booleano
        {
            return _bd.Categoria.Any(c => c.Id == id); // Verificar si existe una categoría en el contexto de base de datos _bd cuyo Id coincida con el

        }

        public Categoria GetCategoria(int categoriaId) // Declarar el método GetCategoria que recibe un parámetro de tipo int llamado categoriaId y devuelve un objeto del tipo Categoria
        {
            return _bd.Categoria.FirstOrDefault(c => c.Id == categoriaId); // Obtener la primera categoría en el contexto de base de datos _bd cuyo Id coincide con el valor proporcionado
        }

        public ICollection<Categoria> GetCategorias() // Declarar el método GetCategorias que no recibe parámetros y devuelve una colección del tipo ICollection<Categoria>
        {
            return _bd.Categoria.OrderBy(c => c.Nombre).ToList(); // Obtener todas las categorías en el contexto de base de datos _bd, ordenadas por el nombre, y convertirlas en una lista
        }

        public bool Guardar() // Declarar el método Guardar que no recibe parámetros y devuelve un valor booleano
        {
            return _bd.SaveChanges() >= 0 ? true : false; // Guardar los cambios en el contexto de base de datos _bd y devolver true si se guardaron correctamente (el número de cambios guardados es mayor o igual a cero), o false en caso contrario
        }

    }
}
