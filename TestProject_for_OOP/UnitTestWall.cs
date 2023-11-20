using Lab_7_OOP_2023;

namespace TestProject_for_OOP
{
    [TestClass]
    public class UnitTestWallClass
    {
        [TestMethod]
        public void TestWallCreation_DefaultConstructor()
        {
            int initialCount = WallClass.ObjectCount;

            WallClass wall = new WallClass();

            Assert.AreEqual(initialCount + 1, WallClass.ObjectCount);
        }

        [TestMethod]
        public void TestWallCreation_ParametersConstructor()
        {
            int initialCount = WallClass.ObjectCount;

            WallClass wall = new WallClass(1, Colours.Red, 60, 'b');

            Assert.AreEqual(Colours.Red, wall.colour);
            Assert.IsTrue(wall.IsLightBright);
            Assert.AreEqual(initialCount + 1, WallClass.ObjectCount);
        }

        [TestMethod]
        public void TestWallCopyConstructor()
        {
            WallClass originalWall = new WallClass(1, Colours.Blue, 80, 'c');

            WallClass copiedWall = new WallClass(originalWall);

            Assert.AreEqual(originalWall.colour, copiedWall.colour);
            Assert.AreEqual(originalWall.IsLightBright, copiedWall.IsLightBright);
        }

        [TestMethod]
        public void TestCountBrightWalls()
        {
            List<WallClass> walls = new List<WallClass>
            {
                new WallClass(1, Colours.Red, 70, 'a'),
                new WallClass(2, Colours.Blue, 40, 'b'),
                new WallClass(3, Colours.Green, 55, 'c'),
            };

            int brightWallCount = WallClass.CountBrightWalls(walls);

            Assert.AreEqual(2, brightWallCount);
        }

        [TestMethod]
        public void TestParseWallFromString()
        {
            string data = "Red;75,6;a";
            WallClass wall = WallClass.Parse(data);

            Assert.AreEqual(Colours.Red, wall.colour);
            Assert.AreEqual('a', wall.letter);
            Assert.IsTrue(wall.IsLightBright);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestParseWallFromInvalidString()
        {
            string invalidData = "Red;75,6";
            WallClass.Parse(invalidData);
        }

        [TestMethod]
        public void TestTryParseWallFromString_Valid()
        {
            string data = "Red;75,6;a";
            bool isParsed = WallClass.TryParse(data, out WallClass wall);

            Assert.IsTrue(isParsed);
            Assert.AreEqual(Colours.Red, wall.colour);
        }

        [TestMethod]
        public void TestTryParseWallFromString_Invalid()
        {
            string invalidData = "Red;75,6";
            bool isParsed = WallClass.TryParse(invalidData, out WallClass wall);

            Assert.IsFalse(isParsed);
            Assert.IsNull(wall);
        }
        
        [TestMethod]
        public void TestWallToStringMethod()
        {
            var wall = new WallClass(1, Colours.Red, 65.54, 'b');
            var result = wall.ToString();
            Assert.AreEqual("Red;65,54;b", result);
        }

        [TestMethod]
        public void TestChangeWallProperties()
        {
            WallClass wall = new WallClass(1, Colours.Red);

            wall.changeColour(Colours.Green);
            wall.changeBrightness(30);
            wall.changeLetter('d');

            Assert.AreEqual(Colours.Green, wall.colour);
            Assert.AreEqual('d', wall.letter);
            Assert.IsFalse(wall.IsLightBright);
        }

        [TestMethod]
        public void TestWallDispose()
        {
            int initialCount = WallClass.ObjectCount;
            WallClass wall = new WallClass();
            wall.Dispose();

            Assert.AreEqual(initialCount, WallClass.ObjectCount);
        }
    }
}
