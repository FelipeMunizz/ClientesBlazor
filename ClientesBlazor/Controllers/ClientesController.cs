using ClientesBlazor.Domain.InterfaceService;
using ClientesBlazor.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientesBlazor.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _service;

    public ClientesController(IClienteService service)
    {
        _service = service;
    }

    [HttpGet("Clientes")]
    public async Task<object> GetAllClientsAsync() => await _service.ListaClientes();

    [HttpGet("Clientes/{idCliente:int}")]
    public async Task<ActionResult<Clientes>> GetClientAsync(int idCliente) => await _service.ObterCliente(idCliente);

    [HttpPost("AddCliente")]
    public async Task<ActionResult<Clientes>> AddClientAsync(Clientes cliente) => await _service.AdicionarCliente(cliente);

    [HttpPut("UpdateCliente")]
    public async Task<IActionResult> UpdateClientAsync(Clientes cliente)
    {
        await _service.EditarCliente(cliente);
        return Ok();
    }

    [HttpDelete("DeleteCliente")]
    public async Task<ActionResult<bool>> DeleteClientAsync(int idCliente) => await _service.DeletarCliente(idCliente);
}
