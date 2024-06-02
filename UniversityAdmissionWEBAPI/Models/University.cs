using System.ComponentModel.DataAnnotations;

namespace UniversityAdmissionWEBAPI.Models
{
    public class University
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Назва")]
        public string Name { get; set; } = string.Empty;
        public string WebSiteLink { get; set; } = string.Empty;
        public double? AvgUniverityAdmissionGrade { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<AdmissionRequest> AdmissionRequests { get; set; }

        public University()
        {
            AdmissionRequests = new List<AdmissionRequest>();
        }
    }
}