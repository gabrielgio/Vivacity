using System;
using Newtonsoft.Json;
using System.IO;
using Cities.Library.Model;
using System.Collections.Generic;

namespace Cities.Library.Parser.Json
{
    /// <summary>
    /// Json parser.
    /// </summary>
    public class JsonParser : IParser
    {
        //file name/path
        private string _file;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cities.Library.Parser.JsonParser`1"/> class.
        /// </summary>
        /// <param name="file">File name</param>
        public JsonParser (string file)
        {
            _file = file;
        }


        /// <summary>
        /// Read from a json file
        /// </summary>
        public IEnumerable<IFile> Read ()
        {
            if (!System.IO.File.Exists (_file)) 
            {
                return new List<IFile> ();
            } 
            else 
            {
                string json = System.IO.File.ReadAllText (_file);
                return JsonConvert.DeserializeObject <IEnumerable<IFile>>(json);
            }
        }
    }
}
