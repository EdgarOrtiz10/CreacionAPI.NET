using ApiPeliculas.Data; // Importar el espacio de nombres (namespace) ApiPeliculas.Data
using ApiPeliculas.Modelos; // Importar el espacio de nombres ApiPeliculas.Modelos
using ApiPeliculas.Repositorio.IRepositorio; // Importar el espacio de nombres ApiPeliculas.Repositorio.IRepositorio
using Microsoft.EntityFrameworkCore;

namespace ApiPeliculas.Repositorio
{
    public class PeliculaRepositorio : IPeliculaRepositorio
    {
        private readonly ApplicationDbContext _bd; // Declarar una variable de solo lectura llamada _bd del tipo ApplicationDbContext

        public PeliculaRepositorio(ApplicationDbContext bd) // Declarar el constructor de la clase que recibe un parámetro del tipo ApplicationDbContext llamado bd
        {
            _bd = bd; // Asignar el valor del parámetro bd a la variable _bd
        }

        public ICollection<Pelicula> GetPeliculas() // Declarar el método GetPeliculas que no recibe parámetros y devuelve una colección del tipo ICollection<Pelicula>
        {
            return _bd.Pelicula.OrderBy(c => c.Nombre).ToList(); // Obtener todas las películas en el contexto de base de datos _bd, ordenadas por el nombre, y convertirlas en una lista
        }

        public Pelicula GetPelicula(int peliculaId) // Declarar el método GetPelicula que recibe un parámetro de tipo int llamado peliculaId y devuelve un objeto del tipo Pelicula
        {
            return _bd.Pelicula.FirstOrDefault(c => c.Id == peliculaId); // Obtener la primera película en el contexto de base de datos _bd cuyo Id coincide con el valor proporcionado
        }

        public bool ExistePelicula(string nombre) // Declarar el método ExistePelicula que recibe un parámetro de tipo string llamado nombre y devuelve un valor booleano
        {
            bool valor = _bd.Pelicula.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim()); // Verificar si existe una película en el contexto de base de datos _bd cuyo nombre coincida con el nombre proporcionado
            return valor; // Devolver el valor de la variable valor
        }

        public bool ExistePelicula(int id) // Declarar el método ExistePelicula que recibe un parámetro de tipo int llamado id y devuelve un valor booleano
        {
            return _bd.Pelicula.Any(c => c.Id == id); // Verificar si existe una película en el contexto de base de datos _bd cuyo Id coincide con el valor proporcionado
        }

        public ICollection<Pelicula> BuscarPelicula(string nombre)
        {
            // Se crea una consulta inicial utilizando la entidad Pelicula del contexto de base de datos
            IQueryable<Pelicula> query = _bd.Pelicula;

            // Verifica si el nombre de búsqueda no es nulo ni vacío
            if (!string.IsNullOrEmpty(nombre))
            {
                // Modifica la consulta para incluir solo las películas que contienen el nombre de búsqueda en su nombre o descripción
                query = query.Where(e => e.Nombre.Contains(nombre) || e.Descripcion.Contains(nombre));
            }

            // Ejecuta la consulta y retorna los resultados como una lista de películas
            return query.ToList();
        }

        public bool CrearPelicula(Pelicula pelicula) // Declarar el método CrearPelicula que recibe un parámetro del tipo Pelicula y devuelve un valor booleano
        {
            pelicula.FechaCreacion = DateTime.Now; // Establecer la propiedad FechaCreacion de la película como la fecha y hora actuales
            _bd.Pelicula.Add(pelicula); // Agregar la película al contexto de base de datos _bd
            return Guardar(); // Llamar al método Guardar y devolver el valor que retorna
        }

        public bool ActualizarPelicula(Pelicula pelicula) // Declarar el método ActualizarPelicula que recibe un parámetro del tipo Pelicula y devuelve un valor booleano
        {
            pelicula.FechaCreacion = DateTime.Now; // Establecer la propiedad FechaCreacion de la película como la fecha y hora actuales
            _bd.Pelicula.Update(pelicula); // Actualizar la película en el contexto de base de datos _bd
            return Guardar(); // Llamar al método Guardar y devolver el valor que retorna
        }

        public bool BorrarPelicula(Pelicula pelicula) // Declarar el método BorrarPelicula que recibe un parámetro del tipo Pelicula y devuelve un valor booleano
        {
            _bd.Pelicula.Remove(pelicula); // Eliminar la película del contexto de base de datos _bd
            return Guardar(); // Llamar al método Guardar y devolver el valor que retorna
        }

        public ICollection<Pelicula> GetPeliculasEnCategoria(int catId)
        {
            // Retorna una colección de películas que coinciden con el ID de categoría proporcionado
            // Incluye la carga de la propiedad de navegación "Categoria" para cada película
            return _bd.Pelicula.Include(ca => ca.Categoria) //permite acceder a los detalles de la categoría asociada a cada película de manera eficiente.
                               .Where(ca => ca.categoriaId == catId)
                               .ToList();
        }

        public bool Guardar() // Declarar el método Guardar que no recibe parámetros y devuelve un valor booleano
        {
            return _bd.SaveChanges() >= 0 ? true : false; // Guardar los cambios en el contexto de base de datos _bd y devolver true si se guardaron correctamente (el número de cambios guardados es mayor o igual a cero), o false en caso contrario
        }
    }
}
