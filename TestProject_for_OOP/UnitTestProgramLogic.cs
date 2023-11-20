using Lab_7_OOP_2023;

namespace TestProject_for_OOP
{
    [TestClass]
    public class UnitTestWall
    {
        [TestMethod]
        public void AddNewWall_AddsWallWithDefaultValues()
        {
            // Arrange
            var wallManager = new WallManager();

            // Act
            wallManager.AddNewWall(Colours.Blue);

            // Assert
            var lastWall = wallManager.GetLastWallId();
            var walls = wallManager.GetWallList();
            var wall = walls.FirstOrDefault(w => w.id == lastWall);

            Assert.IsNotNull(wall, "Wall should not be null");
            Assert.AreEqual(Colours.Blue, wall.colour, "Colours should match");
            Assert.AreEqual(100, wall.brightness, "Default brightness should be 100");
            Assert.AreEqual('a', wall.letter, "Default letter should be 'a'");
        }

        [TestMethod]
        public void AddNewWall_AddsProvidedWallObject()
        {
            // Arrange
            var wallManager = new WallManager();
            var wall = new WallClass(1, Colours.Red, 50, 'x');

            // Act
            wallManager.AddNewWall(wall);

            // Assert
            var walls = wallManager.GetWallList();
            Assert.IsTrue(walls.Contains(wall), "The provided wall object should be added to the list");
        }

        [TestMethod]
        public void FillWallListRandomly_FillsListWithRandomWalls()
        {
            // Arrange
            var wallManager = new WallManager();
            int numberOfWallsToAdd = 5;

            // Act
            wallManager.FillWallListRandomly(numberOfWallsToAdd);

            // Assert
            var walls = wallManager.GetWallList();
            Assert.AreEqual(numberOfWallsToAdd, walls.Count, $"There should be {numberOfWallsToAdd} walls in the list");
        }

        [TestMethod]
        public void SearchWallById_ReturnsCorrectWall()
        {
            // Arrange
            var wallManager = new WallManager();
            var wall = new WallClass(1, Colours.Red, 50, 'x');
            wallManager.AddNewWall(wall);

            // Act
            var foundWall = wallManager.SearchWallById(1);

            // Assert
            Assert.AreEqual(wall, foundWall, "The search should return the correct wall by id");
        }

        [TestMethod]
        public void SearchWallByColour_ReturnsAllWallsOfSpecificColour()
        {
            // Arrange
            var wallManager = new WallManager();
            wallManager.AddNewWall(Colours.Red, 70, 'r');
            wallManager.AddNewWall(Colours.Blue, 30, 'b');
            wallManager.AddNewWall(Colours.Red, 50, 'x');
            wallManager.AddNewWall(Colours.Green, 90, 'g');

            // Act
            var redWalls = wallManager.SearchWallByColour(Colours.Red);
            var blueWalls = wallManager.SearchWallByColour(Colours.Blue);

            // Assert
            Assert.AreEqual(2, redWalls.Count, "There should be exactly 2 red walls");
            Assert.IsTrue(redWalls.All(w => w.colour == Colours.Red), "All found walls should be red");
            Assert.AreEqual(1, blueWalls.Count, "There should be exactly 1 blue wall");
            Assert.IsTrue(blueWalls.All(w => w.colour == Colours.Blue), "All found walls should be blue");

            // Also verify that walls of a colour not added are not found
            var yellowWalls = wallManager.SearchWallByColour(Colours.Yellow);
            Assert.AreEqual(0, yellowWalls.Count, "There should be no yellow walls");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetLastWallId_ThrowsExceptionIfNoWalls()
        {
            // Arrange
            var wallManager = new WallManager();

            // Act
            var lastId = wallManager.GetLastWallId();

            // Assert is handled by ExpectedException
        }
    }
}