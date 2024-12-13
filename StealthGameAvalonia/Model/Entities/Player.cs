using StealthGameAvalonia.Model.Utils;

namespace StealthGameAvalonia.Model.Entities
{
    public class Player(int row, int col) : LevelEntity(row, col)
    {

        public void Move(int r, int c)
        {
            if ((r, c) != (Row, Col))
            {
                Row = r;
                Col = c;
            }
        }

        public (int r, int c) SimulateMove(Direction dir)
        {
            int r = Row;
            int c = Col;
            r += dir switch
            {
                Direction.UP => -1,
                Direction.DOWN => 1,
                _ => 0

            };
            c += dir switch
            {
                Direction.LEFT => -1,
                Direction.RIGHT => 1,
                _ => 0
            };
            return (r, c);
        }

        public override string ToString()
        {
            return "p";
        }
    }
}
