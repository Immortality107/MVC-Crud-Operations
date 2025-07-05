using ServiceContracts.DTOs;
using ServiceContracts.DTOs.PersonDTO;

namespace ServiceContracts
{
    public interface IPersonsService
    {
       Task<PersonResponse>? AddPerson(PersonAddRequest? personAddRequest);

        Task<List<PersonResponse>> GetAllPersons();

        Task<PersonResponse>? GetPersonByPersonID(Guid? ID);

       Task <List< PersonResponse>>? FilterPersons(string SearchBy, string SearchValue);

        Task<List<PersonResponse>>? SortPersons(List<PersonResponse> allPersons, string SearchColumn, enSortOrder sortOrder);

       Task < PersonResponse?> UpdatePerson(PersonUpdateRequest? personUpdateRequest);

       Task < bool> DeletePerson(Guid? PersonID);
    }
}
