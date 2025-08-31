using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts.DTOS;

namespace CRUDexample.Filters.ActionFilters
{
    public class PersonListActionFilter : IActionFilter
    {
        private readonly ILogger<PersonListActionFilter> _logger;

        public PersonListActionFilter(ILogger<PersonListActionFilter> logger) {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("Person Index method Filter on executed");


        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            _logger.LogInformation("Person Index method Filter onexecuting");

            if(context.ActionArguments.ContainsKey("SearchBy") )
            {
                if (context.ActionArguments["SearchBy"] != null)
                {
                    string searchBy = context.ActionArguments["SearchBy"].ToString();

                    var searchOptions = new List<string>
                    {

                        nameof(PersonResponse.Name),
                        nameof(PersonResponse.Email),
                        nameof(PersonResponse.Address),
                        nameof(PersonResponse.Gender),
                        nameof(PersonResponse.ReceiveNewsLetters),
                        nameof(PersonResponse.Age),
                        nameof(PersonResponse.Country),



                    };

                    _logger.LogInformation($"actual searchby value = {searchBy}");

                  if(!  searchOptions.Any(temp => temp == searchBy))
                    {

                        
                        context.ActionArguments["SearchBy"] = nameof(PersonResponse.Name).ToString();
                        _logger.LogInformation($"updated searchby value = {context.ActionArguments["Searchby"].ToString()}");


                    }



                }



            }
        }
    }
}
