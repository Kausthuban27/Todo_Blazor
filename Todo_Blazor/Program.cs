using Todo_Blazor.Components;
using Todo_Blazor.SharedService;

var builder = WebApplication.CreateBuilder(args);

var baseUrl = builder.Configuration["ApiUrl:baseUrl"];
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton<UserState_Management_Service>();
builder.Services.AddHttpClient();
builder.Services.AddScoped(c => new HttpClient
{
    BaseAddress = new Uri(baseUrl!)
});

builder.Services.AddScoped<UserState_Management_Service>();
builder.Services.AddScoped<TodoService>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
