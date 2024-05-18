
using Microsoft.AspNetCore.Mvc;
using tcc_core.GraphQL.Interfaces;
using tcc_core.Models;

namespace tcc_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
       // private readonly IUsuarioService _usuarioService;

       // public UserController(IUsuarioService usuarioService)
       // {
       //     _usuarioService = usuarioService;
       // }

       // // [HttpGet]
       // [HttpGet("Index")]
       // public async Task<IActionResult> Index()
       // {
       //     try
       //     {
       //         var usuarios = await _usuarioService.GetAllUsuariosAsync();
       //         return View(usuarios);
       //     }
       //     catch (Exception ex)
       //     {
       //         return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
       //     }
       // }

       // // GET: Usuario/Details/5
       // // [HttpGet("{id}")]
       // [HttpGet("Details/{id}")]
       // public async Task<IActionResult> Details(int? id)
       // {
       //     if (id == null || _usuarioService == null)
       //     {
       //         return NotFound();
       //     }

       //     try
       //     {
       //         var usuario = await _usuarioService.GetUsuarioByIdAsync(id.Value);
       //         if (usuario == null)
       //         {
       //             return NotFound();
       //         }
       //         return View(usuario);
       //     }
       //     catch (Exception ex)
       //     {
       //         return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
       //     }
       // }

       // // GET: Usuario/Create
       // //  [HttpGet("create")]

       // [HttpGet("Create")]
       // public IActionResult Create()
       // {
       //     return View();
       // }

       // // GET: Usuario/Edit/5
       // // [HttpGet("{id}")]

       // [HttpGet("Edit/{id}")]
       // public async Task<IActionResult> Edit(int? id)
       // {
       //     if (id == null || _usuarioService == null)
       //     {
       //         return NotFound();
       //     }

       //     try
       //     {
       //         var usuario = await _usuarioService.GetUsuarioByIdAsync(id.Value);
       //         if (usuario == null)
       //         {
       //             return NotFound();
       //         }
       //         return View(usuario);
       //     }
       //     catch (Exception ex)
       //     {
       //         return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
       //     }
       // }

       // // GET: Usuario/Delete/5
       // // [HttpGet("{id}")]

       // [HttpGet("Delete/{id}")]
       // public async Task<IActionResult> Delete(int? id)
       // {
       //     if (id == null || _usuarioService == null)
       //     {
       //         return NotFound();
       //     }

       //     try
       //     {
       //         var usuario = await _usuarioService.GetUsuarioByIdAsync(id.Value);
       //         if (usuario == null)
       //         {
       //             return NotFound();
       //         }
       //         return View(usuario);
       //     }
       //     catch (Exception ex)
       //     {
       //         return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
       //     }
       // }

       // // POST: Usuario/Create
       // // To protect from overposting attacks, enable the specific properties you want to bind to.
       // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       // [HttpPost("UserCreate")]
       // [ValidateAntiForgeryToken]
       // public async Task<ActionResult<UsuarioModel>> UserCreate([FromBody] UsuarioModel usuario) 
       // {
       //     if (ModelState.IsValid)
       //     {
       //         try
       //         {
       //             await _usuarioService.AddUsuarioAsync(usuario);
       //             return RedirectToAction(nameof(Index));
       //         }
       //         catch (Exception ex)
       //         {
       //             return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
       //         }
       //     }
       //     return View(usuario);
       // }

       // //Bind("NomeCompleto,Email,Senha")

       // // POST: Usuario/Edit/5
       // // To protect from overposting attacks, enable the specific properties you want to bind to.
       // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

       // [HttpPost("Edit/{id}")]
       // [ValidateAntiForgeryToken]
       // public async Task<IActionResult> Edit(int id, [Bind("Id,NomeCompleto,Email,Senha")] UsuarioModel usuario)
       // {
       //     if (id != usuario.Id)
       //     {
       //         return NotFound();
       //     }

       //     if (ModelState.IsValid)
       //     {
       //         try
       //         {
       //             await _usuarioService.UpdateUsuarioAsync(id, usuario);
       //             return RedirectToAction(nameof(Index));
       //         }
       //         catch (ArgumentException ex)
       //         {
       //             return NotFound(ex.Message);
       //         }
       //         catch (Exception ex)
       //         {
       //             return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
       //         }
       //     }
       //     return View(usuario);
       // }

       // // POST: Usuario/Delete/5

       // [HttpPost("Delete/{id}")]
       //// [HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
       // public async Task<IActionResult> DeleteConfirmed(int id)
       // {
       //     if (_usuarioService == null)
       //     {
       //         return Problem("Entity set 'AppDbContext.Usuario'  is null.");
       //     }
       //     try
       //     {
       //         await _usuarioService.DeleteUsuarioAsync(id);
       //         return RedirectToAction(nameof(Index));
       //     }
       //     catch (ArgumentException ex)
       //     {
       //         return NotFound(ex.Message);
       //     }
       //     catch (Exception ex)
       //     {
       //         return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
       //     }
       // }

       
    }
}
