namespace Main;


public interface IStringEncryptor
{
    public string Encrypt(string word);
    public string Decrypt(string word);
}