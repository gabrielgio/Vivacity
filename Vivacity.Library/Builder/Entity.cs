using System;
using System.Collections.Generic;
using Vivacity.Library.Utils;

namespace Vivacity.Library.Builder
{
    /// <summary>
    /// Represent an object and its repo informations, information needed from a class of any language to create a building
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Name of the object
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Namespace/package of the object
        /// </summary>
        /// <value>The namespace.</value>
        public string Namespace { get; set; }

        /// <summary>
        /// date when the files of the files was create
        /// </summary>
        /// <value>The date.</value>
        public DateTime Date { get; set; }


        /// <summary>
        /// date when the files of the files was create
        /// </summary>
        /// <value>The date.</value>
        public long Revisions { get; set; }

        /// <summary>
        /// Inheritence.
        /// </summary>
        public String Inheritance { get; set; }

        /// <summary>
        /// Aggragations
        /// </summary>
        /// <value>The aggregations.</value>
        public IEnumerable<String> Aggregations { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cities.Library.Builder.Entity"/> class.
        /// </summary>
        public Entity()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cities.Library.Builder.Entity"/> class.
        /// </summary>
        /// <param name="result">Result.</param>
        /// <param name="date">Date.</param>
        /// <param name="revisions">Revisions.</param>
        public  Entity (ParseResult result, DateTime date, int revisions)
        {
            Type = result.Type;
            Namespace = result.Namespace;
            Inheritance = result.Inheritance;
            Aggregations = result.Aggregations;
            Revisions = revisions;
            Date = date;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Cities.Library.Builder.Entity"/>.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Cities.Library.Builder.Entity"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
        /// <see cref="Cities.Library.Builder.Entity"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;
         
            Entity p = obj as Entity;
            if (p == null)
                return false;
            
            return (Type == p.Type) && (Namespace == p.Namespace) && (Inheritance == p.Inheritance) && (Revisions == p.Revisions);
        }

        /// <param name="entity1">Entity1.</param>
        /// <param name="entity2">Entity2.</param>
        public static bool operator == (Entity entity1, Entity entity2)
        {
            if (object.ReferenceEquals(entity1, null))
            {
                return object.ReferenceEquals(entity2, null);
            }

            return entity1.Equals(entity2);
        }

        /// <param name="entity1">Entity1.</param>
        /// <param name="entity2">Entity2.</param>
        public static bool operator !=(Entity entity1, Entity entity2)
        {
            return !(entity1 == entity2);
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="Cities.Library.Builder.Entity"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
            return Type.GetHashCode() ^ Namespace.GetHashCode() ^ Date.GetHashCode() ^ Inheritance.GetHashCode() ^ Aggregations.GetHashCode();
        }
    }
}
