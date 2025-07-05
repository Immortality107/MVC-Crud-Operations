using ServiceContracts;
using ServiceContracts.DTOs.PersonDTO;
using System;
using Xunit;
using Services;
using ServiceContracts.Enums;
using Xunit.Abstractions;
using Entities;
using ServiceContracts.DTOs.CountryDTO;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace CrudTests
{
    public class PersonsServiceTest
    {
        private readonly IPersonsService _personsService;
        private readonly ITestOutputHelper _outputHelper;
        private readonly ICountries _countries;
        public PersonsServiceTest(ITestOutputHelper testOutputHelper)
        {
            _countries = new CountriesService(new PersonsDbContext(new DbContextOptionsBuilder<PersonsDbContext>().Options));
            _personsService = new PersonsService(new PersonsDbContext(new DbContextOptionsBuilder<PersonsDbContext>().Options), _countries);
            _outputHelper = testOutputHelper;

        }

        #region "AddPerson"
        [Fact]
        public void AddPerson_NullPerson()
        {
            //Arrange
            PersonAddRequest? personAddRequest = null;

            //Act
            Assert.Throws<ArgumentNullException>(() =>
            {
                _personsService.AddPerson(personAddRequest);
            }
            );
        }

        [Fact]
        public void AddPerson_PersonNameIsNull()
        {
            //Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest() { PersonName = null };

            //Act
            Assert.Throws<ArgumentException>(() =>
            {
                _personsService.AddPerson(personAddRequest);
            }
            );
        }

        [Fact]
        public async Task AddPerson_ProperPerson()
        {
            //Arrange
            PersonAddRequest? personAddRequest = new PersonAddRequest()
            {
                PersonName = "Test",
                CountryID = Guid.NewGuid(),
                Gender = enGenderOptions.Female,
                Address = "TestAddress",
                DateOfBirth = DateTime.MinValue,
                Email = "TestEmail",
                ReceiveNewsLetters = true
            };

            //Act
            PersonResponse? response =  _personsService.AddPerson(personAddRequest)?.Result;

            List<PersonResponse> ActualAllPersons = _personsService.GetAllPersons().Result;
            //Assert
            Assert.Contains(response, ActualAllPersons);



        }

        #endregion

        #region "GetAllPersons"

        [Fact]
        public void GetAllPersons_EmptyList()
        {
            //Act
            List<PersonResponse> personResponse = _personsService.GetAllPersons().Result;
            //Assert
            Assert.Empty(personResponse);
        }

        [Fact]
        public void GetAllPersons_ProperList()
        {
            //Arrange
            PersonAddRequest? personAddRequest1 = new PersonAddRequest()
            {
                PersonName = "Test",
                CountryID = Guid.NewGuid(),
                Gender = enGenderOptions.Female,
                Address = "TestAddress",
                DateOfBirth = DateTime.MinValue,
                Email = "TestEmail",
                ReceiveNewsLetters = true
            };
            PersonAddRequest? personAddRequest2 = new PersonAddRequest()
            {
                PersonName = "Test",
                CountryID = Guid.NewGuid(),
                Gender = enGenderOptions.Female,
                Address = "TestAddress",
                DateOfBirth = DateTime.MinValue,
                Email = "TestEmail",
                ReceiveNewsLetters = true
            };

            //Act

            PersonResponse response = _personsService.AddPerson(personAddRequest1).Result;
            PersonResponse response2 = _personsService.AddPerson(personAddRequest2).Result;

            List<PersonResponse> Persons = new List<PersonResponse> { response, response2 };
            List<PersonResponse> ExpectedPersons = _personsService.GetAllPersons().Result;
            _outputHelper.WriteLine("Actual:");
            foreach (PersonResponse p in Persons)
            {
                _outputHelper.WriteLine(p.ToString());
            }

            _outputHelper.WriteLine("Expected:");
            foreach (PersonResponse p in ExpectedPersons)
            {
                _outputHelper.WriteLine(p.ToString());


            }
            //Assert
            foreach (PersonResponse personResponse in Persons)
            {

                Assert.Contains(personResponse, ExpectedPersons);

            }

        }
        #endregion

        #region "GetPersonByPersonID"

        [Fact]
        public void GetPersonByPersonID_NullPersonID()
        {
            //Arrange

            Guid? PersonID = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                _personsService.GetPersonByPersonID(PersonID);
            }
            );
        }

        [Fact]
        public void GetPersonByPersonID_NotFoundPerson()
        {
            //Arrange
            Guid? PersonID = Guid.NewGuid();

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                _personsService.GetPersonByPersonID(PersonID);
            });
        }
        [Fact]
        public void GetPersonByPersonID_PersonExists()
        {
            //Arrange
            PersonAddRequest personAddRequest = new PersonAddRequest() { PersonName = "K" };
            PersonResponse? response1 = _personsService.AddPerson(personAddRequest)?.Result;
            Guid? PersonID = response1?.PersonId;

            //Act
            PersonResponse? response = _personsService.GetPersonByPersonID(PersonID)?.Result;
            //Assert
            Assert.Equal(response?.PersonId, PersonID);
        }
        #endregion

        #region "FilterPersons"

        [Fact]
        public void FilterPersons_EmptyList()
        {
            //Arrange
            PersonAddRequest? personAddRequest1 = new PersonAddRequest()
            {
                PersonName = "Test",
                CountryID = Guid.NewGuid(),
                Gender = enGenderOptions.Female,
                Address = "TestAddress",
                DateOfBirth = DateTime.MinValue,
                Email = "TestEmail",
                ReceiveNewsLetters = true
            };
            PersonAddRequest? personAddRequest2 = new PersonAddRequest()
            {
                PersonName = "Test",
                CountryID = Guid.NewGuid(),
                Gender = enGenderOptions.Female,
                Address = "TestAddress",
                DateOfBirth = DateTime.MinValue,
                Email = "TestEmail",
                ReceiveNewsLetters = true
            };

            //Act

            PersonResponse response = _personsService.AddPerson(personAddRequest1).Result;
            PersonResponse response2 = _personsService.AddPerson(personAddRequest2).Result;

            List<PersonResponse> Persons = new List<PersonResponse> { response, response2 };
            List<PersonResponse> ExpectedPersons = _personsService.GetAllPersons().Result;


            List<PersonResponse>? responses = _personsService.FilterPersons(nameof(response.PersonName), "").Result;

            //print
            _outputHelper.WriteLine("Actual:");
            foreach (PersonResponse p in Persons)
            {
                _outputHelper.WriteLine(p.ToString());
            }

            _outputHelper.WriteLine("Expected:");
            foreach (PersonResponse p in ExpectedPersons)
            {
                _outputHelper.WriteLine(p.ToString());


            }

            //Assert
            foreach (PersonResponse personResponse in Persons)
            {

                Assert.Contains(personResponse, responses);

            }

        }
        [Fact]

        public void FilterPersons_SearchValues()
        {
            CountryAddRequest countryAddRequest1 = new CountryAddRequest { CountryName = "Egypt" };
            CountryAddRequest countryAddRequest2 = new CountryAddRequest { CountryName = "KSA" };
                        
            CountryResponse countryResponse = _countries.AddCountry(countryAddRequest1).Result;
            CountryResponse countryResponse2 = _countries.AddCountry(countryAddRequest2).Result;
            //Arrange
            PersonAddRequest? personAddRequest1 = new PersonAddRequest()
            {

                PersonName = "Test",
                CountryID = countryResponse.CountryID,
                Gender = enGenderOptions.Female,
                Address = "TestAddress",
                DateOfBirth = DateTime.MinValue,
                Email = "TestEmail",
                ReceiveNewsLetters = true
            };
            PersonAddRequest? personAddRequest2 = new PersonAddRequest()
            {
                PersonName = "Test",
                CountryID = countryResponse2.CountryID,
                Gender = enGenderOptions.Female,
                Address = "TestAddress",
                DateOfBirth = DateTime.MinValue,
                Email = "TestEmail",
                ReceiveNewsLetters = true
            };

            //Act

            PersonResponse response = _personsService.AddPerson(personAddRequest1).Result;
            PersonResponse response2 = _personsService.AddPerson(personAddRequest2).Result;

            List<PersonResponse> Persons = new List<PersonResponse> { response, response2 };
            List<PersonResponse> ExpectedPersons = _personsService.GetAllPersons().Result;
            List<PersonResponse> personFilteredResponses = _personsService.FilterPersons(nameof(response2.PersonName), "Me").Result;
            _outputHelper.WriteLine("Actual:");
            foreach (PersonResponse p in Persons)
            {
                _outputHelper.WriteLine(p.ToString());
            }

            _outputHelper.WriteLine("Expected:");
            foreach (PersonResponse p in ExpectedPersons)
            {
                _outputHelper.WriteLine(p.ToString());


            }
            //Assert
            foreach (PersonResponse personResponse in Persons)
            {
                if (personResponse.PersonName != null)
                {
                    if (personResponse.PersonName.Contains("Me", StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(personResponse, personFilteredResponses);
                    }
                }

            }
        }
    

#endregion

        #region "Sort_Persons"
        [Fact]
        public void SortPersons()
        {

            CountryAddRequest countryAddRequest1 = new CountryAddRequest { CountryName = "Egypt" };
            CountryAddRequest countryAddRequest2 = new CountryAddRequest { CountryName = "KSA" };

            CountryResponse countryResponse = _countries.AddCountry(countryAddRequest1).Result;
            CountryResponse countryResponse2 = _countries.AddCountry(countryAddRequest2).Result;
            //Arrange
            PersonAddRequest? personAddRequest1 = new PersonAddRequest()
            {
                PersonName = "Test1",
                CountryID = countryResponse.CountryID,
                Gender = enGenderOptions.Female,
                Address = "TestAddress",
                DateOfBirth = DateTime.MinValue,
                Email = "TestEmail",
                ReceiveNewsLetters = true
            };
            PersonAddRequest? personAddRequest2 = new PersonAddRequest()
            {
                PersonName = "Test2",
                CountryID = countryResponse2.CountryID,
                Gender = enGenderOptions.Female,
                Address = "TestAddress",
                DateOfBirth = DateTime.MinValue,
                Email = "TestEmail",
                ReceiveNewsLetters = true
            };

            //Act
            List <PersonAddRequest> PersonsToAdd = new List<PersonAddRequest> { personAddRequest1, personAddRequest2 };
            List < PersonResponse > Actualpeople = new List<PersonResponse> ();
            foreach (PersonAddRequest personAddRequest in PersonsToAdd)
            {
              PersonResponse response =  _personsService.AddPerson(personAddRequest).Result;
              Actualpeople.Add (response);
            }
                    
            List<PersonResponse> ExpectedPersons = _personsService.GetAllPersons().Result;
            List<PersonResponse> ActualpersonSortedResponses = Actualpeople.OrderByDescending(temp => temp.PersonName).ToList();
            List<PersonResponse> ExpectedpersonSortedResponses = ExpectedPersons;
            ExpectedpersonSortedResponses = _personsService.SortPersons(ExpectedpersonSortedResponses, nameof(Person.PersonName), enSortOrder.Desc).Result;
            _outputHelper.WriteLine("Actual:");
            foreach (PersonResponse p in Actualpeople)
            {
                _outputHelper.WriteLine(p.ToString());
            }

            _outputHelper.WriteLine("Expected:");
            foreach (PersonResponse p in ExpectedpersonSortedResponses)
            {
                _outputHelper.WriteLine(p.ToString());


            }
            //Assert
            foreach (PersonResponse personResponse in ActualpersonSortedResponses)
            {

                Assert.Equal(ExpectedpersonSortedResponses, ActualpersonSortedResponses);
            }


        }
        #endregion

        #region "UpdatePerson"

        [Fact]
        public void UpdatePerson_NullPerson()
        {
            //Arrange
            PersonUpdateRequest personUpdateRequest = null;

            //Act
            //Assert

            Assert.Throws<ArgumentNullException>(() =>
            {
                _personsService.UpdatePerson(personUpdateRequest);
            
            });

        }

        [Fact]
        public void UpdatePerson_InvalidPersonID()
        {
            //Arrange

            PersonUpdateRequest personUpdateRequest = new PersonUpdateRequest() { PersonID = Guid.NewGuid()};

            //Act
            //Assert

            Assert.Throws<ArgumentException>(() =>
            {
                _personsService.UpdatePerson(personUpdateRequest);

            });

        }

        [Fact]
        public void UpdatePerson_PersonNameIsNull()
        {
            //Arrange
            CountryAddRequest country = new CountryAddRequest() { CountryName = "France"};
            CountryResponse countryResponse= _countries.AddCountry(country).Result;

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "",
                Address = "addressTest",
                Email = "@",
                Gender = enGenderOptions.Female,
                DateOfBirth = DateTime.MinValue
            };
            PersonResponse personResponse = _personsService.AddPerson(personAddRequest).Result;
            //Act


            PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();

            //Assert

            Assert.Throws<ArgumentException>(() =>
            {
                _personsService.UpdatePerson(personUpdateRequest);

            });

        }
        [Fact]
        public void UpdatePerson_EmailIsNull()
        {
            //Arrange
            CountryAddRequest country = new CountryAddRequest() { CountryName = "France" };
            CountryResponse countryResponse = _countries.AddCountry(country).Result;

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "Khaloda",
                Address = "addressTest",
                Email = null,
                Gender = enGenderOptions.Female,
                DateOfBirth = DateTime.MinValue
            };
            PersonResponse personResponse = _personsService.AddPerson(personAddRequest).Result;

            PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();
            //Act
            //Assert

            Assert.Throws<ArgumentException>(() =>
            {
                _personsService.UpdatePerson(personUpdateRequest);

            });

        }
        [Fact]
        public void UpdatePerson_FullValidInfo()
        {
            //Arrange
            CountryAddRequest country = new CountryAddRequest() { CountryName = "France" };
            CountryResponse countryResponse = _countries.AddCountry(country).Result;

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "Khaloda",
                Address = "addressTest",
                Email = "Test@Test.com",
                CountryID= countryResponse.CountryID,
                Gender = enGenderOptions.Female,
                DateOfBirth = DateTime.MinValue
            };
            PersonResponse personResponse = _personsService.AddPerson(personAddRequest).Result;

            PersonUpdateRequest personUpdateRequest_FromAdd = personResponse.ToPersonUpdateRequest();
            
            //Act         
            PersonResponse person_Response_From_Update = _personsService.UpdatePerson(personUpdateRequest_FromAdd).Result;
            PersonResponse person_Response_From_Get = _personsService.GetPersonByPersonID(personResponse.PersonId).Result;
            //Assert

            Assert.Equal(person_Response_From_Get,person_Response_From_Update);

        }
        #endregion

        #region
        [Fact]
        public void DeletePerson_PersonIDInValid()
        {
  
            //Arrange
            CountryAddRequest country = new CountryAddRequest() { CountryName = "France" };
            CountryResponse countryResponse = _countries.AddCountry(country).Result;

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "",
                Address = "addressTest",
                Email = "@",
                Gender = enGenderOptions.Female,
                DateOfBirth = DateTime.MinValue
            };
            PersonResponse personResponse = _personsService.AddPerson(personAddRequest).Result;
            //Act

            PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();
            personUpdateRequest.PersonID= Guid.NewGuid();
            //Assert

            Assert.False(_personsService.DeletePerson(personUpdateRequest.PersonID).Result);
        }

        [Fact]
        public void DeletePerson_PersonIDValid()
        {

            //Arrange
            CountryAddRequest country = new CountryAddRequest() { CountryName = "France" };
            CountryResponse countryResponse = _countries.AddCountry(country).Result;

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "",
                Address = "addressTest",
                Email = "@",
                Gender = enGenderOptions.Female,
                DateOfBirth = DateTime.MinValue
            };
            PersonResponse? personResponse = _personsService.AddPerson(personAddRequest).Result;
            //Act


            PersonUpdateRequest? personUpdateRequest = personResponse.ToPersonUpdateRequest();
            bool Deleted = _personsService.DeletePerson(personUpdateRequest.PersonID).Result;

            //Assert
            Assert.True(Deleted);

        }

        #endregion
    }
}