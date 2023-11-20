namespace Lab_7_OOP_2023
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract]
    public class WallManager : IEnumerable<WallClass>
    {
        [DataMember] private List<WallClass> walls;

        public WallManager()
        {
            walls = new List<WallClass>();
        }

        public void AddNewWall(Colours colour, double brightness = 100, char letter = 'a')
        {
            int nextId = walls.Any() ? walls.Max(wall => wall.id) + 1 : 1;
            WallClass wall = new WallClass(nextId, colour, brightness, letter);
            walls.Add(wall);
        }

        public void AddNewWall(WallClass wall)
        {
            walls.Add(wall);
        }

        public void MergeWith(WallManager other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other), "Provided WallManager is null.");
            }
            other.ForEach(wall => wall.reloadObjectId());
            walls.AddRange(other.walls);
        }

        public void FillWallListRandomly(int count)
        {
            var random = new Random();
            Colours[] availableColours = (Colours[])Enum.GetValues(typeof(Colours));

            for (int i = 0; i < count; i++)
            {
                Colours randomColour = availableColours[random.Next(availableColours.Length)];
                double randomBrightness = random.NextDouble() * 100;
                char randomLetter = (char)('a' + random.Next(26));

                AddNewWall(randomColour, randomBrightness, randomLetter);
            }
        }

        public WallClass SearchWallById(int id)
        {
            return walls.Find(wall => wall.id == id);
        }

        public List<WallClass> SearchWallByColour(Colours colour)
        {
            return walls.Where(wall => wall.colour == colour).ToList();
        }

        public bool WallExists(int id)
        {
            return walls.Any(wall => wall.id == id);
        }

        public bool WallExists()
        {
            return walls.Any();
        }

        public int GetLastWallId()
        {
            if (walls.Count == 0)
            {
                throw new InvalidOperationException("No walls in the list.");
            }

            return walls.Last().id;
        }

        public List<WallClass> GetWallList()
        {
            return new List<WallClass>(walls);
        }

        public void ClearList()
        {
            walls.Clear();
        }

        public int Count() {  return walls.Count; }

        //Реалізація щодо імітації List

        public IEnumerator<WallClass> GetEnumerator()
        {
            foreach (var wall in walls)
            {
                yield return wall;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void ForEach(Action<WallClass> action)
        {
            foreach (var wall in walls)
            {
                action(wall);
            }
        }

        public bool Remove(WallClass wall)
        {
            return walls.Remove(wall);
        }

        public int FindIndex(Predicate<WallClass> match)
        {
            return walls.FindIndex(match);
        }

        // Індексатор для доступу до елементів списку через [num]
        public WallClass this[int index]
        {
            get
            {
                if (index >= 0 && index < walls.Count)
                {
                    return walls[index];
                }
                else
                {
                    throw new IndexOutOfRangeException("Index is out of range.");
                }
            }
            set
            {
                if (index >= 0 && index < walls.Count)
                {
                    walls[index] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("Index is out of range.");
                }
            }
        }
        // Тут можуть бути інші методи для управління стінами...
    }

}
