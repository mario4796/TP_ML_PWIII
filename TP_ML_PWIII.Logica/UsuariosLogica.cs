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
        bool Registrar(string email, string password);
        bool existeEmail(string email);
        
        bool passwordIguales(string password1, string password2);

    }

    public class UsuariosLogica : IUsuariosLogica
    {

        private readonly MiDbContext _context;
       

        public UsuariosLogica(MiDbContext context)
        {
            _context = context;
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

        public bool validarCredenciales(string email, string password)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email && u.TokenAcceso == password);
            return usuario != null;
        }

        public Usuario? Login(string email, string password)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email && u.TokenAcceso == password);
        }

        public bool Registrar(string email, string password)
        {
            if (existeEmail(email))
            {
                return false; 
            }
            var nuevoUsuario = new Usuario
            {
                Email = email,
                TokenAcceso = password,
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

        bool IUsuariosLogica.passwordIguales(string password1, string password2)
        {
            return password1 == password2;
        }
    }

}
