using BurgerRoyale.Kitchen.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BurgerRoyale.Kitchen.API.Controllers;

[Route("api/Order")]
[ApiController]
public class OrdersController(IPreparationRepository repository) : Controller
{
    [HttpGet(Name = "Get orders")]
    [SwaggerOperation(
         Summary = "Get all orders",
         Description = "Get all orders.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> GetOrders()
    {
        var test = await repository.GetAll();
        return Ok(test);
    }
}
