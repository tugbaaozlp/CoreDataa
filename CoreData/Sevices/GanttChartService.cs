using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CoreData.Models;

namespace CoreData.Services
{
    public class GanttChartService
    {
        private const int RowHeight = 40;
        private const int TaskHeight = 25;
        private const int DayWidth = 30;
        private const int HeaderHeight = 50;
        private const int NameColumnWidth = 150;

        public void DrawGanttChart(Graphics g, List<TaskItem> tasks, Rectangle clientRectangle)
        {
            // clientRectangle uyarısını ve null kontrollerini burada hallediyoruz
            if (tasks == null || !tasks.Any() || clientRectangle.Width <= 0) return;

            // Çizimin panel dışına taşmaması için Clipping (Kırpma) ekliyoruz
            g.SetClip(clientRectangle);

            DateTime startDate = tasks.Min(t => t.StartDate).AddDays(-1);

            for (int i = 0; i < tasks.Count; i++)
            {
                var task = tasks[i];
                int y = HeaderHeight + (i * RowHeight);

                // Görev ismini yaz
                g.DrawString(task.Name, SystemFonts.DefaultFont, Brushes.Black, 10, y + 5);

                // Pozisyon hesaplama
                int daysFromStart = (task.StartDate - startDate).Days;
                int xPos = NameColumnWidth + (daysFromStart * DayWidth);
                int width = task.DurationDays * DayWidth;

                // Kritik yol kontrolü (Slack 0 ise Salmon, değilse SkyBlue)
                Brush currentBrush = task.Slack == 0 ? Brushes.Salmon : Brushes.SkyBlue;

                Rectangle taskRect = new Rectangle(xPos, y, width, TaskHeight);

                // Çizim
                g.FillRectangle(currentBrush, taskRect);
                g.DrawRectangle(Pens.Black, taskRect);
            }

            // Çizim bitince klibi kaldırıyoruz
            g.ResetClip();
        }
    }
}