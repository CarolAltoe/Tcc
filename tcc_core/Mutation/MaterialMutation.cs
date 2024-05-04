using tcc_core.Data;
using Microsoft.EntityFrameworkCore;
using tcc_core.Models;

namespace tcc_core.Mutation
{
    public class MaterialMutation
    {
        public async Task<Material> AddMaterial([Service] AppDbContext context, Material material)
        {
            context.Materiais.Add(material);
            await context.SaveChangesAsync();
            return material;
        }

        public async Task<Material> UpdateMaterial([Service] AppDbContext context, int id, Material material)
        {
            if (id != material.Id)
            {
                throw new GraphQLException("IDs não coincidem.");
            }

            context.Entry(material).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Materiais.Any(e => e.Id == id))
                {
                    throw new GraphQLException("Material não encontrado.");
                }
                else
                {
                    throw;
                }
            }

            return material;
        }

        public async Task<Material> DeleteMaterial([Service] AppDbContext context, int id)
        {
            var material = await context.Materiais.FindAsync(id);
            if (material == null)
            {
                throw new GraphQLException("Material não encontrado.");
            }

            context.Materiais.Remove(material);
            await context.SaveChangesAsync();

            return material;
        }
    }
}