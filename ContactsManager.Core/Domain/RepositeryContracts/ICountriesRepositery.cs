using Entities;

namespace RepositeryContracts
{
	public interface ICountriesRepositery
	{

		Task<Country> AddcCountry(Country country);
		Task<List<Country>> GetAllCountries();


		Task<Country?> GetCountryById(Guid id);

		Task<Country?> GetCountryByName(string name);




	}
}
