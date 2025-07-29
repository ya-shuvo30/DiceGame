using System;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            var diceList = DiceParser.ParseArgs(args);

            Console.WriteLine("Parsed dice:");
            for (int i = 0; i < diceList.Count; i++)
                Console.WriteLine($"{i} - {diceList[i]}");

            var engine = new GameEngine(diceList);
            engine.Play();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("Usage example:");
            Console.WriteLine("dotnet run -- 2,2,4,4,9,9 6,8,1,1,8,6 7,5,3,7,5,3");
        }
    }
}