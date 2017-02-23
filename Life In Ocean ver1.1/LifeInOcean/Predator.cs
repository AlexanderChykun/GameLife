using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LifeInOcean
{
    class Predator : Prey
    {
        public const int DefTimeToFeed = 6;
        public const char DefImagePredators = '☻';
        
        public Predator ( ICellable o, Coordinate Offset, char Image = DefImagePredators, int timeToReproduse = 6, int timeToFeed = DefTimeToFeed )
            : base(o,Offset, Image, timeToReproduse)
        {
            
            _timeToFeed = timeToFeed;
        }

        public override void Process()      //проверяет timeToFeed( если равно 0 то смерть)иначе пытается съесть соседнюю добычу, в противном случае 
                                            //если возможно перемещается в пустую ячейку(север-юг-запад-восток), уменьшает timeToReproduse на 1
        {
            bool IsDie = false;
            Coordinate from = Offset;
            Coordinate toEmpty = (Coordinate)_o.GetEmptyNeighborCoord ( Offset );
            Coordinate toPrey = (Coordinate)_o.GetPreyNeighborCoord ( Offset );
            if ( (_o.GetCellAt ( from ) as Predator).TimeToFeed == 0 )
            {
                _o.AssignCellAt ( from, new Cell ( _o, from ) );
                _o.GetCellAt ( from ).Display ();
                IsDie = true;
            }
            if ( !IsDie )
            {
                if ( from != toPrey )
                {
                    MoveFrom ( from, toPrey );
                    (_o.GetCellAt ( toPrey ) as Predator).TimeToFeed = 6;
                    if ( (_o.GetCellAt ( toPrey ) as Predator).TimeToReproduse == 0 )
                    {
                        Reproduse ( from );
                    }
                    else
                    {
                        (_o.GetCellAt ( toPrey ) as Predator).TimeToReproduse -= 1;
                    }
                }
                else
                {
                    if ( from != toEmpty )
                    {
                        MoveFrom ( from, toEmpty );
                        (_o.GetCellAt ( toEmpty ) as Predator).TimeToFeed -= 1;
                        if ( (_o.GetCellAt ( toEmpty ) as Predator).TimeToReproduse == 0 )
                        {
                            Reproduse ( from );
                        }
                        else
                        {
                            (_o.GetCellAt ( toEmpty ) as Predator).TimeToReproduse -= 1;
                        }
                    }
                    else
                    {
                        (_o.GetCellAt ( from ) as Predator).TimeToFeed -= 1;
                    }
                }
            }
        }
        public override void Reproduse(Coordinate anOffset)   //воспроизводит себя в ячейке с координатами anOffset в массиве Cells из _o
        {
            _o.AssignCellAt ( anOffset, new Predator ( _o, anOffset ) );
            _o.GetCellAt ( anOffset ).Display ();
        }
        public int TimeToFeed
        {
            get
            {
                return _timeToFeed;
            }
            set
            {
                _timeToFeed = value;
            }
        }
        private int _timeToFeed;
    }
}
