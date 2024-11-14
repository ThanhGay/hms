using HMS.Hol.Infrastructures;
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


        }

    }
}
