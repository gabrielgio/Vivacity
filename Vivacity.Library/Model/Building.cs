using System;

namespace Vivacity.Library.Model
{
    /// <summary>
    /// Parallelepiped that represent a building
    /// </summary>
    public class Building
    {
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        public Position Position { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        public Size Size{ get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cities.Library.Model.Building"/> class.
        /// </summary>
        public Building()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cities.Library.Model.Building"/> class.
        /// </summary>
        /// <param name="position">Position.</param>
        /// <param name="size">Size.</param>
        public Building(Position position, Size size)
        {
            Position = position;
            Size = size;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cities.Library.Model.Building"/> class.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <param name="length">Length.</param>
        public Building(double x, double y, double width, double height, double length)
        {
            Position = new Position(x, y);
            Size = new Size(width, height, length);
        }
    }
}

