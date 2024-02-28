using Microsoft.AspNetCore.Mvc;
using Scarpe___Co.Models;

namespace Scarpe___Co.Controllers
{
    public class ArticoloController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(StaticDb.GetAll());
        }

        [HttpGet]
        public IActionResult Details([FromRoute] int? id)
        {
            if(id.HasValue)
            {
                var articolo = StaticDb.GetById(id);
                if(articolo is null)
                {
                    return View("Error");
                }
                return View(articolo);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string nome, double prezzo, string descrizione, string imgCopertina, string imgAggiuntive)
        {
            var articolo = StaticDb.Add(nome, prezzo, descrizione, imgCopertina, imgAggiuntive);
            return RedirectToAction("Details", new {id = articolo.Id });
        }

        [HttpGet]
        public IActionResult Edit([FromRoute] int? id)
        {
            if (id is null) return RedirectToAction("Index", "Articolo");

            var articolo = StaticDb.GetById(id);
            if (articolo is null) return View("Error");

            return View(articolo);
        }

        [HttpPost]
        public IActionResult Edit(Articolo articolo)
        {
            var updatedArticolo = StaticDb.Modify(articolo);
            if (updatedArticolo is null) return View("Error");

            return RedirectToAction("Details", new { id = updatedArticolo.Id });
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var articolo = StaticDb.GetById(id);
            return View(articolo);
        }

        [HttpPost]
        public IActionResult Delete(Articolo articolo)
        {
            var articoloDeleted = StaticDb.HardDelete(articolo.Id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SoftDelete(Articolo articolo)
        {
            var articoloDeleted = StaticDb.SoftDelete(articolo.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Cestino()
        {
            var articoliDeleted = StaticDb.GetAllDeleted();
            return View(articoliDeleted);
        }

        [HttpPost]
        public IActionResult Recover(Articolo articolo)
        {
            var recoveredArticolo = StaticDb.Recover(articolo.Id);
            if (recoveredArticolo is null)
            {
                return RedirectToAction("Cestino");
            }
            return RedirectToAction("Details", new { id = recoveredArticolo.Id });
        }
    }
}
