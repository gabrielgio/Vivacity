using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Vivacity.Library.Parser.Directory
{
    public class DirectoryParser : IParser
    {
        private string _rootDirectory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vivacity.Library.Parser.Directory.DirectoryParser"/> class.
        /// </summary>
        /// <param name="rootDirctory">Root dirctory.</param>
        public DirectoryParser(string rootDirctory)
        {
            _rootDirectory = rootDirctory;

            if (!System.IO.Directory.Exists(_rootDirectory))
                throw new DirectoryNotFoundException(string.Format("Directory {0} does not exist", _rootDirectory));
        }

        /// <summary>
        /// Read all files of a repo.
        /// </summary>
        public IEnumerable<IFile> Read()
        {
            Stack<string> stack = new Stack<string>();

            stack.Push(_rootDirectory);

            while (stack.Count > 0)
            {
                string path = stack.Pop();

                System.IO.Directory.EnumerateDirectories(path).ToList().ForEach(stack.Push);


                foreach (string item in System.IO.Directory.EnumerateFiles(path))
                {
                    FileInfo fileInfo = new FileInfo(item);
                    yield return new DirectoryFile
                    {
                        Date = new DateTimeOffset(fileInfo.CreationTime),
                        AbsolutPath = item,
                        Path = item.Replace(_rootDirectory, string.Empty),
                        Revisions = (DateTime.Now - fileInfo.CreationTime).Days
                    };
                }
            }
        }
    }
}