namespace Ocean.Tests.DataGeneratorTests {

    using Oceanware.Ocean.SampleData;
    using Xunit;

    public class DataGeneratorFixture {

        [Fact]
        public void GetSinglePersonItem() {
            // arrange
            var sampleDataGenerator = new DataGenerator();

            // act
            var result = sampleDataGenerator.GetPersonItem();

            // assert
            Assert.IsType<PersonItem>(result);
        }

        [Fact]
        public void PersonItemListCreatedFromResource() {
            // arrange
            var sampleDataGenerator = new DataGenerator();

            // act
            var results = sampleDataGenerator.GetPersonItems();

            // assert
            Assert.True(results.Count == 500);
        }
    }
}
