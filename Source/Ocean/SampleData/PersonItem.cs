namespace Oceanware.Ocean.SampleData {

    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Class PersonItem.
    /// </summary>
    public class PersonItem {

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        [JsonProperty("address")]
        public String Address { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        [JsonProperty("city")]
        public String City { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>The name of the company.</value>
        [JsonProperty("companyName")]
        public String CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the county.
        /// </summary>
        /// <value>The county.</value>
        [JsonProperty("county")]
        public String County { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [JsonProperty("email")]
        public String Email { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [JsonProperty("firstName")]
        public String FirstName { get; set; }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>The full name.</value>
        [JsonIgnore]
        public String FullName { get { return $"{this.FirstName} {this.LastName}"; } }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        [JsonProperty("lastName")]
        public String LastName { get; set; }

        /// <summary>
        /// Gets or sets the phone1.
        /// </summary>
        /// <value>The phone1.</value>
        [JsonProperty("phone1")]
        public String Phone1 { get; set; }

        /// <summary>
        /// Gets or sets the phone2.
        /// </summary>
        /// <value>The phone2.</value>
        [JsonProperty("phone2")]
        public String Phone2 { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        [JsonProperty("state")]
        public String State { get; set; }

        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>The website.</value>
        [JsonProperty("web")]
        public String Website { get; set; }

        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>The zip.</value>
        [JsonProperty("zip")]
        public String Zip { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonItem"/> class.
        /// </summary>
        public PersonItem() {
        }
    }
}
