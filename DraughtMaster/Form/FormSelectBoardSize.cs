using DraughtMaster.Properties;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DraughtMaster
{
    public partial class FormSelectBoardSize : MetroFramework.Forms.MetroForm
    {
        MyBoardGame myBoardGame;
        DataGridViewLoadSave dataGridViewLoadSave;
        PreviewBoard previewBoard;

        // for button
        List<Control> list_color1 = new List<Control>();
        List<Control> list_color2 = new List<Control>();

        int boardWidth = 0;
        int boardHeight = 0;
        int rowItem = 2;
        bool flagAI = false;

        Color colorSectionA;
        Color colorSectionB;

        public FormSelectBoardSize()
        {

        }

        public FormSelectBoardSize(MyBoardGame myBoardGame)
        {
            
            InitializeComponent();
            this.myBoardGame = myBoardGame;  // 

            // add event to DGV.
            dataGridViewLoadBoard.CellClick += new DataGridViewCellEventHandler(dataGridViewLoadBoard_CellClick);
            dataGridViewLoadSave = new DataGridViewLoadSave(dataGridViewLoadBoard); // put accual object DGV to class.
            dataGridViewLoadSave.setupDataGridViewLoadBoard();  // setup default detail in DGV.
            dataGridViewLoadSave.loadToDataGridViewLoadBoard("boardatorMoreMap"); // Load xml to dataGridView.

            // Create PreviewBoard. // myboardgame parameter for get image item dynamic.
            previewBoard = new PreviewBoard(panel_previewBoard,myBoardGame); // put accual object Panel to PreviewBoard object.
            previewBoard.createPanelPreviewBoard(); // create section all.
            previewBoard.setFolderXML("boardatorMoreMap");
            previewBoard.refreshColorBoard(); // put colour in Preview board.


            setupButtonPreviewColor();

            if (dataGridViewLoadBoard.RowCount > 0) // not empty  save file.
            {
                dataGridViewLoadBoard.Rows[0].Selected = true;  // row 0 selected , in start.
                dataGridViewLoadBoard_CellClick(dataGridViewLoadBoard, null);
            }

            //MetroFramework.MetroMessageBox.Show(this, "Hello", "...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void dataGridViewLoadBoard_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewLoadBoard.SelectedRows)
            {
                previewBoard.clearPictureItemPreview();
                previewBoard.setPreviewFromXML(row.Cells[1].Value.ToString());
                previewBoard.changeVisiblePreviewPanel();
                previewBoard.changeColorPreviewPanel_A();
                previewBoard.changeColorPreviewPanel_B();
                //MessageBox.Show("" + row.Cells[1].Value.ToString());
            }
        }


        private void FormSelectBoardSize_Load(object sender, EventArgs e)
        {
            
        }

        private void setBoardSize(int w,int h){
            boardWidth = w;
            boardHeight = h;
        }

        private void setRowItem(int row)
        {
            rowItem = row;
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

        private void setColorSection(Color A, Color B)
        {
            colorSectionA = A;
            colorSectionB = B;
        }

        private void setColorSectionA(Color A)
        {
            colorSectionA = A;
        }

        private void setColorSectionB(Color B)
        {
            colorSectionB = B;
        }

        private void setColorStartPanelPreview(Button A, Button B)
        {
            A.Text = B.Text = "x";
            setColorSection(A.BackColor, B.BackColor);
            previewBoard.setColorSection(A.BackColor, B.BackColor);
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

        private void btn_back_Click(object sender, EventArgs e)
        {
            // Navigate Page.
            FormMainMenu form = new FormMainMenu(myBoardGame);
            form.Show();
            Hide();
            form.Location = this.Location;
        }

        private void saveSettingToObjectMyGame()
        {
            myBoardGame.setSizeBoard(boardWidth,boardHeight);
            //myBoardGame.setRowItem(rowItem);
            //myBoardGame.setColorSection(colorSectionA,colorSectionB); // have bug when player not choose button.
            myBoardGame.setColorSection(previewBoard.getColorSectionA(), previewBoard.getColorSectionB());
            myBoardGame.setAI(flagAI);
        }

        private void btn_play_Click(object sender, EventArgs e)
        {


            // Save all data setting to Object.
            saveSettingToObjectMyGame();
            
            string size = "";
            string type = "";
            foreach (DataGridViewRow row in dataGridViewLoadBoard.SelectedRows)
            {
                setXMLToObjectGame(row.Cells[1].Value.ToString()); // name file is keyword.
                size = row.Cells[2].Value.ToString();  // size
                type = row.Cells[0].Value.ToString();
            }

            myBoardGame.backup();   // copy data for backup , when user restart board.

            if (flagAI && type == "Mak Horse")
            {
                MessageBox.Show("This feature is not support.");
                return;
            }

            if (size == "8 x 8")
            {
                //MetroMessageBox.Show(this, "Let Go!!!");
                //FormPlay8x8 form = new FormPlay8x8(myBoardGame);
                this.Enabled = false;
                FormChooseStarter form = new FormChooseStarter(this,myBoardGame);
                form.Show();
                //Hide();
                form.Location = new Point(this.Location.X + (this.Width / 2) - (form.Size.Width / 2), this.Location.Y + (this.Height / 2) - (form.Size.Height / 2));
       
            }
            else if (size == "12 x 12")
            {
                this.Enabled = false;
                FormChooseStarter form = new FormChooseStarter(this, myBoardGame);
                form.Show();
                //Hide();
                form.Location = new Point(this.Location.X + (this.Width / 2) - (form.Size.Width / 2), this.Location.Y + (this.Height / 2) - (form.Size.Height / 2));
       
            }
            else
            {
                MessageBox.Show("This feature is not support.");
            }


        }

        private void setXMLToObjectGame(string namefile)
        {
            string namefolder = previewBoard.getFolderXML();
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + namefolder + "\\" + namefile + ".xml";

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

            // set color.
            myBoardGame.setColorSection(previewBoard.getColorSectionA(), previewBoard.getColorSectionB()); 

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.Name == "boardWidth")
                {
                    myBoardGame.setWidth(Convert.ToInt32(node.InnerText.ToString()));
                }
                else if (node.Name == "boardHeight")
                {
                    myBoardGame.setHeight(Convert.ToInt32(node.InnerText.ToString()));
                }
                else if (node.Name == "typeBoard")
                {
                    myBoardGame.setTypeBoard(node.InnerText.ToString());
                }
                else if (node.Name == "listTableBoard")
                {
                    foreach (XmlNode subnode in node.ChildNodes)
                    {
                        if (subnode.Name == "string")
                        {
                            string[] separators = { "," };
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
            //    else if (node.Name == "TextColorHtmlSection1")
            //    {
            //        myBoardGame.setColorSectionA(ColorTranslator.FromHtml(node.InnerText.ToString()));
            //    }
            //    else if (node.Name == "TextColorHtmlSection2")
            //    {
            //        myBoardGame.setColorSectionB(ColorTranslator.FromHtml(node.InnerText.ToString()));
            //    }
           }
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            FormLoadSaveMenu form = new FormLoadSaveMenu(myBoardGame);
            form.Show();
            Hide();
            form.Location = this.Location;
        }

        private void btn_otherBoard_Click(object sender, EventArgs e)
        {
            FormLoadSaveMenu form = new FormLoadSaveMenu(myBoardGame);
            form.Show();
            Hide();
            form.Location = this.Location;
        }

        private void rad_makhorse_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewLoadSave.clear();
            dataGridViewLoadSave.loadToDataGridViewLoadBoard("boardatorMoreMap", "Mak Horse");
            
            if (dataGridViewLoadBoard.RowCount > 0) // not empty  save file.
            {
                dataGridViewLoadBoard.Rows[0].Selected = true;  // row 0 selected , in start.
                dataGridViewLoadBoard_CellClick(dataGridViewLoadBoard, null);
            }
        }

        private void rad_makneeb_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewLoadSave.clear();
            dataGridViewLoadSave.loadToDataGridViewLoadBoard("boardatorMoreMap", "Mak Neeb");

            if (dataGridViewLoadBoard.RowCount > 0) // not empty  save file.
            {
                dataGridViewLoadBoard.Rows[0].Selected = true;  // row 0 selected , in start.
                dataGridViewLoadBoard_CellClick(dataGridViewLoadBoard, null);
            }
        }

        private void rad_all_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewLoadSave.clear();
            dataGridViewLoadSave.loadToDataGridViewLoadBoard("boardatorMoreMap", "Mak Horse");
            dataGridViewLoadSave.loadToDataGridViewLoadBoard("boardatorMoreMap", "Mak Neeb");

            if (dataGridViewLoadBoard.RowCount > 0) // not empty  save file.
            {
                dataGridViewLoadBoard.Rows[0].Selected = true;  // row 0 selected , in start.
                dataGridViewLoadBoard_CellClick(dataGridViewLoadBoard, null);
            }
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
