using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.Services;
using Newtonsoft.Json;
using System.Configuration;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using Microsoft.Security.Application;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;
using OfficeOpenXml;


public partial class ExpenseRecoveryTesting : System.Web.UI.Page
{

    static string subCategory_top;
    static string customerCode;
    static string ageingBucket;
    static string glLongText;
    static string PM;
    static string DM;
    static string role;
    static string MailID;
    static string profitCentre;
    static string category_top;
    static string practiceLine;
    static int CbEditClicks = 0;
    static int EditRowIndex = 0;
    static string Commitment;
    static DataTable dtExpRec;
    static string categoryReportParam = "ALL";
    static string subcategoryReportParam = "ALL";
    static string ageingbucketReportParam = "ALL";
    static string practicelineReportParam = "ALL";
    DataTable dtaccess;
    DAL dal = new DAL();
    public static string connstring = ConfigurationManager.AppSettings["DBConnectString"].ToString();
    //   protected void Page_Load(object sender, EventArgs e)
    //   {
    //static string subCategory_top;
    //   static string customerCode;
    //   static string ageingBucket;
    //   static string glLongText;
    //   static string PM;
    //   static string DM;
    //   static string role;
    //   static string MailID;
    //   static string profitCentre;
    //   static string category_top;
    //   static string practiceLine;
    //   static int CbEditClicks = 0;
    //   static int EditRowIndex = 0;
    //   static string Commitment;
    //   static DataTable dtExpRec;
    //   static string categoryReportParam = "ALL";
    //   static string subcategoryReportParam = "ALL";
    //   static string ageingbucketReportParam = "ALL";
    //   static string practicelineReportParam = "ALL";
    //   static DataTable dts;
    //   DataTable dtaccess;
    //   DAL dal = new DAL();
    //   public static string connstring = ConfigurationManager.AppSettings["DBConnectString"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        //Menu Menumain = (Menu)Master.FindControl("Menu_MainOptions");
        //Menumain.Items[1].Selected = true;
        ClientScript.GetPostBackEventReference(this, string.Empty);

        string cmdText = "select max(LoadedOn) from DataLoad_Tracker where PackageName='ExpenseRecovery.xls'";
        System.Data.DataSet dsAsofdate = dal.GetDataSet(cmdText);
        lblDate.Text = Encoder.HtmlEncode("Data refreshed as on : " + Convert.ToDateTime(dsAsofdate.Tables[0].Rows[0][0]).ToString("dd-MMM-yyyy HH:MM:ss"));

        //if (Session["MailID"] == null)
        //{
        //    Response.Redirect("SessionTimeOut.htm");
        //}

        if (!IsPostBack)
        {
            hdnddldata.Value = GetDDLJson();

            categoryReportParam = "ALL";
            subcategoryReportParam = "ALL";
            ageingbucketReportParam = "ALL";
            practicelineReportParam = "ALL";

            if (Request.QueryString["category"] != null)
            {
                categoryReportParam = Request.QueryString["category"].ToString();
                categoryReportParam = (String.IsNullOrEmpty(this.Request["category"]) ? "ALL" : Server.UrlDecode(this.Request["category"]));

            }

            if (Request.QueryString["subcategory"] != null)
            {
                subcategoryReportParam = Request.QueryString["subcategory"].ToString();
                subcategoryReportParam = (String.IsNullOrEmpty(this.Request["subcategory"]) ? "ALL" : Server.UrlDecode(this.Request["subcategory"]));

            }
            if (Request.QueryString["ageingbucket"] != null)
            {
                ageingbucketReportParam = Request.QueryString["ageingbucket"].ToString();
                ageingbucketReportParam = (String.IsNullOrEmpty(this.Request["ageingbucket"]) ? "ALL" : Server.UrlDecode(this.Request["ageingbucket"]));

            }
            if (Request.QueryString["practiceline"] != null)
            {
                practicelineReportParam = Request.QueryString["practiceline"].ToString();
                practicelineReportParam = (String.IsNullOrEmpty(this.Request["practiceline"]) ? "ALL" : Server.UrlDecode(this.Request["practiceline"]));
                if (practicelineReportParam == "EAS")
                    practicelineReportParam = "ALL";

            }



            //Lblmsg.Visible = false;




            MailID = System.IO.Path.GetFileName(HttpContext.Current.User.Identity.Name);

            //MailID = "Ketan_Sankar";
            //Session.Add("MailID", MailID);
            role = dal.CheckAccess(MailID);
            //dal.CheckAccess(Session["MailID"].ToString(), out role);
            //Session.Add("Role", role);

            SqlCommand cmd = new SqlCommand();
            string query = "exec SP_CheckAdminAccess @mailid";
            cmd.Parameters.AddWithValue("@mailid", MailID);
            cmd.CommandText = query;
            DataSet dsAdminAccess = dal.FetchDataSet(cmd);
            string validity = dsAdminAccess.Tables[0].Rows[0][0].ToString();

            //if ((MailID.ToLower() == "soumya_ramanathan") || (MailID == "rakhi_ap") || (MailID == "sridevi_srirangan") || (MailID == "prema_haridas") || (MailID == "glnrao"))
            if (validity == "VALID")
            {
                btnDownloadPivot.Visible = true;
                btnDownloadPivot.Enabled = true;
            }

            //Button1.Visible = true;
            //   Button1.Enabled = true;

            if (role == "NoAccess")
            {
                Response.Redirect("AccessDenied.aspx");
            }
            //if (!IsPostBack)
            //{
            //    try
            //    {

            //        LoadScreen(role, Session["MailID"].ToString());
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //}

            LoadDropDowns(FetchExpenseData(GetDDLValue()).Tables[0]);
        }

    }

    public class SaveData
    {
        public string Category { get; set; }
        public string Status { get; set; }
        public string ExpenseClosureDate { get; set; }
        public string InitiatedDate { get; set; }
        public string InvoiceNo { get; set; }
        public string Remarks { get; set; }
        public string Unique { get; set; }
        public string ServReqNo { get; set; }
        public string PM { get; set; }
        public string DM { get; set; }
        public string ProfitCenter { get; set; }
        public string CustomerCode { get; set; }
    }

    [WebMethod]
    public static string Save(string Data)
    {
        try
        {
            DAL dal = new DAL();
            string strUserName = HttpContext.Current.User.Identity.Name.Split(new string[] { "\\" }, StringSplitOptions.None).Last().ToString();
            string strModifiedOn = DateTime.Now.ToString();
            string Access = dal.CheckAccess(strUserName);

            SqlConnection con = new SqlConnection(connstring);
            JavaScriptSerializer js = new JavaScriptSerializer();
            var items = js.Deserialize<SaveData[]>(Data);
            foreach (SaveData item in items)
            {
                if (!string.IsNullOrWhiteSpace(item.ExpenseClosureDate))
                    item.ExpenseClosureDate = Convert.ToDateTime(item.ExpenseClosureDate).ToShortDateString();
                if (!string.IsNullOrWhiteSpace(item.InitiatedDate))
                    item.InitiatedDate = Convert.ToDateTime(item.InitiatedDate).ToShortDateString();

                SqlCommand cmd = new SqlCommand("SP_ExpenseRecovery_BulkUpdate_EAS_Screen_V2", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@categoryFromScreen", item.Category);
                cmd.Parameters.AddWithValue("@subcategoryFromScreen", item.Status);
                if (item.ExpenseClosureDate == "")
                {
                    cmd.Parameters.AddWithValue("@expenseclosuredateFromScreen", SqlDateTime.Null);
                }
                else
                    cmd.Parameters.AddWithValue("@expenseclosuredateFromScreen", item.ExpenseClosureDate);
                if (item.ExpenseClosureDate == "")
                {
                    cmd.Parameters.AddWithValue("@InitatedDateFromScreen", SqlDateTime.Null);
                }
                else
                    cmd.Parameters.AddWithValue("@InitatedDateFromScreen", item.InitiatedDate);


                cmd.Parameters.AddWithValue("@remarksFromScreen", item.Remarks);
                cmd.Parameters.AddWithValue("@usernameFromScreen", strUserName);
                cmd.Parameters.AddWithValue("@lastupdateonFromScreen", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                cmd.Parameters.AddWithValue("@uniqueFromScreen", item.Unique);
                cmd.Parameters.AddWithValue("@InvoiceNoFromScreen", item.InvoiceNo);
                cmd.Parameters.AddWithValue("@ServCentraleReqNoFromScreen", item.ServReqNo);
                cmd.Parameters.AddWithValue("@DocumentNumberFromScreen", "");
                cmd.Parameters.AddWithValue("@Upload_TypeFromScreen", "Screen Edit");
                cmd.Parameters.AddWithValue("@PMFromScreen", item.PM);
                cmd.Parameters.AddWithValue("@DMFromScreen", item.DM);
                cmd.Parameters.AddWithValue("@ProfitCentreFromScreen", item.ProfitCenter);
                cmd.Parameters.AddWithValue("@CustomerCodeFromScreen", item.CustomerCode);
                cmd.Parameters.AddWithValue("@AccessTypeFromScreen", Access);

                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }

        }
        catch (Exception ex)
        {

            return ex.Message;
        }

        return "1";
    }

    protected void LoadDropDowns(DataTable dtData)
    {


        //foreach (DataRow row in dtData.Rows)
        //    {
        //        foreach (DataColumn col in dtData.Columns)
        //            {
        //                if (col.DataType == typeof(System.String))
        //                {
        //                    row[col] = row[col].ToString().Trim();
        //                }
        //            }

        //    }
        DataTable dt = dtData;
        // Session.Add("ExpenseRecoveryData", dtData);
        // categoryReportParam = "Billable";


        dt.DefaultView.Sort = "Ageing Bucket";
        ddlAgeingBucket.DataSource = dt.DefaultView.ToTable(true, "Ageing Bucket");
        ddlAgeingBucket.DataTextField = "Ageing Bucket";
        ddlAgeingBucket.DataBind();
        ddlAgeingBucket.SelectedValue = ageingbucketReportParam;

        dt.DefaultView.Sort = "CustomerCode";
        ddlCustomerCode.DataSource = dt.DefaultView.ToTable(true, "CustomerCode");
        ddlCustomerCode.DataTextField = "CustomerCode";
        ddlCustomerCode.DataBind();


        dt.DefaultView.Sort = "Profit Center";
        ddlProfitCentre.DataSource = dt.DefaultView.ToTable(true, "Profit Center");
        ddlProfitCentre.DataTextField = "Profit Center";
        ddlProfitCentre.DataBind();


        dt.DefaultView.Sort = "PracticeLine";
        ddlPracticeLine.DataSource = dt.DefaultView.ToTable(true, "PracticeLine");
        ddlPracticeLine.DataTextField = "PracticeLine";
        ddlPracticeLine.DataValueField = "PracticeLine";
        ddlPracticeLine.DataBind();
        ddlPracticeLine.SelectedValue = practicelineReportParam == "ORC" || practicelineReportParam == "SAP" ? practicelineReportParam + "       " : practicelineReportParam + "      ";
        //ddlPracticeLine.SelectedValue = practicelineReportParam;

        dt.DefaultView.Sort = "G/L Acct Long Text";
        ddlLongText.DataSource = dt.DefaultView.ToTable(true, "G/L Acct Long Text");
        ddlLongText.DataTextField = "G/L Acct Long Text";
        ddlLongText.DataBind();

        dt.DefaultView.Sort = "Sub-Category";
        ddlSubCategory_Top.DataSource = dt.DefaultView.ToTable(true, "Sub-Category");
        ddlSubCategory_Top.DataTextField = "Sub-Category";
        ddlSubCategory_Top.DataBind();
        ddlSubCategory_Top.SelectedValue = subcategoryReportParam;



        dt.DefaultView.Sort = "category";
        ddlCategory_Top.DataSource = dt.DefaultView.ToTable(true, "category");
        ddlCategory_Top.DataTextField = "category";
        ddlCategory_Top.DataBind();
        if (categoryReportParam == "Billable" || categoryReportParam == "Non Billable" || categoryReportParam == "Update Needed From PM/DMs")
        {
            ddlCategory_Top.SelectedValue = "ALL";
        }
        else
        {
            ddlCategory_Top.SelectedValue = categoryReportParam;
        }


        dt.DefaultView.Sort = "PM";
        ddlPM.DataSource = dt.DefaultView.ToTable(true, "PM");
        ddlPM.DataTextField = "PM";
        ddlPM.DataBind();


        dt.DefaultView.Sort = "DM";
        ddlDM.DataSource = dt.DefaultView.ToTable(true, "DM");
        ddlDM.DataTextField = "DM";
        ddlDM.DataBind();

        if (categoryReportParam == "Billable" || categoryReportParam == "Non Billable" || categoryReportParam == "Update Needed From PM/DMs")
        {
            ddlCategory_Top.SelectedValue = "ALL";
        }
        else
        {
            ddlCategory_Top.SelectedValue = categoryReportParam;
        }
        ddlSubCategory_Top.SelectedValue = subcategoryReportParam;
        ddlAgeingBucket.SelectedValue = ageingbucketReportParam;
        ddlPracticeLine.SelectedValue = practicelineReportParam;
    }

    public class DB
    {
        public string customerCode { get; set; }
        public string PM { get; set; }
        public string DM { get; set; }
        public string ageingBucket { get; set; }
        public string glLongText { get; set; }
        public string profitCentre { get; set; }
        public string category { get; set; }
        public string status { get; set; }
        public string practiceLine { get; set; }
        public string MailID { get; set; }
        public string role { get; set; }
        public string Commitment { get; set; }
        public int pageno { get; set; }
        public int pagesize { get; set; }
    }

    private string GetDDLValue()
    {
        DB obj = new DB();
        string MailID = System.IO.Path.GetFileName(HttpContext.Current.User.Identity.Name);
        if (Request.QueryString["PracticeLine"] == null)
        {
            obj.customerCode = "All";
            obj.PM = "All";
            obj.DM = "All";
            obj.glLongText = "All";
            obj.profitCentre = "All";
            obj.ageingBucket = "All";
            obj.category = "All";
            obj.status = "All";
            obj.practiceLine = "All";
            obj.MailID = MailID;
            obj.role = dal.CheckAccess(MailID);
            obj.Commitment = "All";
            obj.pageno = -1;
            obj.pagesize = 0;
        }
        else
        {

            obj.customerCode = ddlCustomerCode.SelectedValue;
            obj.PM = ddlPM.SelectedValue == "-1" ? "All" : ddlPM.SelectedValue;
            obj.DM = ddlDM.SelectedValue == "-1" ? "All" : ddlDM.SelectedValue;
            obj.glLongText = ddlCustomerCode.SelectedValue;
            obj.profitCentre = ddlProfitCentre.SelectedValue;
            obj.ageingBucket = ddlAgeingBucket.SelectedValue == "-1" ? "All" : ddlAgeingBucket.SelectedValue;
            obj.category = ddlCategory_Top.SelectedValue;
            obj.status = ddlSubCategory_Top.SelectedValue;
            obj.practiceLine = ddlPracticeLine.SelectedValue;
            obj.MailID = MailID;
            obj.role = dal.CheckAccess(MailID);
            obj.Commitment = ddlCommitments.SelectedValue;
            //obj.Report_BillingCategory = categoryReportParam;
            //obj.Report_BillingStatus = billingstatusReportParam;
            //obj.Report_AgeingBucket = ageingbucketReportParam;
            //obj.Report_PracticeLine = PracticeLineReportParam;
            obj.pageno = -1;
            obj.pagesize = 0;
        }


        string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
        return json;
    }

    private string GetDDLJson()
    {
        string query = "select Category,Subcategory,[Contract Type] as contracttype from expense_cat_mapping";
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(connstring);
        cmd.CommandText = query;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Connection = con;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds1 = new System.Data.DataSet();
        da.Fill(ds1);
        string json = JsonConvert.SerializeObject(ds1, Formatting.Indented);
        return json;
    }
    [WebMethod]
    public static string GetInitialData(string Data)
    {
        DataSet ds = FetchExpenseData(Data);
        int count = 0; double dbusd = 0;
        if (ds != null && ds.Tables.Count == 3)
        {
            int.TryParse(ds.Tables[1].Rows[0][0] + "", out count);
            double.TryParse(ds.Tables[2].Rows[0][0] + "", out dbusd);
        }

        dbusd = Math.Round(dbusd, 2);




        IntialData obj = new IntialData();
        obj.Count = count;
        obj.DBUSD = dbusd.ToString("C");
        string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
        return json;

    }
    public class IntialData
    {
        public int Count { get; set; }
        public string DBUSD { get; set; }
    }
    protected static DataSet FetchExpenseData(string Data)
    {


        string strUserName = HttpContext.Current.User.Identity.Name.Split(new string[] { "\\" }, StringSplitOptions.None).Last().ToString();
        //strUserName = "Ketan_Sankar";
        SqlConnection con = new SqlConnection(connstring);


        DAL dal = new DAL();
        JavaScriptSerializer js = new JavaScriptSerializer();
        var obj = js.Deserialize<DB>(Data);
        string _role = dal.CheckAccess(strUserName);



        SqlCommand cmd = new SqlCommand("SP_ExpenseRecovery_FetchData_New_V2", con);
        cmd.Parameters.AddWithValue("@customerCode_Filter", obj.customerCode);
        cmd.Parameters.AddWithValue("@ageingBucket_Filter", obj.ageingBucket);
        cmd.Parameters.AddWithValue("@glLongText_Filter", obj.glLongText);
        cmd.Parameters.AddWithValue("@PM_Filter", obj.PM);
        cmd.Parameters.AddWithValue("@DM_Filter", obj.DM);
        cmd.Parameters.AddWithValue("@ProfitCentre_Filter", obj.profitCentre);
        cmd.Parameters.AddWithValue("@category_Filter", obj.category);
        cmd.Parameters.AddWithValue("@Status_Filter", obj.status);
        cmd.Parameters.AddWithValue("@PracticeLine_Filter", obj.practiceLine);
        cmd.Parameters.AddWithValue("@MailID", strUserName);
        cmd.Parameters.AddWithValue("@AccessTYpe", _role);
        cmd.Parameters.AddWithValue("@Commitment", obj.Commitment);
        cmd.Parameters.AddWithValue("@pageno", obj.pageno);
        cmd.Parameters.AddWithValue("@pagesize", obj.pagesize);

        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Connection = con;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new System.Data.DataSet();
        da.Fill(ds);


        if (obj.customerCode == string.Empty || obj.ageingBucket == string.Empty || obj.glLongText == string.Empty || obj.PM == string.Empty || obj.DM == string.Empty || obj.profitCentre == string.Empty || obj.category == string.Empty || obj.status == string.Empty || obj.practiceLine == string.Empty || obj.Commitment == string.Empty || MailID == string.Empty || role == string.Empty)
        {
            //string strUserName = HttpContext.Current.User.Identity.Name.Split(new string[] { "\\" }, StringSplitOptions.None).Last().ToString();
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += strUserName;
            message += MailID;
            message += role;
            message += Environment.NewLine;
            message += Data;
            string path = HttpContext.Current.Server.MapPath("~/Log/LogData.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }

        return ds;
    }

    [WebMethod]
    public static string GetData(string Data)
    {

        DataSet ds = FetchExpenseData(Data);

        string json = JsonConvert.SerializeObject(ds, Formatting.Indented);
        return json;
    }




    protected void ImageHelp_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("FAQ.aspx");

    }




    protected void lbBulkUpdate_Click(object sender, EventArgs e)
    {

        //Lblmsg.Visible = false;
        DAL dal = new DAL();
        DataSet ds = new DataSet();
        DataTable dtGrid = new DataTable();
        DataTable dt = new DataTable();

        //DataSet dsStatus = new DataSet();
        //string query_status = "";
        //query_status = "select * from Expense_category";
        //dsStatus = dal.GetDataSet(query_status);
        //Session.Add("dsStatus", dsStatus);
        string MailID = System.IO.Path.GetFileName(HttpContext.Current.User.Identity.Name);
        string role = dal.CheckAccess(MailID);



        dtGrid = dal.FetchDetailsforTemplate(role, MailID, ddlSubCategory_Top.SelectedValue, ddlCustomerCode.SelectedValue, ddlPM.SelectedValue == "-1" ? "All" : ddlPM.SelectedValue, ddlLongText.SelectedValue, ddlAgeingBucket.SelectedValue == "-1" ? "All" : ddlAgeingBucket.SelectedValue, ddlDM.SelectedValue == "-1" ? "All" : ddlDM.SelectedValue, ddlProfitCentre.SelectedValue, ddlCategory_Top.SelectedValue, ddlPracticeLine.SelectedValue, ddlCommitments.SelectedValue);
        //Session.Add("dtGrid", dtGrid);
        dt = dtGrid;
        //dsStatus = (DataSet)Session["dsStatus"];

        string folderadress = @"~/App_Data\ExpenseRecovery_Upload_NewTemplate2.xlsx";
        folderadress = HttpContext.Current.Server.MapPath(folderadress);
        string storefolderadress = @"~/Template";
        storefolderadress = HttpContext.Current.Server.MapPath(storefolderadress);
        Microsoft.Office.Interop.Excel.Application WRExcel = new Microsoft.Office.Interop.Excel.Application();
        Microsoft.Office.Interop.Excel.Workbooks WRwbs = null;
        //Microsoft.Office.Interop.Excel.Workbook WRwb = new Microsoft.Office.Interop.Excel.Workbook();
        object objOpt = System.Reflection.Missing.Value;
        Microsoft.Office.Interop.Excel.Workbook WRwb = WRExcel.Workbooks.Add(objOpt);
        Microsoft.Office.Interop.Excel._Worksheet WRws = null;

        Microsoft.Office.Interop.Excel.Worksheet excelSheet1 = null;

        WRExcel.Visible = false;
        WRwbs = WRExcel.Workbooks;


        WRwb = WRwbs.Open(folderadress, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt,
            objOpt, objOpt, objOpt, objOpt, objOpt, objOpt);
        Microsoft.Office.Interop.Excel.Sheets WRss = null;
        WRss = WRwb.Sheets;
        String excelFile1 = "~\\App_Data\\ExpenseRecovery_Upload_" + MailID + ".xls";
        String destPath = Server.MapPath(excelFile1);
        try
        {
           



            //Microsoft.Office.Interop.Excel.Workbook excelBook = excelapp.Workbooks.Add(oMissing);
            excelSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)WRss.get_Item("Data");



            int ColumnIndex = 0;
            int rowIndex = 0;
            DataTable dt1 = new DataTable();




            int rows = dt.Rows.Count;
            int columns = dt.Columns.Count;
            int r = 0; int c = 0;
            object[,] DataArray = new object[rows + 1, columns + 1];
            for (c = 0; c <= columns - 1; c++)
            {
                DataArray[r, c] = dt.Columns[c].ColumnName;
                for (r = 0; r <= rows - 1; r++)
                {
                    DataArray[r, c] = dt.Rows[r][c];
                } //end row loop
            } //end column loop

            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)excelSheet1.Cells[2, 1];
            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)excelSheet1.Cells[1 + dt.Rows.Count, dt.Columns.Count];
            Microsoft.Office.Interop.Excel.Range range = excelSheet1.get_Range(c1, c2);


            //Fill Array in Excel
            range.Value2 = DataArray;


         
            excelSheet1.get_Range("F2", "F" + dt.Rows.Count + 1).NumberFormat = "MM/DD/YYYY";
            excelSheet1.get_Range("G2", "G" + dt.Rows.Count + 1).NumberFormat = "MM/DD/YYYY";
            excelSheet1.get_Range("W2", "W" + dt.Rows.Count + 1).NumberFormat = "MM/DD/YYYY";
            excelSheet1.get_Range("AF2", "AF" + dt.Rows.Count + 1).NumberFormat = "MM/DD/YYYY";
            //excelSheet1.get_Range("I2", "I" + dt.Rows.Count + 1).NumberFormat = "@";

            //excelSheet1.get_Range("As2", "As" + dt.Rows.Count + 1).Validation.InputMessage = "Sarath";


            //excelSheet1.get_Range("A1", "AX1").AutoFit();
            excelSheet1.get_Range("A1", "AE1").AutoFilter(1, objOpt, Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlAnd, objOpt, true);


            excelSheet1.Protect(1, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, 1, 1, objOpt);

            WRwb.SaveCopyAs(destPath);
            WRwb.Close(false);

           


           

        }
        catch (Exception ex)
        {
            lbBulkUpdate.Text = (ex.Message);
        }
        finally
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (WRws != null)
            {
                //wksht.Delete();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRws);
                WRws = null;
            }

            if (WRss != null)
            {
                //wksht.Delete();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRss);
                WRss = null;
            }

            if (WRwb != null)
            {
                //wksht.Delete();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRwb);
                WRwb = null;
            }
            if (WRwbs != null)
            {
                //wksht.Delete();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRwbs);
                WRwbs = null;
            }
            if (WRExcel != null)
            {
                WRExcel.Quit();
                int hWnd1 = WRExcel.Application.Hwnd;
                TryKillProcessByMainWindowHwnd(hWnd1);

                //wksht.Delete();
                //WRExcel.Quit();
                //System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRExcel);
                //WRExcel = null;




            }
            if (excelSheet1 != null)
            {
                //wksht.Delete();

                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excelSheet1);
                excelSheet1 = null;

            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();


        }

         ApplyPermissionToExcel(destPath);
        iframe.Attributes.Add("src", "DownloadFile.aspx?key=ExpenseRecovery_Upload");

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        GC.WaitForPendingFinalizers();


    }

    public static bool TryKillProcessByMainWindowHwnd(int hWnd)
    {
        uint processID;
        GetWindowThreadProcessId((IntPtr)hWnd, out processID);
        if (processID == 0) return false;
        try
        {
            Process.GetProcessById((int)processID).Kill();
        }
        catch (ArgumentException)
        {
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }

    [DllImport("user32.dll")]


    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
    protected void btnDownload_Click(object sender, EventArgs e)
    {

    }

    protected void btndownloadpivot_Click(object sender, EventArgs e)
    {
        // Session["key"] = null;
        try
        {
            //Lblmsg.Visible = false;
            DAL dal = new DAL();
            DataSet ds = new DataSet();
            DataTable dtGrid = new DataTable();
            DataTable dt = new DataTable();

            DataSet dsStatus = new DataSet();
            string query_status = "";
            query_status = "select * from Expense_category";
            dsStatus = dal.GetDataSet(query_status);
            //Session.Add("dsStatus", dsStatus);
            DataSet dsGrid = BusinessLogic.GetDataSet("exec SP_FetchDataForMacro");
            dt = dsGrid.Tables[0];
            // DataTable dt1 = dsGrid.Tables[1];
            //Session.Add("dtGrid", dt);

            string folderadress = @"~/ExcelOperations\ExpenseRecovery_template_Pivot.xlsx";
            folderadress = HttpContext.Current.Server.MapPath(folderadress);
            string storefolderadress = @"~/Template";
            storefolderadress = HttpContext.Current.Server.MapPath(storefolderadress);
            Microsoft.Office.Interop.Excel.Application WRExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks WRwbs = null;
            //Microsoft.Office.Interop.Excel.Workbook WRwb = new Microsoft.Office.Interop.Excel.Workbook();
            object objOpt = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Excel.Workbook WRwb = WRExcel.Workbooks.Add(objOpt);
            Microsoft.Office.Interop.Excel._Worksheet WRws = null;
            WRExcel.Visible = false;
            WRwbs = WRExcel.Workbooks;
            WRwb = WRwbs.Open(folderadress, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt,
                objOpt, objOpt, objOpt, objOpt, objOpt, objOpt);
            Microsoft.Office.Interop.Excel.Sheets WRss = null;
            WRss = WRwb.Sheets;

            Microsoft.Office.Interop.Excel.ApplicationClass excelapp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            excelapp.Visible = false;
            //string temp1 = Server.MapPath("./App_Data\\ExpenseRecovery_Upload.xls");
            //if (File.Exists(temp1))
            //    File.Delete(temp1);


            //Object oMissing = System.Reflection.Missing.Value;

            //DataTable dtStatus = new DataTable();
            //dtStatus = dsStatus.Tables[0];

            ////for status
            //var listStatus = new List<string>();
            //for (int i = 0; i < dtStatus.Rows.Count; i++)
            //{
            //    listStatus.Add(dtStatus.Rows[i][0].ToString());
            //}
            //var flatSTP = string.Join(",", listStatus.ToArray());



            //Microsoft.Office.Interop.Excel.Workbook excelBook = excelapp.Workbooks.Add(oMissing);
            Microsoft.Office.Interop.Excel.Worksheet excelSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)WRss.get_Item("ExpenseRec_data");
            //Microsoft.Office.Interop.Excel.Worksheet excelSheet2 = (Microsoft.Office.Interop.Excel.Worksheet)WRss.get_Item("Instructions");
            // excelSheet2.Delete();


            int ColumnIndex = 0;
            int rowIndex = 0;
            DataTable dt1 = new DataTable();
            int rows = dt.Rows.Count;
            int columns = dt.Columns.Count;
            int r = 0; int c = 0;
            object[,] DataArray = new object[rows + 1, columns + 1];
            for (c = 0; c <= columns - 1; c++)
            {
                DataArray[r, c] = dt.Columns[c].ColumnName;
                for (r = 0; r <= rows - 1; r++)
                {
                    DataArray[r, c] = dt.Rows[r][c];
                } //end row loop
            } //end column loop
            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)excelSheet1.Cells[2, 1];
            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)excelSheet1.Cells[1 + dt.Rows.Count, dt.Columns.Count];
            Microsoft.Office.Interop.Excel.Range range = excelSheet1.get_Range(c1, c2);
            //Fill Array in Excel
            range.Value2 = DataArray;



            excelSheet1.get_Range("F2", "F" + dt.Rows.Count + 1).NumberFormat = "MM/DD/YYYY";
            excelSheet1.get_Range("V2", "V" + dt.Rows.Count + 1).NumberFormat = "MM/DD/YYYY";
            excelSheet1.get_Range("AE2", "AE" + dt.Rows.Count + 1).NumberFormat = "MM/DD/YYYY";
            //excelSheet1.get_Range("AD2", "AD" + dt.Rows.Count + 1).NumberFormat = "MM/DD/YYYY";


            string filename = "Expense Recovery Pivot_" + DateTime.Now.ToString("ddMMMyyyy_HHmm") + "IST.xlsx";
            String excelFile1 = "~\\ExcelOperations\\" + filename;
            String destPath = Server.MapPath(excelFile1);
            // Session.Add("destPath", destPath);
            RefreshPivots(WRss);
            WRwb.SaveCopyAs(destPath);



            // Session["key"] = filename;
         
            WRwb.SaveCopyAs(destPath);
            WRwb.Close(false);
            WRExcel.Quit();

            //oExcel = null;
            //oModule = null;
            //oBook = null;
            //WRss = null;
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRExcel);
            // System.Runtime.InteropServices.Marshal.FinalReleaseComObject(oModule);
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRwb);
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRss);
            GC.Collect();
            // DownloadFileProject(filename);
            ApplyPermissionToExcel(destPath);
            iframe.Attributes.Add("src", "DownloadFile.aspx?key=ExpenseRecoveryPivot");




        }
        catch (Exception ex)
        {
        }
    }

    private void ApplyPermissionToExcel(string filePath)
    {
        if (!ExcelPermissionDelay.IsPermissionEnabled())
            return;
        // Process excelProcess = null;
        try
        {

            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            object missing = System.Reflection.Missing.Value;

            Microsoft.Office.Interop.Excel.Workbook wb = excelApp.Workbooks.Open(filePath, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

            wb.Activate();
            wb.Permission.Enabled = true;
            wb.Permission.RemoveAll();
            UserPermission userper = wb.Permission.Add("Everyone", MsoPermission.msoPermissionChange);
            userper.ExpirationDate = DateTime.Now.AddDays(60);


            excelApp.DisplayAlerts = false;
            wb.SaveAs(filePath);

            wb.Close(false, filePath, null);


            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(wb);
            wb = null;

            excelApp.Quit();
            excelApp = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();



        }
        catch (Exception)
        {


        }

    }


    public void RefreshPivots(Microsoft.Office.Interop.Excel.Sheets excelsheets)
    {

        foreach (Microsoft.Office.Interop.Excel.Worksheet pivotSheet in excelsheets)
        {
            Microsoft.Office.Interop.Excel.PivotTables pivotTables = (Microsoft.Office.Interop.Excel.PivotTables)pivotSheet.PivotTables();
            int pivotTablesCount = pivotTables.Count;
            if (pivotTablesCount > 0)
            {
                for (int i = 1; i <= pivotTablesCount; i++)
                {


                    pivotTables.Item(i).RefreshTable();

                }
            }
        }
    }
    protected void btndownloadold_Click(object sender, EventArgs e)
    {
        string MailID = System.IO.Path.GetFileName(HttpContext.Current.User.Identity.Name);
        string role = dal.CheckAccess(MailID);
        //Session["key"] = null;
        //Lblmsg.Visible = false;
        //Message1();
        //Response.Write("<script language='javascript'>alert('For updating the records, the excel should be downloaded using Download Bulk Update Template button');</script>");
        subCategory_top = ddlSubCategory_Top.SelectedValue;
        customerCode = ddlCustomerCode.SelectedValue;
        ageingBucket = ddlAgeingBucket.SelectedValue == "-1" ? "All" : ddlAgeingBucket.SelectedValue;
        glLongText = ddlLongText.SelectedValue;
        PM = ddlPM.SelectedValue == "-1" ? "All" : ddlPM.SelectedValue;
        DM = ddlDM.SelectedValue == "-1" ? "All" : ddlDM.SelectedValue;


        profitCentre = ddlProfitCentre.SelectedValue;
        category_top = ddlCategory_Top.SelectedValue;
        practiceLine = ddlPracticeLine.SelectedValue.ToString();
        Commitment = ddlCommitments.SelectedValue.ToString();

        SqlCommand cmd = new SqlCommand();

        string cmdExpRec = "exec LoadExpenseRecoveryGrid_DownLoad @subCategory_top,@customerCode,@ageingBucket,@glLongText,@PM,@DM,@role,@MailID,@profitCentre,@category_top,@practiceLine,@Commitment";
        cmd.CommandText = cmdExpRec;
        cmd.Parameters.AddWithValue("@subCategory_top", subCategory_top);
        cmd.Parameters.AddWithValue("@customerCode", customerCode);
        cmd.Parameters.AddWithValue("@ageingBucket", ageingBucket);
        cmd.Parameters.AddWithValue("@glLongText", glLongText);
        cmd.Parameters.AddWithValue("@PM", PM);
        cmd.Parameters.AddWithValue("@DM", DM);
        cmd.Parameters.AddWithValue("@profitCentre", profitCentre);
        cmd.Parameters.AddWithValue("@category_top", category_top);

        cmd.Parameters.AddWithValue("@practiceLine", practiceLine);
        cmd.Parameters.AddWithValue("@MailID", MailID);
        cmd.Parameters.AddWithValue("@role", role);
        cmd.Parameters.AddWithValue("@Commitment", Commitment);

        DataSet dsExpRec = dal.FetchDataSet(cmd);


        //string cmdExpRec = "exec [LoadExpenseRecoveryGrid_DownLoad] '" + subCategory_top + "','" + customerCode + "','" + ageingBucket + "','" + glLongText + "','" + PM + "','" + DM + "','" + role + "','" + MailID + "','" + profitCentre + "','" + category_top + "','" + practiceLine + "'";
        //DataSet dsExpRec = BusinessLogic.GetDataSet(cmdExpRec);
        DownloadExcel2(dsExpRec.Tables[0]);
    }



    private void DeleteOldFiles(string folder)
    {
        string[] files = Directory.GetFiles(folder);

        foreach (string file in files)
        {

            try
            {
                FileInfo fi = new FileInfo(file);
                if (fi.LastWriteTime < DateTime.Now.AddHours(-1))
                    fi.Delete();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

 

    void SaveAndDownload(string fileName, DataTable dt)
    {
        try
        {
            fileName = fileName.Replace(".xls", "");
            string endFileName = string.Format("{0}_{1}{2}", fileName, DateTime.Now.ToString("ddMMMyyyy_hhmmtt"), ".xlsx");
            string destPath = Path.Combine(Server.MapPath("~/DownloadedFiles/"), endFileName);


            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = null;
            //Create the worksheet
            if (dt.Rows.Count > 0)
            {
                ws = pck.Workbook.Worksheets.Add("Report");
                ws.Cells["A1"].LoadFromDataTable(dt, true);
                var headerrange = ws.Cells[1, 1, 1, 100];
                headerrange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                headerrange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(141, 180, 226));
                headerrange.Style.Font.Bold = true;



                var allrange = ws.Cells;
                allrange.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                allrange.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                allrange.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                allrange.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                allrange.Style.Border.Left.Color.SetColor(Color.FromName("#f0f0f0"));
                allrange.Style.Border.Right.Color.SetColor(Color.FromName("#f0f0f0"));
                allrange.Style.Border.Top.Color.SetColor(Color.FromName("#f0f0f0"));
                allrange.Style.Border.Bottom.Color.SetColor(Color.FromName("#f0f0f0"));

                ws.Cells.Style.Font.Size = 10; //Default font size for whole sheet
                ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet
                ws.Cells.Style.Font.Color.SetColor(Color.Black);

                ws.Cells.AutoFitColumns();

            }

            DeleteOldFiles(Server.MapPath("~/DownloadedFiles/"));

            FileInfo file = new FileInfo(destPath);

            pck.SaveAs(file);
            pck.Dispose();
            pck = null;
            ApplyPermissionToExcel(destPath);

            iframe.Attributes.Add("src", "DownloadFile.aspx?key=" + endFileName);



        }
        catch (Exception ex)
        {


        }




    }

   

    private void DownloadExcel2(DataTable dt)
    {
        SaveAndDownload("ExpenseRecovery.xls", dt);
       
        //try
        //{
        //    if (dt == null || dt.Columns.Count == 0)
        //        throw new Exception("ExportToExcel: Null or empty input table!\n");

        //    string folderadress = @"~/App_Data\ExpenseRecovery_DownloadTemplate.xlsx";
        //    folderadress = HttpContext.Current.Server.MapPath(folderadress);
        //    string storefolderadress = @"~/Template";
        //    storefolderadress = HttpContext.Current.Server.MapPath(storefolderadress);
        //    Microsoft.Office.Interop.Excel.Application WRExcel = new Microsoft.Office.Interop.Excel.Application();
        //    Microsoft.Office.Interop.Excel.Workbooks WRwbs = null;
        //    //Microsoft.Office.Interop.Excel.Workbook WRwb = new Microsoft.Office.Interop.Excel.Workbook();
        //    object objOpt = System.Reflection.Missing.Value;
        //    Microsoft.Office.Interop.Excel.Workbook WRwb = WRExcel.Workbooks.Add(objOpt);
        //    Microsoft.Office.Interop.Excel._Worksheet WRws = null;



        //    WRExcel.Visible = false;
        //    WRwbs = WRExcel.Workbooks;


        //    WRwb = WRwbs.Open(folderadress, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt, objOpt,
        //        objOpt, objOpt, objOpt, objOpt, objOpt, objOpt);
        //    Microsoft.Office.Interop.Excel.Sheets WRss = null;
        //    WRss = WRwb.Sheets;



        //    String excelFile1 = "~\\App_Data\\ExpenseRecovery.xlsx";
        //    String destPath = Server.MapPath(excelFile1);
        //    if (File.Exists(destPath))
        //    {
        //        File.Delete(destPath);
        //    }
        //    // check fielpath


        //    Microsoft.Office.Interop.Excel.Worksheet excelSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)WRss.get_Item("ExpenseRecovery");



        //    int ColumnIndex = 0;
        //    int rowIndex = 0;
        //    DataTable dt1 = new DataTable();

        //    // column headings
        //    for (int i = 0; i < dt.Columns.Count; i++)
        //    {
        //        excelSheet1.Cells[1, (i + 1)] = dt.Columns[i].ColumnName;

        //    }


        //    int rows = dt.Rows.Count;
        //    int columns = dt.Columns.Count;
        //    int r = 0; int c = 0;
        //    object[,] DataArray = new object[rows + 1, columns + 1];
        //    for (c = 0; c <= columns - 1; c++)
        //    {
        //        DataArray[r, c] = dt.Columns[c].ColumnName;
        //        for (r = 0; r <= rows - 1; r++)
        //        {
        //            DataArray[r, c] = dt.Rows[r][c];
        //        } //end row loop
        //    } //end column loop

        //    Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)excelSheet1.Cells[2, 1];
        //    Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)excelSheet1.Cells[1 + dt.Rows.Count, dt.Columns.Count];
        //    Microsoft.Office.Interop.Excel.Range range = excelSheet1.get_Range(c1, c2);


        //    //Fill Array in Excel
        //    range.Value2 = DataArray;


        //    if (destPath != null && destPath != "")
        //    {
        //        WRwb.SaveCopyAs(destPath);
        //        WRwb.Close(false);
        //        WRExcel.Quit();
        //        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRwb);
        //        // System.Runtime.InteropServices.Marshal.FinalReleaseComObject(oModule);
        //        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRExcel);
        //        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(WRss);
        //        GC.Collect();
        //        // DownloadFileProject(filename);
        //        // Session["key"] = "ExpenseRecovery.xlsx";
        //        //updatepanel1.Update();

        //        ApplyPermissionToExcel(destPath);

            //iframe.Attributes.Add("src", "DownloadFile.aspx?key=ExpenseRecovery");


        //    }

        //    else
        //    {
        //        //Lblmsg.Visible = true;
        //        //Lblmsg.ForeColor = Color.Red;
        //        //Lblmsg.Text = "Some error occurred..File cannot be downloaded";
        //    }







        //}
        //catch (Exception ex)
        //{
        //    throw new Exception("ExportToExcel: \n" + ex.Message);
        //}

        //PopUp("File Saved");
    }


}