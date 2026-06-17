using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System_uznawania_przychodow.DTOs.Payments;
using System_uznawania_przychodow.Services.ContractPaymentServices;

namespace System_uznawania_przychodow.Controllers;
 
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ContractPaymentsController : ControllerBase
{
    private readonly IContractPaymentService _contractPaymentService;
    
    public ContractPaymentsController(IContractPaymentService contractPaymentService)
    {
        _contractPaymentService = contractPaymentService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPayment([FromBody] AddPaymentDto dto)
    {
        var res = await _contractPaymentService.AddPaymentAsync(dto);
        return Created(string.Empty, new {ContractPaymentId = res});
    }
}