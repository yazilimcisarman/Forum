using Forum.API.Extensions;
using Forum.API.Models;
using Forum.Persistence.Context;
using Forum.Persistence.Context.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Events;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Serilog konfigürasyonu
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
        sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
        {
            TableName = "Logs",
            AutoCreateSqlTable = true
        },
        restrictedToMinimumLevel: LogEventLevel.Information
    )
    .CreateLogger();

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
        .WriteTo.MSSqlServer(
            connectionString: context.Configuration.GetConnectionString("DefaultConnection"),
            sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
            {
                TableName = "Logs",
                AutoCreateSqlTable = true
            },
            restrictedToMinimumLevel: LogEventLevel.Information
        );
});
//builder.Host.UseSerilog((context, services, configuration) =>
//{
//    configuration
//        .ReadFrom.Configuration(context.Configuration)
//        .ReadFrom.Services(services)
//        .Enrich.FromLogContext()
//        .WriteTo.Console()
//        .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day);
//});

builder.AddServiceDefaults();

builder.Services.AddDbContext<ForumDbContext>();
builder.Services.AddDbContext<ForumIdentityDbContext>(); 
builder.Services.AddIdentity<ForumIdentityUser, ForumIdentityRole>()
    .AddEntityFrameworkStores<ForumIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddInfrastructureServices();
builder.Services.AddControllers();
builder.Services.AddAuthorization();

builder.Services.AddOpenApi(opt =>
{
    opt.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();


app.MapDefaultEndpoints();

app.MapScalarApiReference(
    opt =>
    {
        opt.Title = "Forum v1";
        opt.Theme = ScalarTheme.DeepSpace;
        opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
    }
);

app.MapOpenApi();

app.UseHttpsRedirection();
app.UseSerilogRequestLogging(opts =>
{
    opts.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("RequestPath", httpContext.Request.Path);
        diagnosticContext.Set("RequestMethod", httpContext.Request.Method);
        diagnosticContext.Set("StatusCode", httpContext.Response.StatusCode);
        diagnosticContext.Set("UserName", httpContext.User.Identity?.Name ?? "Anonymous");
    };
});
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
