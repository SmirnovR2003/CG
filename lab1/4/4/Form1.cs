using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _4.Model;

namespace _4
{
    public partial class Form1 : Form
    {
        private Model.Model _model;

        private Dictionary<int, Delegate> _drawGallows = new Dictionary<int, Delegate>
        {
            { 0,  new Func<Graphics, object>(DrawGallowsWithZeroErrorsNumber) },
            { 1,  new Func<Graphics, object>(DrawGallowsWithOneErrorsNumber) },
            { 2,  new Func<Graphics, object>(DrawGallowsWithTwoErrorsNumber) },
            { 3,  new Func<Graphics, object>(DrawGallowsWithThreeErrorsNumber) },
            { 4,  new Func<Graphics, object>(DrawGallowsWithFourErrorsNumber) },
            { 5,  new Func<Graphics, object>(DrawGallowsWithFiveErrorsNumber) },
            { 6,  new Func<Graphics, object>(DrawGallowsWithSixErrorsNumber) },
            { 7,  new Func<Graphics, object>(DrawGallowsWithSevenErrorsNumber) },
        };

        public Form1(Model.Model model)
        {
            InitializeComponent();
            _model = model;

            _model.OpenLetterEvent += OpenLetter;
            _model.InitializeNewWordEvent += InitializeNewWord;
            _model.ErrorLetterEvent += OpenErrorLetter;
            _model.EndGameEvent += EndGame;

            _model.InitializeNewWord();
        }

        private void letter_Click(object sender, EventArgs e)
        {
            Label letter = (Label)sender;

            _model.OpenLetter(letter.Name[0]);

        }

        private void OpenLetter(char ch)
        {
            foreach(Label letter in word.Controls)
            {
                if (letter.Name[0] == ch)
                {
                    letter.Text = ch.ToString(); 
                    Controls[letter.Text].ForeColor = Color.Green;
                }
            }
        }

        private void OpenErrorLetter(char ch)
        {
            Controls[ch.ToString()].ForeColor = Color.Red;
            canvas.Refresh();
        }

        private void InitializeNewWord()
        {
            canvas.Refresh();
            word.Controls.Clear();

            hint.Text = _model.GetHint();

            string newWord = _model.GetWord();

            for(int i = 0; i < newWord.Length; i++)
            {
                Label label = new Label();

                label.AutoSize = true;
                label.Font = new System.Drawing.Font("Courier New", 22.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                label.Location = new System.Drawing.Point(i*46, 46);
                label.Name = newWord[i].ToString();
                label.Size = new System.Drawing.Size(40, 41);
                label.TabIndex = 0;
                label.Text = "?";

                word.Controls.Add(label);
            }

            foreach (Control c in Controls)
            {
                if (c is Label)
                {
                    c.ForeColor = Color.Black;
                }
            }
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            _drawGallows[_model.GetErrorsNumber()].DynamicInvoke(e.Graphics);
        }

        private void EndGame(bool isWin)
        {
            string message = isWin ? "Победа!\nПолучить новое слово?" : "Проигрышь!\nПолучить новое слово?"; 
            var result = MessageBox.Show(message, "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                _model.InitializeNewWord();
            }
            else
            {
                Close();
            }
        }

        private static object DrawGallowsWithZeroErrorsNumber(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Green), new Rectangle(100, 100, 20, 300));
            g.FillRectangle(new SolidBrush(Color.Green), new Rectangle(100, 100, 150, 20));
            g.FillRectangle(new SolidBrush(Color.Green), new Rectangle(230, 100, 20, 70));
            return null;
        }

        private static object DrawGallowsWithOneErrorsNumber(Graphics g)
        {
            DrawGallowsWithZeroErrorsNumber(g);
            g.FillRectangle(new SolidBrush(Color.Purple), new Rectangle(238, 170, 4, 20));
            return null;

        }

        private static object DrawGallowsWithTwoErrorsNumber(Graphics g)
        {
            DrawGallowsWithOneErrorsNumber(g);
            g.DrawEllipse(new Pen(Color.Purple,4), new Rectangle(224, 190, 30, 30));
            return null;
        }

        private static object DrawGallowsWithThreeErrorsNumber(Graphics g)
        {
            DrawGallowsWithTwoErrorsNumber(g);
            g.FillRectangle(new SolidBrush(Color.Purple), new Rectangle(238, 220, 4, 90));
            return null;
        }

        private static object DrawGallowsWithFourErrorsNumber(Graphics g)
        {
            DrawGallowsWithThreeErrorsNumber(g);
            g.DrawLine(new Pen(Color.Purple, 4), new Point(240,230), new Point(220,270));
            return null;
        }

        private static object DrawGallowsWithFiveErrorsNumber(Graphics g)
        {
            DrawGallowsWithFourErrorsNumber(g);
            g.DrawLine(new Pen(Color.Purple, 4), new Point(240, 230), new Point(260, 270));
            return null;
        }

        private static object DrawGallowsWithSixErrorsNumber(Graphics g)
        {
            DrawGallowsWithFiveErrorsNumber(g);
            g.DrawLine(new Pen(Color.Purple, 4), new Point(239, 310), new Point(220, 370));
            return null;
        }

        private static object DrawGallowsWithSevenErrorsNumber(Graphics g)
        {
            DrawGallowsWithSixErrorsNumber(g);
            g.DrawLine(new Pen(Color.Purple, 4), new Point(239, 310), new Point(260, 370));
            return null;
        }
    }
}
