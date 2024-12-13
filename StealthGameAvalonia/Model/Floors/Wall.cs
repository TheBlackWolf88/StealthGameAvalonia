using StealthGameAvalonia.Model.Entities;
using System.Collections.Generic;

namespace StealthGameAvalonia.Model.Floors
{
    public class Wall : Floor
    {
        public Wall(int row, int col) : base(row, col)
        {
        }

        public Wall(int row, int col, LevelEntity entity) : base(row, col, entity)
        {
        }

        public override string ToString()
        {
            return "w";
        }
    }
}
