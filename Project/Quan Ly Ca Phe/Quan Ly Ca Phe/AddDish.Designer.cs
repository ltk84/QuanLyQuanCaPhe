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
            this.lvBill = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            // lvBill
            // 
            this.lvBill.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvBill.HideSelection = false;
            this.lvBill.Location = new System.Drawing.Point(0, 0);
            this.lvBill.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lvBill.Name = "lvBill";
            this.lvBill.Size = new System.Drawing.Size(800, 450);
            this.lvBill.TabIndex = 5;
            this.lvBill.UseCompatibleStateImageBehavior = false;
            this.lvBill.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Tên món ăn";
            this.columnHeader1.Width = 111;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Số lượng";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Đơn giá";
            this.columnHeader3.Width = 64;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Thành tiền";
            this.columnHeader4.Width = 95;
            // 
            // AddDish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lvBill);
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
        private System.Windows.Forms.ListView lvBill;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}