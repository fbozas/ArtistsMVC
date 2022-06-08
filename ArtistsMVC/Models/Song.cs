using System.ComponentModel.DataAnnotations;

namespace ArtistsMVC.Models
{
    public class Song
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(255, MinimumLength = 3)]
        public string Title { get; set; }

        public string Youtube { get; set; }

        [Display(Name = "Album")]
        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }
}