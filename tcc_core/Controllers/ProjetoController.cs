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
    public class ProjetoController : Controller
    {
        private readonly AppDbContext _context;

        public ProjetoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Projeto
        public async Task<IActionResult> Index(string searchTerm, bool showInativos = false)
        {
            ViewData["CurrentFilter"] = searchTerm;
            ViewData["ShowInativos"] = showInativos;

            var projetos = _context.Projeto.Where(p => p.IsActive || showInativos);
            
            if (!String.IsNullOrEmpty(searchTerm))
            {
                projetos = projetos.Where(s => s.Titulo.Contains(searchTerm));
            }

            var projetoList = await projetos.ToListAsync();
            if (!projetoList.Any())
            {
                ViewData["Message"] = !String.IsNullOrEmpty(searchTerm) ?
                                      "Nenhum projeto encontrado para o termo pesquisado." :
                                      "Nenhum projeto cadastrado.";
            }

            return View(projetoList);
        }

        // GET: Projeto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projeto == null)
            {
                return NotFound();
            }

            var projeto = await _context.Projeto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projeto == null)
            {
                return NotFound();
            }

            return View(projeto);
        }

        // GET: Projeto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projeto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Coordenador,Natureza,DtInicio,DtFinal")] Projeto projeto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projeto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projeto);
        }

        // GET: Projeto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projeto == null)
            {
                return NotFound();
            }

            var projeto = await _context.Projeto.FindAsync(id);
            if (projeto == null)
            {
                return NotFound();
            }
            return View(projeto);
        }

        // POST: Projeto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Coordenador,Natureza,DtInicio,DtFinal")] Projeto projeto)
        {
            if (id != projeto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projeto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjetoExists(projeto.Id))
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
            return View(projeto);
        }

        // GET: Projeto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projeto == null)
            {
                return NotFound();
            }

            var projeto = await _context.Projeto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projeto == null)
            {
                return NotFound();
            }

            return View(projeto);
        }

        // POST: Projeto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projeto = await _context.Projeto.FindAsync(id);
            var movimentacaoAssociada = _context.Movimentacao.Any(m => m.ProjetoId == id);
            var agendamentoAssociado = _context.Agendamento.Any(m => m.ProjetoId == id);

            if (movimentacaoAssociada)
            {
                ModelState.AddModelError("", "Não é possível excluir" +
                    " este projeto, pois está associado a uma movimentação.");
            }
            if (agendamentoAssociado)
            {
                ModelState.AddModelError("", "Não é possível excluir" +
                   " este projeto, pois está associado a um agendamento.");
            }

            if (!ModelState.IsValid)
            {
                return View(projeto);
            }

            if (projeto != null)
            {
                //_context.Projeto.Remove(projeto);
                projeto.IsActive = false;
                _context.Update(projeto);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool ProjetoExists(int id)
        {
          return (_context.Projeto?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // POST: Projeto/Ativar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ativar(int id)
        {
            var projeto = await _context.Projeto.FindAsync(id);
            if (projeto == null)
            {
                return NotFound();
            }

            projeto.IsActive = true;
            _context.Update(projeto);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Projeto ativado com sucesso.";
            return RedirectToAction(nameof(Index));
        }
    }
}
