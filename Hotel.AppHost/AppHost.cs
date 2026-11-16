var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");
var hotelDb = postgres.AddDatabase("hoteldb");

var migrationService = builder.AddProject<Projects.Hotel_MigrationService>("migrationservice")
    .WithReference(hotelDb)
    .WaitFor(hotelDb);

var apiService = builder.AddProject<Projects.Hotel_WebAPI>("apiservice")
    .WithReference(hotelDb)
    .WaitForCompletion(migrationService); // Ключевой момент!

builder.Build().Run();