using System;
using System.IO;

namespace Vivacity.Library.Parser.Directory
{
    /// <summary>
    /// IFile of Directory parse.
    /// </summary>
    public class DirectoryFile : IFile
    {
        /// <summary>
        /// Date of creation.
        /// </summary>
        /// <value>The date.</value>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Numbers of revisions.
        /// </summary>
        /// <value>The revisions.</value>
        public long Revisions { get; set; }

        /// <summary>
        /// Relative path of the file.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; }

        /// <summary>
        /// Absolut path of a file.
        /// </summary>
        /// <value>The absolut path.</value>
        public string AbsolutPath { get; set; }

        /// <summary>
        /// Gets the raw data from the file.
        /// </summary>
        /// <returns>The raw data.</returns>
        public byte[] GetRawData()
        {
            return File.ReadAllBytes(AbsolutPath);
        }
    }
}

