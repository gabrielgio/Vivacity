using System;

namespace Vivacity.Library.Parser
{
    /// <summary>
    /// Generic representation of a file from any type o repo.
    /// </summary>
    public interface IFile
    {
        /// <summary>
        /// Date of creation.
        /// </summary>
        DateTimeOffset Date { get; set; }

        /// <summary>
        /// Numbers of revisions.
        /// </summary>
        /// <value>The revisions.</value>
        long Revisions { get; set; }

        /// <summary>
        /// Relative path of the file.
        /// </summary>
        /// <value>The path.</value>
        string Path { get; set; }

        /// <summary>
        /// Gets the raw data from the file.
        /// </summary>
        /// <returns>The raw data.</returns>
        byte[] GetRawData();
    }
}

