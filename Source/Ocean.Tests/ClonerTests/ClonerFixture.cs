namespace Ocean.Tests.ClonerTests {

    using System;
    using Oceanware.Ocean.Infrastructure;
    using Xunit;

    public class ClonerFixture {

        [Fact]
        public void WhenClonerDeepCopiesAnObjectTheReturnedObjectHasSamePropertyValuesButIsNotTheSameReferenceObject() {
            // arrange
            var sut = new Customer { Count = -1, DateHired = DateTime.Now, FirstName = "Oceanware" };

            // act
            var sutDeepCopy = Cloner.DeepCopy<Customer>(sut);

            // assert
            Assert.False(ReferenceEquals(sut, sutDeepCopy));
            Assert.Equal(sut.FirstName, sutDeepCopy.FirstName);
            Assert.Equal(sut.Count, sutDeepCopy.Count);
            Assert.Equal(sut.DateHired, sutDeepCopy.DateHired);
        }

        [Serializable]
        class Customer {
            public Int32 Count { get; set; }
            public DateTime DateHired { get; set; }
            public String FirstName { get; set; }

            public Customer() {
            }
        }
    }
}
