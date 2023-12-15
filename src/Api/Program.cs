using OpenSearch.Client;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();

if (builder.Configuration["ConnectionStrings:search"] is not string connString)
{
    throw new ArgumentException("Expected connection string search to be a string value");
}

var node = new Uri(connString);

var config = new ConnectionSettings(node)
    .ServerCertificateValidationCallback((_, _, _, _) => true); // hack to ignore SSL for local dev, DON'T folow this for production!

var client = new OpenSearchClient(config);

var response = await client.Cluster.HealthAsync();

logger.LogInformation("OpenSearch is in {status} state", response.Status);

app.MapGet("/", () => "Hello World!");

app.Run();
