using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositeryContracts
{
	public interface IPersonsRepositery
	{

		Task<Person> AddPerson(Person person);

		Task<List<Person>> GetAllPersons();

		Task<Person?> GetPersonById(Guid id);

		Task<List <Person>> GetFilteredPersons( Expression<Func<Person, bool>> predicate );

		Task<Person?> UpdatePerson( Person person );
		Task<bool> DeletePerson( Guid id);








	}
}
