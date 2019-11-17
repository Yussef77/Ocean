namespace Oceanware.Ocean.Blazor {

    using System;

    /// <summary>
    /// Class OceanAutoCompleteItemWrapper.
    /// </summary>
    /// <typeparam name="TItem">The type of the TItem.</typeparam>
    public class OceanAutoCompleteItemWrapper<TItem> {

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public String Id { get; }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <value>The item.</value>
        public TItem Item { get; }

        /// <summary>
        /// Gets or sets the selected item CSS class.
        /// </summary>
        /// <value>The selected item CSS class.</value>
        public String SelectedItemCssClass { get; set; } = "ocean-blazor-not-selected-search-result";

        /// <summary>
        /// Initializes a new instance of the <see cref="OceanAutoCompleteItemWrapper{TItem}"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="id">The identifier.</param>
        public OceanAutoCompleteItemWrapper(TItem item, String id) {
            this.Item = item;
            this.Id = id;
        }
    }
}
