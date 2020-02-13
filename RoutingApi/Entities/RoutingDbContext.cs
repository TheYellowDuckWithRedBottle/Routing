using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutingApi.Entities
{
    public class RoutingDbContext:DbContext//DbCOntext需要options才能工作，所以再构造函数中加入
    {
        public RoutingDbContext(DbContextOptions<RoutingDbContext> options):base(options)
        {

        }
        /// <summary>
        /// 把实体映射到表
        /// </summary>
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> employees { get; set; }
        /// <summary>
        /// 对实体的一些限制
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .Property(x => x.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Company>()
                    .Property(x => x.Introduction).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<Employee>()
                .Property(x => x.EmployeeNo).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Employee>()
                .Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>()
                .Property(x => x.LastName).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Employee>().HasOne(navigationExpression: x => x.Company)
                .WithMany(navigationExpression: x => x.Employees)
                .HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Company>().HasData(
                new Company { 
            Id=Guid.NewGuid(),
            Name="Mircrosoft",
            Introduction="Great Company"
            },
           new Company
           {
              
               Id=Guid.NewGuid(),
               Name="Google",
               Introduction="Dont be evil"
           },
           new Company
           {
               Id=Guid.NewGuid(),
               Name="Alipapa",
               Introduction="Fubao Company"
           }
            );
        
        }
    }
}
