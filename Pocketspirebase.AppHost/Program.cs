using Pocketspire;

var builder = DistributedApplication.CreateBuilder(args);

var pocketbase = builder.AddPocketbaseContainer("customerdb");

var apiService = builder.AddProject<Projects.Pocketspirebase_ApiService>("apiservice");


builder.AddProject<Projects.Pocketspirebase_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService)
    .WaitFor(pocketbase);

builder.Build().Run();
