using Microsoft.AspNetCore.Mvc;
using ProcessoSeletivo.Models;
using ProcessoSeletivo.Repositories;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace ProcessoSeletivo.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var usuarios = _usuarioRepository.ObterTodosUsuarios();
            return View(usuarios);
        }

        [HttpGet]
        public IActionResult Detalhes(int id)
        {
            var usuario = _usuarioRepository.ObterUsuarioPorId(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cadastrar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioRepository.AdicionarUsuario(usuario);
                return RedirectToAction("Listar", "Usuario");
            }

            return View(usuario);
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var usuario = _usuarioRepository.ObterUsuarioPorId(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioRepository.EditarUsuario(usuario);
                return RedirectToAction("Listar", "Usuario");
            }

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Excluir")]
        public IActionResult Excluir(int id)
        {
            try
            {
                _usuarioRepository.ExcluirUsuario(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Log de erro ou manipulação adequada
                return Json(new { success = false });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Excluir")]
        public IActionResult ExcluirConfirmed(int id)
        {
            _usuarioRepository.ExcluirUsuario(id);
            return RedirectToAction("Listar", "Usuario");
        }
    }
}