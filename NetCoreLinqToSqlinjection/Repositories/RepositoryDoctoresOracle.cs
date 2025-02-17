using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using NetCoreLinqToSqlinjection.Models;
using Oracle.ManagedDataAccess.Client;
using static Azure.Core.HttpHeader;

#region Procedimientos oracle
//create or replace procedure sp_delete_doctor
//(p_iddoctor Doctor.Doctor_No%TYPE)
//as
//begin
//  delete from DOCTOR where Doctor_No=p_iddoctor;
//commit;
//end;
#endregion
namespace NetCoreLinqToSqlinjection.Repositories
{
    public class RepositoryDoctoresOracle : IRepositoryDoctores
    {
        private DataTable tablaDoctores;
        private OracleConnection cn;
        private OracleCommand com;
        public RepositoryDoctoresOracle()
        {
            string connectionString =
                @"Data Source=LOCALHOST:1521/XE; Persist Security Info=True; User Id=SYSTEM; Password=oracle";
            this.tablaDoctores = new DataTable();
            this.cn = new OracleConnection(connectionString);
            this.com = new OracleCommand();
            this.com.Connection = this.cn;
            OracleDataAdapter ad =
                new OracleDataAdapter("select * from DOCTOR", connectionString);
            ad.Fill(this.tablaDoctores);
        }

        public void DeleteDoctor(int idDoctor)
        {
            string sql = "SP_DELETE_DOCTOR";

            this.com.Parameters.Add(new OracleParameter(":iddoctor", idDoctor));
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public List<Doctor> GetDoctores()
        {
            var consulta = from datos in this.tablaDoctores.AsEnumerable()
                           select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach (var row in consulta)
            {
                Doctor doctor = new Doctor
                {
                    IdHospital = row.Field<int>("HOSPITAL_COD"),
                    IdDoctor = row.Field<int>("DOCTOR_NO"),
                    Apellido = row.Field<string>("APELLIDO"),
                    Especialidad = row.Field<string>("ESPECIALIDAD"),
                    Salario = row.Field<int>("SALARIO"),
                };
                doctores.Add(doctor);
            }
            return doctores;
        }

        public void InsertarDoctor(int idDoctor, string apellido, string especialidad, int salario, int idHospital)
        {
            string sql = "insert into DOCTOR values (:idhospital, :iddoctor, :apellido, :especialidad, :salario)";

            OracleParameter pamIdHospital = new OracleParameter(":idhospital", idHospital);
            OracleParameter pamIdDoctor = new OracleParameter(":iddoctor", idDoctor);
            OracleParameter pamApellido = new OracleParameter(":apellido", apellido);
            OracleParameter pamEspecialidad = new OracleParameter(":especialidad", especialidad);
            OracleParameter pamSalario = new OracleParameter(":salario", salario);

            this.com.Parameters.Add(pamIdHospital);
            this.com.Parameters.Add(pamIdDoctor);
            this.com.Parameters.Add(pamApellido);
            this.com.Parameters.Add(pamEspecialidad);
            this.com.Parameters.Add(pamSalario);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

        public void UpdateDoctores(int idDoctor, string apellido, string especialidad, int salario)
        {
            string sql = "SP_UPDATE_DOCTOR";

            this.com.Parameters.Add(new OracleParameter(":iddoctor", idDoctor));
            this.com.Parameters.Add(new OracleParameter(":apellido", apellido));
            this.com.Parameters.Add(new OracleParameter(":especialidad", especialidad));
            this.com.Parameters.Add(new OracleParameter(":salario", salario));

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
        public List<Doctor> GetDoctoresEspecialidad(string especialidad)
        {
            string sql = "SELECT * FROM DOCTOR WHERE ESPECIALIDAD = :especialidad";

            OracleDataAdapter ad = new OracleDataAdapter(sql, this.cn);
            ad.SelectCommand.Parameters.Add(new OracleParameter(":especialidad", especialidad));
            ad.Fill(this.tablaDoctores);

            List<Doctor> doctores = new List<Doctor>();
            var consulta = from datos in this.tablaDoctores.AsEnumerable()
                           select datos;

            foreach (var row in consulta)
            {
                Doctor doctor = new Doctor
                {
                    IdHospital = row.Field<int>("HOSPITAL_COD"),
                    IdDoctor = row.Field<int>("DOCTOR_NO"),
                    Apellido = row.Field<string>("APELLIDO"),
                    Especialidad = row.Field<string>("ESPECIALIDAD"),
                    Salario = row.Field<int>("SALARIO"),
                };
                doctores.Add(doctor);
            }

            return doctores;
        }
    }
}
   
            
        
