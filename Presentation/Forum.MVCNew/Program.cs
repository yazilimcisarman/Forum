using Forum.Persistence.Context;
using Forum.Persistence.Context.Identity;
using Forum.Persistence.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ForumDbContext>();
builder.Services.AddDbContext<ForumIdentityDbContext>();
builder.Services.AddIdentity<ForumIdentityUser, ForumIdentityRole>()
    .AddEntityFrameworkStores<ForumIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddInfrastructureServices();
builder.Services.AddAuthorization();

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/", "/Account");
    options.Conventions.AllowAnonymousToPage("/Account/Login");
});
builder.Services.Configure<IdentityOptions>(options =>
{

    //options.Password.RequireDigit = true; //�ifre Say�sal karakteri desteklesin mi?
    options.Password.RequiredLength = 6;  //�ifre minumum karakter say�s�
    options.Password.RequireLowercase = true; //�ifre k���k harf olabilir
    options.Password.RequireLowercase = true; //�ifre b�y�k harf olabilir
    options.Password.RequireNonAlphanumeric = false; //Sembol bulunabilir

    options.Lockout.MaxFailedAccessAttempts = 5; //Kullan�c� ka� ba�ar�s�z giri�ten sonra sisteme giri� yapamas�n
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); //Ba�ar�s�z giri� i�lemlerinden sonra ne kadar s�re sonra sisteme giri� hakk� tan�ns�n
    options.Lockout.AllowedForNewUsers = true; //Yeni �yeler i�in kilit sistemi ge�erli olsun mu

    options.User.RequireUniqueEmail = true; //Kullan�c� benzersiz e-mail adresine sahip olsun

    options.SignIn.RequireConfirmedEmail = false; //Kay�t i�lemleri i�in email onaylamas� zorunlu olsun mu?
    options.SignIn.RequireConfirmedPhoneNumber = false; //Telefon onay� olsun mu?
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Giri� yapmadan eri�ilmeye �al���ld���nda y�nlendirilecek sayfa
    });
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
