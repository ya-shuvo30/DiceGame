using System;
using System.Collections.Generic;

public class Dice
{
    public List<int> Sides { get; private set; }

    public Dice(IEnumerable<int> sides)
    {
        Sides = new List<int>(sides);
    }

    public int RollAt(int index)
    {
        return Sides[index];
    }

    public int SidesCount => Sides.Count;

    public override string ToString()
    {
        return string.Join(",", Sides);
    }
}