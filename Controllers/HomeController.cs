using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using COMP235MVCDemo.Models;
using COMP235MVCDemo.DataAccessObjects;

namespace COMP235MVCDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Movie(Movie m, string Save)
        {
            if (Save == "Save")
            {
                return Details(m, Save);
            }    
            ViewBag.Message = "My Movie Page.";
            MovieDAO dAO = new MovieDAO();
            Movies movies = dAO.getAllMovies();
            Movie movie;
            // error fix in case all movies are deleted => the Movie page will show "No movie to show" 
            //the below code below make the page movie error when we have deleted all movies, or when we dont have movie with id = 1
            
            //below is the code provided in the doc, which is not quite optimized
            //Movie m = dAO.getMovieById(1);
            //return View(m);

            //i changed it as below
            if (movies == null || movies.Items.Count == 0)
            {
                movie = new Movie(0, "No movie to show", "No movie to show", "No movie to show");
            }
            else
            // if there is at least 1 movie, it will show the 1st movie in the array
            {
                movie = dAO.getMovieById(movies.Items[0].Id);
            }
            return View(movie);
        }

        public ActionResult AddMovie(Movie m, string Save) // each view need an Action Result Method to be viewed
        {
            ViewBag.Message = "Add A Movie Page";
            //Save is the name of the Value on the Button
            if (Save == "Save"){
            MovieDAO dAO = new MovieDAO();
                    dAO.InsertMovie(m);
            ViewBag.Message = "Movie Added successfully";
            }
            return View("AddMovie");
        }
        //action result: movie delete in allmovies page
        public ActionResult MoviesDelete(Movie m, Movies movies)
        {
            
                MovieDAO dAO = new MovieDAO();
                dAO.deleteMovie(m);

                movies = dAO.getAllMovies();
                ViewBag.Message = "All movies.";
                return View("AllMovies", movies);
            
            
        }
        //action result: movie delete in movie page => this is quite similar to above method
        public ActionResult MovieDelete(Movie m, Movies Movies)
        {
            MovieDAO dAO = new MovieDAO();
            dAO.deleteMovie(m);
            Movies = dAO.getAllMovies();
            ViewBag.Message = "All movies.";

            return View("AllMovies", Movies);
            
        }


        //action result: all movies showing
        public ActionResult AllMovies(Movies m, String Save)
        {
            ViewBag.Message = "All movies.";
            MovieDAO dAO = new MovieDAO();
            if (Save == "Save")
            {
                Movie movie = m.Items[m.EditIndex];
                dAO.updateMovie(movie);
                movie.IsEditable = false;
                m.EditIndex = -1;
            }
            m = dAO.getAllMovies();
            return View(m);
        }

        //action result: detail for each movie
        public ActionResult Details(Movie movie, string Save)
        {
            MovieDAO dAO = new MovieDAO();
            if(Save == "Save")
            {
                dAO.updateMovie(movie);
                movie.IsEditable = false;

            }    


            movie = dAO.getMovieById(movie.Id);
            return View("Movie", movie);
        }

        //action result: editting movie in the AllMovies page
        public ActionResult MoviesEdit(int? id, Movies movies)
        {
            int id2 = id ?? default(int);
            MovieDAO dAO = new MovieDAO();
            movies = dAO.getAllMovies();
            movies.EditIndex = dAO.setMovieToEditMode(movies.Items, id2);
            ViewBag.Message = "All movies.";
            return View("AllMovies", movies);
        }

        // action result: editting movie in the movie page
        public ActionResult MovieEdit(Movie m)
        {
            ViewBag.Message = "My Movie Page.";
            MovieDAO dAO = new MovieDAO();
            
            dAO.setAMovieToEditMode(m);
            return View("Movie", m);
        }
    }

}