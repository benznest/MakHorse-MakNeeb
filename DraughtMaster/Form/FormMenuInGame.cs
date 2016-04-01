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
    public partial class FormMenuInGame : Form
    {
        MyBoardGame myBoardGame;
        Form parentForm;
        Board myBoard;
        //string typeBoard = "";

        public FormMenuInGame()
        {
            InitializeComponent();
        }

        public FormMenuInGame(Form parentForm, MyBoardGame myBoardGame)
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.parentForm = parentForm;
            this.myBoardGame = myBoardGame;
            //typeBoard = "not";
        }

        public FormMenuInGame(Form parentForm,MyBoardGame myBoardGame,Board myBoard)
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.parentForm = parentForm;
            this.myBoardGame = myBoardGame;
            this.myBoard= myBoard;
            //typeBoard = "makHorse";
        }

        //public FormMenuInGame(Form parentForm, MyBoardGame myBoardGame,BoardPinch myBoard)
        //{
        //    InitializeComponent();
        //    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        //    this.parentForm = parentForm;
        //    this.myBoardGame = myBoardGame;
        //    myBoardPinch = myBoard;
        //    typeBoard = "makNeeb";
        //}

        private void btn_save_Click(object sender, EventArgs e)
        {

            this.Enabled = false;  // lock this form ,
            FormSavePage form = new FormSavePage(this, myBoardGame);  // load save form.
            form.Show();
            //Hide();
            //form.Size.Width / 2;
            //form.Size.Height / 2;
            form.Location = new Point(this.Location.X + (this.Width / 2) - (form.Size.Width / 2), this.Location.Y + (this.Height / 2) - (form.Size.Height / 2));       
       
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            // refresh Item after setting.
            //if (typeBoard == "MakHorse")
            //{
            //    myBoardHorse.refreshItemPictureOnBoard();
            //}
            //else if (typeBoard == "MakNeeb")
            //{
            //    myBoardPinch.refreshItemPictureOnBoard();
            //}
            if (myBoard != null)
            {
                myBoard.refreshItemPictureOnBoard();
            }


            parentForm.Enabled = true;  // lock this form ,
            //parentForm.Activate();
            
            parentForm.WindowState = FormWindowState.Normal;
            parentForm.Focus();

            this.Close();
        }

        private void btn_quit_Click(object sender, EventArgs e)
        {
            // Navigate Page.
            myBoardGame.clearData();
            FormMainMenu form = new FormMainMenu(myBoardGame);
            form.Show();
            Hide();
            form.Location = parentForm.Location;
            parentForm.Close();
            
        }

        private void btn_restart_Click(object sender, EventArgs e)
        {
            myBoardGame.restartGame(parentForm,this);

            //string size = "" + myBoardGame.boardWidth + " x " + myBoardGame.boardHeight+"";
            //myBoardGame.recovery(); // recovery data in board started. 

            //if (size == "8 x 8")
            //{
            //    //MetroMessageBox.Show(this, "Let Go!!!");
            //    FormPlay8x8 form = new FormPlay8x8(myBoardGame);
            //    form.Show();
            //    Hide();
            //    form.Location = parentForm.Location;
            //}
            //else
            //{
            //    MessageBox.Show("This feature is not support.");
            //    FormMainMenu form = new FormMainMenu(myBoardGame);
            //    form.Show();
            //    Hide();
            //    form.Location = parentForm.Location;
            //}
            //parentForm.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Navigate Page.
            FormSetting form = new FormSetting(this, myBoardGame,true);
            form.Show();
            //Hide();
            form.Location = new Point(this.Location.X + (this.Width / 2) - (form.Size.Width / 2), this.Location.Y + (this.Height / 2) - (form.Size.Height / 2));
            //form.Location = this.Location;
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            FormLoadSaveMenu form = new FormLoadSaveMenu(parentForm,myBoardGame);
            form.Show();
            Hide();
            form.Location = parentForm.Location;
        }
    }
}
