using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using System.IO;
using System.Configuration;
using System.Drawing.Printing;
//using System.Printing;
using System.Net;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Net.Mime;
using iTextSharp.tool.xml;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
public partial class Sugar_Report_rptExportInviceNew : System.Web.UI.Page
{
    string billno = string.Empty;
    string mail = string.Empty;
    int company_code;
    int year_code;
    string Doc_Date = string.Empty;
    ReportDocument rprt1 = new ReportDocument();

    string company_name = string.Empty;
    string AL1 = string.Empty;
    string AL2 = string.Empty;
    string AL3 = string.Empty;
    string AL4 = string.Empty;
    string other = string.Empty;
    string bankdetail = string.Empty;
    string Invno = string.Empty;
    string SB_NO = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            billno = Request.QueryString["billno"];
            SB_NO = Request.QueryString["SB_NO"];
            company_code = Convert.ToInt32(Session["Company_Code"].ToString());
            year_code = Convert.ToInt32(Session["year"].ToString());
            company_name = Session["Company_Name"].ToString();
            DataTable dt = GetData(int.Parse(billno), company_code);

            SqlDataAdapter da = new SqlDataAdapter();
            double total = 0.00;
            double Sum = 0.00;
            string itemvalue = "";
            itemvalue = dt.Rows[0]["Amt_Foregin"].ToString();
            string inWords = clsNoToWordEuro.ctgword(itemvalue.ToString());
            dt.Rows[0]["Inword"] = inWords;
            string year = clsCommon.getString("select year from accountingyear where Company_Code=" + Session["Company_Code"].ToString() + " and  yearCode=" + Session["year"].ToString() + "");
            year = year.Replace("20", "");

            string printyear = Session["printinsalebill"].ToString();
            if (printyear == "Y")
            {
                Invno = Session["selectedyear"].ToString() + "-" + SB_NO;
            }
            else
            {
                Invno = "" + year + "/" + SB_NO;
            }

            rprt1.Load(Server.MapPath("cryExportInvoiceNew.rpt"));
            rprt1.SetDataSource(dt);
            cryExportInvoiceNew.ReportSource = rprt1;

            rprt1.DataDefinition.FormulaFields["Invno"].Text = "\""+ "SB" + Invno + "\"";
            rprt1.DataDefinition.FormulaFields["companyname"].Text = "\"" + company_name + "\"";
            rprt1.DataDefinition.FormulaFields["AL1"].Text = "\"" + AL1 + "\"";
            rprt1.DataDefinition.FormulaFields["AL2"].Text = "\"" + AL2 + "\"";
            rprt1.DataDefinition.FormulaFields["AL3"].Text = "\"" + AL3 + "\"";
            rprt1.DataDefinition.FormulaFields["Al4"].Text = "\"" + AL4 + "\"";
            rprt1.DataDefinition.FormulaFields["bankdetail"].Text = "\"" + bankdetail + "\"";
            string imagepath = clsCommon.getString("select ImagePath from tblsign where ImageOrLogo='L' and Company_Code='"
                + Session["Company_Code"].ToString() + "'");
            String path = Server.MapPath("") + "\\" + imagepath;

            imagepath = path.Replace("Report", "Images");
           // rprt1.DataDefinition.FormulaFields["img"].Text = "\"" + imagepath + "\"";

            cryExportInvoiceNew.ReportSource = rprt1;
            cryExportInvoiceNew.RefreshReport();
        }
        catch (Exception)
        {

            throw;
        }


    }
    private DataTable GetData(int bill_no, int company_code)
    {
        DataTable dt = new DataTable();
        string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strcon))
        {
            SqlCommand cmd = new SqlCommand("select * from qryExportInvoiceForReport where Inv_No=" + billno + "and year_code=" + year_code + " and Company_Code=" + company_code, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            
        }

        string qry = "select * from tblvoucherheadaddress where Company_Code='" + Session["Company_Code"].ToString() + "'";
        DataSet ds = clsDAL.SimpleQuery(qry);
        if (ds != null)
        {
            DataTable dt1 = ds.Tables[0];
            if (dt1.Rows.Count > 0)
            {
                AL1 = dt1.Rows[0]["AL1"].ToString();
                AL2 = dt1.Rows[0]["AL2"].ToString();
                AL3 = dt1.Rows[0]["AL3"].ToString();
                AL4 = dt1.Rows[0]["AL4"].ToString();
                other = dt1.Rows[0]["Other"].ToString();
                bankdetail = dt1.Rows[0]["bankdetail"].ToString();
            }
        }
        return dt;
    }


    protected void btnPDF_Click(object sender, EventArgs e)
    {
        try
        {
            // string filepath=@"D:\pdffiles\cryChequePrinting.pdf";
            string filepath = "D:\\PDFFiles";

            if (!System.IO.Directory.Exists(filepath))
            {
                System.IO.Directory.CreateDirectory("D:\\PDFFiles");
            }
            string filename = filepath + "\\ExportInvoice" + company_code + "_" + year_code + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";
            rprt1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filename);

            //open PDF File

            //System.Diagnostics.Process.Start(filename);
            WebClient User = new WebClient();

            Byte[] FileBuffer = User.DownloadData(filename);

            if (FileBuffer != null)
            {

                Response.ContentType = "application/pdf";

                Response.AddHeader("content-length", FileBuffer.Length.ToString());

                Response.BinaryWrite(FileBuffer);

            }
        }
        catch (Exception e1)
        {
            Response.Write("PDF err:" + e1);
            return;
        }
        //   Response.Write("<script>alert('PDF successfully Generated');</script>");

    }
    protected void btnMail_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtEmail.Text != string.Empty)
            {
                string filepath = "D:\\PDFFiles";

                if (!System.IO.Directory.Exists(filepath))
                {
                    System.IO.Directory.CreateDirectory("D:\\PDFFiles");
                }
                string filename = filepath + "\\ExportInvoice" + company_code + "_" + year_code + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";
                rprt1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filename);
                mail = txtEmail.Text;

                ContentType contentType = new ContentType();
                contentType.MediaType = MediaTypeNames.Application.Pdf;
                contentType.Name = "ExportInvoice";
                // Attachment attachment = new Attachment(Server.MapPath(filename), contentType);
                Attachment attachment = new Attachment(filename);
                string mailFrom = Session["EmailId"].ToString();
                string smtpPort = "587";
                string emailPassword = Session["EmailPassword"].ToString();
                EncryptPass enc = new EncryptPass();
                emailPassword = enc.Decrypt(emailPassword);
                MailMessage msg = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.Host = clsGV.Email_Address;
                msg.From = new MailAddress(mailFrom);
                msg.To.Add(mail);
                //msg.Body = "ExportInvoice";
                msg.Body = "Dear Sir / Madam <br /> With reference to above Subject please refer the attached Export Invoice for your reference.<br /> <br/><b>Thanks & Regards</b> <br/> " +
          "Accounts Dept.<br/><b> Bhavani Iron Industries Pvt Ltd. Unit -2 <br/>C 5/3, Fiver Star MIDC, Kagal, Tal. Hatkanangale, Dist. Kolhapur-416 216. <br/>Ph.0231-2305295, 2658556</b>";

                msg.Attachments.Add(attachment);
                msg.IsBodyHtml = true;
                //msg.Subject = "No:" + billno;
                msg.Subject = "Export Invoice No. " + billno + " Dated :" + Doc_Date + "From Bhavani Iron Industries Pvt. Ltd.";
                //msg.IsBodyHtml = true;
                if (smtpPort != string.Empty)
                {
                    SmtpServer.Port = Convert.ToInt32(smtpPort);
                }
                SmtpServer.EnableSsl = true;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(mailFrom, emailPassword);
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object k,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                SmtpServer.Send(msg);
                attachment.Dispose();
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                Response.Write("<script>alert('Mail Send successfully');</script>");
            }
            else
            {
                Response.Write("<script>alert('Please enter Mail Id');</script>");
                return;
            }

        }
        catch (Exception e1)
        {
            Response.Write("Mail err:" + e1);
            return;
        }


    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        this.cryExportInvoiceNew.ReportSource = null;

        cryExportInvoiceNew.Dispose();

       

        if (rprt1 != null)
        {

            rprt1.Close();

            rprt1.Dispose();

            rprt1 = null;

        }
       
        GC.Collect();

        GC.WaitForPendingFinalizers();
    }
}