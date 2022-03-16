namespace Main;

public class RailFence
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

        var(bounceBack, i) = (false, 0);
        foreach(T element in sequence)
        {
            if(i is 0)
                bounceBack = false;
            else if(i == RailCount-1)
                bounceBack = true;
            layers[i].Enqueue(element);
            i += bounceBack ? -1 : 1;
        }

        return layers.SelectMany(e => e);
    }

    public IEnumerable<T> Decrypt<T>(IEnumerable<T> sequence)
    {
        if(RailCount == 1)
            return sequence;

        int sequenceLenght = sequence.Count();
        Queue<T> result = new(sequenceLenght);
        Queue<T>[] layers = new Queue<T>[RailCount].Select(e => new Queue<T>()).ToArray();

        bool bounceBack = false;
        int i = 0;
        foreach(T element in sequence)
        {
            if(i is 0)
                bounceBack = false;
            else if(i == RailCount-1)
                bounceBack = true;
            layers[i].Enqueue(element);
            i += bounceBack ? -1 : 1;
        }
        
        return layers.SelectMany(e => e);
    }
}