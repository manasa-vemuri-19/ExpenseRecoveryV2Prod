using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class FAQ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string cmd = "select * from expense_Category order by category desc";
        DataSet ds = BusinessLogic.GetDataSet(cmd);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int j = 0; j < e.Row.Cells.Count; j++)
        {
           
            e.Row.Cells[j].Style.Add("BORDER", "Black 1px solid");
            e.Row.Cells[j].Style.Add("padding-left", "5px");
        }
        for (int j = 0; j < GridView1.Rows.Count; j++)
        {
            if (GridView1.Rows[j].Cells[0].Text == "Expense Billed")
            {
                //GridView1.Rows[j].Cells[0].Style.Add("ForeColor", "Black");
                //BackColor="#FCE4D6" BorderColor="#F8CBAD" ForeColor="#A9D08E"
                GridView1.Rows[j].BackColor = ColorTranslator.FromHtml("#A9D08E");
            }
            else if (GridView1.Rows[j].Cells[0].Text == "Expense To Be Billed")
            {
                //GridView1.Rows[j].Cells[0].Style.Add("ForeColor", "Black");
                //BackColor="#FCE4D6" BorderColor="#F8CBAD" ForeColor="#A9D08E"
                GridView1.Rows[j].BackColor = ColorTranslator.FromHtml("#FFFFCC");
            }
            else if (GridView1.Rows[j].Cells[0].Text == "Not Billable-Billed as part of milestone at fixed cost")
            {
                //GridView1.Rows[j].Cells[0].Style.Add("ForeColor", "Black");
                //BackColor="#FCE4D6" BorderColor="#F8CBAD" ForeColor="#A9D08E"
                GridView1.Rows[j].BackColor = ColorTranslator.FromHtml("#F8CBAD");
            }
            else if (GridView1.Rows[j].Cells[0].Text == "Not Billable-Not Client Recoverable")
            {
                //GridView1.Rows[j].Cells[0].Style.Add("ForeColor", "Black");
                //BackColor="#FCE4D6" BorderColor="#F8CBAD" ForeColor="#A9D08E"
                GridView1.Rows[j].BackColor = ColorTranslator.FromHtml("#FCE4D6");
            }
        }
    }
}