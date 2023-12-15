using AppHost;

using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var opensearch = builder.AddOpenSearchContainer("search");

builder.AddProject<Api>("api").WithReference(opensearch);

builder.Build().Run();