using StealthGameAvalonia.Model.Entities;
using System.Collections.Generic;

namespace StealthGameAvalonia.Model.Floors
{
    public class Exit : Floor
    {
        public Exit(int row, int col) : base(row, col)
        {
        }

        public Exit(int row, int col, LevelEntity entity) : base(row, col, entity)
        {
        }

        public override string ToString()
        {
            return "e";
        }
    }
}
