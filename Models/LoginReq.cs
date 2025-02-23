using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeData.Models
{
	public class LoginReq
	{
        public string email { get; set; }
        public string password { get; set; }
    }
}