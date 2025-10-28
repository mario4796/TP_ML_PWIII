using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_ML_PWIII.Data.Entidades;

namespace TP_ML_PWIII.Logica
{
    public interface IUsuariosLogica
    {
        Usuario? ObtenerUsuarioPorEmail(string email);
        Usuario? ObtenerUsuarioPorId(int idUsuario);
        void eliminarUsuario(Usuario usuario);
        void modificarUsuario(Usuario usuario);
        bool validarCredenciales(string email, string password);
        Usuario? Login(string email, string password);
        bool Registrar(string email, string username, string password);
        bool existeEmail(string email);
        bool passwordIguales(string password1, string password2);
    }

    public class UsuariosLogica : IUsuariosLogica
    {
        private readonly MiDbContext _context;
        private readonly IPasswordService _passwordService;

        public UsuariosLogica(MiDbContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public Usuario? ObtenerUsuarioPorEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        public Usuario? ObtenerUsuarioPorId(int idUsuario)
        {
            return _context.Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);
        }

        public void eliminarUsuario(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }

        public void modificarUsuario(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        // ahora valida comparando el hash
        public bool validarCredenciales(string email, string password)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email);
            if (usuario == null) return false;
            return _passwordService.VerifyHashedPassword(usuario.TokenAcceso ?? string.Empty, password);
        }

        // login: buscar por email y verificar hash
        public Usuario? Login(string email, string password)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email);
            if (usuario == null) return null;
            if (_passwordService.VerifyHashedPassword(usuario.TokenAcceso ?? string.Empty, password))
                return usuario;
            return null;
        }

        // registrar: hashear antes de guardar
        public bool Registrar(string email, string username, string password)
        {
            if (existeEmail(email))
            {
                return false;
            }

            var hash = _passwordService.HashPassword(password);

            var nuevoUsuario = new Usuario
            {
                Email = email,
                NombreUsuario = username,
                // assign a unique placeholder if SpotifyId is not provided
                SpotifyId = GenerateSpotifyPlaceholder(),
                TokenAcceso = hash,
                FechaRegistro = DateTime.Now
            };
            _context.Usuarios.Add(nuevoUsuario);
            _context.SaveChanges();
            return true;
        }

        public bool existeEmail(string email)
        {
            return _context.Usuarios.Any(u => u.Email == email);
        }

        public bool passwordIguales(string password1, string password2)
        {
            return password1 == password2;
        }

        private static string GenerateSpotifyPlaceholder()
        {
            // prefix for clarity, Guid ensures uniqueness; trim to 50 chars to respect model StringLength(50)
            var placeholder = $"local_{Guid.NewGuid():N}";
            return placeholder.Length <= 50 ? placeholder : placeholder.Substring(0, 50);
        }
    }
}