using ArtistsMVC.Models;
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
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}