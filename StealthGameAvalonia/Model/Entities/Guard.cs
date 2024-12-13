using StealthGameAvalonia.Model.Utils;
namespace StealthGameAvalonia.Model.Entities
{
    public class Guard(int row, int col) : LevelEntity(row, col)
    {
        public Direction Facing { get; private set; } = DirExt.GetRandomDirection();

        public (int r, int c) SimulateMove()
        {
            int r = Row;
            int c = Col;
            r += Facing switch
            {
                Direction.UP => -1,
                Direction.DOWN => 1,
                _ => 0

            };
            c += Facing switch
            {
                Direction.LEFT => -1,
                Direction.RIGHT => 1,
                _ => 0
            };
            return (r, c);
        }

        public void Move(int r, int c)
        {
            Row = r;
            Col = c;
        }

        public void ChangeDirection()
        {
            Facing = DirExt.GetRandomDirection(Facing);
        }



        public override string ToString()
        {
            return "g";
        }

    }
}
