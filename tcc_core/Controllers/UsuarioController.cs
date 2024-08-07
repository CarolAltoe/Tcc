﻿
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
                .FirstOrDefaultAsync(m => m.Email == email);

            if (usuarioLogado == null)
            {
                return NotFound();
            }

            var usuarios = _context.Usuario
                .Where(u => u.Email != email);
                // select m;
            //.Where(u => u.Email != email) // Excluindo o usuário logado da lista

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

           /* if (User.Identity.Name != usuario.Email)
            {
                ViewBag.Cpf = "Acesso restrito";
            }
           */

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
                .FirstOrDefault(u => u.Email == model.Email && u.Senha == model.Senha);

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

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
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

        // POST: Usuario/Edit/5
         [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Edit(int id, 
              [Bind("Id,NomeCompleto,Email,Cpf,Senha")] Usuario usuario)
          {
              if (id != usuario.Id)
              {
                  return NotFound();
              }

              if (ModelState.IsValid)
              {
                  try
                  {
                      _context.Update(usuario);
                      await _context.SaveChangesAsync();
                  }
                  catch (DbUpdateConcurrencyException)
                  {
                      if (!usuarioExists(usuario.Id))
                      {
                          return NotFound();
                      }
                      else
                      {
                          throw;
                      }
                  }
                  return RedirectToAction(nameof(Index));
              }
             /* else
              {
                  // Log ModelState errors
                  var errors = ModelState.Values.SelectMany(v => v.Errors);
                  foreach (var error in errors)
                  {
                      Console.WriteLine(error.ErrorMessage);
                  }
              }**/
              return View(usuario);
          }

       /* [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeCompleto,Email,Cpf,DesejaAlterarSenha,NovaSenha")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userToUpdate = await _context.Usuario.FindAsync(id);
                    if (userToUpdate == null)
                    {
                        return NotFound();
                    }

                    userToUpdate.NomeCompleto = usuario.NomeCompleto;
                    userToUpdate.Email = usuario.Email;
                    userToUpdate.Cpf = usuario.Cpf;

                    // Atualize a senha apenas se o usuário deseja alterar
                    if (usuario.DesejaAlterarSenha && !string.IsNullOrEmpty(usuario.NovaSenha))
                    {
                        userToUpdate.Senha = usuario.NovaSenha;
                    }

                    _context.Update(userToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!usuarioExists(usuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        } */




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
            if (_context.Usuario == null)
            {
                return Problem("Entity set 'AppDbContext.Usuario'  is null.");
            }
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuario.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
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
