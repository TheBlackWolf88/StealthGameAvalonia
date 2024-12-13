namespace StealthGameAvalonia.Model.Entities
{
    public abstract class LevelEntity(int row, int col)
    {
        public int Row { get; protected set; } = row;
        public int Col { get; protected set; } = col;
    }
}
