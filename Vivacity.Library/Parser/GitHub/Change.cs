using System;

namespace Vivacity.Library.Parser.GitHub
{
    public class Change
    {
        public string ChangeType
        {
            get;
            set;
        }

        public DateTimeOffset Date
        {
            get;
            set;
        }

        public int AddedLines
        {
            get;
            set;
        }

        public int DeletedLines
        {
            get;
            set;
        }

        public int TotalChanges
        {
            get;
            set;
        }   
    }
}

