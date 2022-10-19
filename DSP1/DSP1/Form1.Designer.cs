namespace DSP1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.button1 = new System.Windows.Forms.Button();
            this.numberInput = new System.Windows.Forms.ComboBox();
            this.signalTypeInput = new System.Windows.Forms.ComboBox();
            this.constValues = new System.Windows.Forms.ComboBox();
            this.Chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(441, 573);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Расчет";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numberInput
            // 
            this.numberInput.FormattingEnabled = true;
            this.numberInput.Items.AddRange(new object[] {
            "512",
            "1024",
            "2048",
            "4096"});
            this.numberInput.Location = new System.Drawing.Point(4, 572);
            this.numberInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numberInput.Name = "numberInput";
            this.numberInput.Size = new System.Drawing.Size(121, 24);
            this.numberInput.TabIndex = 1;
            // 
            // signalTypeInput
            // 
            this.signalTypeInput.FormattingEnabled = true;
            this.signalTypeInput.Items.AddRange(new object[] {
            "harmonical",
            "polyharmonical",
            "poly+linear"});
            this.signalTypeInput.Location = new System.Drawing.Point(153, 572);
            this.signalTypeInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.signalTypeInput.Name = "signalTypeInput";
            this.signalTypeInput.Size = new System.Drawing.Size(121, 24);
            this.signalTypeInput.TabIndex = 2;
            // 
            // constValues
            // 
            this.constValues.FormattingEnabled = true;
            this.constValues.Items.AddRange(new object[] {
            "A and f const",
            "A and fi const",
            "fi and f const"});
            this.constValues.Location = new System.Drawing.Point(297, 572);
            this.constValues.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.constValues.Name = "constValues";
            this.constValues.Size = new System.Drawing.Size(121, 24);
            this.constValues.TabIndex = 3;
            // 
            // Chart1
            // 
            chartArea1.Name = "ChartArea";
            this.Chart1.ChartAreas.Add(chartArea1);
            this.Chart1.Location = new System.Drawing.Point(-3, -1);
            this.Chart1.Name = "Chart1";
            this.Chart1.Size = new System.Drawing.Size(1323, 544);
            this.Chart1.TabIndex = 4;
            this.Chart1.Text = "chart1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1332, 623);
            this.Controls.Add(this.Chart1);
            this.Controls.Add(this.constValues);
            this.Controls.Add(this.signalTypeInput);
            this.Controls.Add(this.numberInput);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox numberInput;
        private System.Windows.Forms.ComboBox signalTypeInput;
        private System.Windows.Forms.ComboBox constValues;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart1;
    }
}

