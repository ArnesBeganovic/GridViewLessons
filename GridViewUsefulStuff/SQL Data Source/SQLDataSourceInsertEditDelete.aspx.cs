using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GridViewUsefulStuff
{
    public partial class SQLDataSourceInsertEditDelete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
             * Sta sam uradio u html templejtu:
             * Prvo sam napravio gridview (sa edit,delete tipkama) i povezao sa sqlsource kontrolom te testirao da vidim jel prikazuje podatke
             * Zatim sam u gridview propertiju nasao footer i stavio enable=true. To mi je dalo footer
             * Zatim sam pretvorio sva polja u html kodu iz boundfield u templatefield. Ovo mi je omogucilo dalje editovanje polja.
             * Zatim sam u koloni ID u footeru ubacio linkbutton, a ispod ostalih textbox i dropdownbox te podesio ime i ostale dzidze.
             * dropdownbox ima extra property SelectedValue koji vuce u editu ono sto pise kad nije edit
             * Zatim sam dodao RequiredFieldValidatore u svako edit i insert polje i podesio ih
             * Zatim sam dodao dvije ValidationSummary kontrole ispod gridviewa.
             * Jedna je imala  ValidationGroup="INSERT" i sluzila je da prikaze error od svih drugih koji imaju isti ValidationGroup. Stavio sam samo u insert red ovaj properti
             * Druga nije imala nista i bila je default za sve one koji nemaju ValidationGroup tj za one koji su konkretno u edit mode-u
             * 
             * U ovoj fazi, templejt je bio gotov ali mi link insert nije radio.
             * 
             * 
             */

            
        }

        protected void Lnb_Insert_Click(object sender, EventArgs e)
        {
            //Prepare parameters for SQL
            DS.InsertParameters["Name"].DefaultValue = ((TextBox)GridView.FooterRow.FindControl("tb_InsertName")).Text;
            DS.InsertParameters["Gender"].DefaultValue = ((DropDownList)GridView.FooterRow.FindControl("ddl_InsertGender")).SelectedValue;
            DS.InsertParameters["Cty"].DefaultValue = ((TextBox)GridView.FooterRow.FindControl("tb_InsertCity")).Text;

            //U Html-u ima InsertCommand koja ce biti pozvana sa kodom ispod a kod iznad je vec napunio parametre
            DS.Insert();
        }
    }
}