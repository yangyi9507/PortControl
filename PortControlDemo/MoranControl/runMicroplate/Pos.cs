using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortControlDemo.MoranControl
{
    public class Pos
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public Pos(Pos ps)
        {
            X = ps.X;
            Y = ps.Y;
            Z = ps.Z;
        }
        public Pos()
        {
            X = Y = Z = -1;
        }
        public static bool operator ==(Pos p1, Pos p2)
        {
            if (object.Equals(p1, null) && object.Equals(p2, null)) return true;
            if (object.Equals(p1, null) || object.Equals(p2, null)) return false;
            return p1.X == p2.X && p2.Y == p1.Y;
        }
        public static bool operator !=(Pos p1, Pos p2)
        {
            if (object.Equals(p1, null) && object.Equals(p2, null)) return false;
            if (object.Equals(p1, null) || object.Equals(p2, null)) return true;
            return p1.X != p2.X || p2.Y != p1.Y;
        }
    }
    
}
