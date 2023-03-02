//Yazdýðýmýz trace uyarýlarýný bu þekilde proje dosyasýnda oluþturulan ErrorLogs.txt dosyasýný açar flush ile yazar. ErrorLogs.txt dosyasý varsa üstüne yazar
//yoksa oluþturur
using System.Diagnostics;
Trace.Listeners.Add(new TextWriterTraceListener("ErrorLogs.txt"));
Trace.AutoFlush = true;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
