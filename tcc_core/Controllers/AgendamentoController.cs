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
        public async Task<IActionResult> Index(string searchTerm)
        {
            ViewData["CurrentFilter"] = searchTerm;

            var agendamentos = from m in _context.Agendamento
                 .Include(a => a.Projeto).Include(a => a.Usuario)
                 select m;

            if (!String.IsNullOrEmpty(searchTerm))
            {
                agendamentos = agendamentos.Where(s =>
                    s.ResponsavelExterno.Contains(searchTerm) ||
                    s.ResponsavelInterno.Contains(searchTerm));
            }

            var agendamentoList = await agendamentos.ToListAsync();
            if (!agendamentoList.Any())
            {
                ViewData["Message"] = !String.IsNullOrEmpty(searchTerm) ?
                                      "Nenhum agendamento encontrado para o termo pesquisado." :
                                      "Nenhum agendamento cadastrado.";
            }
            return View(await agendamentos.ToListAsync());
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

        [Authorize]
        public IActionResult Create()
        {
            var email = User.Identity.Name;
            var usuario = _context.Usuario.FirstOrDefault(u => u.Email == email);

            if (usuario == null)
            {
                // Tratar o caso de usuário não encontrado, se necessário
            }

            ViewData["ProjetoId"] = new SelectList(_context.Projeto, "Id", "Titulo");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "NomeCompleto", usuario?.Id);
            ViewBag.UsuarioIdDefault = usuario?.Id;

            return View();
        }

        // POST: Agendamento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ResponsavelInterno,ResponsavelExterno,Turma,Descricao," +
    "Feedback,Observacoes,DtInicial,DtFinal,QtdPessoas,ProjetoId,UsuarioId,HasProjeto")] Agendamento agendamento)
        {
            if (!agendamento.HasProjeto)
            {
                agendamento.ProjetoId = null;
            }

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
        // POST: Agendamento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ResponsavelInterno," +
            "ResponsavelExterno,Turma," +
            "Descricao,Feedback,Observacoes,DtInicial,DtFinal,QtdPessoas,ProjetoId," +
            "UsuarioId,HasProjeto")] Agendamento agendamento)
        {
            if (id != agendamento.Id)
            {
                return NotFound();
            }

            if (!agendamento.HasProjeto)
            {
                agendamento.ProjetoId = null;
            }

            if (ModelState.IsValid)
            {
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
