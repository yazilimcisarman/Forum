var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Forum_API>("forum-api");

builder.Build().Run();
