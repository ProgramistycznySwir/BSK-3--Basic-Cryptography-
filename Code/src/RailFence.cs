namespace Main;

public class RailFence : IEncryptor
{
    public int RailCount { get; init; }
    public RailFence(int railCount = 3)
        => (RailCount) = (railCount);
    
    // Performance:
    //  Time: O(n)
    //  Space: n
    //  where:
    //      n is sequence.Lenght
    public IEnumerable<T> Encrypt<T>(IEnumerable<T> sequence)
    {
        if(RailCount == 1)
            return sequence;

        Queue<T>[] layers = new Queue<T>[RailCount].Select(e => new Queue<T>()).ToArray();

        var(bounceBack, currRail) = (false, 0);
        foreach(T element in sequence)
        {
            if(currRail is 0)
                bounceBack = false;
            else if(currRail == RailCount-1)
                bounceBack = true;
            layers[currRail].Enqueue(element);
            currRail += bounceBack ? -1 : 1;
        }

        return layers.SelectMany(e => e);
    }

    public IEnumerable<T> Decrypt<T>(IEnumerable<T> sequence)
    {
        if(RailCount == 1)
            return sequence;
        
        List<T>[] lines = Enumerable.Range(0, RailCount).Select(e => new List<T>()).ToArray();
        int[] lines_Lenght = Enumerable.Repeat(0, RailCount).ToArray();

        var(bounceBack, currRail) = (false, 0);
        foreach(T element in sequence)
        {
            lines_Lenght[currRail]++;

            if(currRail is 0)
                bounceBack = false;
            else if(currRail == RailCount-1)
                bounceBack = true;
            
            currRail += bounceBack ? -1 : 1;
        }

        using (var enumerator = sequence.GetEnumerator().Init())
        for (int line = 0; line < RailCount; line++)
            for (int c = 0; c < lines_Lenght[line]; c++)
                lines[line].Add(enumerator.PopMoveNext());

        int sequence_Count = sequence.Count(); // Cache
        List<T> result = new List<T>(sequence_Count);
        (bounceBack, currRail) = (false, 0);

        var lines_enumerator = lines.Select(e => e.GetEnumerator().Init()).ToArray();
        for (int i = 0; i < sequence_Count; i++)
        {
            result.Add(lines_enumerator[currRail].PopMoveNext());

            if(currRail is 0)
                bounceBack = false;
            else if(currRail == RailCount-1)
                bounceBack = true;
            
            currRail += bounceBack ? -1 : 1;
        }
        foreach (var enumerator in lines_enumerator)
            enumerator.Dispose();

        return result.AsEnumerable();
    }
}