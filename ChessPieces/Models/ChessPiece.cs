using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPieces.Models
{
    internal abstract class ChessPiece
    {
        public Cell Location { get; set; }
        public List<Cell> ReachableCells { get; protected set; }
        protected void CalculateReachableCells()
        {
            ReachableCells = new List<Cell>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Cell currentCell = new Cell(i, j);
                    if (IsReachable(currentCell))
                    {
                        ReachableCells.Add(currentCell);
                    }
                }
            }
        }
        protected abstract bool IsReachable(Cell cell);
    }
}
