using KitM4.Blog.Api.Utilities;
using KitM4.Blog.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.ConfigureServices();
builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.ConfigureSettings(builder.Configuration);

// TODO: add cors
// TODO: add logging

WebApplication app = builder.Build();

using IServiceScope scope = app.Services.CreateScope();
await DatabaseInitializer.InitializeAsync(scope);

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();