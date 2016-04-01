using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DraughtMaster
{
    public class Item
    {
        public enum statusItem :int { NotBorn = -1,Dead =0, Regular =1, Super=2 };
        public int id_player;
        public int status;
        public int id_row;
        public int id_col;

        public Item()
        {
            status = 1;
        }

        public Item(int player,int myStatus)
        {
            id_player = player;
            status = myStatus;
        }

        public Item clone()
        {
            return (Item)this.MemberwiseClone();
        }

        public void setPlayer(int id_player,int myStatus)
        {
            this.id_player = id_player;
            status = (int)statusItem.Regular;
        }

        public int getPlayer()
        {
            return id_player;
        }

        public void setStatusItem(int myStatus)
        {
            status = myStatus;
        }

        public int getStatusItem()
        {
            return status;
        }

        public bool isSuperItem()
        {
            return status == 2;
        }

        public bool isRegularItem()
        {
            return status == 1;
        }

        public void setIdRowAndCol(int id_row,int id_col)
        {
            this.id_row = id_row;
            this.id_col = id_col;
        }

        public void clearIdRowAndCol()
        {
            id_row = id_col = -1;
        }

        public int getIdRow()
        {
            return id_row;
        }

        public int getIdCol()
        {
            return id_col;
        }


    }
}
