using System.Configuration;
using System.Data.SqlClient;

namespace MTTWebAPI.Domain.Factories
{
    public static class SqlConnectionFactory
    {
        public static SqlConnection Create()
        {
            return new SqlConnection {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString
            };
        }
    }
}
