using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WiBit.Net;

namespace TicTacToe
{
    public partial class form_ticTacToe : Form
    {
        private int _scoreWiBit = 0, _scoreBuzz = 0;
        private TicTacToeModel _ttt = new TicTacToeModel();
        public form_ticTacToe()
        {
            InitializeComponent();
        }

        private void form_ticTacToe_Load(object sender, EventArgs e)
        {
            resetBoard();
        }

        private void resetBoard()
        {
            _ttt.Reset();

            for(int i = 1; i < 10; i++)
            {
                string picBoxXName = "pictureBox" + i + "_x";
                string picBoxOName = "pictureBox" + i + "_o";
                string picBoxBlankName = "pictureBox" + i + "_blank";

                FieldInfo picBoxXField = this.GetType().GetField(picBoxXName);
                PictureBox picBoxX = (PictureBox)picBoxXField.GetValue(this);
                picBoxX.Visible = false;

                FieldInfo picBoxOField = this.GetType().GetField(picBoxOName);
                PictureBox picBoxO = (PictureBox)picBoxOField.GetValue(this);
                picBoxO.Visible = false;

                FieldInfo picBoxBlankField = this.GetType().GetField(picBoxBlankName);
                PictureBox picBoxBlank = (PictureBox)picBoxBlankField.GetValue(this);
                picBoxBlank.Visible = true;
            }

            pictureBox_frogAsleep.Visible = false;
            pictureBox_frogAwake.Visible = true;

            pictureBox_flyAsleep.Visible = true;
            pictureBox_flyAwake.Visible = false;
        }

        private void pictureBox_blank_Click(object sender, EventArgs e)
        {
            PictureBox selectedPictureBox = (PictureBox)sender;
            int selectedNumber = int.Parse(
                    selectedPictureBox.Name.Split('_')[0].Replace("pictureBox", "")
                );

            int rowNumber = -1, colNumber = -1;

            switch(selectedNumber)
            {
                case 1: rowNumber = 0; colNumber = 0; break;
                case 2: rowNumber = 0; colNumber = 1; break;
                case 3: rowNumber = 0; colNumber = 2; break;

                case 4: rowNumber = 1; colNumber = 0; break;
                case 5: rowNumber = 1; colNumber = 1; break;
                case 6: rowNumber = 1; colNumber = 2; break;

                case 7: rowNumber = 2; colNumber = 0; break;
                case 8: rowNumber = 2; colNumber = 1; break;
                case 9: rowNumber = 2; colNumber = 2; break;
            }

            string pictureBoxName = "pictureBox" + selectedNumber + "_"
                + _ttt.WhoseTurn().ToString().ToLower();

            FieldInfo targetPictureBoxField = this.GetType().GetField(pictureBoxName);
            PictureBox targetPictureBox = (PictureBox)targetPictureBoxField.GetValue(this);
            targetPictureBox.Visible = true;
            selectedPictureBox.Visible = false;

            _ttt.MakeMove(colNumber, rowNumber);

            if(_ttt.WhoIsWinner() != ' ')
            {
                switch(_ttt.WhoIsWinner())
                {
                    case 'X':
                        _scoreWiBit++;
                        label_wibitScore.Text = _scoreWiBit.ToString();
                        MessageBox.Show("The Winner is \"WiBit The Frog!\"");
                        break;
                    case 'O':
                        _scoreBuzz++;
                        label_buzzScore.Text = _scoreBuzz.ToString();
                        MessageBox.Show("The Winner is \"Buzz The Fly!\"");
                        break;
                    default: MessageBox.Show("It's a DRAW!!!"); break;
                }
                resetBoard();
            }
            else
            {
                if(_ttt.WhoseTurn() == 'X')
                {
                    pictureBox_frogAwake.Visible = true;
                    pictureBox_frogAsleep.Visible = false;

                    pictureBox_flyAwake.Visible = false;
                    pictureBox_flyAsleep.Visible = true;
                }
                else
                {
                    pictureBox_frogAwake.Visible = false;
                    pictureBox_frogAsleep.Visible = true;

                    pictureBox_flyAwake.Visible = true;
                    pictureBox_flyAsleep.Visible = false;
                }
            }
        }

    }
}
