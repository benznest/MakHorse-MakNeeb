using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DraughtMaster.FormTutorial.MakNeeb
{
    public partial class FormTutorialMakNeeb : MetroFramework.Forms.MetroForm
    {
        BoardNeeb myBoard;
        TutorialMakNeeb tutorial;
        MyBoardGame myBoardGame;
        int chapter = 1;

        private void FormTutorialMakHorseChapter1_Load(object sender, EventArgs e)
        {
            
        }

        public FormTutorialMakNeeb()
        {
            InitializeComponent();
            myBoardGame = new MyBoardGame();
            tutorial = new TutorialMakNeeb();
            setDataTutorialForm();
            chapter = 1;
            
        }

        public FormTutorialMakNeeb(MyBoardGame myBoardGame)
        {
            InitializeComponent();
            this.myBoardGame = myBoardGame;
            tutorial = new TutorialMakNeeb();
            setDataTutorialForm();
            chapter = 1;

        }

        private FormTutorialMakNeeb(MyBoardGame myBoardGame, TutorialMakNeeb tutorial,int chapter)
        {
            InitializeComponent();
            myBoardGame.clearData();
            this.chapter = chapter;
            this.tutorial = tutorial;
            this.myBoardGame = myBoardGame;
            setDataTutorialForm();
        }

        private void setDataTutorialForm()
        {

            myBoard = new BoardNeeb(this, Panel_myBorad, 6, 6, false, myBoardGame);

            // tutorial.
            myBoard.setFlagTutorial(true);
            myBoard.setButtonTutorialNextChapter(btn_nextChapter);

            if (chapter == 5)
            {
                btn_nextChapter.Text = "Perfect !!";
                btn_next.Enabled = false;
            }


            //((BoardPinch)myBoard).setFlagToDefault();
            myBoard.setFlagForcedKill(true);

            //set Color Panel Start Board.
            myBoard.setColorPanelBoardSection(Color.Black,Color.White);
            myBoard.refreshColorOnBoard();
            myBoard.setTurnPlayerStart(2); // set start player turn.

            // setup item player on Board.
            myBoard.createObjectItem(5, 5);
            setupMap();
            myBoard.setCounterItemPlayer();

            lbl_header.Text = "Chapter " + chapter + " : " + tutorial.getTopic(chapter);
            lbl_topic.Text = tutorial.getTopic(chapter);
            lbl_detail.Text = tutorial.getDetail(chapter);
        }

        int count_player1 = 0;
        int count_player2 = 0;

        private void setupMap()
        {
            List<Tuple<int, int, int, int>> item = tutorial.getItem(chapter);
            foreach (Tuple<int,int,int,int> element in item)
            {
                int player = element.Item1;
                int status = element.Item2;
                int id_row = element.Item3;
                int id_col = element.Item4;

                createItem(player, status, id_row, id_col);
            }
        }

        private void createItem(int player,int status,int id_row,int id_col)
        {
            int count = 0;
            if (player == 1)
            {
                count = count_player1;
            }
            else if(player == 2)
            {
                count = count_player2;
            }

            myBoard.createItem(count, player, status);
            myBoard.setItemToPanelSection(id_row, id_col, player, myBoard.getItemObject(count, player));
            myBoard.getItemObject(count, player).setIdRowAndCol(id_row, id_col);

            if (player == 1)
            {
                count_player1++;
            }
            else if (player == 2)
            {
                count_player2++;
            }
        }

        private void FormTutorialMakNeeb_Load(object sender, EventArgs e)
        {

        }

        private void btn_nextChapter_Click(object sender, EventArgs e)
        {
            // next chapter
            if (chapter < 5)
            {
                FormTutorialMakNeeb form = new FormTutorialMakNeeb(myBoardGame, tutorial, chapter + 1);
                form.Show();
                Hide();
            }
            else if (chapter == 5)
            {
                FormHowToPlay form = new FormHowToPlay(myBoardGame);
                form.Show();
                Hide();
            }
        }

        private void btn_tryagian_Click(object sender, EventArgs e)
        {
            FormTutorialMakNeeb form = new FormTutorialMakNeeb(myBoardGame, tutorial, chapter);
            form.Show();
            Hide();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            if (chapter > 1)
            {
                FormTutorialMakNeeb form = new FormTutorialMakNeeb(myBoardGame, tutorial, chapter - 1);
                form.Show();
                Hide();
            }
            else if (chapter == 1)
            {
                FormHowToPlay form = new FormHowToPlay(myBoardGame);
                form.Show();
                Hide();
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (chapter < 5)
            {
                FormTutorialMakNeeb form = new FormTutorialMakNeeb(myBoardGame, tutorial, chapter + 1);
                form.Show();
                Hide();
            }
        }

        private void btn_backToMenu_Click(object sender, EventArgs e)
        {
            FormHowToPlay form = new FormHowToPlay(myBoardGame);
            form.Show();
            Hide();
        }
    }
}
