using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mono.TextTemplating;
using ServiceContracts;
using ServiceContracts.DTOs.CountryDTO;
using ServiceContracts.DTOs.PersonDTO;
using System.Globalization;
using System.Threading.Tasks;

namespace xUnit.Controllers
{
    public class PersonsController : Controller
    {
        private  readonly IPersonsService _personsService;
        private  readonly ICountries _countriesService;
        public PersonsController(IPersonsService personsService, ICountries countries)
        {
            _personsService = personsService;
            _countriesService = countries;
        }
        [Route("Persons/Index")]
        [Route("/")]
        public async Task<IActionResult> Index(string searchBy, string? searchString,
            string sortBy = nameof(PersonResponse.PersonName) , enSortOrder sortOrder= enSortOrder.ASC)
        {
            ViewBag.SearchFields = new Dictionary<string, string>() {
                {nameof(PersonResponse.PersonName), "Person Name" },
                 {nameof(PersonResponse.Email), "Email" },
                 {nameof(PersonResponse.Address), "Address" },
                {nameof(PersonResponse.Gender), "Gender" },
                 {nameof(PersonResponse.CountryID), "Country ID" },
                 {nameof(PersonResponse.Age), "Age" },
                { nameof(PersonResponse.DateOfBirth), "Date of Birth" }
            };
            //List <PersonResponse> AllPeople = _personsService.GetAllPersons();
            List<PersonResponse>? persons =  await _personsService.FilterPersons(searchBy, searchString);
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;
            List<PersonResponse>? SortedPersons = await _personsService.SortPersons(persons, sortBy, sortOrder);
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();
            return View(SortedPersons);
        }


        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
           List<CountryResponse> countries=  await _countriesService.GetAllCountries();
            ViewBag.Countries = countries.Select(temp =>  new SelectListItem() { Text= temp.CountryName, Value=temp.CountryID.ToString()});
            ;
            PersonAddRequest p = new PersonAddRequest();
            return View(p);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Create(PersonAddRequest personAddRequest)
        {
            if (!ModelState.IsValid)
            {
                List<CountryResponse> countries = await _countriesService.GetAllCountries();
                ViewBag.Countries = countries.Select(temp =>
                new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });
                ViewBag.Errors = ModelState.Values.SelectMany(Error => Error.Errors.SelectMany(message => message.ErrorMessage));
                return View();

            }
           PersonResponse? response= await _personsService.AddPerson(personAddRequest);
            //List<CountryResponse> Countries = _countriesService.GetAllCountries();
            //ViewBag.Countries = Countries;
            return RedirectToAction("Index") ;
        }
        [Route("[action]/PersonID")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid PersonID)
        {
            List<CountryResponse> countries = await _countriesService.GetAllCountries();
            ViewBag.Countries = countries.Select(temp => new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });
            ;
           PersonResponse? personResponse= await _personsService.GetPersonByPersonID(PersonID);
            if (personResponse == null)
                return RedirectToAction("Index");
            PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();
            return View(personUpdateRequest);
        }
        [HttpPost]
        [Route("[action]/{PersonID}")]
        public async Task<IActionResult> Edit(PersonUpdateRequest personUpdateRequest)
        {
            PersonResponse? personResponse = await _personsService.GetPersonByPersonID(personUpdateRequest.PersonID);
            if (personResponse == null)
                return RedirectToAction("Index");
            if (!ModelState.IsValid)
            {
                List<CountryResponse> countries = await _countriesService.GetAllCountries();
                ViewBag.Countries = countries.Select(temp =>
                new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });
                ViewBag.Errors = ModelState.Values.SelectMany(Error => Error.Errors.SelectMany(message => message.ErrorMessage));
                return View(personResponse.ToPersonUpdateRequest());
            }
            PersonResponse? response = await _personsService.UpdatePerson(personUpdateRequest);
            //List<CountryResponse> Countries = _countriesService.GetAllCountries();
            //ViewBag.Countries = Countries;
            return RedirectToAction("Index");
        }
        [Route("[action]/PersonID")]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid PersonID)
        {
            List<CountryResponse> countries = await _countriesService.GetAllCountries();
            ViewBag.Countries = countries.Select(temp => new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });
            ;
            PersonResponse? personResponse = await _personsService.GetPersonByPersonID(PersonID);
            if (personResponse == null)
                return RedirectToAction("Index");
            return View(personResponse);
        }
        [HttpPost]
        [Route("[action]/{PersonID}")]
        public async Task<IActionResult> Delete(PersonUpdateRequest personUpdateRequest)
        {
            PersonResponse? personResponse = await _personsService.GetPersonByPersonID(personUpdateRequest.PersonID);
            if (personResponse == null)
                return RedirectToAction("Index");
            if (!ModelState.IsValid)
            {
                List<CountryResponse> countries = await _countriesService.GetAllCountries();
                ViewBag.Countries = countries.Select(temp =>
                new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });
                ViewBag.Errors = ModelState.Values.SelectMany(Error => Error.Errors.SelectMany(message => message.ErrorMessage));
                return View();
            }
            bool Deleted = await _personsService.DeletePerson(personResponse.PersonId);
            return RedirectToAction("Index");
        }
    }
}
