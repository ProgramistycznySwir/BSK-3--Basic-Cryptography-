namespace Main;

/// <summary>
/// Zadanie 2b
/// </summary>
// TODO: Implement this!
public class MatrixShift2 : IStringEncryptor
{
    public const string Default_Key = "CONVENIENCE";

    public string Key { get; init; }

    public MatrixShift2(string key = Default_Key){
        (Key) = (key);
    }
    
    public string Encrypt(string word)
    {
        word = word.Replace(" ", "");
        
        int queueLenght = (int) Math.Ceiling((float)word.Length / Key.Length);
        (Queue<char> column, char keyLiteral)[] matrix = new Queue<char>[Key.Length]
                .Select(e => new Queue<char>(queueLenght))
                .Zip(Key)
                .ToArray();

        for(int i = 0; i < word.Length; i++)
            matrix[i%matrix.Length].column.Enqueue(word[i]);

        return string.Join("", matrix.OrderBy(e => e.keyLiteral).Select(e => e.column.CollectString()));
    }
    
    public string Decrypt(string word)
    {
        // Prepare input for organizing.
        List<(char Literal, int Lenght)> Key_lenghts = new(Key.Length);
        for(int i = 0; i < Key.Length; i++)
            Key_lenghts.Add((Key[i], (int) Math.Ceiling((float)(word.Length - i) / Key.Length)));
        Key_lenghts = Key_lenghts.OrderBy(e => e.Literal).ToList();

        LinkedList<(char, Queue<char>)> rawMatrix = new();
        int lenghtUntilNow = 0;
        foreach(var pair in Key_lenghts)
        {
            int lenght = pair.Lenght;
            rawMatrix.AddLast((pair.Literal, new Queue<char>(word[lenghtUntilNow..(lenghtUntilNow+lenght)])));
            lenghtUntilNow += lenght;
        }
        
        // Now we have to place matrix columns in order defined by key.
        Queue<Queue<char>> matrix = new();
        foreach(var keyLiteral in Key)
        {
            var node = rawMatrix.First;
            
            while(node is not null)
            {
                if (node.Value.Item1 == keyLiteral)
                {
                    matrix.Enqueue(node!.Value.Item2);
                    rawMatrix.Remove(node);
                    break;
                }

                node = node.Next;
            }
        }
        // Collect result by reading matrix row by row.
        Queue<char> result = new();
        while(true)
            foreach(var column in matrix)
            {
                if(column.Count is 0)
                    return result.CollectString();
                result.Enqueue(column.Dequeue());
            }
    }

    public static (bool IsSome, string ErrorMessage) TryParseKey(string rawKey, out string key)
    {
        // In fact key could be any comparable set of objects, but exercise does not specify if we have to make it soo robust.
        // Normalize key.
        key = rawKey.ToUpper();
        return (true, "");
    }
}