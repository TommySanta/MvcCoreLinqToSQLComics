using System.Data;
using Microsoft.Data.SqlClient;

namespace NetCoreLinqToSqlinjection.Repositories
{
    public class RepositoryDoctoresSQLServer
    {
        private DataTable tableDoctores;
        private SqlConnection con;
        private SqlCommand com;

        public RepositoryDoctoresSQLServer()
        {
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Encrypt=True;Trust Server Certificate=True";
            this.con = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.con;
            this.tableDoctores = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter
                ("select * from FOCTOR", connectionString);
            ad.Fill(this.tableDoctores);
        }
    }
} 

