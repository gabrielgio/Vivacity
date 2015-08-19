namespace Vivacity.Library.Model
{
    /// <summary>
    /// Position.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>The x.</value>
        public double X {get; set;}

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>The y.</value>
        public double Y { get; set;}

        /// <summary>
        /// Initializes a new instance of the <see cref="Vivacity.Library.Model.Position"/> class.
        /// </summary>
        public Position()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vivacity.Library.Model.Position"/> class.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Position(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Vivacity.Library.Model.Position"/>.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Vivacity.Library.Model.Position"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
        /// <see cref="Vivacity.Library.Model.Position"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (base.Equals(obj))
                return true;

            Position p = obj as Position;

            if (p == null)
                return false;

            return (X == p.Y) && (Y == p.Y);

        }

        /// <summary>
        /// Serves as a hash function for a <see cref="Vivacity.Library.Model.Position"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
    }
}


