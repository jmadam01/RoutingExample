using RoutingAssignment.Middleware;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseCountriesMiddleware();

app.Run();
 