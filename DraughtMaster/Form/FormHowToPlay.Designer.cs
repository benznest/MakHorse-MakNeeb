namespace DraughtMaster
{
    partial class FormHowToPlay
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_next = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picBox_makNeeb = new System.Windows.Forms.PictureBox();
            this.picBox_makhorse = new System.Windows.Forms.PictureBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_makhorse = new System.Windows.Forms.Label();
            this.lbl_makneeb = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_makNeeb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_makhorse)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(41, 32);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(897, 645);
            this.panel3.TabIndex = 12;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panel2.Controls.Add(this.btn_next);
            this.panel2.Controls.Add(this.btn_back);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(0, 69);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(897, 576);
            this.panel2.TabIndex = 9;
            // 
            // btn_next
            // 
            this.btn_next.BackColor = System.Drawing.Color.Crimson;
            this.btn_next.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_next.ForeColor = System.Drawing.Color.White;
            this.btn_next.Location = new System.Drawing.Point(753, 534);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(120, 36);
            this.btn_next.TabIndex = 40;
            this.btn_next.Text = "Next";
            this.btn_next.UseVisualStyleBackColor = false;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // btn_back
            // 
            this.btn_back.BackColor = System.Drawing.Color.Tomato;
            this.btn_back.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_back.ForeColor = System.Drawing.Color.White;
            this.btn_back.Location = new System.Drawing.Point(14, 534);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(97, 36);
            this.btn_back.TabIndex = 39;
            this.btn_back.Text = "Back";
            this.btn_back.UseVisualStyleBackColor = false;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(8, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Rules in playing";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lbl_makneeb);
            this.panel1.Controls.Add(this.lbl_makhorse);
            this.panel1.Controls.Add(this.picBox_makNeeb);
            this.panel1.Controls.Add(this.picBox_makhorse);
            this.panel1.Controls.Add(this.metroLabel2);
            this.panel1.Location = new System.Drawing.Point(0, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(897, 482);
            this.panel1.TabIndex = 8;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // picBox_makNeeb
            // 
            this.picBox_makNeeb.Image = global::DraughtMaster.Properties.Resources.ex_makneeb_wb;
            this.picBox_makNeeb.Location = new System.Drawing.Point(457, 76);
            this.picBox_makNeeb.Name = "picBox_makNeeb";
            this.picBox_makNeeb.Size = new System.Drawing.Size(416, 302);
            this.picBox_makNeeb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_makNeeb.TabIndex = 44;
            this.picBox_makNeeb.TabStop = false;
            this.picBox_makNeeb.Click += new System.EventHandler(this.picBox_makNeeb_Click);
            // 
            // picBox_makhorse
            // 
            this.picBox_makhorse.Image = global::DraughtMaster.Properties.Resources.ex_makhorse;
            this.picBox_makhorse.Location = new System.Drawing.Point(35, 76);
            this.picBox_makhorse.Name = "picBox_makhorse";
            this.picBox_makhorse.Size = new System.Drawing.Size(416, 302);
            this.picBox_makhorse.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_makhorse.TabIndex = 43;
            this.picBox_makhorse.TabStop = false;
            this.picBox_makhorse.Click += new System.EventHandler(this.picBox_makhorse_Click);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.Location = new System.Drawing.Point(35, 39);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(176, 25);
            this.metroLabel2.TabIndex = 42;
            this.metroLabel2.Text = "Choose type of board";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label2.Location = new System.Drawing.Point(3, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(282, 65);
            this.label2.TabIndex = 3;
            this.label2.Text = "How to play";
            // 
            // lbl_makhorse
            // 
            this.lbl_makhorse.AutoSize = true;
            this.lbl_makhorse.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_makhorse.ForeColor = System.Drawing.Color.Tomato;
            this.lbl_makhorse.Location = new System.Drawing.Point(155, 381);
            this.lbl_makhorse.Name = "lbl_makhorse";
            this.lbl_makhorse.Size = new System.Drawing.Size(130, 32);
            this.lbl_makhorse.TabIndex = 47;
            this.lbl_makhorse.Text = "Mak Horse";
            // 
            // lbl_makneeb
            // 
            this.lbl_makneeb.AutoSize = true;
            this.lbl_makneeb.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_makneeb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_makneeb.Location = new System.Drawing.Point(592, 381);
            this.lbl_makneeb.Name = "lbl_makneeb";
            this.lbl_makneeb.Size = new System.Drawing.Size(126, 32);
            this.lbl_makneeb.TabIndex = 48;
            this.lbl_makneeb.Text = "Mak Neeb";
            // 
            // FormHowToPlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.panel3);
            this.Name = "FormHowToPlay";
            this.Load += new System.EventHandler(this.FormHowToPlay_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_makNeeb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_makhorse)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picBox_makNeeb;
        private System.Windows.Forms.PictureBox picBox_makhorse;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.Button btn_next;
        private System.Windows.Forms.Label lbl_makneeb;
        private System.Windows.Forms.Label lbl_makhorse;
    }
}