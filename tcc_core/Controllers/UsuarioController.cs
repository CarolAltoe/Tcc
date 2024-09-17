
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using tcc_core.Data;
using tcc_core.Models;
using tcc_core.Models.ViewModels;

namespace tcc_core.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchTerm)
        {
            ViewData["CurrentFilter"] = searchTerm;

            var email = User.Identity.Name;
            if (email == null)
            {
                return NotFound();
            }

            var usuarioLogado = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Email == email && !m.IsActive);

            if (usuarioLogado == null)
            {
                return NotFound();
            }

            var usuarios = _context.Usuario
                .Where(u => u.Email != email && !u.IsActive);

            if (!String.IsNullOrEmpty(searchTerm))
            {
                usuarios = usuarios.Where(s =>
                    s.NomeCompleto.Contains(searchTerm) ||
                    s.Email.Contains(searchTerm));
            }

            var usuarioList = await usuarios.ToListAsync();
            if (!usuarioList.Any())
            {
                ViewData["Message"] = !String.IsNullOrEmpty(searchTerm) ?
                                      "Nenhum usuário encontrado para o termo pesquisado." :
                                      "Não há outro usuário cadastrado no sistema.";
            }
            ViewBag.UsuarioLogado = usuarioLogado;
            return View(await usuarios.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pesquisar(string itenSearch)
        {
            var query = _context.Usuario.AsQueryable();

            if (!string.IsNullOrEmpty(itenSearch))
            {
                query = query.Where(u => u.NomeCompleto.Contains(itenSearch) || u.Email.Contains(itenSearch));
            }

            var usuarios = await query.ToListAsync();
            return View("Index", usuarios);
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            if (User.Identity.Name != usuario.Email)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            return View(usuario);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = _context.Usuario
                .FirstOrDefault(u => u.Email == model.Email 
                && u.Senha == model.Senha && !u.IsActive);

            if (usuario == null)
            {
                ModelState.AddModelError("", "Usuário ou senha inválidos");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Email)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                // Configurações adicionais de autenticação, se necessário
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomeCompleto," +
            "Email,Cpf,Senha")] Usuario usuario)//Bind("NomeCompleto,Email,Senha,Cpf")
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }

            return View(usuario);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var usuario = _context.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }
            var viewModel = new UsuarioEditViewModel
            {
                Id = usuario.Id,
                NomeCompleto = usuario.NomeCompleto,
                Email = usuario.Email,
                Cpf = usuario.Cpf,
                // Outros campos necessários
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UsuarioEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var usuario = _context.Usuario.Find(viewModel.Id);
                if (usuario == null)
                {
                    return NotFound();
                }

                usuario.NomeCompleto = viewModel.NomeCompleto;
                usuario.Email = viewModel.Email;
                usuario.Cpf = viewModel.Cpf;

                if (!string.IsNullOrEmpty(viewModel.NovaSenha))
                {
                    usuario.Senha = BCrypt.Net.BCrypt.HashPassword(viewModel.NovaSenha); // Supondo que você está usando bcrypt para hashear as senhas
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }




        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            if (User.Identity.Name != usuario.Email)
            {
                return RedirectToAction("AccessDenied", "Home");
            }

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            var movimentacaoAssociada = _context.Movimentacao.Any(m => m.UsuarioId == id);
            var agendamentoAssociado = _context.Agendamento.Any(m => m.UsuarioId == id);

            if (movimentacaoAssociada)
            {
                ModelState.AddModelError("", "Não é possível excluir" +
                    " este usuário, pois está associado a uma movimentação.");
            }
            if (agendamentoAssociado)
            {
                ModelState.AddModelError("", "Não é possível excluir" +
                   " este usuário, pois está associado a um agendamento.");
            }

            if (movimentacaoAssociada || agendamentoAssociado)
            {
                return View(usuario);
            }

            var emailLogado = User.Identity.Name;

            if (usuario.Email == emailLogado)
            {
                // Marcar o usuário como excluído
                usuario.IsActive = true;
                _context.Update(usuario);
                await _context.SaveChangesAsync();

                // Deslogar o usuário
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                // Redirecionar para a página de login com uma mensagem de confirmação
                TempData["SuccessMessage"] = "Seu perfil foi desativado com sucesso. " +
                    "Para ativar novamente contate o administrador do sistema.";
                return RedirectToAction(nameof(Login), "Usuario");
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool usuarioExists(int id)
        {
          return (_context.Usuario?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var email = User.Identity.Name;
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null || usuario.Senha != model.SenhaAtual)
            {
                ModelState.AddModelError("", "A senha atual está incorreta.");
                return View(model);
            }

            usuario.Senha = model.NovaSenha;
            _context.Update(usuario);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Senha alterada com sucesso!";
            return RedirectToAction(nameof(Index)); ;
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _context.Usuario.FirstOrDefaultAsync(u =>
                u.Email == model.Email && u.Cpf == model.Cpf);
            if (user != null)
            {
                if (model.NovaSenha != model.ConfirmacaoSenha)
                {
                    return View();
                }
                user.Senha = model.NovaSenha;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }
            ModelState.AddModelError("", "Email ou CPF incorretos.");
            return View();
        }
    }
}
