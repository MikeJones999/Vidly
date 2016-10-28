using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    { 
        public int Id { get; set; }


        [Required (ErrorMessage = "please enter a Movie Name")]
        //required method with the validation warning - this is optional
        [StringLength(255)]
        public string Name { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }

        [Display(Name = "Release Date - e.g 1 Jan 1995")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Number in Stock")]
        [MinOneMovieInStockForNewMovie]
        public int NumberInStock { get; set; }


    }
}