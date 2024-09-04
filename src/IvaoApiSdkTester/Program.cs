using System.Reflection;

using Cocona;

using Ivao.It.IvaoApiSdk;
using Ivao.It.IvaoApiSdk.Dto.Tracker;
using Ivao.It.IvaoApiSdkTester;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
builder.Services.AddTransient<ITimedRunner, TimedRunner>();

var app = builder.Build();


app.AddCommand("atcnow", async ([FromService] ITrackerApi tracker) => await tracker.GetAtcSummary());
app.AddCommand("atcnow-multi", async ([FromService] ITrackerApi tracker) =>
{
    await tracker.GetAtcSummary();
    await Task.Delay(500);
    await tracker.GetAtcSummary();
    await Task.Delay(500);
    await tracker.GetAtcSummary();
});

app.AddCommand("atc-bookings", async ([FromService] IAtcBookingsApi atcSchedulingApi)
    => await atcSchedulingApi.GetDailyAtcSchedules(icaoFilter: "li", date: DateTime.Now));

app.AddSubCommand("tracker", a =>
{
    a.AddCommand(async ([FromService] ITrackerApi tracker) =>
        await tracker.GetSessions("362802", TrackerConnectionType.Pilot, pageNumber: 1, pageSize: 5));

    a.AddCommand("fpl", async ([FromService] ITrackerApi tracker) => await tracker.GetSessionFlightPlans(54989210));
    a.AddCommand("tracks", async ([FromService] ITrackerApi tracker) => await tracker.GetSessionTracks(54989210));

    a.AddCommand("full", async ([FromService] ITrackerApi tracker, [FromService] ITimedRunner runner) =>
    {
        await runner
            .SetLabel("Full Tracker Data")
            .SetLevel(LogLevel.Warning)
            //.Run(async () => await tracker.GetFullSessionData(55007150)); //Emi BLQ-VLC
            .Run(async () => await tracker.GetFullSessionData(55006106)); //Pascu LHR-FCO
    });
});

app.AddCommand("fras", async ([FromService] ICoreApi coreApi) =>
{
    var fras = await coreApi.GetAllFras("LI", default);
    Console.WriteLine($"Fra count: {fras.Count}");
});

app.Run();



