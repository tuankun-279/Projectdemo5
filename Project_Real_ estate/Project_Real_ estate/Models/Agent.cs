//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project_Real__estate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Agent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Agent()
        {
            this.Advertisements = new HashSet<Advertisement>();
            this.Reports = new HashSet<Report>();
        }

        public int AgentId { get; set; }

        [Required, StringLength(maximumLength: 50, MinimumLength = 3),Display(Name ="Agent Name")]
        public string AgentName { get; set; }

        [Required, StringLength(50)]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
            ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }
        public bool isActivate { get; set; }

        [Required, StringLength(50)]
        [RegularExpression(@"(?=.*\d)(?=.*[A-Za-z]).{8,}",
            ErrorMessage = "Password minimum 8 characters at least 1 letter and 1 number")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required,Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }
        public string Introduction { get; set; }
        [Display(Name ="Email Hide")]
        public bool EmailHide { get; set; }
        [Display(Name = "Payment Type")]
        public int? paymentId { get; set; }
        public Nullable<int> UserId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Advertisement> Advertisements { get; set; }
        public virtual User User { get; set; }
        public virtual Payment Payment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Report> Reports { get; set; }
    }
    public enum EmailHide
    {
        Show = 0,
        Hide = 1
    }
    public enum isActivate
    {
        NotActive = 0,
        Active = 1
    }
}
