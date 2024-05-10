using BurgerRoyale.Kitchen.Application.Contracts.UseCases;
using BurgerRoyale.Kitchen.Application.Models;
using BurgerRoyale.Kitchen.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BurgerRoyale.Kitchen.API.Controllers;

[Route("api/Order")]
[ApiController]
public class OrdersController(IGetOrders service) : Controller
{
    [HttpGet(Name = "Get orders")]
    [SwaggerOperation(
         Summary = "Get all orders",
         Description = "Get all orders.")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> GetOrders()
    {
        var result = await service.GetAllAsync();
        return Ok(result);
    }

    [HttpPost("{id:Guid}/update", Name = "Update")]
    [SwaggerOperation(
        Summary = "Update",
        Description = "Update an order status.")]
    [ProducesResponseType(typeof(OrderResponse),
        StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails),
        StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> RequestPayment(Guid id, [FromQuery] OrderStatus? orderStatus)
    {
        var response = await service.UpdateOrderAsync(id, orderStatus);
        return Ok(response);
    }
}
