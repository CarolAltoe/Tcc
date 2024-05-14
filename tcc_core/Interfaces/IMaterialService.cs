using tcc_core.Models;

namespace tcc_core.Interfaces
{
    public interface IMaterialService
    {
        IEnumerable<Material> GetAllMaterial();
        Material GetMaterialById(int id);
        Material CreateMaterial(Material material);
        Material UpdateMaterial(int id, Material material);
        void DeleteMaterial(int id);
    }
}
