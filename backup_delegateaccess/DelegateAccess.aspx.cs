using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;
using System.Data.SqlClient;

public partial class DelegateAccess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Menu Menumain = (Menu)Master.FindControl("Menu_MainOptions");
        //Menumain.Items[2].Selected = true;
        if (!IsPostBack)
        {
            string MailID = System.IO.Path.GetFileName(HttpContext.Current.User.Identity.Name);


            // MailID = "Sridevi_Srirangan";
            //Session["MailID"] = MailID;
            
            DAL dal = new DAL();
            string role = dal.CheckAccess(MailID);
           //Session.Add("Role", role);
            SqlCommand cmd = new SqlCommand();

            string cmdExpRec = "exec SP_ExpenseRecovery_FetchData @customerCode,@ageingBucket,@glLongText,@PM,@DM,@profitCentre,@category,@status,@practiceLine,@MailID,@role,@Commitment";
            cmd.CommandText = cmdExpRec;
            cmd.Parameters.AddWithValue("@customerCode", "ALL");
            cmd.Parameters.AddWithValue("@ageingBucket", "ALL");
            cmd.Parameters.AddWithValue("@glLongText", "ALL");
            cmd.Parameters.AddWithValue("@PM", "ALL");
            cmd.Parameters.AddWithValue("@DM", "ALL");
            cmd.Parameters.AddWithValue("@profitCentre", "ALL");
            cmd.Parameters.AddWithValue("@category", "ALL");
            cmd.Parameters.AddWithValue("@status", "ALL");
            cmd.Parameters.AddWithValue("@practiceLine", "ALL");
            cmd.Parameters.AddWithValue("@MailID", MailID);
            cmd.Parameters.AddWithValue("@role", role);
            cmd.Parameters.AddWithValue("@Commitment", "ALL");

            DataSet dsExpRec = dal.FetchDataSet(cmd);
           
            lblMessage.Text = "**Please note that, all delegated accesses will be revoked at the end of the month**";
            DataTable dtData = dsExpRec.Tables[0];

            dtData.DefaultView.Sort = "CustomerCode";
            DataTable dtCustomerCode = dtData.DefaultView.ToTable(true, "CustomerCode");
            chkCustomerCodeList.DataSource = dtCustomerCode;
            chkCustomerCodeList.DataTextField = "CustomerCode";
            chkCustomerCodeList.DataBind();



            dtData.DefaultView.Sort = "MasterClientCode";
            DataTable dtMCC = dtData.DefaultView.ToTable(true, "MasterClientCode");
            chkMCCList.DataSource = dtMCC;
            chkMCCList.DataTextField = "MasterClientCode";
            chkMCCList.DataBind();



            dtData.DefaultView.Sort = "PracticeLine";
            DataTable dtPracticeLine = dtData.DefaultView.ToTable(true, "PracticeLine");
            chkPracticeLineList.DataSource = dtPracticeLine;
            chkPracticeLineList.DataTextField = "PracticeLine";
            chkPracticeLineList.DataBind();
      

            dtData.DefaultView.Sort = "Profit Center";
            DataTable dtProfitCentre = dtData.DefaultView.ToTable(true, "Profit Center");
            chkProfitCentreList.DataSource = dtProfitCentre;
            chkProfitCentreList.DataTextField = "Profit Center";
            chkProfitCentreList.DataBind();


            dtData.DefaultView.Sort = "PM";
            DataTable dtPM = dtData.DefaultView.ToTable(true, "PM");
            chkPMList.DataSource = dtPM;
            chkPMList.DataTextField = "PM";
            chkPMList.DataBind();



            dtData.DefaultView.Sort = "DM";
            DataTable dtDM = dtData.DefaultView.ToTable(true, "DM");
            chkDMList.DataSource = dtDM;
            chkDMList.DataTextField = "DM";
            chkDMList.DataBind();

            
                plCustomerCode.Visible = false;
            plMCC.Visible = false;
            plDM.Visible = false;
            plPM.Visible = false;
            plPracticeLine.Visible = false;
            plProfitCentre.Visible = false;
        }
    }
    protected void rdlSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdlSelection.SelectedValue == "CC")
        {
            plCustomerCode.Visible = true;
            plMCC.Visible = false;
            plDM.Visible = false;
            plPM.Visible = false;
            plPracticeLine.Visible = false;
            plProfitCentre.Visible = false;

        }
        else if (rdlSelection.SelectedValue == "MCC")
        {
            plCustomerCode.Visible = false;
            plMCC.Visible = true;
            plDM.Visible = false;
            plPM.Visible = false;
            plPracticeLine.Visible = false;
            plProfitCentre.Visible = false;
        }

        else if (rdlSelection.SelectedValue == "PracticeLine")
        {
            plCustomerCode.Visible = false;
            plMCC.Visible = false;
            plDM.Visible = false;
            plPM.Visible = false;
            plPracticeLine.Visible = true;
            plProfitCentre.Visible = false;
        }
        else if (rdlSelection.SelectedValue == "ProfitCentre")
        {
            plCustomerCode.Visible = false;
            plMCC.Visible = false;
            plDM.Visible = false;
            plPM.Visible = false;
            plPracticeLine.Visible = false;
            plProfitCentre.Visible = true;
        }
        else if (rdlSelection.SelectedValue == "PM")
        {
            plCustomerCode.Visible = false;
            plMCC.Visible = false;
            plDM.Visible = false;
            plPM.Visible = true;
            plPracticeLine.Visible = false;
            plProfitCentre.Visible = false;
        }
        else if (rdlSelection.SelectedValue == "DM")
        {
            plCustomerCode.Visible = false;
            plMCC.Visible = false;
            plDM.Visible = true;
            plPM.Visible = false;
            plPracticeLine.Visible = false;
            plProfitCentre.Visible = false;
        }
    }
    protected void lbSelectAllCusCode_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chkCustomerCodeList.Items.Count; i++)
        {
            chkCustomerCodeList.Items[i].Selected = true;
           // chkCustomerCodeList.Attributes.Add("style", "color: Green ");
        }
    }
    protected void lbUnselectAllCusCode_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chkCustomerCodeList.Items.Count; i++)
        {
            chkCustomerCodeList.Items[i].Selected = false;
           // chkCustomerCodeList.Attributes.Add("style", "color: Black ");
        }
    }


    protected void lbSelectAllCustomerCode_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chkCustomerCodeList.Items.Count; i++)
        {
            chkCustomerCodeList.Items[i].Selected = true;
            //chkCustomerCodeList.Attributes.Add("style", "color: Green ");
        }
    }
    protected void lbUnselectAllCustomerCode_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chkCustomerCodeList.Items.Count; i++)
        {
            chkCustomerCodeList.Items[i].Selected = false;
        }
    }


    protected void lbSelectAllMCC_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chkMCCList.Items.Count; i++)
        {
            chkMCCList.Items[i].Selected = true;
            //chkMCCList.Attributes.Add("style", "color: Green ");
        }
    }
    protected void lbUnselectAllMCC_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chkMCCList.Items.Count; i++)
        {
            chkMCCList.Items[i].Selected = false;
           // chkMCCList.Attributes.Add("style", "color: Black ");
        }
    }


    protected void lbSelectAllPracticeLine_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chkPracticeLineList.Items.Count; i++)
        {
            chkPracticeLineList.Items[i].Selected = true;
            //chkPracticeLineList.Attributes.Add("style", "color: Green ");
        }
    }
    protected void lbUnselectAllPracticeLine_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chkPracticeLineList.Items.Count; i++)
        {
            chkPracticeLineList.Items[i].Selected = false;
           // chkPracticeLineList.Attributes.Add("style", "color: Black ");
        }
    }


    protected void lbSelectAllProfitCentre_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chkProfitCentreList.Items.Count; i++)
        {
            chkProfitCentreList.Items[i].Selected = true;
           // chkProfitCentreList.Attributes.Add("style", "color: Green ");
        }
    }
    protected void lbUnselectAllProfitCentre_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chkProfitCentreList.Items.Count; i++)
        {
            chkProfitCentreList.Items[i].Selected = false;
            //chkProfitCentreList.Attributes.Add("style", "color: Black ");
        }
    }


    protected void lbSelectAllPM_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chkPMList.Items.Count; i++)
        {
            chkPMList.Items[i].Selected = true;
          //  chkPMList.Attributes.Add("style", "color: Green ");
        }
    }
    protected void lbUnselectAllPM_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chkPMList.Items.Count; i++)
        {
            chkPMList.Items[i].Selected = false;
          //  chkPMList.Attributes.Add("style", "color: Black ");
        }
    }


    protected void lbSelectAllDM_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chkDMList.Items.Count; i++)
        {
            chkDMList.Items[i].Selected = true;
            //chkDMList.Attributes.Add("style", "color: Green ");
        }
    }
    protected void lbUnselectAllDM_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chkDMList.Items.Count; i++)
        {
            chkDMList.Items[i].Selected = false;
            //chkDMList.Attributes.Add("style", "color: Black ");
        }
    }

    protected void btnDelegate_Click(object sender, EventArgs e)
    {
        DAL daObj = new DAL();
        lblMessage.Visible = false;

        string UserName = System.IO.Path.GetFileName(HttpContext.Current.User.Identity.Name);
        string EmpMailID = txtEmpmailID.Text.Trim();

        bool isCustomerCodeSelected = chkCustomerCodeList.SelectedIndex != -1;
        bool isMCCSelected = chkMCCList.SelectedIndex != -1;
        bool isPracticeSelected = chkPracticeLineList.SelectedIndex != -1;
        bool isPUSelected = chkProfitCentreList.SelectedIndex != -1;
        bool isPMSelected = chkPMList.SelectedIndex != -1;
        bool isDMSelected = chkDMList.SelectedIndex != -1;

        int result = 0;
        int DelegatedRecords = 0;

        if (EmpMailID == "")
        {
            Response.Write("<script>alert('Please enter a valid MailID')</script>");
            txtEmpmailID.Focus();
        }
        else if (rdlSelection.SelectedValue == "CC" && isCustomerCodeSelected == false)
        {
            Response.Write("<script>alert('Please select atleast one Customer code')</script>");
        }
        else if (rdlSelection.SelectedValue == "MCC" && isMCCSelected == false)
        {
            Response.Write("<script>alert('Please select atleast one MCC')</script>");
        }
        else if (rdlSelection.SelectedValue == "PracticeLine" && isPracticeSelected == false)
        {
            Response.Write("<script>alert('Please select atleast one Practice line')</script>");

        }
        else if (rdlSelection.SelectedValue == "ProfitCentre" && isPUSelected == false)
        {
            Response.Write("<script>alert('Please select atleast one profit centre')</script>");
        }
        else if (rdlSelection.SelectedValue == "PM" && isPMSelected == false)
        {
            Response.Write("<script>alert('Please select atleast one PM')</script>");
        }
        else if (rdlSelection.SelectedValue == "DM" && isDMSelected == false)
        {
            Response.Write("<script>alert('Please select atleast one DM')</script>");
        }
        else
        {
            DataTable dtCustomerCode = new DataTable();
            dtCustomerCode.Columns.Add("CustomerCode");
            DataTable dtMCC = new DataTable();
            dtMCC.Columns.Add("MCC");
            DataTable dtPM = new DataTable();
            dtPM.Columns.Add("PM");
            DataTable dtDM = new DataTable();
            dtDM.Columns.Add("DM");
            DataTable dtPracticeLine = new DataTable();
            dtPracticeLine.Columns.Add("PracticeLine");
            DataTable dtProfitCentre = new DataTable();
            dtProfitCentre.Columns.Add("ProfitCentre");

            if (rdlSelection.SelectedValue == "CC")
            {
                for (int i = 0; i < chkCustomerCodeList.Items.Count; i++)
                {
                    if (chkCustomerCodeList.Items[i].Selected == true)
                    {
                        DataRow dr = dtCustomerCode.NewRow();
                        dr["CustomerCode"] = chkCustomerCodeList.Items[i].Value;
                        dtCustomerCode.Rows.Add(dr);
                    }
                }
            }




            else if (rdlSelection.SelectedValue == "MCC")
            {
                for (int i = 0; i < chkMCCList.Items.Count; i++)
                {
                    if (chkMCCList.Items[i].Selected == true)
                    {
                        DataRow dr = dtMCC.NewRow();
                        dr["MCC"] = chkMCCList.Items[i].Value;
                        dtMCC.Rows.Add(dr);
                    }
                }
            }




            else if (rdlSelection.SelectedValue == "PM")
            {
                for (int i = 0; i < chkPMList.Items.Count; i++)
                {
                    if (chkPMList.Items[i].Selected == true)
                    {
                        DataRow dr = dtPM.NewRow();
                        dr["PM"] = chkPMList.Items[i].Value;
                        dtPM.Rows.Add(dr);
                    }
                }
            }




            else if (rdlSelection.SelectedValue == "DM")
            {
                for (int i = 0; i < chkDMList.Items.Count; i++)
                {
                    if (chkDMList.Items[i].Selected == true)
                    {
                        DataRow dr = dtDM.NewRow();
                        dr["DM"] = chkDMList.Items[i].Value;
                        dtDM.Rows.Add(dr);
                    }
                }
            }




            else if (rdlSelection.SelectedValue == "PracticeLine")
            {
                for (int i = 0; i < chkPracticeLineList.Items.Count; i++)
                {
                    if (chkPracticeLineList.Items[i].Selected == true)
                    {
                        DataRow dr = dtPracticeLine.NewRow();
                        dr["PracticeLine"] = chkPracticeLineList.Items[i].Value;
                        dtPracticeLine.Rows.Add(dr);
                    }
                }
            }




            else if (rdlSelection.SelectedValue == "ProfitCentre")
            {
                for (int i = 0; i < chkProfitCentreList.Items.Count; i++)
                {
                    if (chkProfitCentreList.Items[i].Selected == true)
                    {
                        DataRow dr = dtProfitCentre.NewRow();
                        dr["ProfitCentre"] = chkProfitCentreList.Items[i].Value;
                        dtProfitCentre.Rows.Add(dr);
                    }
                }
            }

            if (dtCustomerCode.Rows.Count != 0 || dtMCC.Rows.Count != 0 || dtPM.Rows.Count != 0 || dtDM.Rows.Count != 0 || dtPracticeLine.Rows.Count != 0 || dtProfitCentre.Rows.Count != 0)
            {
                string s = @EmpMailID;
                string[] values = s.Split(',');
                for (int i = 0; i < values.Length; i++)
                {
                    result = 0;
                    values[i] = values[i].Trim();

                    if (values[i] != UserName)
                    {
                        result = daObj.DelegateAccess(values[i], dtCustomerCode, dtMCC, dtPM, dtDM, dtPracticeLine, dtProfitCentre, UserName);

                        if (result == 1)
                        {
                            DelegatedRecords = DelegatedRecords + 1;
                        }
                    }
                }
            }
            if (DelegatedRecords > 0)
            {
                txtEmpmailID.Text = "";
                txtEmpmailID.Focus();

                for (int i = 0; i < chkCustomerCodeList.Items.Count; i++)
                {
                    chkCustomerCodeList.Items[i].Selected = false;
                }

                for (int i = 0; i < chkMCCList.Items.Count; i++)
                {
                    chkMCCList.Items[i].Selected = false;
                }

                for (int i = 0; i < chkPMList.Items.Count; i++)
                {
                    chkPMList.Items[i].Selected = false;
                }

                for (int i = 0; i < chkDMList.Items.Count; i++)
                {
                    chkDMList.Items[i].Selected = false;
                }
                for (int i = 0; i < chkProfitCentreList.Items.Count; i++)
                {
                    chkProfitCentreList.Items[i].Selected = false;
                }

                for (int i = 0; i < chkPracticeLineList.Items.Count; i++)
                {
                    chkPracticeLineList.Items[i].Selected = false;
                }



                lblMessage.Visible = true;
                lblMessage.Text = "Access has been delegated to " + DelegatedRecords + " member(s) successfully !!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            else if (result == 2)
            {
                Response.Write("<script>alert('Already Delegated by Someone else !!')</script>");
            }
            else if (result == 3)
            {
                Response.Write("<script>alert('ADMIN..Access cannot be delegated to a admin')</script>");
            }

            else if (result == 0)
            {
                Response.Write("<script>alert('Some Error Occurred!!')</script>");
            }
        }
    }
    protected void btnCheckAccess_Click(object sender, EventArgs e)
    {
        lblMessage.Visible = false;
        string UserName = System.IO.Path.GetFileName(HttpContext.Current.User.Identity.Name);
        if (txtEmpmailID.Text.ToLower() != "")
        {
           
            DAL daObj = new DAL();

            SqlCommand cmd = new SqlCommand();
            string query = "";
            //DAL daObj = new DAL();

            query = "exec [dbo].[SP_ExpenseRecoery_CheckDelegatedAccess] @empmailid,@username";

            cmd.Parameters.AddWithValue("@empmailid",  txtEmpmailID.Text.ToLower());
            cmd.Parameters.AddWithValue("@username", UserName);



            cmd.CommandText = query;

            DataSet ds = daObj.FetchDataSet(cmd);
         
            lblMessage.Visible = true;
            if (ds.Tables[0].Rows[0][0].ToString() == "NewUser")
            {
                lblMessage.ForeColor = Color.Green;
                lblMessage.Text = "New User..Please select the records to delegate the access";
            }
            else if (ds.Tables[0].Rows[0][0].ToString() == "Access Cannot be delegated to an ADMIN")
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "ADMIN..Access Cannot be delegated to this mailid";
            }
            else if (ds.Tables[0].Rows[0][0].ToString() == "Already Delegated by SomeOne else")
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Already Delegated by SomeOne else..Access Cannot be delegated to this mailid";
            }
            else
            {
                lblMessage.Visible = false;
                string AccessParameter = ds.Tables[0].Rows[0][0].ToString();
                if (AccessParameter.Equals("PM"))
                {
                    plCustomerCode.Visible = false;
                    plMCC.Visible = false;
                    plDM.Visible = false;
                    plPM.Visible = true;
                    plPracticeLine.Visible = false;
                    plProfitCentre.Visible = false;
                    rdlSelection.SelectedValue = "PM";

                    for (int index = 0; index < chkPMList.Items.Count; index++)
                    {
                        chkPMList.Items[index].Selected = false;
                    }
                    for (int index = 0; index < chkPMList.Items.Count; index++)
                    {

                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            if (chkPMList.Items[index].Text.Trim().ToUpper() == ds.Tables[1].Rows[i][0].ToString().Trim().ToUpper())
                            {
                                chkPMList.Items[index].Selected = true;
                                chkPMList.Items[index].Attributes.Add("style", "color: Green; font-weight: bold; bg-color:Yellow");
                            }

                        }
                    }
                }

                else if (AccessParameter.Equals("DM"))
                {
                    plDM.Visible = true;
                    plCustomerCode.Visible = false;
                    plMCC.Visible = false;
                    plPM.Visible = false;
                    plPracticeLine.Visible = false;
                    plProfitCentre.Visible = false;
                    rdlSelection.SelectedValue = "DM";


                    for (int index = 0; index < chkDMList.Items.Count; index++)
                    {
                        chkDMList.Items[index].Selected = false;
                    }
                    for (int index = 0; index < chkDMList.Items.Count; index++)
                    {



                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            if (chkDMList.Items[index].Text.Trim().ToUpper() == ds.Tables[1].Rows[i][0].ToString().Trim().ToUpper())
                            {
                                chkDMList.Items[index].Selected = true;
                                chkDMList.Items[index].Attributes.Add("style", "color: Green; font-weight: bold; bg-color:Yellow");
                            }

                        }
                    }
                }

                else if (AccessParameter.Equals("MCC"))
                {
                    plMCC.Visible = true;
                    plCustomerCode.Visible = false;
                    plDM.Visible = false;
                    plPM.Visible = false;
                    plPracticeLine.Visible = false;
                    plProfitCentre.Visible = false;
                    rdlSelection.SelectedValue = "MCC";

                    for (int index = 0; index < chkMCCList.Items.Count; index++)
                    {
                        chkMCCList.Items[index].Selected = false;
                    }

                    for (int index = 0; index < chkMCCList.Items.Count; index++)
                    {

                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            if (chkMCCList.Items[index].Text.Trim().ToUpper() == ds.Tables[1].Rows[i][0].ToString().Trim().ToUpper())
                            {
                                chkMCCList.Items[index].Selected = true;
                                chkMCCList.Items[index].Attributes.Add("style", "color: Green; font-weight: bold; bg-color:Yellow");
                            }

                        }
                    }
                }

                else if (AccessParameter.Equals("CC"))
                {
                    plCustomerCode.Visible = true;
                    plMCC.Visible = false;
                    plDM.Visible = false;
                    plPM.Visible = false;
                    plPracticeLine.Visible = false;
                    plProfitCentre.Visible = false;
                    rdlSelection.SelectedValue = "CC";

                    for (int index = 0; index < chkCustomerCodeList.Items.Count; index++)
                    {
                        chkCustomerCodeList.Items[index].Selected = false;
                    }
                    for (int index = 0; index < chkCustomerCodeList.Items.Count; index++)
                    {

                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            if (chkCustomerCodeList.Items[index].Text.Trim().ToUpper() == ds.Tables[1].Rows[i][0].ToString().Trim().ToUpper())
                            {
                                chkCustomerCodeList.Items[index].Selected = true;
                                chkCustomerCodeList.Items[index].Attributes.Add("style", "color: Green; font-weight: bold;");
                            }

                        }
                    }
                }

                else if (AccessParameter.Equals("PracticeLine"))
                {
                    plPracticeLine.Visible = true;
                    plCustomerCode.Visible = false;
                    plMCC.Visible = false;
                    plDM.Visible = false;
                    plPM.Visible = false;
                    plProfitCentre.Visible = false;

                    rdlSelection.SelectedValue = "PracticeLine";

                    for (int index = 0; index < chkPracticeLineList.Items.Count; index++)
                    {
                        chkPracticeLineList.Items[index].Selected = false;
                    }
                    for (int index = 0; index < chkPracticeLineList.Items.Count; index++)
                    {

                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {


                            if (chkPracticeLineList.Items[index].Text.Trim().ToUpper() == ds.Tables[1].Rows[i][0].ToString().Trim().ToUpper())
                            {
                                chkPracticeLineList.Items[index].Selected = true;
                                chkPracticeLineList.Items[index].Attributes.Add("style", "color: Green; font-weight: bold; bg-color:Yellow");
                            }

                        }
                    }
                }
                else if (AccessParameter.Equals("ProfitCentre"))
                {
                    plProfitCentre.Visible = true;
                    plCustomerCode.Visible = false;
                    plMCC.Visible = false;
                    plDM.Visible = false;
                    plPM.Visible = false;
                    plPracticeLine.Visible = false;
                    rdlSelection.SelectedValue = "ProfitCentre";
                    for (int index = 0; index < chkProfitCentreList.Items.Count; index++)
                    {
                        chkProfitCentreList.Items[index].Selected = false;
                    }
                    for (int index = 0; index < chkProfitCentreList.Items.Count; index++)
                    {

                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            //                  string value = ds.Tables[1].Rows[i][0].ToString();
                            if (chkProfitCentreList.Items[index].Text.Trim().ToUpper() == ds.Tables[1].Rows[i][0].ToString().Trim().ToUpper())
                            {
                                chkProfitCentreList.Items[index].Selected = true;
                                chkProfitCentreList.Items[index].Attributes.Add("style", "color: Green; font-weight: bold; bg-color:Yellow");
                            }

                        }
                    }
                }

            }
        }
        else
        {
            lblMessage.Visible = true;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = "Please input some mailid.";
        }
    }
        
    protected void btnCopyDelegatedAccess_Click(object sender, EventArgs e)
    {
        rdlSelection.Visible = true;
        string UserName = System.IO.Path.GetFileName(HttpContext.Current.User.Identity.Name);
        string cmdAlredyDelegatedTo = "select distinct LTRIM(RTRIM(EmpMailID )) as EmpMailID from ExpenseRecovery_NewAccess where AccessType='CUSTOMER' and DelegatedBY='" + UserName+"'";
        DAL dal = new DAL();
        DataSet ds = dal.GetDataSet(cmdAlredyDelegatedTo);

        ds.Tables[0].DefaultView.Sort = "EmpMailID";
        DataTable dtPM = ds.Tables[0].DefaultView.ToTable(true, "EmpMailID");
        rbAlreadyDelegated.DataSource = dtPM;
        rbAlreadyDelegated.DataTextField = "EmpMailID";
        rbAlreadyDelegated.DataBind();
        rbAlreadyDelegated.Visible = true;
        plAlreadyDelegated.Visible = true;
        plProfitCentre.Visible = false;
        plCustomerCode.Visible = false;
        plMCC.Visible = false;
        plDM.Visible = false;
        plPM.Visible = false;
        plPracticeLine.Visible = false;

    }

    protected void rbAlreadyDelegated_SelectedIndexChanged(object sender, EventArgs e)
    {
        string UserName = System.IO.Path.GetFileName(HttpContext.Current.User.Identity.Name);
        string cmdCheckAccess = "exec SP_ExpenseRecoery_CheckDelegatedAccess '" + rbAlreadyDelegated.SelectedValue + "','" + UserName + "'"; ;
        DAL daObj = new DAL();
        DataSet ds = daObj.GetDataSet(cmdCheckAccess);

        string AccessParameter = ds.Tables[0].Rows[0][0].ToString();
            if (AccessParameter.Equals("PM"))
            {
                plCustomerCode.Visible = false;
                plMCC.Visible = false;
                plDM.Visible = false;
                plPM.Visible = true;
                plPracticeLine.Visible = false;
                plProfitCentre.Visible = false;
                rdlSelection.SelectedValue = "PM";

                for (int index = 0; index < chkPMList.Items.Count; index++)
                {
                    chkPMList.Items[index].Selected = false;
                }
                for (int index = 0; index < chkPMList.Items.Count; index++)
                {
            
                    for (int i=0;i<ds.Tables[1].Rows.Count;i++)
                        {
                            if (chkPMList.Items[index].Text.Trim().ToUpper() == ds.Tables[1].Rows[i][0].ToString().Trim().ToUpper())
                            {
                                chkPMList.Items[index].Selected = true;
                                chkPMList.Items[index].Attributes.Add("style", "color: Green; font-weight: bold; bg-color:Yellow");
                            }
                 
                        }
                }
            }

            else if (AccessParameter.Equals("DM"))
            {
                plDM.Visible = true;
                plCustomerCode.Visible = false;
                plMCC.Visible = false;
                plPM.Visible = false;
                plPracticeLine.Visible = false;
                plProfitCentre.Visible = false;
                rdlSelection.SelectedValue = "DM";


                for (int index = 0; index < chkDMList.Items.Count; index++)
                {
                    chkDMList.Items[index].Selected = false;
                }
                for (int index = 0; index < chkDMList.Items.Count; index++)
                {
                  

               
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        if (chkDMList.Items[index].Text.Trim().ToUpper() == ds.Tables[1].Rows[i][0].ToString().Trim().ToUpper())
                        {
                            chkDMList.Items[index].Selected = true;
                            chkDMList.Items[index].Attributes.Add("style", "color: Green; font-weight: bold; bg-color:Yellow");
                        }
                   
                    }   
                }
            }

            else if (AccessParameter.Equals("MCC"))
            {
                plMCC.Visible = true;
                plCustomerCode.Visible = false;
                plDM.Visible = false;
                plPM.Visible = false;
                plPracticeLine.Visible = false;
                plProfitCentre.Visible = false;
                rdlSelection.SelectedValue = "MCC";

                for (int index = 0; index < chkMCCList.Items.Count; index++)
                {
                    chkMCCList.Items[index].Selected = false;
                }
                
                for (int index = 0; index < chkMCCList.Items.Count; index++)
                {

                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        if (chkMCCList.Items[index].Text.Trim().ToUpper() == ds.Tables[1].Rows[i][0].ToString().Trim().ToUpper())
                        {
                            chkMCCList.Items[index].Selected = true;
                            chkMCCList.Items[index].Attributes.Add("style", "color: Green; font-weight: bold; bg-color:Yellow");
                        }
                    
                    }
                }
            }

            else if (AccessParameter.Equals("CC"))
            {
                plCustomerCode.Visible = true;
                plMCC.Visible = false;
                plDM.Visible = false;
                plPM.Visible = false;
                plPracticeLine.Visible = false;
                plProfitCentre.Visible = false;
                rdlSelection.SelectedValue = "CC";

                for (int index = 0; index < chkCustomerCodeList.Items.Count; index++)
                {
                    chkCustomerCodeList.Items[index].Selected = false;
                }
                for (int index = 0; index < chkCustomerCodeList.Items.Count; index++)
                {

                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        if (chkCustomerCodeList.Items[index].Text.Trim().ToUpper() == ds.Tables[1].Rows[i][0].ToString().Trim().ToUpper())
                        {
                            chkCustomerCodeList.Items[index].Selected = true;
                            chkCustomerCodeList.Items[index].Attributes.Add("style", "color: Green; font-weight: bold;");
                        }
                       
                    }
                }
            }

            else if (AccessParameter.Equals("PracticeLine"))
            {
                plPracticeLine.Visible = true;
                plCustomerCode.Visible = false;
                plMCC.Visible = false;
                plDM.Visible = false;
                plPM.Visible = false;
                plProfitCentre.Visible = false;
             
                rdlSelection.SelectedValue = "PracticeLine";

                for (int index = 0; index < chkPracticeLineList.Items.Count; index++)
                {
                    chkPracticeLineList.Items[index].Selected = false;
                }
                for (int index = 0; index < chkPracticeLineList.Items.Count; index++)
                {
                  
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                    

                        if (chkPracticeLineList.Items[index].Text.Trim().ToUpper() == ds.Tables[1].Rows[i][0].ToString().Trim().ToUpper())
                        {
                            chkPracticeLineList.Items[index].Selected = true;
                            chkPracticeLineList.Items[index].Attributes.Add("style", "color: Green; font-weight: bold; bg-color:Yellow");
                        }
                   
                }
                }
            }
            else if (AccessParameter.Equals("ProfitCentre"))
            {
                plProfitCentre.Visible = true;
                plCustomerCode.Visible = false;
                plMCC.Visible = false;
                plDM.Visible = false;
                plPM.Visible = false;
                plPracticeLine.Visible = false;
                rdlSelection.SelectedValue = "ProfitCentre";
                for (int index = 0; index < chkProfitCentreList.Items.Count; index++)
                {
                    chkProfitCentreList.Items[index].Selected = false;
                }
                for (int index = 0; index < chkProfitCentreList.Items.Count; index++)
                {

                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        //                  string value = ds.Tables[1].Rows[i][0].ToString();
                        if (chkProfitCentreList.Items[index].Text.Trim().ToUpper() == ds.Tables[1].Rows[i][0].ToString().Trim().ToUpper())
                        {
                            chkProfitCentreList.Items[index].Selected = true;
                            chkProfitCentreList.Items[index].Attributes.Add("style", "color: Green; font-weight: bold; bg-color:Yellow");
                        }
                       
                    }
                }
            }
      

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        rdlSelection.Visible = true;
        rdlSelection.SelectedValue = "MCC";

        plAlreadyDelegated.Visible = false;
        plProfitCentre.Visible = false;
        plCustomerCode.Visible = false;
        plMCC.Visible = true;
        plDM.Visible = false;
        plPM.Visible = false;
        plPracticeLine.Visible = false;
        lblMailIDMessage.Visible = false;
        lblMessage.Visible = false;
    }
}
