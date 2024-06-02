using System.ComponentModel.DataAnnotations;

namespace UniversityAdmissionWEBAPI.Models
{
    public class FAQ
    {
        [Key]
        public int Id { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }
}