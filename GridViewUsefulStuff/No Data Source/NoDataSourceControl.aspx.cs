using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GridViewUsefulStuff
{
    public partial class NoDataSourceControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
             * Kreiram normalno formu ali sada bez databind kontrole nego onu prvu kontrolu zamjenim sa templejtom
             * U templejtu moram imati  Edit i View opcije sa po dva link buttona.
             * Zatim se napravi ovaj dole metod BindGridViewData koji poziva dataacceslayer metod kada se Page_Load
             * odradi. Ovo sve ce napraviti da se tabela prikaze na ekranu. Sada moram dati funkcionalnost Edit, Update
             * i Delete tipkama. To se radi kroz specijalni event za cijeli gridvideu koji se nalazi u propertiju
             * Zove se RowCommand. Ova komanda prati klik svakog linkbuttona u jednom redu sto znaci da kroz switch
             * ili if mogu prepoznati sta je kliknuto u tom redu i onda napraviti neku logiku iza toga. Zbog toga imam
             * mnogo if else posto imam 4 potencijalne tipke
             * 
             * CausesValidation="false" se dodaje na Cancel i Delete jer mi ne treba validacija tu
             * 
             * OnClientClick="return confirm('Are you sure you want to delete this row')" je na delete funcion i ako je false onda nece biti postbacka
             */
            if (!IsPostBack)
            {
                BindGridViewData();
            }
        }

        private void BindGridViewData()
        {
            GridView2.DataSource = EmployeeDataAccessLayer.GetAllEmployees();
            GridView2.DataBind();
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            

            if (e.CommandName == "EditRow")
            {

                /*
                 * Neko je kliknuo nesto na gridview-u i to nas je dovelo u RowCommand funkciju. Posto svaki linkbutton
                 * ima property CommandName, njemu se moze pristupiti kroz argument e. Ovaj arugment ima properti
                 * CommandSource koji je zapravo reprezentacija objekta koji je kliknuo. Dakle e.CommandSource je objekat
                 * a posto znam da samo imam LinkButtone onda mogu typecastat na to. Sada imam kompletan LinkButton u
                 * memoriji ali on mi nije potreban. POtreban mi je red u kojem se taj LinkButton nalazi a njega 
                 * dobijem koristeci NamingContainer property kojeg opet TypeCastam u GridVidewRow. Znam dakle da je
                 * u pitanju red. Konacno, red ima svoj property RowIndex i tako dobijem redni broj indexa
                 */
                int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                //EditIndex je property GridView-a koji omogucava automatizaciju edita kada imas rowIndex;
                GridView2.EditIndex = rowIndex;
                BindGridViewData();
            } else if (e.CommandName.ToString() == "DeleteRow")
            {
                /*
                 * Ako pogledam u html source kod vidjecu da sam stavio CommandArgument da mi bude funkcija Eval EmployeeID
                 * Dakle, Id je ubacen u ovu vrijednost samo kao string. Sad ga treba povuci i poslati u Metod DeleteEmployee
                 */
                EmployeeDataAccessLayer.DeleteEmployee(Convert.ToInt32(e.CommandArgument));
                BindGridViewData();
            } else if(e.CommandName == "CancelUpdate")
            {
                GridView2.EditIndex = -1;
                BindGridViewData();
            } else if (e.CommandName == "UpdateRow")
            {
                
                int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                int employeeId = Convert.ToInt32(e.CommandArgument);
                //U Gridview-u uzmi Row sa indexom rowIndex i u tom redu nadji kontrolu koja se zove tb_Name i uzmi text iz nje
                string name = ((TextBox)GridView2.Rows[rowIndex].FindControl("tb_Name")).Text;
                string gender = ((DropDownList)GridView2.Rows[rowIndex].FindControl("ddl_Gender")).SelectedValue;
                string city = ((TextBox)GridView2.Rows[rowIndex].FindControl("tb_City")).Text;
                
                EmployeeDataAccessLayer.UpdateEmployee(employeeId, name, gender, city);
                GridView2.EditIndex = -1;
                BindGridViewData();
            }
            else if(e.CommandName == "InsertRow")
            {
                string name = "";
                string gender = "";
                string city = "";
                try
                {
                    name = ((TextBox)GridView2.FooterRow.FindControl("tb_InsertName")).Text;
                    gender = ((DropDownList)GridView2.FooterRow.FindControl("ddl_InsertGender")).SelectedValue;
                    city = ((TextBox)GridView2.FooterRow.FindControl("tb_InsertCity")).Text;
                } catch(NullReferenceException ex)
                {
                    Console.Write("jjjj");
                }

                
                

                EmployeeDataAccessLayer.InsertEmployee(name, gender, city);
                BindGridViewData();
            }
        }
    }
}