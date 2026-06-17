using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System_uznawania_przychodow.DTOs.Revenues;
using System_uznawania_przychodow.Services.RevenueServices;

namespace System_uznawania_przychodow.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RevenueController : ControllerBase
{
    private readonly IRevenueService _revenueService;

    public RevenueController(IRevenueService revenueService)
    {
        _revenueService = revenueService;
    }

    [HttpGet]
    public async Task<IActionResult> CalculateRevenue([FromQuery] CalculateRevenueRequestDto dto)
    {
        var result = await _revenueService.CalculateRevenueAsync(dto);
        return Ok(result);
    }
}