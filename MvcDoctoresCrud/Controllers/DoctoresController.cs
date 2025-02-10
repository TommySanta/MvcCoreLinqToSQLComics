using Microsoft.AspNetCore.Mvc;
using MvcDoctoresCrud.Models;
using MvcDoctoresCrud.Repositories;

namespace MvcDoctoresCrud.Controllers
{
    public class DoctoresController : Controller
    {
        RepositoryDoctores repo;
        public DoctoresController()
        {
            this.repo = new RepositoryDoctores();
        }
        public IActionResult Index()
        {
            List<Doctor> doctores = this.repo.GetDoctores();

            ViewData["ESPECIALIDADES"] = this.repo.GetEspecialidades();
            return View(doctores);
        }
        [HttpPost]
        public IActionResult Index(string especialidad)
        {
            ViewData["ESPECIALIDADES"] = this.repo.GetEspecialidades();
            List<Doctor> doctores = this.repo.GetDoctoresEspecialidad(especialidad);
            return View(doctores);
        }
        public IActionResult Delete(int  doctorno)
        {
            this.repo.DeleteDoctor(doctorno);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Doctor doctor = await this.repo.FindDoctorAsync(id);
            return View(doctor);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Doctor doctor)
        {
            await this.repo.UpdateDoctorAsync(doctor.HospitalCod, doctor.DoctorNo
                , doctor.Apellido,doctor.Especialidad,doctor.Salario);
            return RedirectToAction("Index");
        }
    }
}
