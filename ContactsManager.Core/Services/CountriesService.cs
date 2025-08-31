using Entities;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using RepositeryContracts;
using ServiceContracts;
using ServiceContracts.DTOS;

namespace Services
{
	public class CountriesService : ICountryService
	{

	
		private readonly ICountriesRepositery _countriesRepositery;

		public CountriesService( ICountriesRepositery countriesRepositery)
		{

			

            
			
			_countriesRepositery = countriesRepositery;
		}
		public async Task<CountryResponse> AddCountry(CountryAddRequest request)
		 
		{

			if(request == null) throw new ArgumentNullException(nameof(request));

			if(request.CountryName == null)
			{
				throw new ArgumentException(nameof(request.CountryName));
			}

			if ( await _countriesRepositery.GetCountryByName(request.CountryName) != null) { 
			
			throw new ArgumentException("This country name is already existed");
			}

			Country country = request.ToCountry();

			country.Guid = Guid.NewGuid();
			await _countriesRepositery.AddcCountry(country);
			return country.ToCountryResponse();

		
		}

		public async Task<List<CountryResponse>> GetAllCountries()
		{
			List<Country> countries = await _countriesRepositery.GetAllCountries();

			return countries.Select( c => c.ToCountryResponse()).ToList();
		}

		public async Task<CountryResponse?> GetCountryByID(Guid? id)
		{
			if(id == null) return null;

			Country? country = await _countriesRepositery.GetCountryById(id.Value);

			if(country == null) return null;

			return country.ToCountryResponse();





		}

		public async Task<int> UplaodCountriesFromExcel(IFormFile formFile)
		{

			MemoryStream stream = new MemoryStream();

			await formFile.CopyToAsync(stream);

			int addedCountries = 0;

			using (ExcelPackage excelPackage = new ExcelPackage(stream))
			{

				ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets["Countries"];

				int rowCount = excelWorksheet.Dimension.Rows;
				

				for (int i = 2; i <= rowCount; i++)
				{

					string? countryNmae = excelWorksheet.Cells[i,1].Value.ToString();

					if(_countriesRepositery.GetCountryByName(countryNmae)== null)
					{

						Country country = new Country
						{


							CountryName = countryNmae,
						};
					await	_countriesRepositery.AddcCountry(country);
						addedCountries++;
						
					}





				}
				







			}



			return addedCountries;
		}


    }
}
