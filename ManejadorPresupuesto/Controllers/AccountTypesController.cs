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
        private readonly IUserServices userServices;

        public AccountTypesController(IRepositoryAccountTypes repositoryAccountTypes, IUserServices userServices)
		{
            this.repositoryAccountTypes = repositoryAccountTypes;
            this.userServices = userServices;
        }

		public async Task<IActionResult> Index()
		{
			var userId = userServices.GetUserId();
			var accountTypes = await repositoryAccountTypes.Get(userId);
			return View(accountTypes);
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

			accountType.UsuarioId = userServices.GetUserId();

			var accountTypeExist = await repositoryAccountTypes.Exist(accountType.Nombre, accountType.UsuarioId);

			if (accountTypeExist)
			{
				ModelState.AddModelError(nameof(accountType.Nombre), $"El nombre {accountType.Nombre} ya existe.");

				return View(accountType);
			}

			await repositoryAccountTypes.Create(accountType);

			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var userId = userServices.GetUserId();
			var accountType = await repositoryAccountTypes.GetById(id, userId);

			if (accountType is null)
			{
				return RedirectToAction("NotFound", "Home");
			}

			return View(accountType);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var userId = userServices.GetUserId();
			var accountType = await repositoryAccountTypes.GetById(id, userId);

			if (accountType is null)
			{
				return RedirectToAction("NotFound", "Home");
			}

			return View(accountType);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteAccountType(int id)
		{
			var userId = userServices.GetUserId();
			var accountType = await repositoryAccountTypes.GetById(id, userId);

			if (accountType is null)
			{
				return RedirectToAction("NotFound", "Home");
			}

			await repositoryAccountTypes.Delete(id);

			return RedirectToAction("Index");

		}

		[HttpPost]
		public async Task<IActionResult> Edit(AccountType accountType)
		{
			var userId = userServices.GetUserId();

			var accountTypeExist = await repositoryAccountTypes.GetById(accountType.Id, userId);

			if (accountTypeExist is null)
			{
				return RedirectToAction("NotFound", "Home");
			}

            await repositoryAccountTypes.Update(accountType);
			return RedirectToAction("Index");
        }

		[HttpGet]
		public async Task<IActionResult> CheckIfTypeAccountExists(string Nombre)
		{
			var userId = userServices.GetUserId();
			var existAccountType = await repositoryAccountTypes.Exist(Nombre, userId);

			if (existAccountType)
			{
				return Json($" El nombre {Nombre} ya existe");
			}

			return Json(true);
		}

	}
}
