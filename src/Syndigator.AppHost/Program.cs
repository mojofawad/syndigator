var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .AddDatabase("syndigator");

var api = builder.AddProject<Projects.Syndigator_API>("api")
    .WithReference(postgres);


builder.AddProject<Projects.Syndigator_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(api);

builder.Build().Run();
