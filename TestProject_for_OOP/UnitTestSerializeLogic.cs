using Lab_7_OOP_2023;

namespace TestProject_for_OOP
{
    [TestClass]
    public class UnitTestSerializeLogic
    {
        //json

        [TestMethod]
        public void SerializeToJson_CreatesValidJsonFile()
        {
            // Arrange
            WallManager wallManager = new WallManager();
            wallManager.AddNewWall(Colours.Blue);
            string fileName = "wallManager.json";

            // Act
            ClassSerializeManager.SerialiazeToJson(ref wallManager, fileName);

            // Assert
            Assert.IsTrue(File.Exists(fileName), "JSON file should exist after serialization");
        }

        [TestMethod]
        public void DeserializeFromJson_ReadsValidJsonFile()
        {
            // Arrange
            WallManager wallManager = new WallManager();
            string fileName = "wallManager.json";

            // Act
            ClassSerializeManager.DeserializeFromJson(ref wallManager, fileName);

            // Assert
            Assert.IsTrue(wallManager.WallExists(), "WallManager should have walls after deserialization");
        }

        //Binary

        [TestMethod]
        public void SerializeToBinary_CreatesValidBinaryFile()
        {
            // Arrange
            WallManager wallManager = new WallManager();
            wallManager.AddNewWall(Colours.Red);
            string fileName = "wallManager.dat";

            // Act
            ClassSerializeManager.SerializeToBinary(ref wallManager, fileName);

            // Assert
            Assert.IsTrue(File.Exists(fileName), "Binary file should exist after serialization");
        }

        [TestMethod]
        public void DeserializeFromBinary_ReadsValidBinaryFile()
        {
            // Arrange
            WallManager wallManager = new WallManager();
            string fileName = "wallManager.dat";

            // Act
            ClassSerializeManager.DeserializeFromBinary(ref wallManager, fileName);

            // Assert
            Assert.IsTrue(wallManager.WallExists(), "WallManager should have walls after deserialization");
        }

        //xml

        [TestMethod]
        public void SerializeToCsv_CreatesValidCsvFile()
        {
            // Arrange
            WallManager wallManager = new WallManager();
            wallManager.AddNewWall(Colours.Green);
            string fileName = "walls.csv";

            // Act
            ClassSerializeManager.SerializeToCsv(wallManager, fileName);

            // Assert
            Assert.IsTrue(File.Exists(fileName), "CSV file should exist after serialization");
        }

        [TestMethod]
        public void DeserializeFromCsv_ReadsValidCsvFile()
        {
            // Arrange
            WallManager wallManager = new WallManager();
            string fileName = "walls.csv";

            // Act
            ClassSerializeManager.DeserializeFromCsv(ref wallManager, fileName);

            // Assert
            Assert.IsTrue(wallManager.WallExists(), "WallManager should have walls after deserialization");
        }

    }
}
