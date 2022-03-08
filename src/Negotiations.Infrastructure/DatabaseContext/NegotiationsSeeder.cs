using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Negotiations.Infrastructure.DatabaseContext
{
    public class NegotiationsSeeder
    {
        private readonly NegotiationsContext _dbContext;
        public NegotiationsSeeder(NegotiationsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            var pendingMigration = _dbContext.Database.GetPendingMigrations();
            if (pendingMigration != null && pendingMigration.Any())
            {
                _dbContext.Database.Migrate();
            }
            
        }
    }
}