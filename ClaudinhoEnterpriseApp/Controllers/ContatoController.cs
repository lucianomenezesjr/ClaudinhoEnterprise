using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClaudinhoEnterpriseApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace ClaudinhoEnterpriseApp.Controllers
{
    public class ContatoController : Controller
    {
        private readonly ClaudinhoEnterpriseDbContext _context;

        public ContatoController(ClaudinhoEnterpriseDbContext context)
        {

            _context = context;
        }

        [HttpGet]
        public IActionResult NovoContato()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NovoContato(string Assunto, string Mensagem)
        {
            var IdUsuario = HttpContext.Session.GetInt32("IdUsuario");

            if (IdUsuario == null)
            {
                TempData["Erro"] = "Usuário não logado.";
                return RedirectToAction("Index", "Home");
            }

            var contato = new ContatoLogado
            {
                IdUsuario = IdUsuario.Value,
                Mensagem = $"{Mensagem}",
                Assunto = $"{Assunto}",
                DataCriacao = DateTime.UtcNow  // aqui salva a data e hora sem fuso
            };

            _context.ContatosLogado.Add(contato);
            _context.SaveChanges();

            TempData["Sucesso"] = "Mensagem enviada com sucesso!";
            return RedirectToAction("ListaContatos");
        }



        public IActionResult ListaContatos()
        {
            var IdUsuario = HttpContext.Session.GetInt32("IdUsuario");

            if (IdUsuario == null)
            {
                TempData["Erro"] = "Você precisa estar logado.";
                return RedirectToAction("Index", "Home");
            }

            var usuario = _context.Usuarios.FirstOrDefault(u => u.id == IdUsuario);

            if (usuario == null || usuario.TipoDeUsuario != "Administrador")
            {
                TempData["Erro"] = "Acesso negado! Apenas administradores podem visualizar as mensagens.";
                return RedirectToAction("Index", "Home");
            }

            var contatos = _context.ContatosLogado
                           .Include(c => c.Usuario) // inclui nome e email do usuário
                           .ToList();

            return View(contatos);
        }




    }
}
