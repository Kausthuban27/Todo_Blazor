using Todo_Blazor.Components;
using Todo_Blazor.SharedService;

var builder = WebApplication.CreateBuilder(args);

var todoBaseUrl = builder.Configuration["ApiUrl:todoBaseUrl"]!;
var userBaseUrl = builder.Configuration["ApiUrl:userBaseUrl"]!;
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton<UserState_Management_Service>();
builder.Services.AddHttpClient("TodoHttpClient", client =>
{
    client.BaseAddress = new Uri(todoBaseUrl);
});

builder.Services.AddHttpClient("UserHttpClient", client =>
{
    client.BaseAddress = new Uri(userBaseUrl);
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
