using System;
using System.IO;

class CompletedAssignmet2
{
    static string[] lines;

    static void Main()  
    {
        string filePath = "input.csv";
        lines = File.ReadAllLines(filePath);

        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Display Characters");
            Console.WriteLine("2. Add Character");
            Console.WriteLine("3. Level Up Character");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                DisplayAllCharacters(lines);
            }
            else if (choice == "2")
            {
                AddCharacter(ref lines);
                File.WriteAllLines(filePath, lines);
            }
            else if (choice == "3")
            {
                LevelUpCharacter(lines);
                File.WriteAllLines(filePath, lines); 
            }
            else if (choice == "4")
            {
                return;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }

    static void DisplayAllCharacters(string[] lines)
    {
        for (int i = 1; i < lines.Length; i++) 
        {
            string line = lines[i];
            string name = "";
            string characterClass = "";
            int level = 0;
            int hitPoints = 0;
            string[] equipment;

            if (line.StartsWith("\""))
            {
                int closingQuoteIndex = line.IndexOf('"', 1);
                name = line.Substring(1, closingQuoteIndex - 1);

                string rest = line.Substring(closingQuoteIndex + 2);
                string[] fields = rest.Split(',');

                characterClass = fields[0];
                level = int.Parse(fields[1]);
                hitPoints = int.Parse(fields[2]);
                equipment = fields[3].Split('|');
            }
            else
            {
                string[] fields = line.Split(',');
                name = fields[0];
                characterClass = fields[1];
                level = int.Parse(fields[2]);
                hitPoints = int.Parse(fields[3]);
                equipment = fields[4].Split('|');
            }

            Console.WriteLine($"Name: {name}, Class: {characterClass}, Level: {level}, HP: {hitPoints}, Equipment: {string.Join(", ", equipment)}");
        }
    }

    static void AddCharacter(ref string[] lines)
    {
        Console.Write("Enter character name: ");
        string name = Console.ReadLine();
        if (name.Contains(","))
        { 
            name = "\"" + name + "\"";
        }

        Console.Write("Enter character class: ");
        string characterClass = Console.ReadLine();

        Console.Write("Enter level: ");
        int level = int.Parse(Console.ReadLine());

        Console.Write("Enter hit points: ");
        int hitPoints = int.Parse(Console.ReadLine());

        Console.Write("Enter equipment items separated by commas: ");
        string equipmentInput = Console.ReadLine();
        string[] equipment = equipmentInput.Split(',');
        for (int i = 0; i < equipment.Length; i++)
        {
            equipment[i] = equipment[i].Trim();
        }

        string newLine = name + "," + characterClass + "," + level + "," + hitPoints + "," + string.Join("|", equipment);

        Array.Resize(ref lines, lines.Length + 1);
        lines[lines.Length - 1] = newLine;

        Console.WriteLine("Character added successfully!");
    }

    static void LevelUpCharacter(string[] lines)
    {
        Console.Write("Enter the name of the character to level up: ");
        string nameToLevelUp = Console.ReadLine();

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string name = "";
            string characterClass = "";
            int level = 0;
            int hitPoints = 0;
            string equipmentField = "";

            if (line.StartsWith("\""))
            {
                int closingQuoteIndex = line.IndexOf('"', 1);
                name = line.Substring(1, closingQuoteIndex - 1);
                string rest = line.Substring(closingQuoteIndex + 2);
                string[] fields = rest.Split(',');

                characterClass = fields[0];
                level = int.Parse(fields[1]);
                hitPoints = int.Parse(fields[2]);
                equipmentField = fields[3];
            }
            else
            {
                string[] fields = line.Split(',');
                name = fields[0];
                characterClass = fields[1];
                level = int.Parse(fields[2]);
                hitPoints = int.Parse(fields[3]);
                equipmentField = fields[4];
            }

            if (name == nameToLevelUp)
            {
                level++;
                Console.WriteLine("Character " + name + " leveled up to level " + level + "!");

                if (line.StartsWith("\""))
                {
                    lines[i] = "\"" + name + "\"," + characterClass + "," + level + "," + hitPoints + "," + equipmentField;
                }
                else
                {
                    lines[i] = name + "," + characterClass + "," + level + "," + hitPoints + "," + equipmentField;
                }
                return;
            }
        }

        Console.WriteLine("Character not found.");
    }
}
