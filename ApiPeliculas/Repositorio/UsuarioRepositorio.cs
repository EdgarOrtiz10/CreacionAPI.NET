using ApiPeliculas.Data;
using ApiPeliculas.Modelos;
using ApiPeliculas.Modelos.Dtos;
using ApiPeliculas.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace ApiPeliculas.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _bd; // Declara una variable privada de solo lectura llamada "_bd" de tipo ApplicationDbContext.
        private string claveSecreta;


        public UsuarioRepositorio(ApplicationDbContext bd, IConfiguration config) // Constructor de la clase UsuarioRepositorio que recibe un objeto de tipo ApplicationDbContext.
        {
            _bd = bd; // Asigna el valor del parámetro "bd" a la variable "_bd".
            claveSecreta = config.GetValue<string>("ApiSettings:Secreta");
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
            var passwordEncriptado = obtenermd5(usuarioRegistroDto.Password); // Se encripta la contraseña del usuario utilizando algún método llamado "obtenermd5".

            Usuario usuario = new Usuario() // Se crea un nuevo objeto de tipo Usuario.
            {
                NombreUsuario = usuarioRegistroDto.NombreUsuario, // Se asigna el nombre de usuario proporcionado en el objeto "usuarioRegistroDto" al campo "NombreUsuario" del objeto "usuario".
                Password = passwordEncriptado, // Se asigna la contraseña encriptada al campo "Password" del objeto "usuario".
                Nombre = usuarioRegistroDto.Nombre, // Se asigna el nombre proporcionado en el objeto "usuarioRegistroDto" al campo "Nombre" del objeto "usuario".
                Role = usuarioRegistroDto.Role // Se asigna el rol proporcionado en el objeto "usuarioRegistroDto" al campo "Role" del objeto "usuario".
            };

            _bd.Usuario.Add(usuario); // Se agrega el objeto "usuario" a la base de datos "_bd".
            await _bd.SaveChangesAsync(); // Se guarda la base de datos de manera asíncrona para persistir los cambios.
            usuario.Password = passwordEncriptado; // Se asigna la contraseña encriptada al campo "Password" del objeto "usuario" antes de devolverlo.
            return usuario; // Se devuelve el objeto "usuario".
        }


        public async Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto)
        {
            var passwordEncriptado = obtenermd5(usuarioLoginDto.Password); // Se encripta la contraseña del usuario utilizando algún método llamado "obtenermd5".

            var usuario = _bd.Usuario.FirstOrDefault(
                u => u.NombreUsuario.ToLower() == usuarioLoginDto.NombreUsuario.ToLower()
                && u.Password == passwordEncriptado
            );

            // Validamos si el usuario no existe con la combinación de usuario y contraseña correcta
            if (usuario == null)
            {
                return new UsuarioLoginRespuestaDto()
                {
                    Token = "",
                    Usuario = null
                };
            }

            // Aquí existe el usuario, entonces podemos procesar el inicio de sesión
            var manejadorToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta); // Se obtiene la clave secreta en formato de bytes

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario.ToString()), // Se crea un reclamo con el nombre de usuario
                    new Claim(ClaimTypes.Role, usuario.Role) // Se crea un reclamo con el rol del usuario
                }),
                Expires = DateTime.UtcNow.AddDays(7), // El token expirará después de 7 días desde la fecha actual en formato UTC
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) // Se configuran las credenciales de firma del token
            };

            var token = manejadorToken.CreateToken(tokenDescriptor); // Se crea el token utilizando el manejador de tokens

            UsuarioLoginRespuestaDto usuarioLoginRespuestaDto = new UsuarioLoginRespuestaDto()
            {
                Token = manejadorToken.WriteToken(token), // Se escribe el token en formato de cadena
                Usuario = usuario // Se asigna el objeto de usuario a la respuesta
            };

            return usuarioLoginRespuestaDto; // Se devuelve la respuesta del inicio de sesión
        }



        //Metodo para encriptar contraseña con MD5 se usa tanto en el acceso como el registro 
        public static string obtenermd5(string valor)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(valor);
            data = x.ComputeHash(data);
            string resp = "";
            for (int i  = 0; i < data.Length; i++)
                resp += data[i].ToString("x2").ToLower();
            return resp;

            
        }
    }
}
