using Xunit;


namespace Main;

public class RailFence_Tests
{
    [Theory]
    [InlineData("LETNIPONIEDZIALEK", "LIIIKENPNEZAETODL", 3)]
    [InlineData("SLONECZNYCZERWIEC", "SEYRCLNCNCEWEOZZI", 3)]
    [InlineData("CRYPTOGRAPHY", "CTARPORPYYGH", 3)]
    [InlineData("ABCDEFGHIJKLMNOPRSTUWVXYZ", "AEIMRWZBDFHJLNPSUVYCGKOTX", 3)]
    public void WordEncryption(string word, string encryptedWord_expected, int railCount)
    {
        // Arrange:
        RailFence encryptor = new(railCount: railCount);
        // Act:
        string encryptedWord = encryptor.Encrypt(word);
        // Assert:
        Assert.Equal(encryptedWord_expected, encryptedWord);
    }

    [Theory]
    [InlineData("LIIIKENPNEZAETODL", "LETNIPONIEDZIALEK", 3)]
    [InlineData("SEYRCLNCNCEWEOZZI", "SLONECZNYCZERWIEC", 3)]
    [InlineData("CTARPORPYYGH", "CRYPTOGRAPHY", 3)]
    [InlineData("AEIMRWZBDFHJLNPSUVYCGKOTX", "ABCDEFGHIJKLMNOPRSTUWVXYZ", 3)]
    public void WordDecryption(string word, string decryptedWord_expected, int railCount)
    {
        // Arrange:
        RailFence encryptor = new(railCount: railCount);
        // Act:
        string decryptedWord = encryptor.Decrypt(word);
        // Assert:
        Assert.Equal(decryptedWord_expected, decryptedWord);
    }
}