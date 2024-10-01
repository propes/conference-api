using System.Text.Json;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.AddSingleton<CosmosClient>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var cosmosSettings = configuration.GetSection("CosmosDb");
    var options = new CosmosClientOptions
    {
        SerializerOptions = new CosmosSerializationOptions
        {
            PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
        }
    };
    return new CosmosClient(cosmosSettings["Endpoint"], cosmosSettings["Key"], options);
});

builder.Services.AddSingleton(sp =>
{
    var cosmosClient = sp.GetRequiredService<CosmosClient>();
    var database = cosmosClient.CreateDatabaseIfNotExistsAsync("ConferenceDb").GetAwaiter().GetResult();
    var container = database.Database.CreateContainerIfNotExistsAsync("Conferences", "/id").GetAwaiter().GetResult();
    return container.Container;
});

var app = builder.Build();

app.MapPost("/api/conferences", async (UpdateConference request, Container container) =>
{
    var conference = new Conference
    {
        Id = Guid.NewGuid().ToString(),
        Name = request.Name,
        StartLocal = request.StartLocal,
        TimeZoneId = request.TimeZoneId,
        DateModifiedUtc = DateTime.UtcNow
    };

    await container.CreateItemAsync(conference, new PartitionKey(conference.Id));
    return Results.Created($"/api/conferences/{conference.Id}", conference);
});

app.MapGet("/api/conferences/{id}", async (string id, Container container) =>
{
    try
    {
        var response = await container.ReadItemAsync<Conference>(id, new PartitionKey(id));
        var conference = response.Resource;

        var dto = new ConferenceDto
        {
            Name = conference.Name,
            StartLocal = conference.StartLocal,
            TimeZoneId = conference.TimeZoneId,
            StartUtc = DateTimeHelper.ConvertToUtc(conference.StartLocal, conference.TimeZoneId)
        };

        return Results.Ok(dto);
    }
    catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
    {
        return Results.NotFound();
    }
});

app.Run();
