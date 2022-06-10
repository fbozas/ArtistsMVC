using ArtistsMVC.Models;
using System.Collections.Generic;

namespace ArtistsMVC.Repositories
{
    public class ArtistsRepository
    {
        private readonly ApplicationDbContext _context;

        public ArtistsRepository()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<Artist> GetAll()
        {
            return _context.Artists;
        }
    }
}