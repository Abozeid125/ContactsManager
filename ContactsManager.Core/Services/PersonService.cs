using Entities;
using OfficeOpenXml;
using RepositeryContracts;
using ServiceContracts;
using ServiceContracts.DTOS;
using ServiceContracts.Enums;
using Services.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class PersonService : IPersonService
	{
		private readonly IPersonsRepositery _personsRepositery;
		private readonly ICountryService _countryService;
       
      


        public PersonService(IPersonsRepositery personsRepositery , ICountryService countryService)
        {





			_personsRepositery = personsRepositery;
			_countryService = countryService;
        }
       


	

		public async Task< PersonResponse>  AddPerson(PersonAddRequest? personAddRequest)
		{
			if (personAddRequest == null)
			{
				throw new ArgumentNullException(nameof(personAddRequest));
				
			}
			//if (personAddRequest.Name == null) { 

			//throw new ArgumentException("Person name can't be blank");
			//}


			ValidationHelper.ModelValidations(personAddRequest);




			Person person = personAddRequest.ToPerson();
			person.PersonId = Guid.NewGuid();
			 await _personsRepositery.AddPerson(person);

			return person.ToPersonResponse();
			


		}

		public async Task< List<PersonResponse>> GEtAllPersons()
		{
			var persons = await _personsRepositery.GetAllPersons();

			return  persons.Select( p => p.ToPersonResponse() ).ToList();
		}

		public async Task< PersonResponse?> GetPersonByID(Guid? id)
		{
			
			if(id == null)
				return null;

			Person? person = await _personsRepositery.GetPersonById(id.Value);

			if(person == null)
				return null;
			return person.ToPersonResponse();



		}

		public async Task< List<PersonResponse>> GetFilteredPersons(string SearchBy, string? Value)
		{

			

			if (SearchBy == null || Value == null)
				return await GEtAllPersons();

			List<Person> matchedPersons = SearchBy switch


			{

				nameof(PersonResponse.Name) =>
				   matchedPersons = await _personsRepositery.GetFilteredPersons(p => p.Name.Contains(Value))
					,

				nameof(PersonResponse.Email) =>
						matchedPersons = await _personsRepositery.GetFilteredPersons(p => p.Email.Contains(Value))
					,

				nameof(PersonResponse.PersonId) =>
						matchedPersons = await _personsRepositery.GetFilteredPersons(p => p.PersonId.ToString().Contains(Value))
					,

				nameof(PersonResponse.Gender) =>
						matchedPersons = await _personsRepositery.GetFilteredPersons(p => p.Gender.ToString().Contains(Value))
					,

				nameof(PersonResponse.DateOfBirth) =>
						matchedPersons = await _personsRepositery.GetFilteredPersons(p => p.DateOfBirth.Value.ToString("yyyy-MM-dd").Contains(Value)),

				nameof(PersonResponse.Country) =>
						matchedPersons = await _personsRepositery.GetFilteredPersons(p => p.Country.CountryName.Contains(Value))

					,
				_ => await _personsRepositery.GetAllPersons(),












			};
			return matchedPersons.Select( p => p.ToPersonResponse()).ToList();






		}

		public async Task< List<PersonResponse>> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrderOptions)
		{
			if(string.IsNullOrEmpty(sortBy))
				return allPersons;

			List<PersonResponse> SortedPersons = (sortBy, sortOrderOptions)

				switch
			{
				(nameof(PersonResponse.Name), SortOrderOptions.Ascending) => allPersons.OrderBy(p => p.Name , StringComparer.OrdinalIgnoreCase ).ToList(),
				(nameof(PersonResponse.Name), SortOrderOptions.Descending) => allPersons.OrderByDescending(p => p.Name , StringComparer.OrdinalIgnoreCase ).ToList(),


				(nameof(PersonResponse.Email), SortOrderOptions.Ascending) => allPersons.OrderBy(p => p.Email, StringComparer.OrdinalIgnoreCase).ToList(),
				(nameof(PersonResponse.Email), SortOrderOptions.Descending) => allPersons.OrderByDescending(p => p.Email, StringComparer.OrdinalIgnoreCase).ToList(),


				(nameof(PersonResponse.DateOfBirth), SortOrderOptions.Ascending) => allPersons.OrderBy(p => p.DateOfBirth).ToList(),
				(nameof(PersonResponse.DateOfBirth), SortOrderOptions.Descending) => allPersons.OrderByDescending(p => p.DateOfBirth).ToList(),


				(nameof(PersonResponse.Age), SortOrderOptions.Ascending) => allPersons.OrderBy(p => p.Age).ToList(),
				(nameof(PersonResponse.Age), SortOrderOptions.Descending) => allPersons.OrderByDescending(p => p.Age).ToList(),



				_ => allPersons

			};
			return SortedPersons;




		}

		public async Task< PersonResponse> UpdatePerson(PersonUpdateRequest? personUpdateRequest)
		{
			if(personUpdateRequest == null) 
				throw new ArgumentNullException(nameof(personUpdateRequest));

			//model validation

			ValidationHelper.ModelValidations(personUpdateRequest);

			Person? matchedPerson = await _personsRepositery.GetPersonById(personUpdateRequest.Id);


			if(matchedPerson == null)

				throw new ArgumentException("The person doesn't exist");
			

			matchedPerson.Name = personUpdateRequest.Name;
			matchedPerson.Address = personUpdateRequest.Address;
			matchedPerson.Gender = personUpdateRequest.Gender.ToString();
			matchedPerson.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;
			matchedPerson.Email = personUpdateRequest.Email;
			matchedPerson.CountryId = personUpdateRequest.CountryId;
			matchedPerson.DateOfBirth = personUpdateRequest.DateOfBirth;

            await _personsRepositery.UpdatePerson(matchedPerson);

            return matchedPerson.ToPersonResponse();




		}

		public async Task< bool> DeletePerson(Guid? id)
		{
			
			if (id == null) throw new ArgumentNullException(nameof(id));
			
			Person? matchedPerson =await _personsRepositery.GetPersonById(id.Value);

			if(matchedPerson == null)
				return false;

			await  _personsRepositery.DeletePerson(matchedPerson.PersonId);

			return true;



		}

		public async Task<MemoryStream> GetPersonsExcel()
		{
			
			MemoryStream memoryStream = new MemoryStream();
			

			using (ExcelPackage excelPackage = new ExcelPackage())
			{

				ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("PersonsSheets");

				worksheet.Cells["A1"].Value = "Person Name"; 
				worksheet.Cells["B1"].Value = "Email"; 
				worksheet.Cells["C1"].Value = "Date of Birth"; 
				worksheet.Cells["D1"].Value = "Age"; 
				worksheet.Cells["E1"].Value = "Gender"; 
				worksheet.Cells["F1"].Value = "Country"; 
				worksheet.Cells["G1"].Value = "Address"; 
				worksheet.Cells["H1"].Value = "Receive News Letters";

				List<PersonResponse> persons = await GEtAllPersons();

				int row = 1;

				foreach(PersonResponse personResponse in persons)
				{
					row++;
					worksheet.Cells[$"A{row}"].Value = personResponse.Name;
					worksheet.Cells[$"B{row}"].Value = personResponse.Email;
					worksheet.Cells[$"C{row}"].Value = personResponse.DateOfBirth?.ToString("yyyy-MM-dd");
					worksheet.Cells[$"D{row}"].Value = personResponse.Age;
					worksheet.Cells[$"E{row}"].Value = personResponse.Gender;
					worksheet.Cells[$"F{row}"].Value = personResponse.Country;
					worksheet.Cells[$"G{row}"].Value = personResponse.Address;
					worksheet.Cells[$"H{row}"].Value = personResponse.ReceiveNewsLetters;


					




				}

				worksheet.Cells[$"A:H {row}"].AutoFitColumns();

				await excelPackage.SaveAsAsync(memoryStream);


			}

			memoryStream.Position = 0;
			return memoryStream;

		}
	}
}
