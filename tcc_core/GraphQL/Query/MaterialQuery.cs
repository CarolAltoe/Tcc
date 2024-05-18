using tcc_core.Data;
using tcc_core.Models;

namespace tcc_core.Query
{
    public class MaterialQuery
    {
        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Material> GetMaterial([Service] AppDbContext context)
        {
            return context.Material;
        }

        [UseDbContext(typeof(AppDbContext))]
        public Material GetMaterialById([Service] AppDbContext context, int id)
        {
            return context.Material.FirstOrDefault(m => m.Id == id);
        }
    }
}
