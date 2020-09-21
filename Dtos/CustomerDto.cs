using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VidlyProject.Models;

namespace VidlyProject.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Please enter the customer's name.")] //means that it wont be nullable
        //errormessage to override default validation error message
        [StringLength(255)] //max len 255 instead of inf 
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public MembershipTypeDto MembershipType; //variables must be same name as original class
        public byte MembershipTypeId { get; set; } //foreign key of membership object for optimization purposes
        //implicitly required since its type is byte and therefore not nullable

        //[Min18YearsIfAMember] would thow and exception since we 
        //are casting the instance to a cutomer not a customerdto in the main validation class
        public DateTime? Birthdate { get; set; }  //nullable 
    }
}