using System.Collections.Generic;

public static class ProbabilityCalculator
{
    // Calculate probability that dice a beats dice b
    public static double ProbabilityWin(Dice a, Dice b)
    {
        int win = 0, total = 0;
        foreach(var sideA in a.Sides)
        {
            foreach(var sideB in b.Sides)
            {
                if(sideA > sideB) win++;
                total++;
            }
        }
        return (double)win / total;
    }
}