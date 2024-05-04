using tcc_core.Data;
using tcc_core.Interfaces;
using tcc_core.Models;
using Microsoft.EntityFrameworkCore;


namespace tcc_core.Services
{
    public class MovimentacaoMaterialRepository : IMovimentacaoMaterialRepository
    {
        private readonly AppDbContext _context;

        public MovimentacaoMaterialRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<MovimentacaoMaterial> GetAllMovimentacaoMateriais()
        {
            return _context.MovimentacoesMateriais.ToList();
        }

        public MovimentacaoMaterial GetMovimentacaoMaterialById(int movimentacaoId, int materialId)
        {
            return _context.MovimentacoesMateriais.Find(movimentacaoId, materialId);
        }

        public MovimentacaoMaterial CreateMovimentacaoMaterial(MovimentacaoMaterial movimentacaoMaterial)
        {
            if (movimentacaoMaterial == null)
            {
                throw new ArgumentNullException(nameof(movimentacaoMaterial));
            }

            _context.MovimentacoesMateriais.Add(movimentacaoMaterial);
            _context.SaveChanges();
            return movimentacaoMaterial;
        }

        public MovimentacaoMaterial UpdateMovimentacaoMaterial(int movimentacaoId, int materialId, MovimentacaoMaterial movimentacaoMaterial)
        {
            if (movimentacaoId != movimentacaoMaterial.MovimentacaoId || materialId != movimentacaoMaterial.MaterialId)
            {
                throw new ArgumentException("ID da movimentação/material não corresponde.");
            }

            _context.Entry(movimentacaoMaterial).State = EntityState.Modified;
            _context.SaveChanges();
            return movimentacaoMaterial;
        }

        public void DeleteMovimentacaoMaterial(int movimentacaoId, int materialId)
        {
            var movimentacaoMaterial = _context.MovimentacoesMateriais.Find(movimentacaoId, materialId);
            if (movimentacaoMaterial == null)
            {
                throw new ArgumentException("MovimentaçãoMaterial não encontrado.");
            }

            _context.MovimentacoesMateriais.Remove(movimentacaoMaterial);
            _context.SaveChanges();
        }
    }
}
