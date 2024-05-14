using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BurgerRoyale.Kitchen.API.Extensions;

public static class AddErrorsFromNotifications
{
	public static ModelStateDictionary AddErrorsFromNofifications(this ModelStateDictionary modelState, IEnumerable<Notification> notifications)
	{
		foreach (var item in notifications)
		{
			modelState.AddModelError(item.Key, item.Message);
		}

		return modelState;
	}
}
