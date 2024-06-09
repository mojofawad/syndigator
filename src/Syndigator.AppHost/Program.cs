var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .AddDatabase("syndigatorDb");

var api = builder.AddProject<Projects.Syndigator_API>("api")
    .WithReference(postgres);

// TODO: Convert frontend to Next.js or SvelteKit?
builder.AddProject<Projects.Syndigator_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(api);

// TODO: Build Migrations Service

builder.Build().Run();
