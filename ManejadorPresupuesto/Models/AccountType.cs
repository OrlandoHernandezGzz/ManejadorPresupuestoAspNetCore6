using ManejadorPresupuesto.Validations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ManejadorPresupuesto.Models
{
	public class AccountType //: IValidatableObject
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[FirstCapitalLetter]
        [Remote(action: "CheckIfTypeAccountExists", controller: "AccountTypes")]
        public string Nombre { get; set; }
		public int UsuarioId { get; set; }
		public int Orden { get; set; }

   //     public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
   //     {
			//if (Nombre != null && Nombre.Length > 0)
			//{
			//	var firstLetter = Nombre[0].ToString();

			//	if (firstLetter != firstLetter.ToUpper())
			//	{
			//		yield return new ValidationResult("La primera letra debe ser mayúscula", 
			//			new[] { nameof(Nombre) });
			//	}
			//}
   //     }
    }
}
