using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LifeInOcean
{
    public class Cell
    {
        public const char DefImageCell = '-';
//        public static Ocean _o= new Ocean();
        protected readonly ICellable _o;

        public Cell ( ICellable o, Coordinate Offset, char Image = DefImageCell )
        {
            _o = o;
            _offset = Offset;
            _image = Image;
        }

        ~Cell()
        {
        }
        public Coordinate Offset
        {
            get
            {
                return _offset;
            }
            set
            {
                _offset = value;
            }
        }
        public char Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }
        public void Display() //выводит изображение по соответствующему смещению
        {
            Console.SetCursorPosition(Offset.X, Offset.Y);
            Console.Write ( Image );
        }
        public virtual void Process()
        {
            //Перемещает в соседнюю ячейку используя определенные правила и обновляет массив cells _o
        }
        public virtual void Reproduse(Coordinate anOffset)
        {
            //Воспроизводит себя в ячейке с координатами anOffset в массиве cells из _o
        }
        
        public Cell GetCopy () //Возвращает ячейку с координатами aCoord в массиве cells из _o
        {
            Cell c = (Cell) MemberwiseClone ();
            return c;
        }
        private char _image;
        private Coordinate _offset;

    }
}
