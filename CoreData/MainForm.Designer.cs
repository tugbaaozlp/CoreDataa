namespace CoreData
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvTasks = new System.Windows.Forms.DataGridView();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.pnlGantt = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtTaskName = new System.Windows.Forms.TextBox();
            this.numDuration = new System.Windows.Forms.NumericUpDown();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbResources = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTasks
            // 
            this.dgvTasks.Location = new System.Drawing.Point(20, 20);
            this.dgvTasks.Name = "dgvTasks";
            this.dgvTasks.Size = new System.Drawing.Size(1294, 300);
            this.dgvTasks.TabIndex = 3;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(953, 709);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(150, 30);
            this.btnCalculate.TabIndex = 2;
            this.btnCalculate.Text = "Hesapla ve Çiz";
            this.btnCalculate.Click += new System.EventHandler(this.BtnCalculate_Click);
            // 
            // pnlGantt
            // 
            this.pnlGantt.BackColor = System.Drawing.Color.White;
            this.pnlGantt.Location = new System.Drawing.Point(507, 343);
            this.pnlGantt.Name = "pnlGantt";
            this.pnlGantt.Size = new System.Drawing.Size(807, 360);
            this.pnlGantt.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(1164, 709);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Yeni Görev Ekle";
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // txtTaskName
            // 
            this.txtTaskName.Location = new System.Drawing.Point(86, 382);
            this.txtTaskName.Name = "txtTaskName";
            this.txtTaskName.Size = new System.Drawing.Size(144, 20);
            this.txtTaskName.TabIndex = 4;
            // 
            // numDuration
            // 
            this.numDuration.Location = new System.Drawing.Point(86, 427);
            this.numDuration.Name = "numDuration";
            this.numDuration.Size = new System.Drawing.Size(144, 20);
            this.numDuration.TabIndex = 5;
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(278, 382);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(200, 20);
            this.dtpStart.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 382);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Yeni Görev:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 427);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Süre:";
            // 
            // cmbResources
            // 
            this.cmbResources.FormattingEnabled = true;
            this.cmbResources.Location = new System.Drawing.Point(110, 464);
            this.cmbResources.Name = "cmbResources";
            this.cmbResources.Size = new System.Drawing.Size(120, 21);
            this.cmbResources.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 467);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Sorumlu Personel:";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1338, 751);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbResources);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.numDuration);
            this.Controls.Add(this.txtTaskName);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pnlGantt);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.dgvTasks);
            this.Name = "MainForm";
            this.Text = "CoreData Proje Yönetimi";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.DataGridView dgvTasks;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Panel pnlGantt;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtTaskName;
        private System.Windows.Forms.NumericUpDown numDuration;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbResources;
        private System.Windows.Forms.Label label3;
    }
}