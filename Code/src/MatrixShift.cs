namespace Main;

public class MatrixShift : IEncryptor
{
    public int[] Key { get; init; }
    public int[] NormalizedKey { get; init; }
    public MatrixShift(int[]? key = null){
        Key = key ?? new int[] { 3, 1, 4, 2 };
        NormalizedKey = Key.Select(e => e-1).ToArray();
    }
    
    /// <summary />
    /// <param name="key">Zero-based key.</param>
    /// <returns>Zero-based key.</returns>
    public static int[] InverseKey(int[] key)
    {
        int[] result = new int[key.Length];
        for(int i = 0; i < key.Length; i++)
            result[key[i]] = i;
        return result;
    }

    // TODO: Encrypt and decrypt look similar, you can join them.
    public IEnumerable<T> Encrypt<T>(IEnumerable<T> sequence)
    {
        return Hash(sequence, NormalizedKey);
        if(Key.Length is 1)
            return sequence;

        T[] buffer = new T[Key.Length];

        int sequence_Count = sequence.Count();
        var result = new List<T>(sequence_Count);

        int[] inversedKey = InverseKey(NormalizedKey).ToArray();

        int i = 0;
        foreach(T element in sequence)
        {
            buffer[inversedKey[i]] = element;
            i++;
            if(i == Key.Length)
            {
                i = 0;
                result.AddRange(buffer);
            }
        }
        if(i is not 0) // If buffer is not empty. (i states number of elements in buffer)
            for(int ii = 0; ii < Key.Length; ii++) 
                if(NormalizedKey[ii] < i) // Skip if key literal is bigger than buffer elements.
                    result.Add(buffer[ii]);

        return result.AsEnumerable();
    }

    public IEnumerable<T> Decrypt<T>(IEnumerable<T> sequence)
    {
        return Hash(sequence, NormalizedKey, reverse: true);
        if(Key.Length is 1)
            return sequence;

        T[] buffer = new T[Key.Length];

        int sequence_Count = sequence.Count();
        var result = new List<T>(sequence_Count);

        int[] inversedKey = InverseKey(NormalizedKey).ToArray();

        int bufferIndex = 0;
        foreach(T element in sequence)
        {
            buffer[NormalizedKey[bufferIndex]] = element;
            bufferIndex++;
            if(bufferIndex == Key.Length)
            {
                bufferIndex = 0;
                result.AddRange(buffer);
            }
        }

        // TODO: I don't know why, but the encryption only breaks in last buffer.
        if(bufferIndex is not 0) // If buffer is not empty. (i states number of elements in buffer)
        {
            // If buffer is not empty we have to do last pass little different.
            //  I've decided to revert last loop instead of modyfing loop logic.
            var temp = new T[Key.Length];
            for(int i0 = 0, i1 = 0; i0 < Key.Length; i0++)
            {
                if(NormalizedKey[i0] >= bufferIndex)
                    continue;

                temp[i0] = buffer[NormalizedKey[i1]];
                i1++;
            }
            for(int i0 = 0; i0 < Key.Length; i0++)
                buffer[NormalizedKey[i0]] = temp[i0];

            for(int ii = 0; ii < bufferIndex; ii++)
                result.Add(buffer[ii]);

        }

        return result.AsEnumerable();
    }

    public static IEnumerable<T> Hash<T>(IEnumerable<T> sequence, int[] key, bool reverse = false)
    {
        if(key.Length is 1)
            return sequence;

        T[] buffer = new T[key.Length];
        int sequence_Count = sequence.Count();
        var result = new List<T>(sequence_Count);

        int[] inversedKey = InverseKey(key).ToArray();

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
        if(bufferIndex is not 0) // If buffer is not empty. (i states number of elements in buffer)
        {
            if(reverse is false)
            {
                for(int ii = 0; ii < key.Length; ii++) 
                    if(key[ii] < bufferIndex) // Skip if key literal is bigger than buffer elements.
                        result.Add(buffer[ii]);
            }
            else
            {
                var temp = new T[key.Length];
                for(int i0 = 0, i1 = 0; i0 < key.Length; i0++)
                {
                    if(key[i0] >= bufferIndex)
                        continue;

                    temp[i0] = buffer[key[i1]];
                    i1++;
                }
                for(int i0 = 0; i0 < key.Length; i0++)
                    buffer[key[i0]] = temp[i0];

                for(int i2 = 0; i2 < bufferIndex; i2++)
                    result.Add(buffer[i2]);
            }
        }

        return result.AsEnumerable();
    }
}