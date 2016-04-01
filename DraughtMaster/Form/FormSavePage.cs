using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DraughtMaster
{
    public partial class FormSavePage : Form
    {
        MyBoardGame myBoardGame;
        DataGridViewLoadSave dataGridViewLoadSave;
        Form parentForm;
        public FormSavePage()
        {
            InitializeComponent();
        }

        public FormSavePage(Form parentForm,MyBoardGame myBoardGame)
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.parentForm = parentForm;
            this.myBoardGame = myBoardGame;

            dataGridViewLoadBoard.CellClick += new DataGridViewCellEventHandler(dataGridViewLoadBoard_CellClick);
            dataGridViewLoadSave = new DataGridViewLoadSave(dataGridViewLoadBoard); // put accual Object DGV.
            dataGridViewLoadSave.setupDataGridViewLoadBoard();  // setup default detail in DGV.
            dataGridViewLoadSave.loadToDataGridViewLoadBoard("boardator"); // Load xml to dataGridView.

        }

        private void FormSavePage_Load(object sender, EventArgs e)
        {

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            closeThisForm();
        }

        private void closeThisForm()
        {
            //this.Hide();  // hide this Form.
            parentForm.Enabled = true;  // lock this form ,

            parentForm.WindowState = FormWindowState.Normal;
            parentForm.Focus();

            this.Close();

        }

        private void dataGridViewLoadBoard_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewLoadBoard.SelectedRows)
            {
                //previewBoard.clearPictureItemPreview();
                txt_nameSave.Text = row.Cells[1].Value.ToString();
                //MessageBox.Show("" + row.Cells[1].Value.ToString());
            } 
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
           
        }

        private void btn_savegame_Click(object sender, EventArgs e)
        {
            // replace empty string in char forbiden ti name file.
            txt_nameSave.Text = txt_nameSave.Text
                .Replace("\\", "")
                .Replace("/", "")
                .Replace("<", "")
                .Replace(">", "")
                .Replace("?", "")
                .Replace(":", "")
                .Replace(",", "")
                .Replace("*", "")
                .Replace("|", "")
                .Replace(";", "")
                .Replace("\"", "");

            if (txt_nameSave.Text == String.Empty)
            {
                MessageBox.Show("Please , input your save name.");
            }
            else
            {
                string path = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\boardator";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                path += "\\" + txt_nameSave.Text + ".xml";


                // Save Data to Object Game.

                //myBoardGame.updateTable(myBoard.getBoardSection()); // get boardSection and assgin player holder to object myGame.
                //myBoardGame.updateTableTolist(); // update value provide save object to XML.

                // Save Object Game to XML.
                if (File.Exists(path))
                {
                    if (MessageBox.Show("You want save replace, Confirm?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(MyBoardGame));
                        using (TextWriter textWriter = new StreamWriter(path))
                        {
                            serializer.Serialize(textWriter, myBoardGame);
                        }

                        MessageBox.Show("Saved to " + path);
                        closeThisForm();
                    }
                    else
                    {
                        // nothing
                    }
                }
                else
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(MyBoardGame));
                    using (TextWriter textWriter = new StreamWriter(path))
                    {
                        serializer.Serialize(textWriter, myBoardGame);
                    }

                    //MessageBox.Show("Saved to " + path);
                    MessageBox.Show("Your board saved.");
                    closeThisForm();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            closeThisForm();
        }

        private void btn_cancel_Click_1(object sender, EventArgs e)
        {
            closeThisForm();
        }
    }
}
