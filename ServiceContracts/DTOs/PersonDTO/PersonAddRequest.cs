using Entities;
using ServiceContracts.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ServiceContracts.DTOs.PersonDTO
{
    public class PersonAddRequest
    {
        [Required(ErrorMessage = "Person Name can't be blank")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "Email can't be blank")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email value should be a valid email")]
        public string? Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [Required(ErrorMessage = "Gender can't be blank")]

        public enGenderOptions? Gender { get; set; }
        [Required(ErrorMessage = "Country Can Not Be Empty")]

        public Guid? CountryID { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }


        public Person ToPerson()
        {
            return new Person() { PersonName = PersonName, Email = Email, DateOfBirth = DateOfBirth, Gender = Gender.ToString(), Address = Address, CountryID = CountryID, ReceiveNewsLetters = ReceiveNewsLetters };
        }
    }
    }

