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
    public class AgendamentoController : Controller
    {
        private readonly AppDbContext _context;

        public AgendamentoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Agendamento
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Agendamento.Include(a => a.Projeto).Include(a => a.Usuario);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Agendamento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Agendamento == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamento
                .Include(a => a.Projeto)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agendamento == null)
            {
                return NotFound();
            }

            return View(agendamento);
        }

        // GET: Agendamento/Create
        public IActionResult Create()
        {
            ViewData["ProjetoId"] = new SelectList(_context.Projeto, "Id", "Titulo");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "NomeCompleto");
            return View();
        }

        // POST: Agendamento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ResponsavelInterno,ResponsavelExterno,Turma,Descricao,Feedback,Observacoes,DtInicial,DtFinal,QtdPessoas,ProjetoId,UsuarioId")] Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agendamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjetoId"] = new SelectList(_context.Projeto, "Id", "Titulo", agendamento.ProjetoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "NomeCompleto", agendamento.UsuarioId);
            return View(agendamento);
        }

        // GET: Agendamento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Agendamento == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamento.FindAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }
            ViewData["ProjetoId"] = new SelectList(_context.Projeto, "Id", "Titulo", agendamento.ProjetoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "NomeCompleto", agendamento.UsuarioId);
            return View(agendamento);
        }

        // POST: Agendamento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ResponsavelInterno,ResponsavelExterno,Turma," +
            "Descricao,Feedback,Observacoes,DtInicial,DtFinal,QtdPessoas,ProjetoId,UsuarioId")] Agendamento agendamento)
        {
            if (id != agendamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var projeto = await _context.Projeto.FindAsync(agendamento.ProjetoId);
                var usuario = await _context.Usuario.FindAsync(agendamento.UsuarioId);

                if (projeto == null || usuario == null)
                {
                    ModelState.AddModelError("", "Projeto ou Usuário não encontrado");
                    ViewData["ProjetoId"] = new SelectList(_context.Projeto, "Id", "Titulo", agendamento.ProjetoId);
                    ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "NomeCompleto", agendamento.UsuarioId);
                    return View(agendamento);
                }
                try
                {
                    _context.Update(agendamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgendamentoExists(agendamento.Id))
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
            ViewData["ProjetoId"] = new SelectList(_context.Projeto, "Id", "Titulo", agendamento.ProjetoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "NomeCompleto", agendamento.UsuarioId);
            return View(agendamento);
        }

        // GET: Agendamento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Agendamento == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamento
                .Include(a => a.Projeto)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agendamento == null)
            {
                return NotFound();
            }

            return View(agendamento);
        }

        // POST: Agendamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Agendamento == null)
            {
                return Problem("Entity set 'AppDbContext.Agendamento'  is null.");
            }
            var agendamento = await _context.Agendamento.FindAsync(id);
            if (agendamento != null)
            {
                _context.Agendamento.Remove(agendamento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgendamentoExists(int id)
        {
          return (_context.Agendamento?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
