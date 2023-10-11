using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COMP235MVCDemo.Models
{
    public class Movies
    {
        public List<Movie> Items { get; set; }
        public Movies() { }

        public int EditIndex { get; set; }
    }
}