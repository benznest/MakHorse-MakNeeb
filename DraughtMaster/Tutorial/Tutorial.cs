using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraughtMaster
{
    public class Tutorial
    {
        protected int numChapter;
        protected string[] topic;
        protected string[] detail;
        protected string[] goal;
        protected List<Tuple<int, int, int, int>>[] item;


        public string getTopic(int index)
        {
            return topic[index - 1];
        }

        public string getDetail(int index)
        {
            return detail[index - 1];
        }

        public string getGoal(int index)
        {
            return goal[index - 1];
        }

        public List<Tuple<int, int, int, int>> getItem(int index)
        {
            return item[index - 1];
        }
    }
}
