using back.Modules;
using Microsoft.EntityFrameworkCore;

namespace back.Context
{
    public class CourseContext : DbContext
    {
       
        public DbSet<Course> course { get; set; }
        public DbSet<KnowledgePoint> knowledgePoints { get; set; }

        public DbSet<WebSite> webSites { get; set; }
        public CourseContext(DbContextOptions<CourseContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Course>().ToTable("course");
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseId);
                entity.Property(e => e.CourseId).HasColumnName("courseId");
                entity.Property(e => e.CourseName).HasColumnName("courseName");
                entity.Property(e => e.CreateTime).HasColumnName("createTime");
                entity.Property(e => e.CourseCreatorId).HasColumnName("courseCreatorId");
                entity.Property(e => e.CourseState).HasColumnName("courseState");
            });
            modelBuilder.Entity<WebSite>().ToTable("website");
            modelBuilder.Entity<WebSite>(entity =>
            {
                entity.HasKey(e => e.WebSiteId);
                entity.Property(e => e.WebSiteId).HasColumnName("websiteId");
                entity.Property(e => e.WebSiteType).HasColumnName("WebSiteType");
                entity.Property(e => e.WebSiteName).HasColumnName("WebSiteName");
                entity.Property(e => e.WebSiteUrl).HasColumnName("WebSiteUrl");
                entity.Property(e => e.CourseId).HasColumnName("courseId");

            });
            modelBuilder.Entity<KnowledgePoint>().ToTable("knowledgePoint");
            modelBuilder.Entity<KnowledgePoint>(entity =>
            {
                entity.HasKey(e => e.KnowledgeId);
                entity.Property(e => e.KnowledgeId).HasColumnName("knowledgePointId");
                entity.Property(e => e.KnowledgePointContent).HasColumnName("knowledgePointContent");
                entity.Property(e => e.KnowledgePointName).HasColumnName("knowledgePointName");
                entity.Property(e => e.CourseId).HasColumnName("courseId");

            });
        }
    }
}
