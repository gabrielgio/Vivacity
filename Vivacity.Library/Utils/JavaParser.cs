namespace Vivacity.Library.Utils
{
    public class JavaParser : BaseParser
    {
        /// <summary>
        /// The class regex.
        /// </summary>
        public const string ClassRegex = "(?<=(class|enum|interface|abastract)( +| ))[\\w]+";

        /// <summary>
        /// The namespace ragex.
        /// </summary>
        public const string NamespaceRagex = "(?<=(package)( +| ))[.\\w]+";

        /// <summary>
        /// The aggregations ragex.
        /// </summary>
        public const string AggregationsRagex = "";

        /// <summary>
        /// The class regex.
        /// </summary>
        public const string InheritanceRegex = "(?<=(extends)( +| ))[\\w]+";

        /// <summary>
        /// Initializes a new instance of the <see cref="Cities.Library.Utils.JavaParser"/> class.
        /// </summary>
        public JavaParser() : base(ClassRegex, NamespaceRagex, AggregationsRagex, InheritanceRegex)
        {
        }
    }
}

