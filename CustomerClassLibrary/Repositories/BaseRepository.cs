using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerClassLibrary.Repositories
{
    public abstract class BaseRepository
    {
        public SqlConnection GetConnection()
        {
            return new SqlConnection("Server=MANSUROV-RUS\\SQLEXPRESS;Database=CustomerDB_Mansurov;Trusted_Connection=True;");
        }
    }
}
