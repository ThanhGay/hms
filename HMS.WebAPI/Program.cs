using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Text;
using HMS.Auth.ApplicationService.StartUp;
using HMS.Auth.ApplicationService.UserModule.Implements;
using HMS.Hol.ApplicationService.Common;
using HMS.Hol.ApplicationService.Startup;
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

            // fire base
            // signaIR
            builder.Services.AddSignalR();

            var app = builder.Build();

            // core
            app.UseCors("AllowAllOrigins");

            // Chat hub
            app.MapHub<ChatHub>("/chatHub");
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapHub<ChatHub>("/chatHub");
            //});

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

            async Task HandleWebSocket(WebSocket webSocket)
            {
                var buffer = new byte[1024 * 4];
                while (webSocket.State == WebSocketState.Open)
                {
                    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    if (result.MessageType  == WebSocketMessageType.Text)
                    {
                        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        Console.WriteLine($"Nhận từ client: {message}");

                        var responseMessage = Encoding.UTF8.GetBytes($"Server nhân được: {message}");
                        await webSocket.SendAsync(new ArraySegment<byte>(responseMessage), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    else if(result.MessageType == WebSocketMessageType.Close)
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Đóng kết nối", CancellationToken.None);
                    }
                }
            }
        }
    }
}
