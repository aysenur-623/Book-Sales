using BookSalesProject.MvcUI.ApiServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // uygulama içinde HttpContext.Session ý kullanabilmemizi saðlamak için cotainer a register ediyoruz.
builder.Services.AddHttpContextAccessor(); // IHttpContextAccessor tipinden nesne isteyen yerlere ilgili nesnenin verilebilmesi için

//---------------------------------------------------
// Aþaðýdaki ikili IoC ye, web servisler haberleþmek için bie lazým olan HttpClient nesnesi üretmek ve HttpApiService nesnesi üretmek direktiflerini vermiþ oluyor. 
builder.Services.AddHttpClient();
builder.Services.AddScoped<IHttpApiService, HttpApiService>();
//---------------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // uygulama içinde HttpContext.Session ý kullanabilmemizi saðlamak için pipeline a bu middleware i ekliyoruz

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "adminPanelDefault",
    areaName: "AdminPanel",
    pattern: "{area}/{controller=Auth}/{action=LogIn}/{id?}"
  );



app.Run();
