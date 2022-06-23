using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace ArtistsMVC.Models
{
    public class Artist
    {
        public int ID { get; set; }

        [Required(ErrorMessage= "FirstName is required")]
        [StringLength(60, MinimumLength = 2)]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "LastName is required")]
        [StringLength(60, MinimumLength = 2)]
        public string LastName { get; set; }

        public string Thumbnail { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [NotMapped]
        [Display(Name = "Artist")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public ICollection<Album> Albums { get; set; }
    }
}