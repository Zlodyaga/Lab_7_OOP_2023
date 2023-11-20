using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;

namespace Lab_7_OOP_2023
{
    public static class ClassSerializeManager
    {
        // Серіалізація Json

        public static void SerialiazeToJson<T>(ref T inObject, string inFileName)
        {
                File.Delete(inFileName);
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                using (FileStream stream1 = new FileStream(inFileName, FileMode.OpenOrCreate))
                {
                    ser.WriteObject(stream1, inObject);
                    stream1.Close();
                }
        }

        public static void DeserializeFromJson<T>(ref T inObject, string inFileName)
        {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                using (FileStream stream1 = new FileStream(inFileName, FileMode.Open))
                {
                    inObject = (T)ser.ReadObject(stream1);
                    stream1.Close();
                }
        }

        // Серіалізація Binary

        public static void SerializeToBinary<T>(ref T inObject, string inFileName)
        {
                File.Delete(inFileName);
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream1 = new FileStream(inFileName, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(stream1, inObject);
                    stream1.Close();
                }
        }

        public static void DeserializeFromBinary<T>(ref T inObject, string inFileName)
        {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream1 = new FileStream(inFileName, FileMode.OpenOrCreate))
                {
                    inObject = (T)formatter.Deserialize(stream1);
                    stream1.Close();
                }
        }

        // Серіалізація Csv

        public static void SerializeToCsv(WallManager wallManager, string fileName)
        {
            var csv = new StringBuilder();

            foreach (var wall in wallManager)
            {
                csv.AppendLine(wall.ToString());
            }
            WallClass.reloadMaxId();
            File.WriteAllText(fileName, csv.ToString());
        }

        public static void DeserializeFromCsv(ref WallManager wallManager, string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            foreach (var line in lines)
            {
                if (WallClass.TryParse(line, out WallClass wall))
                {
                    wallManager.AddNewWall(wall);
                }
            }
        }
    }
}
