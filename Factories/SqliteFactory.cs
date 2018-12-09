using MutableDataGridSync.Data;
using MutableDataGridSync.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutableDataGridSync.Factories
{
    static class SqliteFactory
    {
        private static DbContext _dbContext;
        private static LichtpunktRepository _lichtpunktRepository;

        public static DbContext GetDbContext()
        {
            if (_dbContext == null)
            {
                _dbContext = new DbContext("Data Source=test.db");
            }

            return _dbContext;
        }

        public static LichtpunktRepository GetLichtpunktRepository()
        {
            if (_lichtpunktRepository == null)
            {
                _lichtpunktRepository = new LichtpunktRepository(GetDbContext());
            }

            return _lichtpunktRepository;
        }
    }
}
