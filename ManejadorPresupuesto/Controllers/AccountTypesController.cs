using Dapper;
using ManejadorPresupuesto.Models;
using ManejadorPresupuesto.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ManejadorPresupuesto.Controllers
{
	public class AccountTypesController : Controller
	{
        private readonly IRepositoryAccountTypes repositoryAccountTypes;

        public AccountTypesController(IRepositoryAccountTypes repositoryAccountTypes)
		{
            this.repositoryAccountTypes = repositoryAccountTypes;
        }

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(AccountType accountType)
		{

			if (!ModelState.IsValid)
			{
				return View(accountType);
			}

			accountType.UsuarioId = 1;

			var accountTypeExist = await repositoryAccountTypes.Exist(accountType.Nombre, accountType.UsuarioId);

			if (accountTypeExist)
			{
				ModelState.AddModelError(nameof(accountType.Nombre), $"El nombre {accountType.Nombre} ya existe.");

				return View(accountType);
			}

			await repositoryAccountTypes.Create(accountType);

			return View();
		}

		[HttpGet]
		public async Task<IActionResult> CheckIfTypeAccountExists(string Nombre)
		{
			var userId = 1;
			var existAccountType = await repositoryAccountTypes.Exist(Nombre, userId);

			if (existAccountType)
			{
				return Json($" El nombre {Nombre} ya existe");
			}

			return Json(true);
		}

	}
}
