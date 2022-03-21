namespace Main;

/// <summary>
/// Zadanie 2
/// </summary>
// TODO: Implement this!
public class Transposition2 : IStringEncryptor
{
    public const string Default_Key = "CONVENIENCE";

    public string Key { get; init; }

    public Transposition2(string key = Default_Key){
        (Key) = (key);
        // NormalizedKey = Key.Select(e => e-1).ToArray();
    }
    
    
    public string Encrypt(string word)
    {
        int queueLenght = (int) Math.Ceiling((float)word.Length / Key.Length);
        (Queue<char> collumn, char keyLiteral)[] matrix = new Queue<char>[Key.Length]
                .Select(e => new Queue<char>(queueLenght))
                .Zip(Key)
                .ToArray();

        for(int i = 0; i < word.Length; i++)
            matrix[i%matrix.Length].collumn.Enqueue(word[i]);

        return string.Join(' ', matrix.OrderBy(e => e.keyLiteral).Select(e => e.collumn));
        throw new NotImplementedException();
    }
        // => Encrypt(word.AsEnumerable()).CollectString();
    public string Decrypt(string word)
    {
        throw new NotImplementedException();
    }
        // => Decrypt(word.AsEnumerable()).CollectString();

    // public IEnumerable<T> Encrypt<T>(IEnumerable<T> sequence)
    //     => Hash(sequence, NormalizedKey);

    // public IEnumerable<T> Decrypt<T>(IEnumerable<T> sequence)
    //     => Hash(sequence, NormalizedKey, reverse: true);

    // Since MatrixShift is only series manipulation, we could easily invert it by inverting the key, and bool at the
    //  end is doing that for us. Look out for use of reverse flag to seek differences between encryption and decryption.
    public static IEnumerable<T> Hash<T>(IEnumerable<T> sequence, int[] key, bool reverse = false)
    {
        if(key.Length is 1)
            return sequence;

        List<T> result = new List<T>(sequence.Count());
        int[] inversedKey = InverseKey(key).ToArray();

        T[] buffer = new T[key.Length];
        int bufferIndex = 0;
        foreach(T element in sequence)
        {
            buffer[(reverse ? key : inversedKey)[bufferIndex]] = element;
            bufferIndex++;
            if(bufferIndex == key.Length)
            {
                bufferIndex = 0;
                result.AddRange(buffer);
            }
        }
        // After normal pass of the algorithm we have to collect last incomplete segment:
        if(bufferIndex is not 0) // If buffer is not empty. (i states number of elements in buffer)
        {
            if(reverse is false) // Encrypt
            {
                for(int ii = 0; ii < key.Length; ii++) 
                    if(key[ii] < bufferIndex) // Skip if key literal is bigger than buffer elements.
                        result.Add(buffer[ii]);
            }
            // If we want to reverse encryption we have to:
            // Important bit about this whole operation is that the last sequence is often encrypted in a way:
            //  ABCX -(3,1,4,2)> CAXB - where X is just empty element, null.
            //  But in output sequence elements are collected to something like this CAB and we lose information about X.
            //  When we decrypt this sequence we get CABX -(inverse(3,1,4,2))> BCXA => BCA - no bueno.
            //  In step 1 we have to revert last shift inserting X into correct place, we do this by skipping iterations
            //   where key element is greater than last sequence lenght.
            else // Decrypt
            {
                // 1. Revert shift in last segment.
                var temp = new T[key.Length];
                for(int i0 = 0, i1 = 0; i0 < key.Length; i0++)
                {
                    if(key[i0] >= bufferIndex) // 1.1. Most important bit is this part.
                        continue;

                    temp[i0] = buffer[key[i1]];
                    i1++;
                }
                // 2. We have to redo shift, now with elements in correct places.
                for(int i0 = 0; i0 < key.Length; i0++)
                    buffer[key[i0]] = temp[i0];
                // 3. Voila, now we just add elements
                for(int i2 = 0; i2 < bufferIndex; i2++)
                    result.Add(buffer[i2]);
            }
        }

        return result.AsEnumerable();
    }
}