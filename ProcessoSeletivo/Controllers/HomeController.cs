using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProcessoSeletivo.Models;
using ProcessoSeletivo.Repositories;
using System.Diagnostics;
using System.Security.Claims;

namespace ProcessoSeletivo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly UsuarioRepository _usuarioRepository;

        public HomeController(ILogger<HomeController> logger, UsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login(string email, string senha)
        {
            // Exemplo de lógica básica de autenticação
            var usuarioAutenticado = _usuarioRepository.AutenticarUsuario(email, senha);

            if (usuarioAutenticado != null)
            {
                // Se autenticação bem-sucedida, marque o usuário como autenticado
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuarioAutenticado.Email),
            // Adicione mais informações do usuário, se necessário
        };

                var identity = new ClaimsIdentity(claims, "PBSOA");
                var principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync("PBSOA", principal);

                // Redirecione para a lista de usuários
                //return View("../Usuario/Listar");
                return RedirectToAction("Listar", "Usuario");
            }
            else
            {
                // Senão, exiba uma mensagem de erro
                ViewBag.ErroLogin = "Login ou senha incorretos.";
                return View("../Home/Index");
            }
        }
    }
}