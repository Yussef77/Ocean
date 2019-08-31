namespace Oceanware.Ocean.Audit {

    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class AuditSequencePropertyNameComparer.
    /// Derives from the <see cref="System.Collections.Generic.IComparer{Oceanware.Ocean.Audit.AuditPropertyItem}" />
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IComparer{Oceanware.Ocean.Audit.AuditPropertyItem}" />
    public class AuditSequencePropertyNameComparer : IComparer<AuditPropertyItem> {
        const String NineZeros = "000000000";

        /// <summary>
        /// Compares the two <c>SortablePropertyBasketItem</c> sorting by <c>AuditSequence</c> and then <c>PropertyName</c>.
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
                    var xCompareValue = String.Concat(x.AuditSequence.ToString(NineZeros), x.PropertyName);
                    var yCompareValue = String.Concat(y.AuditSequence.ToString(NineZeros), y.PropertyName);
                    return String.CompareOrdinal(xCompareValue, yCompareValue);
                }
            }
        }
    }
}
