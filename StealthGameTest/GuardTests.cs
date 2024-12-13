using Moq;
using StealthGameAvalonia.Model;
using StealthGameAvalonia.Model.Entities;
using StealthGameAvalonia.Model.Utils;
using StealthGameAvalonia.Persistence;
using System.Diagnostics;

namespace StealthGameTest
{
    [TestClass]
    public class GuardTests
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
        public void TestMove()
        {
            Level level = new(TextFileManagerMock);
            Guard g1 = new(1,1);
            (int, int) lastPos = (g1.Row, g1.Col);
            (int r, int c) = g1.SimulateMove();
            g1.Move(r, c);
            Assert.AreNotEqual(lastPos, (g1.Row, g1.Col));
        }

        [TestMethod]
        public void TestSimMove()
        {
            Level level = new(TextFileManagerMock);
            Guard g1 = new(1,1);
            (int r, int c) = g1.SimulateMove();
            bool pass = (r == 0 && c == 1) || (r == 1 && c == 2) || (r == 1 && c == 0) || (r == 2 && c == 1);
            Assert.IsTrue(pass);
        }
    }
}
