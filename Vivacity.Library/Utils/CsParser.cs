using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Vivacity.Library.Utils
{
    /// <summary>
    /// C sharp file parser.
    /// </summary>
    public class CsParser : BaseParser
    {
        /// <summary>
        /// Class finder regex.
        /// </summary>
        public const string ObjectRegex = "(?<=(class|enum|interface|abastract)( +| ))[\\w]+";

        /// <summary>
        /// Namespace/package finder regex.
        /// </summary>
        public const string NamespaceRagex = "(?<=(namespace)( +| ))[.\\w]+";

        /// <summary>
        /// Aggregatoins finder regex
        /// </summary>
        public const string AggregationsRagex = "";

        /// <summary>
        /// The class regex.
        /// </summary>
        public const string InheritanceRegex = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="Cities.Library.Utils.CsParser"/> class.
        /// </summary>
        public CsParser() : base(ObjectRegex, NamespaceRagex, AggregationsRagex, InheritanceRegex)
        {
        }

        /// <summary>
        /// Gets the inheritance.
        /// </summary>
        /// <returns>The inheritance.</returns>
        /// <param name="fileString">File string.</param>
        public override string GetInheritance(string fileString)
        {
            IEnumerable<string> lines = fileString.Split('\n');
            lines = lines.Where(x => x.Contains("class", "enum", "struct"));
            string line = lines.FirstOrDefault();

            if (line == null)
                return null;

            if (line.Contains(":"))
            {
                int colonIndex = line.IndexOf(":");

                string classes = line.Substring(colonIndex + 1);

                int i = 0;

                for (; i < classes.Length; i++)
                {
                    if (classes[i] != ' ')
                    {
                        int x = 1;

                        while (classes.ElementAtOrDefault(i + x) != ',' && classes.ElementAtOrDefault(i + x) != '\n' && classes.Length > (i + x))
                        {
                            if (classes[i + x] == '<')
                                while (classes[i + x] != '>')
                                    x++;
                            x++;
                        }
                        
                        return new string(classes.Skip(i).Take(x).ToArray());
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets aggragations.
        /// </summary>
        /// <returns>The aggragations.</returns>
        /// <param name="fileString">File string.</param>
        public override List<string> GetAggragations(string fileString)
        { 
            fileString = RemoveComments(fileString);
            fileString = RemoveChars(fileString);
            fileString = RemoveStrings(fileString);
            
            IEnumerable<string> lines = fileString.Split('\n');
            lines = lines.Where(x => x.Contains("public", "private", "readonly", "static", "virtual", "override", "protected", "const"));
            lines = lines.Where(x => !x.Contains("class", "struct", "enum", "::", "internal"));
            lines = lines.Select(x => x.Remove("public", "private",  "readonly", "static", "virtual", "override", "protected", "const", "unsafe", "extern"));
            lines = lines.Select(x => x.Trim());
            lines = lines.Select(GetClassName).Where(x => x != null && x != "void");
      
            return lines.ToList();
        }

        /// <summary>
        /// Gets the name of the class.
        /// </summary>
        /// <returns>The class name.</returns>
        /// <param name="fileString">File string.</param>
        public override string GetClassName(string fileString)
        {
            int i = 0;

            for (; i < fileString.Length; i++)
            {
                if (fileString[i] == '[')
                {
                    while (fileString[i] != ']')
                        i++;
                    i++;
                }
                
                if (fileString[i] != ' ')
                {
                    int x = 1;

                    while (fileString.ElementAtOrDefault(i + x) != '\n' && fileString.ElementAtOrDefault(i + x) != ' ' && fileString.Count() > (i+x))
                    {
                        if (fileString[i + x] == '<')
                            while (fileString[i + x] != '>')
                                x++;
                        x++;
                    }
                    return new string(fileString.Skip(i).Take(x).ToArray());
                }
            }

            return "";
        }

        /// <summary>
        /// Removes the chars.
        /// </summary>
        /// <returns>The chars.</returns>
        /// <param name="value">Value.</param>
        public static string RemoveChars(string value)
        {
            bool hadInteration = false;

            int i = 0;

            do
            {
                hadInteration = false;

                for (; i < value.Length-1; i++)
                {
                    if (value[i] == '/' && value[1 + i] == '\'')
                    {
                        int x = 1;

                        while (value[i + x] != '\'' && value.Length > (x + i + 1))
                            x++;

                        hadInteration = true;

                        value = value.Remove(i, x+1);

                        break;
                    }
                }

            } while(hadInteration);

            return value;
        }

        /// <summary>
        /// Removes the comments.
        /// </summary>
        /// <returns>The comments.</returns>
        /// <param name="value">Value.</param>
        public static string RemoveComments(string value)
        {
            bool hadInteration = false;

            int i = 0;

            do
            {
                hadInteration = false;

                for (; i < value.Length-1; i++)
                {
                    if (value[i] == '/' && value[1 + i] == '/')
                    {
                        int x = 1;

                        while (value[i + x] != '\n' && value.Length > (x + i + 1))
                            x++;
                        
                        hadInteration = true;

                        value = value.Remove(i, x);

                        break;
                    } 
                    else if (value[i] == '/' && value[1 + i] == '*')
                    {
                        int x = 1;

                        while (value[i + x] != '*' || value[i + x + 1] != '/' )
                            x++;

                        hadInteration = true;

                        value = value.Remove(i, x);

                        break;
                    }
                }

            } while(hadInteration);

            return value;
        }


        /// <summary>
        /// Remores the string.
        /// </summary>
        /// <returns>The string.</returns>
        /// <param name="value">Value.</param>
        public static string RemoveStrings(string value)
        {
            bool hadInteration = false;

            int i = 0;

            do
            {
                hadInteration = false;

                for (; i < value.Length; i++)
                {
                    if (value.ElementAtOrDefault(i) == '"' && value.ElementAtOrDefault(i - 1) != '\\')
                    {
                        int x = 1;

                        while (value.ElementAtOrDefault(i + x) != '"' && value.ElementAtOrDefault(i + x - 1) != '\\' && value.Length > (x + i) )
                        {
                            x++;
                        }

                        hadInteration = true;

                        if(value.Length > 1+x+i)
                        {
                            value = value.Remove(i, x + 1);
                            break;
                        }
                    }
                    else if(value.ElementAtOrDefault(i) == '@')
                    {
                        int x = 2;

                        while (value.ElementAtOrDefault(i + x) != '"' && value.ElementAtOrDefault(i + x - 1) != '\"' && value.Length > (x + i) )
                        {
                            x++;
                        }

                        hadInteration = true;

                        if(value.Length > 1+x+i)
                        {
                            value = value.Remove(i, x + 1);
                            break;
                        }
                    }
                }

            } while(hadInteration);

            return value;

        }
    }
}
