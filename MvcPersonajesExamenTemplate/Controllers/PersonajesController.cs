using Microsoft.AspNetCore.Mvc;
using MvcPersonajesExamenTemplate.Models;
using MvcPersonajesExamenTemplate.Services;

namespace MvcPersonajesExamenTemplate.Controllers
{
    public class PersonajesController : Controller
    {
        private ServiceApiPersonajes service;
        private ServiceStorageAWS serviceStorage;
        public PersonajesController(ServiceApiPersonajes service, ServiceStorageAWS serviceStorage)
        {
            this.service = service;
            this.serviceStorage = serviceStorage;
        }
        public async Task<IActionResult> Index()
        {
            List<Personaje> personajes =
                await this.service.GetPersonajesAsync();
            return View(personajes);
        }

        public async Task<IActionResult> Details(int id)
        {
            Personaje personaje = await this.service.FindPersonaje(id);
            return View(personaje);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Personaje personaje, IFormFile file)
        {
            personaje.Imagen = file.FileName;
            using (Stream stream = file.OpenReadStream())
            {
                await this.serviceStorage.UploadFileAsync
                    (file.FileName, stream);
            }

            await this.service.CreatePersonajeAsync(personaje);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Personaje personaje = await this.service.FindPersonaje(id);
            return View(personaje);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Personaje personaje)
        {
            await this.service.UpdatePersonajeAsync(personaje);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeletePersonajeAsync(id);
            return RedirectToAction("Index");
        }

    }
}
