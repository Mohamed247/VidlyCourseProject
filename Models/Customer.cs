using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; //[Required]
using System.Linq;
using System.Web;

namespace VidlyProject.Models
{
    public class Customer
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Please enter the customer's name.")] //means that it wont be nullable
        //errormessage to override default validation error message
        [StringLength(255)] //max len 255 instead of inf 
        public string Name { get; set; }


        public bool IsSubscribedToNewsletter { get; set; }


        [Display(Name = "Membership Type")]
        public MembershipType MembershipType { get; set; }  //navigation property, allows us to navigate from one type to another


        [Display(Name = "Membership Type")]

        public byte MembershipTypeId { get; set; } //foreign key of membership object for optimization purposes
        //implicitly required since its type is byte and therefore not nullable

        [Display(Name = "Date of Birth")]
        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }  //nullable 
    }
}