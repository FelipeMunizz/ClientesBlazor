using ClientesBlazor.Domain.InterfaceService;
using ClientesBlazor.Entities.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace ClientesBlazor.Client.Servico;

public class ClienteServico : IClienteService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public ClienteServico(HttpClient httpClient, JsonSerializerOptions options)
    {
        _httpClient = httpClient;
        _options = options;
    }

    public async Task<Clientes> AdicionarCliente(Clientes model)
    {
        var cliente = await _httpClient.PostAsJsonAsync("api/Cliente/AddCliente", model);
        var response = await cliente.Content.ReadFromJsonAsync<Clientes>();
        return response!;
    }

    public async Task<bool> DeletarCliente(int idCLiente)
    {
        var cliente = await _httpClient.DeleteAsync($"api/Cliente/DeleteCliente/{idCLiente}");
        var response = await cliente.Content.ReadFromJsonAsync<bool>();
        return response;
    }

    public async Task<Clientes> EditarCliente(Clientes model)
    {
        var cliente = await _httpClient.PutAsJsonAsync("api/Cliente/UpdateCliente", model);
        var response = await cliente.Content.ReadFromJsonAsync<Clientes>();
        return response;
    }

    public async Task<IList<Clientes>> ListaClientes()
    {
        var clientes = await _httpClient.GetAsync("api/Cliente/Clientes");
        var response = await clientes.Content.ReadFromJsonAsync<List<Clientes>>();
        return response!;
    }

    public async Task<Clientes> ObterCliente(int idCliente)
    {
        var cliente = await _httpClient.GetAsync($"api/Cliente/Cliente/{idCliente}");
        var response = await cliente.Content.ReadFromJsonAsync<Clientes>();
        return response!;
    }
}
