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

	/// <summary>
	/// DRO contains person details to update it
	/// </summary>
	public class PersonUpdateRequest
	{


		[Required (ErrorMessage = "PersonId Can't be blank")]
         public Guid Id { get; set; }


		[Required(ErrorMessage = "Name Can't be blank")]
		public string? Name { get; set; }

		[Required(ErrorMessage = "Eamil Can't be blank")]
		[EmailAddress(ErrorMessage = "It should be a valid Email address")]

		public string? Email { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public GenderOptions? Gender { get; set; }
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
				PersonId = Id,
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
