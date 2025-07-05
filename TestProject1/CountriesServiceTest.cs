using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using Entities;
using ServiceContracts.DTOs.CountryDTO;
namespace CrudTests
{
    public class CountriesServiceTest
    {
        PersonsDbContext db {  get; set; }
        private readonly CountriesService _Countries;
        public CountriesServiceTest() {
            _Countries = new CountriesService(db);
        }
        #region "AddCountry"
        [Fact]

        public void AddCountry_NullCountry()
        {
            //arrange 
            CountryAddRequest addRequest = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                _Countries.AddCountry(addRequest);

            });

        }
        [Fact]
        public void AddCountry_NullCountryName()
        {
            //arrange 
            CountryAddRequest addRequest = new CountryAddRequest() { CountryName = null};

            Assert.Throws<ArgumentException>(() =>
            {
                _Countries.AddCountry(addRequest);

            });

        }

        [Fact]
        public void AddCountry_DublicateCountry()
        {
            //arrange 
            CountryAddRequest Request1 = new CountryAddRequest() { CountryName = "USA"};
            CountryAddRequest Request2 = new CountryAddRequest() { CountryName = "USA"};

            Assert.Throws<ArgumentNullException>(() =>
            {
                _Countries.AddCountry(Request1);
                _Countries.AddCountry(Request2);
            }); 

        }
        [Fact]
        public void AddCountry_ProperCountry()
        {
            //arrange 
            CountryAddRequest Request1 = new CountryAddRequest() { CountryName = "Egypt" };
            
            //Act

            CountryResponse Response = _Countries.AddCountry(Request1).Result;

           List<CountryResponse> AllCountries= _Countries.GetAllCountries().Result;

            //Assert

            Assert.True(Response.CountryID != Guid.Empty);
            Assert.Contains(Response, AllCountries);

        }
        #endregion

        #region "GetAllCountries"
        [Fact]
        public void GetAllCountries_EmptyList()
        {
            //Act

             List<CountryResponse> AllCountries = _Countries.GetAllCountries().Result;

            //Assert

            Assert.Empty(AllCountries);
           
        }

        [Fact]
        public void GetAllCountries_ProperList()
        {
            //arrange 
                 List <CountryAddRequest> ActualAllCountries = 
                new List<CountryAddRequest>() { 
                new CountryAddRequest() { CountryName = "Egypt" },
                new CountryAddRequest() { CountryName = "KSA" }
                };

            List<CountryResponse> ResponseCountries = new List<CountryResponse>();

            foreach (CountryAddRequest c in ActualAllCountries)
            {
                ResponseCountries.Add(_Countries.AddCountry(c).Result);
            }
            //Act

            List<CountryResponse> AllCountries = _Countries.GetAllCountries().Result;

            //Assert
            foreach (CountryResponse response in ResponseCountries)
            {
                Assert.Contains(response,AllCountries);
            }
        }

        #endregion

        #region "GetCountryByCountryID"

        [Fact]
        public void GetCountryByCountryID_NullCountryID() {

            //Arrange
            Guid? Countryid = null;

            //Act
            CountryResponse? countryResponse= _Countries.GetCountryById(Countryid).Result;
             
            //Assert
            Assert.Null(countryResponse);
               
        }



        [Fact]
        public void GetCountryByCountryID_ProperID()
        {
            //Arrange
            CountryResponse countryResponseFromAdd = _Countries.AddCountry(
                new CountryAddRequest() { CountryName="Egypt"}).Result;
            Guid? countryID_FromAdd = countryResponseFromAdd.CountryID;
            //Act
            
            CountryResponse? countryResponseGet = _Countries.GetCountryById(countryID_FromAdd).Result;

            //Assert
            Assert.Equal(countryResponseFromAdd,countryResponseGet);


        }

        #endregion

    }
}
