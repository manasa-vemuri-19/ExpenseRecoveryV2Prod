using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Security.Application;

public partial class Upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(10000);
        lblMsg.Visible = false;
        int Count = 0;
        //PopulateDataForDownload();
        Session["key"] = null;
        DataTable dt = new DataTable();

        string MailID = System.IO.Path.GetFileName(HttpContext.Current.User.Identity.Name);
        if (fuBulkUpload.PostedFile.ContentLength != 0 && fuBulkUpload.PostedFile.ContentLength < (3 * 20480 * 1024))
        {
            try
            {
                string destDir = Server.MapPath("./App_Data");

                string fileName = Path.GetFileName(fuBulkUpload.PostedFile.FileName);
                if (fileName.ToLower().CompareTo("expenserecovery_upload_" + MailID.ToLower() + ".xls") == 0 || fileName.ToLower().CompareTo("expenserecovery_upload_" + MailID.ToLower() + ".xlsx") == 0)
                {
                    string destPath = Path.Combine(destDir, fileName);
                    if (File.Exists(destPath))
                    {
                        File.Delete(destPath);
                    }
                    fuBulkUpload.SaveAs(destPath);
                    OleDbConnection oledbConn;
                    OleDbCommand oledbCmd;
                    //DataTable dt = new DataTable();
                    OleDbDataAdapter oledbAdap;
                    try
                    {
                        //string path = "D:\\VSS\\EAS_Applications\\Expense RecoveryWithSummaryPage\\App_Data";
                        //path = path + "\\" + fileName;

                        string connString = "";



                        //Connection String to Excel Workbook



                        //connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + destPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + destPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";


                        string query = "SELECT * FROM [Data$]";

                        OleDbConnection conn = new OleDbConnection(connString);

                        if (conn.State == ConnectionState.Closed)

                            conn.Open();

                        OleDbCommand cmd = new OleDbCommand(query, conn);

                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                        DataSet ds = new DataSet();

                        da.Fill(ds);

                        dt = ds.Tables[0];

                        da.Dispose();

                        conn.Close();

                        conn.Dispose();


                    }
                    catch (System.Exception ex)
                    { throw ex; }

                    if (dt.Columns.Contains("SubUnit"))
                        dt.Columns.Remove("SubUnit");

                    if (dt.Columns.Contains("F3%"))
                        dt.Columns.Remove("F3%");
                    if (dt.Columns.Contains("ResponseFromFinance"))
                        dt.Columns.Remove("ResponseFromFinance");
                    string _sqlWhere = "[unique] is not null";
                    string _sqlOrder = "";

                    //DataTable newDataTable = dt.Select(_sqlWhere, _sqlOrder).CopyToDataTable();
                    // REMOVE ALL EMPTY ROWS
                    dt.Rows.Cast<DataRow>().ToList().FindAll(Row =>
                    { return String.IsNullOrEmpty(String.Join("", Row.ItemArray)); }).ForEach(Row =>
                    { dt.Rows.Remove(Row); });
                    //DataTable newDataTable =  = dt.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull || string.Compare((field as string).Trim(), string.Empty) == 0)).CopyToDataTable();
                    //Session.Add("dtNew", dt);
                   // Session.Add("dt", dt);


                }
                else
                {
                    //lblMsg.Visible = true;

                    PopUp("Excel filename must be ExpenseRecovery_Upload_" + MailID + ".xls");
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Visible = true;
                PopUp("Some Error Occurred!");
                return;
            }

            //Validations
            DataTable dtExcel = new DataTable();
            dtExcel = dt;
            int newCount = dt.Rows.Count;
            string error = "";
            int cnt = 1;
            for (int i = 0; i < newCount; i++)
            {
                if (i == 184)
                {
                }
                lblMsg.Text = i.ToString();
                lblMsg.Visible = true;

                //if (dtExcel.Rows[i]["Update from Account team"].ToString() == "--Select--")
                //{
                //    lblDownload.Visible = true;
                //        lblDownload.Text ="Please select "
                //}
                if (dtExcel.Rows[i]["Category"].ToString() == "Update Pending")
                {
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = Color.Red;
                    lblMsg.Font.Size = 8;
                    error += cnt++ + ". Choose proper Category...Check in the " + (i + 2) + " row of the sheet!" + "<br>";
                    //return;

                }
                else if ((dtExcel.Rows[i]["Status"].ToString() == "Update Pending") || (dtExcel.Rows[i]["Status"].ToString() == "Past Commitment"))
                {
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = Color.Red;
                    lblMsg.Font.Size = 8;
                    error += cnt++ + ". Choose proper Status...Check in the " + (i + 2) + " row of the sheet!" + "<br>";
                    //return;

                }
                else if (dtExcel.Rows[i]["Category"].ToString() == "Expense Billed")
                {

                    if (dtExcel.Rows[i]["Invoice/Confirmation No"].ToString() == "")
                    {

                        lblMsg.Visible = true;
                        lblMsg.ForeColor = Color.Red;
                        lblMsg.Font.Size = 8;
                        error += cnt++ + ". Enter Invoice/Confirmation Number...Check in the " + (i + 2) + " row of the sheet!" + "<br>";
                        //return;
                    }
                    if (dtExcel.Rows[i]["Status"].ToString() != "Expense confirmation already raised")
                    {
                        lblMsg.Visible = true;
                        lblMsg.ForeColor = Color.Red;
                        lblMsg.Font.Size = 8;
                        error += cnt++ + ". Choose proper Status...Check in the " + (i + 2) + " row of the sheet!" + "<br>";
                        //return;
                    }

                    if (dtExcel.Rows[i]["Initiated Date (mm/dd/yyyy)"].ToString() == "")
                    {
                        error += cnt++ + ". Choose proper expense/reversal initiation Date (mm/dd/yyyy) when expense/reversal was initiation ...Check in the " + (i + 2) + " row of the sheet!" + "<br>";
                        //return;
                    }

                    else if (Convert.ToDateTime(dtExcel.Rows[i]["Initiated Date (mm/dd/yyyy)"]) > DateTime.Now)
                    {
                        lblMsg.Visible = true;
                        lblMsg.ForeColor = Color.Red;
                        lblMsg.Font.Size = 8;
                        error += cnt++ + ". Choose past date (mm/dd/yyyy)  when expense/reversal was initiation...Check in the " + (i + 2) + " row of the sheet!" + "<br>";
                        //return;
                    }

                }

                else if (dtExcel.Rows[i]["Category"].ToString() == "Expense To Be Billed")
                {

                    //if (dtExcel.Rows[i]["Invoice/Confirmation No"].ToString() == "")
                    //{
                    //    lblDownload.Visible = true;
                    //    lblDownload.Text = "Please enter Invoice/Confirmation Number...Please check in the " + (i + 2) + " row of the sheet!";
                    //    //return;
                    //}
                    if (!((dtExcel.Rows[i]["Status"].ToString() == "Pending client sign-off") || (dtExcel.Rows[i]["Status"].ToString() == "Pending with delivery - PM/DM") || (dtExcel.Rows[i]["Status"].ToString() == "Pending with Finance for activity code creation, approval, etc.") || (dtExcel.Rows[i]["Status"].ToString() == "Pending with IS - system issues")))
                    {
                        lblMsg.Visible = true;
                        lblMsg.ForeColor = Color.Red;
                        lblMsg.Font.Size = 8;
                        error += cnt++ + ". Choose proper Status...Check in the " + (i + 2) + " row of the sheet!" + "<br>";
                        //return;
                    }
                    if (dtExcel.Rows[i]["Date (mm/dd/yyyy) by when expense will be closed in system"].ToString() == "")
                    {
                        lblMsg.Visible = true;
                        lblMsg.ForeColor = Color.Red;
                        lblMsg.Font.Size = 8;
                        error += cnt++ + ". Choose proper date (mm/dd/yyyy) by when expense will be closed...Check in the " + (i + 2) + " row of the sheet!" + "<br>";
                        //return;
                    }
                    try
                    {
                        if (Convert.ToDateTime(dtExcel.Rows[i]["Date (mm/dd/yyyy) by when expense will be closed in system"]) < DateTime.Now)
                        {
                            lblMsg.Visible = true;
                            lblMsg.ForeColor = Color.Red;
                            lblMsg.Font.Size = 8;
                            error += cnt++ + ". Choose Future date (mm/dd/yyyy) by when expense will be closed...Check in the " + (i + 2) + " row of the sheet!" + "<br>";
                            //return;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Visible = true;
                        lblMsg.ForeColor = Color.Red;
                        lblMsg.Font.Size = 8;
                        error += cnt++ + ". Enter date in mm/dd/yyyy format" + "<br>";
                        //return;
                    }


                }

                else if (dtExcel.Rows[i]["Category"].ToString() == "Not Billable-Billed as part of milestone at fixed cost")
                {
                    if (!((dtExcel.Rows[i]["Status"].ToString() == "Reversal Pending with Finance") || (dtExcel.Rows[i]["Status"].ToString() == "Reversal Pending with PM/DM")))
                    {
                        lblMsg.Visible = true;
                        lblMsg.ForeColor = Color.Red;
                        lblMsg.Font.Size = 8;
                        error += cnt++ + ". Choose proper Status...Check in the " + (i + 2) + " row of the sheet!" + "<br>";
                        //return;
                    }
                    else
                    {
                        if (dtExcel.Rows[i]["Status"].ToString() == "Reversal Pending with Finance")
                        {

                            if (dtExcel.Rows[i]["Initiated Date (mm/dd/yyyy)"].ToString() == "")
                            {
                                lblMsg.Visible = true;
                                lblMsg.ForeColor = Color.Red;
                                lblMsg.Font.Size = 8;
                                error += cnt++ + ". Choose proper expense/reversal initiation Date (mm/dd/yyyy) when expense/reversal was initiation...Check in the " + (i + 2) + " row of the sheet!" + "<br>";
                                //return;
                            }

                            else if (Convert.ToDateTime(dtExcel.Rows[i]["Initiated Date (mm/dd/yyyy)"]) > DateTime.Now)
                            {
                                lblMsg.Visible = true;
                                lblMsg.ForeColor = Color.Red;
                                lblMsg.Font.Size = 8;
                                error += cnt++ + ". Choose past expense/reversal initiation Date (mm/dd/yyyy) when expense/reversal was initiation...Check in the " + (i + 2) + " row of the sheet!" + "<br>";
                                //return;
                            }
                        }

                        else if (dtExcel.Rows[i]["Status"].ToString() == "Reversal Pending with PM/DM")
                        {

                            if (dtExcel.Rows[i]["Date (mm/dd/yyyy) by when expense will be closed in system"].ToString() == "")
                            {
                                lblMsg.Visible = true;
                                lblMsg.ForeColor = Color.Red;
                                lblMsg.Font.Size = 8;
                                error += cnt++ + ". Choose proper  Date (mm/dd/yyyy) when expense/reversal will be closed...Check in the " + (i + 2) + " row of the sheet!" + "<br>";
                                //return;
                            }

                            else if (Convert.ToDateTime(dtExcel.Rows[i]["Date (mm/dd/yyyy) by when expense will be closed in system"]) < DateTime.Now)
                            {
                                lblMsg.Visible = true;
                                lblMsg.ForeColor = Color.Red;
                                lblMsg.Font.Size = 8;
                                error += cnt++ + ". Choose future  Date (mm/dd/yyyy) when expense/reversal will be closed......Check in the " + (i + 2) + " row of the sheet!" + "<br>";
                                //return;
                            }
                        }

                    }
                }
                else if (dtExcel.Rows[i]["Category"].ToString() == "Not Billable-Not Client Recoverable")
                {
                    if (!((dtExcel.Rows[i]["Status"].ToString() == "Reversal Pending with Finance") || (dtExcel.Rows[i]["Status"].ToString() == "Reversal Pending with PM/DM")))
                    {
                        lblMsg.Visible = true;
                        lblMsg.ForeColor = Color.Red;
                        lblMsg.Font.Size = 8;
                        error += cnt++ + ". Choose proper Status...Check in the " + (i + 2) + " row of the sheet!" + "<br>";
                        //return;
                    }
                    else
                    {
                        if (dtExcel.Rows[i]["Status"].ToString() == "Reversal Pending with Finance")
                        {

                            if (dtExcel.Rows[i]["Initiated Date (mm/dd/yyyy)"].ToString() == "")
                            {
                                lblMsg.Visible = true;
                                lblMsg.ForeColor = Color.Red;
                                lblMsg.Font.Size = 8;
                                error += cnt++ + ". Choose proper expense/reversal initiation Date (mm/dd/yyyy) when expense/reversal was initiation...Check in the " + (i + 2) + " row of the sheet!" + "<br>";
                                //return;
                            }

                            else if (Convert.ToDateTime(dtExcel.Rows[i]["Initiated Date (mm/dd/yyyy)"]) > DateTime.Now)
                            {
                                lblMsg.Visible = true;
                                lblMsg.ForeColor = Color.Red;
                                lblMsg.Font.Size = 8;
                                error += cnt++ + ". Choose past expense/reversal initiation Date (mm/dd/yyyy) when expense/reversal was initiation...Check in the " + (i + 2) + " row of the sheet!" + "<br>";
                                //return;
                            }
                        }

                        else if (dtExcel.Rows[i]["Status"].ToString() == "Reversal Pending with PM/DM")
                        {

                            if (dtExcel.Rows[i]["Date (mm/dd/yyyy) by when expense will be closed in system"].ToString() == "")
                            {
                                lblMsg.Visible = true;
                                lblMsg.ForeColor = Color.Red;
                                lblMsg.Font.Size = 8;
                                error += cnt++ + ". Choose proper  Date (mm/dd/yyyy) when expense/reversal will be closed...Check in the " + (i + 2) + " row of the sheet!" + "<br>";
                                //return;
                            }

                            else if (Convert.ToDateTime(dtExcel.Rows[i]["Date (mm/dd/yyyy) by when expense will be closed in system"]) < DateTime.Now)
                            {
                                lblMsg.Visible = true;
                                lblMsg.ForeColor = Color.Red;
                                lblMsg.Font.Size = 8;
                                error += cnt++ + ". Choose future  Date (mm/dd/yyyy) when expense/reversal will be closed......Check in the " + (i + 2) + " row of the sheet!" + "<br>";
                                //return;
                            }
                        }

                    }
                }

            }
            if (error != "")
            {
                string headMsg = "Please check for the below Error(s):<br>";
                lblMsg.Text = headMsg+error;
                return;
            }
            if (dtExcel.Rows.Count > 0)
            {
                //Saving the datatable
                String DBConnecting = ConfigurationManager.AppSettings["DBConnectString"];
                MailID = System.IO.Path.GetFileName(HttpContext.Current.User.Identity.Name);
                DAL dal = new DAL();
                string role = dal.CheckAccess(MailID);
                using (SqlConnection Conn = new SqlConnection())
                {

                    Conn.ConnectionString = AntiXss.HtmlEncode(DBConnecting);
                    try
                    {

                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = Conn;
                        cmd.CommandText = "SP_ExpenseRecovery_BulkUpdate_EAS";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter paramDB = cmd.Parameters.AddWithValue("@dtExcel", dtExcel);

                        //string username = System.IO.Path.GetFileName(HttpContext.Current.User.Identity.Name);
                        //SqlParameter paramLastUpdatedBy =
                        cmd.Parameters.AddWithValue("@username", MailID);
                        cmd.Parameters.AddWithValue("@AccessType", role);
                        SqlParameter prmReturnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
                        prmReturnValue.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.Add(prmReturnValue);

                        paramDB.Value = dtExcel;
                        paramDB.SqlDbType = SqlDbType.Structured;
                        paramDB.TypeName = "dbo.ExpenseRecoveryBulkUpdate_latest";
                        Conn.Open();
                        cmd.CommandTimeout = 0;
                        cmd.ExecuteNonQuery();

                        int result = (Convert.ToInt32(prmReturnValue.Value));

                        lblMsg.Visible = true;

                        if (result == -1)
                        {
                            lblMsg.ForeColor = Color.Red;
                            lblMsg.Text = "You are not authorized to upload the data present in the excel";
                        }

                        else if (result == 1)
                        {
                            lblMsg.ForeColor = Color.Green;
                            lblMsg.Text = "Data Uploaded Successfully";

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "check", "check()", true);
                        }
                    }

                    catch (Exception ex)
                    {
                        throw ex;
                        lblMsg.Visible = true;
                        lblMsg.Text = "Error Encountered. Please try after sometime";
                    }
                }

            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Please select the correct excel to upload the data";
            }

        }
    }
    protected void PopUp(string msg)
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "", "alert('" + msg + "');", true);
    }
}