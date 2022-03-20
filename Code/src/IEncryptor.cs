namespace Main;


/// <summary>
/// Works with every sequence of elements instead of just words.
/// </summary>
public interface IEncryptor : IStringEncryptor
{
    public new string Encrypt(string word)
        => Encrypt(word).CollectString();
    public new string Decrypt(string word)
        => Encrypt(word).CollectString();
    
    public IEnumerable<T> Encrypt<T>(IEnumerable<T> sequence);
    public IEnumerable<T> Decrypt<T>(IEnumerable<T> sequence);
}