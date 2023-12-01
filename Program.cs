using chat_service_se357.Models;
using Microsoft.EntityFrameworkCore;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog;
using chat_service_se357.APIs;
using Microsoft.AspNetCore.SignalR;
using chat_service_se357.Hubs;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Cors;

namespace chat_service_se357
{
    public class Program
    {
        public static MyUser api_user = new MyUser();
        public static MyConversation api_conversation = new MyConversation();
        public static MyMessage api_message = new MyMessage();
        public static IHubContext<ChatHub>? chatHub;
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Debug()
                   .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                   .WriteTo.File("mylog.txt", rollingInterval: RollingInterval.Day)
                   .CreateLogger();
            try
            {
                
                var builder = WebApplication.CreateBuilder(args);
                builder.Services.AddCors(options =>
                {
                    options.AddDefaultPolicy(builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                        builder.WithOrigins("https://demo-signalr-reactjs.vercel.app")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
                });
                
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("HTTPSystem", builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).WithExposedHeaders("Grpc-Status", "Grpc-Encoding", "Grpc-Accept-Encoding");
                    });
                });
                builder.Services.AddSignalR(options =>
                {
                    options.EnableDetailedErrors = true;
                    options.KeepAliveInterval = TimeSpan.FromSeconds(5);
                    options.MaximumReceiveMessageSize = 10 * 1024 * 1024;
                    options.StreamBufferCapacity = 10 * 1024 * 1024;
                }).AddMessagePackProtocol();

                builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(DataContext.configSql));
                builder.Services.AddDatabaseDeveloperPageExceptionFilter();
                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                using (var scope = app.Services.CreateScope())
                {
                    IServiceProvider services = scope.ServiceProvider;
                    DataContext datacontext = services.GetRequiredService<DataContext>();
                    datacontext.Database.EnsureCreated();
                    await datacontext.Database.MigrateAsync();

                }

                Log.Information(String.Format("Connected to Server {0} : {1} ", DateTime.Now, DataContext.configSql));
                // Configure the HTTP request pipeline.
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();

                //app.UseCors("HTTPSystem");
                app.UseCors();
                app.UseRouting();

                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapHub<ChatHub>("/chatHub", options =>
                    {
                        options.Transports = HttpTransportType.WebSockets;
                    });
                });

                chatHub = (IHubContext<ChatHub>?)app.Services.GetService(typeof(IHubContext<ChatHub>));


                app.MapControllers();
                app.MapGet("/", () => string.Format("Server Chat of SE347- {0}", DateTime.Now));

                app.Run();

                Log.CloseAndFlush();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }
    }
}