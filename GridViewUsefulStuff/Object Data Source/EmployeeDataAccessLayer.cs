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

        //Delete Multiple Rows trazio ovu ali ona je opasna jer otvara vrata SQL inekciji
        public static void DeleteMultipleEmployee(List<string> employeeID)
        {
            string CS = ConfigurationManager.ConnectionStrings["QuotationDB"].ConnectionString;
            using(SqlConnection con = new SqlConnection(CS))
            {
                string strInInput = string.Empty;
                foreach(string str in employeeID)
                {
                    strInInput += str + ",";
                }
                strInInput = strInInput.Remove(strInInput.LastIndexOf(","));
                string deleteCommand = "delete from EmployeeASPNETTutorial where EmployeeId IN (" + strInInput + ")";
                SqlCommand cmd = new SqlCommand(deleteCommand, con);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //Konacno, ova ispod je dobra i treba se koristiti
        public static void DeleteMultipleEmployeeSecure(List<string> employeeID)
        {
            string CS = ConfigurationManager.ConnectionStrings["QuotationDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                //Kljucni dio koda je ova izgradnja parametara preko Lambda funkcije. Recimo da sam selektovao prva tri Id-a ali da su to 10,11,12 redni brojevi
                //Dobicu listu (@Parameter1;@Parameter2;@Parameter3)
                List<string> parameters = employeeID.Select((s, i) => "@Parameter" + i.ToString()).ToList();
                //Zatim tu listu pretvaram u string koji izgleda ovako: @Parameter1,@Parameter2,@Parameter3
                string inClause = string.Join(",", parameters);
                //Zatim to dodajem u delete komantu i sav kod izgleda ovako:
                //Delete from EmployeeASPNETTutorial where EmployeeId IN ( @Parameter1,@Parameter2,@Parameter3)
                string deleteCommandText = "Delete from EmployeeASPNETTutorial where EmployeeId IN (" + inClause + ")";

                SqlCommand cmd = new SqlCommand(deleteCommandText, con);
                //Zatim moram dodati vrijednosti parametara
                for(int i = 0; i < parameters.Count; i++)
                {
                    //Funkcija AddWithValue omogucava da indeksno dodajem vrijednosti iz ulaznog araja
                    cmd.Parameters.AddWithValue(parameters[i], employeeID[i]);   
                }

                con.Open();
                cmd.ExecuteNonQuery();
                
            }
        }
    }
}