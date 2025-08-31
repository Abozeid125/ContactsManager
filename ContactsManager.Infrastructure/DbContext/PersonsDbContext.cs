using ContactsManager.Core.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PersonsDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {
        public PersonsDbContext(DbContextOptions<PersonsDbContext> options) : base(options) { } 

        public DbSet<Person> Persons { get; set; }

        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Person>().ToTable("Persons");


			////sead country data
			//string countryJson = System.IO.File.ReadAllText("Countries.json");

			//List<Country>? countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(countryJson);
			//foreach(var country in countries)
			//{
			//    modelBuilder.Entity<Country>().HasData(country);
			//}


			////sead person data
			//string personJson = System.IO.File.ReadAllText("Persons.json");

			//List<Person>? persons = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(countryJson);
			//foreach (var person in persons)
			//{
			//    modelBuilder.Entity<Person>().HasData(person);
			//}

			modelBuilder.Entity<IdentityUserLogin<Guid>>(entity =>
			{
				entity.HasKey(l => new { l.LoginProvider, l.ProviderKey });
			});

			modelBuilder.Entity<IdentityUserRole<Guid>>(entity =>
			{
				entity.HasKey(r => new { r.UserId, r.RoleId });
			});

			modelBuilder.Entity<IdentityUserToken<Guid>>(entity =>
			{
				entity.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
			});

			modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Guid = Guid.Parse("14629847-905a-4a0e-9abe-80b61655c5cb"),
                    CountryName = "Philippines"
                },
                new Country
                {
                    Guid = Guid.Parse("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                    CountryName = "Thailand"
                },
                new Country
                {
                    Guid = Guid.Parse("12e15727-d369-49a9-8b13-bc22e9362179"),
                    CountryName = "China"
                },
                new Country
                {
                    Guid = Guid.Parse("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                    CountryName = "Palestinian Territory"
                },
                new Country
                {
                    Guid = Guid.Parse("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                    CountryName = "China"
                }
            );





            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    PersonId = Guid.Parse("c03bbe45-9aeb-4d24-99e0-4743016ffce9"),
                    Name = "Marguerite",
                    Email = "mwebsdale0@people.com.cn",
                    DateOfBirth = new DateTime(1989, 8, 28),
                    Gender = "Female",
                    CountryId = Guid.Parse("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                    Address = "4 Parkside Point",
                    ReceiveNewsLetters = false
                },
                new Person
                {
                    PersonId = Guid.Parse("c3abddbd-cf50-41d2-b6c4-cc7d5a750928"),
                    Name = "Ursa",
                    Email = "ushears1@globo.com",
                    DateOfBirth = new DateTime(1990, 10, 5),
                    Gender = "Female",
                    CountryId = Guid.Parse("14629847-905a-4a0e-9abe-80b61655c5cb"),
                    Address = "6 Morningstar Circle",
                    ReceiveNewsLetters = false
                },
                new Person
                {
                    PersonId = Guid.Parse("c6d50a47-f7e6-4482-8be0-4ddfc057fa6e"),
                    Name = "Franchot",
                    Email = "fbowsher2@howstuffworks.com",
                    DateOfBirth = new DateTime(1995, 2, 10),
                    Gender = "Male",
                    CountryId = Guid.Parse("14629847-905a-4a0e-9abe-80b61655c5cb"),
                    Address = "73 Heath Avenue",
                    ReceiveNewsLetters = true
                },
                new Person
                {
                    PersonId = Guid.Parse("d15c6d9f-70b4-48c5-afd3-e71261f1a9be"),
                    Name = "Angie",
                    Email = "asarvar3@dropbox.com",
                    DateOfBirth = new DateTime(1987, 1, 9),
                    Gender = "Male",
                    CountryId = Guid.Parse("12e15727-d369-49a9-8b13-bc22e9362179"),
                    Address = "83187 Merry Drive",
                    ReceiveNewsLetters = true
                },
                new Person
                {
                    PersonId = Guid.Parse("89e5f445-d89f-4e12-94e0-5ad5b235d704"),
                    Name = "Tani",
                    Email = "ttregona4@stumbleupon.com",
                    DateOfBirth = new DateTime(1995, 2, 11),
                    Gender = "Gender",
                    CountryId = Guid.Parse("56bf46a4-02b8-4693-a0f5-0a95e2218bdc"),
                    Address = "50467 Holy Cross Crossing",
                    ReceiveNewsLetters = false
                },
                new Person
                {
                    PersonId = Guid.Parse("2a6d3738-9def-43ac-9279-0310edc7ceca"),
                    Name = "Mitchael",
                    Email = "mlingfoot5@netvibes.com",
                    DateOfBirth = new DateTime(1988, 1, 4),
                    Gender = "Male",
                    CountryId = Guid.Parse("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                    Address = "97570 Raven Circle",
                    ReceiveNewsLetters = false
                },
                new Person
                {
                    PersonId = Guid.Parse("29339209-63f5-492f-8459-754943c74abf"),
                    Name = "Maddy",
                    Email = "mjarrell6@wisc.edu",
                    DateOfBirth = new DateTime(1983, 2, 16),
                    Gender = "Male",
                    CountryId = Guid.Parse("12e15727-d369-49a9-8b13-bc22e9362179"),
                    Address = "57449 Brown Way",
                    ReceiveNewsLetters = true
                },
                new Person
                {
                    PersonId = Guid.Parse("ac660a73-b0b7-4340-abc1-a914257a6189"),
                    Name = "Pegeen",
                    Email = "pretchford7@virginia.edu",
                    DateOfBirth = new DateTime(1998, 12, 2),
                    Gender = "Female",
                    CountryId = Guid.Parse("12e15727-d369-49a9-8b13-bc22e9362179"),
                    Address = "4 Stuart Drive",
                    ReceiveNewsLetters = true
                },
                new Person
                {
                    PersonId = Guid.Parse("012107df-862f-4f16-ba94-e5c16886f005"),
                    Name = "Hansiain",
                    Email = "hmosco8@tripod.com",
                    DateOfBirth = new DateTime(1990, 9, 20),
                    Gender = "Male",
                    CountryId = Guid.Parse("12e15727-d369-49a9-8b13-bc22e9362179"),
                    Address = "413 Sachtjen Way",
                    ReceiveNewsLetters = true
                },
                new Person
                {
                    PersonId = Guid.Parse("cb035f22-e7cf-4907-bd07-91cfee5240f3"),
                    Name = "Lombard",
                    Email = "lwoodwing9@wix.com",
                    DateOfBirth = new DateTime(1997, 9, 25),
                    Gender = "Male",
                    CountryId = Guid.Parse("8f30bedc-47dd-4286-8950-73d8a68e5d41"),
                    Address = "484 Clarendon Court",
                    ReceiveNewsLetters = false
                },
                new Person
                {
                    PersonId = Guid.Parse("28d11936-9466-4a4b-b9c5-2f0a8e0cbde9"),
                    Name = "Minta",
                    Email = "mconachya@va.gov",
                    DateOfBirth = new DateTime(1990, 5, 24),
                    Gender = "Female",
                    CountryId = Guid.Parse("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                    Address = "2 Warrior Avenue",
                    ReceiveNewsLetters = true
                },
                new Person
                {
                    PersonId = Guid.Parse("a3b9833b-8a4d-43e9-8690-61e08df81a9a"),
                    Name = "Verene",
                    Email = "vklussb@nationalgeographic.com",
                    DateOfBirth = new DateTime(1987, 1, 19),
                    Gender = "Female",
                    CountryId = Guid.Parse("501c6d33-1bbe-45f1-8fbd-2275913c6218"),
                    Address = "9334 Fremont Street",
                    ReceiveNewsLetters = true
                }
            );

            //Fluent Api

            modelBuilder.Entity<Person>().Property(temp => temp.TIN)
                .HasColumnName("TaxIdentificationNumber")
                .HasColumnType("varchar(8)")
                .HasDefaultValue("abc12345");

         


            //Table Relations

            //modelBuilder.Entity<Person>(
            //    entity => entity.HasOne<Country>(c => c.Country)
            //    .WithMany(p => p.Persons)
            //    .HasForeignKey(p => p.CountryId));


    }


    }
}
