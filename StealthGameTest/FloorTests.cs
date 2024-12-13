using Moq;

using StealthGameAvalonia.Model;
using StealthGameAvalonia.Model.Entities;
using StealthGameAvalonia.Model.Floors;
using StealthGameAvalonia.Persistence;

namespace StealthGameTest
{
    [TestClass]
    public class FloorTests
    {

        private ITextFileManager TextFileManagerMock
        {
            get
            {
                var mock = new Mock<ITextFileManager>();
                mock.Setup(t => t.Load()).Returns(["test", "1,1", "p,0,0"]);
                return mock.Object;
            }
        }

        [TestMethod]
        public void TestAddE()
        {
            Level level = new(TextFileManagerMock);
            Floor f = new Floor(0, 0);
            Exit e = new Exit(0, 0);
            Wall w = new Wall(0, 0);

            Guard g = new(0, 0);
            f.AddEntity(g);
            Assert.IsNotNull(f.Guard);
            Assert.IsNotNull(f.Entity);
            Assert.AreEqual(f.Guard, g);


            w.AddEntity(g);
            Assert.IsNull(w.Guard);
            Assert.IsNull(w.Entity);

        }

        [TestMethod]
        public void TestRemE()
        {
            Level level = new(TextFileManagerMock);
            Floor f = new(1, 1);
            Guard g1 = new Guard(1, 1);
            Guard g2 = new Guard(1, 1);
            Guard g3 = new Guard(1, 1);

            f.RemoveEntity();
            Assert.IsNull(f.Entity);
            f.AddEntity(g1);
            f.AddEntity(g2);
            f.AddEntity(g3);
            Assert.AreEqual(f.Guard, g1);
            f.RemoveEntity();
            f.AddEntity(g2);
            Assert.AreEqual(f.Guard, g2);


        }

        [TestMethod]
        public void TestGetGuard()
        {
            Level level = new(TextFileManagerMock);
            Floor f = new(1, 1);
            Guard g1 = new Guard(1, 1);
            f.AddEntity(g1);
            Assert.IsNotNull(f.Guard);
            Assert.AreEqual(f.Guard, g1);
        }

        [TestMethod]
        public void TestGetPlayer()
        {
            Level level = new(TextFileManagerMock);
            Assert.IsNotNull(level.Player);
            Assert.AreEqual(level.Player.Row, 0);
            Assert.AreEqual(level.Player.Col, 0);
            Assert.IsTrue(level.Board[0, 0].Entity is Player);

        }
    }
}
