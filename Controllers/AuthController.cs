using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using EmployeeData.Models;

namespace EmployeeData.Controllers
{
    public class AuthController : ApiController
    {

        [HttpPost]
        // POST: api/Auth
        public IHttpActionResult Login([FromBody] LoginReq request)
        {
            if(Request == null || string.IsNullOrEmpty(request.email) || string.IsNullOrEmpty(request.password))
            {
                return BadRequest("Email and Password are required!!");
            }
            SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Employee;Trusted_Connection=True");
            using (conn)
            {
                conn.Open();
                string query = $"select password from emp where email='{request.email}'";
                SqlCommand cmd = new SqlCommand(query, conn);
                object storedPassword = cmd.ExecuteScalar();
                if(storedPassword != null && storedPassword.ToString() == request.password)
                {
                    return Ok(new { message = "Login Successfully!!", token = "tempToken" });
                }
                else
                {
                    return Unauthorized();
                }
            }

        }
    }
}
