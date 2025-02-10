using Microsoft.AspNetCore.Mvc;
using MvcComicsTomas.Models;
using MvcComicsTomas.Repositories;

namespace MvcComicsTomas.Controllers
{
    public class ComicsController : Controller
    {
        RepositoryComics repo;
        public ComicsController()
        {
            this.repo = new RepositoryComics();
        }
        public IActionResult Index()
        {
            List<Comic> comics = this.repo.GetComics();
            return View(comics);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string nombre, string imagen, string descripcion)
        {
            Comic comic = new Comic();
            comic.Nombre = nombre;
            comic.Imagen = imagen;
            comic.Descripcion = descripcion;
            this.repo.CreateComic(comic);
            return RedirectToAction("Index");
        }
        public IActionResult Consultar()
        {
            ViewData["COMICSNOMBRE"] = this.repo.GetNombreComics();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Consultar(string nombre)
        {

            ViewData["COMICSNOMBRE"] = this.repo.GetNombreComics();

            Comic comic = await this.repo.GetComicNombreAsync(nombre);

            return View(comic);
        }

    }
}
