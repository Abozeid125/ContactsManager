using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOS
{
	public class PersonAddRequest
	{


		

		[Required (ErrorMessage = "Name Can't be blank")]
		public string? Name { get; set; }

		[Required(ErrorMessage = "Eamil Can't be blank")]
		[EmailAddress (ErrorMessage = "It should be a valid Email address")]
		[DataType(DataType.EmailAddress)]
		public string? Email { get; set; }

		[DataType(DataType.Date)]
		public DateTime? DateOfBirth { get; set; }

		
		[Required(ErrorMessage = "Please select a gender for the added person")]
		public GenderOptions? Gender { get; set; }


		[Required(ErrorMessage = "Please select a country")]
		public Guid? CountryId { get; set; }
		public string? Address { get; set; }
		public bool ReceiveNewsLetters { get; set; }

		/// <summary>
		/// Convert object of "PersonAddRequest" type to "Person" type
		/// </summary>
		/// <returns></returns>
		public Person ToPerson()
		{

			return new Person
			{
				Name = Name,
				Email = Email,
				DateOfBirth = DateOfBirth,
				Gender = Gender.ToString(),
				CountryId = CountryId,
				Address = Address,
				ReceiveNewsLetters = ReceiveNewsLetters


			};
		

		}
	}
}
