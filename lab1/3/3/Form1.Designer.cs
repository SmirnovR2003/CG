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
            this.canvas = new System.Windows.Forms.Panel();
            this.fillColorComboBox = new System.Windows.Forms.ComboBox();
            this.fillColorLabel = new System.Windows.Forms.Label();
            this.borderColorLabel = new System.Windows.Forms.Label();
            this.borderColorComboBox = new System.Windows.Forms.ComboBox();
            this.drawButton = new System.Windows.Forms.Button();
            this.centerXValue = new System.Windows.Forms.NumericUpDown();
            this.radiusValue = new System.Windows.Forms.NumericUpDown();
            this.centerXLabel = new System.Windows.Forms.Label();
            this.radiusLabel = new System.Windows.Forms.Label();
            this.centerYValue = new System.Windows.Forms.NumericUpDown();
            this.centerYLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.centerXValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radiusValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.centerYValue)).BeginInit();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.SystemColors.Window;
            this.canvas.Location = new System.Drawing.Point(12, 59);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(776, 379);
            this.canvas.TabIndex = 0;
            // 
            // fillColorComboBox
            // 
            this.fillColorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fillColorComboBox.FormattingEnabled = true;
            this.fillColorComboBox.Items.AddRange(new object[] {
            "Red",
            "Green",
            "Blue"});
            this.fillColorComboBox.Location = new System.Drawing.Point(390, 29);
            this.fillColorComboBox.Name = "fillColorComboBox";
            this.fillColorComboBox.Size = new System.Drawing.Size(121, 24);
            this.fillColorComboBox.TabIndex = 1;
            // 
            // fillColorLabel
            // 
            this.fillColorLabel.AutoSize = true;
            this.fillColorLabel.Location = new System.Drawing.Point(387, 9);
            this.fillColorLabel.Name = "fillColorLabel";
            this.fillColorLabel.Size = new System.Drawing.Size(56, 16);
            this.fillColorLabel.TabIndex = 2;
            this.fillColorLabel.Text = "FillColor";
            // 
            // borderColorLabel
            // 
            this.borderColorLabel.AutoSize = true;
            this.borderColorLabel.Location = new System.Drawing.Point(514, 9);
            this.borderColorLabel.Name = "borderColorLabel";
            this.borderColorLabel.Size = new System.Drawing.Size(80, 16);
            this.borderColorLabel.TabIndex = 3;
            this.borderColorLabel.Text = "BorderColor";
            // 
            // borderColorComboBox
            // 
            this.borderColorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.borderColorComboBox.FormattingEnabled = true;
            this.borderColorComboBox.Items.AddRange(new object[] {
            "Red",
            "Green",
            "Blue"});
            this.borderColorComboBox.Location = new System.Drawing.Point(517, 29);
            this.borderColorComboBox.Name = "borderColorComboBox";
            this.borderColorComboBox.Size = new System.Drawing.Size(121, 24);
            this.borderColorComboBox.TabIndex = 4;
            // 
            // drawButton
            // 
            this.drawButton.Location = new System.Drawing.Point(644, 28);
            this.drawButton.Name = "drawButton";
            this.drawButton.Size = new System.Drawing.Size(75, 23);
            this.drawButton.TabIndex = 5;
            this.drawButton.Text = "Draw";
            this.drawButton.UseVisualStyleBackColor = true;
            this.drawButton.Click += new System.EventHandler(this.drawButton_Click);
            // 
            // centerXValue
            // 
            this.centerXValue.Location = new System.Drawing.Point(12, 29);
            this.centerXValue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.centerXValue.Name = "centerXValue";
            this.centerXValue.Size = new System.Drawing.Size(120, 22);
            this.centerXValue.TabIndex = 0;
            // 
            // radiusValue
            // 
            this.radiusValue.Location = new System.Drawing.Point(264, 29);
            this.radiusValue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.radiusValue.Name = "radiusValue";
            this.radiusValue.Size = new System.Drawing.Size(120, 22);
            this.radiusValue.TabIndex = 0;
            // 
            // centerXLabel
            // 
            this.centerXLabel.AutoSize = true;
            this.centerXLabel.Location = new System.Drawing.Point(9, 9);
            this.centerXLabel.Name = "centerXLabel";
            this.centerXLabel.Size = new System.Drawing.Size(54, 16);
            this.centerXLabel.TabIndex = 0;
            this.centerXLabel.Text = "CenterX";
            // 
            // radiusLabel
            // 
            this.radiusLabel.AutoSize = true;
            this.radiusLabel.Location = new System.Drawing.Point(261, 9);
            this.radiusLabel.Name = "radiusLabel";
            this.radiusLabel.Size = new System.Drawing.Size(50, 16);
            this.radiusLabel.TabIndex = 0;
            this.radiusLabel.Text = "Radius";
            // 
            // centerYValue
            // 
            this.centerYValue.Location = new System.Drawing.Point(138, 29);
            this.centerYValue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.centerYValue.Name = "centerYValue";
            this.centerYValue.Size = new System.Drawing.Size(120, 22);
            this.centerYValue.TabIndex = 1;
            // 
            // centerYLabel
            // 
            this.centerYLabel.AutoSize = true;
            this.centerYLabel.Location = new System.Drawing.Point(135, 9);
            this.centerYLabel.Name = "centerYLabel";
            this.centerYLabel.Size = new System.Drawing.Size(55, 16);
            this.centerYLabel.TabIndex = 2;
            this.centerYLabel.Text = "CenterY";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.drawButton);
            this.Controls.Add(this.fillColorComboBox);
            this.Controls.Add(this.borderColorLabel);
            this.Controls.Add(this.fillColorLabel);
            this.Controls.Add(this.borderColorComboBox);
            this.Controls.Add(this.radiusLabel);
            this.Controls.Add(this.centerYLabel);
            this.Controls.Add(this.radiusValue);
            this.Controls.Add(this.centerYValue);
            this.Controls.Add(this.centerXLabel);
            this.Controls.Add(this.centerXValue);
            this.Controls.Add(this.canvas);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.centerXValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radiusValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.centerYValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel canvas;
        private System.Windows.Forms.ComboBox fillColorComboBox;
        private System.Windows.Forms.Label fillColorLabel;
        private System.Windows.Forms.Label borderColorLabel;
        private System.Windows.Forms.ComboBox borderColorComboBox;
        private System.Windows.Forms.Button drawButton;
        private System.Windows.Forms.NumericUpDown centerXValue;
        private System.Windows.Forms.NumericUpDown radiusValue;
        private System.Windows.Forms.Label centerXLabel;
        private System.Windows.Forms.Label radiusLabel;
        private System.Windows.Forms.NumericUpDown centerYValue;
        private System.Windows.Forms.Label centerYLabel;
    }
}

