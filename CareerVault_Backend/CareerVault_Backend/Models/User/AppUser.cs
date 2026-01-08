using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using CareerVault_Backend.Models.Job;

namespace CareerVault_Backend.Models.User
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public string Address { get; set; } = null!;

        public DateTime? DateOfBirth { get; set; } = null!;

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public DateTime? DeletedAt { get; set; }

        public bool IsActive { get; set; } = true;

        // OPTIONAL
        public Applicant? Applicant { get; set; }
        public Employee? Employee { get; set; }
    }
}
