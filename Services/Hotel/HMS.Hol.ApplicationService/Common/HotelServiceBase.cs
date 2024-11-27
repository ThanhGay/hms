using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Hol.Infrastructures;
using Microsoft.Extensions.Logging;

namespace HMS.Hol.ApplicationService.Common
{
    public abstract class HotelServiceBase
    {
        protected readonly ILogger _logger;
        protected readonly HotelDbContext _dbContext;

        protected HotelServiceBase(ILogger logger, HotelDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
    }
}
