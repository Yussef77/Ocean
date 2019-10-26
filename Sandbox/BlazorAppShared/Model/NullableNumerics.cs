namespace BlazorAppShared.Model {

    using System;
    using Oceanware.Ocean.ValidationRules;

    public class NullableNumerics {

        public Int64? BigCount { get; set; }

        public Int32? Count { get; set; }

        [CompareValueValidator(ComparisonType.GreaterThanEqual, "0", ConvertToType.Decimal, RequiredEntry.No)]
        public Decimal? ExtendedPrice { get; set; }

        [CompareValueValidator(ComparisonType.GreaterThanEqual, 0f, RequiredEntry.No)]
        public Single? Price { get; set; }

        [CompareValueValidator(ComparisonType.GreaterThanEqual, 0d, RequiredEntry.No)]
        public Double? Revenue { get; set; }

        public NullableNumerics() {
        }
    }
}
