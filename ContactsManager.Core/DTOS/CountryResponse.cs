using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace ServiceContracts.DTOS
{
	public class CountryResponse
	{
		public Guid CountryId { get; set; }
		public string? CountryName { get; set; }

		public override bool Equals(object? obj)
		{
			if(obj == null) return false;
            if(obj.GetType() != typeof(CountryResponse)) return false; 

            return this.CountryId == ((CountryResponse)obj).CountryId && 
				this.CountryName == ((CountryResponse)obj).CountryName;
				
                
           
        }

	}

	public static class CountryExtensions
	{
		public static CountryResponse ToCountryResponse(this  Country country)
		{
			return new CountryResponse { CountryId = country.Guid, CountryName = country.CountryName };


		}



	}
}
