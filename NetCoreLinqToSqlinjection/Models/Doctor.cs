namespace NetCoreLinqToSqlinjection.Models
{
    public class Doctor
    {
        public int idDoctor { get; set; }
        public string Apellido { get; set; }
        public string Especialidad { get; set; }
        public int Salario { get; set; }
        public int idHospital { get; set; }
    }
}
