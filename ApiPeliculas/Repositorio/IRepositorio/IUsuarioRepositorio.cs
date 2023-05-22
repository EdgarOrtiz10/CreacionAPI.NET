using ApiPeliculas.Modelos; // Importa el espacio de nombres que contiene el modelo Usuario
using ApiPeliculas.Modelos.Dtos; // Importa el espacio de nombres que contiene los DTOs de Usuario

namespace ApiPeliculas.Repositorio.IRepositorio
{
    public interface IUsuarioRepositorio
    {
        // Método para obtener todas las categorías de usuarios
        ICollection<Usuario> GetUsuarios();

        // Método para obtener un usuario por su ID
        Usuario GetUsuario(int usuarioId);

        // Método para verificar si existe un usuario por su nombre
        bool IsUniqueUser(string usuario);

        // Método para realizar el inicio de sesión de un usuario
        Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto);

        // Método para registrar un nuevo usuario
        Task<Usuario> Registro(UsuarioRegistroDto usuarioRegistroDto);
    }
}
