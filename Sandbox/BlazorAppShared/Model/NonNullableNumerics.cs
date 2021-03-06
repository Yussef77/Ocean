﻿namespace BlazorAppShared.Model {

    using System;
    using Oceanware.Ocean.ValidationRules;

    public class NonNullableNumerics {

        public Int64 BigCount { get; set; } = 104587;

        public Int32 Count { get; set; } = 5000;

        [CompareValueValidator(ComparisonType.GreaterThanEqual, "0", ConvertToType.Decimal)]
        public Decimal ExtendedPrice { get; set; } = 1234.99m;

        [CompareValueValidator(ComparisonType.GreaterThanEqual, 0f)]
        public Single Price { get; set; } = 2.99f;

        [CompareValueValidator(ComparisonType.GreaterThanEqual, 0d)]
        public Double Revenue { get; set; } = 25987.87d;

        public NonNullableNumerics() {
        }
    }
}
