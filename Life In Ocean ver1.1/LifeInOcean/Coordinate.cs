using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LifeInOcean
{
    public class Coordinate
    {
        public Coordinate(int X, int Y)
        {
            _x = X;
            _y = Y;
        }
        public int X 
        { 
            get 
            {
                return _x;
            }
            set 
            {
                _x = value;
            }
        }
        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }
        private int _x;
        private int _y;
    }
}
