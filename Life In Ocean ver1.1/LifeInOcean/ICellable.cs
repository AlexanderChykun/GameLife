using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LifeInOcean
{
    public interface ICellable
    {
        Coordinate GetEmptyCellCoord ( ref Coordinate offSet );
        Cell GetCellAt ( Coordinate aCoord );
        void AssignCellAt ( Coordinate aCoord, Cell aCell );
        Coordinate GetEmptyNeighborCoord ( Coordinate Offset );
        Coordinate GetPreyNeighborCoord ( Coordinate Offset );
    }
}
