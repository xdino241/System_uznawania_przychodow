using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System_uznawania_przychodow.DTOs.Clients;
using System_uznawania_przychodow.Services;

namespace System_uznawania_przychodow.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;
    
    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }
    
    [HttpPost("individual")]
    public async Task<IActionResult> AddIndividualClient([FromBody] CreateIndividualClientDto dto)
    {
        var clientId = await _clientService.AddIndividualClientAsync(dto); 
        return Created(string.Empty, new {id = clientId});
    }

    [HttpPost("company")]
    public async Task<IActionResult> AddCompanyClient([FromBody] CreateCompanyClientDto dto)
    {
        var clientId = await _clientService.AddCompanyClientAsync(dto); 
        return Created(string.Empty, new {id = clientId});
    }

    [HttpPut("individual/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateIndividualClient(int id, [FromBody] UpdateIndividualClientDto dto)
    { 
        await _clientService.UpdateIndividualClientAsync(id, dto);
        return NoContent();
    }

    [HttpPut("company/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCompanyClient(int id, [FromBody] UpdateCompanyClientDto dto)
    {
        await _clientService.UpdateCompanyClientAsync(id, dto);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        await _clientService.DeleteClientAsync(id); 
        return NoContent();
    }
}