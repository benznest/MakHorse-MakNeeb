using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DraughtMaster
{
    public partial class FormPlay12x12 : MetroFramework.Forms.MetroForm
    {

        MyBoardGame myBoardGame;
        private Panel[,] myPanelBoard = new Panel[8,8];
        public Label lblTurnplayer;
        string typeBoard="";
        Board myBoard;
        bool flag_AI;
        //BoardPinch myBoard;


        public FormPlay12x12()
        {
            InitializeComponent();
        }


        public FormPlay12x12(MyBoardGame myBoardGame)
        {
            InitializeComponent();
            this.myBoardGame = myBoardGame;

            // Create Board. AI
            this.flag_AI = myBoardGame.getFlagAI();
            //this.flag_AI = true;
            typeBoard = myBoardGame.getTypeBoard();
            if (typeBoard == "Mak Horse")
            {
                myBoard = new BoardHorse(this, Panel_myBorad, 12, 12, flag_AI, myBoardGame);
                myBoard.setFlagForcedKill(myBoardGame.getFlagForcedKillBoardHorse());
            }
            else if (typeBoard == "Mak Neeb")
            {
                myBoard = new BoardNeeb(this, Panel_myBorad, 12, 12, flag_AI, myBoardGame);
                myBoard.setFlagForcedKill(myBoardGame.getFlagForcedKillBoardNeeb());
            }
            else
            {
                MessageBox.Show("Error. type board cannot opened.");
            }

            //set Color Panel Start Board.
            myBoard.setColorPanelBoardSection(myBoardGame.getColorSectionA(),myBoardGame.getColorSectionB());
            myBoard.refreshColorOnBoard();

            //set access content to data in object.
            myBoard.setLabelTurnPlayerContent(lbl_turnPlayer); 
            myBoard.setLabelCounterItemActiveContent(lbl_item_player1, lbl_item_player2);

            // set start player.
            int startplayer = myBoardGame.getPlayerStart();
            myBoard.setTurnPlayerStart(startplayer); // set start player turn.
            MessageBox.Show("Start player is Player" + startplayer);


            myBoard.setProgressBarPlayer(progressBar_player1, progressBar_player2);
            myBoard.setLabelNumItem(lbl_num_player1, lbl_num_player2);

            myBoard.setLabelActiveSkip(lbl_skip_player1, lbl_skip_player2);
            myBoard.setLabelNumSkip(lbl_numSkip_player1, lbl_numSkip_player2);

            myBoard.setButtonSkipturn(btn_skipturn);

            // setup item player on Board.
            myBoard.createObjectItem(20,20);
            setupMap(12,12);
            myBoard.setCounterItemPlayer();
            myBoard.setListItemActiveAI();
            myBoard.updateCounterActiveItem();
            //myBoard.setTextBoxContentForDeBugging(txt_AddressItem, txt_AddressItem2,txt_playerHolder, txt_itemInBoard);
            myBoard.updateDataDeBugging(); 
        }


        private void setupMap(int row,int col)
        {
            int[,] tablePlayerHolder = myBoardGame.getTablePlayerHolder();
            int[,] tableItemStatus = myBoardGame.getTableItemStatus();
            int count_player1 = 0;
            int count_player2 = 0;

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (tablePlayerHolder[i, j] == 1) // have item player.
                    {
                        myBoard.createItem(count_player1, 1, tableItemStatus[i,j]);
                        myBoard.setItemToPanelSection(i, j, 1, myBoard.getItemObject(count_player1, 1));
                        myBoard.getItemObject(count_player1, 1).setIdRowAndCol(i, j);
                        count_player1++;
                    }
                    else if(tablePlayerHolder[i, j] == 2) 
                    {
                        myBoard.createItem(count_player2, 2, tableItemStatus[i,j]);
                        myBoard.setItemToPanelSection(i, j, 2, myBoard.getItemObject(count_player2, 2));
                        myBoard.getItemObject(count_player2, 2).setIdRowAndCol(i, j);
                        count_player2++;
                    }
                    //Console.Write(" " + tableItemStatus[i, j]);
                }
                //Console.Write(" \r\n");
            }
        }

        private void FormPlay12x12_Load(object sender, EventArgs e)
        {

        }

        private void btn_skip_Click(object sender, EventArgs e)
        {
            if (myBoard.getCountListSectionSelected() == 0)
            {
                myBoard.skipTurn();
            }
            else
            {
                MessageBox.Show("You cannot skip turn while choose walk. ");
            }
        }

        private void btn_menu_Click(object sender, EventArgs e)
        {
            myBoardGame.updateTable(myBoard.getBoardSection()); // get boardSection and assgin player holder to object myGame.
            myBoardGame.updateTableTolist(); // update value provide save object to XML.

            this.Enabled = false;  // lock this form ,
            FormMenuInGame form = new FormMenuInGame(this, myBoardGame,myBoard);  // load save form.
            form.Show();
            //Hide();
            form.Location = new Point(this.Location.X + (this.Width / 2) - (form.Size.Width / 2), this.Location.Y + (this.Height / 2) - (form.Size.Height / 2));
        }
    }
}
