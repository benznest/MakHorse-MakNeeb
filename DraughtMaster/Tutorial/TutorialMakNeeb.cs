using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraughtMaster
{
    class TutorialMakNeeb : Tutorial
    {

        public TutorialMakNeeb()
        {
            setDataAndInitialize();
        }

        private void setDataAndInitialize()
        {
            numChapter = 5;
            topic = new string[numChapter];
            detail = new string[numChapter];
            item = new List<Tuple<int, int, int, int>>[numChapter];
            setTopic();
            setDetail();
            setItem();
        }

        private void setTopic()
        {
            topic[0] = "Simple Move";
            topic[1] = "Stab";
            topic[2] = "Outflank";
            topic[3] = "Corner";
            topic[4] = " Godlike !! ";
        }

        private void setDetail()
        {
            detail[0] = "Click item for show your path \r\nthat can move.\r\n\r\nTry move it to new location.";
            detail[1] = "Stab is easy kill , 2 item enemy. \r\n\r\nTry Stab enemy.";
            detail[2] = "Outflank is perfect kill  \r\nSometine will kill more item \r\nby you have only 2 item \r\n\r\n Try Outflank kill!!";
            detail[3] = "Corner is kill enemy on corner board \r\n\r\nTry corner kill.";
            detail[4] = "Godelike is ultimate of kill \r\nif you can it. you are hero.\r\n\r\nTry Godlike with enemy.";
        }

        private void addElementToList(List<Tuple<int, int, int, int>> list, int player, int status, int id_row, int id_col)
        {
            list.Add(new Tuple<int, int, int, int>(player, status, id_row, id_col));
        }

        private void setItem()
        {
            // chapter 1.
            List<Tuple<int, int, int, int>> itemChapter1 = new List<Tuple<int, int, int, int>>();
            addElementToList(itemChapter1, 2, 1, 3, 3);
            item[0] = itemChapter1;

            // chapter 2.
            List<Tuple<int, int, int, int>> itemChapter2 = new List<Tuple<int, int, int, int>>();
            addElementToList(itemChapter2, 2, 1, 4, 3);
            addElementToList(itemChapter2, 1, 1, 0, 2);
            addElementToList(itemChapter2, 1, 1, 0, 4);
            item[1] = itemChapter2;

            // chapter 3.
            List<Tuple<int, int, int, int>> itemChapter3 = new List<Tuple<int, int, int, int>>();
            addElementToList(itemChapter3, 2, 1, 0, 0);
            addElementToList(itemChapter3, 1, 1, 0, 1);
            addElementToList(itemChapter3, 1, 1, 0, 2);
            addElementToList(itemChapter3, 1, 1, 0, 3);
            addElementToList(itemChapter3, 2, 1, 4, 4);
            item[2] = itemChapter3;

            // chapter 4.
            List<Tuple<int, int, int, int>> itemChapter4 = new List<Tuple<int, int, int, int>>();
            addElementToList(itemChapter4, 1, 1, 0, 0);
            addElementToList(itemChapter4, 2, 1, 0, 1);
            addElementToList(itemChapter4, 2, 1, 4, 0);
            item[3] = itemChapter4;

            // chapter 5.
            List<Tuple<int, int, int, int>> itemChapter5 = new List<Tuple<int, int, int, int>>();
            addElementToList(itemChapter5, 2, 1, 5, 3);
            addElementToList(itemChapter5, 2, 1, 3, 0);
            addElementToList(itemChapter5, 1, 1, 3, 1);
            addElementToList(itemChapter5, 1, 1, 3, 2);
            addElementToList(itemChapter5, 1, 1, 3, 4);
            addElementToList(itemChapter5, 2, 1, 3, 5);
            addElementToList(itemChapter5, 1, 1, 2, 3);
            addElementToList(itemChapter5, 1, 1, 1, 3);
            addElementToList(itemChapter5, 2, 1, 0, 3);

            item[4] = itemChapter5;
        }
    }
}
