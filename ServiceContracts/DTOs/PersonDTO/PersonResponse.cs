using Entities;
using ServiceContracts.DTOs.PersonDTO;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.PersonDTO
{
    public class PersonResponse
    {
        public Guid PersonId { get; set; }
        public string? PersonName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }
        public string? Country { get; set; }
        public string? Email { get; set; }
        public double? Age { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(PersonResponse)) return false;

            PersonResponse person = (PersonResponse)obj;
            return PersonId == person.PersonId && PersonName == person.PersonName && 
                Email == person.Email && DateOfBirth == person.DateOfBirth && 
                Gender == person.Gender && CountryID == person.CountryID && 
                Address == person.Address && ReceiveNewsLetters == person.ReceiveNewsLetters;
        }

        public override int GetHashCode()
        {
           return base.GetHashCode();
        }
   

    public override string ToString() {

            return $"Person ID: {PersonId}, Person Name: {PersonName}, Email: {Email}, Date of Birth: {DateOfBirth?.ToString("dd-MMM-yyyy")}, Gender: {Gender}, Country ID: {CountryID}, Country: {Country}, Address: {Address}, Receive News Letters: {ReceiveNewsLetters}";

        }

        public PersonUpdateRequest ToPersonUpdateRequest()
        {
            return new PersonUpdateRequest()
            {
                PersonID = PersonId,
                PersonName = PersonName,
                Address = Address,
                DateOfBirth = DateOfBirth,
                CountryID = CountryID,
                Gender = (enGenderOptions) Enum.Parse(typeof(enGenderOptions), Gender, true),
                ReceiveNewsLetters = ReceiveNewsLetters, Email = Email
            };
        }
    }
    public static class PersonExtensions {

        public static PersonResponse ToPersonResponse(this Person person)
        {
            return new PersonResponse() { PersonId = person.PersonId, PersonName = person.PersonName,
                DateOfBirth = person.DateOfBirth, Gender = person.Gender, CountryID = person.CountryID,
                Address = person.Address,Email=person.Email, ReceiveNewsLetters = person.ReceiveNewsLetters,
                Age = (person.DateOfBirth != null)   ?
                Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25) : null };
        } }
}
