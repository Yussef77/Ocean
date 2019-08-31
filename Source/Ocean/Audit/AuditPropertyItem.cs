namespace Oceanware.Ocean.Audit {

    using System;
    using System.ComponentModel;

    /// <summary>
    /// Class AuditPropertyItem.
    /// </summary>
    public class AuditPropertyItem {

        /// <summary>
        /// Gets the audit format.
        /// </summary>
        /// <value>The audit format.</value>
        public AuditFormat AuditFormat { get; }

        /// <summary>
        /// Gets the audit sequence.
        /// </summary>
        /// <value>The audit sequence.</value>
        public Int32 AuditSequence { get; }

        /// <summary>
        /// Gets the name of the friendly.
        /// </summary>
        /// <value>The name of the friendly.</value>
        public String FriendlyName { get; }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public String PropertyName { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public String Value { get; }

        /// <summary>Initializes a new instance of the <see cref="T:Oceanware.Ocean.Audit.AuditPropertyItem"/> class.</summary>
        /// <param name="auditSequence">The audit sequence.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="friendlyName">The friendly property name.</param>
        /// <param name="value">The property value.</param>
        /// <param name="auditFormat">The audit format.</param>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value auditFormat is not defined.</exception>
        public AuditPropertyItem(Int32 auditSequence, String propertyName, String friendlyName, String value, AuditFormat auditFormat) {
            if (!Enum.IsDefined(typeof(AuditFormat), auditFormat)) {
                throw new InvalidEnumArgumentException(nameof(auditFormat), (Int32)auditFormat, typeof(AuditFormat));
            }

            if (value == null) {
                value = String.Empty;
            }

            this.AuditSequence = auditSequence;
            this.PropertyName = propertyName;
            this.FriendlyName = friendlyName;
            this.Value = value;
            this.AuditFormat = auditFormat;
        }

        /// <summary>
        /// Returns a <see cref="String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="String"/> that represents this instance.</returns>
        public override String ToString() {
            if (this.AuditFormat == AuditFormat.Normal) {
                return this.FriendlyName.Length == 0 ? $"{this.PropertyName} = {this.Value}" : $"{this.FriendlyName} ( {this.PropertyName} ) = {this.Value}";
            }
            return $"{this.PropertyName} = {this.Value}";
        }
    }
}
