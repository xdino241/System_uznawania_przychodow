using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System_uznawania_przychodow.DTOs.Contracts;
using System_uznawania_przychodow.Services;

namespace System_uznawania_przychodow.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ContractsController : ControllerBase
{
    private readonly IContractService _contractService;
    public ContractsController(IContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateContract([FromBody] CreateContractDto dto)
    {
        var res = await _contractService.CreateContractAsync(dto);
        return Created(string.Empty, new { ContractId = res });
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContract(int id)
    {
        await _contractService.DeleteContractAsync(id);
        return NoContent();
    }
}