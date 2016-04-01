using DraughtMaster.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;  // Use Panel libraly.
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DraughtMaster
{
    public enum type :int { Draught =0, PinchDraught=1 };
    
    public class Board
    {
        protected MyBoardGame myBoardGame;

        protected bool tutorial = false;
        protected Button btnNextChapter;
        protected string goalTutorial = "";

        //public enum type { Draught, PinchDraught, TrainDraught };
        protected  static BoardSection[,] boardSection;
        protected int typeBoard;
        protected int row, col; // width and height of board.

        // list for section when selected from event click. by argument is id_row,id_col,id_row killed,id_col killed
        protected static List<Tuple<int,int,int,int>> listSectionSelected;
        // list for section when selected and Marked from event click. 
        //by argument is id_row,id_col,id_row marked,id_col marked.
        protected static List<Tuple<int, int, int, int>> listSectionSelectedFuture;

        // list for store section will killed. use in super item double kill.
        protected static List<Tuple<int, int>> listSectionItemKilled;

        // list for store section will killed. use in super item double kill.
        protected static List<int> listItemActiveAI;

        protected static List<Tuple<int, int>> listSectionItemKiller;
        protected static List<Tuple<int, int>> listSectionItemKillerFuture;

        // for store id_row, id_col last section selected. by -1 is empty.
        protected int temp_row;
        protected int temp_col;

       

        protected static bool turnPlayer1 = false;
        protected static bool turnPlayer2 = true;

        // store number of item active start , and counter item active.
        protected int NUM_ITEM_PLAYER1 = 0;
        protected int NUM_ITEM_PLAYER2 = 0;
        protected int count_item_player1_active = 0;
        protected int count_item_player2_active = 0;

        protected Form myForm;
        protected TextBox txt_playerHolder; // for deBuging.
        protected TextBox txt_addressItem; // for deBuging.
        protected TextBox txt_addressItem2; // for deBuging. (player 2)
        protected TextBox txt_itemInBoard; // for deBuging.

        protected Label lblTurnPlayer;

        protected Label lbl_item_active_player1; //
        protected Label lbl_item_active_player2;

        protected Label lbl_NumItemPlayer1;
        protected Label lbl_NumItemPlayer2;

        // About Skip label.
        protected Label lbl_NumSkipPlayer1;
        protected Label lbl_NumSkipPlayer2;

        protected Label lbl_ActiveSkipPlayer1;
        protected Label lbl_ActiveSkipPlayer2;

        protected ProgressBar progressBar_player1;
        protected ProgressBar progressBar_player2;

        protected Button btn_skipturn;

        protected Item[] itemPlayer1;
        protected Item[] itemPlayer2;

        public Color COLOR_PANEL_A = Color.FromArgb(255, 192, 192);
        public Color COLOR_PANEL_B = Color.Gray;
        public Color COLOR_CAN_WALK = Color.Red;
        public Color COLOR_MARKED= Color.Orange;
        public Color COLOR_PATH = Color.Brown;

        protected static bool flag_force_kill = false;
        protected static bool flag_AI = false;
        protected static bool flag_changeTurn = false;

        protected bool state_forced = false;

        // Constructor.
        public Board(){

        }

        public Board(Form myForm,Panel panelBoard,int row,int col,int typeBoard) 
        {
            this.myForm = myForm;
            boardSection = new BoardSection[row, col]; // assign size board.
            listSectionSelected = new List<Tuple<int, int, int, int>>();
            this.setRowColBoard(row, col);
            createBoardSection(); // create Boardsection.
            accessContent(panelBoard); // access object and assign Panel to Array PanelSection.
            createPictureBoxInBoardSection(row,col);
            this.typeBoard = typeBoard; // assign type of board.
        }

        protected void setObjectMyBoardGame(MyBoardGame myBoardGame)
        {
            this.myBoardGame = myBoardGame;
        }

        public int getCountListSectionSelected()
        {
            return listSectionSelected.Count;
        }

        public void setFlagTutorial(bool flag)
        {
            tutorial = flag;
        }

        public void setColorPanelBoardSection(Color A,Color B)
        {
            COLOR_PANEL_A = A;
            COLOR_PANEL_B = B;
        }


        // set value Row and Col for Board.
        public void setRowColBoard(int row ,int col){
            this.row = row;
            this.col = col;
        }

        public void swapTurnPlayer()
        {
            bool temp = turnPlayer1;
            turnPlayer1 = turnPlayer2;
            turnPlayer2 = temp;
        }

        public void createBoardSection()
        {
            int size =getSizePicture(row, col);
            // Create Boardsection each section and assignment velue in section.
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    boardSection[i, j] = new BoardSection(i,j,typeBoard,this,myBoardGame);

                    // for manage graphic appropiate with differ size board. must know size board.
                    boardSection[i, j].setSizePictureItem(size);
                }
            }
        }

        private int getSizePicture(int row, int col)
        {
            if (row == 12 && col == 12)
            {
                return 44;
            }
            else if (row == 8 && col == 8)
            {
                return 64;
            }
            else
            {
                return 64;
            }
        }

        // access content in form and put into array.
        public void accessContent(Panel myBoard) // parameter is Panel Parent.
        {
            int i = 0, j = 0;
            while (i<row)
            {
                // Fetch Panel children in Parent Panel store to Array.
                object obj = myBoard.Controls.Find("board_" + i + "_" + j, false).FirstOrDefault();
                if (obj != null)
                {
                    boardSection[i, j].setPanelSection((Panel)obj);
                }
                j++;
                if (j >= col)
                {
                    i++;
                    j = 0;
                }
            }
        }

        public BoardSection[,] getBoardSection()
        {
            return boardSection;
        }

        // get object BoardPanel from id.
        public BoardSection getBoardSection(int id_row, int id_col)
        {
            return boardSection[id_row, id_col];
        }

        // get Panel in Object Object PanelSection from id.
        public Panel getPanelSection(int id_row, int id_col)
        {
            return boardSection[id_row, id_col].getObjectPanelSection();
        }

        // Create PictureBox in boardSection. by every section have Picturebox.
        // Why write here? or not write in constructor BoardSection?
        // because if write in that constructor it can't work by not access its Panel.
        // and working must first is access Panel.
        public void createPictureBoxInBoardSection(int row, int col)
        {
            for(int i=0 ; i<row ; i++){
                for(int j=0 ; j<col ; j++){
                    boardSection[i,j].createPictureBoxInPanelSection();
                }
            }
        }

        // Add Player to PanelSection and assign player.
        public void setItemToPanelSection(int id_row,int id_col,int player,object item)
        {
            boardSection[id_row, id_col].setItemToPanelSection(player,item);
        }

        // Refresh Color on board to Color default.
        public void refreshColorOnBoard()
        {
            bool flagColor = true;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if(flagColor){
                        boardSection[i,j].getObjectPanelSection().BackColor = COLOR_PANEL_A;
                    }
                    else{
                        boardSection[i, j].getObjectPanelSection().BackColor = COLOR_PANEL_B;
                    }
                    flagColor = !flagColor; 
                }
                flagColor = !flagColor; 
            }
        }

        // refresh item, use after setting new item.
        public void refreshItemPictureOnBoard()
        {
            foreach(Item item in itemPlayer1){
                if (item != null)
                {
                    int id_row = item.getIdRow();
                    int id_col = item.getIdCol();
                    int status = item.getStatusItem();

                    if (id_row >= 0 && id_col >= 0)
                    {
                        if (status == 1)
                        {
                            boardSection[id_row, id_col].setImageRegularItemToPictureBoxSection(1);
                        }
                        else if (status == 2)
                        {
                            boardSection[id_row, id_col].setImageSuperItemToPictureBoxSection(1);
                        }
                    }

                }
            }

            foreach (Item item in itemPlayer2)
            {
                if (item != null)
                {
                    int id_row = item.getIdRow();
                    int id_col = item.getIdCol();
                    int status = item.getStatusItem();

                    if (id_row >= 0 && id_col >= 0)
                    {
                        if (status == 1)
                        {
                            boardSection[id_row, id_col].setImageRegularItemToPictureBoxSection(2);
                        }
                        else if (status == 2)
                        {
                            boardSection[id_row, id_col].setImageSuperItemToPictureBoxSection(2);
                        }
                    }

                }
            }
        }

        // Clear listSectionSelected in list.
        public void clearListSectionSelected()
        {
            listSectionSelected.Clear();
        }

        // add item id_row,id_col to listSectionSelected.
        public void addSectionToListSectionSelected(int id_row,int id_col,int id_row_killed,int id_col_killed)
        {
            listSectionSelected.Add(new Tuple<int, int,int ,int>(id_row, id_col,id_row_killed,id_col_killed));
        }

        // add item id_row,id_col to listSectionSelected.
        public void addSectionToListSectionSelectedFuture(int id_row_source, int id_col_source, int id_row_destination, int id_col_destination)
        {
            listSectionSelectedFuture.Add(new Tuple<int, int, int, int>(id_row_source, id_col_source, id_row_destination, id_col_destination));
        }

        // add item id_row,id_col to listSectionSelected.
        public void addSectionToListSectionItemKilled(int id_row_killed, int id_col_killed)
        {
            listSectionItemKilled.Add(new Tuple<int, int>(id_row_killed, id_col_killed));
        }

        // remove some record by id_row_source, id_col_source.
        public void removeItemInListSectionSelectedFuture(int id_row_source,int id_col_source)
        {
            listSectionSelectedFuture.RemoveAll(p => p.Item1 == id_row_source && p.Item2 == id_col_source);
        }

        // Clear 
        public void clearListSectionSelectedFuture()
        {
            listSectionSelectedFuture.Clear();
        }

        // Have id_row and col equal in listSectionSelected ? 
        public bool hasInListSectionSelected(int id_row,int id_col)
        {
            foreach (var lst in listSectionSelected)
            {        
                if (lst.Item1.Equals(id_row) && lst.Item2.Equals(id_col)){
                    return true;
                }

            }
            return false;
        }

        public bool hasInListSectionSelectedFuture(int id_row_start,int id_col_start)
        {
            foreach (var lst in listSectionSelectedFuture)
            {
                if (lst.Item1.Equals(id_row_start) && lst.Item2.Equals(id_col_start))
                {
                    return true;
                }

            }
            return false;
        }

        // overload.
        public bool hasInListSectionSelectedFuture(int id_row_start, int id_col_start, int id_row_dest, int id_col_dest)
        {
            foreach (var lst in listSectionSelectedFuture)
            {
                if (lst.Item1.Equals(id_row_start) && lst.Item2.Equals(id_col_start) && lst.Item3.Equals(id_row_dest) && lst.Item4.Equals(id_col_dest))
                {
                    return true;
                }

            }
            return false;
        }

        // Have id_row and col equal in listSectionItemKilled ? 
        public bool hasInListSectionItemKilled(int id_row,int id_col)
        {
            foreach (var lst in listSectionItemKilled)
            {        
                if (lst.Item1.Equals(id_row) && lst.Item2.Equals(id_col)){
                    return true;
                }

            }
            return false;
        }

        public Tuple<int,int> getIdSectionKilledListSectionSelected(int id_row, int id_col)
        {
            foreach (var lst in listSectionSelected)
            {
                if (lst.Item1.Equals(id_row) && lst.Item2.Equals(id_col))
                {
                    if (lst.Item3 != -1 && lst.Item4 != -1)  // have killing. not regular walk.
                    {
                        return new Tuple<int, int>(lst.Item3, lst.Item4);
                    }
                    else
                    {
                        return null;
                    }
                    
                }

            }
            return null;
        }

        public void setGoalTutorial(string goalTutorial)
        {
            this.goalTutorial = goalTutorial;
        }

        public void setButtonTutorialNextChapter(Button btnNextChapter)
        {
            this.btnNextChapter = btnNextChapter;
        }

        public void setButtonSkipturn(Button btn_skipturn){
            this.btn_skipturn = btn_skipturn; 
        }

        // set temp row,col.
        public void setTempRowCol(int id_row,int id_col)
        {
            temp_row = id_row;
            temp_col = id_col;
        }

        // get temp row.
        public int getTempRow()
        {
            return temp_row;
        }

        // get temp col.
        public int getTempCol()
        {
            return temp_col;
        }

        public void setLabelTurnPlayer()
        {
            if (tutorial == false)
            {
                if (turnPlayer1)
                {
                    lblTurnPlayer.Text = "player 1 ";
                }
                else if (turnPlayer2)
                {
                    lblTurnPlayer.Text = "player 2 ";
                }
            }
        }

        public void setFlagForcedKill(bool flag)
        {
            flag_force_kill = flag;
        }

        public bool getFlagForcedKill()
        {
            return flag_force_kill;
        }

        // set access label.
        public void setLabelCounterItemActiveContent(Label lbl_Activecounter_player1, Label lbl_Activecounter_player2)
        {
            if (tutorial == false)
            {
                lbl_item_active_player1 = lbl_Activecounter_player1;
                lbl_item_active_player2 = lbl_Activecounter_player2;
            }
        }

        public void setLabelNumItem(Label lbl_NumItemPlayer1, Label lbl_NumItemPlayer2)
        {
            if (tutorial == false)
            {
                this.lbl_NumItemPlayer1 = lbl_NumItemPlayer1;
                this.lbl_NumItemPlayer2 = lbl_NumItemPlayer2;
            }
        }


        public void setLabelNumSkip(Label lbl_NumSkipPlayer1, Label lbl_NumSkipPlayer2)
        {
            if (tutorial == false)
            {
                this.lbl_NumSkipPlayer1 = lbl_NumSkipPlayer1;
                this.lbl_NumSkipPlayer2 = lbl_NumSkipPlayer2;
            }
        }

        public void setLabelActiveSkip(Label lbl_ActiveSkipPlayer1, Label lbl_ActiveSkipPlayer2)
        {
            if (tutorial == false)
            {
                this.lbl_ActiveSkipPlayer1 = lbl_ActiveSkipPlayer1;
                this.lbl_ActiveSkipPlayer2 = lbl_ActiveSkipPlayer2;
            }
        }

        public void setProgressBarPlayer(ProgressBar progrBar_player1, ProgressBar progrBar_player2)
        {
            if (tutorial == false)
            {
                progressBar_player1 = progrBar_player1;
                progressBar_player1.Value = 100;

                progressBar_player2 = progrBar_player2;
                progressBar_player2.Value = 100;
            }
        }

        public void calculateProgressBarPlayer()
        {
            //casting type int to float for value in progressbar.
            float active_item1 = (float)count_item_player1_active;
            float active_item2 = (float)count_item_player2_active;

            if (progressBar_player1 != null && progressBar_player2 != null)
            {
                progressBar_player1.Value = (int)((active_item1 / NUM_ITEM_PLAYER1) * 100);
                progressBar_player2.Value = (int)((active_item2 / NUM_ITEM_PLAYER2) * 100);
            }
        }

        public void setTurnPlayerStart(int player)
        {
            if (player == 1)
            {
                if (lblTurnPlayer != null)
                {
                    lblTurnPlayer.Text = "player 1";
                }

                turnPlayer1 = true;
                turnPlayer2 = !turnPlayer1;
            }
            else if (player == 2)
            {
                if (lblTurnPlayer != null)
                {
                    lblTurnPlayer.Text = "player 2";
                }
                turnPlayer2 = true;
                turnPlayer1 = !turnPlayer2;
            }
        }

        public void setNumberItem()
        {
            if (lbl_NumItemPlayer1 != null && lbl_NumItemPlayer2 != null)
            {
                lbl_NumItemPlayer1.Text = "/ " + NUM_ITEM_PLAYER1;
                lbl_NumItemPlayer2.Text = "/ " + NUM_ITEM_PLAYER2;
            }
        }

        // set counter and number of item active player.
        public void setCounterItemPlayer()
        {
            // count number of item active player1.
            foreach (Item item in itemPlayer1)
            {
                if (item != null)
                {
                    if (item.getStatusItem() == 1 || item.getStatusItem() == 2)
                    {
                        count_item_player1_active++;
                    }
                }
            }
            NUM_ITEM_PLAYER1 = count_item_player1_active;

            // count number of item active player1.
            foreach (Item item in itemPlayer2)
            {
                if (item != null)
                {
                    if (item.getStatusItem() == 1 || item.getStatusItem() == 2)
                    {
                        count_item_player2_active++;
                    }
                }
            }
            NUM_ITEM_PLAYER2 = count_item_player2_active;
            setNumberItem(); // NUM1 NUM2 set to Label.
        }

        public void setListItemActiveAI()
        {
            for (int i = 0; i < itemPlayer1.Length; i++)
            {
                if (itemPlayer1[i] != null)
                {
                    if (itemPlayer1[i].getStatusItem() == 1 || itemPlayer1[i].getStatusItem() == 2)
                    {
                        listItemActiveAI.Add(i);
                    }
                }
            }
        }


        // Update label display number of item active.
        public void updateCounterActiveItem()
        {
            if (lbl_item_active_player1 != null && lbl_item_active_player2 != null)
            {
                lbl_item_active_player1.Text = "" + count_item_player1_active;
                lbl_item_active_player2.Text = "" + count_item_player2_active;
            }

            if (lbl_ActiveSkipPlayer1 != null && lbl_ActiveSkipPlayer2 != null)
            {
                lbl_ActiveSkipPlayer1.Text = "" + myBoardGame.getSkipTurn(1);
                lbl_ActiveSkipPlayer2.Text = "" + myBoardGame.getSkipTurn(2);
            }

            if (lbl_NumSkipPlayer1 != null && lbl_NumSkipPlayer2 != null)
            {
                lbl_NumSkipPlayer1.Text = "/ " + myBoardGame.getNumSkipTurn();
                lbl_NumSkipPlayer2.Text = "/ " + myBoardGame.getNumSkipTurn();
            }

            if (tutorial == false)
            {
                calculateProgressBarPlayer();
                checkButtonSkip();
            }
        }

        private void checkButtonSkip()
        {
            
            if (turnPlayer1 && myBoardGame.getSkipTurn(1) == 0)
            {
                btn_skipturn.Enabled = false;
            }
            else if (turnPlayer2 && myBoardGame.getSkipTurn(2) == 0)
            {
                btn_skipturn.Enabled = false;
            }
            else
            {
                btn_skipturn.Enabled = true;
            }
        }

        // set access , TextBox for debugging.
        public void setTextBoxContentForDeBugging(TextBox addressItem, TextBox addressItem2, TextBox playerHolder, TextBox itemInBoard)
        {
            txt_addressItem = addressItem;
            txt_addressItem2 = addressItem2;
            txt_playerHolder = playerHolder;
            txt_itemInBoard = itemInBoard;
        }

        public void updateDataDeBugging()
        {
            if (txt_addressItem == null || txt_addressItem2 == null || txt_playerHolder == null || txt_itemInBoard == null)
            {
                return;
            }

            // Update address Item. 
            string str1 = "Player1\r\n";
            int i, j, k = 0;

            foreach (Item item in itemPlayer1)
            {
                if (item != null)
                {
                    str1 = str1 + "Item " + k + " = " + item.getIdRow() + " , " + item.getIdCol() + "\r\n";
                    k++;
                }
            }

            txt_addressItem.Text = "" + str1;

            // Update address Item player 2
            str1 = "Player2\r\n";
            k = 0;
            foreach (Item item in itemPlayer2)
            {
                if (item != null)
                {
                    str1 = str1 + "Item " + k + " = " + item.getIdRow() + " , " + item.getIdCol() + "\r\n";
                    k++;
                }
            }
            txt_addressItem2.Text = "" + str1;


            // Update Player Holder
            string str2 = "";
            k = 0;
            for (i = 0; i < row; i++)
            {
                for (j = 0; j < col; j++)
                {
                    str2 = str2 + " " + boardSection[i, j].getPlayerHolder();
                }
                str2 = str2 + "\r\n";
            }
            txt_playerHolder.Text = "" + str2;

            // Update Item In Board
            string str3 = "x is have item and  - is null.\r\n";
            for (i = 0; i < row; i++)
            {
                for (j = 0; j < col; j++)
                {
                    if (boardSection[i, j].getItemFromPanelSection() != null)
                    {
                        str3 = str3 + "X ";
                    }
                    else
                    {
                        str3 = str3 + "-  ";
                    }

                }
                str3 = str3 + "\r\n";
            }
            txt_itemInBoard.Text = "" + str3;
        }

        public void setLabelTurnPlayerContent(Label lbl_turnPlayer)
        {
            lblTurnPlayer = lbl_turnPlayer;
        }

        public void createObjectItem(int numItemPlayer1, int numItemPlayer2)
        {
            itemPlayer1 = new Item[numItemPlayer1];
            itemPlayer2 = new Item[numItemPlayer2];
        }


        public bool createItem(int index, int player, int status)
        {
            if (player == 1)
            {
                if (itemPlayer1[index] == null)
                {
                    itemPlayer1[index] = new Item(player, status);
                    return true;
                }

            }
            else if (player == 2)
            {
                if (itemPlayer2[index] == null)
                {
                    itemPlayer2[index] = new Item(player, status);
                    return true;
                }
            }
            return false;
        }

        public Item getItemObject(int index, int player)
        {
            if (player == 1)
            {
                return itemPlayer1[index];
            }
            else if (player == 2)
            {
                return itemPlayer2[index];
            }
            return null;
        }

        public void skipTurn()
        {
            if (turnPlayer1 == true)
            {
                if (myBoardGame.getSkipTurn(1) > 0)
                {
                    myBoardGame.decreseSkipTurn(1);
                    swapTurnPlayer();
                    setLabelTurnPlayer();
                    updateCounterActiveItem();
                    MessageBox.Show("Swap Turn to player2");
                }
                else
                {
                    MessageBox.Show("Not.");
                }
            }
            else
            {
                if (myBoardGame.getSkipTurn(2) > 0)
                {
                    myBoardGame.decreseSkipTurn(2);
                    swapTurnPlayer();
                    setLabelTurnPlayer();
                    updateCounterActiveItem();
                    MessageBox.Show("Swap Turn to player1");
                }
                else
                {
                    MessageBox.Show("Not.");
                }
            }

            listSectionItemKiller.Clear();
            listSectionItemKillerFuture.Clear();
            state_forced = false;
            updateCounterActiveItem();
            //updateDataDeBugging();
        }

        public virtual bool callAI()
        {
            return true;
        }

        protected void checkGameover()
        {
            if (tutorial == true)
            {
                if (turnPlayer1 == true)
                {
                    btnNextChapter.BackColor = Color.Tomato;
                    btnNextChapter.Enabled = true;
                }
                return;
            }


            string msg = "";
            if (count_item_player1_active == 0)
            {
                if (flag_AI==true) // AI lose
                {
                    //Form parentForm,MyBoardGame myBoardGame,string result)
                    msg = "You win !!";
                }
                else // player 1 lose.
                {
                    msg = "Player 2 Win.";
                }
                Form currentForm = Application.OpenForms[Application.OpenForms.Count - 1];
                FormEndGame form = new FormEndGame(currentForm, myBoardGame, msg);
                form.Show();
                form.Location = new Point(currentForm.Location.X + (currentForm.Width / 2) - (form.Size.Width / 2), currentForm.Location.Y + (currentForm.Height / 2) - (form.Size.Height / 2));
            }
            else if (count_item_player2_active == 0)
            {
                if (flag_AI==true) // AI win.
                {
                    //Form parentForm,MyBoardGame myBoardGame,string result)
                    msg = "You lose ):";
                }
                else // player 2 lose.
                {
                    msg = "Player 1 Win.";
                }
                Form currentForm = Application.OpenForms[Application.OpenForms.Count - 1];
                FormEndGame form = new FormEndGame(currentForm, myBoardGame, msg);
                form.Show();
                form.Location = new Point(currentForm.Location.X + (currentForm.Width / 2) - (form.Size.Width / 2), currentForm.Location.Y + (currentForm.Height / 2) - (form.Size.Height / 2));
            
            }
            //else if (count_item_player1_active == 1 && count_item_player2_active == 1)
            //{
            //    msg = "Draw. ";
            //    Form currentForm = Form.ActiveForm;
            //    FormEndGame form = new FormEndGame(currentForm, myBoardGame, msg);
            //    form.Show();
            //    form.Location = new Point(currentForm.Location.X + (currentForm.Width / 2) - (form.Size.Width / 2), currentForm.Location.Y + (currentForm.Height / 2) - (form.Size.Height / 2));
            //}
        }

        protected void playSoundItemWalk()
        {
            if (myBoardGame.getFlagSoundEffect())
            {

                Stream str = Properties.Resources.item_walk2;
                SoundPlayer snd = new SoundPlayer(str);
                snd.Play();
                //playMusic();
            }
        }

        protected void playSoundItemKill()
        {
            if (myBoardGame.getFlagSoundEffect())
            {
                Stream str = Properties.Resources.item_kill;
                SoundPlayer snd = new SoundPlayer(str);
                snd.Play();
                //playMusic();
            }
        }

        //protected void playMusic()
        //{
        //    //if (myBoardGame.getFlagSoundEffect())
        //    //{
        //        Stream str = Properties.Resources.Rabbit_Run_OP;
        //        SoundPlayer snd = new SoundPlayer(str);
        //        snd.Play();
        //    //}
        //}
    }
}
