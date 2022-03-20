namespace Main;


/// <summary>
/// Zadanie 3
/// </summary>
public class CaesarCipher : IStringEncryptor
{
    public const int Default_Key = 3;
    public const string Default_Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    
    public string Alphabet { get; init; }
    public Dictionary<char, int> Alphabet_Dict { get; init; }

    public int Key { get; init; }
    public CaesarCipher(int key = Default_Key, string alphabet = Default_Alphabet)
    {
        (Key, Alphabet) = (key, alphabet);
        Alphabet_Dict = Alphabet
                .Zip(Enumerable.Range(0, Alphabet.Length))
                .ToDictionary(e => e.First, e => e.Second);
    }
    
    public string Encrypt(string word)
        => Shift(word, Key);
    public string Decrypt(string word)
        => Shift(word, -Key);

    public string Shift(string word, int shift)
    {
        if(shift is 0)
            return word;

        if(word.ToHashSet().Any(letter => Alphabet_Dict.ContainsKey(letter) is false))
            throw new ArgumentException($"First argument [{nameof(word)}] contains letters which are not in this encryptor alphabet!");

        return word.Select(letter => ShiftLetter(letter, shift)).CollectString();
    }

    public char ShiftLetter(char letter, int shift)
    {
        int index = (Alphabet_Dict[letter] + shift) % Alphabet.Length;
        index = index < 0 ? index + Alphabet.Length : index;
        return Alphabet[index];
    }
}