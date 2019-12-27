using LabMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabGridMasterDetail
{
    class Movie
    {
        public string Name { get; set; }
        public int RottenTomatosScore { get; set; }
        public string Review { get; set; }

        public string MoviePicture { get; set; }

        public List<Actor> Actors{ get; set; }


        public Movie()
        {
            Actors = new List<Actor>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
