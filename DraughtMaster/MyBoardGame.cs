using DraughtMaster.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DraughtMaster
{
    public class MyBoardGame
    {
       public int boardWidth;
       public int boardHeight;
       public string typeBoard;
       public int rowItem;
       public bool flag_AI;
       public bool flag_forced_kill_BoardNeeb = true;
       public bool flag_forced_kill_BoardHorse = true;

        // XML Not support writing Array 2D.
       private int[,] tablePlayerInBoard;
       //private int[,] tableItemInBoard;
       private int[,] tableItemStatusInBoard;

       private int[,] tablePlayerInBoard_recovery;
       private int[,] tableItemStatusInBoard_recovery;

       // order is important in XML. 
       public int index_picPlayer1 = 0;
       public int index_picPlayer2 = 1;

       private bool flag_sound_effect = true;

       public int NUM_SKIP_TURN = 3;
       public int skipturn_player1;
       public int skipturn_player2;

       // Serializer XML Not support write array 2D direction. so we use list string replace.
       // id_row , id_col , player item , item status


       public int playerStart;

       private Color colorSection1;
       private Color colorSection2;


       public List<string> listTableBoard = new List<string>(); 
       //public string namePictureItem1 = "walker_";
       //public string namePictureItemSuper1 = "";
       //public string namePictureItem2 = "";
       //public string namePictureItemSuper2 = "";

        public MyBoardGame()
        {
            tablePlayerInBoard = new int[12, 12];
            //tableItemInBoard = new int[12, 12]; 
            tableItemStatusInBoard = new int[12, 12];

            tablePlayerInBoard_recovery = new int[12, 12];
            tableItemStatusInBoard_recovery = new int[12, 12];

            skipturn_player1 = NUM_SKIP_TURN;
            skipturn_player2 = NUM_SKIP_TURN;

            playerStart = 1;
            colorSection1 = Color.Black;
            colorSection2 = Color.White;

            //namePictureItem1 = "walker_" + index_picPlayer1;
            //namePictureItemSuper1 = "walker_super_" + index_picPlayer1;
            //namePictureItem2 = "walker_" + +index_picPlayer2;
            //namePictureItemSuper2 = "walker_super_" +index_picPlayer2;
        }

        public void backup()
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    tablePlayerInBoard_recovery[i, j] = tablePlayerInBoard[i,j];
                    tableItemStatusInBoard_recovery[i, j] = tableItemStatusInBoard[i, j];
                }
            }
        }

        public void recovery()
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    tablePlayerInBoard[i, j] = tablePlayerInBoard_recovery[i, j];
                    tableItemStatusInBoard[i, j] = tableItemStatusInBoard_recovery[i, j];
                }
            }
        }

        public void setPlayerStart(int player)
        {
            playerStart = player;
        }

        public int getPlayerStart()
        {
            return playerStart;
        }

        public void setSkipTurn(int num_skip_player1, int num_skip_player2)
        {
            skipturn_player1 = num_skip_player1;
            skipturn_player2 = num_skip_player2;
        }

        public void setSkipTurnPlayer1(int num_skip_player1)
        {
            skipturn_player1 = num_skip_player1;
        }

        public void setSkipTurnPlayer2(int num_skip_player2)
        {
            skipturn_player2 = num_skip_player2;
        }

        public void decreseSkipTurn(int player)
        {
            if (player == 1 && skipturn_player1 >0)
            {
                skipturn_player1--;
            }
            else if (player == 2 && skipturn_player2 >0)
            {
                skipturn_player2--;
            }
        }

        //public string getNamePictureItem1(int player)
        //{
        //    if (player == 1)
        //    {
        //        return namePictureItem1;
        //    }
        //    else if (player ==2)
        //    {
        //        return namePictureItem2;
        //    }
        //    return "";
        //}

        public void setIndexPictureItem(int itemPlayer1, int itemPlayer2)
        {
            index_picPlayer1 = itemPlayer1;
            index_picPlayer2 = itemPlayer2;
        }

        public void setIndexPictureItemPlayer1(int index)
        {
            index_picPlayer1 = index;
        }

        public void setIndexPictureItemPlayer2(int index)
        {
            index_picPlayer2 = index;
        }

        public string getNamePictureItemRegular(int player)
        {
            if (player == 1)
            {
                return "walker_" + index_picPlayer1;
            }
            else
            {
                return "walker_" + index_picPlayer2;
            }
        }

        public string getNamePictureItemSuper(int player)
        {
            if (player == 1)
            {
                return "walker_super_" + index_picPlayer1;
            }
            else
            {
                return "walker_super_" + index_picPlayer2;
            }
        }

        //public void setnamePictureItem(string namePictureItem1, string namePictureItemSuper1, string namePictureItem2, string namePictureItemSuper2)
        //{
        //    this.namePictureItem1       = namePictureItem1;
        //    this.namePictureItemSuper1  = namePictureItemSuper1;
        //    this.namePictureItem2       = namePictureItem2;
        //    this.namePictureItemSuper2  = namePictureItemSuper2;
        //}

        //public void setNamePictureItem1(string name)
        //{
        //    namePictureItem1 = name;
        //}

        //public void setNamePictureItem2(string name)
        //{
        //    namePictureItem2 = name;
        //}

        public void setNumSkipTurn(int num)
        {
            NUM_SKIP_TURN = num;
        }

        public int getNumSkipTurn()
        {
            return NUM_SKIP_TURN;
        }

        public int getSkipTurn(int player)
        {
            if (player == 1)
            {
                return skipturn_player1;
            }
            else if (player == 2)
            {
                return skipturn_player2;
            }
            return 0;
        }

        // set , get flag forced to kill fot board neeb.
        public void setFlagForcedKillBoardNeeb(bool flag)
        {
            flag_forced_kill_BoardNeeb = flag;
        }

        public bool getFlagForcedKillBoardNeeb()
        {
            return flag_forced_kill_BoardNeeb;
        }

        // set , get flag forced to kill fot board horse.
        public void setFlagForcedKillBoardHorse(bool flag)
        {
            flag_forced_kill_BoardHorse = flag;
        }

        public bool getFlagForcedKillBoardHorse()
        {
            return flag_forced_kill_BoardHorse;
        }

        // set , get flag sound effect in game.
        public void setFlagSoundEffect(bool flag)
        {
            flag_sound_effect = flag;
        }

        public bool getFlagSoundEffect()
        {
            return flag_sound_effect;
        }

        //test 
        public string TextColorHtmlSection1
        {
            get { return ColorTranslator.ToHtml(colorSection1); }
            set { colorSection1 = ColorTranslator.FromHtml(value); }
        }

        public string TextColorHtmlSection2
        {
            get { return ColorTranslator.ToHtml(colorSection2); }
            set { colorSection2 = ColorTranslator.FromHtml(value); }
        }

        public void setTablePlayerHolder(int id_row, int id_col, int player)
        {
            tablePlayerInBoard[id_row,id_col] = player;
        }

        public void setTableItemStatus(int id_row, int id_col, int status)
        {
            tableItemStatusInBoard[id_row, id_col] = status;
        }

        public string getTypeBoard()
        {
            return typeBoard;
        }

        public string setTypeBoard(string type)
        {
            return typeBoard = type;
        }

        public int[,] getTablePlayerHolder()
        {
            return tablePlayerInBoard;
        }

        public int[,] getTableItemStatus()
        {
            return tableItemStatusInBoard;
        }

        public void setSizeBoard(int w,int h)
        {
            boardWidth = w;
            boardHeight = h;
        }

        public void setWidth(int w)
        {
            boardWidth = w;
        }

        public void setHeight(int h)
        {
            boardHeight = h;
        }

        public void setRowItem(int r)
        {
            rowItem = r;
        }

        public void setAI(bool flag)
        {
            flag_AI = flag;
        }

        public void setColorSection(Color colorSection1,Color colorSection2)
        {
            this.colorSection1 = colorSection1;
            this.colorSection2 = colorSection2;
        }

        public bool getFlagAI()
        {
            return flag_AI;
        }

        public Color getColorSectionA()
        {
            return colorSection1;
        }

        public void setColorSectionA(Color A)
        {
            colorSection1 = A;
        }

        public Color getColorSectionB()
        {
            return colorSection2;
        }

        public void setColorSectionB(Color B)
        {
            colorSection2 = B;
        }

        public void updateTable(BoardSection[,] boardSection)
        {
            for(int i=0;i<boardSection.GetLength(1);i++){  // get lenght index row
                for (int j = 0; j < boardSection.GetLength(0); j++) // get lenght index col
                {
                    tablePlayerInBoard[i, j] = boardSection[i, j].getPlayerHolder();
                    if (tablePlayerInBoard[i, j] != 0) // have player
                    {
                        tableItemStatusInBoard[i, j] = ((Item)(boardSection[i, j].getItemFromPanelSection())).getStatusItem();
                    }
                    else
                    {
                        tableItemStatusInBoard[i, j] = 0;
                    }
                }  
            } 
        }

        // update by parameter is table 2D outside.
        public void updateTableTolist(int[,] tablePlayerInBoard,int[,] tableItemStatusInBoard){
            listTableBoard.Clear();

            int player,status;
            for(int i=0;i<boardHeight;i++){  // get lenght index row
                for (int j = 0; j < boardWidth; j++) // get lenght index col
                {
                    player = tablePlayerInBoard[i,j];
                    status = tableItemStatusInBoard[i, j];
                    if(player != 0){
                        listTableBoard.Add("" + i + "," + j + "," + player + "," + status + "");
                    }
                }
            }
        }

        public void updateTableTolist()
        {
            listTableBoard.Clear();

            int player,status;
            for(int i=0;i<boardWidth;i++){
                for(int j=0;j<boardHeight;j++){
                    player = tablePlayerInBoard[i,j];
                    status = tableItemStatusInBoard[i,j];
                    if (player > 0)
                    {
                        listTableBoard.Add("" + i + "," + j + "," + player + "," + status + "");
                    }
                }
            }
        }

        public void setupItemMapGeneral(int num)
        {
            // set up for player1.
            int j;
            //object img = Resources.ResourceManager.GetObject("walker_green");
            for (int i = 0; i < num; i++)
            {
                if (i % 2 == 0)
                {
                    j = 0;
                }
                else
                {
                    j = 1;
                }

                while (j < 12)
                {
                    tablePlayerInBoard[i, j] = 1;
                    tableItemStatusInBoard[i, j] = 1;
                    //setPicturetoSection(i, j, img);
                    j += 2;
                }
            }

            // set down For player 2.
            //object img2 = Resources.ResourceManager.GetObject("walker_red");
            for (int i = boardHeight - 1; i >= boardHeight - num; i--)
            {
                if (i % 2 == 0)
                {
                    j = 0;
                }
                else
                {
                    j = 1;
                }

                while (j < 12)
                {
                    tablePlayerInBoard[i, j] = 2;
                    tableItemStatusInBoard[i, j] = 1;
                    j += 2;
                }
            }
        }

        public void clearData()
        {
            boardWidth =0 ;
            boardHeight = 0;
            rowItem = 0;
            flag_AI = false ;

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    tablePlayerInBoard[i, j] = 0;
                    tableItemStatusInBoard[i, j] = 0;
                }
            }
        }

        public void restartGame(Form oldForm,Form oldMenu)
        {
            string size = "" + this.boardWidth + " x " + this.boardHeight + "";
            this.recovery(); // recovery data in board started. 

            if (size == "8 x 8")
            {
                //MetroMessageBox.Show(this, "Let Go!!!");
                FormPlay8x8 form = new FormPlay8x8(this);
                form.Show();
                oldForm.Hide();
                form.Location = oldForm.Location;
            }
            else if (size == "12 x 12")
            {
                //MetroMessageBox.Show(this, "Let Go!!!");
                FormPlay12x12 form = new FormPlay12x12(this);
                form.Show();
                oldForm.Hide();
                form.Location = oldForm.Location;
            }
            else
            {
                MessageBox.Show("This feature is not support.");
                FormMainMenu form = new FormMainMenu(this);
                form.Show();
                oldForm.Hide();
                form.Location = oldForm.Location;
            }

            if (oldMenu != null)
            {
                oldMenu.Close();
            }
            oldForm.Close();
        }
        
    }
}
