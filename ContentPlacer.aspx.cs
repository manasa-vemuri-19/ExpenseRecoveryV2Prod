using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ContentPlacer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ClientScript.GetPostBackEventReference(this, string.Empty);
        string URL = string.Empty;
        if (Session["URL"] != null)
        {
           

            URL = Session["URL"].ToString();
            
            Response.Redirect(URL);
        }
        //URL = "http://nebula/expenserecovery_test/ExpenseRecovery.aspx?category=Not%20Billable-Not%20Client%20Recoverable&subcategory=ALL&ageingbucket=%3C%2030%20Days&practiceline=ORC";
        Response.Redirect(URL);
    }
}