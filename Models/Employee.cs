using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeData.Models
{
	public class Employee
	{
        public int id { get; set; }
        public string name { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string token { get; set; }
    }
}