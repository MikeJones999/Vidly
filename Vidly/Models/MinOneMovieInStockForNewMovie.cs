using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MinOneMovieInStockForNewMovie : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           //GetCustomAttribute movie object being passed - and cast to movie
            var movie = (Movie)validationContext.ObjectInstance;

            if (movie.Id == 0 && movie.NumberInStock == 0)
            {
                return new ValidationResult("Cannot have zero copies of newly created Movies");
            }


            return ValidationResult.Success;

        }

    }
}