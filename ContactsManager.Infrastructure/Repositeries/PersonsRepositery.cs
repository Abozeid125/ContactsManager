using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using RepositeryContracts;

namespace Repositeries
{
	public class PersonsRepositery : IPersonsRepositery
	{
		private readonly PersonsDbContext _context;

		public PersonsRepositery(PersonsDbContext personsDbContext)
		{
			_context = personsDbContext;
		}
		public async Task<Person> AddPerson(Person person)
		{
			_context.Persons.Add(person);
		await _context.SaveChangesAsync();
			return person;
			
		}

		public async Task<bool> DeletePerson(Guid id)
		{
			_context.Persons.RemoveRange(_context.Persons.Where(p => p.PersonId == id).ToList());

			await _context.SaveChangesAsync();
			return true;

		}

		public async Task<List<Person>> GetAllPersons()
		{
		return await _context.Persons.Include(p => p.Country).ToListAsync();
		}

		public async Task<List<Person>> GetFilteredPersons(Expression<Func<Person, bool>> predicate)
		{
			return await _context.Persons.Where(predicate).Include( p=> p.Country).ToListAsync();
		}

		public  async Task<Person?> GetPersonById(Guid id)
		{

			return  await _context.Persons.Include(p => p.Country).FirstOrDefaultAsync( p => p.PersonId == id);
		}

		public async  Task<Person?> UpdatePerson(Person person)
		{
		 Person? matchedPerson = await _context.Persons.Include( p => p.Country).FirstOrDefaultAsync( p => p.PersonId == person.PersonId);

			if (matchedPerson != null) { 
			
			matchedPerson.PersonId = person.PersonId;
			matchedPerson.Gender = person.Gender;
			matchedPerson.Address = person.Address;
			matchedPerson.Name= person.Name;
			matchedPerson.Country = person.Country;
			matchedPerson.CountryId = person.CountryId;
			matchedPerson.DateOfBirth = person.DateOfBirth;
			matchedPerson.Email = person.Email;
			matchedPerson.ReceiveNewsLetters = person.ReceiveNewsLetters;
				await _context.SaveChangesAsync();

			return matchedPerson;
			
			} return person;
		}
	}
}
