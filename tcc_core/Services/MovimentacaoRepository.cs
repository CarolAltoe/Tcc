using tcc_core.Data;
using tcc_core.Interfaces;
using tcc_core.Models;
using Microsoft.EntityFrameworkCore;


namespace tcc_core.Services
{
    public class MovimentacaoRepository : IMovimentacaoRepository
    {
        private readonly AppDbContext _context;

        public MovimentacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Movimentacao> GetAllMovimentacoes()
        {
            return _context.Movimentacoes.ToList();
        }

        public Movimentacao GetMovimentacaoById(int id)
        {
            return _context.Movimentacoes.Find(id);
        }

        public Movimentacao CreateMovimentacao(Movimentacao movimentacao)
        {
            if (movimentacao == null)
            {
                throw new ArgumentNullException(nameof(movimentacao));
            }

            _context.Movimentacoes.Add(movimentacao);
            _context.SaveChanges();
            return movimentacao;
        }

        public Movimentacao UpdateMovimentacao(int id, Movimentacao movimentacao)
        {
            if (id != movimentacao.Id)
            {
                throw new ArgumentException("ID da movimentação não corresponde.");
            }

            _context.Entry(movimentacao).State = EntityState.Modified;
            _context.SaveChanges();
            return movimentacao;
        }

        public void DeleteMovimentacao(int id)
        {
            var movimentacao = _context.Movimentacoes.Find(id);
            if (movimentacao == null)
            {
                throw new ArgumentException("Movimentação não encontrada.");
            }

            _context.Movimentacoes.Remove(movimentacao);
            _context.SaveChanges();
        }
    }
}
