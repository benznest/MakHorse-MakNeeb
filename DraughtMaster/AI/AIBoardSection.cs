using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraughtMaster
{
    public class AIBoradSection
    {
        protected int id_row;
        protected int id_col;
        protected int player;
        protected int status;
        // Horse : 0 is dead,1 is regular , 2 is super.
        // Pinch : 0 is dead,1 is regular 

        public AIBoradSection()
        {

        }

        public AIBoradSection(int id_row, int id_col, int player, int status)
        {
            this.id_row = id_row;
            this.id_col = id_col;
            this.player = player;
            this.status = status;
        }

        public AIBoradSection cloneAIBoardHorseSection()
        {
            return (AIBoradSection)this.MemberwiseClone();
        }

        public bool isEmpty()
        {
            if (player == 0)
            {
                return true;
            }
            return false;
        }

        public void setRowCol(int id_row, int id_col)
        {
            this.id_row = id_row;
            this.id_col = id_col;
        }

        public void setPlayer(int player)
        {
            this.player = player;
        }

        public void setStatus(int status)
        {
            this.status = status;
        }

        public int getPlayerHolder()
        {
            return player;
        }

        public int getStatus()
        {
            return status;
        }
    }

}
