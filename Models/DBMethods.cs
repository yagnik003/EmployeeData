using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmployeeData.Models
{
	public class DBMethods
	{
		SqlConnection conn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Employee;Trusted_Connection=True");

		public List<Employee> allEmployee()
		{
			List<Employee> empList = new List<Employee>();

			string query = "Select id,name,role from emp";
			SqlCommand cmd = new SqlCommand(query, conn);

			try
			{
				conn.Open();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						empList.Add(new Employee {
							id = reader.GetInt32(0),
							email = "Confidential",
							password = "Confidential",
                            name = reader.GetString(1),
							role = reader.GetString(2),
						});
					}
				}

			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
			finally
			{
				conn.Close();
			}

			return empList;
		}

		public Employee Create(Employee emp)
		{
			Employee createdEmp = new Employee();
            string queryCheck = $"select email from emp where email='{emp.email}'";
            SqlCommand cmdCheck = new SqlCommand(queryCheck, conn);
            string query = $"Insert into emp (name,email,password,role) values ('{emp.name}','{emp.email}','{emp.password}','{emp.role}')";
			SqlCommand cmd = new SqlCommand(query, conn);
			string querySelect = $"Select id,name,email,password,role from emp where email='{emp.email}'";
			SqlCommand cmdSelect = new SqlCommand(querySelect, conn);

			try
			{
				conn.Open();
				var isValid = cmdCheck.ExecuteScalar();
				if(isValid == null)
				{
                    cmd.ExecuteNonQuery();
                    using (SqlDataReader reader = cmdSelect.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            createdEmp.id = reader.GetInt32(0);
                            createdEmp.name = reader.GetString(1);
                            createdEmp.email = reader.GetString(2);
                            createdEmp.password = reader.GetString(3);
                            createdEmp.role = reader.GetString(4);
                        }
                        else
                        {
                            createdEmp.id = 0;
                            createdEmp.name = "User Can't be created";
                            createdEmp.email = "User Can't be created";
                            createdEmp.password = "User Can't be created";
                            createdEmp.role = "User Can't be created";
                        }
                    }
                }
				else
				{
                    createdEmp.id = 0;
                    createdEmp.name = "User Can't be created";
                    createdEmp.email = "User Can't be created";
                    createdEmp.password = "User Can't be created";
                    createdEmp.role = "User Can't be created";
                }
				
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw;
			}
			finally
			{
				conn.Close();
			}

			return createdEmp;
		}

		public Employee GetWithId(int id)
		{
			Employee emp = new Employee();
            string query = $"Select id,name,role,email,password from emp where id={id}";
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if(reader.Read())
                      {
						emp.id = reader.GetInt32(0);
						emp.email = reader.GetString(3);
						emp.password = reader.GetString(4);
						emp.name = reader.GetString(1);
						emp.role = reader.GetString(2);
                      }
                    else
                    {
                        emp.id = 0;
                        emp.email = "Emp Doesn't Exists";
                        emp.password = "Emp Doesn't Exists";
                        emp.name = "Emp Doesn't Exists";
                        emp.role = "Emp Doesn't Exists";
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                conn.Close();
            }
            return emp;
		}

		public Employee RemoveEmp(int id)
		{
            Employee removedEmp = new Employee();
            string queryCheck = $"select id,name,role,email,password from emp where id={id}";
            SqlCommand cmdCheck = new SqlCommand(queryCheck, conn);
            string query = $"Delete from emp where id={id}";
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = cmdCheck.ExecuteReader();
                if (reader.Read())
                {
                    removedEmp.id = reader.GetInt32(0);
                    removedEmp.name = reader.GetString(1);
                    removedEmp.email = reader.GetString(3);
                    removedEmp.password = reader.GetString(4);
                    removedEmp.role = reader.GetString(2);
                    conn.Close();
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    removedEmp.id = 0;
                    removedEmp.name = "User Doesn't Exists";
                    removedEmp.email = "User Doesn't Exists";
                    removedEmp.password = "User Doesn't Exists";
                    removedEmp.role = "User Doesn't Exists";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                conn.Close();
            }

            return removedEmp;
        }

        public Employee UpdateEmpDetail(int id,Employee emp)
        {
            Employee updatedEmp = new Employee();
            
            

            try
            {
                Employee OldDetail = GetWithId(id);
                if (OldDetail.id != 0)
                {
                    updatedEmp.id = (emp.id == null || emp.id == 0) ? OldDetail.id : 0;
                    updatedEmp.name = (emp.name == null || emp.name == "") ? OldDetail.name : emp.name;
                    updatedEmp.role = (emp.role == null || emp.role == "") ? OldDetail.role : emp.role;
                    updatedEmp.password = (emp.password == null || emp.password == "") ? OldDetail.password : emp.password;
                    updatedEmp.email = (emp.email == null || emp.email == "") ? OldDetail.email : "Cant Update Email";
                    string query = $"update emp set name='{updatedEmp.name}',role='{updatedEmp.role}',password='{updatedEmp.password}' where id={id}";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();

                }
                else
                {
                    updatedEmp.id = 0;
                    updatedEmp.email = "Emp Doesn't Exists";
                    updatedEmp.password = "Emp Doesn't Exists";
                    updatedEmp.name = "Emp Doesn't Exists";
                    updatedEmp.role = "Emp Doesn't Exists";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                conn.Close();
            }
        

            return updatedEmp;
        }
	}
}