using System.Reflection;

using Cocona;

using Ivao.It.IvaoApiSdk;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

var builder = CoconaApp.CreateBuilder();
builder.Logging
    .AddDebug().SetMinimumLevel(LogLevel.Debug)
    .AddConsole().SetMinimumLevel(LogLevel.Debug);

//Asp.net core like config with secrets
builder.Configuration
    .AddJsonFile("appsettings.json", false, false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, false)
    .AddUserSecrets(Assembly.GetExecutingAssembly());

builder.Services.AddIvaoApi(builder.Configuration);

var app = builder.Build();


app.AddCommand("atcnow", async ([FromService] ITrackerApi tracker) => await tracker.GetAtcSummary());

app.AddCommand("atc-bookings", async ([FromService] IAtcBookingsApi atcSchedulingApi)
    => await atcSchedulingApi.GetDailyAtcSchedules(icaoFilter: "li", date: new DateTime(2024, 03, 11)));

app.AddCommand("fpl-list", async ([FromService] IFlightPlansApi fpls) => await fpls.GetUsersFlightPlans("362802"));


app.Run();



