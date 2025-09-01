var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.todoappBE_Web>("web");

builder.Build().Run();
