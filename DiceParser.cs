using System;
using System.Collections.Generic;
using System.Linq;

public static class DiceParser
{
    public static List<Dice> ParseArgs(string[] args)
    {
        var diceList = new List<Dice>();
        foreach (var arg in args)
        {
            var parts = arg.Split(',');
            if(parts.Length < 2)
                throw new ArgumentException("Each dice must have at least 2 sides.");

            var sides = new List<int>();
            foreach(var p in parts)
            {
                if(!int.TryParse(p, out int side))
                    throw new ArgumentException("Dice sides must be integers.");
                sides.Add(side);
            }
            diceList.Add(new Dice(sides));
        }

        if(diceList.Count < 3)
            throw new ArgumentException("You must specify at least 3 dice.");

        return diceList;
    }
}