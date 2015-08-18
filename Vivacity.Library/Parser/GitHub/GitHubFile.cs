using System;
using System.Collections.Generic;
using System.Net;

namespace Vivacity.Library.Parser.GitHub
{
    public class GitHubFile : IFile
    {
        public string RawDataUrl { get; set; }

        public string Sha { get; set; }

        public DateTimeOffset Date { get; set; }

        public long Revisions{ get; set; }

        public string Path { get; set; }

        public List<Change> Changes;

        public GitHubFile()
        {
            Changes = new List<Change>();
        }

        public byte[] GetRawData()
        {
            WebClient web = new WebClient();
            return web.DownloadData(RawDataUrl);
        }
    }
}