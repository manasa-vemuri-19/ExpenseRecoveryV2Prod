using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.Security.Application;


public partial class MasterPageOld : System.Web.UI.MasterPage
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        string userName = System.IO.Path.GetFileName(Context.User.Identity.Name);
        //int length=userName.Length;
        //string sub = userName.Substring(11, length - 11);

        DAL daObj = new DAL();
        
       // lblName.Text = userName;
        string cmdText = "select top 1 asofdate from gmu_expense_recovery where [unique] is not null";
        System.Data.DataSet dsAsofdate = daObj.GetDataSet(cmdText);
        //lblAsofdate.Text = AntiXss.HtmlEncode("Data refreshed as on : " + Convert.ToDateTime(dsAsofdate.Tables[0].Rows[0][0]).ToString("dd-MMM-yyyy"));

    }
    protected void Menu_MainOptions_MenuItemClick(object sender, MenuEventArgs e)
    {

    }
    protected void Menu_MainOptions_MenuItemClick1(object sender, MenuEventArgs e)
    {

    }
}
