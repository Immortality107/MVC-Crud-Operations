using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.PersonDTO
{
    public class PersonUpdateRequest
    {
        public Guid PersonID { get; set; }
        public string? PersonName { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public enGenderOptions? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }

        public string? Email { get; set; }

        public Person ToPerson()
        {
            return new Person() {PersonId= PersonID, PersonName = PersonName, Email = Email, DateOfBirth = DateOfBirth, Gender = Gender.ToString(), Address = Address, CountryID = CountryID, ReceiveNewsLetters = ReceiveNewsLetters };
        }
    }
}
