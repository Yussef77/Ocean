namespace Oceanware.Ocean.Blazor {

    using System;

    public class OceanAutoCompleteItemWrapper<TItem> {

        public String Id { get; }

        public TItem Item { get; }

        public String SelectedItemCssClass { get; set; }

        public OceanAutoCompleteItemWrapper(TItem item, String id) {
            this.Item = item;
            this.Id = id;
        }
    }
}
