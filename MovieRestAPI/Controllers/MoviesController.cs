using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MovieLib;
using MovieRestAPI.Managers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieRestAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase{
        private MoviesManager manager = new MoviesManager();
        // GET: api/<MoviesController>
        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get() {
            return Ok(manager.GetAllMovies());
        }

        // GET api/<MoviesController>/5
        [HttpGet("{id}")]
        public ActionResult<Movie> Get(int id) {
            return Ok(manager.GetMovieById(id));
        }

        // POST api/<MoviesController>
        [HttpPost]
        public ActionResult<Movie> Post([FromBody] Movie newMovie){
            return manager.PostMovie(newMovie);
        }

        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
