using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraughtMaster
{

    public class AIBoard
    {


        // Constructor
        public AIBoard()
        {

        }

        public AIBoard(int num_row,int num_col)
        {
           // row = (byte)num_row;
            //col = (byte)num_col;
        }

        public AIBoard(byte num_row, byte num_col)
        {
           // row = num_row;
           // col = num_col;
        }

        //public void setTable(byte[,] tableBoardPlayerItem,byte[,] tableBoardStatusItem,byte[,] tableBoardPlayer)
        //{
        //    int i = 0, j = 0;

        //    for (i = 0; i < row; i++)
        //    {
        //        for (j = 0; j < col; j++)
        //        {
        //            this.tableBoardPlayerItem[i, j] = tableBoardPlayerItem[i, j];
        //        }
        //    }

        //    for (i = 0; i < row; i++)
        //    {
        //        for (j = 0; j < col; j++)
        //        {
        //            this.tableBoardStatusItem[i, j] = tableBoardStatusItem[i, j];
        //        }
        //    }

        //    for (i = 0; i < row; i++)
        //    {
        //        for (j = 0; j < col; j++)
        //        {
        //            this.tableBoardPlayer[i, j] = tableBoardPlayer[i, j];
        //        }
        //    }
        //}

    }
}
