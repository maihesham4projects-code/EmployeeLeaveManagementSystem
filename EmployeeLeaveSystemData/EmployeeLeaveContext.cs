using EmployeeLeaveSystemData.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveSystemData
{
    public class EmployeeLeaveContext : DbContext
    {

        public DbSet<Employee> employees { get; set; }
        public DbSet<LeaveRequest> leaveRequests { get; set; }




        private string _connectionString = "Server=LAPTOP-T25GT400;Database=EmployeeLeave;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(_connectionString);
        }
        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries<BaseEntity>();
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedAt = DateTime.Now;
                }
                else if (entity.State == EntityState.Modified)
                {
                    entity.Entity.UpdatedAt = DateTime.Now;
                }
            }


            return base.SaveChanges();
        }
    }
}
