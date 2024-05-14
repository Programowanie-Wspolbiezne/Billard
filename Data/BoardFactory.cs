using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class BoardFactory
    {
        public static Data.IBoard createBoard(int width,int height)
        {
            Data.Board board = new Data.Board(width,height);
            return board;
        }
    }
}
