using tcc_core.Data;
using tcc_core.Interfaces;
using tcc_core.Models;
using Microsoft.EntityFrameworkCore;


namespace tcc_core.Services
{
    public class MovimentacaoService : IMovimentacaoService
    {
        private readonly AppDbContext _context;

        public MovimentacaoService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Movimentacao> GetAllMovimentacao()
        {
            return _context.Movimentacao.ToList();
        }

        public Movimentacao GetMovimentacaoById(int id)
        {
            return _context.Movimentacao.Find(id);
        }

        public Movimentacao CreateMovimentacao(Movimentacao movimentacao)
        {
            if (movimentacao == null)
            {
                throw new ArgumentNullException(nameof(movimentacao));
            }

            _context.Movimentacao.Add(movimentacao);
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
            var movimentacao = _context.Movimentacao.Find(id);
            if (movimentacao == null)
            {
                throw new ArgumentException("Movimentação não encontrada.");
            }

            _context.Movimentacao.Remove(movimentacao);
            _context.SaveChanges();
        }
    }
}
