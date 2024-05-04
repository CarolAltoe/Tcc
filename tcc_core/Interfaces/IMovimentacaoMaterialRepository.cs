using tcc_core.Models;

namespace tcc_core.Interfaces
{
    public interface IMovimentacaoMaterialRepository
    {
        IEnumerable<MovimentacaoMaterial> GetAllMovimentacaoMateriais();
        MovimentacaoMaterial GetMovimentacaoMaterialById(int movimentacaoId, int materialId);
        MovimentacaoMaterial CreateMovimentacaoMaterial(MovimentacaoMaterial movimentacaoMaterial);
        MovimentacaoMaterial UpdateMovimentacaoMaterial(int movimentacaoId, int materialId, MovimentacaoMaterial movimentacaoMaterial);
        void DeleteMovimentacaoMaterial(int movimentacaoId, int materialId);
    }
}
