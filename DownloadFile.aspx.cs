using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using OfficeOpenXml;
using System.Drawing;

public partial class DownloadFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        var key = Request.QueryString["Key"].ToString();
        string MailID = System.IO.Path.GetFileName(HttpContext.Current.User.Identity.Name);
        if (Request.QueryString["Key"] != null)
        {
            string folder="";
            string sPath = "";
            if (key.Contains("ExpenseRecoveryPivot"))
            {
                folder = "ExcelOperations";
                sPath = Server.MapPath("ExcelOperations\\");
            }
            else
            {
                folder = "App_Data";
                sPath = Server.MapPath("App_Data\\");
            }
                var MyDir = new DirectoryInfo(Server.MapPath(folder));
                string finalname;

                if (key == "ExpenseRecovery_Upload")
                {
                    finalname = "ExpenseRecovery_Upload_" + MailID + ".xlsx";
                    if (MyDir.GetFiles().SingleOrDefault(k => k.Name.ToUpper() == finalname.ToUpper()) != null)
                    {
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + finalname);
                        Response.TransmitFile(sPath + finalname);
                        Response.End();
                    }
                }
                else if (key == "ExpenseRecoveryPivot")
                {
                //finalname = "Expense Recovery Pivot_" + DateTime.Now.ToString("ddMMMyyyy_HHmmss") + "IST.xlsx";
                finalname = Request.QueryString["FileName"];
                if (MyDir.GetFiles().SingleOrDefault(k => k.Name.ToUpper() == finalname.ToUpper()) != null)
                    {
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + finalname);
                        Response.TransmitFile(sPath + finalname);
                        Response.End();
                    }
                }
                else
                {
                    //finalname = "ExpenseRecovery.xlsx";
                    finalname = key;
                    string destPath = Path.Combine(Server.MapPath("~/DownloadedFiles/"), finalname);
                    MyDir = new DirectoryInfo(Server.MapPath("~/DownloadedFiles/"));
                    if (MyDir.GetFiles().SingleOrDefault(k => k.Name.ToUpper() == finalname.ToUpper()) != null)
                    {
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + finalname);
                        Response.TransmitFile(destPath);
                        Response.End();
                    }

                }



                
            
        }
       
    }

   




}