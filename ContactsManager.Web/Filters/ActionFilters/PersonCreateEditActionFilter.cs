using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts.DTOS;
using ServiceContracts;
using CRUDexample.Controllers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;

namespace CRUDexample.Filters.ActionFilters
{
    public class PersonCreateEditActionFilter : IAsyncActionFilter
    {
        private readonly ICountryService _countryService;

        public PersonCreateEditActionFilter(ICountryService countryService) 
        {
            _countryService = countryService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is PersonController personController)
            {


                if (!personController. ModelState.IsValid)
                {


                    List<CountryResponse> countries = await _countryService.GetAllCountries();
                    personController.ViewBag.Countries = countries.Select(temp =>
                    new SelectListItem() { Text = temp.CountryName, Value = temp.CountryId.ToString() });

                    personController.ViewBag.Errors = personController.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                    var personRequest = context.ActionArguments["personRequest"];

                    context.Result = personController.View(personRequest); //short-circuits or skips the subsequent action filters & action method



                }
                else
                {
                    await next();
                }
            }
            else
            {
                await next();
            }
        }
    }
}
