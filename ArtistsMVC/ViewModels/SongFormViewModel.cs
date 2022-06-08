using ArtistsMVC.Models;
using System.Collections.Generic;

namespace ArtistsMVC.ViewModels
{
    public class SongFormViewModel
    {
        public List<Album> Albums { get; set; }
        public Song Song { get; set; }
    }
}