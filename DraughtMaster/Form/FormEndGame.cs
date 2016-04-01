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
    public partial class FormEndGame : Form
    {
        Form parentForm;
        MyBoardGame myBoardGame;
        string result = "";
        public FormEndGame()
        {
            InitializeComponent();
        }

        public FormEndGame(Form parentForm,MyBoardGame myBoardGame,string result)
        {
            InitializeComponent();
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.parentForm = parentForm;
            this.myBoardGame = myBoardGame;
            this.result = result;

            label_result.Text = "" + result;
        }

        private void btn_restart_Click(object sender, EventArgs e)
        {
            myBoardGame.restartGame(parentForm, this);
            //parentForm and tis form closed in methos restartGame().
        }

        private void btn_quit_Click(object sender, EventArgs e)
        {
            //avigate Page.
            myBoardGame.clearData();
            FormMainMenu form = new FormMainMenu(myBoardGame);
            form.Show();
            form.Location = parentForm.Location;
            parentForm.Close();
            Hide();
        }
    }
}
