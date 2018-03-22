using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BussinessLogic.Models
{
    public partial class SchoolDBContext : DbContext
    {
        public virtual DbSet<ClassRoom> ClassRoom { get; set; }
        public virtual DbSet<ClassRoomCourse> ClassRoomCourse { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentCourse> StudentCourse { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<User> User { get; set; }

        private static SchoolDBContext instance;

        public static SchoolDBContext Instance
        {
            get
            {
                if (instance == null)
                    instance = new SchoolDBContext();
                return instance;
            }
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-8GVJIE7;Database=SchoolDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassRoom>(entity =>
            {
                entity.Property(e => e.ClassRoomId).HasColumnName("ClassRoomID");

                entity.Property(e => e.ClassRoomName).HasMaxLength(20);

                entity.Property(e => e.TypeOfStudy).HasMaxLength(20);
            });

            modelBuilder.Entity<ClassRoomCourse>(entity =>
            {
                entity.HasKey(e => new { e.ClassRoomId, e.CourseId });

                entity.Property(e => e.ClassRoomId).HasColumnName("ClassRoomID");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.DayOfWeek).HasMaxLength(20);

                entity.HasOne(d => d.ClassRoom)
                    .WithMany(p => p.ClassRoomCourse)
                    .HasForeignKey(d => d.ClassRoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassRoomCourse_ClassRoom");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.ClassRoomCourse)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassRoomCourse_Course");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.Description).HasMaxLength(600);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Course)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_Teacher");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.CodCard)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Country).HasMaxLength(20);

                entity.Property(e => e.County).HasMaxLength(20);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.FatherInitial)
                    .IsRequired()
                    .HasColumnType("char(1)");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Image).IsRequired();

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Locality).HasMaxLength(20);

                entity.Property(e => e.Profile)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Series)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_User");
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.CourseId });

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.Validate).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentCourse)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCourse_Course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentCourse)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCourse_Student");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

                entity.Property(e => e.Email).HasMaxLength(30);

                entity.Property(e => e.Fct).HasMaxLength(20);

                entity.Property(e => e.More).HasMaxLength(400);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ProfessionalExperience).HasMaxLength(400);

                entity.Property(e => e.PublishedWorks).HasMaxLength(400);

                entity.Property(e => e.Studies).HasMaxLength(400);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Teacher)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Teacher_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(20);
            });
        }
    }
}
