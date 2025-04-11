using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClaudinhoEnterpriseApp.Models;
using Microsoft.AspNetCore.Identity;


namespace ClaudinhoEnterpriseApp.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ClaudinhoEnterpriseDbContext _context;

        public UsuarioController(ClaudinhoEnterpriseDbContext context)
        {

            _context = context;
        }

        [HttpGet]
        public IActionResult ListaUsuarios()
        {
            var tipoUsuario = HttpContext.Session.GetString("TipoDeUsuario");

            if (tipoUsuario != "Administrador")
            {
                return RedirectToAction("AcessoNegado", "Home");
            }

            var usuarios = _context.Usuarios.ToList();
            return View(usuarios);
        }

        public IActionResult Editar(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.id == id);
            if (usuario == null) return NotFound();

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return View(usuario);

            var usuarioBanco = _context.Usuarios.FirstOrDefault(u => u.id == usuario.id);
            if (usuarioBanco == null) return NotFound();

            usuarioBanco.nome = usuario.nome;
            usuarioBanco.email = usuario.email;
            usuarioBanco.TipoDeUsuario = usuario.TipoDeUsuario;
            
            usuarioBanco.DataDeCriacao = DateTime.SpecifyKind(usuarioBanco.DataDeCriacao, DateTimeKind.Utc);

            if (!string.IsNullOrEmpty(usuario.senha))
            {
                var hasher = new PasswordHasher<Usuario>();
                usuarioBanco.senha = hasher.HashPassword(usuarioBanco, usuario.senha);
            }

            _context.Usuarios.Update(usuarioBanco);
            _context.SaveChanges();
            return RedirectToAction("ListaUsuarios");
        }



        public IActionResult Deletar(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.id == id);
            if (usuario == null) return NotFound();

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return RedirectToAction("ListaUsuarios");
        }




    }
}
