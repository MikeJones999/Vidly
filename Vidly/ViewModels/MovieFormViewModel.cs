using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {

        public IEnumerable<Genre> Genres { get; set; }

        public int? Id { get; set; }


        [Required(ErrorMessage = "please enter a Movie Name")]
        //required method with the validation warning - this is optional
        [StringLength(255)]
        public string Name { get; set; }


        [Required]
        public byte? GenreId { get; set; }

        [Required]
        [Display(Name = "Release Date - e.g 1 Jan 1995")]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Number in Stock")]
        [MinOneMovieInStockForNewMovie]
        public int? NumberInStock { get; set; }


        public string title
        {
            get
            {
                if (Id != 0)
                {
                    return "Edit Movie";
                }

                return "New Movie";

            }

        }

        public MovieFormViewModel()
        {
            //ensure hidden field in form is populated
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
        }



    }
}