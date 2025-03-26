using System.IdentityModel.Tokens.Jwt;
using System.Text;
using HMS.Auth.ApplicationService.StartUp;
using HMS.Hol.ApplicationService.Common;
using HMS.Hol.ApplicationService.Common;
using HMS.Hol.ApplicationService.Startup;
using HMS.Noti.ApplicationService.StartUp;
using HMS.Noti.ApplicationService.StartUp;
using HMS.WebAPI.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Extensions.Hosting;
using Serilog.Sinks.Network;

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
            // Thêm dịch vụ Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "API của tôi", Version = "v1" });

                // Thêm cấu hình bảo mật Bearer Token
                options.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Nhập token vào ô bên dưới (không cần 'Bearer ' phía trước).",
                    }
                );
                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer",
                                },
                            },
                            new List<string>()
                        },
                    }
                );
            });
            //configure serilog
            builder.Host.UseSerilog(
                (context, loggerConfig) =>
                {
                    loggerConfig.ReadFrom.Configuration(context.Configuration);
                }
            );
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
            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowAllOrigins",
                    policy =>
                    {
                        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    }
                );
            });
            var app = builder.Build();

            app.UseCors("AllowAllOrigins");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //configure serilog
            app.UseMiddleware<RequestLogContextMiddleware>();
            app.UseSerilogRequestLogging();

            //upload file
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<TokenValidationMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
