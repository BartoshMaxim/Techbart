using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Techbart.DB
{
    public static partial class Bakery
    {
        public static IDbConnection Sql()
        {
            return new SqlConnection(Settings.Bakery.Api.Sql.ConnectionStrings.SqlReporting);
        }

        public static SqlConnection Sql(string connectionStrings)
        {
            return new SqlConnection(connectionStrings);
        }
    }
}