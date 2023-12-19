using ManejadorPresupuesto.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManejadorPresupuesto.Controllers
{
	public class AccountTypesController : Controller
	{
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(AccountType accountType)
		{

			if (!ModelState.IsValid)
			{
				return View(accountType);
			}

			return View();
		}
	}
}
