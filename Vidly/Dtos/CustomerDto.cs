using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        //required method with the validation warning - this is optional
        [Required(ErrorMessage = "please enter a customer's name")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }


        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }

        //foreign key
        public byte MembershipTypeId { get; set; }


    }
}