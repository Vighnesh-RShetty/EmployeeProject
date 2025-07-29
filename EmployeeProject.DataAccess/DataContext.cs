//using EmployeeProject.DataAccess.Models;
using EmployeeProject.DataAccess.Models;
using EmployeeProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EmployeeProject
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string sqlquery = "Server=VIGNESH;Database=EmployeeDb;Trusted_Connection=True;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(sqlquery, actionProvider => actionProvider.CommandTimeout(60)).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }
        public DbSet<CompanySector> CompanySectors { get; set; }
    }
}

