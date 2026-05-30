using System;
using System.Collections.Generic;
using System.Linq;
using CoreData.Models;

namespace CoreData.Services
{
    public class CPMService
    {
        public void CalculateCriticalPath(List<TaskItem> tasks)
        {
            if (tasks == null || !tasks.Any()) return;

            // 1. İleri Hesaplama (Early Start - Early Finish)
            foreach (var task in tasks.OrderBy(t => t.Id))
            {
                if (task.Dependencies == null || !task.Dependencies.Any())
                {
                    task.EarlyStart = 0;
                }
                else
                {
                    // Bağımlı olduğu görevlerin en geç bitişini başlangıç al
                    task.EarlyStart = tasks.Where(t => task.Dependencies.Contains(t.Id))
                                           .Max(t => t.EarlyFinish);
                }
                task.EarlyFinish = task.EarlyStart + task.DurationDays;
            }

            // 2. Proje Toplam Süresi
            int projectDuration = tasks.Max(t => t.EarlyFinish);

            // 3. Geri Hesaplama (Late Start - Late Finish)
            foreach (var task in tasks.OrderByDescending(t => t.Id))
            {
                var successors = tasks.Where(t => t.Dependencies != null && t.Dependencies.Contains(task.Id)).ToList();
                if (!successors.Any())
                {
                    task.LateFinish = projectDuration;
                }
                else
                {
                    task.LateFinish = successors.Min(s => s.LateStart);
                }
                task.LateStart = task.LateFinish - task.DurationDays;
                task.Slack = task.LateStart - task.EarlyStart; // Bolluk süresi
            }
        }

        public List<string> CheckResourceConflicts(List<TaskItem> tasks)
        {
            var conflicts = new List<string>();

            // Aynı personelin görevlerini grupla
            var groupedTasks = tasks.GroupBy(t => t.AssignedResourceId);

            foreach (var group in groupedTasks)
            {
                var personTasks = group.OrderBy(t => t.StartDate).ToList();
                for (int i = 0; i < personTasks.Count; i++)
                {
                    for (int j = i + 1; j < personTasks.Count; j++)
                    {
                        var t1 = personTasks[i];
                        var t2 = personTasks[j];

                        // Tarih çakışması kontrolü: (Bas1 < Bit2) ve (Bas2 < Bit1)
                        if (t1.StartDate < t2.EndDate && t2.StartDate < t1.EndDate)
                        {
                            conflicts.Add($"UYARI: Personel ID-{t1.AssignedResourceId} aynı anda hem '{t1.Name}' hem de '{t2.Name}' işinde görevli!");
                        }
                    }
                }
            }
            return conflicts;
        }
    }
}