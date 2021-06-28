using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebMVCUitgaven.Database;
using WebMVCUitgaven.Domain;
using WebMVCUitgaven.Models;

namespace WebMVCUitgaven.Controllers
{
   
    public class UitgaveController : Controller
    {
        private readonly IUitgaveDatabase uitgaveDatabase;
        private readonly IWebHostEnvironment hostEnvironment;
        public UitgaveController(IUitgaveDatabase uitgaveDatabase, IWebHostEnvironment hostEnvironment)
        {
            this.uitgaveDatabase = uitgaveDatabase;
            this.hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            var vm = uitgaveDatabase.GetUitgaven().Select(x => new UitgaveListViewModel
            {
                Beschrijving = x.Beschrijving,
                Bedrag = x.Bedrag,
                Soort = x.Soort,
                Datum = x.Datum,
                Extra = x.Extra,
                Koper = x.Koper,
                PhotoUrl = x.PhotoUrl,
                PhotoUrl2 = x.PhotoUrl2,
                Id = x.Id
            }); ;
            return View(vm);
        }
        //public IActionResult Create()
        //{
        //    return View();
        //}
        public IActionResult Detail([FromRoute] int id)
        {
            var uitgave = uitgaveDatabase.GetUitgave(id);
            var vm = new UitgaveDetailViewModel
            {
                Beschrijving = uitgave.Beschrijving,
                Bedrag = uitgave.Bedrag,
                Soort = uitgave.Soort,
                Datum = uitgave.Datum,
                Extra = uitgave.Extra,
                Koper = uitgave.Koper,
                PhotoUrl = uitgave.PhotoUrl,
                PhotoUrl2 = uitgave.PhotoUrl2
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new UitgaveCreateViewModel());
        }
        [HttpPost]
        public IActionResult Create([FromForm] UitgaveCreateViewModel uitgave)
        {
            if (TryValidateModel(uitgave))
            {
                //uitgaveDatabase.Insert(new Uitgave
                //{
                //    Beschrijving = uitgave.Beschrijving,
                //    Bedrag = uitgave.Bedrag,
                //    //Soort = uitgave.Soort.ToString(),
                //    Datum = uitgave.Datum,
                //    Extra = uitgave.Extra,
                //    Koper = uitgave.Koper,
                //    //PhotoUrl = uitgave.PhotoUrl
                //});
                var newUitgave = new Uitgave()
                {
                    Beschrijving = uitgave.Beschrijving,
                    Bedrag = uitgave.Bedrag,
                    Datum = uitgave.Datum,
                    Extra = uitgave.Extra,
                    Soort = uitgave.Soort,
                    Koper = uitgave.Koper
                };
                if (uitgave.Photo != null)
                {
                    string uniqueFileName = UploadPhoto(uitgave.Photo);
                    newUitgave.PhotoUrl = "/photos/" + uniqueFileName;
                }
                if (uitgave.Photo2 != null)
                {
                    string uniqueFileName = UploadPhoto(uitgave.Photo2);
                    newUitgave.PhotoUrl2 = "/photos/" + uniqueFileName;
                }
                uitgaveDatabase.Insert(newUitgave);
                
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Edit([FromRoute] int id)
        {

            var uitgave = uitgaveDatabase.GetUitgave(id);

            var vm = new UitgaveEditViewModel
            {
                Beschrijving = uitgave.Beschrijving,
                Bedrag = uitgave.Bedrag,
                Datum = uitgave.Datum,
                Extra = uitgave.Extra,

                Koper = uitgave.Koper,
                PhotoUrl = uitgave.PhotoUrl,
                PhotoUrl2 = uitgave.PhotoUrl2
            };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, [FromForm] UitgaveEditViewModel uitgave)
        {
            if (TryValidateModel(uitgave))
            {
                //movieDatabase.Update(id, new Movie
                //{

                //    Title = movie.Title,
                //    Descr = movie.Descr,
                //    Genre = movie.Genre
                //});

                var newUitgave = new Uitgave
                {
                    Beschrijving = uitgave.Beschrijving,
                    Datum = uitgave.Datum,
                    Soort = uitgave.Soort,
                    Bedrag = uitgave.Bedrag,

                };
                var dbUitgave = uitgaveDatabase.GetUitgave(id);
                if (uitgave.Photo == null)
                {
                    newUitgave.PhotoUrl = dbUitgave.PhotoUrl;
                }
                else
                {
                    if (string.IsNullOrEmpty(dbUitgave.PhotoUrl))
                    {
                        DeletePhoto(dbUitgave.PhotoUrl);
                    }
                    string uniqueFileName = UploadPhoto(uitgave.Photo);
                    newUitgave.PhotoUrl = Path.Combine("/Photos", uniqueFileName);
                }
                if (uitgave.Photo2 == null)
                {
                    newUitgave.PhotoUrl2 = dbUitgave.PhotoUrl2;
                }
                else
                {
                    if (string.IsNullOrEmpty(dbUitgave.PhotoUrl2))
                    {
                        DeletePhoto(dbUitgave.PhotoUrl2);
                    }
                    string uniqueFileName = UploadPhoto(uitgave.Photo2);
                    newUitgave.PhotoUrl2 = Path.Combine("/Photos", uniqueFileName);
                }
                uitgaveDatabase.Update(id, newUitgave);



                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Delete([FromRoute] int id)
        {
            var uitgave = uitgaveDatabase.GetUitgave(id);

            var vm = new UitgaveDeleteViewModel
            {
                Beschrijving = uitgave.Beschrijving,
                Id = uitgave.Id
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete([FromRoute] int id)
        {
            uitgaveDatabase.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private string UploadPhoto(IFormFile photo)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
            string pathname = Path.Combine(hostEnvironment.WebRootPath, "photos");
            string fileNameWithPath = Path.Combine(pathname, uniqueFileName);
            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                photo.CopyTo(stream);
            }
            return uniqueFileName;
        }
        private void DeletePhoto(string photoUrl)
        {
            string path = Path.Combine(hostEnvironment.WebRootPath, photoUrl.Substring(1));
            System.IO.File.Delete(path);
        }
    }
}
