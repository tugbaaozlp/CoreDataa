using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using CoreData.Models;
using CoreData.Services;
using CoreData.Repositories;
using CoreData.Data;

namespace CoreData
{
    public partial class MainForm : Form
    {
        private readonly ProjectRepository _repo = new ProjectRepository();
        private readonly CPMService _cpmService = new CPMService();
        private readonly GanttChartService _ganttService = new GanttChartService();
        private List<CoreData.Models.TaskItem> _taskList = new List<CoreData.Models.TaskItem>();

        public MainForm()
        {
            InitializeComponent();
            try
            {
                DatabaseContext db = new DatabaseContext();
                db.InitializeDatabase();

                // PROGRAM AÇILDIĞINDA PERSONEL LİSTESİNİ DOLDUR
                LoadResourcesIntoComboBox();
            }
            catch { /* Hata yönetimi */ }

            pnlGantt.Paint += PnlGantt_Paint;
            this.Resize += (s, e) => pnlGantt.Invalidate();
        }

        private void LoadResourcesIntoComboBox()
        {
            try
            {
                var resources = _repo.GetAllResources();

                if (resources != null && resources.Count > 0)
                {
                    cmbResources.DataSource = resources;
                    cmbResources.DisplayMember = "Name";
                    cmbResources.ValueMember = "Id";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Personel listesi yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            // 1. Verileri SQL'den çek
            _taskList = _repo.GetAllTasks();

            if (_taskList == null || _taskList.Count == 0)
            {
                MessageBox.Show("Hesaplanacak görev bulunamadı.");
                return;
            }

            // 2. CPM Algoritmasını çalıştır
            _cpmService.CalculateCriticalPath(_taskList);

            // 3. Çakışma kontrolü yap
            var conflicts = _cpmService.CheckResourceConflicts(_taskList);
            if (conflicts != null && conflicts.Count > 0)
            {
                MessageBox.Show(string.Join("\n", conflicts), "Kaynak Çakışması Tespit Edildi!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // 4. Gantt Panelini tazele
            pnlGantt.Invalidate();

            // 5. Tabloyu doldur ve kolonları düzenle
            dgvTasks.DataSource = null;
            dgvTasks.DataSource = _taskList;

            // Sayısal ID kolonunu kullanıcı görmesin, sadece isim kalsın:
            if (dgvTasks.Columns["AssignedResourceId"] != null)
                dgvTasks.Columns["AssignedResourceId"].Visible = false;

            // Eğer istersen Dependencies kolonunu da gizleyebilirsin (daha temiz görünür):
            if (dgvTasks.Columns["Dependencies"] != null)
                dgvTasks.Columns["Dependencies"].Visible = false;
        }

        private void PnlGantt_Paint(object sender, PaintEventArgs e)
        {
            _ganttService.DrawGanttChart(e.Graphics, _taskList, pnlGantt.ClientRectangle);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskName.Text))
            {
                MessageBox.Show("Görev adı boş olamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int selectedResourceId = (cmbResources.SelectedValue != null) ? (int)cmbResources.SelectedValue : 1;

            var newTask = new CoreData.Models.TaskItem
            {
                Name = txtTaskName.Text,
                StartDate = dtpStart.Value.Date,
                DurationDays = (int)numDuration.Value,
                AssignedResourceId = selectedResourceId,
                Dependencies = new List<int>()
            };

            try
            {
                _repo.AddTask(newTask);
                MessageBox.Show($"'{newTask.Name}' adlı görev {cmbResources.Text} personeline atandı.", "Başarılı");

                txtTaskName.Clear();
                numDuration.Value = 1;
                dtpStart.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydedilirken hata oluştu: " + ex.Message);
            }
        }
    }
}