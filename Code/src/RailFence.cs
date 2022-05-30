namespace Main;

/// <summary>
/// Zadanie 1
/// </summary>
public class RailFence : IEncryptor
{
    public const int Default_RailCount = 3;
    
    public int RailCount { get; init; }
    public RailFence(int railCount = Default_RailCount)
        => (RailCount) = (railCount);
    
    public string Encrypt(string word)
        => Encrypt(word.AsEnumerable()).CollectString();
    public string Decrypt(string word)
        => Decrypt(word.AsEnumerable()).CollectString();

    // Performance:
    //  Time: O(n)
    //  Space: n
    //  where:
    //      n is sequence.Lenght
    public IEnumerable<T> Encrypt<T>(IEnumerable<T> sequence)
    {
        // Jeśli jest tylko jedna szyna od razu zwróć input.
        if(RailCount == 1)
            return sequence;

        // Deklaracji i inicjalizacja szyn.
        Queue<T>[] layers = new Queue<T>[RailCount].Select(e => new Queue<T>()).ToArray();

        // Deklaracja zmiennych używanych w iteracji przez sekwencję.
        var(bounceBack, currRail) = (false, 0);
        foreach(T element in sequence)
        {
            // Algorytm w currRail przechowuje do której szyny ma zapisać dany element, poniższy blok służy do "odbijania"
            //  tej zmiennej pomiędzy 0 i RailCount.
            if(currRail is 0)
                bounceBack = false;
            else if(currRail == RailCount-1)
                bounceBack = true;
            // Umieszczanie elementu na szynie.
            layers[currRail].Enqueue(element);
            currRail += bounceBack ? -1 : 1;
        }

        // Zebranie wszystkich szyn do jednej kolekcji. W innych językach to powinno być .reduce().
        return layers.SelectMany(e => e);
    }

    public IEnumerable<T> Decrypt<T>(IEnumerable<T> sequence)
    {
        // Jeśli jest tylko jedna szyna od razu zwróć input.
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