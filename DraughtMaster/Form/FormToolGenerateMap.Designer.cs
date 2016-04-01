namespace DraughtMaster
{
    partial class FormToolGenerateMap
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
            this.btn_export = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_back = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label_howToUse = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radio_makNeeb = new System.Windows.Forms.RadioButton();
            this.radio_makHorse = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radio_12x12 = new System.Windows.Forms.RadioButton();
            this.radio_12x8 = new System.Windows.Forms.RadioButton();
            this.radio_8x8 = new System.Windows.Forms.RadioButton();
            this.panel_mycreate = new System.Windows.Forms.Panel();
            this.btn_clear = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radio_p2_super = new System.Windows.Forms.RadioButton();
            this.radio_p2_general = new System.Windows.Forms.RadioButton();
            this.radio_p1_super = new System.Windows.Forms.RadioButton();
            this.radio_p1_general = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel_mycreate.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.label2);
            this.panel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel3.Location = new System.Drawing.Point(65, 36);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(855, 611);
            this.panel3.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panel2.Controls.Add(this.btn_export);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btn_back);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(0, 54);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(855, 557);
            this.panel2.TabIndex = 9;
            // 
            // btn_export
            // 
            this.btn_export.BackColor = System.Drawing.Color.SteelBlue;
            this.btn_export.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btn_export.ForeColor = System.Drawing.Color.White;
            this.btn_export.Location = new System.Drawing.Point(688, 505);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(154, 45);
            this.btn_export.TabIndex = 38;
            this.btn_export.Text = "Export";
            this.btn_export.UseVisualStyleBackColor = false;
            this.btn_export.Click += new System.EventHandler(this.btn_export_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(8, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(633, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tool for generate your map.  Choose point starting player in board.";
            // 
            // btn_back
            // 
            this.btn_back.BackColor = System.Drawing.Color.Tomato;
            this.btn_back.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_back.ForeColor = System.Drawing.Color.White;
            this.btn_back.Location = new System.Drawing.Point(12, 503);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(102, 45);
            this.btn_back.TabIndex = 7;
            this.btn_back.Text = "Back";
            this.btn_back.UseVisualStyleBackColor = false;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel_mycreate);
            this.panel1.Location = new System.Drawing.Point(0, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(855, 453);
            this.panel1.TabIndex = 8;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox3);
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Location = new System.Drawing.Point(489, 11);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(363, 442);
            this.panel4.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label_howToUse);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.groupBox3.Location = new System.Drawing.Point(3, 143);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(360, 293);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "How to using";
            // 
            // label_howToUse
            // 
            this.label_howToUse.AutoSize = true;
            this.label_howToUse.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_howToUse.Location = new System.Drawing.Point(19, 29);
            this.label_howToUse.Name = "label_howToUse";
            this.label_howToUse.Size = new System.Drawing.Size(43, 17);
            this.label_howToUse.TabIndex = 0;
            this.label_howToUse.Text = "label3";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radio_makNeeb);
            this.groupBox2.Controls.Add(this.radio_makHorse);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(146, 131);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Game";
            // 
            // radio_makNeeb
            // 
            this.radio_makNeeb.AutoSize = true;
            this.radio_makNeeb.Location = new System.Drawing.Point(19, 60);
            this.radio_makNeeb.Name = "radio_makNeeb";
            this.radio_makNeeb.Size = new System.Drawing.Size(99, 25);
            this.radio_makNeeb.TabIndex = 1;
            this.radio_makNeeb.TabStop = true;
            this.radio_makNeeb.Text = "Mak Neeb";
            this.radio_makNeeb.UseVisualStyleBackColor = true;
            this.radio_makNeeb.CheckedChanged += new System.EventHandler(this.radio_makNeeb_CheckedChanged);
            // 
            // radio_makHorse
            // 
            this.radio_makHorse.AutoSize = true;
            this.radio_makHorse.Location = new System.Drawing.Point(19, 29);
            this.radio_makHorse.Name = "radio_makHorse";
            this.radio_makHorse.Size = new System.Drawing.Size(103, 25);
            this.radio_makHorse.TabIndex = 0;
            this.radio_makHorse.TabStop = true;
            this.radio_makHorse.Text = "Mak Horse";
            this.radio_makHorse.UseVisualStyleBackColor = true;
            this.radio_makHorse.CheckedChanged += new System.EventHandler(this.radio_makHorse_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radio_12x12);
            this.groupBox1.Controls.Add(this.radio_12x8);
            this.groupBox1.Controls.Add(this.radio_8x8);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.groupBox1.Location = new System.Drawing.Point(155, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(122, 131);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Size";
            // 
            // radio_12x12
            // 
            this.radio_12x12.AutoSize = true;
            this.radio_12x12.Location = new System.Drawing.Point(19, 60);
            this.radio_12x12.Name = "radio_12x12";
            this.radio_12x12.Size = new System.Drawing.Size(79, 25);
            this.radio_12x12.TabIndex = 1;
            this.radio_12x12.TabStop = true;
            this.radio_12x12.Text = "12 x 12";
            this.radio_12x12.UseVisualStyleBackColor = true;
            this.radio_12x12.CheckedChanged += new System.EventHandler(this.radio_12x12_CheckedChanged);
            // 
            // radio_12x8
            // 
            this.radio_12x8.AutoSize = true;
            this.radio_12x8.Location = new System.Drawing.Point(19, 91);
            this.radio_12x8.Name = "radio_12x8";
            this.radio_12x8.Size = new System.Drawing.Size(70, 25);
            this.radio_12x8.TabIndex = 1;
            this.radio_12x8.TabStop = true;
            this.radio_12x8.Text = "12 x 8";
            this.radio_12x8.UseVisualStyleBackColor = true;
            this.radio_12x8.Visible = false;
            this.radio_12x8.CheckedChanged += new System.EventHandler(this.radio_12x8_CheckedChanged);
            // 
            // radio_8x8
            // 
            this.radio_8x8.AutoSize = true;
            this.radio_8x8.Location = new System.Drawing.Point(19, 29);
            this.radio_8x8.Name = "radio_8x8";
            this.radio_8x8.Size = new System.Drawing.Size(61, 25);
            this.radio_8x8.TabIndex = 0;
            this.radio_8x8.TabStop = true;
            this.radio_8x8.Text = "8 x 8";
            this.radio_8x8.UseVisualStyleBackColor = true;
            this.radio_8x8.CheckedChanged += new System.EventHandler(this.radio_8x8_CheckedChanged);
            // 
            // panel_mycreate
            // 
            this.panel_mycreate.Controls.Add(this.btn_clear);
            this.panel_mycreate.Controls.Add(this.groupBox4);
            this.panel_mycreate.Controls.Add(this.label5);
            this.panel_mycreate.Controls.Add(this.label4);
            this.panel_mycreate.Location = new System.Drawing.Point(14, 3);
            this.panel_mycreate.Name = "panel_mycreate";
            this.panel_mycreate.Size = new System.Drawing.Size(469, 447);
            this.panel_mycreate.TabIndex = 0;
            // 
            // btn_clear
            // 
            this.btn_clear.BackColor = System.Drawing.Color.RoyalBlue;
            this.btn_clear.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_clear.ForeColor = System.Drawing.Color.White;
            this.btn_clear.Location = new System.Drawing.Point(358, 380);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(108, 26);
            this.btn_clear.TabIndex = 39;
            this.btn_clear.Text = "Clear Board";
            this.btn_clear.UseVisualStyleBackColor = false;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButton1);
            this.groupBox4.Controls.Add(this.radio_p2_super);
            this.groupBox4.Controls.Add(this.radio_p2_general);
            this.groupBox4.Controls.Add(this.radio_p1_super);
            this.groupBox4.Controls.Add(this.radio_p1_general);
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.groupBox4.Location = new System.Drawing.Point(0, 400);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(466, 44);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Item";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(371, 17);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(62, 21);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.Text = "Empty";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radio_p2_super
            // 
            this.radio_p2_super.AutoSize = true;
            this.radio_p2_super.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radio_p2_super.Location = new System.Drawing.Point(286, 17);
            this.radio_p2_super.Name = "radio_p2_super";
            this.radio_p2_super.Size = new System.Drawing.Size(79, 21);
            this.radio_p2_super.TabIndex = 3;
            this.radio_p2_super.Text = "P2 Horse";
            this.radio_p2_super.UseVisualStyleBackColor = true;
            this.radio_p2_super.CheckedChanged += new System.EventHandler(this.radio_p2_super_CheckedChanged);
            // 
            // radio_p2_general
            // 
            this.radio_p2_general.AutoSize = true;
            this.radio_p2_general.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radio_p2_general.Location = new System.Drawing.Point(191, 17);
            this.radio_p2_general.Name = "radio_p2_general";
            this.radio_p2_general.Size = new System.Drawing.Size(89, 21);
            this.radio_p2_general.TabIndex = 2;
            this.radio_p2_general.Text = "P2 Regular";
            this.radio_p2_general.UseVisualStyleBackColor = true;
            this.radio_p2_general.CheckedChanged += new System.EventHandler(this.radio_p2_general_CheckedChanged);
            // 
            // radio_p1_super
            // 
            this.radio_p1_super.AutoSize = true;
            this.radio_p1_super.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radio_p1_super.Location = new System.Drawing.Point(106, 17);
            this.radio_p1_super.Name = "radio_p1_super";
            this.radio_p1_super.Size = new System.Drawing.Size(79, 21);
            this.radio_p1_super.TabIndex = 1;
            this.radio_p1_super.Text = "P1 Horse";
            this.radio_p1_super.UseVisualStyleBackColor = true;
            this.radio_p1_super.CheckedChanged += new System.EventHandler(this.radio_p1_super_CheckedChanged);
            // 
            // radio_p1_general
            // 
            this.radio_p1_general.AutoSize = true;
            this.radio_p1_general.Checked = true;
            this.radio_p1_general.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radio_p1_general.Location = new System.Drawing.Point(11, 17);
            this.radio_p1_general.Name = "radio_p1_general";
            this.radio_p1_general.Size = new System.Drawing.Size(89, 21);
            this.radio_p1_general.TabIndex = 0;
            this.radio_p1_general.TabStop = true;
            this.radio_p1_general.Text = "P1 Regular";
            this.radio_p1_general.UseVisualStyleBackColor = true;
            this.radio_p1_general.CheckedChanged += new System.EventHandler(this.radio_p1_general_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(170, 380);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 17);
            this.label5.TabIndex = 36;
            this.label5.Text = "Player 2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(135, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 17);
            this.label4.TabIndex = 35;
            this.label4.Text = "Player 1 (or Computer)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label2.Location = new System.Drawing.Point(3, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(298, 50);
            this.label2.TabIndex = 3;
            this.label2.Text = "Create Your Map";
            // 
            // FormToolGenerateMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.panel3);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Name = "FormToolGenerateMap";
            this.Load += new System.EventHandler(this.FormToolGenerateMap_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel_mycreate.ResumeLayout(false);
            this.panel_mycreate.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel_mycreate;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radio_makNeeb;
        private System.Windows.Forms.RadioButton radio_makHorse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radio_12x12;
        private System.Windows.Forms.RadioButton radio_12x8;
        private System.Windows.Forms.RadioButton radio_8x8;
        private System.Windows.Forms.Button btn_export;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label_howToUse;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radio_p2_super;
        private System.Windows.Forms.RadioButton radio_p2_general;
        private System.Windows.Forms.RadioButton radio_p1_super;
        private System.Windows.Forms.RadioButton radio_p1_general;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button btn_clear;
    }
}