namespace Quan_Ly_Ca_Phe
{
    partial class CheckOut
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalPrice = new System.Windows.Forms.TextBox();
            this.nudDiscount = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelCost = new System.Windows.Forms.Label();
            this.btnCheckOut = new CustomButton.VBButton2();
            this.btnCancel = new CustomButton.VBButton2();
            this.vbButton21 = new CustomButton.VBButton2();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiscount)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 18F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(147, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 29);
            this.label1.TabIndex = 6;
            this.label1.Text = "Thanh toán";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Bàn số";
            // 
            // txtTotalPrice
            // 
            this.txtTotalPrice.Enabled = false;
            this.txtTotalPrice.Location = new System.Drawing.Point(246, 77);
            this.txtTotalPrice.Name = "txtTotalPrice";
            this.txtTotalPrice.ReadOnly = true;
            this.txtTotalPrice.Size = new System.Drawing.Size(136, 20);
            this.txtTotalPrice.TabIndex = 8;
            this.txtTotalPrice.Text = "0";
            // 
            // nudDiscount
            // 
            this.nudDiscount.Location = new System.Drawing.Point(246, 103);
            this.nudDiscount.Name = "nudDiscount";
            this.nudDiscount.Size = new System.Drawing.Size(76, 20);
            this.nudDiscount.TabIndex = 9;
            this.nudDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(94, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Tổng số tiền (VND)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(94, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Khuyến mãi (%)";
            // 
            // labelCost
            // 
            this.labelCost.AutoSize = true;
            this.labelCost.Location = new System.Drawing.Point(246, 133);
            this.labelCost.Name = "labelCost";
            this.labelCost.Size = new System.Drawing.Size(28, 13);
            this.labelCost.TabIndex = 13;
            this.labelCost.Text = "0.00";
            // 
            // btnCheckOut
            // 
            this.btnCheckOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(169)))), ((int)(((byte)(18)))));
            this.btnCheckOut.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(169)))), ((int)(((byte)(18)))));
            this.btnCheckOut.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnCheckOut.BorderRadius = 2;
            this.btnCheckOut.BorderSize = 0;
            this.btnCheckOut.FlatAppearance.BorderSize = 0;
            this.btnCheckOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckOut.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckOut.ForeColor = System.Drawing.Color.White;
            this.btnCheckOut.Location = new System.Drawing.Point(321, 169);
            this.btnCheckOut.Name = "btnCheckOut";
            this.btnCheckOut.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCheckOut.Size = new System.Drawing.Size(81, 22);
            this.btnCheckOut.TabIndex = 15;
            this.btnCheckOut.Text = "Thanh toán";
            this.btnCheckOut.TextColor = System.Drawing.Color.White;
            this.btnCheckOut.UseVisualStyleBackColor = false;
            this.btnCheckOut.Click += new System.EventHandler(this.vbButton21_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Red;
            this.btnCancel.BackgroundColor = System.Drawing.Color.Red;
            this.btnCancel.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnCancel.BorderRadius = 2;
            this.btnCancel.BorderSize = 0;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(221, 170);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 22);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.TextColor = System.Drawing.Color.White;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // vbButton21
            // 
            this.vbButton21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(169)))), ((int)(((byte)(18)))));
            this.vbButton21.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(169)))), ((int)(((byte)(18)))));
            this.vbButton21.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.vbButton21.BorderRadius = 2;
            this.vbButton21.BorderSize = 0;
            this.vbButton21.FlatAppearance.BorderSize = 0;
            this.vbButton21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.vbButton21.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vbButton21.ForeColor = System.Drawing.Color.White;
            this.vbButton21.Location = new System.Drawing.Point(97, 129);
            this.vbButton21.Name = "vbButton21";
            this.vbButton21.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.vbButton21.Size = new System.Drawing.Size(81, 22);
            this.vbButton21.TabIndex = 16;
            this.vbButton21.Text = "Thành tiền";
            this.vbButton21.TextColor = System.Drawing.Color.White;
            this.vbButton21.UseVisualStyleBackColor = false;
            this.vbButton21.Click += new System.EventHandler(this.vbButton21_Click_1);
            // 
            // CheckOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 211);
            this.Controls.Add(this.vbButton21);
            this.Controls.Add(this.btnCheckOut);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.labelCost);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudDiscount);
            this.Controls.Add(this.txtTotalPrice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CheckOut";
            this.Text = "CheckOut";
            ((System.ComponentModel.ISupportInitialize)(this.nudDiscount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTotalPrice;
        private System.Windows.Forms.NumericUpDown nudDiscount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelCost;
        private CustomButton.VBButton2 btnCancel;
        private CustomButton.VBButton2 btnCheckOut;
        private CustomButton.VBButton2 vbButton21;
    }
}