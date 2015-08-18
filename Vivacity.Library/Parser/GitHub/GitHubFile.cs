using System;
using System.Collections.Generic;
using System.Net;

namespace Vivacity.Library.Parser.GitHub
{
    public class GitHubFile : IFile
    {
        /// <summary>
        /// Gets or sets the raw data URL.
        /// </summary>
        /// <value>The raw data URL.</value>
        public string RawDataUrl { get; set; }

        /// <summary>
        /// Gets or sets the sha.
        /// </summary>
        /// <value>The sha.</value>
        public string Sha { get; set; }

        /// <summary>
        /// Date of creation.
        /// </summary>
        /// <value>The date.</value>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Numbers of revisions.
        /// </summary>
        /// <value>The revisions.</value>
        public long Revisions{ get; set; }

        /// <summary>
        /// Relative path of the file.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; }

        /// <summary>
        /// The changes.
        /// </summary>
        public List<Change> Changes;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vivacity.Library.Parser.GitHub.GitHubFile"/> class.
        /// </summary>
        public GitHubFile()
        {
            Changes = new List<Change>();
        }

        /// <summary>
        /// Gets the raw data from the file.
        /// </summary>
        /// <returns>The raw data.</returns>
        public byte[] GetRawData()
        {
            WebClient web = new WebClient();
            return web.DownloadData(RawDataUrl);
        }
    }
}