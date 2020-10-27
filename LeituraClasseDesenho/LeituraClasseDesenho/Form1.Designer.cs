namespace LeituraClasseDesenho
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLer = new System.Windows.Forms.Button();
            this.picDesenho = new System.Windows.Forms.PictureBox();
            this.btnDesenhar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picDesenho)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLer
            // 
            this.btnLer.Location = new System.Drawing.Point(74, 53);
            this.btnLer.Name = "btnLer";
            this.btnLer.Size = new System.Drawing.Size(75, 23);
            this.btnLer.TabIndex = 0;
            this.btnLer.Text = "Ler";
            this.btnLer.UseVisualStyleBackColor = true;
            this.btnLer.Click += new System.EventHandler(this.btnLer_Click);
            // 
            // picDesenho
            // 
            this.picDesenho.BackColor = System.Drawing.Color.White;
            this.picDesenho.Location = new System.Drawing.Point(289, 48);
            this.picDesenho.Name = "picDesenho";
            this.picDesenho.Size = new System.Drawing.Size(300, 300);
            this.picDesenho.TabIndex = 1;
            this.picDesenho.TabStop = false;
            // 
            // btnDesenhar
            // 
            this.btnDesenhar.Location = new System.Drawing.Point(74, 114);
            this.btnDesenhar.Name = "btnDesenhar";
            this.btnDesenhar.Size = new System.Drawing.Size(75, 23);
            this.btnDesenhar.TabIndex = 2;
            this.btnDesenhar.Text = "Desenhar";
            this.btnDesenhar.UseVisualStyleBackColor = true;
            this.btnDesenhar.Click += new System.EventHandler(this.btnDesenhar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnDesenhar);
            this.Controls.Add(this.picDesenho);
            this.Controls.Add(this.btnLer);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picDesenho)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLer;
        private System.Windows.Forms.PictureBox picDesenho;
        private System.Windows.Forms.Button btnDesenhar;
    }
}

