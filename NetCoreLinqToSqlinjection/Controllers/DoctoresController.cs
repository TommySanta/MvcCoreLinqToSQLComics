using Microsoft.AspNetCore.Mvc;
using NetCoreLinqToSqlinjection.Models;
using NetCoreLinqToSqlinjection.Repositories;

namespace NetCoreLinqToSqlinjection.Controllers
{
    public class DoctoresController : Controller
    {
        IRepositoryDoctores repo;

        public DoctoresController(IRepositoryDoctores repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Doctor> doctores = this.repo.GetDoctores();
            return View(doctores);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Doctor doc)
        {
            this.repo.InsertarDoctor(doc.IdDoctor, doc.Apellido
            , doc.Especialidad, doc.Salario, doc.IdHospital);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int iddoctor)
        {
            this.repo.DeleteDoctor(iddoctor);
            return RedirectToAction("Index");
        }
    }
}
