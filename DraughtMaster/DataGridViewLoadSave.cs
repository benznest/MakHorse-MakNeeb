using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DraughtMaster
{
    public class DataGridViewLoadSave
    {
        DataGridView dataGridViewLoadBoard;

        public DataGridViewLoadSave(DataGridView dataGridViewLoadBoard)
        {
            this.dataGridViewLoadBoard = dataGridViewLoadBoard;
        }

        public void clear(){
            dataGridViewLoadBoard.Rows.Clear();
            dataGridViewLoadBoard.Refresh();
        }

        public void setupDataGridViewLoadBoard()
        {
            dataGridViewLoadBoard.ColumnCount = 4;
            dataGridViewLoadBoard.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewLoadBoard.RowHeadersVisible = false;
            dataGridViewLoadBoard.ColumnHeadersDefaultCellStyle.Font =
            new Font("Arial", 12F);

            dataGridViewLoadBoard.DefaultCellStyle.Font = new Font("Arial", 8.5F);
            dataGridViewLoadBoard.MultiSelect = false;
            dataGridViewLoadBoard.AllowUserToResizeRows = false;
            dataGridViewLoadBoard.AllowUserToResizeColumns = false;
            dataGridViewLoadBoard.ReadOnly = true;
            dataGridViewLoadBoard.AllowUserToAddRows = false;
            dataGridViewLoadBoard.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            dataGridViewLoadBoard.Columns[0].Name = "Game";
            dataGridViewLoadBoard.Columns[1].Name = "Name";
            dataGridViewLoadBoard.Columns[2].Name = "Size";
            dataGridViewLoadBoard.Columns[3].Name = "Date";

            dataGridViewLoadBoard.Columns[0].Width = 80;
            dataGridViewLoadBoard.Columns[1].Width = 210;
        }

        public void loadToDataGridViewLoadBoard(string namefolder)
        {
            loadToDataGridViewLoadBoard(namefolder,"");
        }

        public void loadToDataGridViewLoadBoard(string namefolder,string typeGame)
        {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + namefolder;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            DirectoryInfo d = new DirectoryInfo(path);
            foreach (var file in d.GetFiles("*.xml"))
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.Name);
                fetchDataAndPutToDataGridView(path + "\\" + file.Name, fileNameWithoutExtension, file.LastWriteTime,typeGame);
            }

        }

        public void fetchDataAndPutToDataGridView(string pathXML, string fileName, DateTime dateTimeFile,string typeGame)
        {
            //MessageBox.Show(pathXML);
            XmlDocument doc = new XmlDocument();
            doc.Load(pathXML);

            // Declare data for store.
            int boardWidth = 0;
            int boardHeight = 0;
            int rowItem = 0; ;
            bool flagAI = true;
            string typeBoard = "";
            string player = "vs Computer"; // for store string AI , or Player 2 display.

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.Name == "typeBoard")
                {
                    typeBoard = node.InnerText;
                    //MessageBox.Show(node.Attributes["boardWidth"].InnerText);
                }
                else if (node.Name == "boardWidth")
                {
                    boardWidth = Convert.ToInt32(node.InnerText);
                    //MessageBox.Show(node.Attributes["boardWidth"].InnerText);
                }
                else if (node.Name == "boardHeight")
                {
                    boardHeight = Convert.ToInt32(node.InnerText);
                }
                else if (node.Name == "rowItem")
                {
                    rowItem = Convert.ToInt32(node.InnerText);
                }
                else if (node.Name == "flag_AI")
                {
                    flagAI = Convert.ToBoolean(node.InnerText);
                    player = "vs Player2";
                    if (flagAI)
                    {
                        player = "vs Computer";
                    }
                }
            }
            if (typeGame == typeBoard || typeGame == "") // empty string is not specify.
            {
                string[] row = { typeBoard, fileName, boardWidth + " x " + boardHeight, dateTimeFile.ToShortDateString() };
                dataGridViewLoadBoard.Rows.Add(row);
            }
        }
    }
}
