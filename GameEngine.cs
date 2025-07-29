using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using ConsoleTables;

public class GameEngine
{
    private List<Dice> DiceList;

    public GameEngine(List<Dice> diceList)
    {
        DiceList = diceList;
    }

    // Returns index of player who moves first (0 or 1)
    public int DetermineFirstPlayer()
    {
        Console.WriteLine("Let's determine who makes the first move.");

        int computerValue = RandomNumberGenerator.GetInt32(2);

        Console.WriteLine("I selected a random value in the range 0..1.");
        Console.WriteLine("Try to guess my selection.");
        Console.WriteLine("0 - 0");
        Console.WriteLine("1 - 1");
        Console.WriteLine("X - exit");
        Console.WriteLine("? - help");

        while (true)
        {
            string input = (Console.ReadLine() ?? "").Trim().ToLower();

            if (input == "x")
            {
                Environment.Exit(0);
            }
            else if (input == "?")
            {
                RenderHelpTable(DiceList);
                continue;
            }
            else if (input == "0" || input == "1")
            {
                int userChoice = int.Parse(input);
                Console.WriteLine($"My selection: {computerValue}.");

                if (userChoice == computerValue)
                {
                    Console.WriteLine("You go first!");
                    return 0;
                }
                else
                {
                    Console.WriteLine("I go first!");
                    return 1;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Enter 0, 1, ?, or X.");
            }
        }
    }

    // Returns index of selected dice by user (from available dice)
    public int UserSelectDice(HashSet<int> unavailable)
    {
        while (true)
        {
            Console.WriteLine("Choose your dice:");
            for (int i = 0; i < DiceList.Count; i++)
            {
                if (unavailable.Contains(i)) continue;
                Console.WriteLine($"{i} - {DiceList[i]}");
            }
            Console.WriteLine("X - exit");
            Console.WriteLine("? - help");
            Console.Write("Your selection: ");
            string input = (Console.ReadLine() ?? "").Trim().ToLower();

            if (input == "x")
            {
                Environment.Exit(0);
            }
            else if (input == "?")
            {
                RenderHelpTable(DiceList);
                continue;
            }
            else if (int.TryParse(input, out int idx))
            {
                if (idx >= 0 && idx < DiceList.Count && !unavailable.Contains(idx))
                {
                    Console.WriteLine($"You choose the [{DiceList[idx]}] dice.");
                    return idx;
                }
                else
                {
                    Console.WriteLine("Invalid selection or dice unavailable.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Enter dice index, ?, or X.");
            }
        }
    }

    // Generates a random roll using cryptographic random number
    public int FairRoll(Dice dice)
    {
        Console.WriteLine($"Rolling a fair random number between 0 and {dice.SidesCount - 1}...");
        for (int i = 0; i < dice.SidesCount; i++)
            Console.WriteLine($"{i} - {i}");
        Console.WriteLine("X - exit");
        Console.WriteLine("? - help");

        while (true)
        {
            Console.Write("Your selection: ");
            string input = (Console.ReadLine() ?? "").Trim().ToLower();

            if (input == "x")
                Environment.Exit(0);

            if (input == "?")
            {
                RenderHelpTable(DiceList);
                continue;
            }

            if (int.TryParse(input, out int userValue))
            {
                if (userValue >= 0 && userValue < dice.SidesCount)
                {
                    int computerValue = RandomNumberGenerator.GetInt32(dice.SidesCount);
                    int result = (computerValue + userValue) % dice.SidesCount;
                    Console.WriteLine($"Computer's number is {computerValue}.");
                    Console.WriteLine($"The fair number generation result is {computerValue} + {userValue} = {result} (mod {dice.SidesCount}).");
                    Console.WriteLine($"Your roll result is {dice.Sides[result]}.");
                    return dice.Sides[result];
                }
            }
            Console.WriteLine("Invalid input. Enter a number from the list, ?, or X.");
        }
    }

    // Main game loop
    public void Play()
    {
        int firstPlayer = DetermineFirstPlayer();

        HashSet<int> usedDice = new HashSet<int>();

        int computerDice = firstPlayer == 1 ? UserSelectDice(usedDice) : RngSelectDice(usedDice);
        usedDice.Add(computerDice);

        int userDice = firstPlayer == 0 ? UserSelectDice(usedDice) : RngSelectDice(usedDice);
        usedDice.Add(userDice);

        Console.WriteLine(firstPlayer == 1
            ? $"I make the first move and choose the [{DiceList[computerDice]}] dice."
            : $"You make the first move and choose the [{DiceList[userDice]}] dice.");

        // Play rounds
        Console.WriteLine("It's time for my roll.");
        int computerRoll = FairRoll(DiceList[computerDice]);

        Console.WriteLine("It's time for your roll.");
        int userRoll = FairRoll(DiceList[userDice]);

        if (userRoll > computerRoll)
            Console.WriteLine($"You win ({userRoll} > {computerRoll})!");
        else if (computerRoll > userRoll)
            Console.WriteLine($"I win ({computerRoll} > {userRoll})!");
        else
            Console.WriteLine("It's a tie!");
    }

    // Random dice pick for computer
    private int RngSelectDice(HashSet<int> unavailable)
    {
        var available = new List<int>();
        for (int i = 0; i < DiceList.Count; i++)
        {
            if (!unavailable.Contains(i))
                available.Add(i);
        }
        return available[RandomNumberGenerator.GetInt32(available.Count)];
    }

    // Uses ConsoleTables to render the probability table
    public static void RenderHelpTable(List<Dice> diceList)
    {
        int n = diceList.Count;
        var headers = new List<string> { " " };
        headers.AddRange(diceList.Select((_, i) => $"D{i}"));
        var table = new ConsoleTable(headers.ToArray());
        for (int i = 0; i < n; i++)
        {
            var row = new List<string> { $"D{i}" };
            for (int j = 0; j < n; j++)
            {
                if (i == j)
                    row.Add("-");
                else
                    row.Add($"{ProbabilityCalculator.ProbabilityWin(diceList[i], diceList[j]):0.00}");
            }
            table.AddRow(row.ToArray());
        }
        Console.WriteLine("\nWinning Probability Table (Row beats Column):");
        table.Write();
        Console.WriteLine();
    }
}