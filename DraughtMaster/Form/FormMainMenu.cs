using DraughtMaster.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DraughtMaster
{
    public partial class FormMainMenu : MetroFramework.Forms.MetroForm
    {
        protected MyBoardGame myBoardGame;
       // Stream str;
        //SoundPlayer snd;

        public FormMainMenu()
        {
            //str = Properties.Resources.Rabbit_Run_OP;
            //snd = new SoundPlayer(str);
            //snd.PlayLooping();
            //snd.Play();

            InitializeComponent();
            setEventButton();
            //this.BackgroundImage = (Image)Resources.ResourceManager.GetObject("bg_m");
            myBoardGame = new MyBoardGame();

            
        }

        public FormMainMenu(MyBoardGame myBoardGame)
        {
            InitializeComponent();
            setEventButton();
            //this.BackgroundImage = (Image)Resources.ResourceManager.GetObject("bg_m");
            this.myBoardGame = myBoardGame;
        }


        private void FormMainMenu_Load(object sender, EventArgs e)
        {
            //
        }

        void setEventButton()
        {
            btn_play.MouseEnter += new EventHandler(btn_MouseEnter);
            btn_play.MouseLeave += new EventHandler(btn_MouseLeave);

            btn_load.MouseEnter += new EventHandler(btn_MouseEnter);
            btn_load.MouseLeave += new EventHandler(btn_MouseLeave);

            btn_setting.MouseEnter += new EventHandler(btn_MouseEnter);
            btn_setting.MouseLeave += new EventHandler(btn_MouseLeave);

            btn_editor.MouseEnter += new EventHandler(btn_MouseEnter);
            btn_editor.MouseLeave += new EventHandler(btn_MouseLeave);

            btn_credit.MouseEnter += new EventHandler(btn_MouseEnter);
            btn_credit.MouseLeave += new EventHandler(btn_MouseLeave);

            btn_quit.MouseEnter += new EventHandler(btn_MouseEnter);
            btn_quit.MouseLeave += new EventHandler(btn_MouseLeave);

            btn_how_to_play.MouseEnter += new EventHandler(btn_MouseEnter);
            btn_how_to_play.MouseLeave += new EventHandler(btn_MouseLeave);
        }


        void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.FromArgb(224, 224, 224);
            ((Button)sender).ForeColor = Color.FromArgb(64, 64, 64);
        }

        void btn_MouseEnter(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.Tomato;
            ((Button)sender).ForeColor = Color.White;

        }


        private void btn_play_Click(object sender, EventArgs e)
        {
            // Navigate Page.
            FormSelectBoardSize form = new FormSelectBoardSize(myBoardGame);
            form.Show();
            Hide();
            form.Location = this.Location;
        }

        private void btn_setting_Click(object sender, EventArgs e)
        {
            // Navigate Page.
            this.Enabled = false;
            FormSetting form = new FormSetting(this,myBoardGame,false);
            form.Show();
            //Hide();
            form.Location = new Point(this.Location.X + (this.Width / 2) - (form.Size.Width / 2), this.Location.Y + (this.Height / 2) - (form.Size.Height / 2));       
            //form.Location = this.Location;
        }

        private void btn_editor_Click(object sender, EventArgs e)
        {
            // Navigate Page.
            //FormHelp form = new FormHelp(myBoardGame);
            FormToolGenerateMap form = new FormToolGenerateMap(myBoardGame);
            form.Show();
            Hide();
            form.Location = this.Location;
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            FormLoadSaveMenu form = new FormLoadSaveMenu(myBoardGame);
            form.Show();
            Hide();
            form.Location = this.Location;
        }

        private void btn_credit_Click(object sender, EventArgs e)
        {
            FormCredits form = new FormCredits(myBoardGame);
            form.Show();
            Hide();
            form.Location = this.Location;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_quit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_how_to_play_Click(object sender, EventArgs e)
        {
            FormHowToPlay form = new FormHowToPlay(myBoardGame);
            form.Show();
            Hide();
            form.Location = this.Location;
        }

         


    }
}
