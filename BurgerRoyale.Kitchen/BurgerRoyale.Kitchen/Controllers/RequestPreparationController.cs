using BurgerRoyale.Kitchen.API.Extensions;
using BurgerRoyale.Kitchen.Application.Contracts.UseCases;
using BurgerRoyale.Kitchen.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BurgerRoyale.Kitchen.API.Controllers;

[Route("api/RequestPreparation")]
[ApiController]
public class RequestPreparationController(IRequestPreparation requestPreparation) : ControllerBase
{
	[HttpPost(Name = "RequestPreparation")]
	[SwaggerOperation(
		Summary = "Request an order preparation",
		Description = "Sends an order to the kitchen")]
	[ProducesResponseType(typeof(RequestPreparationResponse),
		StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ValidationProblemDetails),
		StatusCodes.Status400BadRequest)]
	[ProducesDefaultResponseType]
	public async Task<ActionResult> RequestPayment([FromBody] RequestPreparationRequest request)
	{
		RequestPreparationResponse response = await requestPreparation.RequestAsync(request);

		if (!response.IsValid)
		{
			return ValidationProblem(ModelState.AddErrorsFromNofifications(response.Notifications));
		}

		return StatusCode(StatusCodes.Status201Created, response);
	}
}
