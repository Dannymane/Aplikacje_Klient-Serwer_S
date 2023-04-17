using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolRegister.Model.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.DAL.EF
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        // table properties
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SubjectGroup> SubjectGroups { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //configuration commands
            optionsBuilder.UseLazyLoadingProxies(); //enable lazy loading proxies
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Fluent API commands
            modelBuilder.Entity<User>()
            .ToTable("AspNetUsers")
            .HasDiscriminator<int>("UserType")
            .HasValue<User>((int)RoleValue.User)
            .HasValue<Student>((int)RoleValue.Student)
            .HasValue<Parent>((int)RoleValue.Parent)
            .HasValue<Teacher>((int)RoleValue.Teacher);

            modelBuilder.Entity<Group>() // Set Group primary key to Id property
                .HasKey(g => g.Id);

            modelBuilder.Entity<Group>() // Make Group Name property required
                .Property(g => g.Name)
                .IsRequired();

            modelBuilder.Entity<Group>() 
                .HasMany(g => g.Students)
                .WithOne(s => s.Group)
                .HasForeignKey(x => x.GroupId)
                .IsRequired();

            modelBuilder.Entity<SubjectGroup>()
                .HasKey(sg => new { sg.GroupId, sg.SubjectId });

            modelBuilder.Entity<SubjectGroup>()
                .HasOne(sg => sg.Group)
                .WithMany(g => g.SubjectGroups)
                .HasForeignKey(sg => sg.GroupId);

            modelBuilder.Entity<SubjectGroup>()
                .HasOne(sg => sg.Subject)
                .WithMany(s => s.SubjectGroups)
                .HasForeignKey(sg => sg.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subject>()
                .HasKey(s =>s.Id);

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Teacher)
                .WithMany(t => t.Subjects)
                .HasForeignKey(s => s.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grade>()
                .HasKey(g => new { g.DateOfIssue, g.SubjectId, g.StudentId });

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Subject)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.SubjectId);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentId);
        }
    }
}
