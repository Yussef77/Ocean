namespace BlazorAppShared.Model {

    using System;

    public class CityStateZip {
        private string _city;

        public String City {
            get => _city;
            set {
                this.SearchKey = value.ToLower();
                _city = value;
            }
        }

        public String SearchKey { get; set; }

        public String State { get; set; }

        public String Zip { get; set; }

        public CityStateZip() {
        }
    }
}
