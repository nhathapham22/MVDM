using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using COMP235MVCDemo.Models;
using System.Data.SqlClient;

namespace COMP235MVCDemo.DataAccessObjects
{
    
    public class MovieDAO
    {   
        
        //var of connection string - connect to the datasource
        string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=Movies;Integrated Security=True";

        //method: getMovieById
        public Movie getMovieById(int id)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText =
            "SELECT Id,Title,Director,Description FROM Movies where Id = @Id";
            cmd.Parameters.AddWithValue("Id", id);
            List<Movie> movies = new List<Movie>();
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Movie m = new Movie(
            Convert.ToInt32(reader["Id"]),
            reader["Title"].ToString(),
            reader["Director"].ToString(),
            reader["Description"].ToString()
            ) ;
            return m;
        }

        //method: InsertMovie
        public void InsertMovie(Movie m)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO Movies(Title, Director) VALUES(@Title,@Director)";
            cmd.Parameters.AddWithValue("@Title", m.Title);
            
            cmd.Parameters.AddWithValue("@Director", m.Director);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //method: getAllMovies
        public Movies getAllMovies()
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT Id,Title,Director FROM Movies";
            List<Movie> ms = new List<Movie>();
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Movie m = new Movie(
                Convert.ToInt32(reader["Id"]),
                reader["Title"].ToString(),
                reader["Director"].ToString()
                );
                ms.Add(m);
            }
            Movies allMovies = new Movies(); // create an instance of the class assign the collection and return it
            allMovies.Items = ms;
            return allMovies;
        }
        //method: setMovieToEditMode - apply for the whole database
        public int setMovieToEditMode(List<Movie> movies, int id)
        {
            int editIndex = 0;
            foreach (Movie m in movies)
            {
                if (m.Id == id)
                {
                    m.IsEditable = true;
                    return editIndex;
                }
                editIndex++;
            }
            return -1;
        }

        //method: setAMovieToEditMode - apply for 1 movie
        public Movie setAMovieToEditMode(Movie m)
        {

            m.IsEditable = true;
            return m;
        }

        // method: updateMovie
        public void updateMovie(Movie movie)
        {
            //This method accepts updates with, or without, a description
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            if (movie.Description != null)
            {
                cmd.CommandText = "UPDATE Movies SET Title=@Title,Director=@Director,Description=@Description WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Description", movie.Description);
            }
            else
            {
                cmd.CommandText = "UPDATE Movies SET Title=@Title,Director=@Director WHERE Id=@Id";
            }
            
            cmd.Parameters.AddWithValue("@Title", movie.Title);
            cmd.Parameters.AddWithValue("@Director", movie.Director);
            cmd.Parameters.AddWithValue("@Id", movie.Id);

            con.Open();
            cmd.ExecuteNonQuery();
        }
        //method: deleteMovie
        public void deleteMovie(Movie movie)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "DELETE FROM Movies WHERE Id = @Id";
            cmd.Parameters.AddWithValue("Id", movie.Id);

            con.Open();
            cmd.ExecuteNonQuery();

        }


    }
}