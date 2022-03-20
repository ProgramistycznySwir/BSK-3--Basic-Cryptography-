using Xunit;


namespace Main;

public class MatrixShift_Tests
{
    [Theory]
    [InlineData("CRYPTOGRAPHYOSA", "YCPRGTROHAYPAOS", new int[] { 3, 1, 4, 2 })]
    // [InlineData("ABCDEFGHIJKLMNOPRSTUWVXYZ", "AEIMRWZBDFHJLNPSUVYCGKOTX", new int[] { 3, 1, 4, 2 })]
    public void WordEncryption(string word, string encryptedWord_expected, int[] key)
    {
        // Arrange:
        IEncryptor encryptor = new MatrixShift(key: key);
        // Act:
        IEnumerable<char> encryptorOutput = encryptor.Encrypt(word);
        // Assert:
        Assert.NotNull(encryptorOutput);
        string encryptedWord = encryptorOutput.CollectString();
        Assert.Equal(encryptedWord_expected, encryptedWord);
    }

    [Theory]
    [InlineData("YCPRGTROHAYPAOS", "CRYPTOGRAPHYOSA", new int[] { 3, 1, 4, 2 })]
    // [InlineData("AEIMRWZBDFHJLNPSUVYCGKOTX", "ABCDEFGHIJKLMNOPRSTUWVXYZ", new int[] { 3, 1, 4, 2 })]
    public void WordDecryption(string word, string decryptedWord_expected, int[] key)
    {
        // Arrange:
        IEncryptor encryptor = new MatrixShift(key: key);
        // Act:
        IEnumerable<char> encryptorOutput = encryptor.Decrypt(word);
        // Assert:
        Assert.NotNull(encryptorOutput);
        string decryptedWord = encryptorOutput.CollectString();
        Assert.Equal(decryptedWord_expected, decryptedWord);
    }

    [Theory]
    [InlineData("CRYPTOGRAPHYOSA", new int[] { 3, 1, 4, 2 })]
    [InlineData("ABCDEFGHIJKLMNOPRSTUWVXYZ", new int[] { 3, 1, 4, 2 })]
    [InlineData("CRYPTOGRAPHYOSA", new int[] { 3, 4, 1, 5, 2 })]
    [InlineData("ABCDEFGHIJKLMNOPRSTUWVXYZ", new int[] { 3, 4, 1, 5, 2 })]
    public void TwoWayEncryption(string word, int[] key)
    {
        // Arrange:
        IEncryptor encryptor = new MatrixShift(key: key);
        // Act:
        IEnumerable<char> encryptorOutput = encryptor.Decrypt(encryptor.Encrypt(word));
        // Assert:
        Assert.NotNull(encryptorOutput);
        string encryptorOutput_Word = encryptorOutput.CollectString();
        Assert.Equal(word, encryptorOutput_Word);
    }

    // Nie wiem dlaczego, ale sprawdzarka na stronie [http://testowaniealgorytmow2021.site] działa w jakiś inny sposób
    //  niż instrukcja, może to ja opieram się na jakiś błędnych założeniach, ale wydaje mi się, że to ona się myli.
    [Theory]
    [InlineData("CRYPTOGRAPHYOSA", "RPOYGCTAHOPSRYA", new int[] { 3, 1, 4, 2 })]
    // [InlineData("ABCDEFGHIJKLMNOPRSTUWVXYZ", "AEIMRWZBDFHJLNPSUVYCGKOTX", new int[] { 3, 1, 4, 2 })]
    public void WordEncryption_ZeSprawdzarki(string word, string encryptedWord_expected, int[] key)
    {
        // Arrange:
        IEncryptor encryptor = new MatrixShift(key: key);
        // Act:
        IEnumerable<char> encryptorOutput = encryptor.Encrypt(word);
        // Assert:
        Assert.NotNull(encryptorOutput);
        string encryptedWord = encryptorOutput.CollectString();
        Assert.Equal(encryptedWord_expected, encryptedWord);
    }
}