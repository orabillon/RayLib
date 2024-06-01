using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serpent
{
    public class SPoint
    {
        public Point point { get; set; }
        public Direction direction { get; set; }

        public SPoint(Point point, Direction dir)
        {
            this.point = point;
            direction = dir;
        }

        public static bool operator ==(Point obj1, SPoint obj2)
        {
            return obj1.X == obj2.point.X && obj1.Y == obj2.point.Y;
        }
        public static bool operator !=(Point obj1, SPoint obj2)
        {
            return obj1.X != obj2.point.X || obj1.Y != obj2.point.Y;
        }
    }
}
