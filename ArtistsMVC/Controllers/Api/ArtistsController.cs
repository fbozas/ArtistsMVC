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
    public class ArtistsController : ApiController
    {
        private readonly ArtistRepository _artistRepository;
        public ArtistsController()
        {
            _artistRepository = new ArtistRepository();
        }

        public IHttpActionResult GetArtists()
        {
            return Ok(_artistRepository.GetAll()
            .Select(Mapper.Map<Artist, ArtistDto>));
        }

        public IHttpActionResult GetArtist(int id)
        {
            var artist = _artistRepository.GetById(id);
            if (artist == null)
                return NotFound();
            return Ok(Mapper.Map<Artist, ArtistDto>(artist));
        }

        [HttpPost]
        public IHttpActionResult Create(ArtistDto artistDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var artist = Mapper.Map<ArtistDto, Artist>(artistDto);
            _artistRepository.Create(artist);
            artistDto.ID = artist.ID;
            return Created(new Uri(Request.RequestUri + "/" + artist.ID), artistDto);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, ArtistDto artistDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var artistInDb = _artistRepository.GetById(id);
            if (artistInDb == null)
                return NotFound();

            Mapper.Map(artistDto, artistInDb); // Here you should pass the id from body
            _artistRepository.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public IHttpActionResult DeleteArtist(int id)
        {
            var artistInDb = _artistRepository.GetById(id);
            if (artistInDb == null)
                return NotFound();
            _artistRepository.Delete(id);

            return Ok(artistInDb);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _artistRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
