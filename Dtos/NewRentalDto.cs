using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VidlyProject.Models;

namespace VidlyProject.Dtos
{
    public class NewRentalDto
    {
        public int CusomterId { get; set; }
        public List<int> MovieIds { get; set; }
        
    }
}