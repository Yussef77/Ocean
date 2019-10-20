namespace BlazorServerSideApp.Model {

    using System;
    using Oceanware.Ocean.ValidationRules;

    public class Numerics {

        [CompareValueValidator(ComparisonType.GreaterThan, (Int16)0)]
        public Int16 Age { get; set; } = 32;

        public Int64 BigCount { get; set; } = 104587;

        public Int32 Count { get; set; } = 5000;

        [CompareValueValidator(ComparisonType.GreaterThanEqual, "0", ConvertToType.Decimal)]
        public Decimal ExtendedPrice { get; set; } = 1234.99m;

        [CompareValueValidator(ComparisonType.GreaterThan, (Int16)0, RequiredEntry.No)]
        public Int16? PossibleAge { get; set; }

        public Int64? PossibleBigCount { get; set; }

        public Int32? PossibleCount { get; set; }

        [CompareValueValidator(ComparisonType.GreaterThanEqual, "0", ConvertToType.Decimal, RequiredEntry.No)]
        public Decimal? PossibleExtendedPrice { get; set; }

        [CompareValueValidator(ComparisonType.GreaterThanEqual, 0f, RequiredEntry.No)]
        public Single? PossiblePrice { get; set; }

        [CompareValueValidator(ComparisonType.GreaterThanEqual, 0d, RequiredEntry.No)]
        public Double? PossibleRevenue { get; set; }

        [CompareValueValidator(ComparisonType.GreaterThanEqual, 0f)]
        public Single Price { get; set; } = 2.99f;

        [CompareValueValidator(ComparisonType.GreaterThanEqual, 0d)]
        public Double Revenue { get; set; } = 25987.87d;

        public Numerics() {
        }
    }
}
