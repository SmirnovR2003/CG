using System.Drawing;
using System.Windows.Forms;

namespace _3
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.sortButton = new System.Windows.Forms.Button();
            this.OpenedElements = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.FieldForExperiments = new System.Windows.Forms.Panel();
            this.connetionInfo = new System.Windows.Forms.Label();
            this.delEl = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.earth = new System.Windows.Forms.PictureBox();
            this.air = new System.Windows.Forms.PictureBox();
            this.water = new System.Windows.Forms.PictureBox();
            this.fire = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.winMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.FieldForExperiments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.delEl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.earth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.air)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.water)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fire)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.sortButton);
            this.splitContainer1.Panel1.Controls.Add(this.OpenedElements);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AllowDrop = true;
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.connetionInfo);
            this.splitContainer1.Panel2.Controls.Add(this.FieldForExperiments);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(1374, 744);
            this.splitContainer1.SplitterDistance = 625;
            this.splitContainer1.TabIndex = 0;
            // 
            // sortButton
            // 
            this.sortButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.sortButton.Location = new System.Drawing.Point(240, 698);
            this.sortButton.Name = "sortButton";
            this.sortButton.Size = new System.Drawing.Size(136, 33);
            this.sortButton.TabIndex = 8;
            this.sortButton.Text = "Sort";
            this.sortButton.UseVisualStyleBackColor = true;
            this.sortButton.Click += new System.EventHandler(this.sortButton_Click);
            // 
            // OpenedElements
            // 
            this.OpenedElements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenedElements.AutoScroll = true;
            this.OpenedElements.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OpenedElements.Location = new System.Drawing.Point(12, 63);
            this.OpenedElements.Margin = new System.Windows.Forms.Padding(0);
            this.OpenedElements.Name = "OpenedElements";
            this.OpenedElements.Size = new System.Drawing.Size(601, 629);
            this.OpenedElements.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(623, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Opened Elements";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FieldForExperiments
            // 
            this.FieldForExperiments.AllowDrop = true;
            this.FieldForExperiments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldForExperiments.BackColor = System.Drawing.SystemColors.Control;
            this.FieldForExperiments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FieldForExperiments.Controls.Add(this.delEl);
            this.FieldForExperiments.Location = new System.Drawing.Point(15, 63);
            this.FieldForExperiments.Name = "FieldForExperiments";
            this.FieldForExperiments.Size = new System.Drawing.Size(718, 669);
            this.FieldForExperiments.TabIndex = 1;
            // 
            // connetionInfo
            // 
            this.connetionInfo.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.connetionInfo.AutoSize = true;
            this.connetionInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.connetionInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connetionInfo.Location = new System.Drawing.Point(263, 704);
            this.connetionInfo.Name = "connetionInfo";
            this.connetionInfo.Size = new System.Drawing.Size(222, 39);
            this.connetionInfo.TabIndex = 1;
            this.connetionInfo.Text = "connetionInfo";
            this.connetionInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.connetionInfo.Visible = false;
            // 
            // delEl
            // 
            this.delEl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.delEl.BackColor = System.Drawing.SystemColors.Control;
            this.delEl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.delEl.Image = ((System.Drawing.Image)(resources.GetObject("delEl.Image")));
            this.delEl.Location = new System.Drawing.Point(662, 625);
            this.delEl.Name = "delEl";
            this.delEl.Size = new System.Drawing.Size(39, 39);
            this.delEl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.delEl.TabIndex = 0;
            this.delEl.TabStop = false;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(743, 45);
            this.label2.TabIndex = 0;
            this.label2.Text = "Field For Experiments";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // earth
            // 
            this.earth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.earth.Image = ((System.Drawing.Image)(resources.GetObject("earth.Image")));
            this.earth.Location = new System.Drawing.Point(360, 15);
            this.earth.Name = "earth";
            this.earth.Size = new System.Drawing.Size(100, 100);
            this.earth.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.earth.TabIndex = 23;
            this.earth.TabStop = false;
            this.earth.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Element_MouseDown);
            // 
            // air
            // 
            this.air.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.air.Image = ((System.Drawing.Image)(resources.GetObject("air.Image")));
            this.air.Location = new System.Drawing.Point(245, 15);
            this.air.Name = "air";
            this.air.Size = new System.Drawing.Size(100, 100);
            this.air.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.air.TabIndex = 22;
            this.air.TabStop = false;
            this.air.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Element_MouseDown);
            // 
            // water
            // 
            this.water.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.water.Image = ((System.Drawing.Image)(resources.GetObject("water.Image")));
            this.water.Location = new System.Drawing.Point(130, 15);
            this.water.Name = "water";
            this.water.Size = new System.Drawing.Size(100, 100);
            this.water.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.water.TabIndex = 21;
            this.water.TabStop = false;
            this.water.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Element_MouseDown);
            // 
            // fire
            // 
            this.fire.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fire.Image = ((System.Drawing.Image)(resources.GetObject("fire.Image")));
            this.fire.Location = new System.Drawing.Point(15, 15);
            this.fire.Name = "fire";
            this.fire.Size = new System.Drawing.Size(100, 100);
            this.fire.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.fire.TabIndex = 0;
            this.fire.TabStop = false;
            this.fire.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Element_MouseDown);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // winMessage
            // 
            this.winMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.winMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.winMessage.Location = new System.Drawing.Point(0, 0);
            this.winMessage.Name = "winMessage";
            this.winMessage.Size = new System.Drawing.Size(1374, 744);
            this.winMessage.TabIndex = 1;
            this.winMessage.Text = "You win!!!!";
            this.winMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.winMessage.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1374, 744);
            this.Controls.Add(this.winMessage);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Алхимия";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.FieldForExperiments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.delEl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.earth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.air)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.water)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fire)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel OpenedElements;
        private System.Windows.Forms.Panel FieldForExperiments;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox fire;
        private System.Windows.Forms.Button sortButton;
        private System.Windows.Forms.PictureBox earth;
        private System.Windows.Forms.PictureBox air;
        private System.Windows.Forms.PictureBox water;
        private PictureBox delEl;
        private Label connetionInfo;
        private Timer timer1;
        private Label winMessage;
    }
}

