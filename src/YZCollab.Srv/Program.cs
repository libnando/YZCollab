using YZCollab.Srv.Configuration;
using YZCollab.Srv.Hubs;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var enviroment = builder.Environment;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddVersioning();
builder.Services.AddSignalR().AddJsonProtocol(); //.AddStackExchangeRedis();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://localhost:7294")
                .AllowAnyHeader()
                .WithMethods("GET", "POST")
                .AllowCredentials();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors();
app.MapHub<MessageHub>("/hub");

app.Run();

public partial class Program { }