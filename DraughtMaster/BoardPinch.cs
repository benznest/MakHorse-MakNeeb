using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DraughtMaster
{
    public class BoardPinch : Board
    {
        // Flag for working.  inherit.
        //private static bool flag_AI = false;
        //private static bool flag_Item_Killed = false; // true when have least 1 item killed and reset when killing complete.
        //private static bool flag_Way_Killing = false;   // true when have way will killing happened.
        //private static bool flag_Way_DoubleKill = false;  // true when have way will Double killing happened.
        //private static bool flag_DoubleKill = false; // true when start kill item for doublekill way.
        //private static bool flag_changeTurn = false;
        ////private static bool flag_Start_DoubleKill = false; // true when starrt have double kill and killed 1 item.
        //private static bool flag_Map_Stable = false;  // true when no want refresh map to color default.

        // store last id_row , id_col player walk. for use in AI analysis.
        private static int id_row_last_walk = -1;
        private static int id_col_last_walk = -1;

        private List<Tuple<int, int>> listSectionDoubleSideKillFuture = new List<Tuple<int, int>>();
        private List<Tuple<int, int>> listSectionCentralKillFuture = new List<Tuple<int, int>>();
        private List<Tuple<int, int>> listTemp = new List<Tuple<int, int>>();



        public BoardPinch()
        {

        }

        public BoardPinch(Form myForm,Panel panelBoard, int row, int col, bool AI,MyBoardGame myBoardGame)
        {
            this.myBoardGame = myBoardGame;
            this.myForm = myForm;
            typeBoard = (int)type.PinchDraught;
            flag_AI = AI;
            temp_row = -1;
            temp_col = -1;
            listSectionSelected = new List<Tuple<int, int,int,int>>();
            //listSectionSelectedFuture = new List<Tuple<int, int, int, int>>();
            listSectionItemKilled = new List<Tuple<int, int>>();

            listSectionItemKiller = new List<Tuple<int, int>>();  // use in forced kill mode.
            listSectionItemKillerFuture = new List<Tuple<int, int>>();  // use in forced kill mode.


            listItemActiveAI = new List<int>();

            boardSection = new BoardSection[row, col]; // assign size board.
            this.setRowColBoard(row, col);
            createBoardSection(); // create Boardsection.
            accessContent(panelBoard); // access object and assign Panel to Array PanelSection.
            createPictureBoxInBoardSection(row, col);
        }

        // Add item to PanelSection and assign player.
        // Override.
        public void setItemToPanelSection(int id_row, int id_col, int player, Item item)
        {
            boardSection[id_row, id_col].setItemToPanelSection(player, item);
            item.setIdRowAndCol(id_row,id_col);
        }


        public void setLabelTurnPlayerContent(Label lbl_turnPlayer)
        {
            this.lblTurnPlayer = lbl_turnPlayer;
        }

        public bool checkMatchInList(List<Tuple<int,int>> list,Tuple<int,int> tuple)
        {
            foreach (Tuple<int, int> m in list)
            {
                if (tuple.Item1 == m.Item1 && tuple.Item2 == m.Item2)
                {
                    return true;
                }
            }
            return false;
        }

        // First event when click section.
        public void runEvent(int id_row, int id_col)
        {
            // if AI Mode , AI is player 1 
            //MessageBox.Show("x");

            //MessageBox.Show("Forced =" + flag_force_kill);
            if (flag_force_kill && listSectionSelected.Count > 0)
            {
                
                if (listSectionItemKillerFuture.Count > 0)
                {
                    state_forced = true;
                    if (checkMatchInList(listSectionItemKillerFuture, new Tuple<int, int>(id_row, id_col)))
                    {
                        turnEvent(id_row, id_col);
                        checkGameover();

                        if (turnPlayer1 == true && flag_AI == true)
                        {
                            callAIForBoardPinch();
                            swapTurnPlayer();
                        }

                        listSectionItemKiller.Clear();
                        listSectionItemKillerFuture.Clear();
                        state_forced = false;
                    }
                    else
                    {
                        
                        MessageBox.Show("You must kill");
                        clearListSectionSelected();  // clear value in listSectionSelected.
                        refreshColorOnBoard();
                        foreach (Tuple<int, int> tuple in listSectionItemKiller)
                        {
                            boardSection[tuple.Item1, tuple.Item2].getObjectPanelSection().BackColor = Color.Yellow;
                        }
                    }
                }
                else if (listSectionItemKillerFuture.Count == 0)
                {
                    turnEvent(id_row, id_col);
                    checkGameover();

                    if (turnPlayer1 == true && flag_AI == true)
                    {
                        callAIForBoardPinch();
                        swapTurnPlayer();
                    }
                }
            }
            else if (flag_force_kill && listSectionSelected.Count == 0)
            {
                if (state_forced == false)
                {
                    if (turnPlayer1 == true && flag_AI == false)
                    {
                        if (listSectionItemKiller.Count == 0)
                        {
                            //MessageBox.Show("P1");
                            searchForcedKill(1, itemPlayer1);
                        }
                    }
                    else if (turnPlayer2 == true && flag_AI == false)
                    {
                        if (listSectionItemKiller.Count == 0)
                        {
                            //MessageBox.Show("P2");
                            searchForcedKill(2, itemPlayer2);
                        }
                    }
                }
                turnEvent(id_row, id_col);
            }
            else // normally.
            {
                turnEvent(id_row, id_col);
                checkGameover();

                if (turnPlayer1 == true && flag_AI == true)
                {
                    callAIForBoardPinch();
                    swapTurnPlayer();
                }
            }
            updateCounterActiveItem();
            updateDataDeBugging();
        }

        // Manage turn player.
        public void turnEvent(int id_row, int id_col)
        {
            Console.WriteLine("turn event.");
            flag_changeTurn = false;
            int playerHolderSection = boardSection[id_row, id_col].getPlayerHolder();

            // if double kill and not complete.
            if (turnPlayer1 == true && turnPlayer2 == false) // turn is player 1.
            {
                if (playerHolderSection == 1 || playerHolderSection == 0)
                {
                    // click in section can walk.
                     flag_changeTurn = eventPlayer(id_row, id_col, 1);
                       
                }
            }
            else if (turnPlayer1 == false && turnPlayer2 == true) // // turn is player 2.
            {
                if (playerHolderSection == 2 || playerHolderSection == 0)
                {               
                     flag_changeTurn = eventPlayer(id_row, id_col, 2);
                }
            }
            else
            {
                MessageBox.Show("Error, turn mistake");
                Console.WriteLine("error.");
            }

            // swap turn. 
            if (flag_changeTurn)
            {
                swapTurnPlayer();
                setLabelTurnPlayer();
            }
        }

        // event in turn player1.
        public bool eventPlayer(int id_row, int id_col, int myPlayer)
        {
            int statusItem;
            // true when have killing or walk.
            bool flag_changeTurn = false;
            Console.WriteLine("-----------------------------");
            Console.WriteLine("[Before] data for player :" + myPlayer);
            //showDataConsole();
            // have item in section.
            if (boardSection[id_row, id_col].hasItemInSection())
            {
                statusItem = ((Item)boardSection[id_row, id_col].getItemFromPanelSection()).getStatusItem();
                refreshColorOnBoard();

                if (listSectionSelected.Count > 0)
                {
                    clearListSectionSelected(); 
                }

                if (statusItem == 1) // Item regular.
                {
                    if (boardSection[id_row, id_col].getPlayerHolder() == 1) // player 1 (Started Down of Board)
                    {
                        searchPath(id_row, id_col); // search path in section
                        //setFlagToDefault();
                    }
                    else if (boardSection[id_row, id_col].getPlayerHolder() == 2) // player 2
                    {
                        searchPath(id_row, id_col); // search path in section
                        //setFlagToDefault();
                    }
                }
                setTempRowCol(id_row, id_col); // save started point.
            }
            else if (boardSection[id_row, id_col].hasItemInSection() == false) // click panel empty.
            {
                    if (hasInListSectionSelected(id_row, id_col)) // have id in listSectionSelected is can walk.
                    {
                        // store id , temp for analysis in AI.
                        id_row_last_walk = id_row;
                        id_col_last_walk = id_col;

                        flag_changeTurn = true;
                        moveItem(temp_row, temp_col, id_row, id_col);
                        checkKill(id_row, id_col,myPlayer);
                    }
                // not have double killing unfinished.
                //if (flag_Map_Stable == false && listSectionSelectedFuture.Count == 0 && flag_Item_Killed == false)
                //{
                clearListSectionSelected();  // clear value in listSectionSelected.
                refreshColorOnBoard();
                //setFlagToDefault();
                
                //}
            }
            else
            {
                //setFlagToDefault();
                clearListSectionSelected();  // clear value in listSectionSelected.
                refreshColorOnBoard();
            }
            Console.WriteLine("[After] data for player :" + myPlayer);
            showDataConsole();
            return flag_changeTurn;
        }

        // Move item from old section to new section.
        public void moveItem(int old_id_row, int old_id_col, int new_id_row, int new_id_col)
        {
            playSoundItemWalk();
            //MessageBox.Show("" + old_id_row + "," + old_id_col + " To " + new_id_row + "," + new_id_col);
            // get Status item.
            int statusItem = ((Item)(boardSection[old_id_row, old_id_col].getItemFromPanelSection())).getStatusItem();

            // Get item i old section and remove.
            object myItem = boardSection[old_id_row, old_id_col].getItemFromPanelSection(); // get object item old section.
            boardSection[old_id_row, old_id_col].removeItemFromPanelSection();  // remove item in old section.

            // Set item and player holder to new section.
            int player = boardSection[old_id_row, old_id_col].getPlayerHolder(); // get player old section.
            ((Item)myItem).setIdRowAndCol(new_id_row, new_id_col); // set new address in Item.
            boardSection[new_id_row, new_id_col].setItemToPanelSection(player, myItem); // put item to section.
            boardSection[old_id_row, old_id_col].setPlayerHolder(0); // set empty player old section.

            // set flag for Section has item or empty.
            boardSection[old_id_row, old_id_col].setFlagItemInSection(false); // old section to empty.
            boardSection[new_id_row, new_id_col].setFlagItemInSection(true);  // new section has item.

            //remove old picture section and add Picture new section.
            boardSection[old_id_row, old_id_col].removeImage();  // clear image.

            if (statusItem == 1)
            {
                boardSection[new_id_row, new_id_col].setImageRegularItemToPictureBoxSection(player); // put image.
            }
            else if (statusItem == 2)
            {
                boardSection[new_id_row, new_id_col].setImageSuperItemToPictureBoxSection(player); // put image Super item.
            }
        }

        public void deleteItem(int id_row, int id_col)
        {
            playSoundItemKill();
            int player = boardSection[id_row, id_col].getPlayerHolder();
            if (player == 1)
            {
                count_item_player1_active--;
            }
            else if (player == 2)
            {
                count_item_player2_active--;
            }

            if ((Item)(boardSection[id_row, id_col].getItemFromPanelSection()) != null)
            {
                ((Item)(boardSection[id_row, id_col].getItemFromPanelSection())).clearIdRowAndCol(); // clear id address in Item. 
                boardSection[id_row, id_col].removeItemFromPanelSection();
                boardSection[id_row, id_col].removeImage(); // clear image in section.
                boardSection[id_row, id_col].setPlayerHolder(0);  // set empty player in section.
            }
        }

        //private void setFlagToDefault()
        //{
        //    flag_Item_Killed = false;
        //    flag_Way_Killing = false;
        //    flag_Way_DoubleKill = false;
        //    flag_Map_Stable = false;
        //}

        private void showDataConsole()
        {
            Console.WriteLine("listSectionSelected.Count = " + listSectionSelected.Count);
        }

        // check index have real.
        private bool isRangeIndex(int myRow,int myCol)
        {
            return (myRow >= 0 && myRow < row && myCol >= 0 && myCol < col);
        }

        private bool hasItem(int myRow, int myCol)
        {
            return boardSection[myRow, myCol].getPlayerHolder() != 0;
        }

        // display path can walk.
        public void searchPath(int myRow, int myCol)
        {
            int i= myRow;
            int j = myCol;

            // up
            i = myRow -1;
            j = myCol;
            while (isRangeIndex(i, j) && !hasItem(i, j))
            {
                addSectionToListSectionSelected(i, j, -1, -1);
                boardSection[i, j].getObjectPanelSection().BackColor = COLOR_CAN_WALK;                    
                i--;
            }

            // Down
            i = myRow + 1;
            j = myCol;
            while (isRangeIndex(i, j) && !hasItem(i,j))
            {
                addSectionToListSectionSelected(i, j, -1, -1);
                boardSection[i, j].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                i++;
            }

            // left
            i = myRow;
            j = myCol-1;
            while (isRangeIndex(i, j) && !hasItem(i, j))
            {
                addSectionToListSectionSelected(i, j, -1, -1);
                boardSection[i, j].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                j--;
            }

            // right
            i = myRow;
            j = myCol + 1;
            while (isRangeIndex(i, j) && !hasItem(i, j))
            {
                addSectionToListSectionSelected(i, j, -1, -1);
                boardSection[i, j].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                j++;
            }
        }

        //get enemy player.
        private int getEnemy(int player)
        {
            if (player == 1) return 2;
            else if (player == 2) return 1;
            return 0;
        }

        // get player holder section.
        private int getPlayerHolder(int id_row,int id_col)
        {
            return boardSection[id_row, id_col].getPlayerHolder();
        }

        // add id section row col to list temp.
        private void addSectionToListTemp(int i, int j)
        {
            listTemp.Add(new Tuple<int, int>(i, j));
        }

        // clear all item in list.
        private void clearListTemp()
        {
            listTemp.Clear();
        }

        private void moveAllItem(List<Tuple<int, int>> source,List<Tuple<int, int>> destination)
        {
            foreach (Tuple<int, int> s in source)
            {
                destination.Add(s);
            }
            source.Clear();
        }

        // add id section row col to list.
        private void addSectionToListSectionDoubleSideKillFuture(int i, int j)
        {
            listSectionDoubleSideKillFuture.Add(new Tuple<int, int>(i, j));
        }

        // clear all item in list.
        private void clearListSectionDoubleSideKillFuture()
        {
            listSectionDoubleSideKillFuture.Clear();
        }

        // add id section row col to list.
        private void addSectionToListSectionCentralKillFuture(int i, int j)
        {
            listSectionCentralKillFuture.Add(new Tuple<int, int>(i, j));
        }

        // clear all item in list.
        private void clearListSectionCentralKillFuture()
        {
            listSectionCentralKillFuture.Clear();
        }

        // delete item from section all in list.

        private void deleteAllInListSectionKillFuture()
        {
            deleteAllInListSectionDoubleSideKillFuture();
            deleteAllInListSectionCentralKillFuture();
        }

        private void deleteAllInListSectionDoubleSideKillFuture()
        {
            foreach (Tuple<int, int> section in listSectionDoubleSideKillFuture)
            {
                Console.Write("\n" + section.Item1 + "," + section.Item2);
                deleteItem(section.Item1, section.Item2);
            }
            clearListSectionDoubleSideKillFuture();
        }

        private void deleteAllInListSectionCentralKillFuture()
        {
            foreach (Tuple<int, int> section in listSectionCentralKillFuture)
            {
                Console.Write("\n" + section.Item1 + "," + section.Item2);
                deleteItem(section.Item1, section.Item2);
            }
            clearListSectionCentralKillFuture();
        }

        // check and search item enemy for delete.
        private void checkKill(int id_row, int id_col, int player)
        {
            //int enemy = getEnemy(player);
            //int i = id_row;
           // int j = id_col;

            //check kill corner.
            checkKillCorner(id_row, id_col, player);

            // check central kill. CK ,central kill is we can kill more any item.
            checkTopCentralKill  (id_row, id_col, player);
            checkDownCentralKill (id_row, id_col, player);
            checkLeftCentralKill (id_row, id_col, player);
            checkRightCentralKill(id_row, id_col, player);

            // cannot delete item now , because will effect in double side kill.
            // check double side kill. DSK, is we can kill most only 2 item. 
            checkTopDownDoubleSideKill  (id_row, id_col, player);
            checkLeftRightDoubleSideKill(id_row, id_col, player);

            deleteAllInListSectionCentralKillFuture();
            deleteAllInListSectionDoubleSideKillFuture();

            //clear all list.
            clearListTemp();
            clearListSectionCentralKillFuture();
            clearListSectionDoubleSideKillFuture();
        }

        private void checkKillCorner(int i,int j,int player)
        {
            int enemy = getEnemy(player);
            // case corner top-left. 
            if (i == 0 && j == 1)
            {
                if (boardSection[1, 0].getPlayerHolder() == player && boardSection[0, 0].getPlayerHolder() == enemy)
                {
                    listSectionCentralKillFuture.Add(new Tuple<int, int>(0, 0));
                    //deleteItem(0, 0);
                    return;
                }
            }
            if (i == 1 && j == 0)
            {
                if (boardSection[0, 1].getPlayerHolder() == player && boardSection[0, 0].getPlayerHolder() == enemy)
                {
                    listSectionCentralKillFuture.Add(new Tuple<int, int>(0, 0));
                    //deleteItem(0, 0);
                    return;

                }
            }

            // case corner top-right. 
            if (i == 0 && j == col - 2)
            {
                if (boardSection[1, col - 1].getPlayerHolder() == player && boardSection[0, col-1].getPlayerHolder() == enemy)
                {
                    listSectionCentralKillFuture.Add(new Tuple<int, int>(0,col-1));
                    //deleteItem(0, col-1);
                    return;
                }
            }
            if (i == 1 && j == col-1)
            {
                if (boardSection[0, col - 2].getPlayerHolder() == player && boardSection[0, col - 1].getPlayerHolder() == enemy)
                {
                    listSectionCentralKillFuture.Add(new Tuple<int, int>(0, col-1));
                    //deleteItem(0, col-1);
                    return;
                }
            }

            // case corner down-left. 
            if (i == row-2 && j == 0)
            {
                if (boardSection[row - 1, 1].getPlayerHolder() == player && boardSection[row-1, 0].getPlayerHolder() == enemy)
                {
                    listSectionCentralKillFuture.Add(new Tuple<int, int>(row-1, 0));
                    //deleteItem(row-1, 0);
                    return;
                }
            }
            if (i == row-1 && j == 1)
            {
                if (boardSection[row - 2, 0].getPlayerHolder() == player && boardSection[row - 1, 0].getPlayerHolder() == enemy)
                {
                    listSectionCentralKillFuture.Add(new Tuple<int, int>(row-1, 0));
                    //deleteItem(row - 1, 0);
                    return;
                }
            }

            // case corner down-right. 
            if (i == row - 2 && j == col-1)
            {
                if (boardSection[row - 1, col - 2].getPlayerHolder() == player && boardSection[row - 1, col-1].getPlayerHolder() == enemy)
                {
                    listSectionCentralKillFuture.Add(new Tuple<int, int>(row-1, col-1));
                    //deleteItem(row - 1, col -1);
                    return;
                }
            }
            if (i == row - 1 && j == col-2)
            {
                if (boardSection[row - 2, col - 1].getPlayerHolder() == player && boardSection[row - 1, col - 1].getPlayerHolder() == enemy)
                {
                    listSectionCentralKillFuture.Add(new Tuple<int, int>(row - 1, col - 1));
                    //deleteItem(row - 1, col -1);
                    return;
                }
            }
        }

        // check killing , Central kill.
        private bool workingCentralKill(int i, int j, int player, int enemy)
        {
            if (getPlayerHolder(i, j) == enemy)
            {
                addSectionToListTemp(i, j);
                return true;  // continue loop.
            }

            if (getPlayerHolder(i, j) == 0)  // found empty section.
            {
                clearListTemp();    // can not any kill. cancel search.
                return false;
            }

            if (getPlayerHolder(i, j) == player) // kill complete.
            {
                if (listTemp.Count > 0)
                {
                    moveAllItem(listTemp, listSectionCentralKillFuture); // move item temp to list.
                    //MessageBox.Show("xxx");
                }
                return false;
            }
            return false;
        }

        private void checkTopCentralKill(int i, int j, int player)
        {
            i--;
            bool flag = true ;
            int enemy = getEnemy(player);
            while (isRangeIndex(i, j) && flag)
            {
                flag = workingCentralKill(i, j, player, enemy);
                i--;
                if (!isRangeIndex(i, j) || !flag)
                {
                    clearListTemp();
                }
            }
        }

        private void checkDownCentralKill(int i, int j, int player)
        {
            i++;
            bool flag = true;
            int enemy = getEnemy(player);
            while (isRangeIndex(i, j) && flag)
            {
                flag = workingCentralKill(i, j, player, enemy);
                i++;
                if (!isRangeIndex(i, j) || !flag)
                {
                    clearListTemp();
                }
            }
            
        }

        private void checkLeftCentralKill(int i, int j, int player)
        {
            j--; ;
            bool flag = true;
            int enemy = getEnemy(player);
            while (isRangeIndex(i, j) && flag)
            {
                flag = workingCentralKill(i, j, player, enemy);
                j--;
                if (!isRangeIndex(i, j) || !flag)
                {
                    clearListTemp();
                }
            }
        }

        private void checkRightCentralKill(int i, int j, int player)
        {
            j++;
            bool flag = true;
            int enemy = getEnemy(player);
            while (isRangeIndex(i, j) && flag)
            {
                flag = workingCentralKill(i, j, player, enemy);
                j++;
                if (!isRangeIndex(i, j) || !flag)
                {
                    clearListTemp();
                }
                //Console.Write("u");
            }
        }

        // Interface check top - down.
        private bool checkTopDownDoubleSideKill(int i, int j, int player)
        {
            bool flag1 =checkTopDoubleSideKill(i, j, player);
            bool flag2 = checkDownDoubleSideKill(i, j, player);
            if (flag1 && flag2)
            {
                moveAllItem(listTemp, listSectionDoubleSideKillFuture);
                clearListTemp();
            }
            else
            {
                clearListTemp();
            }
            return false;
        }

        // Interface check left - right.
        private bool checkLeftRightDoubleSideKill(int i, int j, int player)
        {
            bool flag1 = checkLeftDoubleSideKill(i, j, player);
            bool flag2 = checkRightDoubleSideKill(i, j, player);
            if (flag1 && flag2)
            {
                moveAllItem(listTemp, listSectionDoubleSideKillFuture);
                clearListTemp();
            }
            else
            {
                clearListTemp();
            }
            return false;
        }

        // check killing , Double Side kill.
        private bool checkTopDoubleSideKill(int i, int j, int player)
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
                if (getPlayerHolder(i, j) == 0)
                {
                    return false;
                }

                if (getPlayerHolder(i, j) == enemy)
                {
                    addSectionToListTemp(i, j);
                    return true;
                }
                i--;
            }
            return false;
        }

        private bool checkDownDoubleSideKill(int i, int j, int player)
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
                if (getPlayerHolder(i, j) == 0)
                {
                    return false;
                }

                if (getPlayerHolder(i, j) == enemy)
                {
                    addSectionToListTemp(i, j);
                    return true;
                }
                i++;
            }
            return false;
        }

        private bool checkLeftDoubleSideKill(int i, int j, int player)
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
                if (getPlayerHolder(i, j) == 0)
                {
                    return false;
                }

                if (getPlayerHolder(i, j) == enemy)
                {
                    addSectionToListTemp(i, j);
                    return true;
                }
                j--;
            }
            return false;
        }

        private bool checkRightDoubleSideKill(int i, int j, int player)
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
                if (getPlayerHolder(i, j) == 0)
                {
                    return false;
                }

                if (getPlayerHolder(i, j) == enemy)
                {
                    addSectionToListTemp(i, j);
                    return true;
                }
                j++;
            }
            return false;
        }

        private bool haveKilling(){
            if (listSectionCentralKillFuture.Count > 0 || listSectionDoubleSideKillFuture.Count > 0)
            {
                return true;
            }
            return false;
        }

        public void searchForcedKill(int player,Item[] item)
        {
            List<Tuple<int, int>> listCanWalk = new List<Tuple<int, int>>();
            foreach(Item myitem in item)
            {
                if (myitem != null)
                {
                    int id_row = myitem.getIdRow();
                    int id_col = myitem.getIdCol();
                    //Console.Write("\n item = " + id_row + "," + id_col);
                    if (isRangeIndex(id_row, id_col))
                    {
                        listCanWalk.Clear();
                        workingSearchPath(id_row, id_col, listCanWalk); // search location and add to list.
                        //Console.Write(" " + listCanWalk.Count);
                        foreach (Tuple<int, int> tuple in listCanWalk)
                        {
                            int id_row_tuple = tuple.Item1;
                            int id_col_tuple = tuple.Item2;

                            // check central kill. CK ,central kill is we can kill more any item.
                            // for fast process , so insert havekill() between checkKill.

                            listSectionCentralKillFuture.Clear();

                            checkKillCorner(id_row_tuple, id_col_tuple, player);
                            if (haveKilling())
                            {
                                listSectionItemKiller.Add(new Tuple<int, int>(id_row, id_col));
                                listSectionItemKillerFuture.Add(new Tuple<int, int>(id_row_tuple, id_col_tuple));
                                listSectionCentralKillFuture.Clear();
                            }

                            checkTopCentralKill(id_row_tuple, id_col_tuple, player);
                            if (haveKilling())
                            {
                                listSectionItemKiller.Add(new Tuple<int, int>(id_row, id_col));
                                listSectionItemKillerFuture.Add(new Tuple<int, int>(id_row_tuple, id_col_tuple));
                                listSectionCentralKillFuture.Clear();
                            }

                            checkDownCentralKill(id_row_tuple, id_col_tuple, player);
                            if (haveKilling())
                            {
                                listSectionItemKiller.Add(new Tuple<int, int>(id_row, id_col));
                                listSectionItemKillerFuture.Add(new Tuple<int, int>(id_row_tuple, id_col_tuple));
                                listSectionCentralKillFuture.Clear();
                            }

                            checkLeftCentralKill(id_row_tuple, id_col_tuple, player);
                            if (haveKilling())
                            {
                                listSectionItemKiller.Add(new Tuple<int, int>(id_row, id_col));
                                listSectionItemKillerFuture.Add(new Tuple<int, int>(id_row_tuple, id_col_tuple));
                                listSectionCentralKillFuture.Clear();
                            }

                            checkRightCentralKill(id_row_tuple, id_col_tuple, player);
                            if (haveKilling())
                            {
                                listSectionItemKiller.Add(new Tuple<int, int>(id_row, id_col));
                                listSectionItemKillerFuture.Add(new Tuple<int, int>(id_row_tuple, id_col_tuple));
                                listSectionCentralKillFuture.Clear();
                            }

                            // check double side kill. DSK, is we can kill most only 2 item. 
                            checkTopDownDoubleSideKill(id_row_tuple, id_col_tuple, player);
                            if (haveKilling())
                            {
                                listSectionItemKiller.Add(new Tuple<int, int>(id_row, id_col));
                                listSectionItemKillerFuture.Add(new Tuple<int, int>(id_row_tuple, id_col_tuple));
                                listSectionDoubleSideKillFuture.Clear();
                            }

                            checkLeftRightDoubleSideKill(id_row_tuple, id_col_tuple, player);
                            if (haveKilling())
                            {
                                listSectionItemKiller.Add(new Tuple<int, int>(id_row, id_col));
                                listSectionItemKillerFuture.Add(new Tuple<int, int>(id_row_tuple, id_col_tuple));
                                listSectionDoubleSideKillFuture.Clear();
                            }
                            listSectionCentralKillFuture.Clear();
                            listSectionDoubleSideKillFuture.Clear();
                        }

                        //Console.Write("\nList Killer = "+listSectionItemKiller.Count+"");
                        //Console.Write("\nList Killer F = " + listSectionItemKillerFuture.Count + "");
                        listCanWalk.Clear();
                        listSectionCentralKillFuture.Clear();
                        listSectionDoubleSideKillFuture.Clear();
                    }
                }
            }
        }

        public void workingSearchPath(int myRow, int myCol, List<Tuple<int, int>> listCanWalk)
        {
            listCanWalk.Clear();
            int i = myRow;
            int j = myCol;

            // up
            i = myRow - 1;
            j = myCol;
            while (isRangeIndex(i, j) && boardSection[i,j].getPlayerHolder()==0)
            {
                listCanWalk.Add(new Tuple<int, int>(i, j));
                i--;
            }

            // Down
            i = myRow + 1;
            j = myCol;
            while (isRangeIndex(i, j) && boardSection[i, j].getPlayerHolder() == 0)
            {
                listCanWalk.Add(new Tuple<int, int>(i, j));
                i++;
            }

            // left
            i = myRow;
            j = myCol - 1;
            while (isRangeIndex(i, j) && boardSection[i, j].getPlayerHolder() == 0)
            {
                listCanWalk.Add(new Tuple<int, int>(i, j));
                j--;
            }

            // right
            i = myRow;
            j = myCol + 1;
            while (isRangeIndex(i, j) && boardSection[i, j].getPlayerHolder() == 0)
            {
                listCanWalk.Add(new Tuple<int, int>(i, j));
                j++;
            }
        }



        //public bool isRangeIndex(int id_row, int id_col)
        //{
        //    if (id_row >= 0 && id_row < row && id_col >= 0 && id_col < col)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        public void callAIForBoardPinch()
        {
            bool lose = false;
            // MessageBox.Show("Start AI");
            // Initial object BoardSection For AI.
            AIBoradSection[,] tableBoardSectionForAI = new AIBoradSection[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    // get value important for AI assign to table.
                    int player = boardSection[i, j].getPlayerHolder();
                    int status = 0;
                    if (player > 0)
                    {
                        status = ((Item)(boardSection[i, j].getItemFromPanelSection())).getStatusItem();
                        //status = 1;
                    }
                    tableBoardSectionForAI[i, j] = new AIBoradSection(i, j, player, status);
                }
            }

            // Create Object AI , assign data and table to AI.
            AIBoardNeeb nicky = new AIBoardNeeb(row, col, tableBoardSectionForAI, itemPlayer1, itemPlayer2);
            Console.Write("Start AI : ");
            Console.Write("\n\n");
            //nicky.setMaxStep(3);
            int point = nicky.run(0);
            //MessageBox.Show("" + point);

            List<Tuple<int, int, int, int>> listBestAnswer = nicky.getListBestAnswer2();
            if (listBestAnswer.Count <=0)
            {
                lose = true;
                //MessageBox.Show("Error , ");
            }
            List<Tuple<int, int>> listBestAnswerKillEnemy = nicky.getListBestAnswerKillEnemy2();
            if (listBestAnswer != null)
            {
                Console.Write("\nCount listanswer = " + listBestAnswer.Count + "\n");
                foreach (Tuple<int, int, int, int> buffer in listBestAnswer)
                {
                    //Tuple<int, int, int, int, int, int> buffer = listBestAnswer.First();
                    int id_row_source = buffer.Item1;
                    int id_col_source = buffer.Item2;
                    int id_row_des = buffer.Item3;
                    int id_col_des = buffer.Item4;
                    Console.Write("\n " + id_row_source +","+ id_col_source+" "+ id_row_des+","+ id_col_des
                       );
                    
                    moveItem(id_row_source, id_col_source, id_row_des, id_col_des);
                }
                listBestAnswer.Clear();
            }

            if (listBestAnswerKillEnemy != null)
            {
                Console.Write("\nCount listBestAnswerKillEnemy = " + listBestAnswerKillEnemy.Count + "\n");
                //Tuple<int, int, int, int, int, int> buffer = listBestAnswer.First();


                //List<Tuple<int, int, int, int, int, int>> listDouble = nicky.getListBestAnswerDoubleKill();
                //Console.Write("\nCount listdouble = " + listDouble.Count + "\n");
                //listBestAnswer.AddRange(listDouble);

                foreach (Tuple<int, int> buffer in listBestAnswerKillEnemy)
                {
                    //Tuple<int, int, int, int, int, int> buffer = listBestAnswer.First();
                    int id_row = buffer.Item1;
                    int id_col = buffer.Item2;
                    Console.Write("\n " + id_row+ "," + id_col
                       );
                    
                    deleteItem(id_row, id_col);
                }
                listBestAnswerKillEnemy.Clear();
            }
            updateDataDeBugging();

            if (lose)
            {
                //
            }
        }

    }
}
