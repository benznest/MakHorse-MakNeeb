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
    public partial class FormHowToPlay : MetroFramework.Forms.MetroForm
    {
        MyBoardGame myBoardGame;
        string choose = "makhorse";
        public FormHowToPlay()
        {
            InitializeComponent();
        }

        public FormHowToPlay(MyBoardGame myBoardGame)
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormHowToPlay_Load(object sender, EventArgs e)
        {

        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (choose == "makhorse")
            {
                FormTutorial.MakHorse.FormTutorialMakHorse form = new FormTutorial.MakHorse.FormTutorialMakHorse(myBoardGame);
                form.Show();
                Hide();
            }
            else if (choose == "makneeb")
            {
                FormTutorial.MakNeeb.FormTutorialMakNeeb form = new FormTutorial.MakNeeb.FormTutorialMakNeeb(myBoardGame);
                form.Show();
                Hide();
            }

        }

        private void picBox_makhorse_Click(object sender, EventArgs e)
        {
            choose = "makhorse";
            picBox_makhorse.Image = Properties.Resources.ex_makhorse;
            picBox_makNeeb.Image = Properties.Resources.ex_makneeb_wb;
            lbl_makhorse.ForeColor = Color.Tomato;
            lbl_makneeb.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void picBox_makNeeb_Click(object sender, EventArgs e)
        {
            choose = "makneeb";
            picBox_makhorse.Image = Properties.Resources.ex_makhorse_wb;
            picBox_makNeeb.Image = Properties.Resources.ex_makneeb;
            lbl_makhorse.ForeColor = Color.FromArgb(64, 64, 64);
            lbl_makneeb.ForeColor = Color.Tomato;
        }
    }
}
