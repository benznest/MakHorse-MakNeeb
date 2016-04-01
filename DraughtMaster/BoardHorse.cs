using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DraughtMaster
{
    // Class for General Draught Game.
    public class BoardHorse : Board
    {
        // Flag for working.
        private static bool flag_AI = false;
        private static bool flag_Item_Killed = false; // true when have least 1 item killed and reset when killing complete.
        private static bool flag_Way_Killing = false;   // true when have way will killing happened.
        private static bool flag_Way_DoubleKill = false;  // true when have way will Double killing happened.
        private static bool flag_DoubleKill = false; // true when start kill item for doublekill way.
        private static bool flag_changeTurn = false;
        //private static bool flag_Start_DoubleKill = false; // true when starrt have double kill and killed 1 item.
        private static bool flag_Map_Stable = false;  // true when no want refresh map to color default.


        // store last id_row , id_col player walk. for use in AI analysis.
        private static int id_row_last_walk = -1;
        private static int id_col_last_walk = -1;

        private bool flag_lock_double_kill = false;

        // Constructor.
        public BoardHorse()
        {

        }

        public BoardHorse(Form myForm,Panel panelBoard, int row, int col, bool AI,MyBoardGame myBoardGame)
        {
            this.myBoardGame = myBoardGame;
            this.myForm = myForm;
            typeBoard = (int)type.Draught;
            flag_AI = AI;
            temp_row = -1;
            temp_col = -1;
            listSectionSelected = new List<Tuple<int, int,int,int>>();
            listSectionSelectedFuture = new List<Tuple<int, int, int, int>>();
            listSectionItemKilled = new List<Tuple<int, int>>();
            listItemActiveAI = new List<int>();

            listSectionItemKiller = new List<Tuple<int, int>>();  // use in forced kill mode.
            listSectionItemKillerFuture = new List<Tuple<int, int>>();  // use in forced kill mode.

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

        public void setFlagToDefault()
        {
            flag_Item_Killed = false;
            flag_Way_Killing = false;
            flag_Way_DoubleKill = false;
            flag_Map_Stable = false;
            flag_lock_double_kill = false;
        }

        private void showDataConsole()
        {
            Console.WriteLine("listSectionSelectedFuture.Count = " + listSectionSelectedFuture.Count);
            Console.WriteLine("listSectionSelected.Count = " + listSectionSelected.Count);
            Console.WriteLine("flag_Item_Killed  = " + flag_Item_Killed +
           "\r\nflag_Section_Killing =" + flag_Way_Killing +
            "\r\nflag_Section_DoubleKill =" + flag_Way_DoubleKill +
            "\r\nflag_Map_Stable =" + flag_Map_Stable);
        }

        // First event when click section.
        public void runEvent(int id_row, int id_col)
        {
            if (turnPlayer1 == true && flag_AI == true)
            {
                callAIForBoardHorse();
            }
            else if (flag_force_kill && listSectionSelected.Count > 0)
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
                            callAIForBoardHorse();
                            swapTurnPlayer();
                        }

                        listSectionItemKiller.Clear();
                        listSectionItemKillerFuture.Clear();
                        state_forced = false;
                    }
                    else
                    {
                        MessageBox.Show("Kill it !!");
                        clearListSectionSelected();  // clear value in listSectionSelected.
                        clearListSectionSelectedFuture();
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
                    //checkGameover();

                    if (turnPlayer1 == true && flag_AI == true)
                    {
                        callAIForBoardHorse();
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
                
                //checkGameover();
            }
            else
            {
                // if AI Mode , AI is player 1 
                turnEvent(id_row, id_col);
                //checkGameover();
                if (turnPlayer1 == true && flag_AI == true)
                {
                    callAIForBoardHorse();
                    swapTurnPlayer();
                }
            }

            //checkGameover();
            updateCounterActiveItem();
            updateDataDeBugging();
        }

        // Manage turn player.
        public void turnEvent(int id_row, int id_col)
        {
            Console.WriteLine("turn event.");
            flag_changeTurn = false;
            int playerHolderSection = boardSection[id_row,id_col].getPlayerHolder();

            // if double kill and not complete.
            if (turnPlayer1 == true && turnPlayer2 == false) // turn is player 1.
            {
                if (playerHolderSection == 1 || playerHolderSection == 0)
                {
                    if (flag_DoubleKill) // double kill Started.
                    {
                        // click in section can walk.
                        if (listSectionSelectedFuture.Count >= 0 && hasInListSectionSelected(id_row, id_col))
                        {
                            flag_changeTurn = eventPlayer(id_row, id_col, 1);
                        }
                        else if (hasInListSectionSelected(id_row, id_col) == false) // click section empty or not in list.
                        {
                            // not do .
                            if (listSectionSelectedFuture.Count == 0)
                            {
                                flag_DoubleKill = false;
                            }
                        }
                    }
                    else
                    {
                        flag_changeTurn = eventPlayer(id_row, id_col, 1);
                    }
                }
            }
            else if (turnPlayer1 == false && turnPlayer2 == true) // // turn is player 2.
            {
                if (playerHolderSection == 2 || playerHolderSection == 0)
                {
                    if (flag_DoubleKill) // double kill Started.
                    {
                        // click in section can walk.
                        if (listSectionSelectedFuture.Count >= 0 && hasInListSectionSelected(id_row, id_col))
                        {
                            flag_changeTurn = eventPlayer(id_row, id_col, 2);
                        }
                        else if (hasInListSectionSelected(id_row, id_col) == false) // click section empty or not in list.
                        {
                            // not do .
                            if(listSectionSelectedFuture.Count == 0 ){
                                flag_DoubleKill = false;
                            }
                        }
                    }
                    else
                    {
                        flag_changeTurn = eventPlayer(id_row, id_col, 2);
                    }
                }
            }
            else
            {
                MessageBox.Show("Error, turn mistake");
                Console.WriteLine("error.");
            }

            // swap turn. 
            if (flag_changeTurn && listSectionSelectedFuture.Count == 0)
            {
                swapTurnPlayer();
                setLabelTurnPlayer();
            }
            checkGameover();
        }

        // event in turn player1.
        public bool eventPlayer(int id_row,int id_col,int myPlayer)
        {
            //MessageBox.Show("flag_lock_double_kill = " + flag_lock_double_kill);
            int statusItem;
            // true when have killing or walk.
            bool flag_changeTurn = false;
            Console.WriteLine("-----------------------------");
            Console.WriteLine("[Before] data for player :" + myPlayer);
            showDataConsole();
            // have item in section.
            if (boardSection[id_row, id_col].hasItemInSection() && listSectionSelectedFuture.Count == 0 
                && flag_Item_Killed == false && flag_lock_double_kill==false) 
            {
                statusItem = ((Item)boardSection[id_row, id_col].getItemFromPanelSection()).getStatusItem();
                refreshColorOnBoard();

                if (statusItem ==1) // Item regular.
                {
                    if (boardSection[id_row, id_col].getPlayerHolder() == 1) // player 1 (Started Down of Board)
                    {
                        searchPathRegularItemForPlayer1(id_row, id_col,false); // search path in section
                        setFlagToDefault();
                    }
                    else if (boardSection[id_row, id_col].getPlayerHolder() == 2) // player 2
                    {
                        searchPathRegularItemForPlayer2(id_row, id_col,false); // search path in section
                        setFlagToDefault();
                    }
                }
                else if (statusItem == 2) // SuperItem (Hot)
                {
                    int player = boardSection[id_row, id_col].getPlayerHolder();
                    searchPathSuperItem(id_row, id_col,player);
                }
                setTempRowCol(id_row, id_col); // save started point.
            }
            else if (boardSection[id_row, id_col].hasItemInSection() == false) // click panel empty.
            {
                // if (hasInListSectionSelected(id_row, id_col))
                Tuple<int,int> sectionKilled = getIdSectionKilledListSectionSelected(id_row, id_col);
                if (sectionKilled != null) // Have Killing.
                {
                    
                    // store id , temp for analysis in AI.
                    id_row_last_walk = id_row;
                    id_col_last_walk = id_col;

                    flag_changeTurn = true;  

                    flag_Item_Killed = true;//

                    moveItem(temp_row, temp_col, id_row, id_col); // have id in listSectionSelected is can walk.
                    deleteItem(sectionKilled.Item1, sectionKilled.Item2);  // remove Item enemy.
                    removeItemInListSectionSelectedFuture(temp_row, temp_col);

                    if (listSectionSelectedFuture.Count > 0) // is part of Double kill.
                    {
                        flag_DoubleKill = true;
                        flag_lock_double_kill = true;
                    }
                    else // single kill or last item of double kill.
                    {
                        flag_DoubleKill = false;
                        flag_lock_double_kill = false;
                        flag_changeTurn = true;

                    }

                    statusItem = ((Item)boardSection[id_row, id_col].getItemFromPanelSection()).getStatusItem();
                    if (id_row == 0 && statusItem == 1) // Goal.in area down.
                    {
                        //MessageBox.Show("Hot!!");
                        chageToSuperItem(id_row, id_col);
                        flag_changeTurn = true;
                        flag_lock_double_kill = false;
                        setFlagToDefault();
                        refreshColorOnBoard();

                        return flag_changeTurn;
                    }
                    else if (id_row == row-1 && statusItem == 1) // Goal.in area up.
                    {
                        chageToSuperItem(id_row, id_col);
                        flag_changeTurn = true;
                        flag_lock_double_kill = false;
                        setFlagToDefault();
                        refreshColorOnBoard();

                        return flag_changeTurn;
                    }
                }
                else  // not have killing , but will have in list.
                {
                    if (flag_Item_Killed)
                    {
                        return false;
                    }

                    // walk general .
                    if (hasInListSectionSelected(id_row, id_col)) // have id in listSectionSelected is can walk.
                    {
                        //flag_DoubleKill = false;
                        listSectionSelectedFuture.Clear();

                        // store id , temp for analysis in AI.
                        id_row_last_walk = id_row;
                        id_col_last_walk = id_col;

                        flag_changeTurn = true;
                        moveItem(temp_row, temp_col, id_row, id_col);

                        statusItem = ((Item)boardSection[id_row, id_col].getItemFromPanelSection()).getStatusItem();
                        if (id_row == 0 && statusItem == 1) // Goal.
                        {
                            chageToSuperItem(id_row, id_col);
                            flag_changeTurn = true;
                            flag_lock_double_kill = false;
                            setFlagToDefault();
                            refreshColorOnBoard();

                            return flag_changeTurn;
                        }
                        else if (id_row == row - 1 && statusItem == 1) // Goal.
                        {
                            chageToSuperItem(id_row, id_col);
                            flag_changeTurn = true;
                            flag_lock_double_kill = false;
                            setFlagToDefault();
                            refreshColorOnBoard();

                            return flag_changeTurn;
                        }
                    }
                    else
                    {
                          clearListSectionSelectedFuture();
                    }
                }

                // not have double killing unfinished.
                //if (flag_Map_Stable == false && listSectionSelectedFuture.Count == 0 && flag_Item_Killed == false)
                //{
                if (flag_lock_double_kill == false && flag_Item_Killed == false)
                {
                    clearListSectionSelected();  // clear value in listSectionSelected.
                    refreshColorOnBoard();
                }

                    // can kill more. 
                if (listSectionSelectedFuture.Count > 0 || listSectionSelected.Count > 0)
                {
                    // // // //
                    if (listSectionSelectedFuture.Count == 0 && listSectionSelected.Count == 1 && flag_Item_Killed)
                    {
                        if (listSectionSelected.First().Item1 == id_row && listSectionSelected.First().Item2 == id_col)
                        {
                            setFlagToDefault();
                            refreshColorOnBoard();
                            flag_changeTurn = true;
                            clearListSectionSelectedFuture();
                            clearListSectionSelected();
                            return flag_changeTurn;
                        }
                    }

                    if (boardSection[id_row, id_col].getItemFromPanelSection() != null)
                    {
                        // can killing more , so still old turn player.

                        flag_changeTurn = false;
                        // check if super Item (hot) 
                        if (((Item)(boardSection[id_row, id_col].getItemFromPanelSection())).isSuperItem())
                        {
                            clearListSectionSelectedFuture();
                            //listSectionItemKilled.Clear(); // must clear because searchDoubleKill listkill will dupicate.
                            clearListSectionSelected();
                            refreshColorOnBoard();
                            int player = boardSection[id_row, id_col].getPlayerHolder(); // get player.
                            //searchPathSuperItem(id_row, id_col, player);

                            listSectionItemKilled.Clear();
                            searchPathSuperItemDoubleKill(id_row, id_col, player, true);
                            if (listSectionSelected.Count <= 0 && listSectionSelectedFuture.Count <= 0)
                            {
                                setFlagToDefault();
                                flag_changeTurn = true;
                                
                            }
                            //MessageBox.Show(""+listSectionSelected.Count);
                        }
                        else if (((Item)(boardSection[id_row, id_col].getItemFromPanelSection())).isRegularItem())
                        {
                            clearListSectionSelectedFuture();
                            clearListSectionSelected();
                            refreshColorOnBoard();
                            if (myPlayer == 1)
                            {
                                searchPathRegularItemForPlayer1(id_row, id_col,true); // search path in section
                                if (listSectionSelected.Count <= 0 && listSectionSelectedFuture.Count <= 0)
                                {
                                    flag_changeTurn = true;
                                    flag_lock_double_kill = false;
                                    setFlagToDefault();
                                }
                            }
                            else
                            {
                                searchPathRegularItemForPlayer2(id_row, id_col,true); // search path in section
                                if (listSectionSelected.Count <= 0 && listSectionSelectedFuture.Count <= 0)
                                {
                                    flag_changeTurn = true;
                                    flag_lock_double_kill = false;
                                    setFlagToDefault();
                                }
                            }
                        }
                        setTempRowCol(id_row, id_col);
                    }
                    else
                    {
                        if (flag_Item_Killed)
                        {
                            flag_lock_double_kill = true;
                            //MessageBox.Show("" + id_row + "-" + id_col);
                        }
                    }
                    //MessageBox.Show("xxx");   
                }
                else
                {
                    setFlagToDefault();
                }
                    
                //}
            }

   
            //else
            //{
            //    setFlagToDefault();
            //    clearListSectionSelectedFuture();
            //    clearListSectionSelected();  // clear value in listSectionSelected.
            //    refreshColorOnBoard();
            //}
            Console.WriteLine("[After] data for player :" + myPlayer);
            showDataConsole();
            return flag_changeTurn;
        }

        // Regular Item player 2 (Down to up)
        public void searchPathRegularItemForPlayer2(int myRow,int myCol,bool modeKillOnly)
        {
            bool flag_left = searchTopLeftRegularItem(myRow, myCol, modeKillOnly);
            bool flag_right = searchTopRightRegularItem(myRow, myCol, modeKillOnly);

            if (flag_left && myRow -1 -1 >=0 && myCol-1-1>=0)
            {
                searchMarkPathWayDoubleForPlayer2(myRow -1 -1, myCol-1 -1);
            }
            if (flag_right && myRow - 1 - 1 >= 0 && myCol + 1 + 1 <col)
            {
                searchMarkPathWayDoubleForPlayer2(myRow - 1 - 1, myCol + 1 + 1);
            }
        }

        public void searchMarkPathWayDoubleForPlayer2(int myRow, int myCol)
        {
            bool flag_left = false;
            bool flag_right = false;
            
            // check having kill.
            if (myRow - 1 - 1 >= 0 && myCol - 1 - 1 >= 0 ) 
            {
                flag_left = searchTopLeftPathRegularItemDoubleKill(myRow, myCol);
            }
            if (myRow - 1 - 1 >= 0 && myCol + 1 + 1 < col)
            {
                flag_right = searchTopRightPathRegularItemDoubleKill(myRow, myCol);
            }

            // Recursive. 
            if (flag_left && myRow - 1 - 1 >= 0 && myCol - 1 - 1 >= 0) // left can kill, and will double kill. 
            {
                searchMarkPathWayDoubleForPlayer2(myRow - 1 - 1, myCol - 1 - 1);
            }
            if (flag_right && myRow - 1 - 1 >= 0 && myCol + 1 + 1 < col) // right can kill, and will double kill. 
            {
                searchMarkPathWayDoubleForPlayer2(myRow - 1 - 1, myCol + 1 + 1);
            }
           
        }

        public bool searchTopLeftRegularItem(int myRow, int myCol, bool modeKillOnly)
        {
            if (myRow - 1 >= 0) // check index top have real in Board.
            {
                if (myCol - 1 >= 0 )
                { // check index left have real in Board.
                    if (boardSection[myRow - 1, myCol - 1].hasItemInSection()) // check that section have item.
                    {
                        if (boardSection[myRow - 1, myCol - 1].getPlayerHolder() == 1) // that section ,item is enemy.
                        {
                            if (((myRow - 1) - 1) >= 0 && ((myCol - 1) - 1) >= 0) // check next section after kill enemy have real in Board.
                            {
                                // check section after kill enemy is empty for to walk.
                                if (boardSection[myRow - 1 - 1, myCol - 1 - 1].hasItemInSection() == false)
                                {
                                        boardSection[myRow - 1 - 1, myCol - 1 - 1].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                                        flag_Way_Killing = true;

                                        // put point section to list , and assign section of enemy for killed.
                                        addSectionToListSectionSelected(myRow - 1 - 1, myCol - 1 - 1, myRow - 1, myCol - 1);
                                        return true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (modeKillOnly == false)
                        {
                            boardSection[myRow - 1, myCol - 1].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                            addSectionToListSectionSelected(myRow - 1, myCol - 1, -1, -1);
                        }
                    }

                }
            }
            return false;
        }

        public bool searchTopLeftPathRegularItemDoubleKill(int myRow, int myCol)
        {
            if (myRow - 1 >= 0) // check index top have real in Board.
            {
                if (myCol - 1 >= 0)
                { // check index left have real in Board.
                    if (boardSection[myRow - 1, myCol - 1].hasItemInSection()) // check that section have item.
                    {
                        if (boardSection[myRow - 1, myCol - 1].getPlayerHolder() == 1) // that section ,item is enemy.
                        {
                            if (((myRow - 1) - 1) >= 0 && ((myCol - 1) - 1) >= 0) // check next section after kill enemy have real in Board.
                            {
                                // check section after kill enemy is empty for to walk.
                                if (boardSection[myRow - 1 - 1, myCol - 1 - 1].hasItemInSection() == false)
                                {
                                   // MessageBox.Show("put row : "+(myRow - 1 - 1)+" ,col : "+( myCol - 1 - 1 )+"to futurelist");
                                    boardSection[myRow - 1 - 1, myCol - 1 - 1].getObjectPanelSection().BackColor = COLOR_MARKED;
                                    flag_Way_DoubleKill = true;
                                    addSectionToListSectionSelectedFuture(myRow,myCol,myRow-1-1,myCol-1-1);
                                    // put point section to list , and assign section of enemy for killed.
                                    //addSectionToListSectionSelected(myRow - 1 - 1, myCol - 1 - 1, myRow - 1, myCol - 1);
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool searchTopRightRegularItem(int myRow, int myCol,bool modeKillOnly)
        {
            // Top-Right ,Search path.
            if (myCol + 1 < col && myRow - 1 >=0) // index check right have real in Board.
            {
                if (boardSection[myRow - 1, myCol + 1].hasItemInSection()) // check that section have item.
                {
                    if (boardSection[myRow - 1, myCol + 1].getPlayerHolder() == 1) // that section ,item is enemy.
                    {
                        if (((myRow - 1) - 1) >= 0 && ((myCol + 1) + 1) < col) // check next section after kill enemy have real in Board.
                        {
                            // check section after kill enemy is empty for to walk.
                            if (boardSection[myRow - 1 - 1, myCol + 1 + 1].hasItemInSection() == false)
                            {
                                    boardSection[myRow - 1 - 1, myCol + 1 + 1].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                                    //flag_Section_DoubleKill = true;
                                    flag_Way_Killing = true;

                                    // put point section to list , and assign section of enemy for killed.
                                    addSectionToListSectionSelected(myRow - 1 - 1, myCol + 1 + 1, myRow - 1, myCol + 1);
                                    return true;
                            }
                        }
                    }
                }
                else // that section not item.
                {
                        // Mark section is selected for can walk.
                    if (modeKillOnly == false)
                    {
                        boardSection[myRow - 1, myCol + 1].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                        addSectionToListSectionSelected(myRow - 1, myCol + 1, -1, -1);
                    }
                }
            }
            return false;
        }

        public bool searchTopRightPathRegularItemDoubleKill(int myRow, int myCol)
        {
            // Top-Right ,Search path.
            if (myCol + 1 < col) // index check right have real in Board.
            {
                if (boardSection[myRow - 1, myCol + 1].hasItemInSection()) // check that section have item.
                {
                    if (boardSection[myRow - 1, myCol + 1].getPlayerHolder() == 1) // that section ,item is enemy.
                    {
                        if (((myRow - 1) - 1) >= 0 && ((myCol + 1) + 1) < col) // check next section after kill enemy have real in Board.
                        {
                            // check section after kill enemy is empty for to walk.
                            if (boardSection[myRow - 1 - 1, myCol + 1 + 1].hasItemInSection() == false)
                            {
                                boardSection[myRow - 1 - 1, myCol + 1 + 1].getObjectPanelSection().BackColor = COLOR_MARKED;
                                flag_Way_DoubleKill = true;
                                addSectionToListSectionSelectedFuture(myRow, myCol, myRow - 1 - 1, myCol + 1 + 1);
                                // put point section to list , and assign section of enemy for killed.
                                //addSectionToListSectionSelected(myRow - 1 - 1, myCol + 1 + 1, myRow - 1, myCol + 1);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }


        // Regular Item player 1 (up to down)
        public void searchPathRegularItemForPlayer1(int myRow, int myCol,bool modeKillOnly)
        {
            bool flag_left = searchDownLeftRegularItem(myRow, myCol, modeKillOnly);
            bool flag_right = searchDownRightRegularItem(myRow, myCol, modeKillOnly);

            if (flag_left && myRow + 1 + 1 < row && myCol - 1 - 1  >= 0)
            {
                searchMarkPathWayDoubleForPlayer1(myRow + 1 + 1, myCol - 1 - 1); // left
            }
            if (flag_right && myRow + 1 + 1 < row && myCol + 1 + 1 < col)
            {
                searchMarkPathWayDoubleForPlayer1(myRow + 1 + 1, myCol + 1 + 1); // right
            }
        }

        public void searchMarkPathWayDoubleForPlayer1(int myRow, int myCol)
        {
            bool flag_left = false;
            bool flag_right = false;

            // check having kill.
            if (myRow + 1 + 1 < row && myCol - 1 - 1 >= 0)
            {
                flag_left = searchDownLeftPathRegularItemDoubleKill(myRow, myCol);
            }
            if (myRow + 1 + 1 < row && myCol + 1 + 1 < col)
            {
                flag_right = searchDownRightPathRegularItemDoubleKill(myRow, myCol);
            }

            // Recursive. 
            if (flag_left && myRow + 1 + 1 < row && myCol - 1 - 1 >= 0) // left can kill, and will double kill. 
            {
                searchMarkPathWayDoubleForPlayer1(myRow + 1 + 1, myCol - 1 - 1);
            }
            if (flag_right && myRow + 1 + 1 < row && myCol + 1 + 1 < col) // right can kill, and will double kill. 
            {
                searchMarkPathWayDoubleForPlayer1(myRow + 1 + 1, myCol + 1 + 1);
            }

        }

        public bool searchDownLeftRegularItem(int myRow, int myCol,bool modeKillOnly)
        {
            if (myRow + 1 < row) // check index top have real in Board.
            {
                if (myCol - 1 >= 0)
                { // check index left have real in Board.
                    if (boardSection[myRow + 1, myCol - 1].hasItemInSection()) // check that section have item.
                    {
                        if (boardSection[myRow + 1, myCol - 1].getPlayerHolder() == 2) // that section ,item is enemy.
                        {
                            if (((myRow + 1) + 1) < row && ((myCol - 1) - 1) >= 0) // check next section after kill enemy have real in Board.
                            {
                                // check section after kill enemy is empty for to walk.
                                if (boardSection[myRow + 1 + 1, myCol - 1 - 1].hasItemInSection() == false)
                                {
                                    boardSection[myRow + 1 + 1, myCol - 1 - 1].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                                    flag_Way_Killing = true;

                                    // put point section to list , and assign section of enemy for killed.
                                    addSectionToListSectionSelected(myRow + 1 + 1, myCol - 1 - 1, myRow + 1, myCol - 1);
                                    return true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (modeKillOnly == false)
                        {
                            boardSection[myRow + 1, myCol - 1].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                            addSectionToListSectionSelected(myRow + 1, myCol - 1, -1, -1);
                        }
                    }

                }
            }
            return false;
        }

        public bool searchDownLeftPathRegularItemDoubleKill(int myRow, int myCol)
        {
            if (myRow + 1 < row) // check index top have real in Board.
            {
                if (myCol - 1 >= 0)
                { // check index left have real in Board.
                    if (boardSection[myRow + 1, myCol - 1].hasItemInSection()) // check that section have item.
                    {
                        if (boardSection[myRow + 1, myCol - 1].getPlayerHolder() == 2) // that section ,item is enemy.
                        {
                            if (((myRow + 1) + 1) < row && ((myCol - 1) - 1) >= 0) // check next section after kill enemy have real in Board.
                            {
                                // check section after kill enemy is empty for to walk.
                                if (boardSection[myRow + 1 + 1, myCol - 1 - 1].hasItemInSection() == false)
                                {
                                    // MessageBox.Show("put row : "+(myRow - 1 - 1)+" ,col : "+( myCol - 1 - 1 )+"to futurelist");
                                    boardSection[myRow + 1 + 1, myCol - 1 - 1].getObjectPanelSection().BackColor = COLOR_MARKED;
                                    flag_Way_DoubleKill = true;
                                    addSectionToListSectionSelectedFuture(myRow, myCol, myRow + 1 + 1, myCol - 1 - 1);
                                    // put point section to list , and assign section of enemy for killed.
                                    //addSectionToListSectionSelected(myRow - 1 - 1, myCol - 1 - 1, myRow - 1, myCol - 1);
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool searchDownRightRegularItem(int myRow, int myCol,bool modeKillOnly)
        {
            // Top-Right ,Search path.
            if (myRow + 1 < row && myCol + 1 < col) // index check right have real in Board.
            {
                if (boardSection[myRow + 1, myCol + 1].hasItemInSection()) // check that section have item.
                {
                    if (boardSection[myRow + 1, myCol + 1].getPlayerHolder() == 2) // that section ,item is enemy.
                    {
                        if (((myRow + 1) + 1) < row && ((myCol + 1) + 1) < col) // check next section after kill enemy have real in Board.
                        {
                            // check section after kill enemy is empty for to walk.
                            if (boardSection[myRow + 1 + 1, myCol + 1 + 1].hasItemInSection() == false)
                            {
                                boardSection[myRow + 1 + 1, myCol + 1 + 1].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                                //flag_Section_DoubleKill = true;
                                flag_Way_Killing = true;

                                // put point section to list , and assign section of enemy for killed.
                                addSectionToListSectionSelected(myRow + 1 + 1, myCol + 1 + 1, myRow + 1, myCol + 1);
                                return true;
                            }
                        }
                    }
                }
                else // that section not item.
                {
                    // Mark section is selected for can walk.
                    if (modeKillOnly == false)
                    {
                        boardSection[myRow + 1, myCol + 1].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                        addSectionToListSectionSelected(myRow + 1, myCol + 1, -1, -1);
                    }
                }
            }
            return false;
        }

        public bool searchDownRightPathRegularItemDoubleKill(int myRow, int myCol)
        {
            // Top-Right ,Search path.
            if (myRow +1 < row && myCol + 1 < col) // index check right have real in Board.
            {
                if (boardSection[myRow + 1, myCol + 1].hasItemInSection()) // check that section have item.
                {
                    if (boardSection[myRow + 1, myCol + 1].getPlayerHolder() == 2) // that section ,item is enemy.
                    {
                        if (((myRow + 1) + 1) < row && ((myCol + 1) + 1) < col) // check next section after kill enemy have real in Board.
                        {
                            // check section after kill enemy is empty for to walk.
                            if (boardSection[myRow + 1 + 1, myCol + 1 + 1].hasItemInSection() == false)
                            {
                                boardSection[myRow + 1 + 1, myCol + 1 + 1].getObjectPanelSection().BackColor = COLOR_MARKED;
                                flag_Way_DoubleKill = true;
                                addSectionToListSectionSelectedFuture(myRow, myCol, myRow + 1 + 1, myCol + 1 + 1);
                                // put point section to list , and assign section of enemy for killed.
                                //addSectionToListSectionSelected(myRow - 1 - 1, myCol + 1 + 1, myRow - 1, myCol + 1);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }






        //public void searchPathWayForPlayer1(int myRow, int myCol)
        //{

        //    // For Regular item Player1. (Up of Board)
        //    // Down-Left ,Search path.
        //    if (myRow + 1 < row) // index check down have real in Board..
        //    {
        //        if (myCol - 1 >= 0) // check index left have real in Board.
        //        {
        //            if (boardSection[myRow + 1, myCol - 1].hasItemInSection()) // check that section have item.
        //            {
        //                if (boardSection[myRow + 1, myCol - 1].getPlayerHolder() == 2) // that section ,item is enemy.
        //                {
        //                    if (((myRow + 1) + 1) < row && ((myCol - 1) - 1) >= 0) // check next section after kill enemy have real in Board.
        //                    {
        //                        // check section after kill enemy is empty for to walk.
        //                        if (boardSection[myRow + 1 + 1, myCol - 1 - 1].hasItemInSection() == false)
        //                        {
        //                            // Mark section is selected for can walk.  
        //                            boardSection[myRow + 1 + 1, myCol - 1 - 1].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
        //                            // put point section to list , and assign section of enemy for killed.
        //                            addSectionToListSectionSelected(myRow + 1 + 1, myCol - 1 - 1, myRow + 1, myCol - 1);
        //                        }
        //                    }
        //                }
        //            }
        //            else // that section not item.
        //            {
        //                // Mark section is selected for can walk.
        //                boardSection[myRow + 1, myCol - 1].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
        //                addSectionToListSectionSelected(myRow + 1, myCol - 1, -1, -1);
        //            }
        //        }

        //        if (myCol + 1 < col) // index check right have real in Board..
        //        {
        //            if (boardSection[myRow + 1, myCol + 1].hasItemInSection()) // check that section have item.
        //            {
        //                if (boardSection[myRow + 1, myCol + 1].getPlayerHolder() == 2) // that section ,item is enemy.
        //                {
        //                    if (((myRow + 1) + 1) < row && ((myCol + 1) + 1) < col) // check next section after kill enemy have real in Board.
        //                    {
        //                        // check section after kill enemy is empty for to walk.
        //                        if (boardSection[myRow + 1 + 1, myCol + 1 + 1].hasItemInSection() == false)
        //                        {
        //                            // Mark section is selected for can walk.  
        //                            boardSection[myRow + 1 + 1, myCol + 1 + 1].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
        //                            // put point section to list , and assign section of enemy for killed.
        //                            addSectionToListSectionSelected(myRow + 1 + 1, myCol + 1 + 1, myRow + 1, myCol + 1);
        //                        }
        //                    }
        //                }
        //            }
        //            else // that section not item.
        //            {
        //                // Mark section is selected for can walk.
        //                boardSection[myRow + 1, myCol + 1].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
        //                addSectionToListSectionSelected(myRow + 1, myCol + 1, -1, -1);
        //            }
        //        }
        //    }   
        //}
        
        // Move item from old section to new section.

        public void moveItem(int old_id_row,int old_id_col,int new_id_row,int new_id_col)
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
            else if(statusItem == 2){
                boardSection[new_id_row, new_id_col].setImageSuperItemToPictureBoxSection(player); // put image Super item.
            }
        }

        public void deleteItem(int id_row, int id_col)
        {
            playSoundItemKill();
            int player = boardSection[id_row, id_col].getPlayerHolder();
            if(player == 1){
                count_item_player1_active--;
            }
            else if(player == 2){
                count_item_player2_active--;
            }

            ((Item)(boardSection[id_row, id_col].getItemFromPanelSection())).clearIdRowAndCol(); // clear id address in Item. 
            boardSection[id_row, id_col].removeItemFromPanelSection();
            boardSection[id_row, id_col].removeImage(); // clear image in section.
            boardSection[id_row, id_col].setPlayerHolder(0);  // set empty player in section.

        }

        // Change Status Item to supet by use id_row,id_col boardSection.
        public void chageToSuperItem(int myRow,int myCol)
        {
            ((Item)boardSection[myRow, myCol].getItemFromPanelSection()).setStatusItem(2);
            int player = boardSection[myRow, myCol].getPlayerHolder();
            boardSection[myRow, myCol].setImageSuperItemToPictureBoxSection(player);
        }

        // Change Status Item to supet by use object Item.
        public void chageToSuperItem(Item item)
        {
            item.setStatusItem(2);
        }

        // search path for Super Item.
        private void searchPathSuperItem(int id_row,int id_col,int myPlayer)
        {
            Console.Write("\r\nx\r\n");
            searchTopLeftPathSuperItem(id_row - 1,id_col - 1,myPlayer);
            searchTopRightPathSuperItem(id_row - 1, id_col + 1, myPlayer);
            searchDownLeftPathSuperItem(id_row + 1, id_col - 1, myPlayer);
            searchDownRightPathSuperItem(id_row + 1, id_col + 1, myPlayer);

            if (flag_Way_DoubleKill == false) // have double kill least 1 time. not change turn.
            {
                //swapTurnPlayer();
            }

            listSectionItemKilled.Clear();
        }

        // search walk top left.    
        private void searchTopLeftPathSuperItem(int i,int j,int myPlayer){
            while(i>=0 && j>=0){
                if (boardSection[i, j].hasItemInSection() == false) // section not item.
                {
                    addSectionToListSectionSelected(i, j, -1, -1);
                    boardSection[i, j].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                    i--;
                    j--;
                }
                else // found item in section.
                {
                    if (boardSection[i, j].getPlayerHolder() != myPlayer) // found Enemy
                    {
                        if (i - 1 >= 0 && j - 1 >= 0) // check index next area have real.
                        {
                            if(boardSection[i - 1,j - 1].hasItemInSection() == false){ // next area is empty.
                                addSectionToListSectionItemKilled(i, j); // add section to list killed.
                                addSectionToListSectionSelected(i-1, j-1, i, j);
                                boardSection[i - 1, j - 1].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                                searchPathSuperItemDoubleKill(i - 1 , j - 1, myPlayer,false); // recursive. not can walk real , just discovery way.
                            }
                        }
                    }
                    break;  // found Section not empty. or has Item.
                }
            }
        }

        private void searchTopRightPathSuperItem(int i,int j,int myPlayer){
            // search walk top right.
            while (i >= 0 && j < col)
            {
                if (boardSection[i, j].hasItemInSection() == false)
                {
                    addSectionToListSectionSelected(i, j, -1, -1);
                    boardSection[i, j].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                    i--;
                    j++;
                }
                else
                {
                    if (boardSection[i, j].getPlayerHolder() != myPlayer) // found Enemy
                    {
                        if (i - 1 >= 0 && j + 1 < col) // check index next area have real.
                        {
                            if (boardSection[i - 1, j + 1].hasItemInSection() == false)
                            { // next area is empty.
                                addSectionToListSectionItemKilled(i, j); // add section to list killed.
                                addSectionToListSectionSelected(i - 1, j + 1, i, j);
                                boardSection[i - 1, j + 1].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                                searchPathSuperItemDoubleKill(i - 1, j + 1, myPlayer, false); // recursive. not can walk real , just discovery way.
                            }
                        }
                    }
                    break;  // found Section not empty. or has Item.
                }
            }
        }
    
        private void searchDownLeftPathSuperItem(int i,int j,int myPlayer){
            // search walk down left.
            while (i < row && j >=0)
            {

                if (boardSection[i, j].hasItemInSection() == false)
                {
                    addSectionToListSectionSelected(i, j, -1, -1);
                    boardSection[i, j].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                    i++;
                    j--;
                }
                else
                {
                    if (boardSection[i, j].getPlayerHolder() != myPlayer) // found Enemy
                    {
                        if (i + 1 < row && j - 1 >= 0) // check index next area have real.
                        {
                            if (boardSection[i + 1, j - 1].hasItemInSection() == false)
                            { // next area is empty.
                                addSectionToListSectionItemKilled(i, j); // add section to list killed.
                                addSectionToListSectionSelected(i + 1, j - 1, i, j);
                                boardSection[i + 1, j - 1].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                                searchPathSuperItemDoubleKill(i + 1, j - 1, myPlayer, false); // recursive. not can walk real , just discovery way.
                            }
                        }
                    }
                    break;  // found Section not empty. or has Item.
                }
                
            }
        }

        private void searchDownRightPathSuperItem(int i, int j, int myPlayer)
        {
            // search walk down right.
            while (i < row && j < col)
            {
                if (boardSection[i, j].hasItemInSection() == false)
                {
                    addSectionToListSectionSelected(i, j, -1, -1);
                    boardSection[i, j].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                    i++;
                    j++;
                }
                else
                {
                    if (boardSection[i, j].getPlayerHolder() != myPlayer) // found Enemy
                    {
                        if (i + 1 < row && j + 1 < col) // check index next area have real.
                        {
                            if (boardSection[i + 1, j + 1].hasItemInSection() == false)
                            { // next area is empty.
                                addSectionToListSectionItemKilled(i, j); // add section to list killed.
                                addSectionToListSectionSelected(i + 1, j + 1, i, j);
                                boardSection[i + 1, j + 1].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                                searchPathSuperItemDoubleKill(i + 1, j + 1, myPlayer, false); // recursive. not can walk real , just discovery way.
                            }
                        }
                    }
                    break;  // found Section not empty. or has Item.
                }

            }
        }

        // Inteface for searching path double kill in super Item.
        private void searchPathSuperItemDoubleKill(int id_row, int id_col, int myPlayer,bool ModeCanWalk)
        {
            Console.Write("\r\nrow = " + id_row + "  col = " + id_col);
            searchTopLeftPathSuperItemDoubleKill(id_row - 1, id_col - 1, myPlayer ,ModeCanWalk );
            searchTopRightPathSuperItemDoubleKill(id_row - 1, id_col + 1, myPlayer,ModeCanWalk);
            searchDownLeftPathSuperItemDoubleKill(id_row + 1, id_col - 1, myPlayer,ModeCanWalk);
            searchDownRightPathSuperItemDoubleKill(id_row + 1, id_col + 1, myPlayer, ModeCanWalk);
        }

        
        // search path Double Kill for Super Item
        private void searchTopLeftPathSuperItemDoubleKill(int i, int j, int myPlayer, bool ModeCanWalk)
        {
            List<Tuple<int, int>> listBufferSectionPath = new List<Tuple<int,int>>();
            // store point start.
            int row_start = i;
            int col_start = j;
            
            while (i >= 0 && j >= 0)
            {
                if (boardSection[i, j].hasItemInSection() == false)
                {
                    listBufferSectionPath.Add(new Tuple<int, int>(i, j));
                    i--;
                    j--;
                }
                else
                {
                    if (boardSection[i, j].getPlayerHolder() != myPlayer) // found Enemy
                    {
                        if (i - 1 >= 0 && j - 1 >= 0) // check index next area have real.
                        {
                            if (boardSection[i - 1, j - 1].getPlayerHolder() == 0 ) // next area is empty. and can kill.
                            {
                                if (hasInListSectionItemKilled(i,j) == false)
                                {
                                    flag_Way_DoubleKill = true;
                                    foreach (Tuple<int, int> buffer in listBufferSectionPath)
                                    {
                                        boardSection[buffer.Item1, buffer.Item2].getObjectPanelSection().BackColor = COLOR_MARKED;
                                    }

                                    addSectionToListSectionItemKilled(i, j); // add section killed.

                                    if (ModeCanWalk) // can walk to section.
                                    {
                                        addSectionToListSectionSelected(i-1,j-1,i,j);
                                        boardSection[i - 1, j - 1].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                                        ModeCanWalk = false; // change to discovery mode , because do one time only.
                                    }
                                    else // not can walk now , just discovery.
                                    {
                                        addSectionToListSectionSelectedFuture(row_start, col_start, i - 1, j - 1);
                                        boardSection[i - 1, j - 1].getObjectPanelSection().BackColor = COLOR_PATH;
                                    }
                                    searchPathSuperItemDoubleKill(i - 1, j - 1, myPlayer,ModeCanWalk);
                                }
                            }
                        }
                    }
                    listBufferSectionPath.Clear();
                    break;  // found Section not empty. or has Item.
                }
            }
        }

        // search path Double Kill for Super Item
        private void searchTopRightPathSuperItemDoubleKill(int i, int j, int myPlayer, bool ModeCanWalk)
        {
            List<Tuple<int, int>> listBufferSectionPath = new List<Tuple<int, int>>();
            // store point start.
            int row_start = i;
            int col_start = j;

            while (i >= 0 && j < col)
            {
                if (boardSection[i, j].getPlayerHolder() == 0)
                {
                   
                    listBufferSectionPath.Add(new Tuple<int, int>(i, j));
                    
                    i--;
                    j++;
                }
                else
                {
                    if (boardSection[i, j].getPlayerHolder() != myPlayer) // found Enemy
                    {
                        if (i - 1 >= 0 && j + 1 < col) // check index next area have real.
                        {
                            if (boardSection[i - 1, j + 1].getPlayerHolder() == 0) // next area is empty. and can kill.
                            {
                                if (hasInListSectionItemKilled(i, j) == false)
                                {
                                    flag_Way_DoubleKill = true;
                                    foreach (Tuple<int, int> buffer in listBufferSectionPath)
                                    {
                                        boardSection[buffer.Item1, buffer.Item2].getObjectPanelSection().BackColor = COLOR_MARKED;
                                    }
                                    addSectionToListSectionItemKilled(i, j); // add section killed.
                                    if (ModeCanWalk) // can walk to section.
                                    {
                                        addSectionToListSectionSelected(i - 1, j + 1, i, j);
                                        boardSection[i - 1, j + 1].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                                        ModeCanWalk = false; // change to discovery mode , because do one time only.
                                    }
                                    else // not can walk now , just discoery.
                                    {
                                        addSectionToListSectionSelectedFuture(row_start, col_start, i - 1, j + 1);
                                        boardSection[i - 1, j + 1].getObjectPanelSection().BackColor = COLOR_PATH;
                                    }
                                    searchPathSuperItemDoubleKill(i - 1, j + 1, myPlayer,ModeCanWalk);
                                }
                            }
                        }
                    }
                    listBufferSectionPath.Clear();
                    break;  // found Section not empty. or has Item.
                }
            }
        }

        // search path Double Kill for Super Item
        private void searchDownLeftPathSuperItemDoubleKill(int i, int j, int myPlayer, bool ModeCanWalk)
        {
            List<Tuple<int, int>> listBufferSectionPath = new List<Tuple<int, int>>();
            // store point start.
            int row_start = i;
            int col_start = j;

            while (i < row  && j >= 0)
            {
                if (boardSection[i, j].hasItemInSection() == false)
                {
                    
                    listBufferSectionPath.Add(new Tuple<int, int>(i, j));
                    
                    i++;
                    j--;
                }
                else
                {
                    if (boardSection[i, j].getPlayerHolder() != myPlayer) // found Enemy
                    {
                        if (i + 1 < row && j - 1 >= 0 ) // check index next area have real.
                        {
                            if (boardSection[i + 1, j - 1].hasItemInSection() == false) // next area is empty. and can kill.
                            {
                                if (hasInListSectionItemKilled(i, j) == false)
                                {
                                    flag_Way_DoubleKill = true;
                                    foreach (Tuple<int, int> buffer in listBufferSectionPath)
                                    {
                                        boardSection[buffer.Item1, buffer.Item2].getObjectPanelSection().BackColor = COLOR_MARKED;
                                    }
                                    addSectionToListSectionItemKilled(i, j); // add section killed.
                                    if (ModeCanWalk) // can walk to section.
                                    {
                                        addSectionToListSectionSelected(i + 1, j - 1, i, j);
                                        boardSection[i + 1, j - 1].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                                        ModeCanWalk = false; // change to discovery mode , because do one time only.
                                    }
                                    else // not can walk now , just discoery.
                                    {
                                        addSectionToListSectionSelectedFuture(row_start, col_start, i + 1, j - 1);
                                        boardSection[i + 1, j - 1].getObjectPanelSection().BackColor = COLOR_PATH;
                                    }
                                    searchPathSuperItemDoubleKill(i + 1, j - 1, myPlayer,ModeCanWalk);
                                }
                            }
                        }
                    }
                    listBufferSectionPath.Clear();
                    break;  // found Section not empty. or has Item.
                }
            }
        }

        // search path Double Kill for Super Item
        private void searchDownRightPathSuperItemDoubleKill(int i, int j, int myPlayer, bool ModeCanWalk)
        {
            
            List<Tuple<int, int>> listBufferSectionPath = new List<Tuple<int, int>>();
            // store point start.
            int row_start = i;
            int col_start = j;

            while (i < row && j < col)
            {
                if (boardSection[i, j].hasItemInSection() == false)
                {                
                    listBufferSectionPath.Add(new Tuple<int, int>(i, j));
                    i++;
                    j++;
                }
                else
                {
                    
                    if (boardSection[i, j].getPlayerHolder() != myPlayer) // found Enemy
                    {
                        if (i + 1 < row && j + 1 < col) // check index next area have real.
                        {
                            if (boardSection[i + 1, j + 1].hasItemInSection() == false) // next area is empty. and can kill.
                            {
                               
                                if (hasInListSectionItemKilled(i, j) == false)
                                {
                                    
                                    flag_Way_DoubleKill = true;
                                    foreach (Tuple<int, int> buffer in listBufferSectionPath)
                                    {
                                        boardSection[buffer.Item1, buffer.Item2].getObjectPanelSection().BackColor = COLOR_MARKED;
                                    }
                                    addSectionToListSectionItemKilled(i, j); // add section killed.
                                    if (ModeCanWalk) // can walk to section.
                                    {
                                        addSectionToListSectionSelected(i + 1, j + 1, i, j);
                                        boardSection[i + 1, j + 1].getObjectPanelSection().BackColor = COLOR_CAN_WALK;
                                        ModeCanWalk = false; // change to discovery mode , because do one time only.
                                    }
                                    else // not can walk now , just discoery.
                                    {
                                        addSectionToListSectionSelectedFuture(row_start, col_start, i + 1, j + 1);
                                        boardSection[i + 1, j + 1].getObjectPanelSection().BackColor = COLOR_PATH;
                                    }
                                    searchPathSuperItemDoubleKill(i + 1, j + 1, myPlayer,ModeCanWalk);
                                }
                            }
                        }
                    }
                    listBufferSectionPath.Clear();
                    break;  // found Section not empty. or has Item.
                }
            }
        }

        public bool checkMatchInList(List<Tuple<int, int>> list, Tuple<int, int> tuple)
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

        private Item findItemActive(){
            foreach(Item item in itemPlayer2){
                if(item.getStatusItem()==1||item.getStatusItem()==2){
                    return item;
                }
            }
            return null;
        }

        // AI , player 1 is AI.
        public override bool callAI()
        {
            callAIForBoardHorse();
            return true;
        }

        private void searchAIRegularItemDoubleKill(int id_row,int id_col,int statusItem)
        {
            bool left = searchDownLeftAI(id_row, id_col, statusItem);
            if (left)
            {
                searchAIRegularItemDoubleKill(id_row + 1 + 1, id_col - 1 - 1, statusItem);
            }
            else
            {
                bool right = searchDownRightAI(id_row , id_col , statusItem);
                if (right)
                {
                    searchAIRegularItemDoubleKill(id_row + 1 + 1, id_col + 1 + 1, statusItem);
                }
            }           
        }

        private bool searchDownLeftAI(int id_row,int id_col,int statusItem)
        {
            if (id_row + 1 < row && id_col - 1 >= 0) // index has real.
            {
                if (boardSection[id_row + 1, id_col - 1].getPlayerHolder() == 2) // has kill.
                {
                    if (id_row + 1 + 1 < row && id_col - 1 - 1 >= 0) // index has real.
                    {
                        if (boardSection[id_row + 1 + 1, id_col - 1 - 1].getPlayerHolder() == 0) // next section empty. can walk. 
                        {
                            deleteItem(id_row + 1, id_col - 1);
                            moveItem(id_row, id_col, id_row + 1 + 1, id_col - 1 - 1);
                            searchAIRegularItemDoubleKill(id_row + 1 + 1, id_col - 1 - 1,statusItem);

                            if (id_row + 1 + 1 == row - 1 && statusItem == 1) // Goal.in area up.
                            {
                                chageToSuperItem(id_row +1 +1, id_col -1 -1);
                            }
                            return true;
                        }
                    }
                }
                else if (boardSection[id_row + 1, id_col - 1].getPlayerHolder() == 0) // next section empty. can walk. 
                {
                    moveItem(id_row, id_col, id_row + 1, id_col - 1);

                    if (id_row + 1 == row - 1 && statusItem == 1) // Goal.in area up.
                    {
                        chageToSuperItem(id_row + 1, id_col - 1);
                    }
                    return true;
                }

                
            }
            return false;
        }

        private bool searchDownRightAI(int id_row, int id_col,int statusItem)
        {
            if (id_row + 1 < row && id_col + 1 < col) // index has real.
            {
                if (boardSection[id_row + 1, id_col + 1].getPlayerHolder() == 2) // has kill.
                {
                    if (id_row + 1 + 1 < row && id_col + 1 + 1 < col) // index has real.
                    {
                        if (boardSection[id_row + 1 + 1, id_col + 1 + 1].getPlayerHolder() == 0) // next section empty. can walk. 
                        {
                            deleteItem(id_row + 1, id_col + 1);
                            moveItem(id_row, id_col, id_row + 1 + 1, id_col + 1 + 1);
                            searchAIRegularItemDoubleKill(id_row + 1 + 1, id_col + 1 + 1, statusItem);

                            if (id_row + 1 + 1 == row - 1 && statusItem == 1) // Goal.in area up.
                            {
                                chageToSuperItem(id_row + 1 + 1, id_col + 1 + 1);
                            }
                            return true;
                        }
                    }
                }
                else if (boardSection[id_row + 1, id_col + 1].getPlayerHolder() == 0) // next section empty. can walk. 
                {
                    moveItem(id_row, id_col, id_row + 1, id_col + 1);
                    if (id_row + 1 == row - 1 && statusItem == 1) // Goal.in area up.
                    {
                        chageToSuperItem(id_row + 1, id_col + 1);
                    }
                    return true;
                }
            }
            return false;
        }

        public bool isRangeIndex(int id_row, int id_col)
        {
            if (id_row >= 0 && id_row < row && id_col >= 0 && id_col < col)
            {
                return true;
            }
            return false;
        }

        // about forced kill regular item have 4 method.
        public bool checkPlayer1CanKillRegularLeft(int id_row,int id_col){
            if (isRangeIndex(id_row + 1, id_col - 1))
            {
                if (boardSection[id_row + 1, id_col - 1].getPlayerHolder() == 2) // found item enemy.
                {
                    if (isRangeIndex(id_row + 1 + 1, id_col - 1 - 1))
                    {
                        if (boardSection[id_row + 1 + 1, id_col - 1 - 1].getPlayerHolder() <= 0) // check can kill
                        {
                            listSectionItemKillerFuture.Add(new Tuple<int, int>(id_row + 1 + 1, id_col - 1 - 1));
                            listSectionItemKiller.Add(new Tuple<int, int>(id_row, id_col));
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool checkPlayer1CanKillRegularRight(int id_row, int id_col)
        {
            if (isRangeIndex(id_row + 1, id_col + 1))
            {
                if (boardSection[id_row + 1, id_col + 1].getPlayerHolder() == 2) // found item enemy.
                {
                    if (isRangeIndex(id_row + 1 + 1, id_col + 1 + 1))
                    {
                        if (boardSection[id_row + 1 + 1, id_col + 1 + 1].getPlayerHolder() <= 0) // check can kill
                        {
                            listSectionItemKillerFuture.Add(new Tuple<int, int>(id_row + 1 + 1, id_col + 1 + 1));
                            listSectionItemKiller.Add(new Tuple<int, int>(id_row, id_col));
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool checkPlayer2CanKillRegularLeft(int id_row, int id_col)
        {
            if (isRangeIndex(id_row - 1, id_col - 1))
            {
                if (boardSection[id_row - 1, id_col - 1].getPlayerHolder() == 1) // found item enemy.
                {
                    if (isRangeIndex(id_row - 1 - 1, id_col - 1 - 1))
                    {
                        if (boardSection[id_row - 1 - 1, id_col - 1 - 1].getPlayerHolder() <= 0) // check can kill
                        {
                            listSectionItemKillerFuture.Add(new Tuple<int, int>(id_row - 1 - 1, id_col - 1 - 1));
                            listSectionItemKiller.Add(new Tuple<int, int>(id_row, id_col));
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool checkPlayer2CanKillRegularRight(int id_row, int id_col)
        {
            if (isRangeIndex(id_row - 1, id_col + 1))
            {
                if (boardSection[id_row - 1, id_col + 1].getPlayerHolder() == 1) // found item enemy.
                {
                    if (isRangeIndex(id_row - 1 - 1, id_col + 1 + 1))
                    {
                        if (boardSection[id_row - 1 - 1, id_col + 1 + 1].getPlayerHolder() <= 0) // check can kill
                        {
                            listSectionItemKillerFuture.Add(new Tuple<int, int>(id_row - 1 - 1, id_col + 1 + 1));
                            listSectionItemKiller.Add(new Tuple<int, int>(id_row, id_col));
                            return true;
                        }
                    }
                }
            }
            return false;
        }


        // about forced kill Super item. have 1 method main and 4 sub method.
        public void checkCanKillSuper(int id_row, int id_col,int player)
        {
            checkCanKillTopLeftSuperItem(id_row, id_col, player);
            checkCanKillTopRightSuperItem(id_row, id_col, player);
            checkCanKillDownLeftSuperItem(id_row, id_col, player);
            checkCanKillDownRightSuperItem(id_row, id_col, player);
        }

        private void checkCanKillTopLeftSuperItem(int id_row, int id_col, int player)
        {
            //int row_start = i;
            //int col_start = j;
            int i = id_row -1;
            int j = id_col -1;

            while (isRangeIndex(i, j))
            {
                if (boardSection[i, j].hasItemInSection() == false)
                {
                    i--;
                    j--;
                }
                else // found item.
                {
                    if (boardSection[i, j].getPlayerHolder() != player) // found Enemy
                    {
                        if (isRangeIndex(i - 1, j - 1)) // check index next area have real.
                        {
                            if (boardSection[i - 1, j - 1].hasItemInSection() == false) // next area is empty. and can kill.
                            {
                                listSectionItemKiller.Add(new Tuple<int, int>(id_row, id_col));
                                listSectionItemKillerFuture.Add(new Tuple<int, int>(i - 1, j - 1));
                            }
                        }
                    }
                    break;  // found Section not empty. or has Item.
                }
            }
        }

        private void checkCanKillTopRightSuperItem(int id_row, int id_col, int player)
        {
            //int row_start = i;
            //int col_start = j;
            int i = id_row - 1;
            int j = id_col + 1;

            while (isRangeIndex(i, j))
            {
                if (boardSection[i, j].hasItemInSection() == false)
                {
                    i--;
                    j++;
                }
                else // found item.
                {
                    if (boardSection[i, j].getPlayerHolder() != player) // found Enemy
                    {
                        if (isRangeIndex(i - 1, j + 1)) // check index next area have real.
                        {
                            if (boardSection[i - 1, j + 1].hasItemInSection() == false) // next area is empty. and can kill.
                            {
                                listSectionItemKiller.Add(new Tuple<int, int>(id_row, id_col));
                                listSectionItemKillerFuture.Add(new Tuple<int, int>(i - 1, j + 1));
                            }
                        }
                    }
                    break;  // found Section not empty. or has Item.
                }
            }
        }

        private void checkCanKillDownLeftSuperItem(int id_row, int id_col, int player)
        {
            //int row_start = i;
            //int col_start = j;
            int i = id_row + 1;
            int j = id_col - 1;

            while (isRangeIndex(i, j))
            {
                if (boardSection[i, j].hasItemInSection() == false)
                {
                    i++;
                    j--;
                }
                else // found item.
                {
                    if (boardSection[i, j].getPlayerHolder() != player) // found Enemy
                    {
                        if (isRangeIndex(i + 1, j - 1)) // check index next area have real.
                        {
                            if (boardSection[i + 1, j - 1].hasItemInSection() == false) // next area is empty. and can kill.
                            {
                                listSectionItemKiller.Add(new Tuple<int, int>(id_row, id_col));
                                listSectionItemKillerFuture.Add(new Tuple<int, int>(i + 1, j - 1));
                            }
                        }
                    }
                    break;  // found Section not empty. or has Item.
                }
            }
        }

        private void checkCanKillDownRightSuperItem(int id_row, int id_col, int player)
        {
            //int row_start = i;
            //int col_start = j;
            int i = id_row + 1;
            int j = id_col + 1;

            while (isRangeIndex(i, j))
            {
                if (boardSection[i, j].hasItemInSection() == false)
                {
                    i++;
                    j++;
                }
                else // found item.
                {
                    if (boardSection[i, j].getPlayerHolder() != player) // found Enemy
                    {
                        if (isRangeIndex(i + 1, j + 1)) // check index next area have real.
                        {
                            if (boardSection[i + 1, j + 1].hasItemInSection() == false) // next area is empty. and can kill.
                            {
                                listSectionItemKiller.Add(new Tuple<int, int>(id_row, id_col));
                                listSectionItemKillerFuture.Add(new Tuple<int, int>(i + 1, j + 1));
                            }
                        }
                    }
                    break;  // found Section not empty. or has Item.
                }
            }
        }

        public void searchForcedKill(int player, Item[] item)
        {
            List<Tuple<int, int>> listCanWalk = new List<Tuple<int, int>>();
            foreach (Item myitem in item)
            {
                if (myitem != null)
                {
                    int id_row = myitem.getIdRow();
                    int id_col = myitem.getIdCol();
                    //Console.Write("\n item = " + id_row + "," + id_col);
                    if (isRangeIndex(id_row, id_col))
                    {
                        if (myitem.getStatusItem() == 1 && player ==1)  // regular item for player1.
                        {
                            checkPlayer1CanKillRegularLeft(id_row, id_col);
                            checkPlayer1CanKillRegularRight(id_row, id_col);
                        }
                        else if (myitem.getStatusItem() == 1 && player == 2) // regular item for player2.
                        {
                            checkPlayer2CanKillRegularLeft(id_row, id_col);
                            checkPlayer2CanKillRegularRight(id_row, id_col);
                        }
                        else if(myitem.getStatusItem() == 2 ) //Horse item , player1 and 2 is same .
                        {
                            checkCanKillSuper(id_row, id_col, player);
                        }
                    }
                }
            }
        }

        public void callAIForBoardHorse()
        {
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
                    }
                    tableBoardSectionForAI[i, j] = new AIBoradSection(i, j, player, status);
                }
            }

            // Create Object AI , assign data and table to AI.
            AIBoardHorse nicky = new AIBoardHorse(row,col,tableBoardSectionForAI,itemPlayer1,itemPlayer2);
            Console.Write("Start AI : ");
            Console.Write("\n\n");
            //nicky.setMaxStep(3);
            int point =nicky.run(0);
            //MessageBox.Show(""+point);

            int id_will_row_des_super=0;
            int id_will_col_des_super=0;

            List<Tuple<int, int, int, int, int, int>> listBestAnswer = nicky.getListBestAnswer();
            if (listBestAnswer != null)
            {
                Console.Write("\nCount listanswer = " + listBestAnswer.Count + "\n");
                //Tuple<int, int, int, int, int, int> buffer = listBestAnswer.First();


                List<Tuple<int, int, int, int, int, int>> listDouble = nicky.getListBestAnswerDoubleKill();
                Console.Write("\nCount listdouble = " + listDouble.Count + "\n");
                listBestAnswer.AddRange(listDouble);
                    
                foreach (Tuple<int, int, int, int, int, int> buffer in listBestAnswer)
                {
                //Tuple<int, int, int, int, int, int> buffer = listBestAnswer.First();
                    int id_row_source = buffer.Item1;
                    int id_col_source = buffer.Item2;
                    int id_row_des    = buffer.Item3;
                    int id_col_des    = buffer.Item4;
                    int id_row_killed = buffer.Item5;
                    int id_col_killed = buffer.Item6;

                    id_will_row_des_super = id_row_des;
                    id_will_col_des_super = id_col_des;
                    Console.Write("\n " + id_row_source +"," + id_col_source +
                                  "  " + id_row_des +"," + id_col_des +
                                  "  " + id_row_killed +"," + id_col_killed
                    );
                    updateDataDeBugging();
                    moveItem(id_row_source,id_col_source,id_row_des,id_col_des);
                    if (isRangeIndex(id_row_killed, id_col_killed))
                    {
                        deleteItem(id_row_killed, id_col_killed);
                    }

                    int status = ((Item)(boardSection[id_row_des, id_col_des].getItemFromPanelSection())).getStatusItem();
                    if (id_row_des == row-1 && status==1)
                    {
                        chageToSuperItem(id_row_des, id_col_des);
                    }
                }
                listBestAnswer.Clear();


                if (nicky.getFlagHaveKillFromSuperItem())
                {
                    //Thread.Sleep(1000);
                    callAIForSuperDoubleKill(id_will_row_des_super, id_will_col_des_super, nicky.getBestIndexItemSuper());
                }
            }
        }

        public void callAIForSuperDoubleKill(int id_row,int id_col,int indexOldItemSuper)
        {
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
                    }
                    tableBoardSectionForAI[i, j] = new AIBoradSection(i, j, player, status);
                }
            }

            // Create Object AI , assign data and table to AI.
            AIBoardHorse nicky = new AIBoardHorse(row, col, tableBoardSectionForAI, itemPlayer1, itemPlayer2);

            nicky.runDoubleKillSuperItemMode(id_row, id_col, indexOldItemSuper);
            List<Tuple<int, int, int, int, int, int>> listBestAnswer = nicky.getListbestAnswerSuperItemDoubleKill();

            int id_will_row_des_super = 0;
            int id_will_col_des_super = 0;

            
            if (listBestAnswer != null)
            {
                Console.Write("\nCount listanswer = " + listBestAnswer.Count + "\n");
                //Tuple<int, int, int, int, int, int> buffer = listBestAnswer.First();

                foreach (Tuple<int, int, int, int, int, int> buffer in listBestAnswer)
                {
                    //Tuple<int, int, int, int, int, int> buffer = listBestAnswer.First();
                    int id_row_source = buffer.Item1;
                    int id_col_source = buffer.Item2;
                    int id_row_des = buffer.Item3;
                    int id_col_des = buffer.Item4;
                    int id_row_killed = buffer.Item5;
                    int id_col_killed = buffer.Item6;

                    id_will_row_des_super = id_row_des;
                    id_will_col_des_super = id_col_des;

                    Console.Write("\n " + id_row_source + "," + id_col_source +
                                  "  " + id_row_des + "," + id_col_des +
                                  "  " + id_row_killed + "," + id_col_killed
                       );
                    updateDataDeBugging();
                    moveItem(id_row_source, id_col_source, id_row_des, id_col_des);


                    if (isRangeIndex(id_row_killed, id_col_killed))
                    {
                        deleteItem(id_row_killed, id_col_killed);
                    }

                }
                listBestAnswer.Clear();

                if (nicky.getFlagHaveKillFromSuperItem())
                {
                    //Thread.Sleep(1000);
                    callAIForSuperDoubleKill(id_will_row_des_super, id_will_col_des_super, nicky.getBestIndexItemSuper());
                }
            }

          
        }

    }

}
