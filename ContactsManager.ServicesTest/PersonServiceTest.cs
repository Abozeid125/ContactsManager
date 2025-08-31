using Entities;

using ServiceContracts;
using ServiceContracts.DTOS;
using ServiceContracts.Enums;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace CRUDTests
{
	public class PersonServiceTest
	{

		private readonly IPersonService _personService;
		private readonly ICountryService _countryService;
		private readonly ITestOutputHelper _testOutputHelper;

		public PersonServiceTest(ITestOutputHelper testOutputHelper)
		{
			
            _countryService = new CountriesService(null);

            _personService = new PersonService(null,_countryService);
			_testOutputHelper = testOutputHelper;
		}


		#region AddPerson

		[Fact]
		public async Task AddPerson_NullPerson()
		{

			//Arrange
			PersonAddRequest? personAddRequest = null;



			//Assert
			await Assert.ThrowsAsync<ArgumentNullException>( async () => //Act  

			await _personService.AddPerson(personAddRequest));

		}

		[Fact]
		public async Task AddPerson_NullPersonName()
		{


			//Arrange
			PersonAddRequest? personAddRequest = new PersonAddRequest { Name = null };

			//Act  


			//Assert
		await	Assert.ThrowsAsync<ArgumentException>(async  () => await _personService.AddPerson(personAddRequest)
);



		}

		[Fact]
		public async Task AddPerson_WithProperData()
		{


			//Arrange
			PersonAddRequest? personAddRequest = new PersonAddRequest
			{
				Name = "Ahmed",
				CountryId = Guid.NewGuid(),
				Email = "dnivfniififv@kamil.com"
				,
				Gender = ServiceContracts.Enums.GenderOptions.Male
			};

			//Act  

			PersonResponse personResponse = await _personService.AddPerson(personAddRequest);

			List<PersonResponse> ListResponse = await _personService.GEtAllPersons();

			//Assert
			Assert.True(personResponse.PersonId != Guid.Empty);
			Assert.Contains(personResponse, ListResponse);




		}



		#endregion


		#region GetPersonByID

		[Fact]
		public async Task GetPersonByID_NullID()
		{

			//Arrange

			Guid? id = null;

			//Act
			PersonResponse? personResponse = await _personService.GetPersonByID(id);

			//assert
			Assert.Null(personResponse);





		}

		[Fact]

		public async Task GetPersonID_ValidID()
		{
			CountryAddRequest countryAddRequest = new CountryAddRequest
			{
				CountryName = "Canada"
			};

			CountryResponse countryResponse = await _countryService.AddCountry(countryAddRequest);

			PersonAddRequest personAddRequest = new PersonAddRequest
			{
				Name = "ahmed",
				Email = "Auadavnia@gmail.com",
				CountryId = countryResponse.CountryId,
				Gender = ServiceContracts.Enums.GenderOptions.Male

			};

			PersonResponse? personResponse_add = await _personService.AddPerson(personAddRequest);
			PersonResponse? personResponse_Get = await _personService.GetPersonByID(personResponse_add.PersonId);

			Assert.Equal(personResponse_add, personResponse_Get);


		}





		#endregion



		#region GetAllPersons


		[Fact]

		public async Task GetAllPersons_EmptyList()
		{
			List<PersonResponse> personResponses = await _personService.GEtAllPersons();

			Assert.Empty(personResponses);



		}

		[Fact]

		public async void GetAllPersons_NonEmptyList()
		{

			CountryAddRequest countryAddRequest1 = new CountryAddRequest { CountryName = "Egypt" };
			CountryAddRequest countryAddRequest2 = new CountryAddRequest { CountryName = "China" };

			CountryResponse countryResponse1 = await _countryService.AddCountry(countryAddRequest1);
			CountryResponse countryResponse2 = await _countryService.AddCountry(countryAddRequest2);


			PersonAddRequest personAddRequest1 = new PersonAddRequest
			{

				Name = "Ahmed",
				Email = "Ahmed@gmail.com",
				CountryId = countryResponse1.CountryId,
			};
			PersonAddRequest personAddRequest2 = new PersonAddRequest
			{

				Name = "Hassan",
				Email = "Hassan@gmail.com",
				CountryId = countryResponse1.CountryId,
			};
			PersonAddRequest personAddRequest3 = new PersonAddRequest
			{

				Name = "Hassan",
				Email = "Hassan@gmail.com",
				CountryId = countryResponse2.CountryId,
			};

			List<PersonAddRequest> personAddRequests = new List<PersonAddRequest> {

			personAddRequest1, personAddRequest2, personAddRequest3
			};
			List<PersonResponse> personResponsesAdd = new List<PersonResponse>();

			foreach (var personAddRequest in personAddRequests)
			{


				PersonResponse response = await _personService.AddPerson(personAddRequest);

				personResponsesAdd.Add(response);
			}

			foreach (var personResponse in personResponsesAdd)
			{
				_testOutputHelper.WriteLine(personResponse.Name + " " + personResponse.PersonId);
			}

			List<PersonResponse> personResponsesGet = await _personService.GEtAllPersons();
			foreach (var person in personResponsesGet)
			{
				_testOutputHelper.WriteLine(person.Name + " " + person.PersonId);

			}

			foreach (var personResponse in personResponsesAdd)
			{
				Assert.Contains(personResponse, personResponsesGet);


			}









		}


		#endregion

		#region GetFilteredPersons

		[Fact]

		public async Task GetFilteredPersons_EmptyValue()
		{

			CountryAddRequest countryAddRequest1 = new CountryAddRequest { CountryName = "Egypt" };
			CountryAddRequest countryAddRequest2 = new CountryAddRequest { CountryName = "China" };

			CountryResponse countryResponse1 = await _countryService.AddCountry(countryAddRequest1);
			CountryResponse countryResponse2 = await _countryService.AddCountry(countryAddRequest2);


			PersonAddRequest personAddRequest1 = new PersonAddRequest
			{

				Name = "Ahmed",
				Email = "Ahmed@gmail.com",
				CountryId = countryResponse1.CountryId,
			};
			PersonAddRequest personAddRequest2 = new PersonAddRequest
			{

				Name = "Hassan",
				Email = "Hassan@gmail.com",
				CountryId = countryResponse1.CountryId,
			};
			PersonAddRequest personAddRequest3 = new PersonAddRequest
			{

				Name = "Hassan",
				Email = "Hassan@gmail.com",
				CountryId = countryResponse2.CountryId,
			};

			List<PersonAddRequest> personAddRequests = new List<PersonAddRequest> {

			personAddRequest1, personAddRequest2, personAddRequest3
			};
			List<PersonResponse> personResponsesAdd = new List<PersonResponse>();

			foreach (var personAddRequest in personAddRequests)
			{


				PersonResponse response = await _personService.AddPerson(personAddRequest);

				personResponsesAdd.Add(response);
			}

			foreach (var personResponse in personResponsesAdd)
			{
				_testOutputHelper.WriteLine(personResponse.Name + " " + personResponse.PersonId);
			}

			List<PersonResponse> personResponsesSearch = await _personService.GetFilteredPersons(nameof(Person.Name), "");

			foreach (var person in personResponsesSearch)
			{
				_testOutputHelper.WriteLine(person.Name + " " + person.PersonId);

			}

			foreach (var personResponse in personResponsesAdd)
			{
				Assert.Contains(personResponse, personResponsesSearch);


			}









		}





		[Fact]

		public async Task GetFilteredPersons_BasedOnPersonName()
		{

			CountryAddRequest countryAddRequest1 = new CountryAddRequest { CountryName = "Egypt" };
			CountryAddRequest countryAddRequest2 = new CountryAddRequest { CountryName = "China" };

			CountryResponse countryResponse1 = await _countryService.AddCountry(countryAddRequest1);
			CountryResponse countryResponse2 = await _countryService.AddCountry(countryAddRequest2);


			PersonAddRequest personAddRequest1 = new PersonAddRequest
			{

				Name = "Ahmed",
				Email = "Ahmed@gmail.com",
				CountryId = countryResponse1.CountryId,
			};
			PersonAddRequest personAddRequest2 = new PersonAddRequest
			{

				Name = "Hassan",
				Email = "Hassan@gmail.com",
				CountryId = countryResponse1.CountryId,
			};
			PersonAddRequest personAddRequest3 = new PersonAddRequest
			{

				Name = "Hassan",
				Email = "Hassan@gmail.com",
				CountryId = countryResponse2.CountryId,
			};

			List<PersonAddRequest> personAddRequests = new List<PersonAddRequest> {

			personAddRequest1, personAddRequest2, personAddRequest3
			};
			List<PersonResponse> personResponsesAdd = new List<PersonResponse>();

			foreach (var personAddRequest in personAddRequests)
			{


				PersonResponse response = await _personService.AddPerson(personAddRequest);

				personResponsesAdd.Add(response);
			}

			foreach (var personResponse in personResponsesAdd)
			{
				_testOutputHelper.WriteLine(personResponse.Name + " " + personResponse.PersonId);
			}

			List<PersonResponse> personResponsesSearch = await _personService.GetFilteredPersons(nameof(Person.Name), "ah");
			foreach (var person in personResponsesSearch)
			{
				_testOutputHelper.WriteLine(person.Name + " " + person.PersonId);

			}

			foreach (var personResponse in personResponsesAdd)


			{
				if (personResponse.Name != null)
					if (personResponse.Name.Contains("ma", StringComparison.OrdinalIgnoreCase))

						Assert.Contains(personResponse, personResponsesSearch);


			}









		}




		#endregion

		#region GetSortedPersons


		[Fact]
		public async Task GetSortedPersons()
		{

			CountryAddRequest countryAddRequest1 = new CountryAddRequest { CountryName = "Egypt" };
			CountryAddRequest countryAddRequest2 = new CountryAddRequest { CountryName = "China" };

			CountryResponse countryResponse1 = await _countryService.AddCountry(countryAddRequest1);
			CountryResponse countryResponse2 = await _countryService.AddCountry(countryAddRequest2);


			PersonAddRequest personAddRequest1 = new PersonAddRequest
			{

				Name = "Ahmed",
				Email = "Ahmed@gmail.com",
				CountryId = countryResponse1.CountryId,
			};
			PersonAddRequest personAddRequest2 = new PersonAddRequest
			{

				Name = "Hassan",
				Email = "Hassan@gmail.com",
				CountryId = countryResponse1.CountryId,
			};
			PersonAddRequest personAddRequest3 = new PersonAddRequest
			{

				Name = "Hassan",
				Email = "Hassan@gmail.com",
				CountryId = countryResponse2.CountryId,
			};

			List<PersonAddRequest> personAddRequests = new List<PersonAddRequest> {

			personAddRequest1, personAddRequest2, personAddRequest3
			};
			List<PersonResponse> personResponsesAdd = new List<PersonResponse>();

			foreach (var personAddRequest in personAddRequests)
			{


				PersonResponse response = await _personService.AddPerson(personAddRequest);

				personResponsesAdd.Add(response);
			}

			List<PersonResponse> personSortedDesc = personResponsesAdd.OrderByDescending(x => x.Name).ToList();

			foreach (var personResponse in personResponsesAdd)
			{
				_testOutputHelper.WriteLine(personResponse.Name + " " + personResponse.PersonId);
			}


			List<PersonResponse> allPersons = await _personService.GEtAllPersons();

			List<PersonResponse> personResponsesSorted = await _personService.GetSortedPersons(allPersons, nameof(Person.Name), SortOrderOptions.Descending);
			foreach (var person in personResponsesSorted)
			{
				_testOutputHelper.WriteLine(person.Name + " " + person.PersonId);

			}


			for (int i = 0; i < personResponsesSorted.Count; i++)
			{

				Assert.Equal(personSortedDesc[i], personResponsesSorted[i]);


			}









		}

		#endregion


		#region updatePerson


		[Fact]

		//When PersonRequest is null

		public async Task UpdatePerson_NullPerson()
		{
			PersonUpdateRequest? personUpdateRequest = null;

			await Assert.ThrowsAsync<ArgumentNullException>( async () =>


			{

				await _personService.UpdatePerson(personUpdateRequest);

			});








		}

		public async Task UpdatePerson_InValidID()
		{
			PersonUpdateRequest personUpdateRequest = new PersonUpdateRequest() { Id = Guid.NewGuid() };


			Assert.ThrowsAsync<ArgumentException>(async () =>


			{

       await	_personService.UpdatePerson(personUpdateRequest);

			});








		}


		[Fact]

		public async Task Update_person_NUllName()
		{
			CountryAddRequest countryAddRequest = new CountryAddRequest()
			{
				CountryName = "Egypt",



			};

			CountryResponse countryResponse = await _countryService.AddCountry(countryAddRequest);


			PersonAddRequest personAddRequest = new PersonAddRequest()
			{
				Name = "Ahmed",
				CountryId = countryResponse.CountryId,
				Email = "rniiwvi@gmail.com",
				//DateOfBirth = new DateTime(29 - 09 - 2005),
				Gender = GenderOptions.Male

			};

			PersonResponse personResponse = await _personService.AddPerson(personAddRequest);


			PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();

			personUpdateRequest.Name = null;


			await Assert.ThrowsAsync<ArgumentException>( async () =>

			{
				await _personService.UpdatePerson(personUpdateRequest);

			});









		}


		[Fact]

		public async Task Update_person_UpdateNameEmail()
		{
			CountryAddRequest countryAddRequest = new CountryAddRequest()
			{
				CountryName = "Egypt",



			};

			CountryResponse countryResponse = await _countryService.AddCountry(countryAddRequest);


			PersonAddRequest personAddRequest = new PersonAddRequest()
			{
				Name = "Ahmed",
				CountryId = countryResponse.CountryId,
				Email = "rniiwvi@gmail.com",
				Gender = GenderOptions.Male
				//	DateOfBirth = new DateTime(29 - 09 - 2005)

			};

			PersonResponse personResponse = await _personService.AddPerson(personAddRequest);


			PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();

			personUpdateRequest.Name = "Hassan";
			personUpdateRequest.Email = "Hassan4444@gmail.com";

			PersonResponse person_updated = await _personService.UpdatePerson(personUpdateRequest);

			PersonResponse? person_expected = await _personService.GetPersonByID(personResponse.PersonId);

			Assert.Equal(person_expected, person_updated);















		}


		#endregion


		#region Delete_Person

		[Fact]

		public async Task DeletePerson_ValidId()
		{


			CountryAddRequest countryAddRequest = new CountryAddRequest()
			{
				CountryName = "Egypt",



			};

			CountryResponse countryResponse = await _countryService.AddCountry(countryAddRequest);


			PersonAddRequest personAddRequest = new PersonAddRequest()
			{
				Name = "Ahmed",
				CountryId = countryResponse.CountryId,
				Email = "rniiwvi@gmail.com",
				Gender = GenderOptions.Male
				//	DateOfBirth = new DateTime(29 - 09 - 2005)

			};

			PersonResponse personResponse = await _personService.AddPerson(personAddRequest);

			bool IsDeleted =await _personService.DeletePerson(personResponse.PersonId);

			Assert.True(IsDeleted);




		}



		[Fact]

		public async Task DeletePerson_InValidId()
		{


			CountryAddRequest countryAddRequest = new CountryAddRequest()
			{
				CountryName = "Egypt",



			};

			CountryResponse countryResponse = await _countryService.AddCountry(countryAddRequest);


			PersonAddRequest personAddRequest = new PersonAddRequest()
			{
				Name = "Ahmed",
				CountryId = countryResponse.CountryId,
				Email = "rniiwvi@gmail.com",
				Gender = GenderOptions.Male
				//	DateOfBirth = new DateTime(29 - 09 - 2005)

			};

			PersonResponse personResponse = await _personService.AddPerson(personAddRequest);

			bool IsDeleted = await _personService.DeletePerson(Guid.NewGuid());

			Assert.False(IsDeleted);




		}


		#endregion
	}



	}










