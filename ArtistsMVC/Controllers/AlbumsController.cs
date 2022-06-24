using ArtistsMVC.Models;
using ArtistsMVC.Repositories;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ArtistsMVC.Controllers
{
    public class AlbumsController : Controller
    {

        private readonly AlbumRepository _albumRepository;
        private readonly ArtistRepository _artistsRepository;

        public AlbumsController()
        {
            _albumRepository = new AlbumRepository();
            _artistsRepository = new ArtistRepository();
        }

        // GET: Albums
        public ActionResult Index()
        {
            var albums = _albumRepository.GetAllWithArtist();
            return View(albums.ToList());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {

            try
            {
                Album album = _albumRepository.GetByIdWithArtist(id);
                if (album == null)
                {
                    return HttpNotFound();
                }

                return View(album);
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            ViewBag.ArtistId = new SelectList(_artistsRepository.GetAll(), "ID", "FullName");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                if(album.ImageFile == null)
                {
                    album.Thumbnail = "na_image.jpg";
                }
                else
                {
                    album.Thumbnail = Path.GetFileName(album.ImageFile.FileName);
                    string fullPath = Path.Combine(Server.MapPath("~/img/"), album.Thumbnail);
                    album.ImageFile.SaveAs(fullPath);
                }

                _albumRepository.Create(album);
                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(_artistsRepository.GetAll(), "ID", "FullName", album.ArtistId);
            return View(album);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = _albumRepository.GetById(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistId = new SelectList(_artistsRepository.GetAll(), "ID", "FullName", album.ArtistId);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                if(album.ImageFile != null)
                {
                    album.Thumbnail = Path.GetFileName(album.ImageFile.FileName);
                    string fullPath = Path.Combine(Server.MapPath("~/img/"), album.Thumbnail);
                    album.ImageFile.SaveAs(fullPath);
                }
                

                _albumRepository.Update(album);
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(_artistsRepository.GetAll(), "ID", "FirstName", album.ArtistId);
            return View(album);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Album album = _albumRepository.GetByIdWithArtist(id);

            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _albumRepository.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _artistsRepository.Dispose();
                _albumRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
