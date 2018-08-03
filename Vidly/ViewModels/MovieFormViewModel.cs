using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        public int? Id { get; set; }

        [Required(ErrorMessage = "Movie Name rquired")]
        public string Name { get; set; }

        [Display(Name="Genre")]
        public byte? GenreId { get; set; }

        [Display(Name="Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name="Number in Stock")]
        public byte? NumberInStock { get; set; }


        public string Title
        {
            get
            {
                return  Id != 0 ? "Edit Movie" : "New Movie";
            }
        }

        public MovieFormViewModel()
        {
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