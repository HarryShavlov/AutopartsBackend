using AutopartsBackend.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Настройка Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Autoparts API", Version = "v1" });
});

// Регистрация HttpClient для ML-сервиса
builder.Services.AddHttpClient<IRecognitionService, RecognitionService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5001"); // URL ML-сервиса
    client.Timeout = TimeSpan.FromSeconds(30);
});

// Настройка HTTPS
builder.WebHost.UseUrls("http://localhost:5005", "https://localhost:5006");

var app = builder.Build();

// Включение Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Autoparts API v1");
    });
}

// HTTPS редирект
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.MapGet("/test-ssl", () => "HTTPS работает!");
app.Run();

// using AutopartsBackend.API;
// using AutopartsBackend.API.Services;
// using Microsoft.AspNetCore.Builder;

// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.OpenApi.Models;

// var builder = WebApplication.CreateBuilder(args);

// // Добавление сервисов
// builder.Services.AddControllers();
// builder.Services.AddEndpointsApiExplorer();

// // Настройка Swagger
// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Autoparts API", Version = "v1" });
// });

// // Регистрация HttpClient для ML-сервиса
// builder.Services.AddHttpClient<IRecognitionService, RecognitionService>(client =>
// {
//     client.BaseAddress = new Uri("http://localhost:5001"); // URL ML-сервиса
// });

// // Добавьте конфигурацию HTTPS
// builder.WebHost.ConfigureKestrel(options => {
//     options.ListenLocalhost(5006, listenOptions => {
//         listenOptions.UseHttps();
//     });
// });



// var app = builder.Build();

// // Настройте редирект с явным указанием порта
// app.UseHttpsRedirection(options => {
//     options.HttpsPort = builder.Configuration.GetValue<int?>("HttpsPort") ?? 5006;
// });

// // Включение Swagger только в Development
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI(c =>
//     {
//         c.SwaggerEndpoint("/swagger/v1/swagger.json", "Autoparts API v1");
//     });
// }

// // app.UseHttpsRedirection();
// app.UseAuthorization();
// app.MapControllers();
// app.Run();