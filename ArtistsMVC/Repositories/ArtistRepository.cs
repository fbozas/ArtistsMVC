using ArtistsMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ArtistsMVC.Repositories
{
    public class ArtistRepository : IDisposable
    {
        private readonly ApplicationDbContext _context;

        public ArtistRepository()
        {
            _context = new ApplicationDbContext();
        }
        //------------------------------------------------------------------------
        public IEnumerable<Artist> GetAll()
        {
            return _context.Artists;
        }

        public IEnumerable<Artist> GetAllWithAlbums()
        {
            return _context.Artists
                .Include(a => a.Albums);
        }

        public Artist GetById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return _context.Artists
                .SingleOrDefault(a => a.ID == id);
        }

        public Artist GetByIdWithAlbums(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return _context.Artists
                .Include(a => a.Albums)
                .SingleOrDefault(a => a.ID == id);
        }

        public void Create(Artist artist)
        {
            _context.Artists.Add(artist);
            _context.SaveChanges();
        }

        public void Update(Artist artist)
        {
            _context.Entry(artist).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            Artist artist = GetById(id);
            _context.Artists.Remove(artist);
            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Artist> GetFirstFour()
        {
            var artists = _context.Artists;

            var firstFour = artists.Take(4);

            return firstFour;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}