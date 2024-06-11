using Microsoft.EntityFrameworkCore;
using tcc_core.Models;


namespace tcc_core.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<UsuarioModel> Usuario { get; set; }
        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<Agendamento> Agendamento { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<Movimentacao> Movimentacao { get; set; }
        public DbSet<MovimentacaoMaterial> MovimentacaoMaterial{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Projeto)
                .WithMany(p => p.Agendamento)
                .HasForeignKey(a => a.ProjetoId)
                .OnDelete(DeleteBehavior.Restrict); // Evite exclusão em cascata

            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Agendamento)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict); // Evite exclusão em cascata

            modelBuilder.Entity<Movimentacao>()
                .HasOne(m => m.Projeto)
                .WithMany(p => p.Movimentacao)
                .HasForeignKey(m => m.ProjetoId)
                .OnDelete(DeleteBehavior.Restrict); // Evite exclusão em cascata

            modelBuilder.Entity<Movimentacao>()
                .HasOne(m => m.Usuario)
                .WithMany(u => u.Movimentacao)
                .HasForeignKey(m => m.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict); // Evite exclusão em cascata

            modelBuilder.Entity<MovimentacaoMaterial>()
                .HasKey(mm => new { mm.MovimentacaoId, mm.MaterialId });

            modelBuilder.Entity<MovimentacaoMaterial>()
                .HasOne(mm => mm.Movimentacao)
                .WithMany(m => m.MovimentacaoMaterial)
                .HasForeignKey(mm => mm.MovimentacaoId);

            modelBuilder.Entity<MovimentacaoMaterial>()
                .HasOne(mm => mm.Material)
                .WithMany(m => m.MovimentacaoMaterial)
                .HasForeignKey(mm => mm.MaterialId);
        }


    }

}