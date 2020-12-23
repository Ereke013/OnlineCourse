using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineCourse.Models
{
    public class UserEdit
    {

        public string Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }


        [Required]
        [Display(Name = "FullName")]
        public string FullName { get; set; }

    }
}