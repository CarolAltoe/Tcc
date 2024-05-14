using tcc_core.Data;
using tcc_core.Interfaces;
using tcc_core.Models;
using Microsoft.EntityFrameworkCore;


namespace tcc_core.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly AppDbContext _context;

        public MaterialService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Material> GetAllMaterial()
        {
            return _context.Material.ToList();
        }

        public Material GetMaterialById(int id)
        {
            return _context.Material.Find(id);
        }

        public Material CreateMaterial(Material material)
        {
            if (material == null)
            {
                throw new ArgumentNullException(nameof(material));
            }

            _context.Material.Add(material);
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
            var material = _context.Material.Find(id);
            if (material == null)
            {
                throw new ArgumentException("Material não encontrado.");
            }

            _context.Material.Remove(material);
            _context.SaveChanges();
        }
    }
}
