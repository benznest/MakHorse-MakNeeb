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
    public partial class FormChooseStarter : Form
    {
        Button virture_choose = new Button();
        Button virture_not_choose = new Button();
        MyBoardGame myBoardGame;
        Form parentForm;

        string state = "player1";

        public FormChooseStarter()
        {
            InitializeComponent();

            virture_choose.BackColor = btn_player1.BackColor;
            virture_choose.ForeColor = btn_player1.ForeColor;

            virture_not_choose.BackColor = btn_player2.BackColor;
            virture_not_choose.ForeColor = btn_player2.ForeColor;

        }
        public FormChooseStarter(Form parentForm,MyBoardGame myBoardGame)
        {
            InitializeComponent();

            this.parentForm = parentForm;
            this.myBoardGame = myBoardGame;

            if (myBoardGame.getFlagAI())
            {
                btn_player1.Text = "Computer";
                btn_player2.Text = "You";
            }

            virture_choose.BackColor = btn_player1.BackColor;
            virture_choose.ForeColor = btn_player1.ForeColor;

            virture_not_choose.BackColor = btn_player2.BackColor;
            virture_not_choose.ForeColor = btn_player2.ForeColor;

        }

        private void choose(Button btn)
        {
            btn.BackColor = virture_choose.BackColor;
            btn.ForeColor = virture_choose.ForeColor;
        }

        private void notchoose(Button btn)
        {
            btn.BackColor = virture_not_choose.BackColor;
            btn.ForeColor = virture_not_choose.ForeColor;
        }


        private void btn_player1_Click(object sender, EventArgs e)
        {
            state = "player1";
            choose(btn_player1);
            notchoose(btn_player2);
            notchoose(btn_random);
        }

        private void btn_player2_Click(object sender, EventArgs e)
        {
            state = "player2";
            choose(btn_player2);
            notchoose(btn_player1);
            notchoose(btn_random);
        }

        private void btn_random_Click(object sender, EventArgs e)
        {
            state = "random";
            choose(btn_random);
            notchoose(btn_player2);
            notchoose(btn_player1);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            parentForm.Enabled = true;
            parentForm.Focus();
            Hide();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (state == "player1")
            {
                myBoardGame.setPlayerStart(1);
            }
            else if (state == "player2")
            {
                myBoardGame.setPlayerStart(2);
            }
            else   // random
            {
                Random rd = new Random();
                int randomNumber = rd.Next(0, 300);
                if (randomNumber % 2 == 0)
                {
                    myBoardGame.setPlayerStart(1);
                }
                else
                {
                    myBoardGame.setPlayerStart(2);
                }
            }

            if (myBoardGame.boardWidth == 8 && myBoardGame.boardHeight == 8)
            {
                FormPlay8x8 form = new FormPlay8x8(myBoardGame);
                form.Show();
                form.Location = parentForm.Location;
                Hide();
                parentForm.Close();
            }
            else if (myBoardGame.boardWidth == 12 && myBoardGame.boardHeight == 12)
            {
                FormPlay12x12 form = new FormPlay12x12(myBoardGame);
                form.Show();
                form.Location = parentForm.Location;
                Hide();
                parentForm.Close();
            }
        }
    }
}
