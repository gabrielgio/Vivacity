using System;
using Vivacity.Library.Parser;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Vivacity.Library.Utils
{
    /// <summary>
    /// Excecute regex string to get informations from a file.
    /// </summary>
    public class BaseParser
    {
        private string _objectRegex;

        private string _namespaceRagex;

        private string _aggregationsRegex;

        private string _inheritanceRegex;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cities.Library.Utils.BaseParser"/> class.
        /// </summary>
        /// <param name="objectRegex">Regex to find name of the class.</param>
        /// <param name="namespaceRegex">Regex to find namespace</param>
        /// <param name="agregattionsRegex">Regex to find aggragations.</param>
        public BaseParser(string objectRegex, string namespaceRegex, string agregattionsRegex, string inheritance)
        {
            _objectRegex = objectRegex;
            _namespaceRagex = namespaceRegex;
            _aggregationsRegex = agregattionsRegex;
            _inheritanceRegex = inheritance;
        }

        /// <summary>
        /// Parse the specified file.
        /// </summary>
        /// <returns>Result</returns>
        /// <param name="fileString">String of the file.</param>
        public virtual ParseResult Parse(string fileString)
        {
            return new ParseResult
            {
                Type = GetClassName(fileString),
                Namespace = GetNamespace(fileString),
                Aggregations = GetAggragations(fileString),
                Inheritance = GetInheritance(fileString)
            };
        }

        /// <summary>
        /// Gets the aggragations.
        /// </summary>
        /// <returns>The aggragations.</returns>
        /// <param name="fileString">File string.</param>
        public virtual List<string> GetAggragations(string fileString)
        {

            Regex regex = new Regex(_aggregationsRegex);

            Match match = regex.Match(fileString);

            List<string> results = new List<string>();

            if (match.Captures.Count > 0)
                foreach (Capture item in regex.Match(fileString).Captures)
                    results.Add(item.Value);
            
            return results;
        }

        /// <summary>
        /// Parse the specified file.
        /// </summary>
        /// <param name="file">File.</param>
        public virtual ParseResult Parse(IFile file)
        {
            return Parse(System.Text.Encoding.UTF8.GetString(file.GetRawData()));
        }

        /// <summary>
        /// Gets the namespace.
        /// </summary>
        /// <returns>The namespace.</returns>
        /// <param name="fileString">File string.</param>
        public virtual string GetNamespace(string fileString)
        {
            Regex regex = new Regex(_namespaceRagex);

            Match match = regex.Match(fileString);

            if (match.Captures.Count > 0)
            {
                return regex.Match(fileString).Captures[0].Value;
            }

            return String.Empty;
        }

        /// <summary>
        /// Gets the name of the class.
        /// </summary>
        /// <returns>The class name.</returns>
        /// <param name="fileString">File string.</param>
        public virtual string GetClassName(string fileString)
        {
            Regex regex = new Regex(_objectRegex);

            Match match = regex.Match(fileString);

            if (match.Captures.Count > 0)
            {
                return regex.Match(fileString).Captures[0].Value;
            }

            return String.Empty;
        }

        /// <summary>
        /// Gets the inheritance.
        /// </summary>
        /// <returns>The inheritance.</returns>
        /// <param name="fileString">File string.</param>
        public virtual string GetInheritance(string fileString)
        {
            Regex regex = new Regex(_inheritanceRegex);

            Match match = regex.Match(fileString);

            if (match.Captures.Count > 0)
            {
                return regex.Match(fileString).Captures[0].Value;
            }

            return String.Empty;
        }
    }
}

