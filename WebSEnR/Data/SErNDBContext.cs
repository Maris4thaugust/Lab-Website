
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebSEnR.Models;
using WebSEnR.Models.AboutLabModel;
using WebSEnR.Models.ProjectsModel;

namespace WebSEnR.Data
{
    public class SErNDBContext : DbContext
    {
        public SErNDBContext(DbContextOptions<SErNDBContext> options) : base(options)
        {
            
        }
        public DbSet<labproduct> labproducts { get; set; }
        public DbSet<MinisProject> MinisProjects { get; set;}
        public DbSet<UniProject> Uniprojects { get; set; }
        public DbSet<enterprise_project> enterpriseprojects { get; set; }
        public DbSet<post_etp_queue> posts_etp_queue { get; set; }
        public DbSet<RegisterQueue> registerqueue { get; set; }
        public DbSet<post_lab> posts_lab { get; set; }
        public DbSet<post_etp> posts_etp { get; set; }
        public DbSet<LabMembers> lab_members { get; set; }
        public DbSet<Equipments> Equipments { get; set; }
        public DbSet<DocumentModel> DocumentModel { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<News> News { get; set; }
    }
}
