using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Videously.Models;

namespace Videously.ViewModels
{
    public class MovieFormViewModel
    {
        public Movie Movie { get; set; }
        public List<Genre> Genres { get; set; }
    }
}