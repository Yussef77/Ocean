namespace Oceanware.Ocean.Audit {

    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class PropertyNameComparer.
    /// Derives from the <see cref="System.Collections.Generic.IComparer{Oceanware.Ocean.Audit.AuditPropertyItem}" />
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IComparer{Oceanware.Ocean.Audit.AuditPropertyItem}" />
    public class PropertyNameComparer : IComparer<AuditPropertyItem> {

        /// <summary>
        /// Compares the two <c>SortablePropertyBasketItem</c> sorting by <c>PropertyName</c>.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>Int32 representing the compare operation.</returns>
        public Int32 Compare(AuditPropertyItem x, AuditPropertyItem y) {
            if (x == null) {
                if (y == null) {
                    return 0;
                } else {
                    return -1;
                }
            } else {
                if (y == null) {
                    return 1;
                } else {
                    return String.CompareOrdinal(x.PropertyName, y.PropertyName);
                }
            }
        }
    }
}
