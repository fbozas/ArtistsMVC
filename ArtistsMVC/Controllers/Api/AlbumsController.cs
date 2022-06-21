using ArtistsMVC.Dtos;
using ArtistsMVC.Models;
using ArtistsMVC.Repositories;
using AutoMapper;
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
        
        
        public IEnumerable<AlbumDto> GetAlbums()
        {
            return _albumRepository.GetAll()
                .Select(Mapper.Map<Album, AlbumDto>);
        }

        public AlbumDto GetAlbum(int id)
        {
            var album = _albumRepository.GetById(id);

            if (album == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Album, AlbumDto>(album);
        }

        [HttpPost]
        public AlbumDto CreateAlbum(AlbumDto albumDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var album = Mapper.Map<AlbumDto, Album>(albumDto);
            _albumRepository.Create(album);
            albumDto.ID = album.ID;
            return albumDto;
        }

        [HttpPut]
        public void Update(int id, AlbumDto albumDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var albumInDb = _albumRepository.GetById(id);
            if(albumInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map(albumDto, albumInDb); // Here you should pass the id from body
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
