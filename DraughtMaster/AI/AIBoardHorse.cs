using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraughtMaster
{

    public class AIBoardHorse : AIBoard
    {
        // Inherit variable from AIBoard.
        // Now , I dont want move it. 
        protected int row;
        protected int col;
        protected int max_step;

        protected int start_row;
        protected int start_col;
        protected AIBoradSection[,] tableBoardSectionForAI;

        // store real vlue.
        protected Item[] itemPlayer;
        protected Item[] itemAI;

        // value for calclate state for test.
        protected AI_item[] virture_itemAI;
        protected Item bestsuperItem;
        protected int bestIndexItemSuper = 0;
        protected AI_item virture_BestsuperItem;
        //protected AI_item[] virture_itemPlayer;

        // list 6 tuple , 
        //id_row_source,id_col_source 
        //id_row_des,id_col_des
        //id_row_killed,id_col_kill  , if not kill value is -1;
        List<Tuple<int, int, int, int, int, int>> listAnswer = new List<Tuple<int,int,int,int,int,int>>();
        List<Tuple<int, int, int, int, int, int>> listAnswerDoubleKill = new List<Tuple<int, int, int, int, int, int>>();
        List<Tuple<int, int, int, int, int, int>> listBestAnswer = new List<Tuple<int, int, int, int, int, int>>();
        List<Tuple<int, int, int, int, int, int>> listBestAnswerDoubleKill = new List<Tuple<int, int, int, int, int, int>>();

        public AIBoardHorse()
        {

        }

        public AIBoardHorse(int row,int col,AIBoradSection[,] tableBoardSectionForAI,Item[] itemAI,Item[] itemPlayer)
        {
            this.row = row;
            this.col = col;
            this.tableBoardSectionForAI = new AIBoradSection[row, col];
            //this.point = new int[itemAI.Length];

            // initial Object item.
            this.itemAI = new Item[itemAI.Length];
            this.itemPlayer = new Item[itemPlayer.Length];

            this.virture_itemAI = new AI_item[itemAI.Length];

            // Clone value in object parameter to Object AI.
            for (int i = 0; i < itemAI.Length; i++)
            {
                // Clone Object item.
                if (itemAI[i] != null)
                {
                    this.itemAI[i] = itemAI[i].clone();
                    //Console.Write("\n" + itemAI[i].getIdRow() + "," + itemAI[i].getIdCol());
                }
            }

            for (int i = 0; i < itemPlayer.Length; i++)
            {
                // Clone Object item.
                if (itemPlayer[i] != null)
                {
                    this.itemPlayer[i] = itemPlayer[i].clone();
                    //Console.Write("\n" + itemPlayer[i].getIdRow() + "," + itemPlayer[i].getIdCol());
                }
            }

            //Console.Write("\n");

            // Copy value table.
            for(int i=0;i<row;i++){
                for(int j=0;j<col;j++){
                    // Copy data in object parameter.
                    this.tableBoardSectionForAI[i, j] = tableBoardSectionForAI[i, j].cloneAIBoardHorseSection();
                    //Console.Write("" + tableBoardSectionForAI[i, j].getPlayerHolder() + " ");
                }
                //Console.Write("\n");
            }


            // item .
            for (int i = 0; i < virture_itemAI.Length; i++)
            {
                //set table item view.
                if (this.itemAI[i] != null)
                {
                    virture_itemAI[i] = new AI_item(this.row, this.col, this.tableBoardSectionForAI, this.itemAI[i], this.itemPlayer);
                }
            }
        }

        //public List<Tuple<int, int, int, int, int, int>> getListAnswer()
       // {
        //    return listAnswer;
        //}

        public List<Tuple<int, int, int, int, int, int>> getListBestAnswer()
        {
            return listBestAnswer;
        }

        public List<Tuple<int, int, int, int, int, int>> getListbestAnswerSuperItemDoubleKill()
        {
            List<Tuple<int, int, int, int, int, int, int>> list = virture_itemAI[bestIndexItemSuper].getListSectionSelectedAI();
            if (list.Count > 0)
            {
                listBestAnswer.Clear();
                listBestAnswer.Add(
                    new Tuple<int, int, int, int, int, int>(
                        list.First().Item1,
                        list.First().Item2,
                        list.First().Item3,
                        list.First().Item4,
                        list.First().Item5,
                        list.First().Item6));
                return listBestAnswer;
            }
            return null;
        }

        public List<Tuple<int, int, int, int, int, int>> getListBestAnswerDoubleKill()
        {
            return listBestAnswerDoubleKill;
        }

        public bool isRangeIndex(int id_row, int id_col)
        {
            if (id_row >= 0 && id_row < row && id_col >= 0 && id_col < col)
            {
                return true;
            }
            return false;
        }

        public int run(int step)
        {
            // AI first.
            int point = 0;
            int max = -9999;
            for (int i = 0; i < virture_itemAI.Length; i++)
            {
                if (virture_itemAI[i] != null)
                {
                        point = virture_itemAI[i].run(step);
                        //Console.Write("" + point + " ");
                        if (point > max)
                        {
                            if (itemAI[i].getStatusItem() == 2) // horse
                            {
                                virture_BestsuperItem = virture_itemAI[i];
                                bestsuperItem = itemAI[i];
                                bestIndexItemSuper = i;
                            }
                            listAnswer = virture_itemAI[i].getListAnswer();
                            listAnswerDoubleKill = virture_itemAI[i].getListDoubleKill();
                            Console.Write("\nNODE MAX : "+i +"\n");
                            swapToBestAnswer();
                            max = point;
                        }
                }
            }
            return max;
        }

        public int getBestIndexItemSuper()
        {
            return bestIndexItemSuper;
        }

        public void runDoubleKillSuperItemMode(int id_row, int id_col,int index)
        {
            bestIndexItemSuper = index;
            virture_itemAI[index].runDoubleSuperMode( id_row, id_col);
        }

        public bool getFlagHaveKillFromSuperItem()
        {
            return virture_itemAI[bestIndexItemSuper].getFlagHaveKillFromSuperItem();
        }

        public void swapToBestAnswer()
        {
            listBestAnswer.Clear();
            foreach(Tuple<int, int, int, int, int, int> buffer in listAnswer){
                int id_row_source = buffer.Item1;
                int id_col_source = buffer.Item2;
                int id_row_des = buffer.Item3;
                int id_col_des = buffer.Item4;
                int id_row_killed = buffer.Item5;
                int id_col_killed = buffer.Item6;
                listBestAnswer.Add(new Tuple<int, int, int, int, int, int>(id_row_source, id_col_source, id_row_des, id_col_des, id_row_killed, id_col_killed));
            }
            //listAnswer.Clear();

            listBestAnswerDoubleKill.Clear();
            foreach (Tuple<int, int, int, int, int, int> buffer in listAnswerDoubleKill)
            {
                int id_row_source = buffer.Item1;
                int id_col_source = buffer.Item2;
                int id_row_des = buffer.Item3;
                int id_col_des = buffer.Item4;
                int id_row_killed = buffer.Item5;
                int id_col_killed = buffer.Item6;
                listBestAnswerDoubleKill.Add(new Tuple<int, int, int, int, int, int>(id_row_source, id_col_source, id_row_des, id_col_des, id_row_killed, id_col_killed));
            }
            //listAnswerDoubleKill.Clear();
        }

        //public void setMaxStep(int s)
        //{
        //    max_step = s;
        //}

        ////public int getPoint()
        ////{
        ////    return point;
        ////}

        //public int getRow()
        //{
        //    return row;
        //}

        //public int getCol()
        //{
        //    return col;
        //}

        //public int max(int a, int b)
        //{
        //    if(a>b) return a ;
        //    return b;
        //}

        //private void displayPoint()
        //{
        //    for (int i = 0; i < point.Length; i++)
        //    {
        //        Console.Write(""+point[i]+" ");
        //    }
        //    Console.Write("\r\n");
        //}

    
        



    }
}
