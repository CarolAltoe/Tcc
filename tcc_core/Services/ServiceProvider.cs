
using tcc_core.Interfaces;

namespace tcc_core.Services
{
    public static class ServiceProvider 
    { 
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services 
                .AddScoped<IUsuarioService, UsuarioService>()
                .AddScoped<IProjetoService, ProjetoService>()
                .AddScoped<IAgendamentoService, AgendamentoService>()
                .AddScoped<IMovimentacaoService, MovimentacaoService>()
                .AddScoped<IMaterialService, MaterialService>()
                .AddScoped<IMovimentacaoMaterialService, MovimentacaoMaterialService>();
        }
    }
}
