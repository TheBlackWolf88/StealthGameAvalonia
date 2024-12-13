using System;

namespace StealthGameAvalonia.Model.Utils
{
    public static class DirExt
    {
        private static readonly Direction[] dirs = Enum.GetValues<Direction>();

        public static Direction FromString(string s)
        {
            return s switch
            {
                "u" => Direction.UP,
                "d" => Direction.DOWN,
                "l" => Direction.LEFT,
                "r" => Direction.RIGHT,
                _ => throw new Exception("Wrong direction char!")
            };
        }


        public static string ToString(Direction d)
        {
            return d switch
            {
                Direction.UP => "u",
                Direction.DOWN => "d",
                Direction.LEFT => "l",
                Direction.RIGHT => "r",
                _ => ""
            };
        }

        public static Direction GetRandomDirection()
        {
            Random rand = new();
            Direction ret = dirs[rand.Next(0, dirs.Length)];
            return ret;


        }

        public static Direction GetRandomDirection(Direction last)
        {
            Random rand = new();
            Direction ret = dirs[rand.Next(0, dirs.Length)];
            while (ret == last)
            {
                ret = dirs[rand.Next(0, dirs.Length)];
            }
            return ret;


        }
    }

}


