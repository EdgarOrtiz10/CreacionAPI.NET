using ApiPeliculas.Data;
using ApiPeliculas.Modelos;
using ApiPeliculas.Modelos.Dtos;
using ApiPeliculas.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Identity;

namespace ApiPeliculas.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _bd; // Declara una variable privada de solo lectura llamada "_bd" de tipo ApplicationDbContext.

        public UsuarioRepositorio(ApplicationDbContext bd) // Constructor de la clase UsuarioRepositorio que recibe un objeto de tipo ApplicationDbContext.
        {
            _bd = bd; // Asigna el valor del parámetro "bd" a la variable "_bd".
        }

        public Usuario GetUsuario(int usuarioId) // Método que recibe un entero "usuarioId" y devuelve un objeto de tipo Usuario.
        {
            return _bd.Usuario.FirstOrDefault(u => u.Id == usuarioId); // Retorna el primer Usuario encontrado en la base de datos "_bd" cuyo Id coincida con "usuarioId".
        }

        public ICollection<Usuario> GetUsuarios() // Método que devuelve una colección de objetos Usuario.
        {
            return _bd.Usuario.OrderBy(u => u.NombreUsuario).ToList(); // Retorna una lista ordenada de Usuarios de la base de datos "_bd" ordenada por el campo "NombreUsuario".
        }

        public bool IsUniqueUser(string usuario) // Método que verifica si un usuario es único.
        {
            var usuariobd = _bd.Usuario.FirstOrDefault(u => u.NombreUsuario == usuario); // Busca en la base de datos "_bd" un Usuario cuyo campo "NombreUsuario" coincida con el parámetro "usuario".
            if (usuariobd == null) // Si no se encuentra ningún Usuario en la base de datos con el mismo nombre de usuario, es único.
            {
                return true; // Retorna true, indicando que el usuario es único.
            }
            return false; // Retorna false, indicando que el usuario no es único.
        }

        public async Task<Usuario> Registro(UsuarioRegistroDto usuarioRegistroDto)
        {
            var passwordEncriptado = obtenermd5(usuarioRegistroDto); // Se encripta la contraseña del usuario utilizando algún método llamado "obtenermd5".

            Usuario usuario = new Usuario() // Se crea un nuevo objeto de tipo Usuario.
            {
                NombreUsuario = usuarioRegistroDto.NombreUsuario, // Se asigna el nombre de usuario proporcionado en el objeto "usuarioRegistroDto" al campo "NombreUsuario" del objeto "usuario".
                Password = passwordEncriptado.Password, // Se asigna la contraseña encriptada al campo "Password" del objeto "usuario".
                Nombre = usuarioRegistroDto.Nombre, // Se asigna el nombre proporcionado en el objeto "usuarioRegistroDto" al campo "Nombre" del objeto "usuario".
                Role = usuarioRegistroDto.Role // Se asigna el rol proporcionado en el objeto "usuarioRegistroDto" al campo "Role" del objeto "usuario".
            };

            _bd.Usuario.Add(usuario); // Se agrega el objeto "usuario" a la base de datos "_bd".
            await _bd.SaveChangesAsync(); // Se guarda la base de datos de manera asíncrona para persistir los cambios.
            usuario.Password = passwordEncriptado; // Se asigna la contraseña encriptada al campo "Password" del objeto "usuario" antes de devolverlo.
            return usuario; // Se devuelve el objeto "usuario".
        }


        public Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
        {
            throw new NotImplementedException();
        }
    }
}
