
namespace VetAM
{
    partial class Form1
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
            this.btnZatvori = new System.Windows.Forms.Button();
            this.btnPrijava = new System.Windows.Forms.Button();
            this.pnlIme = new System.Windows.Forms.Panel();
            this.pnlLozinka = new System.Windows.Forms.Panel();
            this.txtLozinka = new System.Windows.Forms.TextBox();
            this.txtIme = new System.Windows.Forms.TextBox();
            this.lblGreska = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnZatvori
            // 
            this.btnZatvori.FlatAppearance.BorderSize = 0;
            this.btnZatvori.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZatvori.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZatvori.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(184)))), ((int)(((byte)(206)))));
            this.btnZatvori.Location = new System.Drawing.Point(285, 3);
            this.btnZatvori.Name = "btnZatvori";
            this.btnZatvori.Size = new System.Drawing.Size(33, 32);
            this.btnZatvori.TabIndex = 0;
            this.btnZatvori.Text = "X";
            this.btnZatvori.UseVisualStyleBackColor = true;
            this.btnZatvori.Click += new System.EventHandler(this.btnZatvori_Click);
            // 
            // btnPrijava
            // 
            this.btnPrijava.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(184)))), ((int)(((byte)(206)))));
            this.btnPrijava.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrijava.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrijava.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.btnPrijava.Location = new System.Drawing.Point(96, 256);
            this.btnPrijava.Name = "btnPrijava";
            this.btnPrijava.Size = new System.Drawing.Size(124, 33);
            this.btnPrijava.TabIndex = 10;
            this.btnPrijava.Text = "Prijavi se";
            this.btnPrijava.UseVisualStyleBackColor = false;
            this.btnPrijava.Click += new System.EventHandler(this.btnPrijava_Click);
            // 
            // pnlIme
            // 
            this.pnlIme.BackColor = System.Drawing.Color.White;
            this.pnlIme.Location = new System.Drawing.Point(35, 141);
            this.pnlIme.Name = "pnlIme";
            this.pnlIme.Size = new System.Drawing.Size(250, 1);
            this.pnlIme.TabIndex = 6;
            // 
            // pnlLozinka
            // 
            this.pnlLozinka.BackColor = System.Drawing.Color.White;
            this.pnlLozinka.Location = new System.Drawing.Point(35, 198);
            this.pnlLozinka.Name = "pnlLozinka";
            this.pnlLozinka.Size = new System.Drawing.Size(250, 1);
            this.pnlLozinka.TabIndex = 9;
            // 
            // txtLozinka
            // 
            this.txtLozinka.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.txtLozinka.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLozinka.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLozinka.ForeColor = System.Drawing.Color.White;
            this.txtLozinka.Location = new System.Drawing.Point(35, 173);
            this.txtLozinka.Name = "txtLozinka";
            this.txtLozinka.PasswordChar = '*';
            this.txtLozinka.Size = new System.Drawing.Size(250, 19);
            this.txtLozinka.TabIndex = 8;
            this.txtLozinka.Text = "Lozinka";
            this.txtLozinka.Click += new System.EventHandler(this.txtLozinka_Click);
            this.txtLozinka.TextChanged += new System.EventHandler(this.txtLozinka_TextChanged);
            this.txtLozinka.Leave += new System.EventHandler(this.txtLozinka_Leave);
            // 
            // txtIme
            // 
            this.txtIme.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.txtIme.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIme.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIme.ForeColor = System.Drawing.Color.White;
            this.txtIme.Location = new System.Drawing.Point(35, 116);
            this.txtIme.Name = "txtIme";
            this.txtIme.Size = new System.Drawing.Size(250, 19);
            this.txtIme.TabIndex = 7;
            this.txtIme.Text = "korisničko ime";
            this.txtIme.Click += new System.EventHandler(this.txtIme_Click);
            this.txtIme.TextChanged += new System.EventHandler(this.txtIme_TextChanged);
            this.txtIme.Leave += new System.EventHandler(this.txtIme_Leave);
            // 
            // lblGreska
            // 
            this.lblGreska.AutoSize = true;
            this.lblGreska.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreska.ForeColor = System.Drawing.Color.Red;
            this.lblGreska.Location = new System.Drawing.Point(50, 211);
            this.lblGreska.Name = "lblGreska";
            this.lblGreska.Size = new System.Drawing.Size(220, 16);
            this.lblGreska.TabIndex = 11;
            this.lblGreska.Text = "Pogrešno korisničko ime i/ili lozinka.";
            this.lblGreska.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(321, 366);
            this.Controls.Add(this.lblGreska);
            this.Controls.Add(this.btnPrijava);
            this.Controls.Add(this.pnlIme);
            this.Controls.Add(this.pnlLozinka);
            this.Controls.Add(this.txtLozinka);
            this.Controls.Add(this.txtIme);
            this.Controls.Add(this.btnZatvori);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnZatvori;
        private System.Windows.Forms.Button btnPrijava;
        private System.Windows.Forms.Panel pnlIme;
        private System.Windows.Forms.Panel pnlLozinka;
        private System.Windows.Forms.TextBox txtLozinka;
        private System.Windows.Forms.TextBox txtIme;
        private System.Windows.Forms.Label lblGreska;
    }
}

