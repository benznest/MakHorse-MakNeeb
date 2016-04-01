using DraughtMaster.Properties;
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
using System.Xml;

namespace DraughtMaster
{
    public partial class FormLoadSaveMenu : MetroFramework.Forms.MetroForm
    {
        MyBoardGame myBoardGame;
        DataGridViewLoadSave dataGridViewLoadSave;
        PreviewBoard previewBoard;

        int boardWidth = 0;
        int boardHeight = 0;

        Color colorA = Color.Transparent;
        Color colorB = Color.Transparent;

        //for button.
        List<Control> list_color1 = new List<Control>();
        List<Control> list_color2 = new List<Control>();

        Color colorSectionA = Color.Transparent;
        Color colorSectionB = Color.Transparent;

        bool flagAI = false;
        Form parentForm; // optional.

        public FormLoadSaveMenu()
        {
            InitializeComponent();
        }

        public FormLoadSaveMenu(Form parentForm,MyBoardGame myBoardGame)
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.parentForm = parentForm; 
            this.myBoardGame = myBoardGame;
            setupContent();
            setupButtonPreviewColor();
        }

        public FormLoadSaveMenu(MyBoardGame myBoardGame)
        {
            InitializeComponent();
            this.myBoardGame = myBoardGame;
            setupContent();
            setupButtonPreviewColor();
        }

        private void setupContent()
        {
            // add event to DGV.
            dataGridViewLoadBoard.CellClick += new DataGridViewCellEventHandler(dataGridViewLoadBoard_CellClick);
            dataGridViewLoadSave = new DataGridViewLoadSave(dataGridViewLoadBoard); // put accual object DGV to class.
            dataGridViewLoadSave.setupDataGridViewLoadBoard();  // setup default detail in DGV.
            dataGridViewLoadSave.loadToDataGridViewLoadBoard("boardator"); // Load xml to dataGridView.

            // Create PreviewBoard.
            previewBoard = new PreviewBoard(panel_previewBoard); // put accual object Panel to PreviewBoard object.
            previewBoard.createPanelPreviewBoard(); // create section all.
            previewBoard.setFolderXML("boardator");
            previewBoard.refreshColorBoard(); // put colour in Preview board.

            if (dataGridViewLoadBoard.RowCount > 0) // not empty  save file.
            {
                dataGridViewLoadBoard.Rows[0].Selected = true;  // row 0 selected , in start.
                dataGridViewLoadBoard_CellClick(dataGridViewLoadBoard, null);
            }
        }

        private void setColorSectionA(Color A)
        {
            colorSectionA = A;
        }

        private void setColorSectionB(Color B)
        {
            colorSectionB = B;
        }


        private void setupButtonPreviewColor()
        {
            // get Object to list
            GetAllControl(panel_groupColor1, list_color1);
            GetAllControl(panel_groupColor2, list_color2);

            // Add event click to object button group 1.
            foreach (Control control in list_color1)
            {
                if (control.GetType() == typeof(Button))
                {
                    control.Click += new EventHandler(btn_color1_Click);
                }
            }
            // Add event click to object button group 2.
            foreach (Control control in list_color2)
            {
                if (control.GetType() == typeof(Button))
                {
                    control.Click += new EventHandler(btn_color2_Click);
                }
            }
        }



        private void GetAllControl(Control c, List<Control> list)
        {
            foreach (Control control in c.Controls)
            {
                list.Add(control);

                if (control.GetType() == typeof(Panel))
                    GetAllControl(control, list);
            }
        }

        private void btn_color1_Click(object sender, EventArgs e)
        {
            clearTextInListControl(list_color1); // clear text in all button.
            ((Button)sender).Text = "X";
            setColorSectionA(((Button)sender).BackColor);
            previewBoard.setColorSectionA(((Button)sender).BackColor);
            previewBoard.refreshColorBoard();
        }

        private void btn_color2_Click(object sender, EventArgs e)
        {
            clearTextInListControl(list_color2); // clear text in all button.
            ((Button)sender).Text = "X";
            setColorSectionB(((Button)sender).BackColor);
            previewBoard.setColorSectionB(((Button)sender).BackColor);
            previewBoard.refreshColorBoard();
        }

        private void clearTextInListControl(List<Control> list)
        {
            // Clear text in All button.
            foreach (Control control in list)
            {
                if (control.GetType() == typeof(Button))
                {
                    control.Text = "";
                }
            }
        }

        private void rad_makhorse_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewLoadSave.clear();
            dataGridViewLoadSave.loadToDataGridViewLoadBoard("boardator", "Mak Horse");

            if (dataGridViewLoadBoard.RowCount > 0) // not empty  save file.
            {
                dataGridViewLoadBoard.Rows[0].Selected = true;  // row 0 selected , in start.
                dataGridViewLoadBoard_CellClick(dataGridViewLoadBoard, null);
            }
        }

        private void rad_makneeb_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewLoadSave.clear();
            dataGridViewLoadSave.loadToDataGridViewLoadBoard("boardator", "Mak Neeb");

            if (dataGridViewLoadBoard.RowCount > 0) // not empty  save file.
            {
                dataGridViewLoadBoard.Rows[0].Selected = true;  // row 0 selected , in start.
                dataGridViewLoadBoard_CellClick(dataGridViewLoadBoard, null);
            }
        }

        private void rad_all_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewLoadSave.clear();
            dataGridViewLoadSave.loadToDataGridViewLoadBoard("boardator", "Mak Horse");
            dataGridViewLoadSave.loadToDataGridViewLoadBoard("boardator", "Mak Neeb");

            if (dataGridViewLoadBoard.RowCount > 0) // not empty  save file.
            {
                dataGridViewLoadBoard.Rows[0].Selected = true;  // row 0 selected , in start.
                dataGridViewLoadBoard_CellClick(dataGridViewLoadBoard, null);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridViewLoadBoard_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            foreach (DataGridViewRow row in dataGridViewLoadBoard.SelectedRows)
            {
                previewBoard.clearPictureItemPreview();
                previewBoard.setPreviewFromXML(row.Cells[1].Value.ToString()); // 1 is name file.
                previewBoard.changeVisiblePreviewPanel();
                previewBoard.changeColorPreviewPanel_A();
                previewBoard.changeColorPreviewPanel_B();
                //MessageBox.Show("" + row.Cells[1].Value.ToString());
            } 
        }

        private void btn_play_Click(object sender, EventArgs e)
        {
            myBoardGame.clearData();
            myBoardGame.setAI(flagAI);
            // Save all data setting to Object.
            //saveSettingToObjectMyGame();
            string size="";
            foreach (DataGridViewRow row in dataGridViewLoadBoard.SelectedRows)
            {
                setXMLToObjectGame(row.Cells[1].Value.ToString()); // name file is keyword.
                size = row.Cells[2].Value.ToString();  // size
            }

            //if (colorSectionA != Color.Transparent && colorSectionB != Color.Transparent) {
            //    myBoardGame.setColorSection(colorSectionA, colorSectionB);

            //}
            myBoardGame.setColorSection(previewBoard.getColorSectionA(), previewBoard.getColorSectionB());
            //myBoardGame.setColorSection(colorSectionA, colorSectionB);

            if (size == "8 x 8")
            {
                myBoardGame.backup();
                FormPlay8x8 form = new FormPlay8x8(myBoardGame);
                form.Show();
                Hide();
                form.Location = this.Location;
                if (parentForm != null)
                {
                    parentForm.Close();
                }
            }
            else if (size == "12 x 12")
            {
                myBoardGame.backup();
                FormPlay12x12 form = new FormPlay12x12(myBoardGame);
                form.Show();
                Hide();
                form.Location = this.Location;
                if (parentForm != null)
                {
                    parentForm.Close();
                }
            }

        }

        private void setXMLToObjectGame(string namefile)
        {
            string namefolder = previewBoard.getFolderXML();
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\"+ namefolder+"\\" + namefile +".xml";
            
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(path);
            }
            catch (Exception)
            {
                MessageBox.Show("Error , not found file.");
                return;
            }

            myBoardGame.setColorSection(colorA, colorB); // save color.

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.Name == "typeBoard")
                {
                    myBoardGame.setTypeBoard(node.InnerText.ToString());
                }
                else if (node.Name == "boardWidth")
                {
                    myBoardGame.setWidth(Convert.ToInt32(node.InnerText.ToString()));
                }
                else if (node.Name == "boardHeight")
                {
                    myBoardGame.setHeight(Convert.ToInt32(node.InnerText.ToString()));
                }
                else if(node.Name == "listTableBoard")
                {
                    foreach (XmlNode subnode in node.ChildNodes)
                    {
                        if (subnode.Name == "string")
                        {
                            string[] separators = {","};
                            string value = subnode.InnerText.ToString();
                            string[] words = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                            int i = 0;
                            int j = 0;
                            int player = 0;
                            int status = 0;
                            i = Convert.ToInt32(words[0]);
                            j = Convert.ToInt32(words[1]);
                            player = Convert.ToInt32(words[2]);
                            status = Convert.ToInt32(words[3]);
                            //Console.Write("" + player);
                            if (player > 0) // have item.
                            {
                                myBoardGame.setTablePlayerHolder(i, j, player);
                                myBoardGame.setTableItemStatus(i, j, status);
                            }
                        }
                    }                   
                }
                else if (node.Name == "playerStart")
                {
                    myBoardGame.setPlayerStart(Convert.ToInt32(node.InnerText.ToString()));
                }
                else if (node.Name == "NUM_SKIP_TURN")
                {
                    myBoardGame.setNumSkipTurn(Convert.ToInt32(node.InnerText.ToString()));
                }
                else if (node.Name == "skipturn_player1")
                {
                    myBoardGame.setSkipTurnPlayer1(Convert.ToInt32(node.InnerText.ToString()));
                }
                else if (node.Name == "skipturn_player2")
                {
                    myBoardGame.setSkipTurnPlayer2(Convert.ToInt32(node.InnerText.ToString()));
                }
                else if (node.Name == "TextColorHtmlSection1")
                {
                    myBoardGame.setColorSectionA (ColorTranslator.FromHtml(node.InnerText.ToString()));
                }
                else if (node.Name == "TextColorHtmlSection2")
                {
                     myBoardGame.setColorSectionB ( ColorTranslator.FromHtml(node.InnerText.ToString()));
                }
                else if(node.Name == "index_picPlayer1")
                {
                    myBoardGame.setIndexPictureItemPlayer1(Convert.ToInt32(node.InnerText.ToString()));
                }
                else if(node.Name == "index_picPlayer2")
                {
                    myBoardGame.setIndexPictureItemPlayer2(Convert.ToInt32(node.InnerText.ToString()));
                }
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            if (parentForm == null)
            {
                FormMainMenu form = new FormMainMenu(myBoardGame);
                form.Show();
                Hide();
                form.Location = this.Location;
            }
            else
            {
                parentForm.Enabled = true;
                Hide();
            }
        }

        private void FormLoadSaveMenu_Load(object sender, EventArgs e)
        {

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            string namefile = dataGridViewLoadBoard.SelectedRows[0].Cells[1].Value.ToString();

            // get namefile for delete.
            if (MessageBox.Show("You removing '"+namefile+"'. Are you sure?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dataGridViewLoadBoard.RowCount > 0 && dataGridViewLoadBoard.SelectedRows.Count > 0)
                {
                    
                    string path = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\boardator\\" + namefile + ".xml";
                    File.Delete(path);

                    if (dataGridViewLoadBoard.SelectedRows.Count > 0)
                    {
                        int remove = dataGridViewLoadBoard.SelectedRows[0].Index;
                        dataGridViewLoadBoard.Rows.RemoveAt(remove);
                    }

                    // after delete.
                    if (dataGridViewLoadBoard.RowCount > 0) // not empty  save file.
                    {
                        dataGridViewLoadBoard.Rows[0].Selected = true;  // row 0 selected , in start.
                        dataGridViewLoadBoard_CellClick(dataGridViewLoadBoard, null);
                    }
                    else
                    {
                        previewBoard.setColorSection(Color.White, Color.White);
                        previewBoard.refreshColorBoard();
                        previewBoard.clearPictureItemPreview();
                    }
                }
            }
        }

        private void btn_old_color_Click(object sender, EventArgs e)
        {
        }

        private void dataGridViewLoadBoard_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void radio_ai_CheckedChanged(object sender, EventArgs e)
        {
            flagAI = true;
        }

        private void radio_player_CheckedChanged(object sender, EventArgs e)
        {
            flagAI = false;
        }
    }
}
