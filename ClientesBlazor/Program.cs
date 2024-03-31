using ClientesBlazor.Components;
using ClientesBlazor.Domain.InterfaceRepository.IClientes;
using ClientesBlazor.Domain.InterfaceRepository.IGeneric;
using ClientesBlazor.Domain.InterfaceService;
using ClientesBlazor.Domain.Service;
using ClientesBlazor.Helper.Configuracoes;
using ClientesBlazor.Infra.Context;
using ClientesBlazor.Infra.Repository.ClientRepository;
using ClientesBlazor.Infra.Repository.Generic;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(Configuration.ConectionString));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSingleton(typeof(InterfaceGeneric<>), typeof(RepositoryGeneric<>));
builder.Services.AddScoped<InterfaceCliente, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClientesService>();

builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration.GetSection("BaseAddress").Value!)
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ClientesBlazor.Client._Imports).Assembly);

app.Run();
