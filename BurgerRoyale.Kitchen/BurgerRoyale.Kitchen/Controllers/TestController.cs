using BurgerRoyale.Kitchen.Application.Models;
using BurgerRoyale.Kitchen.Domain.Contracts.IntegrationServices;
using BurgerRoyale.Kitchen.Domain.Contracts.Queues;
using BurgerRoyale.Kitchen.Infrastructure.QueueConfiguration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace BurgerRoyale.Kitchen.API.Controllers;

[Route("api/Test")]
[ApiController]
public class TestController(
    IMessageService messageService,
    IMessageQueue messageQueue) : ControllerBase
{
    [HttpGet("queues-test", Name = "Test queues")]
    [SwaggerOperation(
        Summary = "test",
        Description = "test queues.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> TestQueues()
    {
        var orderProductList = new List<Domain.Entities.OrderProduct>();
        var orderProduct = new Domain.Entities.OrderProduct() { ProductName = "Burger", ProductId = Guid.NewGuid(), Quantity = 2 };
        orderProductList.Add(orderProduct);
        await messageService.SendMessageAsync(
            messageQueue.OrderPreparationRequestQueue(),
            new RequestPreparationRequest
            {
                OrderId = Guid.NewGuid(),
                Products = orderProductList,
            }
        );

        return Ok();
    }
}
