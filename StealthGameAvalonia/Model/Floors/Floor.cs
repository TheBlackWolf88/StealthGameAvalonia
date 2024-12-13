using StealthGameAvalonia.Model.Entities;
using System.Collections.Generic;

namespace StealthGameAvalonia.Model.Floors
{
    public class Floor
    {
        public int Row { get; protected set; }
        public int Col { get; protected set; }

        public Player? Player
        {
            get
            {
                if (Entity is Player)
                {
                    return (Player)Entity;
                }
                return null;
            }
        }
        public LevelEntity? Entity { get; protected set; }
        public Guard? Guard
        {
            get
            {
                if (Entity is Guard)
                {
                    return (Guard)Entity;
                }

                return null;
            }
        }

        public Floor(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public Floor(int row, int col, LevelEntity entities)
        {
            Row = row;
            Col = col;
            Entity = entities;
        }

        public void AddEntity(LevelEntity entity)
        {
            if (this is not Wall && Entity is null)
            {
                Entity = entity;
            }
        }

        public void RemoveEntity()
        {
            if (this is not Wall && Entity is not null)
            {
                Entity = null;
            }
        }


    }
}
