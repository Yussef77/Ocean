namespace Oceanware.Ocean.SampleData {

    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Class AddressItem.
    /// </summary>
    public class AddressItem {

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
        /// Gets or sets the county.
        /// </summary>
        /// <value>The county.</value>
        [JsonProperty("county")]
        public String County { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        [JsonProperty("state")]
        public String State { get; set; }

        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>The zip.</value>
        [JsonProperty("zip")]
        public String Zip { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressItem"/> class.
        /// </summary>
        public AddressItem() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressItem"/> class.
        /// </summary>
        /// <param name="personItem">The person item.</param>
        /// <exception cref="ArgumentNullException">Thrown when personItem is null.</exception>
        public AddressItem(PersonItem personItem) {
            if (personItem is null) {
                throw new ArgumentNullException(nameof(personItem));
            }

            this.Address = personItem.Address;
            this.City = personItem.City;
            this.County = personItem.County;
            this.State = personItem.State;
            this.Zip = personItem.Zip;
        }
    }
}
