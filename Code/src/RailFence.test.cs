using Xunit;


namespace Main;

public class RailFence_Tests
{
    // public int RailCount { get; init; }
    // public RailFence_Tests(int railCount = 3)
    //     => (RailCount) = (railCount);
    
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

    // public IEnumerable<T> Encrypt<T>(IEnumerable<T> sequence)
    // {
    //     if(RailCount == 1)
    //         return sequence;

    //     Queue<T>[] layers = new Queue<T>[RailCount].Select(e => new Queue<T>()).ToArray();

    //     var(bounceBack, i) = (false, 0);
    //     foreach(T element in sequence)
    //     {
    //         if(i is 0)
    //             bounceBack = false;
    //         else if(i == RailCount-1)
    //             bounceBack = true;
    //         layers[i].Enqueue(element);
    //         i += bounceBack ? -1 : 1;
    //     }

    //     return layers.SelectMany(e => e);
    // }

    // public IEnumerable<T> Decrypt<T>(IEnumerable<T> sequence)
    // {
    //     if(RailCount == 1)
    //         return sequence;

    //     int sequenceLenght = sequence.Count();
    //     Queue<T> result = new(sequenceLenght);
    //     Queue<T>[] layers = new Queue<T>[RailCount].Select(e => new Queue<T>()).ToArray();

    //     bool bounceBack = false;
    //     int i = 0;
    //     foreach(T element in sequence)
    //     {
    //         if(i is 0)
    //             bounceBack = false;
    //         else if(i == RailCount-1)
    //             bounceBack = true;
    //         layers[i].Enqueue(element);
    //         i += bounceBack ? -1 : 1;
    //     }
        
    //     return layers.SelectMany(e => e);
    // }
}