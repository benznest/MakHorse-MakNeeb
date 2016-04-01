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
    public partial class FormCredits : MetroFramework.Forms.MetroForm
    {
        protected MyBoardGame myBoardGame;
        public FormCredits()
        {
            InitializeComponent();
        }

        public FormCredits(MyBoardGame myBoardGame)
        {
            this.myBoardGame = myBoardGame;
            InitializeComponent();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            FormMainMenu form = new FormMainMenu(myBoardGame);
            form.Show();
            Hide();
            form.Location = this.Location;
        }
    }
}
