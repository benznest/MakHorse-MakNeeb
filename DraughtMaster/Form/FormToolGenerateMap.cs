using DraughtMaster.Properties;
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
    public partial class FormToolGenerateMap : MetroFramework.Forms.MetroForm
    {
        MyBoardGame myBoardGame;
        List<Control> list_color1 = new List<Control>();
        Button[,] btnSection = new Button[12, 12];
        Color[] btn_status = { Color.White, Color.Tomato, Color.Red, Color.LimeGreen ,Color.ForestGreen, };

        // 0 = none  
        // 1 = player1 Regular , 2 = player1 Regular
        // 3 = player2 Super   , 4 = player2 super

        Color color_selected = Color.DimGray;
        Color color_notselected = Color.Gainsboro;

        int status_select = 1;


        int boardHeight = 0;
        int boardWidth = 0;

        string state = "Player1Regular";

        public FormToolGenerateMap()
        {
            InitializeComponent();
        }

        public FormToolGenerateMap(MyBoardGame myBoardGame)
        {
            InitializeComponent();
            this.myBoardGame = myBoardGame;
            setupButtonPreviewColor();

            // default 8x8.
            setSizeBoard(8, 8);
            radio_8x8.Checked = true;
            updateDisableButton();
            updateEnableButton();
            setLabelHowToUse();
            
            // default makHorse
            setBoardMakHorse();
            radio_makHorse.Checked = true;

            //updateEnableButton();
        }

        private void FormToolGenerateMap_Load(object sender, EventArgs e)
        {

        }

        private void setSizeBoard(int w, int h)
        {
            boardHeight = h;
            boardWidth = w;
        }

        private bool isSizeBoard(int w, int h)
        {
            if (boardHeight == w && boardWidth == h)
            {
                return true;
            }
            return false;
        }

        private void setLabelHowToUse()
        {
            string str = " 1. Choose type of your game. " +
                         "\r\n 2. Choose size of Board." +
                         "\r\n 3. Click choose type of item." +
                         "\r\n     Pink is regular of player 1" +
                         "\r\n     Red is super of player 1" +
                         "\r\n     Green is regular of player 2" +
                         "\r\n     Great green is super of player 2" +
                         "\r\n     White color is empty in board." +
            "\r\n     Important , player1 start on top board " +
            "\r\n     and player2 start in down board." +
            "\r\n 4. Click export button to save this board." +
            "\r\n 5. Load your board in Load mission Menu.";
            label_howToUse.Text = str;
        }

        private void setupButtonPreviewColor()
        {
            // get Object to list
            int x = 5, y = 18;
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    btnSection[i, j] = new Button();
                    btnSection[i, j].Location = new Point(x, y);
                    btnSection[i, j].Width = 30;
                    btnSection[i, j].Height = 30;
                    btnSection[i, j].Text = "";
                    btnSection[i, j].Font = new Font(btnSection[i, j].Font.Name, 9.0F);
                    btnSection[i, j].ForeColor = Color.WhiteSmoke;
                    btnSection[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    btnSection[i, j].Enabled = false;
                    btnSection[i, j].Click += new EventHandler(btn_changeStatus_Click);
                    panel_mycreate.Controls.Add(btnSection[i, j]);

                    x += btnSection[i, j].Width;
                }
                x = 5;
                y += 30;
            }


        }

        private void btn_changeStatus_Click(object sender, EventArgs e)
        {
            //int status = Convert.ToInt32(((Button)sender).Text);
            //status = (status + 1) % 5;
            //((Button)sender).Text = "" + status;

            //((Button)sender).BackColor = btn_status[status];
            if (state == "Player1Regular")
            {
                ((Button)sender).BackColor = Color.Tomato;
                ((Button)sender).Text = "1r";
            }
            else if(state == "Player1Super"){
                 ((Button)sender).BackColor = Color.Red;
                 ((Button)sender).Text = "1S";
            }
            else if(state == "Player2Regular")
            {
                ((Button)sender).BackColor = Color.LimeGreen;
                ((Button)sender).Text = "2r";
            }
            else if(state == "Player2Super")
            {
                ((Button)sender).BackColor = Color.ForestGreen;
                ((Button)sender).Text = "2S";
            }
            else
            {
                ((Button)sender).BackColor = Color.White;
            }
        }


        private void radio_makHorse_CheckedChanged(object sender, EventArgs e)
        {
            setBoardMakHorse();
        }

        private void setBoardMakHorse()
        {
            for (int i = 0; i < boardHeight; i++)
            {
                for (int j = 0; j < boardWidth; j++)
                {
                    if ((i % 2 == 0 && j % 2 == 0) || (i % 2 == 1 && j % 2 == 1))
                    {
                        if (btnSection[i, j].Enabled == false)
                        {
                            btnSection[i, j].Enabled = true;
                            btnSection[i, j].BackColor = Color.White;
                            btnSection[i, j].Text = "0";
                        }
                    }
                    else
                    {
                        btnSection[i, j].BackColor = Color.Gainsboro;
                        btnSection[i, j].Enabled = false;
                        btnSection[i, j].Text = "";
                    }
                }
            }
        }

        private void radio_makNeeb_CheckedChanged(object sender, EventArgs e)
        {
            setBoardMakNeeb();
        }

        private void setBoardMakNeeb()
        {
            for (int i = 0; i < boardHeight; i++)
            {
                for (int j = 0; j < boardWidth; j++)
                {
                    if (btnSection[i, j].Enabled == false)
                    {
                        btnSection[i, j].Text = "0";
                        btnSection[i, j].BackColor = Color.White;
                        btnSection[i, j].Enabled = true;
                    }
                }
            }
        }

        private void updateDisableButton()
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (i >= boardHeight || j >= boardWidth)
                    {
                        btnSection[i, j].Enabled = false;
                        btnSection[i, j].BackColor = Color.White;
                        btnSection[i, j].Text = "";
                    }
                }
            }
        }

        public void updateEnableButton()
        {
            if (radio_makHorse.Checked)
            {
                setBoardMakHorse();
            }
            else
            {
                setBoardMakNeeb();
            }
        }

        private void radio_8x8_CheckedChanged(object sender, EventArgs e)
        {
            if (!isSizeBoard(8, 8))
            {
                setSizeBoard(8, 8);
                updateDisableButton();
                updateEnableButton();
            }
        }

        private void radio_12x8_CheckedChanged(object sender, EventArgs e)
        {
            if (!isSizeBoard(12, 8))
            {
                setSizeBoard(12, 8);
                updateDisableButton();
                updateEnableButton();
            }
        }

        private void radio_12x12_CheckedChanged(object sender, EventArgs e)
        {
            if (!isSizeBoard(12, 12))
            {
                setSizeBoard(12, 12);
                updateDisableButton();
                updateEnableButton();
            }
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            int[,] tablePlayerHolder = new int[boardHeight, boardWidth];
            int[,] tableStatusItem = new int[boardHeight, boardWidth];
            int count_player1 = 0;
            int count_player2 = 0;
            for (int i = 0; i < boardHeight; i++)
            {
                for (int j = 0; j < boardWidth; j++)
                {
                    if(btnSection[i,j].BackColor == btn_status[1]){
                        tablePlayerHolder[i, j] = 1;
                        tableStatusItem[i,j] = 1;
                        count_player1++;
                    }
                    else if(btnSection[i,j].BackColor == btn_status[2]){
                        tablePlayerHolder[i, j] = 1;

                        if (radio_makNeeb.Checked) // mak neeb not hav super.
                        {
                            tableStatusItem[i, j] = 1;
                        }
                        else
                        {
                            tableStatusItem[i, j] = 2;
                        }
                        
                        count_player1++;
                    }
                    else if (btnSection[i, j].BackColor == btn_status[3])
                    {
                        tablePlayerHolder[i, j] = 2;
                        tableStatusItem[i, j] = 1;
                        count_player2++;
                    }
                    else if (btnSection[i, j].BackColor == btn_status[4])
                    {
                        tablePlayerHolder[i, j] = 2;

                        if (radio_makNeeb.Checked) // mak neeb not hav super.
                        {
                            tableStatusItem[i, j] = 1;
                        }
                        else
                        {
                            tableStatusItem[i, j] = 2;
                        }

                        count_player2++;
                    }
                    else
                    {
                        tablePlayerHolder[i, j] = 0;
                        tableStatusItem[i, j] = 0;
                    }

                    // set Hsuper , prevent bug.
                    if (i == 0 && btnSection[i, j].BackColor == btn_status[3])
                    {
                        tableStatusItem[i, j] = 2;
                    }
                    else if (i == boardHeight - 1 && btnSection[i, j].BackColor == btn_status[1])
                    {
                        tableStatusItem[i, j] = 2;
                    }
                }
            }

            if(count_player1 >0 && count_player1 <= 20 && count_player2 >0 && count_player1 <=20){
                //MessageBox.Show(count_player1 + "  - " + count_player2);
                if (radio_makHorse.Checked)
                {
                    myBoardGame.setTypeBoard("Mak Horse");
                }
                else if(radio_makNeeb.Checked)
                {
                    myBoardGame.setTypeBoard("Mak Neeb");
                }

                myBoardGame.clearData();
                myBoardGame.setSizeBoard(boardWidth, boardHeight);
                myBoardGame.updateTableTolist(tablePlayerHolder,tableStatusItem);
                FormSavePage form = new FormSavePage(this,myBoardGame);  // load save form.
                form.Show();
                //Hide();
                form.Location = new Point(this.Location.X + (this.Width / 4), this.Location.Y + (this.Height/4));
        
            }
            else
            {
                MessageBox.Show("Sorry , each player should have item more than 1 and less than 20.");
            }
                
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            // Navigate Page.
            myBoardGame.clearData();
            FormMainMenu form = new FormMainMenu(myBoardGame);
            form.Show();
            Hide();
            form.Location = this.Location;
        }

        private void radio_p1_general_CheckedChanged(object sender, EventArgs e)
        {
            state = "Player1Regular";
        }

        private void radio_p1_super_CheckedChanged(object sender, EventArgs e)
        {
            state = "Player1Super";
        }

        private void radio_p2_general_CheckedChanged(object sender, EventArgs e)
        {
            state = "Player2Regular";
        }

        private void radio_p2_super_CheckedChanged(object sender, EventArgs e)
        {
            state = "Player2Super";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            state = "Empty";
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            if (radio_makHorse.Checked)
            {
                for (int i = 0; i < boardHeight; i++)
                {
                    for (int j = 0; j < boardWidth; j++)
                    {
                        if ((i % 2 == 0 && j % 2 == 0) || (i % 2 == 1 && j % 2 == 1))
                        {
                                btnSection[i, j].Enabled = true;
                                btnSection[i, j].BackColor = Color.White;
                                btnSection[i, j].Text = "0";
                        }
                        //else
                        //{
                        //    btnSection[i, j].BackColor = Color.Gainsboro;
                        //    btnSection[i, j].Enabled = false;
                        //    btnSection[i, j].Text = "";
                        //}
                    }
                }
            }
            else if(radio_makNeeb.Checked)
            {
                for (int i = 0; i < boardHeight; i++)
                {
                    for (int j = 0; j < boardWidth; j++)
                    {
                            btnSection[i, j].Text = "0";
                            btnSection[i, j].BackColor = Color.White;
                            btnSection[i, j].Enabled = true;
                    }
                }
            }
        }
    }
}
