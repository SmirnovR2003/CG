namespace JumpingLetters
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.thirdLetter = new System.Windows.Forms.Label();
            this.secondLetter = new System.Windows.Forms.Label();
            this.firstLetter = new System.Windows.Forms.Label();
            this.jump = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.thirdLetter);
            this.panel1.Controls.Add(this.secondLetter);
            this.panel1.Controls.Add(this.firstLetter);
            this.panel1.Location = new System.Drawing.Point(81, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(591, 268);
            this.panel1.TabIndex = 0;
            // 
            // thirdLetter
            // 
            this.thirdLetter.Location = new System.Drawing.Point(453, 80);
            this.thirdLetter.Name = "thirdLetter";
            this.thirdLetter.Size = new System.Drawing.Size(100, 160);
            this.thirdLetter.TabIndex = 2;
            this.thirdLetter.Paint += new System.Windows.Forms.PaintEventHandler(this.thirdLetter_Paint);
            // 
            // secondLetter
            // 
            this.secondLetter.Location = new System.Drawing.Point(249, 80);
            this.secondLetter.Name = "secondLetter";
            this.secondLetter.Size = new System.Drawing.Size(100, 160);
            this.secondLetter.TabIndex = 1;
            this.secondLetter.Paint += new System.Windows.Forms.PaintEventHandler(this.secondLetter_Paint);
            // 
            // firstLetter
            // 
            this.firstLetter.Location = new System.Drawing.Point(34, 80);
            this.firstLetter.Margin = new System.Windows.Forms.Padding(0);
            this.firstLetter.Name = "firstLetter";
            this.firstLetter.Size = new System.Drawing.Size(100, 160);
            this.firstLetter.TabIndex = 0;
            this.firstLetter.Paint += new System.Windows.Forms.PaintEventHandler(this.firstLetter_Paint);
            // 
            // jump
            // 
            this.jump.Location = new System.Drawing.Point(597, 341);
            this.jump.Name = "jump";
            this.jump.Size = new System.Drawing.Size(75, 23);
            this.jump.TabIndex = 1;
            this.jump.Text = "Прыгать";
            this.jump.UseVisualStyleBackColor = true;
            this.jump.Click += new System.EventHandler(this.jump_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.jump);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "JumpingLetters";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label thirdLetter;
        private System.Windows.Forms.Label secondLetter;
        private System.Windows.Forms.Label firstLetter;
        private System.Windows.Forms.Button jump;
    }
}

