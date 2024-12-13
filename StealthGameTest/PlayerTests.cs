using Moq;
using StealthGameAvalonia.Model.Utils;
using StealthGameAvalonia.Model.Entities;
using StealthGameAvalonia.Model;
using StealthGameAvalonia.Persistence;

namespace StealthGameTest
{
    [TestClass]
    public class PlayerTests
    {

        private ITextFileManager TextFileManagerMock
        {
            get
            {
                var mock = new Mock<ITextFileManager>();
                mock.Setup(t => t.Load()).Returns(["test", "5,5", "p,0,0", "w, 2,2"]);
                return mock.Object;
            }
        }
        [TestMethod]
        public void TestMove()
        {
            Level level = new(TextFileManagerMock);
            Assert.IsNotNull(level.Board[0, 0].Player);
            Player p = level.Board[0, 0].Player!;
            (int r, int c) = p.SimulateMove(Direction.DOWN);
            p.Move(r, c);
            Assert.AreEqual(p.Row, 1);
            Assert.AreEqual(p.Col, 0);
            (r, c) = p.SimulateMove(Direction.DOWN);
            p.Move(r, c);
            Assert.AreEqual(p.Row, 2);
            Assert.AreEqual(p.Col, 0);
            (r, c) = p.SimulateMove(Direction.DOWN);
            p.Move(r, c);
            Assert.AreEqual(p.Row, 3);
            Assert.AreEqual(p.Col, 0);
            (r, c) = p.SimulateMove(Direction.DOWN);
            p.Move(r, c);
            Assert.AreEqual(p.Row, 4);
            Assert.AreEqual(p.Col, 0);
            //The player has no knowledge of the map

            (r, c) = p.SimulateMove(Direction.DOWN);
            p.Move(r, c);
            Assert.AreEqual(p.Row, 5);
            Assert.AreEqual(p.Col, 0);

        }

        [TestMethod]
        public void TestSimMove()
        {
            Level level = new(TextFileManagerMock);
            Assert.IsNotNull(level.Board[0, 0].Player);
            Player p = level.Board[0, 0].Player!;
            (int r, int c) = p.SimulateMove(Direction.UP);
            p.Move(r, c);
            Assert.AreEqual(p.Row, -1);
            Assert.AreEqual(p.Col, 0);
            (r, c) = p.SimulateMove(Direction.RIGHT);
            p.Move(r, c);

            Assert.AreEqual(p.Row, -1);
            Assert.AreEqual(p.Col, 1);
            (r, c) = p.SimulateMove(Direction.RIGHT);
            p.Move(r, c);

            Assert.AreEqual(p.Row, -1);
            Assert.AreEqual(p.Col, 2);
            (r, c) = p.SimulateMove(Direction.RIGHT);
            p.Move(r, c);

            Assert.AreEqual(p.Row, -1);
            Assert.AreEqual(p.Col, 3);
            (r, c) = p.SimulateMove(Direction.DOWN);
            p.Move(r, c);

            Assert.AreEqual(p.Row, 0);
            Assert.AreEqual(p.Col, 3);
            (r, c) = p.SimulateMove(Direction.DOWN);
            p.Move(r, c);

            Assert.AreEqual(p.Row, 1);
            Assert.AreEqual(p.Col, 3);
            (r, c) = p.SimulateMove(Direction.DOWN);
            p.Move(r, c);

            Assert.AreEqual(p.Row, 2);
            Assert.AreEqual(p.Col, 3);
            (r, c) = p.SimulateMove(Direction.LEFT);
            p.Move(r, c);

            Assert.AreEqual(p.Row, 2);
            Assert.AreEqual(p.Col, 2);

            (r, c) = p.SimulateMove(Direction.LEFT);
            p.Move(r, c);

            Assert.AreEqual(p.Row, 2);
            Assert.AreEqual(p.Col, 1);
            (r, c) = p.SimulateMove(Direction.LEFT);
            p.Move(r, c);

            Assert.AreEqual(p.Row, 2);
            Assert.AreEqual(p.Col, 0);
            (r, c) = p.SimulateMove(Direction.UP);
            p.Move(r, c);

            Assert.AreEqual(p.Row, 1);
            Assert.AreEqual(p.Col, 0);
            (r, c) = p.SimulateMove(Direction.UP);
            p.Move(r, c);

            Assert.AreEqual(p.Row, 0);
            Assert.AreEqual(p.Col, 0);

        }
    }
}
