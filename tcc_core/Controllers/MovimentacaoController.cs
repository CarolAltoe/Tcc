using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tcc_core.Data;
using tcc_core.Models;

namespace tcc_core.Controllers
{
    [Authorize]
    public class MovimentacaoController : Controller
    {
        private readonly AppDbContext _context;

        public MovimentacaoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Movimentacao
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Movimentacao.Include(m => m.Projeto).Include(m => m.Usuario);
            return View(await appDbContext.ToListAsync());
        }

        // Método para GET de Detalhes da Movimentação
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacao
                .Include(m => m.Usuario)
                .Include(m => m.Projeto)
                .Include(m => m.MovimentacaoMaterial)
                    .ThenInclude(mm => mm.Material)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movimentacao == null)
            {
                return NotFound();
            }

            return View(movimentacao);
        }

        public IActionResult Create()
        {
            ViewData["ProjetoId"] = new SelectList(_context.Projeto, "Id", "Titulo");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "NomeCompleto");
            ViewBag.Materiais = new SelectList(_context.Material, "Id", "Descricao");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Responsavel,TipoMovimentacao,DtMovimentacao,UsuarioId,ProjetoId")] Movimentacao movimentacao, int[] MovimentacaoMaterial_MaterialId, decimal[] MovimentacaoMaterial_Quantidade)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < MovimentacaoMaterial_MaterialId.Length; i++)
                {
                    movimentacao.MovimentacaoMaterial.Add(new MovimentacaoMaterial
                    {
                        MaterialId = MovimentacaoMaterial_MaterialId[i],
                        Quantidade = MovimentacaoMaterial_Quantidade[i]
                    });
                }

                _context.Add(movimentacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProjetoId"] = new SelectList(_context.Projeto, "Id", "Titulo", movimentacao.ProjetoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "NomeCompleto", movimentacao.UsuarioId);
            ViewBag.Materiais = new SelectList(_context.Material, "Id", "Descricao");
            return View(movimentacao);
        }

        // Método para GET de Editar Movimentação
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacao
                .Include(m => m.Usuario)
                .Include(m => m.Projeto)
                .Include(m => m.MovimentacaoMaterial)
                    .ThenInclude(mm => mm.Material)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movimentacao == null)
            {
                return NotFound();
            }

            ViewData["ProjetoId"] = new SelectList(_context.Projeto, "Id", "Titulo");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "NomeCompleto");
            ViewData["Materiais"] = new SelectList(_context.Material, "Id", "Descricao");

            return View(movimentacao);
        }

        // Método para POST de Editar Movimentação
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Responsavel,TipoMovimentacao,DtMovimentacao,UsuarioId,ProjetoId,MovimentacaoMaterial")] Movimentacao movimentacao, [FromForm] List<int> MovimentacaoMaterial_MaterialId, [FromForm] List<int> MovimentacaoMaterial_Quantidade)
        {
            if (id != movimentacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Atualiza os campos principais da movimentação
                    _context.Update(movimentacao);

                    // Atualiza os materiais associados
                    var existingMaterials = _context.MovimentacaoMaterial.Where(mm => mm.MovimentacaoId == movimentacao.Id).ToList();
                    _context.MovimentacaoMaterial.RemoveRange(existingMaterials);

                    for (int i = 0; i < MovimentacaoMaterial_MaterialId.Count; i++)
                    {
                        var newMaterial = new MovimentacaoMaterial
                        {
                            MovimentacaoId = movimentacao.Id,
                            MaterialId = MovimentacaoMaterial_MaterialId[i],
                            Quantidade = MovimentacaoMaterial_Quantidade[i]
                        };
                        _context.MovimentacaoMaterial.Add(newMaterial);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimentacaoExists(movimentacao.Id))
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

            ViewData["ProjetoId"] = new SelectList(_context.Projeto, "Id", "Titulo", movimentacao.ProjetoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "NomeCompleto", movimentacao.UsuarioId);
            ViewData["Materiais"] = new SelectList(_context.Material, "Id", "Descricao");

            return View(movimentacao);
        }

      

        // Método para GET de Excluir Movimentação
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacao
                .Include(m => m.Usuario)
                .Include(m => m.Projeto)
                .Include(m => m.MovimentacaoMaterial)
                    .ThenInclude(mm => mm.Material)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movimentacao == null)
            {
                return NotFound();
            }

            return View(movimentacao);
        }

        // Método para POST de Excluir Movimentação
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movimentacao = await _context.Movimentacao.FindAsync(id);
            var movimentacaoMateriais = _context.MovimentacaoMaterial.Where(mm => mm.MovimentacaoId == id);

            _context.MovimentacaoMaterial.RemoveRange(movimentacaoMateriais);
            _context.Movimentacao.Remove(movimentacao);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool MovimentacaoExists(int id)
        {
            return _context.Movimentacao.Any(e => e.Id == id);
        }

    }

}