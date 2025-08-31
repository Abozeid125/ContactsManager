using ServiceContracts.DTOS;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{

    /// <summary>
    /// Represents Business logic for person entity 
    /// </summary>
    public interface IPersonService
    {/// <summary>
    /// Add a new person in the list of persons
    /// </summary>
    /// <returns>a new person response with generated ID </returns>
        Task< PersonResponse> AddPerson(PersonAddRequest? personAddRequest);


        /// <summary>
        /// returns list of oerson responses
        /// </summary>
        /// <returns></returns>
      Task<  List<PersonResponse>> GEtAllPersons();


        /// <summary>
        /// returns an object person according to its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       Task<PersonResponse?> GetPersonByID(Guid? id);

        /// <summary>
        /// returns all persons that matched with these search parameters
        /// </summary>
        /// <param name="SearchBy"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        Task<List<PersonResponse>> GetFilteredPersons(string SearchBy, string? Value);


        /// <summary>
        /// 
        /// Update specifies person details based on personID
        /// </summary>
        /// <param name="allPersons"></param>
        /// <param name="sortBy"></param>
        /// <param name="sortOrderOptions"></param>
        /// <returns>return sorted persons as personResponse list </returns>
        Task<List<PersonResponse>> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrderOptions);
        
        Task<PersonResponse>  UpdatePerson(PersonUpdateRequest? personUpdateRequest);

        public Task< bool> DeletePerson (Guid? id);
        public Task<MemoryStream> GetPersonsExcel();





    }
}
