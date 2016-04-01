namespace DraughtMaster
{
    partial class FormMainMenu
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_how_to_play = new System.Windows.Forms.Button();
            this.btn_credit = new System.Windows.Forms.Button();
            this.btn_load = new System.Windows.Forms.Button();
            this.btn_quit = new System.Windows.Forms.Button();
            this.btn_play = new System.Windows.Forms.Button();
            this.btn_setting = new System.Windows.Forms.Button();
            this.btn_editor = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Location = new System.Drawing.Point(0, 38);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1000, 667);
            this.panel3.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(0, 15);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1000, 624);
            this.panel2.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(168)))), ((int)(((byte)(236)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btn_how_to_play);
            this.panel1.Controls.Add(this.btn_credit);
            this.panel1.Controls.Add(this.btn_load);
            this.panel1.Controls.Add(this.btn_quit);
            this.panel1.Controls.Add(this.btn_play);
            this.panel1.Controls.Add(this.btn_setting);
            this.panel1.Controls.Add(this.btn_editor);
            this.panel1.Location = new System.Drawing.Point(0, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 646);
            this.panel1.TabIndex = 8;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btn_how_to_play
            // 
            this.btn_how_to_play.BackColor = System.Drawing.Color.Gray;
            this.btn_how_to_play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_how_to_play.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_how_to_play.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_how_to_play.Location = new System.Drawing.Point(293, 387);
            this.btn_how_to_play.Name = "btn_how_to_play";
            this.btn_how_to_play.Size = new System.Drawing.Size(407, 53);
            this.btn_how_to_play.TabIndex = 10;
            this.btn_how_to_play.Text = "HOW TO PLAY";
            this.btn_how_to_play.UseVisualStyleBackColor = false;
            this.btn_how_to_play.Click += new System.EventHandler(this.btn_how_to_play_Click);
            // 
            // btn_credit
            // 
            this.btn_credit.BackColor = System.Drawing.Color.Gray;
            this.btn_credit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_credit.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_credit.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_credit.Location = new System.Drawing.Point(293, 505);
            this.btn_credit.Name = "btn_credit";
            this.btn_credit.Size = new System.Drawing.Size(407, 53);
            this.btn_credit.TabIndex = 9;
            this.btn_credit.Text = "CREDITS";
            this.btn_credit.UseVisualStyleBackColor = false;
            this.btn_credit.Click += new System.EventHandler(this.btn_credit_Click);
            // 
            // btn_load
            // 
            this.btn_load.BackColor = System.Drawing.Color.Gray;
            this.btn_load.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_load.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_load.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_load.Location = new System.Drawing.Point(293, 270);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(407, 53);
            this.btn_load.TabIndex = 8;
            this.btn_load.Text = "LOAD";
            this.btn_load.UseVisualStyleBackColor = false;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // btn_quit
            // 
            this.btn_quit.BackColor = System.Drawing.Color.Gray;
            this.btn_quit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_quit.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_quit.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_quit.Location = new System.Drawing.Point(293, 564);
            this.btn_quit.Name = "btn_quit";
            this.btn_quit.Size = new System.Drawing.Size(407, 53);
            this.btn_quit.TabIndex = 7;
            this.btn_quit.Text = "QUIT";
            this.btn_quit.UseVisualStyleBackColor = false;
            this.btn_quit.Click += new System.EventHandler(this.btn_quit_Click);
            // 
            // btn_play
            // 
            this.btn_play.BackColor = System.Drawing.Color.Gray;
            this.btn_play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_play.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_play.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_play.Location = new System.Drawing.Point(293, 214);
            this.btn_play.Name = "btn_play";
            this.btn_play.Size = new System.Drawing.Size(407, 50);
            this.btn_play.TabIndex = 6;
            this.btn_play.Text = "PLAY";
            this.btn_play.UseVisualStyleBackColor = false;
            this.btn_play.Click += new System.EventHandler(this.btn_play_Click);
            // 
            // btn_setting
            // 
            this.btn_setting.BackColor = System.Drawing.Color.Gray;
            this.btn_setting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_setting.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_setting.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_setting.Location = new System.Drawing.Point(293, 329);
            this.btn_setting.Name = "btn_setting";
            this.btn_setting.Size = new System.Drawing.Size(407, 53);
            this.btn_setting.TabIndex = 1;
            this.btn_setting.Text = "SETTING";
            this.btn_setting.UseVisualStyleBackColor = false;
            this.btn_setting.Click += new System.EventHandler(this.btn_setting_Click);
            // 
            // btn_editor
            // 
            this.btn_editor.BackColor = System.Drawing.Color.Gray;
            this.btn_editor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_editor.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_editor.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_editor.Location = new System.Drawing.Point(293, 446);
            this.btn_editor.Name = "btn_editor";
            this.btn_editor.Size = new System.Drawing.Size(407, 53);
            this.btn_editor.TabIndex = 0;
            this.btn_editor.Text = "BOARD EDITOR";
            this.btn_editor.UseVisualStyleBackColor = false;
            this.btn_editor.Click += new System.EventHandler(this.btn_editor_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DraughtMaster.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(144, 37);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(774, 171);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // FormMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.panel3);
            this.Name = "FormMainMenu";
            this.Load += new System.EventHandler(this.FormMainMenu_Load);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.Button btn_quit;
        private System.Windows.Forms.Button btn_play;
        private System.Windows.Forms.Button btn_setting;
        private System.Windows.Forms.Button btn_editor;
        private System.Windows.Forms.Button btn_credit;
        private System.Windows.Forms.Button btn_how_to_play;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}