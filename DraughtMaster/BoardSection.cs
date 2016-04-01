using DraughtMaster.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DraughtMaster
{
    public class BoardSection
    {
        // size of board.
        //private int row;
        //private int col;
        private MyBoardGame myBoardGame;
        private int typeBoardSection;
        private int id_row;
        private int id_col;
        private bool hasItem;
        private int player;
        private Board boardParent;  // pointer to Parent Board.
        private object item;         // item in section.
        private Panel panelSection;
        private PictureBox pictureBoxSection;
        private int sizePictureItem;

        // Constructor.
        public BoardSection()
        {
            hasItem = false;
            player = 0; // not have player in section.
        }

        public BoardSection(int id_row, int id_col, int typeBoard, Board parent,MyBoardGame myBoardGame)
        {
            this.myBoardGame = myBoardGame;
            this.boardParent = parent;
            this.typeBoardSection = typeBoard;
            this.id_row = id_row;
            this.id_col = id_col;
            hasItem = false;
            player = 0; // not have player in section.
            this.sizePictureItem = 64;
        }

        public BoardSection(int id_row, int id_col, int typeBoard)
        {
            this.typeBoardSection = typeBoard;
            this.id_row = id_row;
            this.id_col = id_col;
            hasItem = false;
            player = 0; // not have player in section.
        }

        // for manage graphic appropiate board size differ.
        //public void setSizeBoard(int row,int col)
        //{
        //    this.row = row;
        //    this.col = col;
        //}

        // for manage graphic appropiate board size differ.
        public void setSizePictureItem(int size)
        {
            sizePictureItem = size;
        }

        // create PictureBox or image into PanelSection.
        public void createPictureBoxInPanelSection()
        {
            pictureBoxSection = new PictureBox();
            setPictureBoxSection(); // set value PictureBox.
            panelSection.Controls.Add(pictureBoxSection); // Put PictureBox to panel.
            pictureBoxSection.Click += new EventHandler(pictureBoxSection_Click); // Add event to pictureBox.
        }

        // Event when click pictureBox in PanelSection.
        public void pictureBoxSection_Click(object sender, EventArgs e)
        {
            if (typeBoardSection == (int)type.Draught)
            {
                ((BoardHorse)boardParent).runEvent(id_row, id_col);
            }
            else if (typeBoardSection == (int)type.PinchDraught)
            {
                ((BoardNeeb)boardParent).runEvent(id_row, id_col);
            }
        }

        // set value default PictureBox.
        public void setPictureBoxSection()
        {
            pictureBoxSection.Location = new Point(3, 3);
            pictureBoxSection.Size = new System.Drawing.Size(sizePictureItem, sizePictureItem);
            pictureBoxSection.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        // Set Panel Object to panelSection. for access PanelSection.
        public void setPanelSection(Panel panel)
        {
            panelSection = panel;
        }

        // get object PanelSection.
        public Panel getObjectPanelSection()
        {
            return panelSection;
        }

        // set item to panelSection. 
        public void setItemToPanelSection(int player, object item)
        {
            this.item = item;
            setPlayerHolder(player);
            if (((Item)item).getStatusItem() == 1)
            {
                setImageRegularItemToPictureBoxSection(player);
            }
            else if (((Item)item).getStatusItem() == 2)
            {
                setImageSuperItemToPictureBoxSection(player);
            }
            
        }

        // get item Player from Section.
        public object getItemFromPanelSection()
        {
            return item;
        }

        // remove item from section.
        public void removeItemFromPanelSection()
        {
            item = null;
        }

        public void removeImage()
        {
            pictureBoxSection.Image = null;
        }

        // set image player to PictureBox from player.
        public void setImageRegularItemToPictureBoxSection(int player)
        {
            if (player == 1)
            {
                object img = Resources.ResourceManager.GetObject(myBoardGame.getNamePictureItemRegular(1));
                pictureBoxSection.Image = (Image)img;
            }
            else if (player == 2)
            {
                object img = Resources.ResourceManager.GetObject(myBoardGame.getNamePictureItemRegular(2));
                pictureBoxSection.Image = (Image)img;
            }
            else
            {
                pictureBoxSection.Image = null;
            }
        }

        // set image player to PictureBox from player.
        public void setImageSuperItemToPictureBoxSection(int player)
        {
            if (player == 1)
            {
                object img = Resources.ResourceManager.GetObject(myBoardGame.getNamePictureItemSuper(1));
                pictureBoxSection.Image = (Image)img;
            }
            else if (player == 2)
            {
                object img = Resources.ResourceManager.GetObject(myBoardGame.getNamePictureItemSuper(2));
                pictureBoxSection.Image = (Image)img;
            }
            else
            {
                pictureBoxSection.Image = null;
            }
        }


        // set player holder PanelSection.
        public void setPlayerHolder(int player)
        {
            this.player = player;
            if (player > 0)
            {
                hasItem = true;
            }
            else
            {
                hasItem = false;
            }
        }

        // get player.
        public int getPlayerHolder()
        {
            return player;
        }

        public bool hasItemInSection()
        {
            return hasItem;
        }

        public void setFlagItemInSection(bool flag)
        {
            hasItem = flag;
        }
    }
}
