using System.IdentityModel.Tokens.Jwt;
using System.Text;
using HMS.Auth.ApplicationService.StartUp;
using HMS.Hol.ApplicationService.Common;
using HMS.Hol.ApplicationService.Startup;
using HMS.Noti.ApplicationService.StartUp;
using HMS.WebAPI.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using HMS.Noti.ApplicationService.StartUp;
using Serilog.Extensions.Hosting;
using Serilog.Sinks.Network;
using HMS.Hol.ApplicationService.Common;

namespace HMS.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // configure jwt
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            builder
                .Services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                        ValidAudience = builder.Configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"])
                        ),
                    };
                });
            builder.Services.AddAuthorization();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.ConfigureAuth(typeof(Program).Namespace);
            builder.ConfigureHotel(typeof(Program).Namespace);
            builder.ConfigureNotification(typeof(Program).Namespace);

            //configure serilog
            builder.Host.UseSerilog((context, loggerConfig) =>
            {
                loggerConfig.ReadFrom.Configuration(context.Configuration);
            });
            // Đăng ký dịch vụ cần thiết
            //builder.Services.AddSingleton<DiagnosticContext>();

            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(builder.Configuration)
            //    .CreateLogger();
            //builder.Host.UseSerilog();
            //builder.Services.AddHttpContextAccessor();
            //builder.Services.AddScoped<Utils>();

            // configure logging
            //builder.Logging.ClearProviders();
            //builder.Logging.AddConsole();
            //builder.Logging.SetMinimumLevel(LogLevel.Information);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //configure serilog
            app.UseMiddleware<RequestLogContextMiddleware>();
            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<TokenValidationMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
