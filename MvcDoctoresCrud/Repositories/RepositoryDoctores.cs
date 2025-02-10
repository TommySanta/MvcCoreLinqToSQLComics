using Microsoft.Data.SqlClient;
using MvcDoctoresCrud.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MvcDoctoresCrud.Repositories
{
    public class RepositoryDoctores
    {
        DataTable tablaDoctores;
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;
        public RepositoryDoctores()
        {
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Encrypt=True;Trust Server Certificate=True";
            string sql = "select * from DOCTOR";
            SqlDataAdapter adEmp = new SqlDataAdapter(sql, connectionString);
            this.tablaDoctores = new DataTable();
            SqlDataAdapter ad = new SqlDataAdapter(sql, connectionString);
            ad.Fill(this.tablaDoctores);
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }
        public List<Doctor> GetDoctores()
        {
            var consulta = from datos in this.tablaDoctores.AsEnumerable() select datos;

            List<Doctor> doctores = new List<Doctor>();

            foreach (var row in consulta)
            {
                Doctor doc = new Doctor();
                doc.HospitalCod = row.Field<int>("HOSPITAL_COD");
                doc.DoctorNo = row.Field<int>("DOCTOR_NO");
                doc.Apellido = row.Field<string>("APELLIDO");
                doc.Especialidad = row.Field<string>("ESPECIALIDAD");
                doc.Salario = row.Field<int>("SALARIO");
                doctores.Add(doc);
            }
            return doctores;
        }
        public List<Doctor> GetDoctoresEspecialidad(string especialidad)
        {
            var consulta = from datos in this.tablaDoctores.AsEnumerable()
                           where datos.Field<string>("ESPECIALIDAD") == especialidad
                           select datos;

            List<Doctor> doctores = new List<Doctor>();
            foreach (var row in consulta)
            {
                Doctor doctor = new Doctor
                {
                    HospitalCod = row.Field<int>("HOSPITAL_COD"),
                    DoctorNo = row.Field<int>("DOCTOR_NO"),
                    Apellido = row.Field<string>("APELLIDO"),
                    Especialidad = row.Field<string>("ESPECIALIDAD"),
                    Salario = row.Field<int>("SALARIO"),
                };
                doctores.Add(doctor);
            }
            return doctores;

        }

        public List<string> GetEspecialidades()
        {
            var consulta = (from datos in this.tablaDoctores.AsEnumerable()
                            select datos.Field<string>("ESPECIALIDAD")).Distinct();
            return consulta.ToList();
        }

        public void DeleteDoctor(int doctorno)
        {
            string sql = "delete from DOCTOR where DOCTOR_NO=@doctorno";
            this.com.Parameters.AddWithValue("@doctorno", doctorno);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();

        }
        public async Task<Doctor> FindDoctorAsync(int doctorNo)
        {
            string sql = "select * from DOCTOR where DOCTOR_NO=@doctorno";
            this.com.Parameters.AddWithValue("@doctorno", doctorNo);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            Doctor departamento = new Doctor();
            await this.reader.ReadAsync();
            departamento.HospitalCod = int.Parse(this.reader["HOSPITAL_COD"].ToString());
            departamento.DoctorNo = int.Parse(this.reader["DOCTOR_NO"].ToString());
            departamento.Apellido = this.reader["APELLIDO"].ToString();
            departamento.Especialidad = this.reader["ESPECIALIDAD"].ToString();
            departamento.Salario = int.Parse(this.reader["SALARIO"].ToString());
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
            return departamento;
        }
        public async Task UpdateDoctorAsync(int hospitalCod, int doctorNo, string apellido, string especialidad, int salario)
        {
            string sql = "UPDATE DOCTOR SET APELLIDO = @apellido, ESPECIALIDAD = @especialidad, SALARIO = @salario WHERE DOCTOR_NO = @doctorNo";

            this.com.Parameters.AddWithValue("@doctorNo", doctorNo);
            this.com.Parameters.AddWithValue("@apellido", apellido);
            this.com.Parameters.AddWithValue("@especialidad", especialidad);
            this.com.Parameters.AddWithValue("@salario", salario);

            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = sql;

            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }
    }
}
