using chat_service_se357.Models;
using Microsoft.EntityFrameworkCore;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog;
using chat_service_se357.APIs;

namespace chat_service_se357
{
    public class Program
    {
        public static MyUser api_user = new MyUser();
        public static MyConversation api_conversation = new MyConversation();
        public static MyMessage api_message = new MyMessage();
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
                builder.WebHost.ConfigureKestrel((context, option) =>
                {
                    option.ListenAnyIP(50000, listenOptions =>
                    {
                        // listenOptions.UseHttps("./smartlook.com.vn.pfx", "stvg");

                    });
                    option.Limits.MaxConcurrentConnections = null;
                    option.Limits.MaxRequestBodySize = null;
                    option.Limits.MaxRequestBufferSize = null;
                });
                // Add services to the container.
                //builder.Logging.AddSerilog();
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("HTTPSystem", builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).WithExposedHeaders("Grpc-Status", "Grpc-Encoding", "Grpc-Accept-Encoding");
                    });
                });

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

                app.UseCors("HTTPSystem");
                app.UseRouting();

                app.UseAuthorization();


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