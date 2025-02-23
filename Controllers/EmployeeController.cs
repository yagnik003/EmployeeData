using EmployeeData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace EmployeeData.Controllers
{
    public class EmployeeController : ApiController
    {
        DBMethods DB = new DBMethods();
        // GET: api/Employee
        public IHttpActionResult Get()
        {        
            var user = DB.allEmployee();
            return Ok(user);
        }

        // GET: api/Employee/5
        public IHttpActionResult Get(int id)
        {
            return Ok(DB.GetWithId(id));
        }

        // POST: api/Employee
        public IHttpActionResult Post([FromBody]Employee emp)
        {
            return Ok(DB.Create(emp));
        }

        // PUT: api/Employee/5
        public IHttpActionResult Put(int id, [FromBody] Employee emp)
        {
            return Ok(DB.UpdateEmpDetail(id, emp));
        }

        // DELETE: api/Employee/5
        public IHttpActionResult Delete(int id)
        {
            return Ok(DB.RemoveEmp(id));
        }
    }
}
