using HMS.Hol.ApplicationService.BillManager.Abstracts;
using HMS.Hol.ApplicationService.BillManager.Implements;
using HMS.Hol.ApplicationService.Common;
using HMS.Hol.ApplicationService.HotelManager.Abstracts;
using HMS.Hol.ApplicationService.HotelManager.Implements;
using HMS.Hol.ApplicationService.RoomManager.Abstracts;
using HMS.Hol.ApplicationService.RoomManager.Implements;
using HMS.Hol.Infrastructures;
using HMS.Shared.ApplicationService.Hotel.Room;
using HMS.Shared.Constant.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Hol.ApplicationService.Startup
{
    public static class HotelStartup
    {
        public static void ConfigureHotel(this WebApplicationBuilder builder, string? assemblyName)
        {
            builder.Services.AddDbContext<HotelDbContext>(
                options =>
                {
                    options.UseSqlServer(
                        builder.Configuration.GetConnectionString("Default"),
                        options =>
                        {
                            options.MigrationsAssembly(assemblyName);
                            options.MigrationsHistoryTable(
                                DbSchema.TableMigrationsHistory,
                                DbSchema.Hotel
                            );
                        }
                    );
                },
                ServiceLifetime.Scoped
            );

            builder.Services.AddScoped<IHotelService, HotelService>();
            builder.Services.AddScoped<IRoomService, RoomService>();
            builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();
            builder.Services.AddScoped<IBillBookingService, BillBookingService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IInteriorService, InteriorService>();
            builder.Services.AddScoped<IInformationRoomService, InformationRoomService>();
            builder.Services.AddScoped<Utils>();
        }

    }
}
