using ArtistsMVC.Models;
using ArtistsMVC.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ArtistsMVC.Controllers
{
    public class SongsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SongsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Songs
        public ActionResult Index()
        {
            var songs = _context
                .Songs
                .Include(s => s.Album.Artist)
                .ToList();

            if(User.IsInRole("Administrator") || User.IsInRole("Editor"))
            {
                return View(songs);
            }

            return View("SongsIndexWithoutEdit", songs);
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var song = _context
                .Songs
                .Include(s => s.Album.Artist)
                .SingleOrDefault(s => s.ID == id);

            if(song == null)
            {
                return HttpNotFound();
            }

            return View(song);
        }

        public ActionResult New()
        {
            // Get albums from the database
            var albums = _context.Albums.ToList();
            // Init and fill the viewmodel
            var viewmodel = new SongFormViewModel()
            {
                Song = new Song(),
                Albums = albums
            };

            // Return the appropriate view with the viewmodel
            return View("SongForm", viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Song song)
        {
            song.Youtube = $"https://www.youtube.com/embed/{song.Youtube}";

            if(song.ID == 0)
            {
                _context.Songs.Add(song);
            }
            else
            {
                var songInDb = _context.Songs.SingleOrDefault(s => s.ID == song.ID);
                songInDb.Title = song.Title;
                songInDb.Youtube = song.Youtube;
                songInDb.AlbumId = song.AlbumId;
            }

            if (!ModelState.IsValid)
            {
                var viewModel = new SongFormViewModel()
                {
                    Song = song,
                    Albums = _context.Albums.ToList()
                };

                return View("SongForm", viewModel);
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Songs");
        }

        [Authorize(Roles = "Administrator,Editor")]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Get the song from database
            var song = _context.Songs
                .Include(s => s.Album)
                .SingleOrDefault(s => s.ID == id);

            // Check if the song is null
            if(song == null)
            {
                return HttpNotFound();
            }

            // Get the albums from database
            var albums = _context.Albums.ToList();

            // Init the viewModel and fill it
            var viewModel = new SongFormViewModel()
            {
                Song = song,
                Albums = albums
            };

            return View("SongForm", viewModel);
        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var song = _context.Songs
                .Include(s => s.Album)
                .SingleOrDefault(s => s.ID == id);

            if(song == null)
            {
                return HttpNotFound();
            }

            return View(song);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var song = _context.Songs
                .SingleOrDefault(s => s.ID == id);

            if(song == null)
            {
                return HttpNotFound();
            }

            _context.Songs.Remove(song);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}