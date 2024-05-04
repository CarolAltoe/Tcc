using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tcc_core.Data;
using tcc_core.Models;

namespace tcc_core.Controllers
{
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
            var appDbContext = _context.MovimentacoesMateriais.Include(m => m.Material).Include(m => m.Movimentacao);
            return View(await appDbContext.ToListAsync());
        }

        // GET: MovimentacaoMaterial/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MovimentacoesMateriais == null)
            {
                return NotFound();
            }

            var movimentacaoMaterial = await _context.MovimentacoesMateriais
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
            ViewData["MaterialId"] = new SelectList(_context.Materiais, "Id", "Id");
            ViewData["MovimentacaoId"] = new SelectList(_context.Movimentacoes, "Id", "Id");
            return View();
        }

        // POST: MovimentacaoMaterial/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovimentacaoId,MaterialId,Quantidade")] MovimentacaoMaterial movimentacaoMaterial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movimentacaoMaterial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaterialId"] = new SelectList(_context.Materiais, "Id", "Id", movimentacaoMaterial.MaterialId);
            ViewData["MovimentacaoId"] = new SelectList(_context.Movimentacoes, "Id", "Id", movimentacaoMaterial.MovimentacaoId);
            return View(movimentacaoMaterial);
        }

        // GET: MovimentacaoMaterial/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MovimentacoesMateriais == null)
            {
                return NotFound();
            }

            var movimentacaoMaterial = await _context.MovimentacoesMateriais.FindAsync(id);
            if (movimentacaoMaterial == null)
            {
                return NotFound();
            }
            ViewData["MaterialId"] = new SelectList(_context.Materiais, "Id", "Id", movimentacaoMaterial.MaterialId);
            ViewData["MovimentacaoId"] = new SelectList(_context.Movimentacoes, "Id", "Id", movimentacaoMaterial.MovimentacaoId);
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
            ViewData["MaterialId"] = new SelectList(_context.Materiais, "Id", "Id", movimentacaoMaterial.MaterialId);
            ViewData["MovimentacaoId"] = new SelectList(_context.Movimentacoes, "Id", "Id", movimentacaoMaterial.MovimentacaoId);
            return View(movimentacaoMaterial);
        }

        // GET: MovimentacaoMaterial/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MovimentacoesMateriais == null)
            {
                return NotFound();
            }

            var movimentacaoMaterial = await _context.MovimentacoesMateriais
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
            if (_context.MovimentacoesMateriais == null)
            {
                return Problem("Entity set 'AppDbContext.MovimentacoesMateriais'  is null.");
            }
            var movimentacaoMaterial = await _context.MovimentacoesMateriais.FindAsync(id);
            if (movimentacaoMaterial != null)
            {
                _context.MovimentacoesMateriais.Remove(movimentacaoMaterial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimentacaoMaterialExists(int id)
        {
          return (_context.MovimentacoesMateriais?.Any(e => e.MovimentacaoId == id)).GetValueOrDefault();
        }
    }
}
