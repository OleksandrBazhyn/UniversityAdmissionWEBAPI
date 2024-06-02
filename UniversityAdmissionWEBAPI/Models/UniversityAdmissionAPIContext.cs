using Microsoft.EntityFrameworkCore;

namespace UniversityAdmissionWEBAPI.Models
{
    public class UniversityAdmissionAPIContext : DbContext
    {
        public virtual DbSet<AdmissionRequest> AdmissionRequests { get; set; }
        public virtual DbSet<Entrant> Entrants { get; set; }
        public virtual DbSet<FAQ> FAQs { get; set; }
        public virtual DbSet<University> Universities { get; set; }

        public UniversityAdmissionAPIContext(DbContextOptions<UniversityAdmissionAPIContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}