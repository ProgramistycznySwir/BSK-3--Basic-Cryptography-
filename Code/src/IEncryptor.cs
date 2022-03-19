namespace Main;


public interface IEncryptor
{
    public IEnumerable<T> Encrypt<T>(IEnumerable<T> sequence);
    public IEnumerable<T> Decrypt<T>(IEnumerable<T> sequence);
}