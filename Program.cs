using AzureDocumentIntelligence.Middlewares;
using AzureDocumentIntelligence.Models;
using AzureDocumentIntelligence.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<AzureSettings>(
    builder.Configuration.GetSection("AzureDocumentIntelligence")
);
builder.Services.Configure<SecuritySettings>(
    builder.Configuration.GetSection("Security"));
// Register as a singleton service
builder.Services.AddSingleton<AppService>();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
app.UseMiddleware<SignatureValidationMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
