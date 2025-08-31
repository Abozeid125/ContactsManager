using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace CRUDexample.Controllers
{




	[Route("Countries")]
	public class CountriesController : Controller
	{
		private readonly ICountryService _countryService;

		public CountriesController(ICountryService countryService) 
	    {
			_countryService = countryService;
		}

		[Route("UploadExcelFile")]
		public IActionResult UploadExcelFile()
		{


			return View();
		}

		[Route("UploadExcelFile")]
		[HttpPost]

		public async Task<IActionResult> UploadExcelFile(IFormFile excelFile)
		{

			if(excelFile == null || excelFile.Length == 0)
			{

				ViewBag.ErrorMessage = "Please selext .xlsx file";
				return View();



			}
			if(!Path.GetExtension(excelFile.FileName).Equals(".xlsx",StringComparison.OrdinalIgnoreCase))
			{

				ViewBag.ErrorMessage = "UnSupported file. 'xlsx' file is expected ";
				return View();


			}

		int AddedCountries = 	await _countryService.UplaodCountriesFromExcel(excelFile);

			ViewBag.Message = $"{AddedCountries} Countries where added";
			return View();


		}
	}
}
