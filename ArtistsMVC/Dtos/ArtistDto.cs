using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ArtistsMVC.Dtos
{
    public class ArtistDto
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        [StringLength(60, MinimumLength = 2)]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "LastName is required")]
        [StringLength(60, MinimumLength = 2)]
        public string LastName { get; set; }

        [NotMapped]
        [Display(Name = "Artist")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}