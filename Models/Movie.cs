using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COMP235MVCDemo.Models
{
    
    public class Movie
    {
        public Movie() { }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }


        public string Description { get; set; }
        public bool IsEditable { get; set; }
        public Movie(int id, string title, string director)
        {
            Id = id;
            Title = title;
            Director = director;
            
        }
        public Movie(int id, string title, string director, string description)
        {
            Id = id;
            Title = title;
            Director = director;
            Description = description;
        }


    }
}