﻿using System.ComponentModel.DataAnnotations;

namespace UniversityAdmissionWEBAPI.Models
{
    public class AdmissionRequest
    {
        [Key]
        public int Id { get; set; }
        public virtual Entrant Entrant { get; set; }
        public virtual University University { get; set; }
    }
}