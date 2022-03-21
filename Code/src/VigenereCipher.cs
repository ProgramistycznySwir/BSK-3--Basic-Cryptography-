namespace Main;


/// <summary>
/// Zadanie 4
/// </summary>
/// TODO: Make it work.
public class VigenereCipher : IStringEncryptor
{
    public const string Default_Key = "BREAKBREAKBR";
    public const string Default_Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    
    public string Key { get; init; }
    
    public string Alphabet { get; init; }
    public Dictionary<char, int> Alphabet_Dict { get; init; }

    public VigenereCipher(string key = Default_Key, string alphabet = Default_Alphabet)
    {
        (Key, Alphabet) = (key, alphabet);
        Alphabet_Dict = Alphabet
                .Zip(Enumerable.Range(0, Alphabet.Length))
                .ToDictionary(e => e.First, e => e.Second);
    }
    

    public string Encrypt(string word)
        => Hash(word, Key);

    public string Decrypt(string word)
        => Hash(word, Key, reverse: true);

    public string Hash(string word, string key, bool reverse = false)
    {
        List<char> result = new();

        for (int i = 0; i < word.Length; i++)
            result.Add(ShiftLetter(word[i], key[i % key.Length], reverse));

        return result.CollectString();
    }

    public char ShiftLetter(char letter, char key, bool revert)
    {
        if (Alphabet_Dict.ContainsKey(letter) is false)
            return letter;

        int index = Alphabet_Dict[letter];
        index += revert is false ? Alphabet_Dict[key] : -Alphabet_Dict[key];
        index = Math.Clamp(index, 0, Alphabet.Length - 1);
        
        return Alphabet[index];
    }
}