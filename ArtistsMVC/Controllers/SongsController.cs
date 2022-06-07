using ArtistsMVC.Models;
using System.Data.Entity;
using System.Linq;
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

            return View(songs);
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}