using System.Collections.Generic;
using MovieLib;

namespace MovieRestAPI.Managers {
    public class MoviesManager{
        private static int nextId = 1;

            private static List<Movie> moviesList = new List<Movie>(){
                new Movie(){Country = "Denmark", Id = nextId++, LengthInMinutes = 120, MovieName = "Druk"},
                new Movie(){Country = "Korea", Id = nextId++, LengthInMinutes = 174, MovieName = "Train to Busan"},
                new Movie(){Country = "America", Id = nextId++, LengthInMinutes = 143, MovieName = "Star Wars"},
            };

        public IEnumerable<Movie> GetAllMovies(){
            IEnumerable<Movie> result = moviesList;
            return result;
        }

        public Movie GetMovieById(int id){
            var result = moviesList.Find(m => m.Id == id);
            return result;
        }

        public Movie PostMovie(Movie newMovie){
            newMovie.Id = nextId++;
            moviesList.Add(newMovie);
            return newMovie;
        }
    }
}
