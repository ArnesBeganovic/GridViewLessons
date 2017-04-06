using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GridViewUsefulStuff.Object_Data_Source
{
    public partial class ObjectDataSourceInsertEditDelete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Lnb_Insert_Click(object sender, EventArgs e)
        {
            //Prepare parameters for SQL
            ObjectDataSource1.InsertParameters["Name"].DefaultValue = ((TextBox)GridView.FooterRow.FindControl("tb_InsertName")).Text;
            ObjectDataSource1.InsertParameters["Gender"].DefaultValue = ((DropDownList)GridView.FooterRow.FindControl("ddl_InsertGender")).SelectedValue;
            ObjectDataSource1.InsertParameters["City"].DefaultValue = ((TextBox)GridView.FooterRow.FindControl("tb_InsertCity")).Text;

            //U Html-u ima InsertCommand koja ce biti pozvana sa kodom ispod a kod iznad je vec napunio parametre
            ObjectDataSource1.Insert();
        }
    }
}