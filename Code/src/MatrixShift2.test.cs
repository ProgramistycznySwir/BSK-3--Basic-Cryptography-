using Xunit;


namespace Main;

public class MatrixShift2_Tests
{
    [Theory]
    [InlineData("HERE IS A SECRET MESSAGE ENCIPHERED BY TRANSPOSITION",
            "HECRN CEYI ISEP SGDI RNTO AAES RMPN SSRO EEBT ETIA EEHS",
            "CONVENIENCE")]
    public void WordEncryption(string word, string encryptedWord_expected, string key)
    {
        // Arrange:
        MatrixShift2 encryptor = new(key: key);
        // Act:
        string encryptedWord = encryptor.Encrypt(word);
        // Assert:
        Assert.Equal(encryptedWord_expected, encryptedWord);
    }

    [Theory]
    [InlineData("HECRN CEYI ISEP SGDI RNTO AAES RMPN SSRO EEBT ETIA EEHS",
            "HEREISASECRETMESSAGEENCIPHEREDBYTRANSPOSITION",
            "CONVENIENCE")]
    public void WordDecryption(string word, string decryptedWord_expected, string key)
    {
        // Arrange:
        MatrixShift2 encryptor = new(key: key);
        // Act:
        string decryptedWord = encryptor.Decrypt(word);
        // Assert:
        Assert.Equal(decryptedWord_expected, decryptedWord);
    }

    // Important in those tests is that to not test words with whitespaces, cause algorithm is not ment to handle them.
    [Theory]
    [InlineData("CRYPTOGRAPHYOSA", "ROSHAR")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "ROSHAR")]
    [InlineData("CRYPTOGRAPHYOSA", "CONVENIENCE")]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "CONVENIENCE")]
    public void TwoWayEncryption(string word, string key)
    {
        // Arrange:
        MatrixShift2 encryptor = new(key: key);
        // Act:
        string encryptorOutput = encryptor.Decrypt(encryptor.Encrypt(word));
        // Assert:
        Assert.Equal(word, encryptorOutput);
    }
    
    // [Theory]
    // [InlineData("HECRN CEYI ISEP SGDI RNTO AAES RMPN SSRO EEBT ETIA EEHS",
    //         "HEAESIORENDMEYNSASAOHERNICSSBCIPTITSEEPEERGRT",
    //         "CONVENIENCE")]
    // public void WordEncryption_ZeSprawdzarki(string word, string encryptedWord_expected, string key)
    // {
    //     // Arrange:
    //     MatrixShift2 encryptor = new(key: key);
    //     // Act:
    //     string encryptorOutput = encryptor.Encrypt(word);
    //     // Assert:
    //     Assert.Equal(encryptedWord_expected, encryptorOutput);
    // }
}