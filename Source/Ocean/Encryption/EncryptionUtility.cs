namespace Oceanware.Ocean.Encryption {

    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Class EncryptionUtility.
    /// </summary>
    public class EncryptionUtility {

        /// <summary>
        /// Initializes a new instance of the <see cref="EncryptionUtility"/> class.
        /// </summary>
        public EncryptionUtility() {
        }

        /// <summary>
        /// Decrypts the specified encrypted text.
        /// </summary>
        /// <param name="encryptedText">The encrypted text.</param>
        /// <param name="key">The key.</param>
        /// <returns>String.</returns>
        /// <exception cref="ArgumentException">Thrown when encryptedText was null, empty, or white space.</exception>
        /// <exception cref="ArgumentException">Thrown when key was null, empty, or white space.</exception>
        /// <exception cref="ArgumentException">Thrown when unable to create AES crypto object.</exception>
        public String Decrypt(String encryptedText, String key) {
            if (String.IsNullOrEmpty(encryptedText)) {
                throw new ArgumentException(Strings.TheEncryptedTextMustHaveAValue, nameof(encryptedText));
            }
            if (String.IsNullOrEmpty(key)) {
                throw new ArgumentException(Strings.KeyMustHaveAValue, nameof(key));
            }

            var combined = Convert.FromBase64String(encryptedText);
            var buffer = new Byte[combined.Length];

            using (var hash = new SHA512CryptoServiceProvider()) {
                var aesKey = new Byte[24];
                Buffer.BlockCopy(hash.ComputeHash(Encoding.UTF8.GetBytes(key)), 0, aesKey, 0, 24);
                using (var aes = Aes.Create()) {
                    if (aes == null) {
                        throw new NullReferenceException(Strings.UnableToCreateAESCryptoObject);
                    }

                    aes.Key = aesKey;

                    var iv = new Byte[aes.IV.Length];
                    var cipherText = new Byte[buffer.Length - iv.Length];

                    Array.ConstrainedCopy(combined, 0, iv, 0, iv.Length);
                    Array.ConstrainedCopy(combined, iv.Length, cipherText, 0, cipherText.Length);

                    aes.IV = iv;

                    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    using (var resultStream = new MemoryStream()) {
                        using (var aesStream = new CryptoStream(resultStream, decryptor, CryptoStreamMode.Write))
                        using (var plainStream = new MemoryStream(cipherText)) {
                            plainStream.CopyTo(aesStream);
                        }

                        return Encoding.UTF8.GetString(resultStream.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// Encrypts the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="key">The key.</param>
        /// <returns>String.</returns>
        /// <exception cref="ArgumentException">Thrown when text was null, empty, or white space.</exception>
        /// <exception cref="ArgumentException">Thrown when key was null, empty, or white space.</exception>
        /// <exception cref="ArgumentException">Thrown when unable to create AES crypto object.</exception>
        public String Encrypt(String text, String key) {
            if (String.IsNullOrEmpty(text)) {
                throw new ArgumentException(Strings.TheTextMustHaveAValue, nameof(text));
            }
            if (String.IsNullOrEmpty(key)) {
                throw new ArgumentException(Strings.KeyMustHaveAValue, nameof(key));
            }

            var buffer = Encoding.UTF8.GetBytes(text);
            using (var hash = new SHA512CryptoServiceProvider()) {
                var aesKey = new Byte[24];
                Buffer.BlockCopy(hash.ComputeHash(Encoding.UTF8.GetBytes(key)), 0, aesKey, 0, 24);
                using (var aes = Aes.Create()) {
                    if (aes == null) {
                        throw new NullReferenceException(Strings.UnableToCreateAESCryptoObject);
                    }

                    aes.Key = aesKey;

                    using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                    using (var resultStream = new MemoryStream()) {
                        using (var aesStream = new CryptoStream(resultStream, encryptor, CryptoStreamMode.Write))
                        using (var plainStream = new MemoryStream(buffer)) {
                            plainStream.CopyTo(aesStream);
                        }

                        var result = resultStream.ToArray();
                        var combined = new Byte[aes.IV.Length + result.Length];
                        Array.ConstrainedCopy(aes.IV, 0, combined, 0, aes.IV.Length);
                        Array.ConstrainedCopy(result, 0, combined, aes.IV.Length, result.Length);

                        return Convert.ToBase64String(combined);
                    }
                }
            }
        }
    }
}
