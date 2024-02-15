using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;



namespace _3
{

    public partial class Form1 : Form
    {

        //при перемецщении с поля экспериментов удалять элемент
        private struct SDragAndDrop
        {
            public bool start;
            public int startX;
            public int startY;
            public int lastX;
            public int lastY;
        };

        private SDragAndDrop dragAndDrop;
        private Elements elements;

        private const int ELEMENT_SIDE = 100; 
        private const int INTERVAL_BEETWEN_ELEMENTS = 15;
        private const int ELEMENT_COUNT_IN_LINE = 5;


        public Form1()
        {
            InitializeComponent();
            dragAndDrop = new SDragAndDrop();
            elements = new Elements();

            foreach(var element in elements.GetOpenedElements()) 
            { 
                CreateNewElement(element);
            }
        }

        private void DragElement_MouseDown(object sender, MouseEventArgs e)
        {
            dragAndDrop.start = true;

            dragAndDrop.startX = MousePosition.X;
            dragAndDrop.startY = MousePosition.Y;

            PictureBox element = (PictureBox)sender;

            dragAndDrop.lastX = element.Location.X;
            dragAndDrop.lastY = element.Location.Y;
            element.BringToFront();
        }

        private void DragElement_MouseUp(object sender, MouseEventArgs e)
        {
            dragAndDrop.start = false;
            PictureBox element = (PictureBox)sender;
            if (element.Bounds.IntersectsWith(delEl.Bounds))
            {
                FieldForExperiments.Controls.Remove(element);
                return;
            }
            foreach (PictureBox el in FieldForExperiments.Controls)
            {
                if (element.Equals(el)) continue;

                var connection = new KeyValuePair<string, string>(element.Name, el.Name);

                if (element.Bounds.IntersectsWith(el.Bounds) && elements.CheckСonnection(connection))
                {
                    foreach (var newEl in elements.TryCraetingElementsByСonnection(connection))
                    {
                        if(!OpenedElements.Controls.ContainsKey(newEl))
                        {
                            CreateNewElement(newEl);
                            Point newElementLocation = new Point(
                                (element.Location.X + el.Location.X)/2,
                                (element.Location.Y + el.Location.Y)/2
                                );

                            PictureBox newElement = InitElement((PictureBox)FieldForExperiments.Controls[FieldForExperiments.Controls.Count-2]);

                            newElement.MouseDown += new MouseEventHandler(DragElement_MouseDown);
                            newElement.MouseMove += new MouseEventHandler(Element_MouseMove);
                            newElement.MouseUp += new MouseEventHandler(DragElement_MouseUp);

                            FieldForExperiments.Controls.Add(newElement);
                            newElement.BringToFront();

                            ShowConnectionInfo(element.Name, el.Name, newElement.Name);

                            FieldForExperiments.Controls.Remove(element);
                            FieldForExperiments.Controls.Remove(el);

                            if (OpenedElements.Controls.Count == elements.GetElementsCount())
                            {
                                winMessage.Show();
                                winMessage.BringToFront();
                                break;
                            }

                        }
                    }
                    break;
                }
            }
            Refresh();
        }

        private void ShowConnectionInfo(string name1, string name2, string result)
        {
            connetionInfo.Visible = true;
            connetionInfo.Text = $"{name1} + {name2} = {result}";
            timer1.Interval = 2000;
            timer1.Stop();
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {           
            connetionInfo.Visible = false;
            timer1.Stop();
        }

        private void CreateNewElement(string newEl)
        {
            PictureBox newElement = new PictureBox();
            OpenedElements.Controls.Add(newElement);

            newElement.SizeMode = PictureBoxSizeMode.StretchImage;
            newElement.BorderStyle = BorderStyle.FixedSingle;
            newElement.Image = Image.FromFile($"images/{newEl}.png");
            newElement.Location = new Point(
                (INTERVAL_BEETWEN_ELEMENTS + (OpenedElements.Controls.Count - 1) % ELEMENT_COUNT_IN_LINE * (INTERVAL_BEETWEN_ELEMENTS + ELEMENT_SIDE)),
                (INTERVAL_BEETWEN_ELEMENTS + (OpenedElements.Controls.Count - 1) / ELEMENT_COUNT_IN_LINE * (INTERVAL_BEETWEN_ELEMENTS + ELEMENT_SIDE))
                );
            newElement.Name = newEl;
            newElement.Size = new Size(ELEMENT_SIDE, ELEMENT_SIDE);
            newElement.TabIndex = 0;
            newElement.TabStop = false;
            newElement.MouseDown += new MouseEventHandler(Element_MouseDown);

        }

        private PictureBox InitElement(PictureBox element) 
        {
            PictureBox newElement = new PictureBox
            {
                Size = element.Size,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = element.Image,
                Location = element.Location,
                BorderStyle = BorderStyle.FixedSingle,
                Name = element.Name,
            };

            return newElement;
        }

        private void Element_MouseDown(object sender, MouseEventArgs e)
        {
            dragAndDrop.start = true;

            dragAndDrop.startX = MousePosition.X;
            dragAndDrop.startY = MousePosition.Y;

            PictureBox element = (PictureBox)sender;

            PictureBox newElement = InitElement(element);

            newElement.MouseDown += new MouseEventHandler(Element_MouseDown);

            element.MouseDown -= new MouseEventHandler(Element_MouseDown);
            element.MouseDown += new MouseEventHandler(DragElement_MouseDown);
            element.MouseMove += new MouseEventHandler(Element_MouseMove);
            element.MouseUp += new MouseEventHandler(Element_MouseUp);

            element.Location = element.FindForm().PointToClient(element.Parent.PointToScreen(element.Location));

            OpenedElements.Controls.Add(newElement);

            Controls.Add(element);
            Controls.SetChildIndex(element, 0);

            dragAndDrop.lastX = element.Location.X;
            dragAndDrop.lastY = element.Location.Y;
        }

        private void Element_MouseMove(object sender, MouseEventArgs e)
        {
            if (!dragAndDrop.start)
            {
                return;
            }

            PictureBox dargableElement = (PictureBox)sender;

            dargableElement.Location = new Point(
                MousePosition.X - dragAndDrop.startX + dragAndDrop.lastX,
                MousePosition.Y - dragAndDrop.startY + dragAndDrop.lastY);
            this.Refresh();
        }

        private void Element_MouseUp(object sender, MouseEventArgs e)
        {
            dragAndDrop.start = false;

            PictureBox dargableElement = (PictureBox)sender;

            Point FieldForExperimentsLocation =
                    FieldForExperiments.FindForm().PointToClient(FieldForExperiments.Parent.PointToScreen(FieldForExperiments.Location));

            Rectangle FieldForExperimentsBounds = new Rectangle(
                FieldForExperimentsLocation.X,
                FieldForExperimentsLocation.Y,
                FieldForExperiments.Width,
                FieldForExperiments.Height
                );

            if (dargableElement.Bounds.IntersectsWith(FieldForExperimentsBounds))
            {
                FieldForExperiments.Controls.Add(dargableElement);

                dargableElement.Location =
                    new Point(
                        dargableElement.Location.X - FieldForExperimentsLocation.X,
                        dargableElement.Location.Y - FieldForExperimentsLocation.Y
                        );

                dargableElement.MouseUp -= new MouseEventHandler(Element_MouseUp);
                dargableElement.MouseUp += new MouseEventHandler(DragElement_MouseUp);
                dargableElement.BringToFront();
            }
            else
            {
                Controls.Remove(dargableElement);
            }
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            List <string> namesArray = new List<string>();
            foreach (Control item in OpenedElements.Controls)
            {
                namesArray.Add(item.Name);
            };

            namesArray.Sort();

            for(int i = namesArray.Count - 1; i >= 0; i--) 
            {
                OpenedElements.Controls[namesArray[i]].BringToFront();
            }

            for (int i = 0; i < OpenedElements.Controls.Count; i++)
            {
                OpenedElements.Controls[i].Location = new Point(
                    (INTERVAL_BEETWEN_ELEMENTS + i % ELEMENT_COUNT_IN_LINE * (INTERVAL_BEETWEN_ELEMENTS + ELEMENT_SIDE)),
                    (INTERVAL_BEETWEN_ELEMENTS + i / ELEMENT_COUNT_IN_LINE * (INTERVAL_BEETWEN_ELEMENTS + ELEMENT_SIDE))
                    );
            }
        }
    }
}
