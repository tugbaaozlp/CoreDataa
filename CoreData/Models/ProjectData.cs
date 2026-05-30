using System.Collections.Generic;
using CoreData.Models; // TaskItem ve Resource sınıflarını görebilmesi için bu satır şart!

namespace CoreData.Models
{
    public class ProjectData
    {
        // Başlangıçta null hatası almamak için listeleri örnekliyoruz (initialize)
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
        public List<Resource> Resources { get; set; } = new List<Resource>();
    }
}