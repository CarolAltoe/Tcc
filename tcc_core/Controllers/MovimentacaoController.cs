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
using tcc_core.Models.Enuns;

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
        public async Task<IActionResult> Index(string searchTerm)
        {
            ViewData["CurrentFilter"] = searchTerm;

            var movimentacoes = from m in _context.Movimentacao
                .Include(m => m.Projeto)
                .Include(m => m.Usuario)
                .Include(m => m.MovimentacaoMaterial)
                .ThenInclude(mm => mm.Material)
                  select m;

            if (!String.IsNullOrEmpty(searchTerm))
            {
                movimentacoes = movimentacoes.Where(s =>
                   // s.TipoMovimentacao.Contains(searchTerm) ||
                    s.Responsavel.Contains(searchTerm));
            }

            var movimentacaoList = await movimentacoes.ToListAsync();
            if (!movimentacaoList.Any())
            {
                ViewData["Message"] = !String.IsNullOrEmpty(searchTerm) ?
                                      "Nenhuma movimentação encontrada para o termo pesquisado." :
                                      "Nenhuma movimentação cadastrada.";
            }
           
           /* var movimentacoes = await _context.Movimentacao
                .Include(m => m.Projeto)
                .Include(m => m.Usuario)
                .Include(m => m.MovimentacaoMaterial)
                .ThenInclude(mm => mm.Material)
                .ToListAsync();*/
          //  return View(movimentacoes);
            return View(await movimentacoes.ToListAsync());
        }

        // GET: Movimentacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacao
                .Include(m => m.Projeto)
                .Include(m => m.Usuario)
                .Include(m => m.MovimentacaoMaterial)
                .ThenInclude(mm => mm.Material)
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
            var email = User.Identity.Name;
            var usuario = _context.Usuario.FirstOrDefault(u => u.Email == email);

            ViewData["Materiais"] = new SelectList(_context.Material, "Id", "Descricao");
            ViewData["ProjetoId"] = new SelectList(_context.Projeto, "Id", "Titulo");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "NomeCompleto", usuario?.Id);
            ViewBag.UsuarioIdDefault = usuario?.Id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Responsavel," +
            "TipoMovimentacao,DtMovimentacao,HasProjeto,UsuarioId" +
            ",ProjetoId")] Movimentacao movimentacao, int[] materiaisIds, 
            decimal[] quantidades)
        {
            if (!movimentacao.HasProjeto)
            {
                movimentacao.ProjetoId = null;
            }

            if (ModelState.IsValid)
            {
                for (int i = 0; i < materiaisIds.Length; i++)
                {
                    var materialId = materiaisIds[i];
                    var quantidade = quantidades[i];

                    var material = await _context.Material.FindAsync(materialId);
                    if (material == null)
                    {
                        ModelState.AddModelError(string.Empty, $"Material com ID {materialId} não encontrado.");
                        return View(movimentacao);
                    }

                    if (movimentacao.TipoMovimentacao == TipoMovimentacao.Entrada)
                    {
                        material.QuantidadeAtual += quantidade;
                    }
                    else if (movimentacao.TipoMovimentacao == TipoMovimentacao.Saida)
                    {
                        material.QuantidadeAtual -= quantidade;
                        if (material.QuantidadeAtual < 0)
                        {
                            ModelState.AddModelError(string.Empty, $"Quantidade insuficiente para o material com ID {materialId}.");
                            return View(movimentacao);
                        }
                    }

                    movimentacao.MovimentacaoMaterial.Add(new MovimentacaoMaterial { 
                        MaterialId = materialId, Quantidade = quantidade });

                    _context.Update(material);
                }

                _context.Add(movimentacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Materiais"] = new SelectList(_context.Material, "Id", "Descricao");
            ViewData["ProjetoId"] = new SelectList(_context.Projeto, "Id", "Titulo", movimentacao.ProjetoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "NomeCompleto", movimentacao.UsuarioId);
            return View(movimentacao);
        }

        // GET: Movimentacao/Edit/5
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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimentacao == null)
            {
                return NotFound();
            }

            ViewData["Materiais"] = new SelectList(_context.Material, "Id", "Descricao");
            ViewData["ProjetoId"] = new SelectList(_context.Projeto, "Id", "Titulo");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "NomeCompleto");
            return View(movimentacao);
        }

        // POST: Movimentacao/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Responsavel," +
            "TipoMovimentacao,DtMovimentacao,HasProjeto,UsuarioId," +
            "ProjetoId")] Movimentacao movimentacao, int[] materiaisIds,
            decimal[] quantidades)
        {
            if (id != movimentacao.Id)
            {
                return NotFound();
            }

            if (!movimentacao.HasProjeto)
            {
                movimentacao.ProjetoId = null;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingMovimentacao = await _context.Movimentacao
                        .Include(m => m.Projeto)
                        .Include(m => m.Usuario)
                        .Include(m => m.MovimentacaoMaterial)
                        .FirstOrDefaultAsync(m => m.Id == id);

                    if (existingMovimentacao == null)
                    {
                        return NotFound();
                    }

                    existingMovimentacao.Responsavel = movimentacao.Responsavel;
                    existingMovimentacao.TipoMovimentacao = movimentacao.TipoMovimentacao;
                    existingMovimentacao.DtMovimentacao = movimentacao.DtMovimentacao;
                    existingMovimentacao.HasProjeto = movimentacao.HasProjeto;
                    existingMovimentacao.UsuarioId = movimentacao.UsuarioId;
                    existingMovimentacao.ProjetoId = movimentacao.ProjetoId;

                    // Ajustar as quantidades atuais dos materiais antes de limpar MovimentacaoMaterial
                    foreach (var movimentacaoMaterial in existingMovimentacao.MovimentacaoMaterial)
                    {
                        var material = await _context.Material.FindAsync(movimentacaoMaterial.MaterialId);
                        if (material != null)
                        {
                            if (existingMovimentacao.TipoMovimentacao == TipoMovimentacao.Entrada)
                            {
                                material.QuantidadeAtual -= movimentacaoMaterial.Quantidade;
                            }
                            else if (existingMovimentacao.TipoMovimentacao == TipoMovimentacao.Saida)
                            {
                                material.QuantidadeAtual += movimentacaoMaterial.Quantidade;
                            }
                        }
                    }

                    // Atualizar materiais associados
                    existingMovimentacao.MovimentacaoMaterial.Clear();

                    // Adicione os novos materiais e ajuste QuantidadeAtual
                    foreach (var (materialId, quantidade) in materiaisIds.Zip(quantidades, (id, qtd) => (id, qtd)))
                    {
                        var material = await _context.Material.FindAsync(materialId);
                        if (material == null)
                        {
                            ModelState.AddModelError(string.Empty, $"Material com ID {materialId} não encontrado.");
                            return View(movimentacao);
                        }

                        if (movimentacao.TipoMovimentacao == TipoMovimentacao.Entrada)
                        {
                            material.QuantidadeAtual += quantidade;
                        }
                        else if (movimentacao.TipoMovimentacao == TipoMovimentacao.Saida)
                        {
                            material.QuantidadeAtual -= quantidade;
                            if (material.QuantidadeAtual < 0)
                            {
                                ModelState.AddModelError(string.Empty, $"Quantidade insuficiente para o material com ID {materialId}.");
                                return View(movimentacao);
                            }
                        }

                        existingMovimentacao.MovimentacaoMaterial.Add(new MovimentacaoMaterial { MaterialId = materialId, Quantidade = quantidade });
                        _context.Update(material); // Atualize o material no contexto
                    }

                    _context.Update(existingMovimentacao);
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
            ViewData["Materiais"] = new SelectList(_context.Material, "Id", "Descricao");
            ViewData["ProjetoId"] = new SelectList(_context.Projeto, "Id", "Titulo", movimentacao.ProjetoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "NomeCompleto", movimentacao.UsuarioId);
            return View(movimentacao);
        }

        // GET: Movimentacao/Delete/5
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

        // POST: Movimentacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movimentacao = await _context.Movimentacao
                .Include(m => m.Usuario)
                .Include(m => m.Projeto)
                .Include(m => m.MovimentacaoMaterial)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movimentacao == null)
            {
                return NotFound();
            }

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


