namespace Vivacity.Library.Utils
{
    /// <summary>
    /// Unicon file parser.
    /// </summary>
    public class IcnParser : BaseParser
    {
        /// <summary>
        /// Object finder regex.
        /// </summary>
        public const string ObjectRegex = "(?<=(class|enum|interface|abastract)( +))[\\w]+";

        /// <summary>
        /// Namespace/package finder regex.
        /// </summary>
        public const string NamespaceRagex = "(?<=(package)( +))[\\w]+";

        /// <summary>
        /// Aggragations finder regex.
        /// </summary>
        public const string AggregationsRagex = "";

        /// <summary>
        /// The class regex.
        /// </summary>
        public const string InheritanceRegex = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="Cities.Library.Utils.IcnParser"/> class.
        /// </summary>
        public IcnParser() : base(ObjectRegex, NamespaceRagex, AggregationsRagex, InheritanceRegex)
        {

        }
    }

}

