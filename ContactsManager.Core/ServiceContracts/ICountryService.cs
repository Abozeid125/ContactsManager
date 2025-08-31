using Microsoft.AspNetCore.Http;
using ServiceContracts.DTOS;


namespace ServiceContracts
{
	public interface ICountryService
	{

		Task< CountryResponse> AddCountry (CountryAddRequest request);
		public Task< List<CountryResponse>> GetAllCountries();


		/// <summary>
		/// return an object with th is taken as patameter
		/// </summary>
		/// <param name="id"></param>
		/// <returns> a CountryResponse object </returns>
		Task< CountryResponse?> GetCountryByID(Guid? id);
		Task<int> UplaodCountriesFromExcel(IFormFile formFile);



	}
}
