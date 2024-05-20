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

        // GET: Movimentacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movimentacao == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacao
                .Include(m => m.Projeto)
                .Include(m => m.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimentacao == null)
            {
                return NotFound();
            }

            return View(movimentacao);
        }

        // GET: Movimentacao/Create
        public IActionResult Create()
        {
            ViewData["ProjetoId"] = new SelectList(_context.Projeto, "Id", "Titulo");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "NomeCompleto");
            return View();
        }

        // POST: Movimentacao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Responsavel,TipoMovimentacao,DtMovimentacao,UsuarioId,ProjetoId")] Movimentacao movimentacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movimentacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjetoId"] = new SelectList(_context.Projeto, "Id", "Titulo", movimentacao.ProjetoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "NomeCompleto", movimentacao.UsuarioId);
            return View(movimentacao);
        }

        // GET: Movimentacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movimentacao == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacao.FindAsync(id);
            if (movimentacao == null)
            {
                return NotFound();
            }
            ViewData["ProjetoId"] = new SelectList(_context.Projeto, "Id", "Titulo", movimentacao.ProjetoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "NomeCompleto", movimentacao.UsuarioId);
            return View(movimentacao);
        }

        // POST: Movimentacao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Responsavel,TipoMovimentacao,DtMovimentacao,UsuarioId,ProjetoId")] Movimentacao movimentacao)
        {
            if (id != movimentacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimentacao);
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
            return View(movimentacao);
        }

        // GET: Movimentacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movimentacao == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacao
                .Include(m => m.Projeto)
                .Include(m => m.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimentacao == null)
            {
                return NotFound();
            }

            return View(movimentacao);
        }

        // POST: Movimentacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movimentacao == null)
            {
                return Problem("Entity set 'AppDbContext.Movimentacao'  is null.");
            }
            var movimentacao = await _context.Movimentacao.FindAsync(id);
            if (movimentacao != null)
            {
                _context.Movimentacao.Remove(movimentacao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimentacaoExists(int id)
        {
          return (_context.Movimentacao?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
