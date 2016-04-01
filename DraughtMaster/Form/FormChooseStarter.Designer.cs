namespace DraughtMaster
{
    partial class FormChooseStarter
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_random = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_player1 = new System.Windows.Forms.Button();
            this.btn_player2 = new System.Windows.Forms.Button();
            this.label_result = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panel2.Controls.Add(this.btn_start);
            this.panel2.Controls.Add(this.btn_cancel);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(710, 473);
            this.panel2.TabIndex = 10;
            // 
            // btn_start
            // 
            this.btn_start.BackColor = System.Drawing.Color.Tomato;
            this.btn_start.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_start.ForeColor = System.Drawing.Color.White;
            this.btn_start.Location = new System.Drawing.Point(0, 401);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(706, 69);
            this.btn_start.TabIndex = 150;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = false;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.Silver;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Font = new System.Drawing.Font("Berlin Sans FB", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancel.ForeColor = System.Drawing.Color.White;
            this.btn_cancel.Location = new System.Drawing.Point(662, 3);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(44, 48);
            this.btn_cancel.TabIndex = 149;
            this.btn_cancel.Text = "X";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Honeydew;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btn_random);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_player1);
            this.panel1.Controls.Add(this.btn_player2);
            this.panel1.Controls.Add(this.label_result);
            this.panel1.Location = new System.Drawing.Point(0, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(710, 338);
            this.panel1.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(485, 261);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 40);
            this.label3.TabIndex = 153;
            this.label3.Text = "Top or Down";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_random
            // 
            this.btn_random.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_random.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_random.ForeColor = System.Drawing.Color.DarkGray;
            this.btn_random.Location = new System.Drawing.Point(463, 124);
            this.btn_random.Name = "btn_random";
            this.btn_random.Size = new System.Drawing.Size(206, 134);
            this.btn_random.TabIndex = 152;
            this.btn_random.Text = "Random";
            this.btn_random.UseVisualStyleBackColor = false;
            this.btn_random.Click += new System.EventHandler(this.btn_random_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(310, 261);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 40);
            this.label2.TabIndex = 151;
            this.label2.Text = "Down";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(109, 261);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 40);
            this.label1.TabIndex = 150;
            this.label1.Text = "Top";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_player1
            // 
            this.btn_player1.BackColor = System.Drawing.Color.LawnGreen;
            this.btn_player1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_player1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btn_player1.FlatAppearance.BorderSize = 3;
            this.btn_player1.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_player1.ForeColor = System.Drawing.Color.White;
            this.btn_player1.Location = new System.Drawing.Point(42, 124);
            this.btn_player1.Name = "btn_player1";
            this.btn_player1.Size = new System.Drawing.Size(206, 134);
            this.btn_player1.TabIndex = 149;
            this.btn_player1.Text = "Player 1";
            this.btn_player1.UseVisualStyleBackColor = false;
            this.btn_player1.Click += new System.EventHandler(this.btn_player1_Click);
            // 
            // btn_player2
            // 
            this.btn_player2.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_player2.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_player2.ForeColor = System.Drawing.Color.DarkGray;
            this.btn_player2.Location = new System.Drawing.Point(254, 124);
            this.btn_player2.Name = "btn_player2";
            this.btn_player2.Size = new System.Drawing.Size(206, 134);
            this.btn_player2.TabIndex = 148;
            this.btn_player2.Text = "Player 2";
            this.btn_player2.UseVisualStyleBackColor = false;
            this.btn_player2.Click += new System.EventHandler(this.btn_player2_Click);
            // 
            // label_result
            // 
            this.label_result.AutoSize = true;
            this.label_result.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_result.ForeColor = System.Drawing.Color.Gray;
            this.label_result.Location = new System.Drawing.Point(153, 24);
            this.label_result.Name = "label_result";
            this.label_result.Size = new System.Drawing.Size(411, 65);
            this.label_result.TabIndex = 2;
            this.label_result.Text = "Who start player ?";
            this.label_result.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormChooseStarter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 497);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormChooseStarter";
            this.Text = "FormChooseStarter";
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_player1;
        private System.Windows.Forms.Button btn_player2;
        private System.Windows.Forms.Label label_result;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_random;
    }
}