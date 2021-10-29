namespace Quan_Ly_Ca_Phe
{
    partial class AddDish
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbCater = new System.Windows.Forms.ComboBox();
            this.cbFood = new System.Windows.Forms.ComboBox();
            this.nudAddFood = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudAddFood)).BeginInit();
            this.SuspendLayout();
            // 
            // cbCater
            // 
            this.cbCater.FormattingEnabled = true;
            this.cbCater.Location = new System.Drawing.Point(46, 34);
            this.cbCater.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbCater.Name = "cbCater";
            this.cbCater.Size = new System.Drawing.Size(348, 28);
            this.cbCater.TabIndex = 1;
            // 
            // cbFood
            // 
            this.cbFood.FormattingEnabled = true;
            this.cbFood.Location = new System.Drawing.Point(46, 95);
            this.cbFood.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbFood.Name = "cbFood";
            this.cbFood.Size = new System.Drawing.Size(348, 28);
            this.cbFood.TabIndex = 2;
            // 
            // nudAddFood
            // 
            this.nudAddFood.Location = new System.Drawing.Point(46, 147);
            this.nudAddFood.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.nudAddFood.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudAddFood.Name = "nudAddFood";
            this.nudAddFood.Size = new System.Drawing.Size(75, 26);
            this.nudAddFood.TabIndex = 4;
            this.nudAddFood.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // AddDish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.nudAddFood);
            this.Controls.Add(this.cbFood);
            this.Controls.Add(this.cbCater);
            this.Name = "AddDish";
            this.Text = "AddDish";
            ((System.ComponentModel.ISupportInitialize)(this.nudAddFood)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbCater;
        private System.Windows.Forms.ComboBox cbFood;
        private System.Windows.Forms.NumericUpDown nudAddFood;
    }
}