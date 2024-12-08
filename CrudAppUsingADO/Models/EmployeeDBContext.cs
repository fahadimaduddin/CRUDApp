using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CrudAppUsingADO.Models
{
    public class EmployeeDBContext
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public List<Employee> GetEmployees()
        {
            List<Employee> EmployeeLists = new List<Employee>();
            SqlConnection con = new SqlConnection(cs);
            string spquery = "spGetAllEmployees";
            SqlCommand cmd = new SqlCommand(spquery, con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr =  cmd.ExecuteReader();
            while(dr.Read())
            {
                Employee emp = new Employee();
                emp.ID = Convert.ToInt32(dr.GetValue(0).ToString());
                emp.Name = dr.GetValue(1).ToString();
                emp.Gender = dr.GetValue(2).ToString();
                emp.Salary = Convert.ToInt32(dr.GetValue(3).ToString());
                emp.City = dr.GetValue(4).ToString();
                EmployeeLists.Add(emp);
            }
            con.Close();
            return EmployeeLists;
        }

        public bool AddEmployee(Employee emp)
        {
            SqlConnection con = new SqlConnection(cs);
            string spquery = "spAddEmployees";
            SqlCommand cmd = new SqlCommand(spquery, con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Name",emp.Name);
            cmd.Parameters.AddWithValue("@Gender", emp.Gender);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            cmd.Parameters.AddWithValue("@City", emp.City);
            int a = cmd.ExecuteNonQuery();
            con.Close();
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateEmployee(Employee emp)
        {
            SqlConnection con = new SqlConnection(cs);
            string spquery = "spUpdateEmployees";
            SqlCommand cmd = new SqlCommand(spquery, con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@ID", emp.ID);
            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Gender", emp.Gender);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            cmd.Parameters.AddWithValue("@City", emp.City);
            int a = cmd.ExecuteNonQuery();
            con.Close();
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteEmployee(int id)
        {
            SqlConnection con = new SqlConnection(cs);
            string spquery = "spDeleteEmployees";
            SqlCommand cmd = new SqlCommand(spquery, con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@ID", id);
            int a = cmd.ExecuteNonQuery();
            con.Close();
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}