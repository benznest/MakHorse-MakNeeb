using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraughtMaster
{
    public class AI_item
    {
        public int row;
        public int col;

        //protected int id_row;
        //protected int id_col;
        protected int step;  

        protected AIBoradSection[,] tableBoardSection;
        protected Item myItem;
        protected Item[] itemEnemy;
        protected int index;

        private string flag_answer = "";

        //List<Tuple<int, int, int, int, int, int>> listAnswerDouble = new List<Tuple<int, int, int, int, int, int>>();
        List<Tuple<int, int, int, int, int, int>> listAnswerDouble;
        List<Tuple<int, int, int, int, int, int>> listAnswerLeft ;
        List<Tuple<int, int, int, int, int, int>> listAnswerRight;
        List<Tuple<int, int, int, int, int, int>> listAnswerSuper;
        bool flagDoubleSuperItem = false;

        List<Tuple<int, int ,int, int, int, int, int>> listSectionSelectedAI;
        //row,col old position | new ro,col position | location killed | point.

        public AI_item(int row,int col,AIBoradSection[,] tableBoardSection,Item myItem,Item[] itemEnemy)
        {
            this.row = row;
            this.col = col;

            // clone table.
            this.tableBoardSection = new AIBoradSection[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    this.tableBoardSection[i, j] = tableBoardSection[i, j].cloneAIBoardHorseSection();

                }
            }

            // clone item ai.
            //this.myItem = new Item();
            this.myItem = myItem.clone();
            Console.Write("" + this.myItem.getIdRow() + "," + this.myItem.getIdCol() + " ");

            this.itemEnemy = new Item[itemEnemy.Length];
            // clone item enemy (player).
            for (int i = 0; i < itemEnemy.Length; i++)
            {
                if (itemEnemy[i] != null)
                {
                    this.itemEnemy[i] = itemEnemy[i].clone();
                }
            }

            listAnswerDouble = new List<Tuple<int, int, int, int, int, int>>();
            listAnswerLeft = new List<Tuple<int, int, int, int, int, int>>();
            listAnswerRight = new List<Tuple<int, int, int, int, int, int>>();
            listAnswerSuper = new List<Tuple<int, int, int, int, int, int>>();
            listSectionSelectedAI = new List<Tuple<int,int,int, int, int, int ,int>>();
        }

        public List<Tuple<int, int, int, int, int, int>> getListAnswer()
        {
            if (flag_answer == "left")
            {
                return listAnswerLeft;
            }
            else if (flag_answer == "right")
            {
                return listAnswerRight;
            }
            else
            {
                flagDoubleSuperItem = true;
                return listAnswerSuper;
            }
        }

        public List<Tuple<int, int ,int, int, int, int, int>> getListSectionSelectedAI()
        {
            return listSectionSelectedAI;
        }

        public bool getFlagHaveKillFromSuperItem()
        {
            return flagDoubleSuperItem;
        }

        public List<Tuple<int, int, int, int, int, int>> getListDoubleKill()
        {
            return listAnswerDouble;
        }


        private void addAnswerDouble(int id_row_source, int id_col_source, int id_row_des, int id_col_des, int id_row_killed, int id_col_killed)
        {
            listAnswerDouble.Add(new Tuple<int, int, int, int, int, int>(id_row_source, id_col_source, id_row_des, id_col_des, id_row_killed, id_col_killed));
        }

        private void addAnswerLeft(int id_row_source,int id_col_source,int id_row_des,int id_col_des,int id_row_killed,int id_col_killed)
        {
            listAnswerLeft.Add(new Tuple<int, int, int, int, int, int>(id_row_source,id_col_source, id_row_des,id_col_des, id_row_killed,id_col_killed));
        }

        private void addAnswerRight(int id_row_source, int id_col_source, int id_row_des, int id_col_des, int id_row_killed, int id_col_killed)
        {
            listAnswerRight.Add(new Tuple<int, int, int, int, int, int>(id_row_source, id_col_source, id_row_des, id_col_des, id_row_killed, id_col_killed));
        }

        public int max(int a, int b)
        {
            if (a > b) return a;
            return b;
        }

        public int run(int step)
        {
            //Console.Write(""+myItem.id_row+"-"+ myItem.id_col +" ");
            flagDoubleSuperItem = false;
            listAnswerDouble.Clear();
            listAnswerLeft.Clear();
            listAnswerRight.Clear();
            listAnswerSuper.Clear();
            listSectionSelectedAI.Clear();
            if (isRangeIndex(myItem.id_row, myItem.id_col))
            {
                //Console.Write(""+myItem.id_row+"-"+ myItem.id_col +" ");
                return turnAI(step);
            }
            return 0;
        }

        public int runDoubleSuperMode(int id_row,int id_col)
        {
            flagDoubleSuperItem = false;
            listAnswerDouble.Clear();
            listAnswerLeft.Clear();
            listAnswerRight.Clear();
            listAnswerSuper.Clear();
            listSectionSelectedAI.Clear();
            searchPathSuperItemAI(id_row,id_col, 1,true);
            return 0;
        }

        public int turnAI(int step)
        {
            int pointLeft = 0, pointRight = 0;
            int subpointLeft = 0, subpointRight = 0;
            int pointSuper = 0;

            if (step >= 3)  // base case.
            {
                return 0;
            }


            if (myItem != null) // do only item alive.
            {

                if (myItem.getStatusItem() == 1)  // item is regular.
                {
                    if (myItem.getIdRow() == 2 && myItem.getIdCol() == 2)
                    {
                        Console.Write("\n Left : \n");
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < col; j++)
                            {
                                Console.Write("" + tableBoardSection[i, j].getPlayerHolder() + " ");
                            }
                            Console.Write("\n");
                        }
                    }


                    subpointLeft = 0;
                    subpointLeft = checkLeftRegularAI(myItem.getIdRow(), myItem.getIdCol());
                    if (subpointLeft > 0) // have walk or kill
                    {
                        //Console.Write("" + subpoint+" ");
                        // pointTotal += subpoint;
                        pointLeft += subpointLeft;
                        pointLeft += turnPlayer(step);
                    }

                    if (myItem.getIdRow() == 2 && myItem.getIdCol() == 2)
                    {
                        Console.Write("\n Right : \n");
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < col; j++)
                            {
                                Console.Write("" + tableBoardSection[i, j].getPlayerHolder() + " ");
                            }
                            Console.Write("\n");
                        }
                    }

                    subpointRight = 0;
                    subpointRight = checkRightRegularAI(myItem.getIdRow(), myItem.getIdCol());
                    if (subpointRight > 0)
                    {
                        //Console.Write("" + subpoint + " ");
                        //  pointTotal += subpoint;
                        pointRight += subpointRight;
                        pointRight += turnPlayer(step);
                    }
                }
                else if (myItem.getStatusItem() == 2)  // Super Item.
                {
                    searchPathSuperItemAI(myItem.getIdRow(),myItem.getIdCol(),1,false);
                    //Console.Write("listSelectedAI = " + listSectionSelectedAI.Count);
                    pointSuper = runSuperItem();
                }
            }

            if (pointLeft >= pointRight && pointLeft >= pointSuper)
            {
                flag_answer = "left";
            }
            else if (pointRight > pointLeft && pointRight > pointSuper)
            {
                flag_answer = "right";
            }
            else  // super 
            {
                flag_answer = "super";
            }

            return max(pointLeft, pointRight);
        }


        public int turnPlayer(int step)
        {
            int pointLeft = 0, pointRight = 0;
            int subpointLeft = 0, subpointRight = 0;
            for (int i = 0; i < itemEnemy.Length; i++)
            {
                if (itemEnemy[i] != null)
                {

                    if (itemEnemy[i].getStatusItem() == 1)  // get item player, available.
                    {
                        
                        // left way.
                        subpointLeft = 0;
                        subpointLeft = checkLeftRegularPlayer(itemEnemy[i].getIdRow(), itemEnemy[i].getIdCol());
                        //Console.Write("\nLetf: " + itemEnemy[i].getIdRow()+","+ itemEnemy[i].getIdCol());
                        pointLeft = subpointLeft;
                            //  pointTotal += subpoint;
                            //pointLeft += turnAI(step + 1);
                        
                        

                        // right way
                        subpointRight = 0;
                        subpointRight = checkRightRegularPlayer(itemEnemy[i].getIdRow(), itemEnemy[i].getIdCol());
                        //Console.Write("\nRight: " + itemEnemy[i].getIdRow() + "," + itemEnemy[i].getIdCol());
                        pointRight = subpointRight;
                            // pointTotal += subpoint;
                            //pointRight += turnAI(step + 1);
                        
                        
                    }
                    else if (itemEnemy[i].getStatusItem() == 2)  // Super Item.
                    {

                    }
                }
            }
            return max(pointLeft, pointRight);
        }

        public bool isRangeIndex(int id_row, int id_col)
        {
            if (id_row >= 0 && id_row < row && id_col >= 0 && id_col < col)
            {
                return true;
            }
            return false;
        }

        public void deleteItem(AIBoradSection[,] table,int id_dest_row, int id_dest_col)
        {
            table[id_dest_row, id_dest_col].setPlayer(0);
            table[id_dest_row, id_dest_col].setStatus(0);
        }

        public void moveItem(AIBoradSection[,] table,int id_source_row, int id_source_col, int id_dest_row, int id_dest_col)
        {
            int temp_row = id_source_row;
            int temp_col = id_source_col;
            int temp_player = table[id_source_row, id_source_col].getPlayerHolder();

            // swap id and player , row , col in table.
            table[id_source_row, id_source_col].setRowCol(id_dest_row, id_dest_col);
            int player = table[id_dest_row, id_dest_col].getPlayerHolder();
            table[id_source_row, id_source_col].setPlayer(player);

            table[id_dest_row, id_dest_col].setRowCol(temp_row, temp_col);
            table[id_dest_row, id_dest_col].setPlayer(temp_player);
        }

        public int checkLeftRegularAI(int id_row, int id_col)
        {
            // move value to temp.
            AIBoradSection[,] tempTableBoardSection = new AIBoradSection[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    tempTableBoardSection[i, j] = new AIBoradSection(i, j, tableBoardSection[i, j].getPlayerHolder(), tableBoardSection[i, j].getStatus());
                }
            }

            if (isRangeIndex(id_row + 1, id_col - 1))
            { // check index
                if (tempTableBoardSection[id_row + 1, id_col - 1].isEmpty()) // can walk 1 position. not kill.
                {
                    moveItem(tempTableBoardSection,id_row, id_col, id_row + 1, id_col - 1);
                    addAnswerLeft(id_row, id_col, id_row + 1, id_col - 1, -1, -1);

                    return 1;
                }
                else  // have item 
                {
                    if (tableBoardSection[id_row + 1, id_col - 1].getPlayerHolder() == 2) // found item enemy.
                    {
                        if (isRangeIndex(id_row + 1 + 1, id_col - 1 - 1))
                        {
                            if (tempTableBoardSection[id_row + 1 + 1, id_col - 1 - 1].isEmpty()) // check can kill
                            {
                                deleteItem(tempTableBoardSection,id_row + 1, id_col - 1);
                                moveItem(tempTableBoardSection,id_row, id_col, id_row + 1 + 1, id_col - 1 - 1); // move to kill.
                                addAnswerLeft(id_row, id_col, id_row + 1 + 1, id_col - 1 - 1, id_row + 1, id_col - 1);
                                return 10 + checkDoubleRegularAI(id_row + 1 + 1, id_col - 1 - 1);
                            }
                        }
                    }
                }
            }
            return 0;
        }

        public int checkRightRegularAI(int id_row, int id_col)
        {
            // move value to temp.
            AIBoradSection[,] tempTableBoardSection = new AIBoradSection[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    tempTableBoardSection[i, j] = new AIBoradSection(i, j, tableBoardSection[i, j].getPlayerHolder(), tableBoardSection[i, j].getStatus());
                }
            }

            if (isRangeIndex(id_row + 1, id_col + 1))
            { // check index
                if (tempTableBoardSection[id_row + 1, id_col + 1].isEmpty()) // can walk 1 position. not kill.
                {
                    moveItem(tempTableBoardSection,id_row, id_col, id_row + 1, id_col + 1);
                    addAnswerRight(id_row, id_col, id_row + 1, id_col + 1, -1, -1);
                    return 1;
                }
                else  // have item 
                {

                     //for debug
                    //if(id_row==2 && id_col==2){
                    //    for (int i = 0; i < row; i++)
                    //    {
                    //        for (int j = 0; j < col; j++)
                    //        {
                    //            Console.Write("" + tableBoardSection[i, j].getPlayerHolder()+" ");
                    //        }
                    //        Console.Write("\n");
                    //    }
                    //}

                    if (tempTableBoardSection[id_row + 1, id_col + 1].getPlayerHolder() == 2) // found item enemy.
                    {
                        if (isRangeIndex(id_row + 1 + 1, id_col + 1 + 1))
                        {
                            if (tempTableBoardSection[id_row + 1 + 1, id_col + 1 + 1].isEmpty()) // check can kill
                            {
                                deleteItem(tempTableBoardSection,id_row + 1, id_col + 1);
                                moveItem(tempTableBoardSection,id_row, id_col, id_row + 1 + 1, id_col + 1 + 1); // move to kill.
                                addAnswerRight(id_row, id_col, id_row + 1 + 1, id_col + 1 + 1, id_row + 1, id_col + 1);
                                return 10 + checkDoubleRegularAI(id_row + 1 + 1, id_col + 1 + 1);
                            }
                        }
                    }
                }
            }
            return 0;
        }

        public int checkDoubleRegularAI(int id_row, int id_col)
        {
            if (checkLeftDoubleRegularAI(id_row, id_col))
            {
                return 10 + checkDoubleRegularAI(id_row + 1 + 1, id_col - 1 - 1);
            }

            if (checkRightDoubleRegularAI(id_row, id_col))
            {
                return 10 + checkDoubleRegularAI(id_row + 1 + 1, id_col + 1 + 1);
            }

            return 0;
        }



        public bool checkLeftDoubleRegularAI(int id_row, int id_col)
        {
            // move value to temp.
            AIBoradSection[,] tempTableBoardSection = new AIBoradSection[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    tempTableBoardSection[i, j] = new AIBoradSection(i, j, tableBoardSection[i, j].getPlayerHolder(), tableBoardSection[i, j].getStatus());
                }
            }

            if (isRangeIndex(id_row + 1, id_col - 1))
            {
                if (tempTableBoardSection[id_row + 1, id_col - 1].getPlayerHolder() == 2) // found item enemy.
                {
                    if (isRangeIndex(id_row + 1 + 1, id_col - 1 - 1))
                    {
                        if (tempTableBoardSection[id_row + 1 + 1, id_col - 1 - 1].isEmpty()) // check can kill
                        {
                            deleteItem(tempTableBoardSection,id_row + 1, id_col - 1);
                            moveItem(tempTableBoardSection,id_row, id_col, id_row + 1 + 1, id_col - 1 - 1); // move to kill.
                            addAnswerDouble(id_row, id_col, id_row + 1 + 1, id_col - 1 - 1, id_row + 1, id_col - 1);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool checkRightDoubleRegularAI(int id_row, int id_col)
        {
            // move value to temp.
            AIBoradSection[,] tempTableBoardSection = new AIBoradSection[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    tempTableBoardSection[i, j] = new AIBoradSection(i, j, tableBoardSection[i, j].getPlayerHolder(), tableBoardSection[i, j].getStatus());
                }
            }

            if (isRangeIndex(id_row + 1, id_col + 1))
            {
                if (tempTableBoardSection[id_row + 1, id_col + 1].getPlayerHolder() == 2) // found item enemy.
                {
                    if (isRangeIndex(id_row + 1 + 1, id_col + 1 + 1))
                    {
                        if (tempTableBoardSection[id_row + 1 + 1, id_col + 1 + 1].isEmpty()) // check can kill
                        {
                            deleteItem(tempTableBoardSection,id_row + 1, id_col + 1);
                            moveItem(tempTableBoardSection,id_row, id_col, id_row + 1 + 1, id_col + 1 + 1); // move to kill.
                            addAnswerDouble(id_row, id_col, id_row + 1 + 1, id_col + 1 + 1, id_row + 1, id_col + 1);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public int checkLeftRegularPlayer(int id_row, int id_col)
        {
            // move value to temp.
            AIBoradSection[,] tempTableBoardSection = new AIBoradSection[row,col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    tempTableBoardSection[i,j] = new AIBoradSection(i, j, tableBoardSection[i, j].getPlayerHolder(), tableBoardSection[i, j].getStatus());
                }
            }

                if (isRangeIndex(id_row - 1, id_col - 1))
                { // check index
                    if (tempTableBoardSection[id_row-1, id_col-1].isEmpty()) // can walk 1 position. not kill.
                    {
                        moveItem(tempTableBoardSection,id_row, id_col, id_row - 1, id_col - 1);
                        return -1;
                    }
                    else  // have item 
                    {
                        if (tempTableBoardSection[id_row - 1, id_col - 1].getPlayerHolder() == 1) // found item enemy.
                        {
                            if (isRangeIndex(id_row - 1 - 1, id_col - 1 - 1))
                            {
                                if (tempTableBoardSection[id_row - 1 - 1, id_col - 1 - 1].isEmpty()) // check can kill
                                {
                                    deleteItem(tempTableBoardSection,id_row - 1, id_col - 1);
                                    moveItem(tempTableBoardSection,id_row, id_col, id_row - 1 - 1, id_col - 1 - 1); // move to kill.
                                    return -10;
                                }
                            }
                        }
                    }
                }
            return 0;
        }



        public int checkRightRegularPlayer(int id_row, int id_col)
        {
            // move value to temp.
            AIBoradSection[,] tempTableBoardSection = new AIBoradSection[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    tempTableBoardSection[i, j] = new AIBoradSection(i, j, tableBoardSection[i, j].getPlayerHolder(), tableBoardSection[i, j].getStatus());
                }
            }


            if (isRangeIndex(id_row - 1, id_col + 1))
            { // check index
                if (tempTableBoardSection[id_row - 1, id_col + 1].isEmpty()) // can walk 1 position. not kill.
                {
                    moveItem(tempTableBoardSection,id_row, id_col, id_row - 1, id_col + 1);
                    return -1;
                }
                else  // have item 
                {
                    if (tempTableBoardSection[id_row - 1, id_col + 1].getPlayerHolder() == 1) // found item enemy.
                    {
                        if (isRangeIndex(id_row - 1 - 1, id_col + 1 + 1))
                        {
                            if (tempTableBoardSection[id_row - 1 - 1, id_col + 1 + 1].isEmpty()) // check can kill
                            {
                                deleteItem(tempTableBoardSection,id_row - 1, id_col + 1);
                                moveItem(tempTableBoardSection,id_row, id_col, id_row - 1 - 1, id_col + 1 + 1); // move to kill.
                                return -10;
                            }
                        }
                    }
                }
            }
            return 0;
        }

        private void searchPathSuperItemAI(int id_row,int id_col,int myPlayer,bool flagDoubleKill)
        {
            Console.Write("\r\nx\r\n");
            searchTopLeftPathSuperItemAI(id_row, id_col, myPlayer, flagDoubleKill);
            searchTopRightPathSuperItemAI(id_row, id_col, myPlayer, flagDoubleKill);
            searchDownLeftPathSuperItemAI(id_row, id_col, myPlayer, flagDoubleKill);
            searchDownRightPathSuperItemAI(id_row, id_col, myPlayer, flagDoubleKill);

            //listSectionItemKilled.Clear();
        }

        public void addSectionToListSectionSelectedAI(int id_row, int id_col, int id_new_row, int id_new_col, int id_row_killed, int id_col_killed, int point)
        {
            listSectionSelectedAI.Add(new Tuple<int, int, int, int, int, int, int>(id_row, id_col, id_new_row, id_new_col, id_row_killed, id_col_killed, point));
        }

        // search walk top left.    
        private void searchTopLeftPathSuperItemAI(int i, int j, int myPlayer, bool flagDoubleKill)
        {
            // move value to temp.
            AIBoradSection[,] tempTableBoardSection = new AIBoradSection[row, col];
            for (int u = 0; u < row; u++)
            {
                for (int p = 0; p < col; p++)
                {
                    tempTableBoardSection[u, p] = new AIBoradSection(u, p, tableBoardSection[u, p].getPlayerHolder(), tableBoardSection[u, p].getStatus());
                }
            }

            int id_row_old = i;
            int id_col_old = j;
            i--;
            j--;

            while (i >= 0 && j >= 0)
            {
                if (tempTableBoardSection[i, j].isEmpty()) // section not item.
                {
                    if (flagDoubleKill == false)
                    {
                        addSectionToListSectionSelectedAI(id_row_old, id_col_old, i, j, -1, -1, 0);
                    }
                    i--;
                    j--;
                }
                else // found item in section.
                {
                    if (tempTableBoardSection[i, j].getPlayerHolder() != myPlayer) // found Enemy
                    {
                        if (i - 1 >= 0 && j - 1 >= 0) // check index next area have real.
                        {
                            if (tempTableBoardSection[i - 1, j - 1].isEmpty())
                            { // next area is empty.
                                addSectionToListSectionSelectedAI(id_row_old,id_col_old,i - 1, j - 1, i, j,20);
                                flagDoubleSuperItem = true;
                                //searchPathSuperItemDoubleKill(i - 1, j - 1, myPlayer, false); // recursive. not can walk real , just discovery way.
                            }
                        }
                    }
                    break;  // found Section not empty. or has Item.
                }
            }
        }

        private void searchTopRightPathSuperItemAI(int i, int j, int myPlayer, bool flagDoubleKill)
        {
            // move value to temp.
            AIBoradSection[,] tempTableBoardSection = new AIBoradSection[row, col];
            for (int u = 0; u < row; u++)
            {
                for (int p = 0; p < col; p++)
                {
                    tempTableBoardSection[u, p] = new AIBoradSection(u, p, tableBoardSection[u, p].getPlayerHolder(), tableBoardSection[u, p].getStatus());
                }
            }

            int id_row_old = i;
            int id_col_old = j;
            i--;
            j++;

            // search walk top right.
            while (i >= 0 && j < col)
            {
                if (tempTableBoardSection[i, j].isEmpty())
                {
                    if (flagDoubleKill == false)
                    {
                        addSectionToListSectionSelectedAI(id_row_old, id_col_old, i, j, -1, -1, 0);
                    }
                    i--;
                    j++;
                }
                else
                {
                    if (tempTableBoardSection[i, j].getPlayerHolder() != myPlayer) // found Enemy
                    {
                        if (i - 1 >= 0 && j + 1 < col) // check index next area have real.
                        {
                            if (tempTableBoardSection[i - 1, j + 1].isEmpty())
                            { // next area is empty.

                                addSectionToListSectionSelectedAI(id_row_old,id_col_old,i - 1, j + 1, i, j,20);
                                flagDoubleSuperItem = true;
                                //searchPathSuperItemDoubleKill(i - 1, j + 1, myPlayer, false); // recursive. not can walk real , just discovery way.
                            }
                        }
                    }
                    break;  // found Section not empty. or has Item.
                }
            }
        }

        private void searchDownLeftPathSuperItemAI(int i, int j, int myPlayer, bool flagDoubleKill)
        {
            // move value to temp.
            AIBoradSection[,] tempTableBoardSection = new AIBoradSection[row, col];
            for (int u = 0; u < row; u++)
            {
                for (int p = 0; p < col; p++)
                {
                    tempTableBoardSection[u, p] = new AIBoradSection(u, p, tableBoardSection[u, p].getPlayerHolder(), tableBoardSection[u, p].getStatus());
                }
            }

            int id_row_old = i;
            int id_col_old = j;
            i++;
            j--;

            // search walk down left.
            while (i < row && j >= 0)
            {

                if (tempTableBoardSection[i, j].isEmpty())
                {
                    if (flagDoubleKill == false)
                    {
                        addSectionToListSectionSelectedAI(id_row_old, id_col_old, i, j, -1, -1, 0);
                    }
                    i++;
                    j--;
                }
                else
                {
                    if (tempTableBoardSection[i, j].getPlayerHolder() != myPlayer) // found Enemy
                    {
                        if (i + 1 < row && j - 1 >= 0) // check index next area have real.
                        {
                            if (tempTableBoardSection[i + 1, j - 1].isEmpty())
                            { // next area is empty.
                                addSectionToListSectionSelectedAI(id_row_old,id_col_old ,i + 1, j - 1, i, j,20);
                                flagDoubleSuperItem = true;
                                //searchPathSuperItemDoubleKill(i + 1, j - 1, myPlayer, false); // recursive. not can walk real , just discovery way.
                            }
                        }
                    }
                    break;  // found Section not empty. or has Item.
                }
            }
        }

        private void searchDownRightPathSuperItemAI(int i, int j, int myPlayer, bool flagDoubleKill)
        {
            // move value to temp.
            AIBoradSection[,] tempTableBoardSection = new AIBoradSection[row, col];
            for (int u = 0; u < row; u++)
            {
                for (int p = 0; p < col; p++)
                {
                    tempTableBoardSection[u, p] = new AIBoradSection(u, p, tableBoardSection[u, p].getPlayerHolder(), tableBoardSection[u, p].getStatus());
                }
            }

            int id_row_old = i;
            int id_col_old = j;
            i++;
            j++;

            // search walk down right.
            while (i < row && j < col)
            {
                if (tempTableBoardSection[i, j].isEmpty())
                {
                    if (flagDoubleKill == false)
                    {
                        addSectionToListSectionSelectedAI(id_row_old, id_col_old, i, j, -1, -1, 0);
                    }
                    i++;
                    j++;
                }
                else
                {
                    if (tempTableBoardSection[i, j].getPlayerHolder() != myPlayer) // found Enemy
                    {
                        if (i + 1 < row && j + 1 < col) // check index next area have real.
                        {
                            if (tempTableBoardSection[i + 1, j + 1].isEmpty())
                            { // next area is empty.
                                
                                addSectionToListSectionSelectedAI(id_row_old,id_col_old,i + 1, j + 1, i, j,20);
                                flagDoubleSuperItem = true;
                                //searchPathSuperItemDoubleKill(i + 1, j + 1, myPlayer, false); // recursive. not can walk real , just discovery way.
                            }
                        }
                    }
                    break;  // found Section not empty. or has Item.
                }
            }
        }

        private int runSuperItem()
        {
            int i = 0;
            //find max point in here.
            int max= -99999;
            //int maxIndex=0;
            foreach (Tuple<int, int, int, int, int, int, int> tuple in listSectionSelectedAI)
            {
                int point = tuple.Item7;
                if (point >= max)
                {
                    max = point;
                    listAnswerSuper.Clear();
                    listAnswerSuper.Add(new Tuple<int, int, int, int, int, int>
                        (tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6));
                }
                i++;
            }
            
            //listAnswerSuper.Add(new Tuple<int,int,int,int,int,int>())
            return max;
        }
    }
}
