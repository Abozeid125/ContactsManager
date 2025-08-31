using ContactsManager.Core.Domain.IdentityEntities;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositeries;
using RepositeryContracts;
using ServiceContracts;
using Services;

namespace CRUDexample.StartupExtensions
{
    public static class ConfigureServiceExtension
    {

        public static IServiceCollection ConfigureServices(this IServiceCollection services , IConfiguration configuration)
        {


           services.AddControllersWithViews(options =>
           {
               options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

           });
           services.AddScoped<ICountryService, CountriesService>();
           services.AddScoped<IPersonService, PersonService>();
           services.AddScoped<ICountriesRepositery, CountriesRepositery>();
           services.AddScoped<IPersonsRepositery, PersonsRepositery>();
           services.AddDbContext<PersonsDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString(

                    "DeafaultConnection"
                    ))


                );
          
            services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<PersonsDbContext>()
                .AddDefaultTokenProviders().AddUserStore<UserStore<ApplicationUser,ApplicationRole,PersonsDbContext,Guid>>().AddRoleStore<RoleStore<ApplicationRole,PersonsDbContext,Guid>>();

            services.AddAuthorization(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.FallbackPolicy = policy;
                
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/account/login";
                
            });
            return services;
        }


    }
}
