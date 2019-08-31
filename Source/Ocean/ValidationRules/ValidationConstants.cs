namespace Oceanware.Ocean.ValidationRules {

    using System;

    /// <summary>Class ValidationConstants.</summary>
    public class ValidationConstants {

        /// <summary>RuleSet Delete</summary>
        public const String Delete = "Delete";

        /// <summary>RuleSet Insert</summary>
        public const String Insert = "Insert";

        /// <summary>RuleSet Insert or Update </summary>
        public const String InsertUpdate = "Insert|Update";

        /// <summary>RuleSet Insert, Delete, or Update </summary>
        public const String InsertDeleteUpdate = "Insert|Delete|Update";
        
        /// <summary>RuleSet delimiter, value is pipe symbol</summary>
        public const String RuleSetDelimiter = "|";

        /// <summary>RuleSet Update</summary>
        public const String Update = "Update";

        /// <summary>RuleSet Update or Delete</summary>
        public const String UpdateDelete = "Update|Delete";

    }
}
