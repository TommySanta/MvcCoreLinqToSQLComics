using Microsoft.Data.SqlClient;
using MvcCoreLinqToSQL.Models;
using System.Data;

namespace MvcCoreLinqToSQL.Repositories
{
    public class RepositoryEnfermos
    {
        private DataTable tablaEnfermos;
        private SqlConnection cn;
        private SqlCommand com;
        public RepositoryEnfermos()
        {
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Encrypt=True;Trust Server Certificate=True";
            string sql = "select * from ENFERMO";
            SqlDataAdapter adEmp = new SqlDataAdapter(sql, connectionString);
            this.tablaEnfermos = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter(sql, connectionString);
            ad.Fill(this.tablaEnfermos);
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;

        }
        public List<Enfermo> GetEnfermos()
        {
            var consulta = from datos in this.tablaEnfermos.AsEnumerable() select datos;

            List<Enfermo> enfermos = new List<Enfermo>();

            foreach (var row in consulta)
            {
                Enfermo enf = new Enfermo();
                enf.Inscripcion = row.Field<string>("INSCRIPCION");
                enf.Apellido = row.Field<string>("APELLIDO");
                enf.Direccion = row.Field<string>("DIRECCION");
                enf.FechaNacimiento = row.Field<DateTime>("FECHA_NAC");
                enf.Sexo = row.Field<string>("S");
                enf.NSS = row.Field<string>("NSS");
                enfermos.Add(enf);
            }
            return enfermos;
        }
        public Enfermo FindEnfermo(string incripcion)
        {
            var consulta = from datos in this.tablaEnfermos.AsEnumerable()
                           where datos.Field<string>("INSCRIPCION") == incripcion
                           select datos;
            var row = consulta.First();
            Enfermo enf = new Enfermo();
            enf.Inscripcion = row.Field<string>("INSCRIPCION");
            enf.Apellido = row.Field<string>("APELLIDO");
            enf.Direccion = row.Field<string>("DIRECCION");
            enf.FechaNacimiento = row.Field<DateTime>("FECHA_NAC");
            enf.Sexo = row.Field<string>("S");
            enf.NSS = row.Field<string>("NSS");

            return enf;
        }

        public void DeleteEnfermo(string inscripcion)
        {
            string sql = "delete from ENFERMO where INSCRIPCION=@inscripcion";
            this.com.Parameters.AddWithValue("@inscripcion", inscripcion);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();

        }
    }
}
