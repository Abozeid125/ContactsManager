using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOS
{

	/// <summary>
	/// DTO class that is returned as response to most of person services
	/// </summary>
	public class PersonResponse
	{
		public Guid PersonId { get; set; }

		public string? Name { get; set; }
		public string? Country { get; set; }

		public string? Email { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string? Gender { get; set; }
		public Guid? CountryId { get; set; }
		public string? Address { get; set; }
		public bool ReceiveNewsLetters { get; set; }

		public double? Age { get; set; }

		/// <summary>
		/// Compares current object data with parameter object
		/// </summary>
		/// <param name="obj"></param>
		/// <returns>True Or False , Depending if all person details are matched or not </returns>
		public override bool Equals(object? obj)
		{
			if (obj == null) return false;
			if (obj.GetType() != typeof(PersonResponse)) return false;

			PersonResponse person = (PersonResponse)obj;

			return PersonId == person.PersonId && Name == person.Name && Email == person.Email && DateOfBirth == person.DateOfBirth &&
				Gender == person.Gender && CountryId == person.CountryId && Address == person.Address && ReceiveNewsLetters == person.ReceiveNewsLetters;

			



		}

		public PersonUpdateRequest ToPersonUpdateRequest ()
		{


			return new PersonUpdateRequest
			{
				Id = PersonId, Name = Name, Email = Email, DateOfBirth = DateOfBirth
				, Address = Address, ReceiveNewsLetters = ReceiveNewsLetters,
				Gender =  (GenderOptions)Enum.Parse(typeof(GenderOptions), Gender,true),
				CountryId = CountryId,



			};

		}


		


		



	}

	public static class PersonExtensions
	{
		/// <summary>
		/// Convert an object of person class to personResponse clas
		/// </summary>
		/// <param name="country"></param>
		/// <returns>Person Response class</returns>
		public static PersonResponse ToPersonResponse(this Person person)
		{

			return new PersonResponse { 
			PersonId = person.PersonId,
			Name = person.Name,
			Email = person.Email,
			DateOfBirth = person.DateOfBirth,
			Gender = person.Gender,
			CountryId = person.CountryId,
			Address = person.Address,
			ReceiveNewsLetters = person.ReceiveNewsLetters,
			Age = (person.DateOfBirth != null)? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays/365.25 ): null ,
			Country = person.Country?.CountryName
			
			};



		}



		

	}
}
