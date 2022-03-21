using Xunit;


namespace Main;

public class VigenereCipher_Tests
{
    [Theory]
    [InlineData("CRYPTOGRAPHY", "DICPDPXVAZIP", "BREAKBREAKBR")]
    // [InlineData("ABCDEFGHIJKLMNOPRSTUWVXYZ", "AEIMRWZBDFHJLNPSUVYCGKOTX", new int[] { 3, 1, 4, 2 })]
    public void WordEncryption(string word, string encryptedWord_expected, string key)
    {
        // Arrange:
        VigenereCipher encryptor = new(key: key);
        // Act:
        string encryptedWord = encryptor.Encrypt(word);
        // Assert:
        Assert.Equal(encryptedWord_expected, encryptedWord);
    }

    [Theory]
    [InlineData("DICPDPXVAZIP", "CRYPTOGRAPHY", "BREAKBREAKBR")]
    public void WordDecryption(string word, string decryptedWord_expected, string key)
    {
        // Arrange:
        VigenereCipher encryptor = new(key: key);
        // Act:
        string decryptedWord = encryptor.Decrypt(word);
        // Assert:
        Assert.Equal(decryptedWord_expected, decryptedWord);
    }

    [Theory]
    [InlineData("CRYPTOGRAPHY", "BREAKBREAKBR")]
    [InlineData("ABCDEFGHIJKLMNOPRSTUWVXYZ", "BREAKBREAKBR")]
    [InlineData("CRYPTOGRAPHYOSA", "OTHERKEY")]
    [InlineData("ABCDEFGHIJKLMNOPRSTUWVXYZ", "OTHERKEY")]
    public void TwoWayEncryption(string word, string key)
    {
        // Arrange:
        VigenereCipher encryptor = new(key: key);
        // Act:
        string encryptorOutput = encryptor.Decrypt(encryptor.Encrypt(word));
        // Assert:
        Assert.Equal(word, encryptorOutput);
    }
}