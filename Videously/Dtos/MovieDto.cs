using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Videously.Models;

namespace Videously.Dtos
{
    public class MovieDto
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title cannot be empty")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Genre cannot be empty")]
        public GenreDto Genre { get; set; }

        [Required(ErrorMessage = "Release Date cannot be empty")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Date added cannot be empty")]
        public DateTime DateAdded { get; set; }

        [Required(ErrorMessage = "Number in Stock cannot be empty")]
        [Range(1, 20, ErrorMessage = "Number in Stock should be between 1 and 20")]
        public byte NumberInStock { get; set; }
    }
}