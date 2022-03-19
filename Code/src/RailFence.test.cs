using Xunit;


namespace Main;

public class RailFence_Tests
{
    [Theory]
    [InlineData("CRYPTOGRAPHY", "CTARPORPYYGH", 3)]
    [InlineData("ABCDEFGHIJKLMNOPRSTUWVXYZ", "AEIMRWZBDFHJLNPSUVYCGKOTX", 3)]
    public void WordEncryption(string word, string encryptedWord_expected, int railCount)
    {
        // Arrange:
        IEncryptor encryptor = new RailFence(railCount: railCount);
        // Act:
        IEnumerable<char> encryptorOutput = encryptor.Encrypt(word);
        // Assert:
        Assert.NotNull(encryptorOutput);
        string encryptedWord = encryptorOutput.CollectString();
        Assert.Equal(encryptedWord_expected, encryptedWord);
    }

    [Theory]
    [InlineData("CTARPORPYYGH", "CRYPTOGRAPHY", 3)]
    [InlineData("AEIMRWZBDFHJLNPSUVYCGKOTX", "ABCDEFGHIJKLMNOPRSTUWVXYZ", 3)]
    public void WordDecryption(string word, string decryptedWord_expected, int railCount)
    {
        // Arrange:
        IEncryptor encryptor = new RailFence(railCount: railCount);
        // Act:
        IEnumerable<char> encryptorOutput = encryptor.Decrypt(word);
        // Assert:
        Assert.NotNull(encryptorOutput);
        string decryptedWord = encryptorOutput.CollectString();
        Assert.Equal(decryptedWord_expected, decryptedWord);
    }
}