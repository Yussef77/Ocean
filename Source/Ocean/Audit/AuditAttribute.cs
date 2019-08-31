namespace Oceanware.Ocean.Audit {

    using System;

    /// <summary>
    /// Class AuditAttribute, when applied to a business entity class property, that property will participate in audit message creation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class AuditAttribute : Attribute {

        /// <summary>
        /// Get the sort order that will be applied to this property when audit messages are created.  Default value is 999999.
        /// </summary>
        public Int32 AuditSequence { get; } = 999999;

        /// <summary>
        /// Gets or sets the skip audit. When <c>SkipAudit.Yes</c>, property will not be included in audit ouput.
        /// </summary>
        /// <value>The skip audit.</value>
        public SkipAudit SkipAudit { get; set; } = SkipAudit.No;

        /// <summary>
        /// AuditSequence, sort order will default to 999999 when using this default constructor.
        /// </summary>
        public AuditAttribute() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditAttribute"/> class.
        /// </summary>
        /// <param name="auditSequence">The audit sequence sort order.</param>
        public AuditAttribute(Int32 auditSequence) {
            this.AuditSequence = auditSequence;
        }
    }
}
