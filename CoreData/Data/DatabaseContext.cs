using System;
using Microsoft.Data.SqlClient;

namespace CoreData.Data
{
    public class DatabaseContext
    {
        // Server ismini görseldeki gibi 'LOQ\SQLEXPRESS' olarak güncelledik.
        private readonly string connectionString = @"Server=LOQ\SQLEXPRESS;Database=CoreDataDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public void InitializeDatabase()
        {
            // Master veritabanı bağlantısı için de sunucu adını güncelledik
            string masterConnString = @"Server=LOQ\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";

            using (var conn = new SqlConnection(masterConnString))
            {
                conn.Open();
                string createDbQuery = @"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'CoreDataDB')
                                        CREATE DATABASE CoreDataDB";
                using (var cmd = new SqlCommand(createDbQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            using (var conn = GetConnection())
            {
                conn.Open();

                // 1. Kaynaklar Tablosu
                string resTable = @"IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Resources')
                                   CREATE TABLE Resources (
                                       Id INT PRIMARY KEY IDENTITY(1,1), 
                                       Name NVARCHAR(100)
                                   )";

                // 2. Görevler Tablosu
                string taskTable = @"IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Tasks')
                                    CREATE TABLE Tasks (
                                        Id INT PRIMARY KEY IDENTITY(1,1), 
                                        Name NVARCHAR(250), 
                                        StartDate DATETIME, 
                                        Duration INT, 
                                        ResourceId INT,
                                        Dependencies NVARCHAR(MAX)
                                    )";

                using (var cmd = new SqlCommand(resTable, conn)) cmd.ExecuteNonQuery();
                using (var cmd = new SqlCommand(taskTable, conn)) cmd.ExecuteNonQuery();
            }
        }
    }
}