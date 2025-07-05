using ServiceContracts.DTOs.CountryDTO;

namespace ServiceContracts
{
    public interface ICountries
    {
       public Task< CountryResponse> AddCountry(CountryAddRequest? request);

      public Task<  List <CountryResponse>> GetAllCountries();

       public Task< CountryResponse>? GetCountryById(Guid? id);

    }
}
