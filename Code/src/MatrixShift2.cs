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
        word = string.Join("", word.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        
        int queueLenght = (int) Math.Ceiling((float)word.Length / Key.Length);
        (Queue<char> column, char keyLiteral)[] matrix = new Queue<char>[Key.Length]
                .Select(e => new Queue<char>(queueLenght))
                .Zip(Key)
                .ToArray();

        for(int i = 0; i < word.Length; i++)
            matrix[i%matrix.Length].column.Enqueue(word[i]);

        return string.Join(' ', matrix.OrderBy(e => e.keyLiteral).Select(e => e.column.CollectString()));
    }
    
    public string Decrypt(string word)
    {
        // Prepare input for organizing.
        LinkedList<(char, Queue<char>)> rawMatrix = new(
                Key.OrderBy(e => e)
                .Zip(word.Split(' ').Select(e => new Queue<char>(e))));

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
}