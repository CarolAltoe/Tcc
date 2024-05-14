using tcc_core.Data;
using tcc_core.Interfaces;
using tcc_core.Models;
using Microsoft.EntityFrameworkCore;


namespace tcc_core.Services
{
    public class MovimentacaoMaterialService : IMovimentacaoMaterialService
    {
        private readonly AppDbContext _context;

        public MovimentacaoMaterialService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<MovimentacaoMaterial> GetAllMovimentacaoMaterial()
        {
            return _context.MovimentacaoMaterial.ToList();
        }

        public MovimentacaoMaterial GetMovimentacaoMaterialById(int movimentacaoId, int materialId)
        {
            return _context.MovimentacaoMaterial.Find(movimentacaoId, materialId);
        }

        public MovimentacaoMaterial CreateMovimentacaoMaterial(MovimentacaoMaterial movimentacaoMaterial)
        {
            if (movimentacaoMaterial == null)
            {
                throw new ArgumentNullException(nameof(movimentacaoMaterial));
            }

            _context.MovimentacaoMaterial.Add(movimentacaoMaterial);
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
            var movimentacaoMaterial = _context.MovimentacaoMaterial.Find(movimentacaoId, materialId);
            if (movimentacaoMaterial == null)
            {
                throw new ArgumentException("MovimentaçãoMaterial não encontrado.");
            }

            _context.MovimentacaoMaterial.Remove(movimentacaoMaterial);
            _context.SaveChanges();
        }
    }
}
