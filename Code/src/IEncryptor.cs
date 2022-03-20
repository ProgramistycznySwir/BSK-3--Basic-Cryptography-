namespace Main;


/// <summary>
/// Works with every sequence of elements instead of just words.
/// </summary>
public interface IEncryptor : IStringEncryptor
{
    public new string Encrypt(string word)
        => Encrypt(word.AsEnumerable()).CollectString();
    public new string Decrypt(string word)
        => Decrypt(word.AsEnumerable()).CollectString();
    
    public IEnumerable<T> Encrypt<T>(IEnumerable<T> sequence);
    public IEnumerable<T> Decrypt<T>(IEnumerable<T> sequence);
}