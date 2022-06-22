using ArtistsMVC.Models;
using System;
using System.Collections.Generic;

namespace ArtistsMVC.Repositories
{
    public class ArtistsRepository : IDisposable
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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}