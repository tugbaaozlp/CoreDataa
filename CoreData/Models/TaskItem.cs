using System;
using System.Collections.Generic;
using System.ComponentModel; // Başlıkları Türkçeleştirmek için gerekli

namespace CoreData.Models
{
    public class TaskItem
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Görev Adı")]
        public string Name { get; set; }

        [DisplayName("Başlangıç Tarihi")]
        public DateTime StartDate { get; set; }

        [DisplayName("Süre (Gün)")]
        public int DurationDays { get; set; }

        [DisplayName("Bitiş Tarihi")]
        public DateTime EndDate => StartDate.AddDays(DurationDays);

        // CPM Alanları
        [DisplayName("Erken Başl.")]
        public int EarlyStart { get; set; }

        [DisplayName("Erken Bitiş")]
        public int EarlyFinish { get; set; }

        [DisplayName("Geç Başl.")]
        public int LateStart { get; set; }

        [DisplayName("Geç Bitiş")]
        public int LateFinish { get; set; }

        [DisplayName("Bolluk (Slack)")]
        public int Slack { get; set; }

        // Bu alan tabloda görünmesin diye DisplayName eklemiyoruz veya Visible=false yaparız
        public int AssignedResourceId { get; set; }

        // YENİ ALAN: İşi alan kişinin adı tabloda görünsün diye
        [DisplayName("Sorumlu Personel")]
        public string ResourceName { get; set; }

        public List<int> Dependencies { get; set; } = new List<int>();
    }
}