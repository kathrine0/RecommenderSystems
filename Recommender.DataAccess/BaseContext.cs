using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender.DataAccess
{
    public class BaseContext : DbContext
    {
        public BaseContext(string connectionString) : base(connectionString)
        {

        }

        public bool IsCompatible
        {
            get
            {
                return Database.CompatibleWithModel(true);
            }
        }
    }
}
