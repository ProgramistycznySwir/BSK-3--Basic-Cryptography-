using Xunit;


namespace Main;

public class MatrixShift_Tests
{
    [Theory]
    [InlineData("CRYPTOGRAPHYOSA", "YCPRGTROHAYPAOS", new int[] { 3, 1, 4, 2 })]
    public void WordEncryption(string word, string encryptedWord_expected, int[] key)
    {
        // Arrange:
        MatrixShift encryptor = new(key: key);
        // Act:
        string encryptedWord = encryptor.Encrypt(word);
        // Assert:
        Assert.Equal(encryptedWord_expected, encryptedWord);
    }

    [Theory]
    [InlineData("YCPRGTROHAYPAOS", "CRYPTOGRAPHYOSA", new int[] { 3, 1, 4, 2 })]
    public void WordDecryption(string word, string decryptedWord_expected, int[] key)
    {
        // Arrange:
        MatrixShift encryptor = new(key: key);
        // Act:
        string decryptedWord = encryptor.Decrypt(word);
        // Assert:
        Assert.Equal(decryptedWord_expected, decryptedWord);
    }

    [Theory]
    [InlineData("CRYPTOGRAPHYOSA", new int[] { 3, 1, 4, 2 })]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", new int[] { 3, 1, 4, 2 })]
    [InlineData("CRYPTOGRAPHYOSA", new int[] { 3, 4, 1, 5, 2 })]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", new int[] { 3, 4, 1, 5, 2 })]
    public void TwoWayEncryption(string word, int[] key)
    {
        // Arrange:
        MatrixShift encryptor = new(key: key);
        // Act:
        string encryptorOutput = encryptor.Decrypt(encryptor.Encrypt(word));
        // Assert:
        Assert.Equal(word, encryptorOutput);
    }

    // Nie wiem dlaczego, ale sprawdzarka na stronie [http://testowaniealgorytmow2021.site] działa w jakiś inny sposób
    //  niż instrukcja, może to ja opieram się na jakiś błędnych założeniach, ale wydaje mi się, że to ona się myli.
    [Theory]
    [InlineData("CRYPTOGRAPHYOSA", "RPOYGCTAHOPSRYA", new int[] { 3, 1, 4, 2 })]
    // [InlineData("ABCDEFGHIJKLMNOPRSTUWVXYZ", "AEIMRWZBDFHJLNPSUVYCGKOTX", new int[] { 3, 1, 4, 2 })]
    public void WordEncryption_ZeSprawdzarki(string word, string encryptedWord_expected, int[] key)
    {
        // Arrange:
        MatrixShift encryptor = new(key: key);
        // Act:
        string encryptorOutput = encryptor.Encrypt(word);
        // Assert:
        Assert.Equal(encryptedWord_expected, encryptorOutput);
    }
}