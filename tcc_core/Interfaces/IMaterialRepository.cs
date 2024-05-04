using tcc_core.Models;

namespace tcc_core.Interfaces
{
    public interface IMaterialRepository
    {
        IEnumerable<Material> GetAllMateriais();
        Material GetMaterialById(int id);
        Material CreateMaterial(Material material);
        Material UpdateMaterial(int id, Material material);
        void DeleteMaterial(int id);
    }
}
