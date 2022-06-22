using ArtistsMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ArtistsMVC.Repositories
{
    public class MessageRepository : IDisposable
    {
        private readonly ApplicationDbContext _context;

        public MessageRepository()
        {
            _context = new ApplicationDbContext();
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Message> GetAll()
        {
            return _context.Messages;
        }
        public Message GetByID(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var message = _context.Messages.SingleOrDefault(a => a.ID == id);

            if (!message.IsRead)
            {
                message.IsRead = true;
                Save();
            }

            return message;
        }

        public void Create(Message message)
        {
            message.Date = DateTime.Now;
            _context.Messages.Add(message);
            Save();
        }

        public void Update(Message message)
        {
            _context.Entry(message).State = EntityState.Modified;
            Save();
        }

        public void Delete(int? id)
        {
            Message thisMessage = GetByID(id);
            _context.Messages.Remove(thisMessage);
            Save();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}