using NetCoreLinqToSqlinjection.Models;

namespace NetCoreLinqToSqlinjection.Repositories
{
    public interface IRepositoryDoctores
    {
        List<Doctor> GetDoctores();

        void InsertarDoctor(int idDoctor, string apellido, string especialidad, int salario, int idHospital);

        void DeleteDoctor(int idDoctor);

        void UpdateDoctores(int idDoctor, string apellido, string especialidad, int salario);
        List<Doctor> GetDoctoresEspecialidad(string especialidad);
    }
}
