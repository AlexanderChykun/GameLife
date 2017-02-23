using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LifeInOcean
{
    public class Ocean : ICellable
    {
        public const int MaxCols = 70;
        public const int MaxRows = 25;
        public const int DefNumPrey = 50;
        public const int DefNumPredators = 20;
        public const int DefNumObstacles = 150;
        Random rand = new Random ();
       // ICellable o;
        public Ocean(int numRows = MaxRows, int numCols = MaxCols, int iter = 25)
        {
           
            _numRows = numRows;
            _numCols = numCols;
            _cells = new Cell [_numCols, _numRows];
            
            for (int i = 0; i < _cells.GetLength(0); i++)
		    {
                for (int j = 0; j < _cells.GetLength(1); j++)
			    {
                    Coordinate c = new Coordinate(i,j);
                    _cells[i, j] = new Cell(this ,c);
			    }
		    }
        }
        Random random = new Random();
        public void initialize (int numPrey = DefNumPrey, int numPredators = DefNumPredators, int numObstacles = DefNumObstacles)
        {
            _numPrey = numPrey;
            _numPredators = numPredators;
            _numObstacles = numObstacles;

            InitCells(_numObstacles, _numPrey, _numPredators);
            DisplayBorder();
        }

        public void InitCells(int numObstacles, int numPrey, int numPredators) //Вставляет преграды и добычу и хищников в океан
        {
            AddObstacles(_numObstacles);
            AddPredators(_numPredators);
            AddPrey(_numPrey);
            DisplayStats();
            DisplayCell();
        }
        public void AddObstacles(int numObstacles) //устанавливает numObstacles преград в океане
        {
            for (int i = 0; i < numObstacles; i++)
            {
                Coordinate offSet = new Coordinate(random.Next(_numCols), random.Next(_numRows));
                GetEmptyCellCoord(ref offSet);
                _cells[offSet.X,offSet.Y] = new Obstacle(this, offSet);
            }
        }

        public void AddPredators(int numPredators) //устанавливает numPredators хищников в океане
        {
            for (int i = 0; i < numPredators; i++)
            {
                Coordinate offSet = new Coordinate(random.Next(_numCols), random.Next(_numRows));
                GetEmptyCellCoord(ref offSet);
                _cells[offSet.X, offSet.Y] = new Predator(this, offSet);
            }
        }
        public void AddPrey(int numPrey) //устанавливает numPrey добычи в океане
        {
            Coordinate offSet = new Coordinate(random.Next(_numCols), random.Next(_numRows));
            for (int i = 0; i < numPrey; i++)
            {
                GetEmptyCellCoord(ref offSet);
                _cells[offSet.X, offSet.Y] = new Prey(this, offSet);
                
            }
            
        }
        public void DisplayBorder() //отображает максимальную ограниченную область океана
        {
            Console.SetWindowSize(MaxCols + 3, MaxRows + 10);
            Console.SetCursorPosition(0, MaxRows);
            for (int i = 0; i < MaxCols + 1; i++)
            {
                Console.Write("═"); 
            }
            for (int i = 0; i < MaxRows + 1; i++)
            {
                Console.SetCursorPosition(MaxCols, i);
                Console.Write("║");
            }
        }
        public void DisplayCell() //Пересчитывает и выводит массив Cells
        {
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    _cells[i, j].Display();
                }
            }
        }
        public void DisplayStats()  //Обновляет отображаемый номер итерации, количество преград, хищников и добычи
        {
            int iPredators = 0;
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    if (_cells[i, j].Image == Predator.DefImagePredators)
                    {
                        iPredators++;
                    }
                }
            }
            int iPrey = 0;
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    if (_cells[i, j].Image == Prey.DefImagePrey)
                    {
                        iPrey++;
                    }
                }
            }
            Console.SetCursorPosition(0, MaxRows + 3);
            Console.WriteLine("Номер итерации: {0,4}\nКоличество: преград - {1,4}, хищников - {2,4}, добычи - {3,4} ",
                IterCount, NumObstacles, iPredators, iPrey);
        }
        public void Run(int numOfIteration) //Запрашивает у пользователя количество итераций и начинает моделирование
        {
            IterCount = numOfIteration;
            Cell[,] Cells = new Cell[_numCols, _numRows];
            for (int i = 1; i <= numOfIteration; i++)
            {
                GetArrCopy (ref Cells, _cells);
                IterCount--;
                foreach (Cell c in Cells)
                {
                    c.Process();
                }
                DisplayStats ();
                DisplayBorder();
                Thread.Sleep ( 300 );
            }
        }
        public void GetArrCopy ( ref Cell[,] Cell, Cell[,] _cells )
        {
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    Cell[i, j] = _cells[i, j].GetCopy();
                }
            }
        }
        public Coordinate GetEmptyCellCoord(ref Coordinate offSet) //Возвращает координаты пустой ячейки
        {
            for (int i = 0; ; i++)
            {
                offSet = new Coordinate(random.Next(_numCols), random.Next(_numRows));
                if ((_cells[offSet.X, offSet.Y] as Cell).Image == Cell.DefImageCell)
                {
                    return offSet;
                }
                else
                {
                    i--;
                }
            }
        }
        public Coordinate GetEmptyNeighborCoord ( Coordinate Offset ) //Ищет соседнюю пустую ячейку(север-юг-восток-запад)
        {
            return GetNeighborWithImage ( Cell.DefImageCell ,Offset).Offset;

        }
        public Coordinate GetPreyNeighborCoord ( Coordinate Offset )  //Ищет соседнюю ячейку с добычей(север-юг-восток-запад)
        {
            return GetNeighborWithImage ( Prey.DefImagePrey,Offset ).Offset;

        }
        public void GetRandomNum ( ref int[] RandArr )
        {
            for ( int i = 0; i < RandArr.Length; i++ )
            {
                RandArr[i] = rand.Next ( 1, 5 );
                for ( int j = 0; j < RandArr.Length; j++ )
                {
                    if ( i == j )
                    {
                        continue;
                    }
                    else
                    {
                        if ( RandArr[i] == RandArr[j] )
                        {
                            i--;
                            break;
                        }
                    }
                }
            }
        }
        public Cell GetNeighborWithImage ( char anImage, Coordinate Offset )
        {
            int[] RandArr = new int[4];
            GetRandomNum ( ref RandArr );
            for ( int i = 0; i < RandArr.Length; i++ )
            {
                if ( RandArr[i] == 1 )
                {
                    if ( (East ( Offset )as Cell).Image == anImage )
                    {
                        return East ( Offset );
                    }
                }
                if ( RandArr[i] == 2 )
                {
                    if (( West ( Offset )as Cell).Image == anImage )
                    {
                        return West ( Offset );
                    }
                }
                if ( RandArr[i] == 3 )
                {
                    if ( (South ( Offset )as Cell).Image == anImage )
                    {
                        return South ( Offset );
                    }
                }
                if ( RandArr[i] == 4 )
                {
                    if (( North ( Offset )as Cell).Image == anImage )
                    {
                        return North ( Offset );
                    }
                }
            }
            return GetCellAt ( Offset );
        }
        public Cell GetCellAt ( Coordinate aCoord ) //Возвращает ячейку с координатами aCoord в массиве cells из _o
        {
            return _cells[aCoord.X, aCoord.Y];
        }
       
        public void AssignCellAt ( Coordinate aCoord, Cell aCell ) //Помещает ячейку aCell в место с координатами aCoord в массиве cells из _o
        {
            _cells[aCoord.X, aCoord.Y] = aCell.GetCopy ();
            _cells[aCoord.X, aCoord.Y].Offset = aCoord;
        }
        public Cell East ( Coordinate Offset )
        {

            if ( (_cells[Offset.X, Offset.Y] as Cell).Offset.X != Ocean.MaxCols - 1 )
            {
                Coordinate coord = new Coordinate ( Offset.X + 1, Offset.Y );

                return GetCellAt ( coord ); ;
            }
            else
            {
                return _cells[Offset.X, Offset.Y];
            }
        }
        public Cell North ( Coordinate Offset ) //Возвращает ячейку которая находится на севере от данной
        {
            if ( (_cells[Offset.X, Offset.Y] as Cell).Offset.Y != 0 )
            {
                Coordinate coord = new Coordinate ( Offset.X, Offset.Y - 1 );

                return GetCellAt ( coord ); ;
            }
            else
            {
                return _cells[Offset.X, Offset.Y];
            }
        }
        public Cell South ( Coordinate Offset )  //Возвращает ячейку которая находится на юге от данной
        {
            if ( (_cells[Offset.X, Offset.Y] as Cell).Offset.Y != Ocean.MaxRows - 1 )
            {
                Coordinate coord = new Coordinate ( Offset.X, Offset.Y + 1 );

                return GetCellAt ( coord ); ;
            }
            else
            {
                return _cells[Offset.X, Offset.Y];
            }
        }
        public Cell West ( Coordinate Offset ) //Возвращает ячейку которая находится на западе от данной
        {
            if (( _cells[Offset.X, Offset.Y]as Cell).Offset.X != 0 )
            {
                Coordinate coord = new Coordinate ( Offset.X - 1, Offset.Y );

                return GetCellAt ( coord ); ;
            }
            else
            {
                return _cells[Offset.X, Offset.Y];
            }
        }
        public int NumPredators
        {
            get
            {
                return _numPredators;
            }
            set
            {
                _numPredators = value;
            }
        }
        public int NumPrey
        {
            get
            {
               
                return _numPrey;
            }
            set
            {
                _numPrey = value;
            }
        }
        public int NumObstacles
        {
            get
            {
                return _numObstacles;
            }
            set
            {
                _numObstacles = value;
            }
        }
        public int IterCount
        {
            get
            {
                return _iterCount;
            }
            set
            {
                _iterCount = value;
            }
        }
        public Cell[,] _cells;
        
        private int _iterCount;
        private int _numRows;
        private int _numCols;
        private int _numPrey;
        private int _numPredators;
        private int _numObstacles;

        
    }
}
