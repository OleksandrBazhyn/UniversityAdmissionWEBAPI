using System.ComponentModel.DataAnnotations;

namespace UniversityAdmissionWEBAPI.Models
{
    public class Entrant
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        public double NationalExamGrade { get; set; }

        public bool IsPrivileged { get; set; }

        public virtual ICollection<AdmissionRequest> AdmissionRequests { get; set; }

        public Entrant()
        {
            AdmissionRequests = new List<AdmissionRequest>();
        }
    }
}