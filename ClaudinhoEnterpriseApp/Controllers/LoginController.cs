using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClaudinhoEnterpriseApp.Models;
using Microsoft.AspNetCore.Identity;


namespace ClaudinhoEnterpriseApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly ClaudinhoEnterpriseDbContext _context;

        public LoginController(ClaudinhoEnterpriseDbContext context)
        {

            _context = context;
        }

        // GET 
        public IActionResult Index()
        {
            return View();
        }

        // POST: LOGIN

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.email == email);

            if (usuario != null)
            {
                var hasher = new PasswordHasher<Usuario>();
                var resultado = hasher.VerifyHashedPassword(usuario, usuario.senha, senha);

                if (resultado == PasswordVerificationResult.Success)
                {
                    HttpContext.Session.SetInt32("IdUsuario", usuario.id); 
                    HttpContext.Session.SetString("NomeUsuario", usuario.nome);
                    HttpContext.Session.SetString("TipoDeUsuario", usuario.TipoDeUsuario);
                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["Mensagem"] = "E-mail ou senha inválidos!";
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Cadastro(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var hasher = new PasswordHasher<Usuario>();
                usuario.senha = hasher.HashPassword(usuario, usuario.senha);
                usuario.DataDeCriacao = DateTime.UtcNow; // Evita problema de timezone no PostgreSQL

                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                TempData["Mensagem"] = "Cadastro realizado!";
                return RedirectToAction("Index");
            }

            return View("Index");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult EditarPerfil()
        {
            var nomeUsuario = HttpContext.Session.GetString("NomeUsuario");

            if (nomeUsuario == null)
                return RedirectToAction("Login");

            var usuario = _context.Usuarios.FirstOrDefault(u => u.nome == nomeUsuario);

            if (usuario == null)
                return RedirectToAction("Login");

            return View(usuario);
        }

        [HttpPost]
        public IActionResult EditarPerfil(Usuario model)
        {
            if (ModelState.IsValid)
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.id == model.id);

                if (usuario == null)
                    return NotFound();

                usuario.nome = model.nome;
                usuario.email = model.email;
                usuario.senha = model.senha;

                _context.SaveChanges();

                HttpContext.Session.SetString("NomeUsuario", usuario.nome); // atualiza o nome na sessão
                TempData["Mensagem"] = "Perfil atualizado com sucesso!";
                return RedirectToAction("EditarPerfil");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult ExcluirPerfil()
        {
            var nomeUsuario = HttpContext.Session.GetString("NomeUsuario");

            if (nomeUsuario == null)
                return RedirectToAction("Login");

            var usuario = _context.Usuarios.FirstOrDefault(u => u.nome == nomeUsuario);

            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
                HttpContext.Session.Clear(); // remove a sessão
            }

            return RedirectToAction("Index", "Home");
        }

    }
}