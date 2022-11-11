using Fibonacci.SecondApp.Models;
using Fibonacci.SecondApp.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Fibonacci.SecondApp.Controllers;

[ApiController]
[Route("[controller]")]
public class FibonacciController : ControllerBase
{
    private readonly ICalculationService calculationService;
    private readonly ILogger<FibonacciController> logger;
    public FibonacciController(ICalculationService calculationService, ILogger<FibonacciController> logger)
    {
        this.calculationService = calculationService;
        this.logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> PostNumber([FromBody] FibonacciNumberModel model)
    {
        try
        {
            logger.LogInformation("{calcId} {index} {number}",model.CalculationId, model.Index, model.Number);
            await calculationService.CalculateNextFibonacciNumber(model);
        }
        catch(Exception ex)
        {
            logger.LogError(ex.ToString());
            return BadRequest();
        }

        return Ok();
    }

}
