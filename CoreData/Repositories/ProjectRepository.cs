using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using CoreData.Data;
using CoreData.Models;

namespace CoreData.Repositories
{
    public class ProjectRepository
    {
        private readonly DatabaseContext _db = new DatabaseContext();

        // 1. Tüm Görevleri (Personel İsimleriyle Birlikte) Getir
        public List<TaskItem> GetAllTasks()
        {
            var tasks = new List<TaskItem>();
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                // JOIN kullanarak personelin adını (r.Name) de çekiyoruz
                string sql = @"SELECT t.*, r.Name as PersonelName 
                               FROM Tasks t 
                               LEFT JOIN Resources r ON t.ResourceId = r.Id";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var task = new TaskItem
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                StartDate = (DateTime)reader["StartDate"],
                                DurationDays = (int)reader["Duration"],
                                AssignedResourceId = reader["ResourceId"] == DBNull.Value ? 0 : (int)reader["ResourceId"],
                                // SQL'den gelen PersonelName'i modele aktar
                                ResourceName = reader["PersonelName"]?.ToString() ?? "Atanmadı",
                                Dependencies = new List<int>()
                            };

                            string depStr = reader["Dependencies"]?.ToString();
                            if (!string.IsNullOrEmpty(depStr))
                            {
                                task.Dependencies = depStr.Split(',')
                                    .Select(int.Parse)
                                    .ToList();
                            }

                            tasks.Add(task);
                        }
                    }
                }
            }
            return tasks;
        }

        // 2. Yeni Görev Ekle
        public void AddTask(TaskItem task)
        {
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                string depStr = task.Dependencies != null ? string.Join(",", task.Dependencies) : "";

                string sql = "INSERT INTO Tasks (Name, StartDate, Duration, ResourceId, Dependencies) VALUES (@n, @s, @d, @r, @dep)";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@n", task.Name);
                    cmd.Parameters.AddWithValue("@s", task.StartDate);
                    cmd.Parameters.AddWithValue("@d", task.DurationDays);
                    cmd.Parameters.AddWithValue("@r", task.AssignedResourceId);
                    cmd.Parameters.AddWithValue("@dep", depStr);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 3. ComboBox'ı doldurmak için tüm personelleri getir
        public List<Resource> GetAllResources()
        {
            var resources = new List<Resource>();
            using (var conn = _db.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM Resources";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resources.Add(new Resource
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString()
                            });
                        }
                    }
                }
            }
            return resources;
        }
    }
}