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
    public class MessagesController : ApiController
    {
        private readonly MessageRepository _messageRepository;

        public MessagesController()
        {
            _messageRepository = new MessageRepository();
        }

        public IHttpActionResult GetMessages()
        {
            return Ok(_messageRepository
                .GetAll()
                .Select(Mapper.Map<Message, MessageDto>));
        }

        public IHttpActionResult GetMessage(int id)
        {
            var message = _messageRepository.GetByID(id);

            if (message == null)
                return NotFound();

            return Ok(Mapper.Map<Message, MessageDto>(message));
        }

        [HttpPost]
        public IHttpActionResult CreateMessage(MessageDto messageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var message = Mapper.Map<MessageDto, Message>(messageDto);
            _messageRepository.Create(message);

            messageDto.ID = message.ID;
            messageDto.Date = message.Date;
            // to do
            return Created(new Uri($"{Request.RequestUri}/{message.ID}"), messageDto);
        }


        [HttpPut]
        public IHttpActionResult Update(int id, MessageDto messageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var messageInDb = _messageRepository.GetByID(id);
            if (messageInDb == null)
            {
                return NotFound();
            }

            Mapper.Map(messageDto, messageInDb);
            _messageRepository.Save();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public IHttpActionResult DeleteMessage(int id)
        {
            var messageInDb = _messageRepository.GetByID(id);

            if (messageInDb == null)
                return NotFound();

            _messageRepository.Delete(id);
            return Ok(messageInDb);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _messageRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
