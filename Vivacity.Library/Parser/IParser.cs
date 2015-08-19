using System.Collections.Generic;
using Vivacity.Library.Parser;

namespace Vivacity.Library.Parser
{
    /// <summary>
    /// Parser.
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Read and get all files of a repo.
        /// </summary>
        IEnumerable<IFile> Read();
    }
}