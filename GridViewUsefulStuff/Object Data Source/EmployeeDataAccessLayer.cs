using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GridViewUsefulStuff
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
    }

    public class EmployeeDataAccessLayer
    {
        public static List<Employee> GetAllEmployees()
        {
            /*
             * SELECT METHOD FROM DATABASE
             */
            List<Employee> listEmployees = new List<Employee>();
            string CS = ConfigurationManager.ConnectionStrings["QuotationDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("select * from EmployeeASPNETTutorial", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Employee employee = new Employee();
                    employee.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.City = rdr["Cty"].ToString();

                    listEmployees.Add(employee);
                }
            }
            return listEmployees;
        }
        public static void DeleteEmployee(int EmployeeId)
        {
            /*
             * UPDATE SQL
             */
            string CS = ConfigurationManager.ConnectionStrings["QuotationDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand
                    ("Delete from EmployeeASPNETTutorial where EmployeeId = @EmployeeId",con);
                SqlParameter param = new SqlParameter("@EmployeeId", EmployeeId);
                cmd.Parameters.Add(param);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static int UpdateEmployee(int EmployeeId, string Name, string Gender, string City)
        {
            /*
             *UPDATE SQL 
             */
            string CS = ConfigurationManager.ConnectionStrings["QuotationDB"].ConnectionString;
            using(SqlConnection con = new SqlConnection(CS))
            {
                string updateQuarry = "update EmployeeASPNETTutorial set Name = @Name, " +
                    "Gender = @Gender, Cty = @City where EmployeeId = @EmployeeId";
                SqlCommand cmd = new SqlCommand(updateQuarry,con);
                SqlParameter paramOriginalEmployeeId = new SqlParameter("@EmployeeId", EmployeeId);
                cmd.Parameters.Add(paramOriginalEmployeeId);
                SqlParameter paramOriginalNameId = new SqlParameter("@Name", Name);
                cmd.Parameters.Add(paramOriginalNameId);
                SqlParameter paramOriginalGenderId = new SqlParameter("@Gender", Gender);
                cmd.Parameters.Add(paramOriginalGenderId);
                SqlParameter paramOriginalCityId = new SqlParameter("@City", City);
                cmd.Parameters.Add(paramOriginalCityId);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        public static int InsertEmployee(string Name, string Gender,string City)
        {
            /*
             * INSERT SQL
             */
            string CS = ConfigurationManager.ConnectionStrings["QuotationDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                string insertQuery = "insert into EmployeeASPNETTutorial (Name, Gender, Cty) values(@Name,@Gender,@City)";
                SqlCommand cmd = new SqlCommand(insertQuery,con);
                SqlParameter nameParam = new SqlParameter("@Name", Name);
                cmd.Parameters.Add(nameParam);
                SqlParameter genderParam = new SqlParameter("@Gender", Gender);
                cmd.Parameters.Add(genderParam);
                SqlParameter cityParam = new SqlParameter("@City", City);
                cmd.Parameters.Add(cityParam);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}