using ArtistsMVC.Models;
using ArtistsMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArtistsMVC.Controllers.Api
{
    public class AlbumsController : ApiController
    {
        private readonly AlbumRepository _albumRepository;

        public AlbumsController()
        {
            _albumRepository = new AlbumRepository();
        }
        
        
        public IEnumerable<Album> GetAlbums()
        {
            return _albumRepository.GetAll();
        }

        public Album GetAlbum(int id)
        {
            var album = _albumRepository.GetById(id);

            if (album == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return album;
        }

        [HttpPost]
        public Album CreateAlbum(Album album)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _albumRepository.Create(album);
            return album;
        }

        [HttpPut]
        public void Update(int id, Album album)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var albumInDb = _albumRepository.GetById(id);
            if(albumInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            //_albumRepository.Update(album); // throws an exception
            albumInDb.Title = album.Title;
            albumInDb.Description = album.Description;
            albumInDb.ArtistId = album.ArtistId;
            _albumRepository.Save();
        }

        [HttpDelete]
        public void DeleteAlbum(int id)
        {
            var albumInDb = _albumRepository.GetById(id);
            if (albumInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _albumRepository.Delete(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _albumRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
