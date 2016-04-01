using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using DraughtMaster.Properties;

namespace DraughtMaster
{
    public partial class FormSetting : Form
    {
        Form parentForm;
        MyBoardGame myBoardGame;

        int numPicture = 7;
        PictureBox[] picItem;
        PictureBox[] picItemSuper;
        RadioButton[] radioPlayer1;
        RadioButton[] radioPlayer2;

        public FormSetting()
        {
            InitializeComponent();
        }

        public FormSetting(Form parentForm, MyBoardGame myBoardGame,bool flag_lock_playing)
        {
            InitializeComponent();
            picItem      = new PictureBox[numPicture];
            picItemSuper = new PictureBox[numPicture];
            radioPlayer1 = new RadioButton[numPicture];
            radioPlayer2 = new RadioButton[numPicture];

            this.parentForm = parentForm;
            this.myBoardGame = myBoardGame;

            accessContent(panel_item, panel_itemSuper,groupBoxPlayer1,groupBoxPlayer2);
            putImageFormResourceToPictureBox();
            setRadioButtonConsistWithData(myBoardGame.index_picPlayer1,myBoardGame.index_picPlayer2);

            toggle_soundeffect.Checked = myBoardGame.getFlagSoundEffect();
            toggle_forcedKillBoardNeeb.Checked = myBoardGame.getFlagForcedKillBoardNeeb();
            toggle_forcedKillBoardHorse.Checked = myBoardGame.getFlagForcedKillBoardHorse();
            comboBox_skipturn.SelectedIndex = myBoardGame.getNumSkipTurn();

            lockSetting(flag_lock_playing);
        }

        private void FormSetting_Load(object sender, EventArgs e)
        {

        }

        private void lockSetting(bool flag)
        {
            if (flag)
            {
                toggle_forcedKillBoardNeeb.Enabled = false;
                toggle_forcedKillBoardHorse.Enabled = false;
                comboBox_skipturn.Enabled = false;
            }
            else
            {
                toggle_forcedKillBoardNeeb.Enabled = true;
                toggle_forcedKillBoardHorse.Enabled = true;
                comboBox_skipturn.Enabled = true;
            }
        }

        // access content in form and put into array.
        public void accessContent(Panel myPanelItem, Panel myPanelItemSuper, GroupBox groupBoxPlayer1, GroupBox groupBoxPlayer2) // parameter is Panel Parent.
        {
            int i = 0;
            while (i < numPicture)
            {
                // Fetch Panel children in Parent Panel store to Array.
                // access pivturebox regular image.
                object obj = myPanelItem.Controls.Find("pictureBox_r" + i, false).FirstOrDefault();
                if (obj != null)
                {
                     picItem[i] = (PictureBox)obj;
                }

                // access pivturebox super mage.
                object obj2 = myPanelItemSuper.Controls.Find("pictureBox_s" + i, false).FirstOrDefault();
                if (obj2 != null)
                {
                    picItemSuper[i] = (PictureBox)obj2;
                }

                // access radio button player1.
                object obj3 = groupBoxPlayer1.Controls.Find("radio_p1_" + i, false).FirstOrDefault();
                if (obj3 != null)
                {
                    radioPlayer1[i] = (RadioButton)obj3;
                }

                // access radio button player2.
                object obj4 = groupBoxPlayer2.Controls.Find("radio_p2_" + i, false).FirstOrDefault();
                if (obj4 != null)
                {
                    radioPlayer2[i] = (RadioButton)obj4;
                }

                i++;
            }
        }

        private void putImageFormResourceToPictureBox()
        {
            for (int i = 0; i < numPicture; i++)
            {
                object img = Resources.ResourceManager.GetObject("walker_"+i);
                picItem[i].Image = (Image)img;

                object img2 = Resources.ResourceManager.GetObject("walker_super_" + i);
                picItemSuper[i].Image = (Image)img2;
            }
        }

        private void setRadioButtonConsistWithData(int index_picPlayer1, int index_picPlayer2)
        {
            for (int i = 0; i < numPicture; i++)
            {
                if (index_picPlayer1 == i)
                {
                    radioPlayer1[i].Checked = true;
                }
                if (index_picPlayer2 == i)
                {
                    radioPlayer2[i].Checked = true;
                }
            }
        }


        private void btn_back_Click(object sender, EventArgs e)
        {
            // Navigate Page.
            FormMainMenu form = new FormMainMenu(myBoardGame);
            form.Show();
            Hide();
            form.Location = this.Location;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            parentForm.Enabled = true;  // lock this form ,
            //parentForm.Activate();

            parentForm.WindowState = FormWindowState.Normal;
            parentForm.Focus();

            this.Close();
        }

        private void toggle_soundeffect_CheckedChanged(object sender, EventArgs e)
        {
                myBoardGame.setFlagSoundEffect(toggle_soundeffect.Checked);
        }

        private void btn_back_Click_1(object sender, EventArgs e)
        {
            parentForm.Enabled = true;  // lock this form ,
            //parentForm.Activate();

            parentForm.WindowState = FormWindowState.Normal;
            parentForm.Focus();

            this.Close();
        }

        private void comboBox_skipturn_SelectedIndexChanged(object sender, EventArgs e)
        {
            int skipturn = comboBox_skipturn.SelectedIndex;
            myBoardGame.setNumSkipTurn(skipturn);
            myBoardGame.setSkipTurn(skipturn, skipturn);
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            int index_player1 = 0;
            int index_player2 = 0;

            for (int i = 0; i < numPicture; i++)
            {
                if (radioPlayer1[i].Checked)
                {
                    index_player1 = i;
                }

                if (radioPlayer2[i].Checked)
                {
                    index_player2 = i;
                }
            }

            if (index_player1 == index_player2)
            {
                MessageBox.Show("Error, should not same item.");
            }
            else
            {
                //string nameItem = "walker_";
                //string nameItemSuper = "walker_super_";
                //myBoardGame.setnamePictureItem(nameItem+index_player1,nameItemSuper+index_player1
                //                              ,nameItem+index_player2,nameItemSuper+index_player2);

                myBoardGame.setIndexPictureItem(index_player1, index_player2);
                MessageBox.Show("Item is Changed.");
            }
        }

        private void toggle_forcedKillBoardNeeb_CheckedChanged(object sender, EventArgs e)
        {
            myBoardGame.setFlagForcedKillBoardNeeb(toggle_forcedKillBoardNeeb.Checked);
        }

        private void toggle_forcedKillBoardHorse_CheckedChanged(object sender, EventArgs e)
        {
            myBoardGame.setFlagForcedKillBoardHorse(toggle_forcedKillBoardHorse.Checked);
        }
    }
}
