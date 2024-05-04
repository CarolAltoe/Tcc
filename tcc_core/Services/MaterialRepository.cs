using tcc_core.Data;
using tcc_core.Interfaces;
using tcc_core.Models;
using Microsoft.EntityFrameworkCore;


namespace tcc_core.Services
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly AppDbContext _context;

        public MaterialRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Material> GetAllMateriais()
        {
            return _context.Materiais.ToList();
        }

        public Material GetMaterialById(int id)
        {
            return _context.Materiais.Find(id);
        }

        public Material CreateMaterial(Material material)
        {
            if (material == null)
            {
                throw new ArgumentNullException(nameof(material));
            }

            _context.Materiais.Add(material);
            _context.SaveChanges();
            return material;
        }

        public Material UpdateMaterial(int id, Material material)
        {
            if (id != material.Id)
            {
                throw new ArgumentException("ID do material não corresponde.");
            }

            _context.Entry(material).State = EntityState.Modified;
            _context.SaveChanges();
            return material;
        }

        public void DeleteMaterial(int id)
        {
            var material = _context.Materiais.Find(id);
            if (material == null)
            {
                throw new ArgumentException("Material não encontrado.");
            }

            _context.Materiais.Remove(material);
            _context.SaveChanges();
        }
    }
}
