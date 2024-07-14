using AutoMapper;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Videously.Dtos;
using Videously.Models;

namespace Videously.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        // GET /api/movies
        public IHttpActionResult GetMovies()
        {
            return Ok(_context.Movies.Include(c => c.Genre).Select(Mapper.Map<Movie, MovieDto>));
        }

        // GET /api/customers/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.Include(b => b.Genre).SingleOrDefault(c => c.Id == id);
            if (movie == null)
            {
                return NotFound();
                //throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST /api/movies/
        [HttpPost]
        public IHttpActionResult PostMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.Entry(movie.Genre).State = EntityState.Unchanged;
            _context.SaveChanges();
            movieDto.Id = movie.Id;
            return Created(new Uri($"{Request.RequestUri}/{movieDto.Id}"), movieDto);
        }

        // PUT /api/movies/1
        [HttpPut]
        public IHttpActionResult PutMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movieInDb = _context.Movies.Include(g => g.Genre).SingleOrDefault(c => c.Id == id);
            if (movieInDb == null)
            {
                return NotFound();
            }

            Mapper.Map(movieDto, movieInDb);
            _context.Entry(movieInDb.Genre).State = EntityState.Unchanged;
            return Ok(_context.SaveChanges());
        }

        // PUT /api/movies/1
        [HttpPut]
        public IHttpActionResult PutMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movieInDb = _context.Movies.Include(g => g.Genre).SingleOrDefault(c => c.Id == movieDto.Id);
            if (movieInDb == null)
            {
                return NotFound();
            }

            Mapper.Map(movieDto, movieInDb);

            return Ok(_context.SaveChanges());
        }

        // DELETE /api/movies/1
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.Include(g => g.Genre).SingleOrDefault(c => c.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            return Ok(_context.SaveChanges());
        }
    }
}
