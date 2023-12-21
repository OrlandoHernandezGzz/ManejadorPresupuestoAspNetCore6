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

			await repositoryAccountTypes.Create(accountType);

			return View();
		}
	}
}
