using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArtistsMVC.Models
{
    public class Album
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public ICollection<Song> Songs { get; set; }

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

    }
}