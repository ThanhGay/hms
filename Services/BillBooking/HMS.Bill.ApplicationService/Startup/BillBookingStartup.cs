using HMS.Bill.Infrastructures;
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

namespace HMS.Bill.ApplicationService.Startup
{
    public static class BillBookingStartup
    {
        public static void ConfigureBillBooking(this WebApplicationBuilder builder, string? assemblyName)
        {
            builder.Services.AddDbContext<BillBookingDbContext>(
                options =>
                {
                    options.UseSqlServer(
                        builder.Configuration.GetConnectionString("Default"),
                        options =>
                        {
                            options.MigrationsAssembly(assemblyName);
                            options.MigrationsHistoryTable(
                                DbSchema.TableMigrationsHistory,
                                DbSchema.BillBooking
                            );
                        }
                    );
                },
                ServiceLifetime.Scoped
            );


        }

    }
     
}
