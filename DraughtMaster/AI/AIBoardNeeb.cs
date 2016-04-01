using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraughtMaster
{
    public class AIBoardNeeb : AIBoard
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
        protected AI_itemBoardNeeb[] virture_itemAI;
        protected AI_itemBoardNeeb[] virture_itemPlayer;

        // list 6 tuple , 
        //id_row_source,id_col_source 
        //id_row_des,id_col_des
        //id_row_killed,id_col_kill  , if not kill value is -1;
        List<Tuple<int, int>> listAnswer = new List<Tuple<int, int>>();
        List<Tuple<int, int>> listAnswerKillEnemy = new List<Tuple<int, int>>();
        //List<Tuple<int, int, int, int, int, int>> listAnswerDoubleKill = new List<Tuple<int, int, int, int, int, int>>();
        List<Tuple<int, int, int, int>> listBestAnswer = new List<Tuple<int, int, int, int>>();
        List<Tuple<int, int>> listBestAnswerKillEnemy = new List<Tuple<int, int>>();

        List<Tuple<int, int ,int,int>>[] listAnswerFirst;
        List<Tuple<int, int>>[] listBestAnswerKillEnemyFirst;
        int[] TotalPoint;

        public AIBoardNeeb()
        {

        }

        public AIBoardNeeb(int row,int col,AIBoradSection[,] tableBoardSectionForAI,Item[] itemAI,Item[] itemPlayer)
        {
            this.row = row;
            this.col = col;
            this.tableBoardSectionForAI = new AIBoradSection[row, col];
            //this.point = new int[itemAI.Length];

            // initial Object item.
            this.itemAI = new Item[itemAI.Length];
            this.itemPlayer = new Item[itemPlayer.Length];

            this.virture_itemAI = new AI_itemBoardNeeb[itemAI.Length];
            this.virture_itemPlayer = new AI_itemBoardNeeb[itemPlayer.Length];

            this.listAnswerFirst = new List<Tuple<int, int,int,int>>[itemAI.Length];
            listBestAnswerKillEnemyFirst = new List<Tuple<int, int>>[itemAI.Length];

            this.TotalPoint = new int[itemAI.Length];
            // Clone value in object parameter to Object AI.
            for (int i = 0; i < itemAI.Length; i++)
            {
                // Clone Object item.
                if (itemAI[i] != null)
                {
                    TotalPoint[i] = 0;
                    this.itemAI[i] = itemAI[i].clone();
                    listAnswerFirst[i] = new List<Tuple<int, int ,int ,int>>();
                    listBestAnswerKillEnemyFirst[i] = new List<Tuple<int, int>>();
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
                    virture_itemAI[i] = new AI_itemBoardNeeb(this.row, this.col, this.tableBoardSectionForAI, this.itemAI[i], this.itemPlayer);
                }
            }

            for (int i = 0; i < virture_itemPlayer.Length; i++)
            {
                //set table item view.
                if (this.itemPlayer[i] != null)
                {
                    virture_itemPlayer[i] = new AI_itemBoardNeeb(this.row, this.col, this.tableBoardSectionForAI, this.itemPlayer[i], this.itemAI);
                }
            }
        }

        //public List<Tuple<int, int, int, int, int, int>> getListAnswer()
       // {
        //    return listAnswer;
        //}

        public List<Tuple<int, int, int, int>> getListBestAnswer()
        {
            return listBestAnswer;
        }

        public List<Tuple<int, int>> getListBestAnswerKillEnemy()
        {
            return listBestAnswerKillEnemy;
        }

        public List<Tuple<int, int,int, int>> getListBestAnswer2()
        {
            int max = -9999;
            int max_index = 0;
            Console.Write(" \n" );
            for (int i = 0; i < TotalPoint.Length; i++)
            {
                Console.Write(" " + TotalPoint[i]);
                if (TotalPoint[i] > max && itemAI[i] != null && listAnswerFirst[i].Count > 0)
                {
                    max = TotalPoint[i];
                    max_index = i;
                }
            }
            return listAnswerFirst[max_index];
        }

        public List<Tuple<int, int>> getListBestAnswerKillEnemy2()
        {
            int max = -9999;
            int max_index = 0;
            for (int i = 0; i < TotalPoint.Length; i++)
            {
                if (TotalPoint[i] > max && itemAI[i] != null && listAnswerFirst[i].Count > 0)
                {
                    max = TotalPoint[i];
                    max_index = i;
                }
            }
            return listBestAnswerKillEnemyFirst[max_index];
        }

        public bool isRangeIndex(int id_row, int id_col)
        {
            if (id_row >= 0 && id_row < row && id_col >= 0 && id_col < col)
            {
                return true;
            }
            return false;
        }

        private void cloneBoard(AIBoradSection[,] source,AIBoradSection[,] destination)
        {
            for (int i = 0; i<source.GetLength(1); i++)
            {
                for (int j = 0;j< source.GetLength(0); j++)
                {
                    destination[i, j] = source[i, j].cloneAIBoardHorseSection();
                }
            }
        }

        public int run(int step)
        {
            //AIBoradSection[,] tableBoardSectionForAI;
            if (step >=1)  // base case.
            {
                return 0;
            }

            // AI first.
            int pointAI = 0;
            int pointPlayer = 0;
            int max =0;
            int maxAI = -9999;

            for (int i = 0; i < virture_itemAI.Length; i++) // do each item ai.
            {
                if (virture_itemAI[i] != null && virture_itemAI[i].getItem().getPlayer() > 0)
                {
                    Console.Write("\nNODE" + i + "");
                    pointAI = 0;
                    pointAI = virture_itemAI[i].run();
                    //Console.Write("" + point + " ");

                    if (step == 0)  // answer first step only.
                    {
                        MoveToListFirst(virture_itemAI[i].getItem(), i, virture_itemAI[i].getListBestAnswer());
                        MoveToListKillFirst(i, virture_itemAI[i].getListBestAnswerKillEnemy());
                    }
                    if (pointAI > maxAI)
                    {
                        maxAI = pointAI;
                        if (step == 0)  // answer first step only.
                        {
                            listAnswer.Clear();
                            listAnswerKillEnemy.Clear();

                            listAnswer = virture_itemAI[i].getListBestAnswer();
                            if (listAnswer.Count == 0)
                            {
                                Console.Write("\n !!!!!Found Error. ");
                            }
                            listAnswerKillEnemy = virture_itemAI[i].getListBestAnswerKillEnemy();
                            //listAnswerDoubleKill = virture_itemAI[i].getListDoubleKill();
                            //Console.Write("\nNODE MAX : " + i + " , point =" + pointAI + "\n");
                            swapToBestAnswer(itemAI[i]);
                        }
                    }
                }
                else
                {
                    continue;
                }

                int maxPlayer = -9999;
                for (int j = 0; j < virture_itemPlayer.Length; j++) // do each item ai.
                {

                    if (virture_itemPlayer[j] != null && virture_itemPlayer[j].getItem().getPlayer() > 0)
                    {
                        // table after turn ai.
                        virture_itemPlayer[j].setTableBoardSection(virture_itemAI[i].getTableBoardSection());
                        pointPlayer = 0;
                        pointPlayer = virture_itemPlayer[j].run();
                        //Console.Write("" + point + " ");
                        if (pointPlayer > maxPlayer)
                        {
                            maxPlayer = pointPlayer;
                            Console.Write("\nNODE MAX : " + i + " , point =" + pointPlayer + "\n");
                        }

                        // set table to ai.
                        virture_itemAI[i].setTableBoardSection(virture_itemPlayer[j].getTableBoardSection());
                    }
                }

                TotalPoint[i] += maxAI - maxPlayer;
                //int point = 
            }
            return maxAI + run(step + 1);
        }


        // for test.
        public void MoveToListFirst(Item item, int indexListFirst, List<Tuple<int, int>> list)
        {
            int id_row_old = item.getIdRow();
            int id_col_old = item.getIdCol();

            listAnswerFirst[indexListFirst].Clear();
            foreach (Tuple<int, int> buffer in list)
            {
                int id_row_des = buffer.Item1;
                int id_col_des = buffer.Item2;
                listAnswerFirst[indexListFirst].Add(new Tuple<int, int, int, int>(id_row_old, id_col_old, id_row_des, id_col_des));
            }
        }

        public void MoveToListKillFirst(int indexListFirst, List<Tuple<int, int>> list)
        {
            listBestAnswerKillEnemyFirst[indexListFirst].Clear();
            foreach (Tuple<int, int> buffer in list)
            {
                int id_row_des = buffer.Item1;
                int id_col_des = buffer.Item2;
                listBestAnswerKillEnemyFirst[indexListFirst].Add(new Tuple<int, int>(id_row_des, id_col_des));
            }
        }


        public void swapToBestAnswer(Item item)
        {
            if (listAnswer.Count <= 0)
            {
                return;
            }

            int id_row_old = item.getIdRow();
            int id_col_old = item.getIdCol();

            listBestAnswer.Clear();
            foreach(Tuple<int, int> buffer in listAnswer)
            {
                int id_row_des = buffer.Item1;
                int id_col_des = buffer.Item2;
                listBestAnswer.Add(new Tuple<int, int, int, int>(id_row_old, id_col_old, id_row_des, id_col_des));
            }

            listBestAnswerKillEnemy.Clear();
            foreach (Tuple<int, int> buffer in listAnswerKillEnemy)
            {
                int id_row = buffer.Item1;
                int id_col = buffer.Item2;
                listBestAnswerKillEnemy.Add(new Tuple<int, int>(id_row, id_col));
            }
        }


    }
}
