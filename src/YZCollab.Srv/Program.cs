using StackExchange.Redis;
using System.Net;
using YZCollab.Srv.Configuration;
using YZCollab.Srv.Hubs;
using YZCollab.Srv.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var enviroment = builder.Environment;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddVersioning();
builder.Services.AddSignalR().AddJsonProtocol();
builder.Services.AddTransient<IMessageHubService, MessageHubService>();

#region redis
//.AddStackExchangeRedis($"{configuration.GetValue<string>("Redis")}", redisOptions =>
// {
//     redisOptions.ConnectionFactory = async writer =>
//     {
//         var config = new ConfigurationOptions
//         {
//             AbortOnConnectFail = false
//         };

//         config.EndPoints.Add(IPAddress.Loopback, 0);
//         config.SetDefaultPorts();

//         var connection = await ConnectionMultiplexer.ConnectAsync(config, writer);

//         connection.ConnectionFailed += (_, e) =>
//         {
//             Console.WriteLine("Connection to Redis failed.");
//         };

//         if (!connection.IsConnected)
//         {
//             Console.WriteLine("Did not connect to Redis.");
//         }

//         return connection;
//     };
// });

#endregion

#region cors

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        config =>
        {
            var corsOrigins = configuration.GetSection("CorsOrigins").Get<string[]>();

            if (corsOrigins == null)
                return;

            config.WithOrigins(corsOrigins)
                .AllowAnyHeader()
                .WithMethods("GET", "POST")
                .AllowCredentials();
        });
});

#endregion

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