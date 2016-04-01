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
    public partial class FormHelp : MetroFramework.Forms.MetroForm
    {
        MyBoardGame myBoardGame;
        public FormHelp()
        {
            InitializeComponent();
        }

        public FormHelp(MyBoardGame myBoardGame)
        {
            InitializeComponent();
            this.myBoardGame = myBoardGame;
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            // Navigate Page.
            FormMainMenu form = new FormMainMenu(myBoardGame);
            form.Show();
            Hide();
            form.Location = this.Location;
        }

    }
}
