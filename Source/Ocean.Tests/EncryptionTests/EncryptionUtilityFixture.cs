namespace Ocean.Tests.EncryptionTests {

    using System;
    using Oceanware.Ocean.Encryption;
    using Xunit;

    public class EncryptionUtilityFixture {

        [Fact]
        public void AfterEncryptingAStringItCanBeDecrypted() {
            // arrange
            const String ExpectedResult = "This is my test string";
            const String Key = "aslk;djflkasdjflksdajflksjadflkjasdklfjklsadfjlkasdjflsadjfklasdjfljsadklf";
            var encryptionUtility = new EncryptionUtility();

            // act
            var encryptedText = encryptionUtility.Encrypt(ExpectedResult, Key);
            var decryptedText = encryptionUtility.Decrypt(encryptedText, Key);

            // assert
            Assert.Equal(decryptedText, ExpectedResult);
        }
    }
}
