using BurgerRoyale.Kitchen.Application.Models;

namespace BurgerRoyale.Kitchen.Application.Contracts.UseCases;

public interface IRequestPreparation
{
    Task<RequestPreparationResponse> RequestAsync(RequestPreparationRequest request);
}
