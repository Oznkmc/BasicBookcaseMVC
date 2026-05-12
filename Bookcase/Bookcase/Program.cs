using Microsoft.EntityFrameworkCore;
using Bookcase.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Servisleri Konteynıra Ekle
builder.Services.AddControllersWithViews();

// --- BURASI HATALIYDI, ŞİMDİ DÜZELTTİK ---
builder.Services.AddDbContext<UygulamaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VarsayilanBaglanti")));
// ------------------------------------------

var app = builder.Build();

// 2. HTTP İstek Hattını Yapılandır
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
