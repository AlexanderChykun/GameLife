using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LifeInOcean
{
    class Obstacle : Cell
    {
        public const char DefImageObstacles = '#';
        public Obstacle(ICellable o, Coordinate Offset, char Image = DefImageObstacles
            )
            : base(o,Offset, Image)
        {
        }
 
       
    }
}
