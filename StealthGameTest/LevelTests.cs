using Avalonia.Logging;
using Moq;
using StealthGameAvalonia.Model;
using StealthGameAvalonia.Model.Entities;
using StealthGameAvalonia.Model.Floors;
using StealthGameAvalonia.Model.Utils;
using StealthGameAvalonia.Persistence;
using System.Diagnostics;

namespace StealthGameTest
{
    [TestClass]
    public class LevelTests
    {
        private readonly string[][] files = [
            ["test", "10,10", "p,0,0", "g,2,2", "w,2,3", "w, 5, 8", "e, 7, 7"], 
            ["test", "10,10", "p,0,0", "g,2,2", "g,4,5","g,7,9","w,2,3", "w,5,8","w,6,6","w,9,2", "e,7,7"],
            ["test", "5,5", "p,0,0", "g,2,2"]
        ];
        private ITextFileManager MockTxtMgr(int fileNo)
        {
            var mock = new Mock<ITextFileManager>();
            mock.Setup(t => t.Load()).Returns(files[fileNo]);
            return mock.Object;
        }


        [TestMethod]
        public void TestCtor()
        {
            Level level = new Level(MockTxtMgr(0));
            Assert.IsTrue(level.Board[0, 0].Player != null);
            Assert.IsNotNull(level.Board[2, 2].Guard);
            Assert.IsInstanceOfType(level.Board[2, 3], typeof(Wall));
            Assert.IsInstanceOfType(level.Board[5, 8], typeof(Wall));
            Assert.IsInstanceOfType(level.Board[7, 7], typeof(Exit));
        }



        [TestMethod]
        public void TestValidateCoords()
        {
            Level level = new Level(MockTxtMgr(1));
            //checks bounds
            Assert.IsFalse(level.ValidateCoordForGuard(-1, -1));
            Assert.IsFalse(level.NoWallInSight(-1, -1));
            Assert.IsFalse(level.ValidateCoordForGuard(10, 10));
            Assert.IsFalse(level.NoWallInSight(10, 10));
            //checks walls
            Assert.IsFalse(level.ValidateCoordForGuard(2, 3));
            Assert.IsFalse(level.ValidateCoordForGuard(5, 8));
            Assert.IsFalse(level.NoWallInSight(2, 3));
            Assert.IsFalse(level.NoWallInSight(5, 8));
            //check exit
            Assert.IsFalse(level.ValidateCoordForGuard(7, 7));
            Assert.IsTrue(level.NoWallInSight(7, 7));
            //check empty
            Assert.IsTrue(level.ValidateCoordForGuard(5, 5)); 
            Assert.IsTrue(level.NoWallInSight(5, 5));

            //check guard
            Assert.IsFalse(level.ValidateCoordForGuard(2, 2));
            Assert.IsTrue(level.NoWallInSight(2, 2));


        }

        [TestMethod]
        public void TestPlayerInRange()
        {
            Level level = new(MockTxtMgr(2));
            Guard g = level.Board[2, 2].Guard!;
            Player p = level.Player;
            Assert.IsNotNull(g);
            Assert.IsNotNull(p);

            Assert.IsTrue(level.IsPlayerInRange(g));
            level.Board[1, 1] = new Wall(1, 1); 
            Assert.IsFalse(level.IsPlayerInRange(g));

            p.Move(0, 2);
            Assert.IsTrue(level.IsPlayerInRange(g));
            level.Board[1, 2] = new Wall(1, 2);
            Assert.IsFalse(level.IsPlayerInRange(g));

            p.Move(4, 1);
            Assert.IsTrue(level.IsPlayerInRange(g));
            level.Board[3, 2] = new Wall(3, 2);
            Assert.IsFalse(level.IsPlayerInRange(g));

            p.Move(2, 3);
            Assert.IsTrue(level.IsPlayerInRange(g));


        }

        //a tobbi metodus ezek trivialis hasznalatai
    }
}