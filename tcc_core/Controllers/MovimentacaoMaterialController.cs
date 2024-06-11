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
    public class MovimentacaoMaterialController : Controller
    {
        private readonly AppDbContext _context;

        public MovimentacaoMaterialController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MovimentacaoMaterial
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.MovimentacaoMaterial.Include(m => m.Material).Include(m => m.Movimentacao);
            return View(await appDbContext.ToListAsync());
        }

        // GET: MovimentacaoMaterial/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MovimentacaoMaterial == null)
            {
                return NotFound();
            }

            var movimentacaoMaterial = await _context.MovimentacaoMaterial
                .Include(m => m.Material)
                .Include(m => m.Movimentacao)
                .FirstOrDefaultAsync(m => m.MovimentacaoId == id);
            if (movimentacaoMaterial == null)
            {
                return NotFound();
            }

            return View(movimentacaoMaterial);
        }

        // GET: MovimentacaoMaterial/Create
        public IActionResult Create()
        {
            ViewData["MaterialId"] = new SelectList(_context.Material, "Id", "Id");
            ViewData["MovimentacaoId"] = new SelectList(_context.Movimentacao, "Id", "Id");
            ViewData["MaterialId"] = new SelectList(_context.Material, "Id", "Descricao"); // Adicionado para carregar os materiais disponíveis
            return View();
        }

        // POST: MovimentacaoMaterial/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Responsavel,TipoMovimentacao,DtMovimentacao,UsuarioId,ProjetoId")] Movimentacao movimentacao, int[] materialIds, decimal[] quantidades)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movimentacao);
                await _context.SaveChangesAsync();

                // Adiciona os materiais associados à movimentação
                for (int i = 0; i < materialIds.Length; i++)
                {
                    var movimentacaoMaterial = new MovimentacaoMaterial
                    {
                        MovimentacaoId = movimentacao.Id,
                        MaterialId = materialIds[i],
                        Quantidade = quantidades[i]
                    };
                    _context.Add(movimentacaoMaterial);
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["ProjetoId"] = new SelectList(_context.Projeto, "Id", "Titulo", movimentacao.ProjetoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "NomeCompleto", movimentacao.UsuarioId);
            ViewData["MaterialId"] = new SelectList(_context.Material, "Id", "Descricao"); // Adicionado para carregar os materiais disponíveis
            return View(movimentacao);
        }

        // GET: MovimentacaoMaterial/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MovimentacaoMaterial == null)
            {
                return NotFound();
            }

            var movimentacaoMaterial = await _context.MovimentacaoMaterial.FindAsync(id);
            if (movimentacaoMaterial == null)
            {
                return NotFound();
            }
            ViewData["MaterialId"] = new SelectList(_context.Material, "Id", "Id", movimentacaoMaterial.MaterialId);
            ViewData["MovimentacaoId"] = new SelectList(_context.Movimentacao, "Id", "Id", movimentacaoMaterial.MovimentacaoId);
            return View(movimentacaoMaterial);
        }

        // POST: MovimentacaoMaterial/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovimentacaoId,MaterialId,Quantidade")] MovimentacaoMaterial movimentacaoMaterial)
        {
            if (id != movimentacaoMaterial.MovimentacaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimentacaoMaterial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimentacaoMaterialExists(movimentacaoMaterial.MovimentacaoId))
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
            ViewData["MaterialId"] = new SelectList(_context.Material, "Id", "Id", movimentacaoMaterial.MaterialId);
            ViewData["MovimentacaoId"] = new SelectList(_context.Movimentacao, "Id", "Id", movimentacaoMaterial.MovimentacaoId);
            return View(movimentacaoMaterial);
        }

        // GET: MovimentacaoMaterial/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MovimentacaoMaterial == null)
            {
                return NotFound();
            }

            var movimentacaoMaterial = await _context.MovimentacaoMaterial
                .Include(m => m.Material)
                .Include(m => m.Movimentacao)
                .FirstOrDefaultAsync(m => m.MovimentacaoId == id);
            if (movimentacaoMaterial == null)
            {
                return NotFound();
            }

            return View(movimentacaoMaterial);
        }

        // POST: MovimentacaoMaterial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MovimentacaoMaterial == null)
            {
                return Problem("Entity set 'AppDbContext.MovimentacaoMaterial'  is null.");
            }
            var movimentacaoMaterial = await _context.MovimentacaoMaterial.FindAsync(id);
            if (movimentacaoMaterial != null)
            {
                _context.MovimentacaoMaterial.Remove(movimentacaoMaterial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimentacaoMaterialExists(int id)
        {
          return (_context.MovimentacaoMaterial?.Any(e => e.MovimentacaoId == id)).GetValueOrDefault();
        }
    }
}
