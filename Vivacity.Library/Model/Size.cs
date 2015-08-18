using System;
using Newtonsoft.Json;

namespace Vivacity.Library.Model
{
    /// <summary>
    /// Represent size of an object
    /// </summary>
    public class Size
    {
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public double Width { get; set;}

        /// <summary>
        /// Gets or sets the heigth.
        /// </summary>
        /// <value>The heigth.</value>
        public double Heigth { get; set;}

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>The length.</value>
        public double Length { get; set;}

        /// <summary>
        /// Gets the ratio.
        /// </summary>
        /// <value>The ratio.</value>
        [JsonIgnore]
        public double Ratio { get { return Width / Length; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cities.Library.Model.Size"/> class.
        /// </summary>
        public Size()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cities.Library.Model.Size"/> class.
        /// </summary>
        /// <param name="width">Width.</param>
        /// <param name="heigth">Heigth.</param>
        /// <param name="length">Length.</param>
        public Size(double width, double heigth, double length)
        {
            Width = width;
            Heigth = heigth;
            Length = length;
        }

    }
}

