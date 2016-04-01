using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraughtMaster
{
    public class AI_itemBoardNeeb
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

        private bool flag_left = true;

        //List<Tuple<int, int, int, int, int, int>> listAnswerDouble = new List<Tuple<int, int, int, int, int, int>>();
        //List<Tuple<int, int, int, int, int, int>> listAnswerDouble;
        //List<Tuple<int, int, int, int, int, int>> listAnswerLeft ;
        //List<Tuple<int, int, int, int, int, int>> listAnswerRight;
        List<Tuple<int, int>> listCanWalk;
        List<Tuple<int, int>> listAnswer;
        List<Tuple<int, int>> listBestAnswer;
        List<Tuple<int, int>> listBestAnswerKillEnemny;

        List<Tuple<int, int>> listTempKillEnemy;
        List<Tuple<int, int>> listKillEnemy;


        public AI_itemBoardNeeb(int row, int col, AIBoradSection[,] tableBoardSection, Item myItem, Item[] itemEnemy)
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

            //listAnswerDouble = new List<Tuple<int, int, int, int, int, int>>();
            listCanWalk = new List<Tuple<int, int>>();
            listAnswer = new List<Tuple<int, int>>();

            listTempKillEnemy = new List<Tuple<int, int>>();
            listKillEnemy = new List<Tuple<int, int>>();

            listBestAnswer = new List<Tuple<int, int>>();
            listBestAnswerKillEnemny = new List<Tuple<int, int>>();
        }

        public AIBoradSection[,] getTableBoardSection()
        {
            return tableBoardSection;
        }

        public void setTableBoardSection(AIBoradSection[,] table)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    this.tableBoardSection[i, j] = table[i, j].cloneAIBoardHorseSection();

                }
            }
        }

        public Item getItem()
        {
            return myItem;
        }

        public List<Tuple<int, int>> getListBestAnswer()
        {
            return listBestAnswer;
        }

        public List<Tuple<int, int>> getListBestAnswerKillEnemy()
        {
            return listBestAnswerKillEnemny;
        }

        //public List<Tuple<int, int, int, int, int, int>> getListDoubleKill()
        //{
        //    return listAnswerDouble;
        //}


        //private void addAnswerDouble(int id_row_source, int id_col_source, int id_row_des, int id_col_des, int id_row_killed, int id_col_killed)
        //{
        //    listAnswerDouble.Add(new Tuple<int, int, int, int, int, int>(id_row_source, id_col_source, id_row_des, id_col_des, id_row_killed, id_col_killed));
        //}

        //private void addAnswerLeft(int id_row_source,int id_col_source,int id_row_des,int id_col_des,int id_row_killed,int id_col_killed)
        //{
        //    listAnswerLeft.Add(new Tuple<int, int, int, int, int, int>(id_row_source,id_col_source, id_row_des,id_col_des, id_row_killed,id_col_killed));
        //}

        //private void addAnswerRight(int id_row_source, int id_col_source, int id_row_des, int id_col_des, int id_row_killed, int id_col_killed)
        //{
        //    listAnswerRight.Add(new Tuple<int, int, int, int, int, int>(id_row_source, id_col_source, id_row_des, id_col_des, id_row_killed, id_col_killed));
        //}

        private void addAnswer(int id_row,int id_col)
        {
            listAnswer.Add(new Tuple<int, int>(id_row, id_col));
        }

        private void addToListCanWalk(int id_row, int id_col)
        {
            listCanWalk.Add(new Tuple<int, int>(id_row, id_col));
        }

        private void addToListTempKillEnemy(int id_row, int id_col)
        {
            listTempKillEnemy.Add(new Tuple<int, int>(id_row, id_col));
        }

        private void addToListKillEnemy(int id_row, int id_col)
        {
            listKillEnemy.Add(new Tuple<int, int>(id_row, id_col));
        }

        private void addToListBestAnswer(int id_row, int id_col)
        {
            listBestAnswer.Add(new Tuple<int, int>(id_row, id_col));
        }

        private void clearListTempKillEnemy()
        {
            listTempKillEnemy.Clear();
        }

        private void clearListKillEnemy()
        {
            listKillEnemy.Clear();
        }

        private void moveAllItem(List<Tuple<int, int>> source, List<Tuple<int, int>> destination)
        {
            foreach (Tuple<int, int> s in source)
            {
                destination.Add(s);
            }
            source.Clear();
        }


        public int max(int a, int b)
        {
            if (a > b) return a;
            return b;
        }

        public bool isRangeIndex(int id_row, int id_col)
        {
            if (id_row >= 0 && id_row < row && id_col >= 0 && id_col < col)
            {
                return true;
            }
            return false;
        }

        public int run()
        {
            //Console.Write(""+myItem.id_row+"-"+ myItem.id_col +" ");
            
            listAnswer.Clear();
            listTempKillEnemy.Clear();
            if (isRangeIndex(myItem.id_row, myItem.id_col))
            {
                //Console.Write(""+myItem.id_row+"-"+ myItem.id_col +" ");
                return turnAI(tableBoardSection);
            }
            return 0;
        }

        public int turnAI(AIBoradSection[,] table)
        {
            int subpoint = 0;

            if (myItem != null) // do only item alive.
            {

                if (myItem.getStatusItem() == 1)  // item is regular.
                {
                    subpoint = 0;
                    workingSearchPath(table,myItem.getIdRow(), myItem.getIdCol());
                    //Tuple<int, int> x = listCanWalk.Last();
                    //listBestAnswer.Add(x); // prevent can not walk. by put data least one.
                    subpoint = checkKill(myItem.getPlayer());  // ai is player1 , parameter.
                    //turnPlayer(table,step);
                }

            }
            return subpoint;
        }

        //private int turnPlayer(AIBoradSection[,] table,int step)
        //{
        //    AIBoradSection[,] tempTableBoardSection = new AIBoradSection[row, col];
        //    for (int i = 0; i < row; i++)
        //    {
        //        for (int j = 0; j < col; j++)
        //        {
        //            tempTableBoardSection[i, j] = new AIBoradSection(i, j, table[i, j].getPlayerHolder(), table[i, j].getStatus());
        //        }
        //    }

        //    int subpoint = 0;
        //    foreach (Item itemPlayer in itemEnemy)
        //    {
        //        if (itemPlayer != null) // do only item alive.
        //        {
        //            if (itemPlayer.getStatusItem() > 0)  // item is regular.
        //            {
        //                subpoint = 0;
        //                workingSearchPath(tempTableBoardSection,myItem.getIdRow(), myItem.getIdCol());
        //                subpoint = checkKill(2);  // 2 is player2.

        //                turnAI(tempTableBoardSection, step + 1); // change turn.
        //            }
        //        }
        //    }
        //    return subpoint;
        //}



        private bool hasItem(AIBoradSection[,] table,int myRow, int myCol)
        {
            return table[myRow, myCol].getPlayerHolder() != 0;
        }

        //get enemy player.
        private int getEnemy(int player)
        {
            if (player == 1) return 2;
            else if (player == 2) return 1;
            return 0;
        }

        // get player holder section.
        private int getPlayerHolder(AIBoradSection[,] table, int id_row, int id_col)
        {
            return table[id_row, id_col].getPlayerHolder();
        }

        // display path can walk.
        public void workingSearchPath(AIBoradSection[,] table,int myRow, int myCol)
        {
            listCanWalk.Clear();
            int i = myRow;
            int j = myCol;

            // up
            i = myRow - 1;
            j = myCol;
            while (isRangeIndex(i, j) && !hasItem(tableBoardSection,i, j))
            {
                addToListCanWalk(i, j);
                //addSectionToListSectionSelected(i, j, -1, -1);
                //boardSection[i, j].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                i--;
            }

            // Down
            i = myRow + 1;
            j = myCol;
            while (isRangeIndex(i, j) && !hasItem(tableBoardSection,i, j))
            {
                addToListCanWalk(i, j);
                //boardSection[i, j].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                i++;
            }

            // left
            i = myRow;
            j = myCol - 1;
            while (isRangeIndex(i, j) && !hasItem(tableBoardSection,i, j))
            {
                addToListCanWalk(i, j);
                //addSectionToListSectionSelected(i, j, -1, -1);
                //boardSection[i, j].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                j--;
            }

            // right
            i = myRow;
            j = myCol + 1;
            while (isRangeIndex(i, j) && !hasItem(tableBoardSection,i, j))
            {
                addToListCanWalk(i, j);
                //addSectionToListSectionSelected(i, j, -1, -1);
                //boardSection[i, j].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                j++;
            }
        }

        void randomSwapOrderItemInList(List<Tuple<int,int>> myList)
        {
            Console.Write("\nList Canwalk , random = " + myList.Count);
            for (int i = 0; i < myList.Count; i++)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, myList.Count - 1);

                Tuple<int, int> item = myList[randomNumber];
                myList.RemoveAt(randomNumber);
                myList.Insert(0, item);
            }
        }

        // check and search item enemy for delete.
        private int checkKill(int player)
        {
            int max = -9999;
            //int enemy = getEnemy(player);
            //int i = id_row;
            // int j = id_col;

            randomSwapOrderItemInList(listCanWalk);  // for random new order. fair.
            int point = 0;
            foreach (Tuple<int, int> buffer in listCanWalk)
            {
                point = 0;
                int id_row = buffer.Item1;
                int id_col = buffer.Item2;

                // check central kill. CK ,central kill is we can kill more any item.
                point += checkTopCentralKill(id_row, id_col, player);
                point += checkDownCentralKill(id_row, id_col, player);

                point += checkRightCentralKill(id_row, id_col, player);
                point += checkLeftCentralKill (id_row, id_col, player);
                

                // cannot delete item now , because will effect in double side kill.
                // check double side kill. DSK, is we can kill most only 2 item. 
                point += checkTopDownDoubleSideKill(id_row, id_col, player, myItem.getIdRow(), myItem.getIdCol());
                point += checkLeftRightDoubleSideKill(id_row, id_col, player, myItem.getIdRow(), myItem.getIdCol());

                //Console.Write("\n point = " + point);

                if (point > max)
                {
                    // clear data old of best answer.
                    max = point;
                    listBestAnswer.Clear();
                    listBestAnswerKillEnemny.Clear();

                    addAnswer(id_row,id_col);
                    
                    moveAllItem(listAnswer, listBestAnswer);
                    
                    moveAllItem(listKillEnemy, listBestAnswerKillEnemny);
                }

                //deleteAllInListSectionCentralKillFuture();
                //deleteAllInListSectionDoubleSideKillFuture();

                //clear all list.
                listAnswer.Clear();
                clearListTempKillEnemy();
                clearListKillEnemy();
            }
            
            return max;
        }

        // check killing , Central kill.
        private int workingCentralKill(int i, int j, int player, int enemy)
        {
            if (getPlayerHolder(tableBoardSection,i, j) == enemy)
            {
                addToListTempKillEnemy(i, j);
                return 10;  // continue loop.
            }

            if (getPlayerHolder(tableBoardSection,i, j) == 0)  // found empty section.
            {
                // can not any kill. cancel search.
                clearListTempKillEnemy();
                return -1;
            }

            if (getPlayerHolder(tableBoardSection,i, j) == player) // kill complete.
            {
                if (listTempKillEnemy.Count > 0)
                {
                    moveAllItem(listTempKillEnemy, listKillEnemy); // move item temp to list.
                    // moveAllItem is move and clear source.
                    return 20;
                }
                else
                {
                    // not have temp.
                    return -1;
                }
            }
            return 0;
        }

        private int checkTopCentralKill(int i, int j, int player)
        {
            i--;
            int total = 0;
            int point = 0;
            int enemy = getEnemy(player);
            while (isRangeIndex(i, j) && point >= 0 && point < 20)
            {
                point = workingCentralKill(i, j, player, enemy);
                if (point <= 0)
                {
                    total = 0;
                    break;
                }
                else
                {
                    total += point;
                }
                i--;

                if (!isRangeIndex(i, j) && point < 20)
                {
                    clearListTempKillEnemy();
                    total = 0;
                    break;
                }
            }
            return total;
        }

        private int checkDownCentralKill(int i, int j, int player)
        {
            i++;
            int total = 0;
            int point = 0;
            int enemy = getEnemy(player);
            while (isRangeIndex(i, j) && point >= 0 && point < 20)
            {
                point = workingCentralKill(i, j, player, enemy);
                if (point <= 0)
                {
                    total = 0;
                    break;
                }
                else
                {
                    total += point;
                }
                i++;

                if (!isRangeIndex(i, j) && point < 20)
                {
                    clearListTempKillEnemy();
                    total = 0;
                    break;
                }
            }
            return total;
        }

        private int checkLeftCentralKill(int i, int j, int player)
        {
            j--;
            int total = 0;
            int point = 0;
            int enemy = getEnemy(player);
            while (isRangeIndex(i, j) && point >= 0 && point < 20)
            {
                point = workingCentralKill(i, j, player, enemy);
                if (point <= 0)
                {
                    total = 0;
                    break;
                }
                else
                {
                    total += point;
                }
                j--;

                if (!isRangeIndex(i, j) && point < 20)
                {
                    clearListTempKillEnemy();
                    total = 0;
                    break;
                }
            }
            return total;
        }

        private int checkRightCentralKill(int i, int j, int player)
        {
            j++;
            int total = 0;
            int point = 0;
            int enemy = getEnemy(player);
            while (isRangeIndex(i, j) && point >= 0 && point < 20)
            {
                point = workingCentralKill(i, j, player, enemy);
                if (point <= 0)
                {
                    total = 0;
                    break;
                }
                else
                {
                    total += point;
                }

                j++;

                if (!isRangeIndex(i, j) && point < 20)
                {
                    clearListTempKillEnemy();
                    total = 0;
                    break;
                }
            }
            return total;
        }

        // Interface check top - down.

        // id_row col item because when simulate move but real item not move really.
        private int checkTopDownDoubleSideKill(int i, int j, int player,int id_row_item,int id_col_item)
        {
            bool flag1 = checkTopDoubleSideKill(i, j, player ,id_row_item,id_col_item);
            bool flag2 = checkDownDoubleSideKill(i, j, player, id_row_item, id_col_item);
            if (flag1 && flag2)
            {
                moveAllItem(listTempKillEnemy, listKillEnemy);
                clearListTempKillEnemy();
                return 20;
            }
            else
            {
                clearListTempKillEnemy();
            }
            return 0;
        }

        // Interface check left - right.
        private int checkLeftRightDoubleSideKill(int i, int j, int player, int id_row_item, int id_col_item)
        {
            bool flag1 = checkLeftDoubleSideKill(i, j, player, id_row_item, id_col_item);
            bool flag2 = checkRightDoubleSideKill(i, j, player, id_row_item, id_col_item);
            if (flag1 && flag2)
            {
                moveAllItem(listTempKillEnemy, listKillEnemy);
                clearListTempKillEnemy();
                return 20;
            }
            else
            {
                clearListTempKillEnemy();
            }
            return 0;
        }

        // check killing , Double Side kill.
        private bool checkTopDoubleSideKill(int i, int j, int player, int id_row_item, int id_col_item)
        {
            bool flag = true;
            int enemy = getEnemy(player);
            i--;
            while (isRangeIndex(i, j) && flag)
            {
                //if (getPlayerHolder(i, j) == player)
                //{
                //    do nothing.
                //}
                if (getPlayerHolder(tableBoardSection,i, j) == 0)
                {
                    return false;
                }

                // old location item , after move it empty. but now not empty. , we must think it wmpty.
                if (i == id_row_item && j == id_col_item)
                {
                    return false;
                }

                if (getPlayerHolder(tableBoardSection, i, j) == enemy)
                {
                    addToListTempKillEnemy(i, j);
                    return true;
                }
                i--;
            }
            return false;
        }

        private bool checkDownDoubleSideKill(int i, int j, int player, int id_row_item, int id_col_item)
        {
            bool flag = true;
            int enemy = getEnemy(player);
            i++;
            while (isRangeIndex(i, j) && flag)
            {
                //if (getPlayerHolder(i, j) == player)
                //{
                //    do nothing.
                //}
                if (getPlayerHolder(tableBoardSection, i, j) == 0)
                {
                    return false;
                }

                // old location item , after move it empty. but now not empty. , we must think it wmpty.
                if (i == id_row_item && j == id_col_item)
                {
                    return false;
                }

                if (getPlayerHolder(tableBoardSection, i, j) == enemy)
                {
                    addToListTempKillEnemy(i, j);
                    return true;
                }
                i++;
            }
            return false;
        }

        private bool checkLeftDoubleSideKill(int i, int j, int player, int id_row_item, int id_col_item)
        {
            bool flag = true;
            int enemy = getEnemy(player);
            j--;
            while (isRangeIndex(i, j) && flag)
            {
                //if (getPlayerHolder(i, j) == player)
                //{
                //    do nothing.
                //}
                if (getPlayerHolder(tableBoardSection, i, j) == 0)
                {
                    return false;
                }

                // old location item , after move it empty. but now not empty. , we must think it wmpty.
                if (i == id_row_item && j == id_col_item)
                {
                    return false;
                }

                if (getPlayerHolder(tableBoardSection, i, j) == enemy)
                {
                    addToListTempKillEnemy(i, j);
                    return true;
                }
                j--;
            }
            return false;
        }

        private bool checkRightDoubleSideKill(int i, int j, int player, int id_row_item, int id_col_item)
        {
            bool flag = true;
            int enemy = getEnemy(player);
            j++;
            while (isRangeIndex(i, j) && flag)
            {
                //if (getPlayerHolder(i, j) == player)
                //{
                //    do nothing.
                //}
                if (getPlayerHolder(tableBoardSection, i, j) == 0)
                {
                    return false;
                }

                // old location item , after move it empty. but now not empty. , we must think it wmpty.
                if (i == id_row_item && j == id_col_item)
                {
                    return false;
                }

                if (getPlayerHolder(tableBoardSection, i, j) == enemy)
                {
                    addToListTempKillEnemy(i, j);
                    return true;
                }
                j++;
            }
            return false;
        }

    }
}
