using tcc_core.Data;
using tcc_core.Models;

namespace tcc_core.Query
{
    public class MaterialQuery
    {
        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Material> GetMateriais([Service] AppDbContext context)
        {
            return context.Materiais;
        }

        [UseDbContext(typeof(AppDbContext))]
        public Material GetMaterialById([Service] AppDbContext context, int id)
        {
            return context.Materiais.FirstOrDefault(m => m.Id == id);
        }
    }
}
