using DraughtMaster.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DraughtMaster
{
    public class PreviewBoard
    {
        public Panel[,] previewBoardSection;
        public Panel parentPanel;
        public PictureBox[,] previewPictureItemBoardSection;
        public int boardWidth = 0;
        public int boardHeight = 0;
        public string namefolder ="";

        Color colorSectionA = Color.White;
        Color colorSectionB = Color.White;
        MyBoardGame myBoardGame;

        string namePictureItemRegularPlayer1 = "";
        string namePictureItemRegularPlayer2 = "";
        string namePictureItemSuperPlayer1 = "";
        string namePictureItemSuperPlayer2 = "";



        public PreviewBoard()
        {
            
        }

        public PreviewBoard(Panel parentPanel)
        {
            this.parentPanel = parentPanel;
        }

        public PreviewBoard(Panel parentPanel, MyBoardGame myBoardGame)
        {
            this.parentPanel = parentPanel;
            this.myBoardGame = myBoardGame;

            namePictureItemRegularPlayer1 = myBoardGame.getNamePictureItemRegular(1);
            namePictureItemRegularPlayer2 = myBoardGame.getNamePictureItemRegular(2);
            namePictureItemSuperPlayer1 = myBoardGame.getNamePictureItemSuper(1);
            namePictureItemSuperPlayer2 = myBoardGame.getNamePictureItemSuper(2);
        }

        public void setNamePicturItem(string namePictureItemRegularPlayer1, string namePictureItemRegularPlayer2, string namePictureItemSuperPlayer1, string namePictureItemSuperPlayer2)
        {
            this.namePictureItemRegularPlayer1 = namePictureItemRegularPlayer1;
            this.namePictureItemRegularPlayer2 = namePictureItemRegularPlayer2;
            this.namePictureItemSuperPlayer1 = namePictureItemSuperPlayer1;
            this.namePictureItemSuperPlayer2 = namePictureItemSuperPlayer2;
        }

        public void setColorSection(Color A, Color B)
        {
            colorSectionA = A;
            colorSectionB = B;
        }

        public void setColorSectionA(Color A)
        {
            colorSectionA = A;
        }

        public void setColorSectionB(Color B)
        {
            colorSectionB = B;
        }

        public Color getColorSectionA()
        {
            return colorSectionA;
        }

        public Color getColorSectionB()
        {
            return colorSectionB;
        }


        public void setFolderXML(string namefolder)
        {
            this.namefolder = namefolder;
        }

        public string getFolderXML()
        {
            return namefolder;
        }

        public void createPanelPreviewBoard() //
        {
            previewBoardSection = new Panel[12, 12];
            previewPictureItemBoardSection = new PictureBox[12, 12];
            int x = 33, y = 33;
            int w = 30, h = 30;
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    previewBoardSection[i, j] = new Panel();
                    parentPanel.Controls.Add(previewBoardSection[i, j]);
                    previewBoardSection[i, j].Location = new Point(x, y);
                    previewBoardSection[i, j].Width = w;
                    previewBoardSection[i, j].Height = h;
                    x += w;

                    // Create pictureBox and add to panelSection Preview.
                    previewPictureItemBoardSection[i, j] = new PictureBox();
                    previewBoardSection[i, j].Controls.Add(previewPictureItemBoardSection[i, j]);
                   
                }
                x = 33;
                y += h;
            }
        }

        public void setPicturetoSection(int i, int j, object img)
        {
            previewPictureItemBoardSection[i, j].Location = new Point(2, 2);
            previewPictureItemBoardSection[i, j].Size = new System.Drawing.Size(25, 25);
            previewPictureItemBoardSection[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
            previewPictureItemBoardSection[i, j].Image = (Image)img;
        }

        // choose section in fill color.
        public void refreshColorBoard(string section)
        {
            if (section == "A")
            {
                changeColorPreviewPanel_A();
            }
            else if (section == "B")
            {
                changeColorPreviewPanel_B();
            }
        }

        // color ALL
        public void refreshColorBoard()
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (i % 2 == 0 && j % 2 == 0)
                    {
                        previewBoardSection[i, j].BackColor = colorSectionA;
                    }
                    else if (i % 2 != 0 && j % 2 != 0)
                    {
                        previewBoardSection[i, j].BackColor = colorSectionA;
                    }
                    else if (i % 2 == 0 && j % 2 != 0)
                    {
                        previewBoardSection[i, j].BackColor = colorSectionB;
                    }
                    else if (i % 2 != 0 && j % 2 == 0)
                    {
                        previewBoardSection[i, j].BackColor = colorSectionB;
                    }
                }
            }
        }

        public void changeColorPreviewPanel_A()
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (i % 2 == 0 && j % 2 == 0)
                    {
                        previewBoardSection[i, j].BackColor = colorSectionA;
                    }
                    else if (i % 2 != 0 && j % 2 != 0)
                    {
                        previewBoardSection[i, j].BackColor = colorSectionA;
                    }
                }
            }
        }

        public void changeColorPreviewPanel_B()
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {

                    if (i % 2 == 0 && j % 2 != 0)
                    {
                        previewBoardSection[i, j].BackColor = colorSectionB;
                    }
                    else if (i % 2 != 0 && j % 2 == 0)
                    {
                        previewBoardSection[i, j].BackColor = colorSectionB;
                    }
                }
            }
        }

        public void changeVisiblePreviewPanel()
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (i < boardHeight && j < boardWidth)
                    {
                        previewBoardSection[i, j].Visible = true;
                    }
                    else
                    {
                        previewBoardSection[i, j].Visible = false;
                    }
                }
            }
        }

        public void changeVisiblePreviewPanel(int width, int height)
        {
            if (width == boardWidth && height == boardHeight)
            {
                return;
            }

            boardWidth = width;
            boardHeight = height;
            clearPictureItemPreview(); // clear picture.
            changeVisiblePreviewPanel();

            
        }

        public void clearPictureItemPreview()
        {
            foreach (PictureBox p in previewPictureItemBoardSection)
            {
                p.Image = null;
            }
        }

        public void setPreviewFromXML(string namefile)
        {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\"+namefolder+"\\" + namefile + ".xml";

            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.Name == "boardWidth")
                {
                    boardWidth = Convert.ToInt32(node.InnerText);
                }
                else if (node.Name == "boardHeight")
                {
                    boardHeight = Convert.ToInt32(node.InnerText);
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

                            int i = Convert.ToInt32(words[0]);
                            int j = Convert.ToInt32(words[1]);
                            int player = Convert.ToInt32(words[2]);
                            int status = Convert.ToInt32(words[3]);
                            Console.Write("" + player);
                            if (player > 0) // have item.
                            {
                                setItemtoPreviewBoard(i, j, player, status);
                            }
                        }
                    }
                }
                else if (node.Name == "TextColorHtmlSection1")
                {
                    colorSectionA = ColorTranslator.FromHtml(node.InnerText.ToString());
                }
                else if (node.Name == "TextColorHtmlSection2")
                {
                    colorSectionB = ColorTranslator.FromHtml(node.InnerText.ToString());
                }
                else if (node.Name == "index_picPlayer1")
                {
                    namePictureItemRegularPlayer1 = "walker_" + Convert.ToInt32(node.InnerText.ToString());
                    namePictureItemSuperPlayer1   = "walker_super_" + Convert.ToInt32(node.InnerText.ToString());
                }
                else if (node.Name == "index_picPlayer2")
                {
                    namePictureItemRegularPlayer2 = "walker_" + Convert.ToInt32(node.InnerText.ToString());
                    namePictureItemSuperPlayer2   = "walker_super_" + Convert.ToInt32(node.InnerText.ToString());
                }
            }
        }

        private void setItemtoPreviewBoard(int i, int j, int player, int status)
        {
            object img = null;
            if (player == 1)
            {
                if (status == 1)
                {
                    img = Resources.ResourceManager.GetObject(namePictureItemRegularPlayer1);
                }
                else if(status == 2)
                {
                    img = Resources.ResourceManager.GetObject(namePictureItemSuperPlayer1);
                }

            }
            else if (player == 2)
            {
                if (status == 1)
                {
                    img = Resources.ResourceManager.GetObject(namePictureItemRegularPlayer2);
                }
                else if (status == 2)
                {
                    img = Resources.ResourceManager.GetObject(namePictureItemSuperPlayer2);
                }
            }

            setPicturetoSection(i, j, img);
        }

    }
}
