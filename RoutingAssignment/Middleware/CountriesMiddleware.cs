using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using RoutingAssignment.Helpers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RoutingAssignment.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CountriesMiddleware
    {
        private readonly RequestDelegate _next;

        public CountriesMiddleware(RequestDelegate next)
        {

            
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            

            await _next(httpContext);
        }

        
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CountriesMiddlewareExtensions
    {
        public static IApplicationBuilder UseCountriesMiddleware(this IApplicationBuilder builder)
        {

            CountriesHelper ch = new CountriesHelper();

            builder.UseRouting();
            builder.UseEndpoints(endpoints =>
            {
                endpoints.Map("countries", async (context) => {

                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync(ch.WriteCountries());
                });

                endpoints.Map("countries/{countryID}", async (context) =>
                {


                    int idKey;
                    
                    int.TryParse(Convert.ToString(context.Request.RouteValues["countryID"]),out idKey);

                    if (idKey > ch.GetTotalCountries())
                    {
                        string error = ch.GetCountryById((int)idKey);
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync(error);
                    }
                    else if (idKey == 0)
                    {
                        string error = ch.GetCountryById((int)idKey);
                        context.Response.StatusCode = 404;
                        await context.Response.WriteAsync(error);
                    }
                    else if (idKey >100)
                    {

                        string error = ch.GetCountryById((int)idKey);
                        context.Response.StatusCode = 404;
                        await context.Response.WriteAsync(error);
                    }
                    else
                    {
                        string country = ch.GetCountryById((int)idKey);
                        context.Response.StatusCode = 200;
                        await context.Response.WriteAsync(country);
                    }
                    



                });


                endpoints.Map("countries/count", async (context) =>
                {

                    var countryCount = ch.GetTotalCountries();
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync(countryCount.ToString());

                });

            });

            return builder.UseMiddleware<CountriesMiddleware>();
        }
    }
}
