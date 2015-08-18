using System.Collections.Generic;

namespace Vivacity.Library.Model
{
    /// <summary>
    /// City's layout.
    /// </summary>
    public class City
    {
        /// <summary>
        /// Gets or sets the buildings dimension.
        /// </summary>
        /// <value>The buildings.</value>
        public List<Building> Buildings { get; set; }

        /// <summary>
        /// Gets or sets the streets position.
        /// </summary>
        /// <value>The streets.</value>
        public List<Street> Streets { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vivacity.Library.Model.City"/> class.
        /// </summary>
        public City()
        {
            Buildings = new List<Building>();
            Streets = new List<Street>();
        }
    }
}

