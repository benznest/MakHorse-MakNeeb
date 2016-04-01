using DraughtMaster.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DraughtMaster
{
    public partial class FormMoreMap : Form
    {
        
        
        /// <summary>
        ///  
        /// </summary>
        
        
        
        MyBoardGame myBoardGame;
        DataGridViewLoadSave dataGridViewLoadSave;
        PreviewBoard previewBoard;

        int boardWidth = 0;
        int boardHeight = 0;

        Color colorA;
        Color colorB;

        public FormMoreMap()
        {
            InitializeComponent();

        }

        public FormMoreMap(MyBoardGame myBoardGame)
        {
            InitializeComponent();
            this.myBoardGame = myBoardGame;

            // add event to DGV.
            dataGridViewLoadBoard.CellClick += new DataGridViewCellEventHandler(dataGridViewLoadBoard2_CellClick);
            dataGridViewLoadSave = new DataGridViewLoadSave(dataGridViewLoadBoard); // put accual object DGV to class.
            dataGridViewLoadSave.setupDataGridViewLoadBoard();  // setup default detail in DGV.
            dataGridViewLoadSave.loadToDataGridViewLoadBoard("boardatorMoreMap"); // Load xml to dataGridView.

            // Create PreviewBoard.
            previewBoard = new PreviewBoard(panel_previewBoard,myBoardGame); // put accual object Panel to PreviewBoard object.
            previewBoard.createPanelPreviewBoard(); // create section all.
            previewBoard.setFolderXML("boardatorMoreMap");
            previewBoard.refreshColorBoard(); // put colour in Preview board.

            if (dataGridViewLoadBoard.RowCount > 0) // not empty  save file.
            {
                dataGridViewLoadBoard.Rows[0].Selected = true;  // row 0 selected , in start.
                dataGridViewLoadBoard2_CellClick(dataGridViewLoadBoard, null);
            }
        }

        private void dataGridViewLoadBoard2_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void FormMoreMap_Load(object sender, EventArgs e)
        {

        }



        private void btn_play_Click(object sender, EventArgs e)
        {
            // Save all data setting to Object.
            //saveSettingToObjectMyGame();
            string size = "";
            foreach (DataGridViewRow row in dataGridViewLoadBoard.SelectedRows)
            {
                setXMLToObjectGame(row.Cells[1].Value.ToString()); // name file is keyword.
                size = row.Cells[2].Value.ToString();  // size
            }

            //myBoardGame.setColorSection(colorSectionA, colorSectionB);

            if (size == "8 x 8")
            {
                FormPlay8x8 form = new FormPlay8x8(myBoardGame);
                form.Show();
                Hide();
                form.Location = this.Location;
            }
        }

        private void setXMLToObjectGame(string namefile)
        {
            string namefolder = previewBoard.getFolderXML();
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\"+namefolder+"\\" + namefile + ".xml";

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
                if (node.Name == "boardWidth")
                {
                    myBoardGame.setWidth(Convert.ToInt32(node.InnerText.ToString()));
                }
                else if (node.Name == "boardHeight")
                {
                    myBoardGame.setHeight(Convert.ToInt32(node.InnerText.ToString()));
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
                else if (node.Name == "TextColorHtmlSection1")
                {
                    myBoardGame.setColorSectionA(ColorTranslator.FromHtml(node.InnerText.ToString()));
                }
                else if (node.Name == "TextColorHtmlSection2")
                {
                    myBoardGame.setColorSectionB(ColorTranslator.FromHtml(node.InnerText.ToString()));
                }
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            FormSelectBoardSize form = new FormSelectBoardSize(myBoardGame);
            form.Show();
            Hide();
            form.Location = this.Location;
        }
    }
}
