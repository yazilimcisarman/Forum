using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddRazorPages(options =>
//{
//    options.Conventions.AuthorizeFolder("/", "/Account");
//    options.Conventions.AllowAnonymousToPage("/Account/Login");
//});

//burasini kaldirdim
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Accounts/Login"; // Giriþ yapmadan eriþilmeye çalýþýldýðýnda yönlendirilecek sayfa
//    });
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

builder.Services.AddHttpClient("api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7123/api/");
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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
