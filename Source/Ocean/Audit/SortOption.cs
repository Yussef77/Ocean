namespace Oceanware.Ocean.Audit {

    /// <summary>
    /// Represents the values for the enum SortOption.
    /// </summary>
    public enum SortOption {

        /// <summary>
        /// Sort by property name
        /// </summary>
        PropertyName,

        /// <summary>
        /// Sort by audit sequence and then property name
        /// </summary>
        AuditSequencePropertyName,

        /// <summary>
        /// Do not sort
        /// </summary>
        None
    }
}
