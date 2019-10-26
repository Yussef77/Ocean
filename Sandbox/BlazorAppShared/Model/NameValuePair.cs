namespace BlazorAppShared.Model {

    using System;

    public class NameValuePair {

        public string Name { get; }

        public string Value { get; }

        public NameValuePair(String name, Object value) {
            this.Name = name;
            if (value == null) {
                this.Value = "null";
            } else {
                this.Value = value.ToString();
                if (String.IsNullOrWhiteSpace(this.Value)) {
                    this.Value = "empty string";
                }
            }
        }
    }
}
