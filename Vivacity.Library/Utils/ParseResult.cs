using System.Collections.Generic;

namespace Vivacity.Library.Utils
{
    /// <summary>
    /// Result of a file parse.
    /// </summary>
    public class ParseResult
    {
        /// <summary>
        /// Name of the class
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; set; }

        /// <summary>
        /// Namespace/package
        /// </summary>
        /// <value>The namespace.</value>
        public string Namespace { get; set; }

        /// <summary>
        /// Inheritance.
        /// </summary>
        /// <value>The inheritance.</value>
        public string Inheritance { get; set; }

        /// <summary>
        /// Agrregations.
        /// </summary>
        public IEnumerable<string> Aggregations;
    }
}

