using System;

namespace Vivacity.Library.Parser.GitHub
{
    /// <summary>
    /// Changes of the commit.
    /// </summary>
    public class Change
    {
        /// <summary>
        /// Gets or sets change type:
        /// - Added
        /// - Modified
        /// - Deleted
        /// </summary>
        /// <value>The type of the change.</value>
        public string ChangeType { get; set; }

        /// <summary>
        /// Gets or sets change date.
        /// </summary>
        /// <value>The date.</value>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Gets or sets the added lines.
        /// </summary>
        /// <value>The added lines.</value>
        public int AddedLines { get; set; }

        /// <summary>
        /// Gets or sets the deleted lines.
        /// </summary>
        /// <value>The deleted lines.</value>
        public int DeletedLines { get; set; }

        /// <summary>
        /// Gets or sets the total changes.
        /// </summary>
        /// <value>The total changes.</value>
        public int TotalChanges { get; set; }
    }
}

