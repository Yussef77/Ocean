namespace Oceanware.Ocean.Tests.AuditTests {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Oceanware.Ocean.Audit;
    using Xunit;

    public class AuditFixture {

        [Fact]
        public void WhenAuditToIDictionaryAuditFormatNormalReturnExpectedResult() {
            // arrange
            var sut = new Customer { Count = -1, IsActive = true, FirstName = "Oceanware" };
            const Int32 ExpectedDictionaryCount = 3;

            // act
            var result = AuditMessageFactory.AuditToIDictionary(sut, IncludeAllProperties.No, SortOption.AuditSequencePropertyName, auditFormat: AuditFormat.Normal);
            var stringResult = DictionaryToString(result);

            // assert
            Assert.True(result.Count == ExpectedDictionaryCount);
            Assert.True("FirstName = Oceanware, Count = -1, IsActive = True" == stringResult);
        }

        [Fact]
        public void WhenAuditToIDictionaryIncludeAllPropertiesYesIncludeErrorProperty() {
            // arrange
            var sut = new Customer { Count = -1, IsActive = true, FirstName = "Oceanware" };
            const Int32 ExpectedDictionaryCount = 4;

            // act
            var result = AuditMessageFactory.AuditToIDictionary(sut, IncludeAllProperties.Yes, SortOption.AuditSequencePropertyName);
            var stringResult = DictionaryToString(result);

            // assert
            Assert.True(result.Count == ExpectedDictionaryCount);
            Assert.True("FirstName = Oceanware, Count = -1, IsActive = True, Error = Problem" == stringResult);
        }

        [Fact]
        public void WhenAuditToIDictionarySortOptionPropertyNameReturnExpectedResult() {
            // arrange
            var sut = new Customer { Count = -1, IsActive = true, FirstName = "Oceanware" };
            const Int32 ExpectedDictionaryCount = 3;

            // act
            var result = AuditMessageFactory.AuditToIDictionary(sut, IncludeAllProperties.No, SortOption.PropertyName);
            var stringResult = DictionaryToString(result);

            // assert
            Assert.True(result.Count == ExpectedDictionaryCount);
            Assert.True("Count = -1, FirstName = Oceanware, IsActive = True" == stringResult);
        }

        [Fact]
        public void WhenAuditToStringAuditFormatNormalReturnExpectedResult() {
            // arrange
            var sut = new Customer { Count = -1, IsActive = true, FirstName = "Oceanware" };

            // act
            var result = AuditMessageFactory.AuditToString(sut, IncludeAllProperties.No, SortOption.AuditSequencePropertyName, auditFormat: AuditFormat.Normal);

            // assert
            Assert.Equal("First Name ( FirstName ) = Oceanware, Count ( Count ) = -1, Is Active ( IsActive ) = True", result);
        }

        [Fact]
        public void WhenAuditToStringIncludeAllPropertiesYesIncludeErrorProperty() {
            // arrange
            var sut = new Customer { Count = -1, IsActive = true, FirstName = "Oceanware" };

            // act
            var result = AuditMessageFactory.AuditToString(sut, IncludeAllProperties.Yes, SortOption.AuditSequencePropertyName);

            // assert
            Assert.Equal("FirstName = Oceanware, Count = -1, IsActive = True, Error = Problem", result);
        }

        [Fact]
        public void WhenAuditToStringSortOptionPropertyNameReturnExpectedResult() {
            // arrange
            var sut = new Customer { Count = -1, IsActive = true, FirstName = "Oceanware" };

            // act
            var result = AuditMessageFactory.AuditToString(sut, IncludeAllProperties.No, SortOption.PropertyName);

            // assert
            Assert.Equal("Count = -1, FirstName = Oceanware, IsActive = True", result);
        }

        String DictionaryToString(IDictionary<String, String> dictionary) {
            var stringArray = dictionary.Select(x => $"{x.Key} = {x.Value}").ToArray();
            var stringResult = String.Join(", ", stringArray);
            return stringResult;
        }

        [Serializable]
        class Customer {

            [Audit(2)]
            public Int32 Count { get; set; }

            public String Error { get; }

            [Audit(1)]
            public String FirstName { get; set; }

            [Audit(3)]
            public Boolean IsActive { get; set; }

            [Audit(SkipAudit = SkipAudit.Yes)]
            public String SSN { get; set; }

            public Customer() {
                this.Error = "Problem";
            }
        }
    }
}
