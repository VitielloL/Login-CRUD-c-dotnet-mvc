using ProcessoSeletivo.Data;
using Microsoft.EntityFrameworkCore;
using ProcessoSeletivo.Models;

namespace ProcessoSeletivo.Repositories
{
    public class UsuarioRepository
    {
        private readonly ApplicationContext _context;

        public UsuarioRepository(ApplicationContext context)
        {
            _context = context;
        }
        public void AdicionarUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public List<Usuario> ObterTodosUsuarios()
        {
            return _context.Usuarios.ToList();
        }
        public Usuario ObterUsuarioPorId(int usuarioId)
        {
            var buscarCliente = _context.Usuarios.FirstOrDefault(obj => obj.UsuarioId == usuarioId);
            if (buscarCliente != null)
            {
                return buscarCliente;
            }

            return null;
            //return _context.Usuarios.Find(usuarioId);
        }
        public void EditarUsuario(Usuario usuario)
        {
            var atualizarUsuario = ObterUsuarioPorId(2);

            atualizarUsuario.Nome = usuario.Nome;
            atualizarUsuario.Email = usuario.Email;
            atualizarUsuario.FotoPerfil = usuario.FotoPerfil;
            atualizarUsuario.Senha = usuario.Senha;

            _context.Usuarios.Update(atualizarUsuario);
            //_context.Entry(usuario).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void ExcluirUsuario(int usuarioId)
        {
            var usuario = _context.Usuarios.Find(usuarioId);

            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
        }
        public Usuario AutenticarUsuario(string email, string senha)
        {
            var usuarioAutenticado = _context.Usuarios
                .FirstOrDefault(u => u.Email == email && u.Senha == senha);

            return usuarioAutenticado;
        }
    }
}
