using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PersonsDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Country> Countries { get; set; }

        public PersonsDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Person>().ToTable("Persons");
            //modelBuilder.Entity<Person>().Property(p => p.PersonId).ValueGeneratedNever();
            //modelBuilder.Entity<Country>().Property(c => c.CountryID).ValueGeneratedNever();

            string? Stcountries = System.IO.File.ReadAllText("Countries.json");
            List<Country>? countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(Stcountries);
            if (countries != null)
            {
                foreach (Country c in countries)
                {
                    modelBuilder.Entity<Country>().HasData(c);
                }
            }
            string? Stpersons = System.IO.File.ReadAllText("Persons.json");
            List<Person>? people = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(Stpersons);
            if (people != null)
            {
                foreach (Person P in people)
                {
                    modelBuilder.Entity<Person>().HasData(P);
                }
            }

            modelBuilder.Entity<Person>().Property(temp => temp.TIN).
                HasColumnName("TaxIdentificationNumber").HasColumnType("varchar(8)").HasDefaultValue("ABDD1234");
        }

        public List<Person> sp_GetAllPersons()
        {
            return Persons.FromSqlRaw("Execute[dbo].[GetAllPersons]").ToList();

        }

        public int sp_InsertPerson(Person person)
        {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@PersonID", person.PersonId),
                new SqlParameter("@PersonName", person.PersonName),
                new SqlParameter("@Email", person.Email),
                new SqlParameter("@Address", person.Address),
                new SqlParameter("@Gender", person.Gender),
                new SqlParameter("@ReceiveNewsLetters", person.ReceiveNewsLetters),
                new SqlParameter("@CountryID", person.CountryID),
                new SqlParameter("@DateOfBirth", person.DateOfBirth)
            };
            return Database.ExecuteSqlRaw("EXECUTE [dbo].[SP_InsertPerson] @PersonID,@PersonName," +
                                      "@Email,@DateOfBirth,@Gender,@CountryID,@Address,@ReceiveNewsLetters", parameters);
        }


    }
}


