using CRUDexample.Filters.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;
using ServiceContracts;
using ServiceContracts.DTOS;
using ServiceContracts.Enums;
using Services;

namespace CRUDexample.Controllers
{

	[Route("persons")]
	public class PersonController : Controller
	{
        private readonly IPersonService _personService;
		private readonly ICountryService _countryService;

		public PersonController( IPersonService personService , ICountryService countryService) 
		
		{
            _personService = personService;
			_countryService = countryService;
		}

		[Route("[action]")]
		[Route("/")]
		[TypeFilter(typeof(PersonListActionFilter))]
		public async Task< IActionResult> Index(string searchBy, string? searchString, string sortBy = nameof(PersonResponse.Name), SortOrderOptions sortOrderOptions = SortOrderOptions.Ascending)
		{

			ViewBag.SearchFields = new Dictionary<string, string>
			{
				{ nameof(PersonResponse.Name), "Person Name" },
				{ nameof(PersonResponse.Email), "Email" },
				{ nameof(PersonResponse.DateOfBirth), "Date of Birth" },
				{ nameof(PersonResponse.Age), "Age" },
				{ nameof(PersonResponse.Gender), "Gender" },
				{ nameof(PersonResponse.Country), "Country" },
				{ nameof(PersonResponse.Address), "Address" },
				{ nameof(PersonResponse.ReceiveNewsLetters), "Receive News Letter" },


			};

			List<PersonResponse> persons =await  _personService.GetFilteredPersons(searchBy, searchString);

			ViewBag.CurrentSearchString = searchString;
			ViewBag.CurrentSearchBy = searchBy;
			ViewBag.CurrentSortby = sortBy;
			ViewBag.CurrentSortOrder = sortOrderOptions.ToString();


			 List<PersonResponse> sortedPersons = await _personService.GetSortedPersons(persons, sortBy, sortOrderOptions);

			return View(sortedPersons);
		}


		[Route("[action]")]

		[HttpGet]
        public async Task< IActionResult> Create()
        {

            List<CountryResponse> countries = await _countryService.GetAllCountries();
            ViewBag.Countries = countries.Select(temp =>
              new SelectListItem() { Text = temp.CountryName, Value = temp.CountryId.ToString() }
            );

            //new SelectListItem() { Text="Harsha", Value="1" }
            //<option value="1">Harsha</option>
            return View();

        }


		[HttpPost]
		[Route("[action]")]
        [TypeFilter(typeof(PersonCreateEditActionFilter))]


        public async Task< IActionResult> Create(PersonAddRequest personRequest)
		{
			
			PersonResponse personResponse = await _personService.AddPerson(personRequest);




			return RedirectToAction("Index", "Person");

		}


		[HttpGet]
		[Route("[action]/{personid}")]
		public async Task<IActionResult> Edit(Guid? personId)
		{
			PersonResponse? personResponse = await _personService.GetPersonByID(personId);

			if (personResponse == null)
			{
				return RedirectToAction("Index", "Person");
			}

			PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();
			List<CountryResponse> countries =await _countryService.GetAllCountries();

			ViewBag.errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
			ViewBag.countries = countries.Select(temp => new SelectListItem { Text = temp.CountryName, Value = temp.CountryId.ToString() });

			return View(personUpdateRequest);




		}

		[HttpPost]
		[Route("[action]/{personId}")]
        [TypeFilter(typeof(PersonCreateEditActionFilter))]


        public async Task< IActionResult> Edit(PersonUpdateRequest personRequest)
		{

			PersonResponse? personResponse =await _personService.GetPersonByID(personRequest.Id);

			if (personResponse == null)
			{
				return RedirectToAction("Index");

			}
			


				  List<string> errors = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage).ToList();
				ViewBag.errors = errors;

			await _personService.UpdatePerson(personRequest);

			return View(personRequest);


		}

		[HttpGet]
		[Route("[action]/{personId}")]


		public async Task<IActionResult> Delete( Guid? personId)
		
		{

			PersonResponse? personResponse = await _personService.GetPersonByID(personId);

			if (personResponse == null)
			{

				return RedirectToAction("Index");
			}







			return View(personResponse);
		
		
		}


		[HttpPost]
		[Route("[action]/{personId}")]

		public async Task<IActionResult> Delete(PersonResponse personResponse)
		{

			PersonResponse? response = await _personService.GetPersonByID(personResponse.PersonId);

			if (response == null)
				return RedirectToAction("Index");

			await _personService.DeletePerson(response.PersonId);

			return RedirectToAction("Index");




		}

		public async Task<IActionResult> PersonsPDF()
		{

			List<PersonResponse> persons = await _personService.GEtAllPersons();

			return new ViewAsPdf("PersonsPDF", persons, ViewData)
			{
				PageMargins = new Rotativa.AspNetCore.Options.Margins() { Top = 20, Right = 20, Bottom = 20, Left = 20 },
				PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape

			};


		}

		[Route("PersonsExcel")]

		public async Task<IActionResult> PersonsExcel()
		{
			MemoryStream memoryStream = await _personService.GetPersonsExcel();
			return File(memoryStream,
				"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
				"persons.xlsx");
		}

	}
}
