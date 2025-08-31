using Entities;

using Microsoft.Extensions.Options;
using ServiceContracts;
using ServiceContracts.DTOS;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTests
{
	
	public class Countries
	{
		private readonly ICountryService _countryService;

		public Countries(ICountryService countryService )
		{

			


            _countryService = new CountriesService(null);

		}
		#region  AddCountry()
		//When CountryaddRequest is null, it should throw argument null exception
		[Fact] 

		public async Task AddCountry_NullCountry()
		{
			//Arrange
			CountryAddRequest? request = null;

			

			//Arrange

			 await Assert.ThrowsAsync<ArgumentNullException>( async () =>
			{
				//Act 
				await _countryService.AddCountry(request);

			});



		}

		//When CountryName is null, it should throw argument exception

		[Fact]

		public async Task AddCountry_NullCountryName()
		{
			//Arrange
			CountryAddRequest request = new CountryAddRequest { CountryName = null };



			//Arrange

		await	Assert.ThrowsAsync<ArgumentException>( async () =>
			{
				//Act 
				 await _countryService.AddCountry(request);

			});



		}

		//When CountryNamr is Duplicated, it should throw argument exception
		[Fact]
		public async Task AddCountry_DuolicatedCountryName()
		{
			//Arrange
			CountryAddRequest request1 = new CountryAddRequest { CountryName = "Egypt" };
			CountryAddRequest request2= new CountryAddRequest { CountryName = "Egypt" };



			//Arrange

		await	Assert.ThrowsAsync<ArgumentException>( async () =>
			{
				//Act 
				await _countryService.AddCountry(request1);
				await _countryService.AddCountry(request2);

			});



		}


		//When all thing is proper it should add the country to the existing list of countries
		[Fact]
		public async Task AddCountry_ProperDetails()
		{
			//Arrange
			CountryAddRequest request = new CountryAddRequest { CountryName = "Japan" };




		
			
				//Act 
			CountryResponse countryResponse =  await	_countryService.AddCountry(request);

			List<CountryResponse> responseList = await _countryService.GetAllCountries() ;

			//Assert

			Assert.True(countryResponse.CountryId != Guid.Empty );
			Assert.Contains(countryResponse, responseList);

           
		}
		#endregion


		#region GetAllCountries

		[Fact]

		public async Task GetAllCountries_EmptyList()
		{

			//Act
			List<CountryResponse> countryResponses = await _countryService.GetAllCountries();

			//Assert
			Assert.Empty(countryResponses);



		}

		[Fact]

		public async Task GetAllCountries_ExpectedEqualActual() {

			List<CountryAddRequest> countryAddRequests = new List<CountryAddRequest>
		{

			new CountryAddRequest{ CountryName = "USA"},
			new CountryAddRequest{ CountryName = "UK"}


		};

			List<CountryResponse> ExpectedCountryResponse = new List<CountryResponse>();

			foreach(var c in countryAddRequests)
			{


				ExpectedCountryResponse.Add(await _countryService.AddCountry(c));


			}
		
		List<CountryResponse> ActualResponses = await _countryService.GetAllCountries();

			foreach (var c in ExpectedCountryResponse) { 
			
			Assert.Contains(c, ActualResponses);
				//this function compares by obj.Equal(obj2) which compare by refernce type not value so we will override it in CountryResponse class
			
			}
		
		
		
		
		}



		#endregion

		#region GetCountryBYID

		[Fact]

		public async Task GetCountryByID_NullID()
		{
			//Arrange
			Guid? id = null;

			//Act
			CountryResponse? response = await _countryService.GetCountryByID(id);


			//Assert
			Assert.Null(response);



		}

		[Fact]

		public async Task GetCountryByID_ValidID() {

			//Arrange
			CountryAddRequest countryAddRequest = new CountryAddRequest { CountryName = "China" };

			CountryResponse ResponseFromAdd = await _countryService.AddCountry(countryAddRequest);

			//Act
			CountryResponse? ResponseFromGet = await _countryService.GetCountryByID(ResponseFromAdd.CountryId);

			//Assert

			Assert.Equal(ResponseFromAdd, ResponseFromGet);

		
		
		
		
		}


		#endregion


	}
}
