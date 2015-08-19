using System;
using System.Collections.Generic;

namespace Vivacity.Library.Model
{
    public class City
    {
        public List<Building> Buildings { get; set; }

        public List<Street> Streets { get; set; }


        public City()
        {
            Buildings = new List<Building>();
            Streets = new List<Street>();
        }
    }
}

