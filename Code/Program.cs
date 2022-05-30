using Main;
// Nie róbcie tak dzieci, tak nie wolno, chyba że jesteście takimi brudasami jak ja :)
using static System.Console;


#region >>> Functions <<<

static string RequestInput(string request, bool custom = false)
{
    WriteLine(custom ? request : $"Enter {request}:");
    return ReadLine()!;
}


static IStringEncryptor TestingRailFence()
{
    WriteLine();
    WriteLine("Encryptor: Rail Fence");
    int railCount;
    if(int.TryParse(RequestInput($"Rail count (default={RailFence.Default_RailCount})"), out railCount) is false)
    {
        WriteLine("[ERR] Couldn't parse railCount, setting to default.");
        railCount = RailFence.Default_RailCount;
    }
    WriteLine($"RailCount: {railCount}");
    return new RailFence(railCount);
}

static IStringEncryptor TestingMatrixShift()
{
    WriteLine();
    WriteLine("Encryptor: Matrix Shift");
    int[] key = new int[0];
    while(true)
    {
        string input = RequestInput($"Key [e.g. 1-2-3-4, only integers] (ENTER for default={string.Join('-', MatrixShift.Default_Key)})");
        if(input is "")
        {
            WriteLine($"Key set to default.");
            key = MatrixShift.Default_Key;
            break;
        }
        var parseResult = MatrixShift.TryParseKey(input, out key);
        if(parseResult.IsSome)
            break;

        WriteLine($"[ERR] Couldn't parse key with message: \"{parseResult.ErrorMessage}\". Try again.");
    }

    WriteLine($"Key: {string.Join('-', key)}");
    return new MatrixShift(key);
}
static IStringEncryptor TestingMatrixShift2()
{
    WriteLine();
    WriteLine("Encryptor: Matrix Shift");
    string key = "";
    while(true)
    {
        string input = RequestInput($"Key [only capital letters] (ENTER for default={string.Join('-', MatrixShift2.Default_Key)})");
        if(input is "")
        {
            WriteLine($"Key set to default.");
            key = MatrixShift2.Default_Key;
            break;
        }
        var parseResult = MatrixShift2.TryParseKey(input, out key);
        if(parseResult.IsSome)
            break;

        WriteLine($"[ERR] Couldn't parse key with message: \"{parseResult.ErrorMessage}\". Try again.");
    }

    WriteLine($"Key: {string.Join('-', key)}");
    return new MatrixShift2(key);
}
#endregion



WriteLine("Which algorith would you like to test?");
WriteLine(@"
1. Rail Fence,
2. Matrix transmutation (with sequence key),
3. Matrix transmutation (with string key).
");

IStringEncryptor encryptor = int.Parse(ReadKey().KeyChar.ToString()) switch {
    1 => TestingRailFence(),
    2 => TestingMatrixShift(),
    3 => TestingMatrixShift2(),
    _ => throw new ArgumentException("Algorithm number should be number between 1 and 3!")
};

while(true)
{
    WriteLine();
    var word = RequestInput("Phrase");
    WriteLine("Choose mode: 1. Encrypt, 2. Decrypt, 3. Two-way:");
    string result = int.Parse(ReadKey().KeyChar.ToString()) switch {
        1 => encryptor.Encrypt(word),
        2 => encryptor.Decrypt(word),
        3 => encryptor.Decrypt(encryptor.Encrypt(word)),
        _ => throw new ArgumentException("Algorithm number should be number between 1 and 3!")
    };
    WriteLine($"Result: {result}");
}
