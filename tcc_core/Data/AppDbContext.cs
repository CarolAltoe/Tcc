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

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<Material> Materiais { get; set; }
        public DbSet<Movimentacao> Movimentacoes { get; set; }
        public DbSet<MovimentacaoMaterial> MovimentacoesMateriais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Projeto)
                .WithMany(p => p.Agendamentos)
                .HasForeignKey(a => a.ProjetoId);

            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Agendamentos)
                .HasForeignKey(a => a.UsuarioId);

            modelBuilder.Entity<Movimentacao>()
                .HasOne(m => m.Projeto)
                .WithMany(p => p.Movimentacoes)
                .HasForeignKey(m => m.ProjetoId);

            modelBuilder.Entity<Movimentacao>()
                .HasOne(m => m.Usuario)
                .WithMany(u => u.Movimentacoes)
                .HasForeignKey(m => m.UsuarioId);

            modelBuilder.Entity<MovimentacaoMaterial>()
                .HasKey(mm => new { mm.MovimentacaoId, mm.MaterialId });

            modelBuilder.Entity<MovimentacaoMaterial>()
                .HasOne(mm => mm.Movimentacao)
                .WithMany(m => m.MovimentacoesMateriais)
                .HasForeignKey(mm => mm.MovimentacaoId);

            modelBuilder.Entity<MovimentacaoMaterial>()
                .HasOne(mm => mm.Material)
                .WithMany(m => m.MovimentacoesMateriais)
                .HasForeignKey(mm => mm.MaterialId);
        }
    }

}