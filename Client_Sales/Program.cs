using Client_Sales.API;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddNewtonsoftJson();

builder.Services.AddHttpClient<ISalesClient, SalesClient>(client =>
            client.BaseAddress = new Uri(builder.Configuration.GetSection("SalesApi").Value));

builder.Services.AddRazorPages();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
