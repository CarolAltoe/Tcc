using tcc_core.Data;
using Microsoft.EntityFrameworkCore;
using tcc_core.Mutation;
using tcc_core.Query;
using System;
using tcc_core.Interfaces;
using tcc_core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddServices();

builder.Services.AddSwaggerGen();

builder.Services.AddGraphQLServer()
    .AddQueryType<UsuarioQuery>()
    .AddQueryType<ProjetoQuery>()
    .AddQueryType<MaterialQuery>()
    .AddQueryType<AgendamentoQuery>()
    .AddQueryType<MovimentacaoQuery>()
    .AddMutationType<UsuarioMutation>()
    .AddMutationType<ProjetoMutation>()
    .AddMutationType<MaterialMutation>()
    .AddMutationType<AgendamentoMutation>()
    .AddMutationType<MovimentacaoMutation>()
    .AddFiltering()
    .AddSorting();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGraphQL("/graphql/lab");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});

//Migrations
using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
context.Database.Migrate();

app.Run();



/*
builder.Services.AddTransient<UsuarioQuery>();
builder.Services.AddTransient<ProjetoQuery>();
builder.Services.AddTransient<AgendamentoQuery>();
builder.Services.AddTransient<MovimentacaoQuery>();
builder.Services.AddTransient<MaterialQuery>();
builder.Services.AddTransient<RootQuery>();

builder.Services.AddTransient<UsuarioMutation>();
builder.Services.AddTransient<ProjetoMutation>();
builder.Services.AddTransient<AgendamentoMutation>();
builder.Services.AddTransient<MovimentacaoMutation>();
builder.Services.AddTransient<MaterialMutation>();
builder.Services.AddTransient<RootMutation>();
*/
