using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GridViewUsefulStuff.Delete_Multiple_Rows
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
             * Ovdje sad koristim kod koji sam ranije napravio u EmployeeDataAccessLayeru. Napravio sam funkciju GetData()
             * koja jednostavno pozove query i rezultat stavi kao datasource za gridview i onda bind-a podatke.
             * 
             * Zatim sam podesio gridview tako sto sam otisao na EditColumns i dodao tva template fielda za checkbox i 
             * employeeId i tri bound fielda. Bitan properti je AutoGenerateColumns = false koji ce ukinuti automatsko 
             * kreiranje kolona. To ce istovremeno napraviti prazne kolone. Zato treba boundati polja sa rezultatom. Za ova
             * tri bound polja to je uradjeno preko wizzarda u EditCOlums i onda na svakoj ima property DataField u koji je
             * unio ime kolone iz klase Employee.
             */
            if (!IsPostBack)
            {
                GetData();
            }
        }

        private void GetData()
        {
            GridView1.DataSource = EmployeeDataAccessLayer.GetAllEmployees();
            GridView1.DataBind();
        }


        protected void cb_DeleteHeader_CheckedChanged1(object sender, EventArgs e)
        {
            foreach(GridViewRow gridViewRow in GridView1.Rows)
            {
                /*
                 * Znaci sa svaku kontrolu koja se zove cbDelete njen Checked properti ce  imati isto
                 * kao i sender a sender je header checkbox
                 */
                ((CheckBox)gridViewRow.FindControl("cb_Delete")).Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void cb_Delete_CheckedChanged(object sender, EventArgs e)
        {
            //Nadji checkbox u headeru
            CheckBox headerCheckBox = (CheckBox)GridView1.HeaderRow.FindControl("cb_DeleteHeader");

            //Logika
            if (headerCheckBox.Checked)
            {
                headerCheckBox.Checked = ((CheckBox)sender).Checked;
            }
            else
            {
                bool allCheckBoxesChecked = true;
                foreach( GridViewRow row in GridView1.Rows)
                {
                    if (!((CheckBox)row.FindControl("cb_Delete")).Checked)
                    {
                        allCheckBoxesChecked = false;
                        break;
                    }
                }
                headerCheckBox.Checked = allCheckBoxesChecked;
            }
        }

        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            List<string> listEmployeeToDelete = new List<string>();
            foreach( GridViewRow row in GridView1.Rows)
            {
                if (((CheckBox)row.FindControl("cb_Delete")).Checked)
                {
                    string employeeId = ((Label)row.FindControl("lbl_EmployeeId")).Text;
                    listEmployeeToDelete.Add(employeeId);
                }
            }

            if (listEmployeeToDelete.Count > 0)
            {
                lbl_Result.ForeColor = System.Drawing.Color.Navy;

                /*
                 * Sada mogu koristiti kod ispod ali on je neefikasan jer za svaki red poziva delete i onda brise jedan po jedan. Zato je i zatvoren kod
                 *
                 *foreach (string strEmployeeId in listEmployeeToDelete)
                 *{
                 *    EmployeeDataAccessLayer.DeleteEmployee(Convert.ToInt32(strEmployeeId));
                 *}
                */

                /*Zato sam napravio novi metod koji vrsi Concaternation stringova i koji radi. Medjutim i on ne valja jer je opasan. Otvara mogucnost SQL inekcije
                 * EmployeeDataAccessLayer.DeleteMultipleEmployee(listEmployeeToDelete);
                */

                //Pravi metod

                EmployeeDataAccessLayer.DeleteMultipleEmployeeSecure(listEmployeeToDelete);
                lbl_Result.Text = listEmployeeToDelete.Count.ToString() + " row(s) deleted";
                GetData();
            } else
            {
                lbl_Result.ForeColor = System.Drawing.Color.Red;
                lbl_Result.Text = "No rows selected or deleted!";
            }
        }
    }
}