namespace Oceanware.Ocean.SampleData {

    using System;

    /// <summary>
    /// Class CompanyItem.
    /// </summary>
    public class CompanyItem {

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        public String Category { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>The name of the company.</value>
        public String CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public String Description { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public String Url { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyItem"/> class.
        /// </summary>
        public CompanyItem() {
        }
    }
}
