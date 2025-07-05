using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTOs;
using ServiceContracts.DTOs.CountryDTO;

namespace Services
{
  public class CountriesService : ICountries
  {
    //private field
    private readonly PersonsDbContext _db;

    //constructor
    public CountriesService(PersonsDbContext personsDbContext)
    {
      _db = personsDbContext;
    }

    public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
    {
      //Validation: countryAddRequest parameter can't be null
      if (countryAddRequest == null)
      {
        throw new ArgumentNullException(nameof(countryAddRequest));
      }

      //Validation: CountryName can't be null
      if (countryAddRequest.CountryName == null)
      {
        throw new ArgumentException(nameof(countryAddRequest.CountryName));
      }

      //Validation: CountryName can't be duplicate
      if (await _db.Countries.CountAsync(temp => temp.CountryName == countryAddRequest.CountryName) > 0)
      {
        throw new ArgumentException("Given country name already exists");
      }

      //Convert object from CountryAddRequest to Country type
      Country country = countryAddRequest.ToCountry();

      //generate CountryID
      country.CountryID = Guid.NewGuid();

      //Add country object into _countries
      _db.Countries.Add(country);
      await _db.SaveChangesAsync();

      return country.ToCountryResponse();
    }

     public async Task <List<CountryResponse>> GetAllCountries()
    {
      return await _db.Countries
        .Select(country => country.ToCountryResponse()).ToListAsync();
    }
        public async Task<CountryResponse>? GetCountryById(Guid? countryID)
        {
            if (countryID == null)
                throw new ArgumentNullException(nameof(countryID));

            Country? country_response_from_list = await _db.Countries
              .FirstOrDefaultAsync(temp => temp.CountryID == countryID);

            if (country_response_from_list == null)
                throw new ArgumentNullException(nameof(countryID));

            return country_response_from_list.ToCountryResponse();
        }
    }
}

