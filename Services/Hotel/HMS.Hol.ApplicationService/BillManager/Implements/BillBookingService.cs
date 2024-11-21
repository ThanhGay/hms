using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Hol.ApplicationService.BillManager.Abstracts;
using HMS.Hol.ApplicationService.Common;
using HMS.Hol.Infrastructures;
using Microsoft.Extensions.Logging;

namespace HMS.Hol.ApplicationService.BillManager.Implements
{
    public class BillBookingService : HotelServiceBase, IBillBookingService
    {
        public BillBookingService(ILogger<BillBookingService> logger, HotelDbContext dbContext)
            : base(logger, dbContext) { }
    }
}
