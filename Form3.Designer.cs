using System.Windows.Forms;

namespace app
{
    partial class Form3
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
            this.components = new System.ComponentModel.Container();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button9 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Takvim = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 30;
            this.listBox1.Location = new System.Drawing.Point(12, 5);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(818, 424);
            this.listBox1.TabIndex = 0;
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // button9
            // 
            this.button9.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.button9.Location = new System.Drawing.Point(836, 273);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(416, 75);
            this.button9.TabIndex = 22;
            this.button9.Text = "Yönetim Paneli";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(836, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 32);
            this.label1.TabIndex = 15;
            this.label1.Text = "PERSONEL BİLGİ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label3.Location = new System.Drawing.Point(836, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(235, 32);
            this.label3.TabIndex = 17;
            this.label3.Text = "Ad Soyad Yer Tutucu";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label5.Location = new System.Drawing.Point(836, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(227, 32);
            this.label5.TabIndex = 19;
            this.label5.Text = "Pozisyon Yer Tutucu";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(1076, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(176, 32);
            this.label6.TabIndex = 20;
            this.label6.Text = "TARİH ve SAAT";
            // 
            // Takvim
            // 
            this.Takvim.AutoSize = true;
            this.Takvim.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.Takvim.Location = new System.Drawing.Point(1076, 63);
            this.Takvim.Name = "Takvim";
            this.Takvim.Size = new System.Drawing.Size(162, 32);
            this.Takvim.TabIndex = 21;
            this.Takvim.Text = "DD/MM/YYYY";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label8.Location = new System.Drawing.Point(1076, 116);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 32);
            this.label8.TabIndex = 24;
            this.label8.Text = "HH:MM";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label10.Location = new System.Drawing.Point(907, 194);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(288, 32);
            this.label10.TabIndex = 27;
            this.label10.Text = "Restaurant Adı Yer Tutucu";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 435);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1240, 234);
            this.flowLayoutPanel1.TabIndex = 30;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(836, 354);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(416, 75);
            this.button1.TabIndex = 31;
            this.button1.Text = "Çıkış Yap";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.Takvim);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1280, 720);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "Form3";
            this.ShowIcon = false;
            this.Text = "Adisyon Uygulaması";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_handleClose);
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ListBox listBox1;
        private Button button9;
        private Label label1;
        private Label label3;
        private Label label5;
        private Label label6;
        private Label Takvim;
        private Label label8;
        private Timer timer1;
        private Label label10;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button button1;
    }
}