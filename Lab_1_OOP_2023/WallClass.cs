using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Lab_7_OOP_2023
{
    [Serializable]
    [DataContract]
    public class WallClass
    {
        [DataMember] public int id { get; private set; }
        [DataMember] public Colours colour { get; private set; } = (Colours)1;
        [DataMember] public double brightness { get; private set; } = 100;
        [DataMember] public char letter { get; private set; } = 'a';

        private static int maxId = 0;
        private static int objectCount = 0; //Для підрахунку всіх об'єктів

        public static int ObjectCount
        {
            get { return objectCount; }
        }

        public static void setObjectCount(int num) { objectCount = num; }

        public static void reloadMaxId() { maxId = objectCount; }

        public void reloadObjectId() { this.id = ++maxId; }

        public void Dispose() //Деконструктор неможна явно визвати у С#, отже робимо тимчасове вилучення з програми об'єкту
        {
            objectCount--;
        }
        
        // Обчислювальна властивість для визначення яскравості світла на стіні
        public bool IsLightBright
        {
            get
            {
                // Визначимо "яскраве" світло як те, яке має яскравість більше 50
                return brightness > 50;
            }
            private set { }
        }

        public static bool IsLightBrightStatic(WallClass wallClass) => wallClass.IsLightBright;

        public WallClass()
        {
            objectCount++;
        }

        public WallClass(int id, Colours colour, double brightness, char letter) : this(id, colour)
        {
            this.brightness = brightness;
            this.letter = letter;
        }

        public WallClass(int id, Colours colour)
        {
            objectCount++;
            this.id = id;
            this.colour = colour;
            if (id > maxId) maxId = id;
        }

        public WallClass(WallClass objectTemp)
        {
            objectCount++;
            this.id = ++maxId; //Уникаємо дублювання id
            this.colour = objectTemp.colour;
            this.brightness = objectTemp.brightness;
            this.letter = objectTemp.letter;
        }

        public static int CountBrightWalls(List<WallClass> walls) //Static метод для підрахунку світлих стін у листі
        {
            int count = 0;
            foreach (var wall in walls)
            {
                if (wall.IsLightBright)
                {
                    count++;
                }
            }
            return count;
        }

        public static WallClass Parse(string s)
        {
            string[] parts = s.Split(';');

            if (parts.Length != 3)
            {
                throw new ArgumentException("Invalid string format. Expected 3 parts separated by ';'");
            }

            try
            {
                Colours colour = (Colours)Enum.Parse(typeof(Colours), parts[0]);
                double brightness = double.Parse(parts[1]);
                char letter = char.Parse(parts[2]);

                maxId++;
                return new WallClass(maxId, colour, brightness, letter);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}"); // Добавим вывод ошибки
                throw new FormatException("Error parsing the string", ex);
            }
        }


        public static bool TryParse(string s, out WallClass result)
        {
            try
            {
                result = Parse(s);
                objectCount--;
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        public override string ToString()
        {
            return $"{(Colours)colour};{brightness.ToString("F2")};{letter}";
        }

        public void printWall()
        {
            Console.WriteLine("Wall " + this.id + " has " + (Colours)this.colour + " colour " + this.letter);
        }

        public void printWallDetailed()
        {
            Console.WriteLine("Wall " + this.id + " has " + (Colours)this.colour + " colour " + this.letter + " with brightness " + Math.Round(this.brightness, 2) + "%");
        }

        public void printWallDetailed(int errorNum)
        {
            Console.WriteLine("Wall " + this.id + " has " + (Colours)this.colour + " colour " + this.letter + " with brightness " + Math.Round(this.brightness, 2) + "% with error " + errorNum);
        }

        public Colours showColour()
        {
            return this.colour;
        }

        public void changeColour(Colours colour)
        {
            this.colour = colour;
        }

        public void changeBrightness(double brightness)
        {
            this.brightness = brightness;
        }

        public void changeLetter(char letter)
        {
            this.letter = letter;
        }
    }
}
