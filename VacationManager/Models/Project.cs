﻿using System.ComponentModel.DataAnnotations;

namespace VacationManager.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        public virtual ICollection<Team>? Teams { get; set;}
    }
}
