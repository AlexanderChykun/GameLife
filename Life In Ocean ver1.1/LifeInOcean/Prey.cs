using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LifeInOcean
{
    class Prey : Cell
    {
        public const int DefTimeToReproduse = 6;
        public const char DefImagePrey = '♥';

        public Prey(ICellable o, Coordinate Offset, char Image = DefImagePrey, int timeToReproduse = DefTimeToReproduse)
            : base(o, Offset, Image)
        {
            
            _timeToReproduse = timeToReproduse; 
        }
        public override void Process() //Добыча перемещается eсли возможно в пустую ячейку и уменьшает_timeToReproduse на 1
        {
            Coordinate from = Offset;
            Coordinate toEmpty = _o.GetEmptyNeighborCoord ( Offset );
            if ( from != toEmpty )
            {
                
                MoveFrom ( from, toEmpty );
                if ( (_o.GetCellAt ( toEmpty ) as Prey).TimeToReproduse == 0 )
                {
                    Reproduse(from);
                }
                else
                {
                    (_o.GetCellAt ( toEmpty ) as Prey).TimeToReproduse -= 1;
                }
            }
        }
        public void MoveFrom(Coordinate from, Coordinate to) //Перемещает из координат from в координату to в массиве cells из _o
        {
            _o.AssignCellAt ( to, _o.GetCellAt ( from ) );
            _o.GetCellAt ( to ).Display ();
            _o.AssignCellAt ( from, new Cell ( _o, from ) );
            _o.GetCellAt ( from ).Display ();         
        }
        public override void Reproduse(Coordinate anOffset)  //Воспроизводит себя в ячейке с координатами anOffset в массиве cells из _o
        {
            _o.AssignCellAt ( anOffset, new Prey ( _o, anOffset ) );
            _o.GetCellAt ( anOffset ).Display ();
        }
        public int TimeToReproduse
        {
            get
            {
                return _timeToReproduse;
            }
            set
            {
                _timeToReproduse = value;
            }
        }
        private int _timeToReproduse;
    }
}
