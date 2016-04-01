using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraughtMaster
{
    class TutorialMakHorse : Tutorial
    {

        public TutorialMakHorse()
        {
            setDataAndInitialize();
        }

        private void setDataAndInitialize()
        {
            numChapter = 5;
            topic = new string[numChapter];
            detail = new string[numChapter];
            goal = new string[numChapter];
            item = new List<Tuple<int, int, int, int>>[numChapter];
            setTopic();
            setDetail();
            setItem();
        }

        private void setTopic()
        {
            topic[0] = "Simple Move";
            topic[1] = "Simple Kill";
            topic[2] = "Double Kill";
            topic[3] = "Upgrade";
            topic[4] = "Super Kill";
        }

        private void setDetail()
        {
            detail[0] = "This is regular item. \r\nClick item for show your path \r\nthat can move.\r\n\r\nTry move it to new location.";
            detail[1] = "This is Kill mission. \r\nClick item for show your path \r\nwhen you have chance kill enemy\r\n\r\nTry move and kill enemy.";
            detail[2] = "When you have chance kill 2 enemy item \r\nin one turn.\r\nClick item for show your path \r\n\r\nTry move and double kill enemy.";
            detail[3] = "When you move to final Board \r\nYou can upgrade to Horse \r\nfor kill more easy. \r\n\r\nTry move to final Board nd upgrade item.";
            detail[4] = "You has Horse ,Super item \r\nSuper Kill is easy for you. \r\n\r\nTry super Kill with your enemy.";
        }

        private void setGoal()
        {
            goal[0] = "walk";
            goal[1] = "kill";
            goal[2] = "kill";
            goal[3] = "walk";
            goal[4] = "kill";
        }

        private void addElementToList(List<Tuple<int, int, int, int>> list,int player,int status,int id_row,int id_col)
        {
            list.Add(new Tuple<int, int, int, int>( player,status, id_row, id_col));
        }

        private void setItem()
        {
            // chapter 1.
            List<Tuple<int, int, int, int>> itemChapter1 = new List<Tuple<int,int,int,int>>();
            addElementToList(itemChapter1, 2 , 1 , 3 , 3);
            item[0] = itemChapter1;

            // chapter 2.
            List<Tuple<int, int, int, int>> itemChapter2 = new List<Tuple<int, int, int, int>>();
            addElementToList(itemChapter2, 2, 1, 4, 2);
            addElementToList(itemChapter2, 1, 1, 3, 3);
            item[1] = itemChapter2;

            // chapter 3.
            List<Tuple<int, int, int, int>> itemChapter3 = new List<Tuple<int, int, int, int>>();
            addElementToList(itemChapter3, 2, 1, 5, 3);
            addElementToList(itemChapter3, 1, 1, 4, 2);
            addElementToList(itemChapter3, 1, 1, 2, 2);
            item[2] = itemChapter3;

            // chapter 4.
            List<Tuple<int, int, int, int>> itemChapter4 = new List<Tuple<int, int, int, int>>();
            addElementToList(itemChapter4, 2, 1, 1, 3);
            item[3] = itemChapter4;

            // chapter 5.
            List<Tuple<int, int, int, int>> itemChapter5 = new List<Tuple<int, int, int, int>>();
            addElementToList(itemChapter5, 2, 2, 0, 2);
            addElementToList(itemChapter5, 1, 1, 2, 4);
            addElementToList(itemChapter5, 1, 1, 4, 4);
            addElementToList(itemChapter5, 1, 1, 3, 1);
            item[4] = itemChapter5;
        }


    }
}
