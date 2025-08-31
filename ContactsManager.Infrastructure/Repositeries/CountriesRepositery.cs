using Entities;
using Microsoft.EntityFrameworkCore;
using RepositeryContracts;

namespace Repositeries
{
	public class CountriesRepositery : ICountriesRepositery
	{
		private readonly PersonsDbContext _context;

		public CountriesRepositery(PersonsDbContext personsDbContext)
		{
			_context = personsDbContext;
		}
		public async Task<Country> AddcCountry(Country country)
		{
			_context.Countries.Add(country);
			await _context.SaveChangesAsync();
			return country;
			
		}

		public async Task<List<Country>> GetAllCountries()
		{
			return await _context.Countries.ToListAsync();
		}

		public async Task<Country?> GetCountryById(Guid id)
		{
			return await _context.Countries.FirstOrDefaultAsync(c => c.Guid == id);

		}

		public async Task<Country?> GetCountryByName(string name)
		{
			return await _context.Countries.FirstOrDefaultAsync(c => c.CountryName == name);

		}
	}
}
