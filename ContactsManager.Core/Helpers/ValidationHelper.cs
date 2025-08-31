using ServiceContracts.DTOS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helper
{
	public static class ValidationHelper
	{

		public static void ModelValidations ( object obj)
		{ 

		//Model validation
		ValidationContext validationContext = new ValidationContext(obj);
		List<ValidationResult> results = new List<ValidationResult>();
		bool IsValid = Validator.TryValidateObject(obj, validationContext, results, true);
			if (!IsValid)
			{

				throw new ArgumentException(results.FirstOrDefault()?.ErrorMessage);
			}
	}



}
}
