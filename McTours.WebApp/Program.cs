//Yazd���m�z trace uyar�lar�n� bu �ekilde proje dosyas�nda olu�turulan ErrorLogs.txt dosyas�n� a�ar flush ile yazar. ErrorLogs.txt dosyas� varsa �st�ne yazar
//yoksa olu�turur
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
