using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_7_OOP_2023
{
    class Program
    {
        //Extensions (Методи розширення, щоб не писати додатковий код у Main)
        static int CheckIntInput(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Incorrect input. Please enter a valid integer.");
                }
            }
        }

        static Colours chooseColour()
        {
            Colours[] availableColours = (Colours[])Enum.GetValues(typeof(Colours));
            for (int i = 1; i <= availableColours.Length; i++) Console.WriteLine($"{i} - {availableColours[i - 1]}");
            while (true)
            {
                int num = CheckIntInput("Choose colour for wall:");
                if (num > 0 && num <= availableColours.Length) return (Colours)(--num);
                else Console.WriteLine($"Incorrect colour. Please, choose colour between 1-{availableColours.Length}");
            }
        }

        static double chooseBrightness()
        {
            Console.WriteLine("Choose 1-100 percent of brightness");
            while (true)
            {
                string temp = Console.ReadLine();
                double num;
                if (double.TryParse(temp, out num) && num >= 1 && num <= 100) return num;
                else Console.WriteLine("Incorrect answer. Please, choose brightness between 1-100");
            }
        }

        static char chooseLetter()
        {
            Console.WriteLine("Choose letter for wall");
            char letter;
            while (true)
            {
                string temp = Console.ReadLine();
                if (char.TryParse(temp, out letter) && char.IsLetter(letter)) return letter;
                else Console.WriteLine("Incorrect answer. Please, write any letter");
            }
        }

        static void Main(string[] args)
        {
            string path = "fileForSerialization"; //Шлях для збереження серіалізації
            WallManager wallManager = new WallManager();
            bool exit = false;
            while (!exit)
            {
                switch (CheckIntInput("Choose what you wanna do:" +
                    "\n1 - Add wall" +
                    "\n2 - Show walls" +
                    "\n3 - Search wall" +
                    "\n4 - Delete wall" +
                    "\n5 - Change wall" +
                    "\n6 - Actions with wall" +
                    "\n7 - Serialize walls" +
                    "\n8 - Deserialize walls" +
                    "\n9 - Clear walls" +
                    "\n0 - Exit"))
                {
                    case 1:
                        Console.Clear();
                        int numWalls = CheckIntInput("How much walls do you wanna create?");
                        switch (CheckIntInput("How you wanna add objects?" +
                            "\n1 - Manual wall entry" +
                            "\n2 - Random wall entry" +
                            "\n3 - Copy object" +
                            "\n4 - Plain add (Just default properties)" +
                            "\nAny other number - Exit"))
                        {
                            case 1: //Add wall
                                if (numWalls >= 1)
                                {
                                    for (int i = 0; i < numWalls; i++)
                                    {
                                        Colours colour = chooseColour();

                                        Console.WriteLine("Do you wanna choose brightness and letter? By default it has 100% and letter 'a'\n1 - Yes\nAny other answear - No");
                                        string temp = Console.ReadLine();
                                        int num;
                                        if (int.TryParse(temp, out num) && num == 1)
                                        {
                                            double brightness = chooseBrightness();
                                            char letter = chooseLetter();

                                            WallClass wallDetailed = new WallClass();
                                            WallClass.TryParse($"{colour};{brightness};{letter}", out wallDetailed); //Використання TryParse

                                            wallManager.AddNewWall(wallDetailed);
                                        }
                                        else
                                        {
                                            wallManager.AddNewWall(colour);
                                        }
                                        Console.WriteLine("Wall with id " + wallManager.GetLastWallId() + " created!");
                                    }
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Incorrect answear. Please, choose more than 0");
                                }
                                break;
                            case 2: 
                                wallManager.FillWallListRandomly(numWalls);
                                break;
                            case 3: 
                                if (!wallManager.WallExists()) { Console.WriteLine("Sorry, you don't have any walls"); break; }
                                wallManager.ForEach(value => value.printWallDetailed());
                                WallClass tempCopy = new WallClass(wallManager.SearchWallById(CheckIntInput("Write id of the wall to copy")));
                                wallManager.AddNewWall(tempCopy);
                                break;
                            case 4:
                                WallClass tempPlain = new WallClass();
                                wallManager.AddNewWall(tempPlain);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 2: //Show wall
                        Console.Clear();
                        if (!wallManager.WallExists()) { Console.WriteLine("Sorry, you don't have any walls"); break; }
                        Console.WriteLine(WallClass.ObjectCount + " walls\n" + WallClass.CountBrightWalls(wallManager.GetWallList()) + " light walls\n");
                        wallManager.ForEach(value => value.printWallDetailed());
                        break;
                    case 3: //Search wall
                        Console.Clear();
                        if (!wallManager.WallExists()) { Console.WriteLine("Sorry, you don't have any walls"); break; }
                        while (true)
                        {
                            switch (CheckIntInput("How you wanna search objects?\n1 - by id\n2 - by colour"))
                            {
                                case 1:
                                    int wallId = CheckIntInput("Write id of the wall");
                                    if (wallManager.WallExists(wallId))
                                    {
                                        wallManager.SearchWallById(wallId).printWallDetailed();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Wall with the given ID was not found.");
                                    }
                                    break;
                                case 2:
                                    wallManager.SearchWallByColour(chooseColour()).ForEach(wall => wall.printWallDetailed());
                                    break;
                            }
                            break;
                        }
                        break;
                    case 4: //Delete wall
                        Console.Clear();
                        if (!wallManager.WallExists()) { Console.WriteLine("Sorry, you don't have any walls"); break; }
                        wallManager.ForEach(value => value.printWallDetailed()); //Виведення стін на екран для комфорту
                        while (true)
                        {
                            switch (CheckIntInput("How you wanna delete objects?\n1 - by id\n2 - by colour"))
                            {
                                case 1:
                                    int idToDelete = CheckIntInput("Write id of the wall");
                                    if (wallManager.WallExists(idToDelete))
                                    {
                                        WallClass temp = wallManager.SearchWallById(idToDelete);
                                        temp.Dispose();
                                        wallManager.Remove(temp);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Wall with the given ID was not found.");
                                    }
                                    break;
                                case 2:
                                    wallManager.SearchWallByColour(chooseColour()).ForEach(wall => { wall.Dispose(); wallManager.Remove(wall); });
                                    break;
                            }
                            GC.Collect(); //Спроба вилучити об'єкти з програми, бо вони все ще займають пам'ять
                            break;
                        }
                        break;
                    case 5: //Change wall
                        Console.Clear();
                        if (!wallManager.WallExists()) { Console.WriteLine("Sorry, you don't have any walls"); break; }
                        wallManager.ForEach(value => value.printWallDetailed()); //Виведення стін на екран для комфорту
                        while (true)
                        {
                            int idWallChange = CheckIntInput("Write id of the wall to change");
                            if (wallManager.WallExists(idWallChange))
                            {
                                int index = wallManager.FindIndex(wall => wall.id == idWallChange);
                                wallManager[index].printWallDetailed();
                                switch (CheckIntInput("Which property do you wanna change?\n1 - Colour\n2 - Brightness\n3 - Letter"))
                                {
                                    case 1:
                                        wallManager[index].changeColour(chooseColour());
                                        break;
                                    case 2:
                                        wallManager[index].changeBrightness(chooseBrightness());
                                        break;
                                    case 3:
                                        wallManager[index].changeLetter(chooseLetter());
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            }
                            else Console.WriteLine("This wall doesn't exist. Please, write another id");
                        }
                        break;
                    case 6: //Actions with wall
                        Console.Clear();
                        if (!wallManager.WallExists()) { Console.WriteLine("Sorry, you don't have any walls"); break; }
                        switch (CheckIntInput("What action do you want to perform?" +
                            "\n1 - Check if the wall is bright" +
                            "\n2 - Show wall with an error" +
                            "\n3 - Count the number of bright walls" +
                            "\n4 - Show all bright walls" +
                            "\nAny other number - Exit"))
                        {
                            case 1:
                                int idWall = CheckIntInput("Choose the wall's id for the check: ");
                                if (wallManager.WallExists(idWall))
                                {
                                    WallClass selectedWall = wallManager.SearchWallById(idWall);
                                    if (WallClass.IsLightBrightStatic(selectedWall))
                                    {
                                        Console.WriteLine("You have chosen a bright wall.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("You have chosen a dark wall.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("This wall does not exist. Please enter another id.");
                                }
                                break;
                            case 2:
                                while (true)
                                {
                                    int idWallCheck = CheckIntInput("Enter the wall's id");
                                    if (wallManager.WallExists(idWallCheck))
                                    {
                                        wallManager.SearchWallById(idWallCheck).printWallDetailed(CheckIntInput("Enter the error number for this wall"));
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("This wall does not exist. Please enter another id.");
                                    }
                                }
                                break;
                            case 3:
                                Console.WriteLine("Number of bright walls: " + WallClass.CountBrightWalls(wallManager.GetWallList()));
                                break;
                            case 4:
                                List<WallClass> brightWalls = wallManager.Where(wall => wall.IsLightBright).ToList();
                                Console.WriteLine("Selected all the bright walls:");
                                brightWalls.ForEach(wall => wall.printWallDetailed());
                                break;
                            default:
                                break;
                        }
                        break;
                    case 7: //Serialize to file
                        Console.Clear();
                        if (!wallManager.WallExists()) { Console.WriteLine("Sorry, you don't have any walls"); break; }
                        switch (CheckIntInput("How you wanna serialize walls?" +
                            "\n1 - json" +
                            "\n2 - binary (txt)" +
                            "\n3 - csv" +
                            "\nAny other number - Exit"))
                        {
                            case 1: //json
                                ClassSerializeManager.SerialiazeToJson(ref wallManager, path + ".json");
                                Console.WriteLine("Serialization successful!");
                                break;
                            case 2: //binary
                                ClassSerializeManager.SerializeToBinary(ref wallManager, path + ".txt");
                                Console.WriteLine("Serialization successful!");
                                break;
                            case 3: //csv
                                ClassSerializeManager.SerializeToCsv(wallManager, path + ".csv");
                                Console.WriteLine("Serialization successful!");
                                break;
                            default:
                                break;
                        }
                        break; 
                    case 8: //Deserialize from file 
                        Console.Clear();
                        WallManager tempWalls = new WallManager();
                        switch (CheckIntInput("How you wanna deserialize walls?" +
                            "\n1 - json" +
                            "\n2 - binary (txt)" +
                            "\n3 - csv" +
                            "\nAny other number - Exit"))
                        {
                            case 1: //json
                                ClassSerializeManager.DeserializeFromJson(ref tempWalls, path + ".json");
                                break;
                            case 2: //binary
                                ClassSerializeManager.DeserializeFromBinary(ref tempWalls, path + ".txt");
                                break;
                            case 3: //csv
                                ClassSerializeManager.DeserializeFromCsv(ref tempWalls, path + ".csv");
                                WallClass.setObjectCount(wallManager.Count());
                                WallClass.reloadMaxId();
                                break;
                            default:
                                break;
                        }
                        wallManager.MergeWith(tempWalls);
                        Console.WriteLine("Deserialization successful!");
                        WallClass.setObjectCount(wallManager.Count());
                        WallClass.reloadMaxId();
                        break;
                    case 9: //Clear list
                        wallManager.ClearList();
                        break;
                    case 0: //Exit
                        exit = true;
                        break;
                }
            }
        }
    }
}
