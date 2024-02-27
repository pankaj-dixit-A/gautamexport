using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
public partial class PgeExport_Invoice_New : System.Web.UI.Page
{
    #region data section
    string qry = string.Empty;
    string tblPrefix = string.Empty;
    string tblHead = string.Empty;
    string tblDetails = string.Empty;
    string qryCommon = string.Empty;
    string searchString = string.Empty;
    string strTextBox = string.Empty;
    string qryDisplay = string.Empty;
    string user = string.Empty;
    string isAuthenticate = string.Empty;
    static WebControl objAsp = null;
    int Item_Code = 3;
    int Qty = 5;
    int Rate = 6;
    int Value = 7;
    int Rowaction = 8;
    int Srno = 9;
    string SystemMasterTable = string.Empty;
    #endregion
    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "Export_Head";
            tblDetails = "Export_Line";
            qryCommon = "qryInvoice_Export";
            SystemMasterTable = tblPrefix + "SystemMaster";
            user = Session["user"].ToString();
            pnlPopup.Style["display"] = "none";
            string Doc = Request.QueryString["Doc_No"];
            string Action = Request.QueryString["Action"];
            if (!Page.IsPostBack)
            {
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    //pnlPopup.Style["display"] = "none";
                    //ViewState["currentTable"] = null;
                    //ViewState["currentTableNew"] = null;
                    //clsButtonNavigation.enableDisable("N");
                    //this.makeEmptyForm("N");
                    //ViewState["mode"] = "I";
                    //if (Session["Doc_No"] != null)
                    //{
                    //    hdnf.Value = Session["Doc_No"].ToString();
                    //    //trntype = Session["RP_TYPE"].ToString();

                    //    //drpTrnType.SelectedValue = Session["RP_TYPE"].ToString();
                    //    qry = getDisplayQuery();
                    //    this.fetchRecord(qry);
                    //    this.enableDisableNavigateButtons();

                    //    if (Session["Allow"] == "Allow")
                    //    {
                    //        btnEdit.Enabled = false;
                    //        btnAdd.Enabled = false;
                           
                    //    }

                    //    else
                    //    {
                            
                    //    }

                    //    Session["SB_NO"] = null;

                    //    Session["Allow"] = null;
                    //}
                    //else
                    //{
                    //    btnAdd_Click(this, new EventArgs());
                    //    //this.showLastRecord();
                    //}
                    if (Action == "1")
                    {
                        hdnf.Value = Doc;

                        pnlPopup.Style["display"] = "none";
                        ViewState["currentTable"] = null;
                        ViewState["currentTableNew"] = null;
                        clsButtonNavigation.enableDisable("N");
                        this.makeEmptyForm("N");
                        ViewState["mode"] = "I";
                        //this.showLastRecord();
                        qry = this.getDisplayQuery();
                        this.fetchRecord(qry);
                        this.enableDisableNavigateButtons();
                    }
                    else
                    {
                        btnAdd_Click(this, new EventArgs());
                    }
                    
                }
                else
                {
                    Response.Redirect("~/UnAuthorized/Unauthorized_User.aspx", false);
                }
                if (objAsp != null)
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(objAsp);
                if (hdnfClosePopup.Value == "Close" || hdnfClosePopup.Value == "")
                {
                    pnlPopup.Style["display"] = "none";
                }
                else
                {
                    pnlPopup.Style["display"] = "block";
                    objAsp = btnSearch;
                }
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [getMaxCode]
    private void getMaxCode()
    {
        try
        {
            DataSet ds = null;
            using (clsGetMaxCode obj = new clsGetMaxCode())
            {
                obj.tableName = tblHead + " where ";
                obj.code = "Doc_no";
                ds = new DataSet();
                ds = obj.getMaxCode();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ViewState["mode"] != null)
                            {
                                if (ViewState["mode"].ToString() == "I")
                                {
                                    txtDoc_No.Text = ds.Tables[0].Rows[0][0].ToString();
                                }
                            }
                        }
                    }
                }
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [makeEmptyForm]
    private void makeEmptyForm(string dAction)
    {
        try
        {
            if (dAction == "N")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Text = "";
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = false;
                    }
                    if (c is System.Web.UI.WebControls.Label)
                    {
                        ((System.Web.UI.WebControls.Label)c).Text = "";
                    }
                }
                pnlPopup.Style["display"] = "none";
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                btnOpenDetailsPopup.Enabled = false;
                grdDetail.DataSource = null; grdDetail.DataBind();
                ViewState["currentTable"] = null;
                ViewState["currentTableNew"] = null;
                btnSave.Text = "Save";
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                txtEdit_Doc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtAc_Code.Enabled = false;
                btntxtAc_Code.Enabled = false;
                txtInv1.Enabled = false;
                txtInv2.Enabled = false;
                txtInv3.Enabled = false;
                txtInv4.Enabled = false;
                txtInv5.Enabled = false;
                txtInv6.Enabled = false;
                txtInv7.Enabled = false;
                txtInv8.Enabled = false;
                txtInv9.Enabled = false;
                txtInv10.Enabled = false;
                txtQty.Enabled = false;
                txtRate.Enabled = false;
                txtValue.Enabled = false;
                btntxtPart_no.Enabled = false;
                btnGo.Enabled = false;
                btnBack.Enabled = true;

            }
            if (dAction == "A")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Text = "";
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;
                    }
                }
                btnSave.Text = "Save";
                btntxtDoc_No.Text = "Change No";
                btntxtDoc_No.Enabled = true;
                txtEdit_Doc_No.Enabled = false;
                txtDoc_No.Enabled = false;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                pnlgrdDetail.Enabled = true;
                ViewState["currentTableNew"] = null;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null; grdDetail.DataBind();
                grdGenerate.DataSource = null; grdGenerate.DataBind();
                ViewState["currentTable"] = null;
                txtQty.Enabled = true;
                txtRate.Enabled = true;
                txtValue.Enabled = true;
                txtDoc_Date.Enabled = true;
                txtDoc_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                txtAc_Code.Enabled = true;
                lblAc_Code_Name.Text = string.Empty;
                btntxtAc_Code.Enabled = true;
                txtInv1.Enabled = true;
                txtInv2.Enabled = true;
                txtInv3.Enabled = true;
                txtInv4.Enabled = true;
                txtInv5.Enabled = true;
                txtInv6.Enabled = true;
                txtInv7.Enabled = true;
                txtInv8.Enabled = true;
                txtInv9.Enabled = true;
                txtInv10.Enabled = true;
                lblExport_Amount.Text = string.Empty;
                btntxtPart_no.Enabled = true;
                btnGo.Enabled = true;
                btnBack.Enabled = false;

                lblExport_Qty.Text = string.Empty;
                lblDifference.Text = string.Empty;
                lblUnique_ID.Text = string.Empty;
                lblInvoice_Qty.Text = string.Empty;
                lblInvoice_Value.Text = string.Empty;
                #region set Business logic for save
                #endregion
            }
            if (dAction == "S")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = false;
                    }
                }
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                txtEdit_Doc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtAc_Code.Enabled = false;
                btntxtAc_Code.Enabled = false;
                txtInv1.Enabled = false;
                txtInv2.Enabled = false;
                txtInv3.Enabled = false;
                txtInv4.Enabled = false;
                txtInv5.Enabled = false;
                txtInv6.Enabled = false;
                txtInv7.Enabled = false;
                txtInv8.Enabled = false;
                txtInv9.Enabled = false;
                txtInv10.Enabled = false;
                txtQty.Enabled = false;
                txtRate.Enabled = false;
                txtValue.Enabled = false;
                txtQty.Text = string.Empty;
                txtRate.Text = string.Empty;
                txtValue.Text = string.Empty;
                btnAdddetails.Text = "ADD";
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
                btntxtPart_no.Enabled = false;
                btnGo.Enabled = false;
                btnBack.Enabled = true;
            }
            if (dAction == "E")
            {
                foreach (System.Web.UI.Control c in pnlMain.Controls)
                {
                    if (c is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)c).Enabled = true;
                    }
                }
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = true;
                txtEdit_Doc_No.Enabled = false;
                lblMsg.Text = string.Empty;
                txtDoc_Date.Enabled = true;
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                txtAc_Code.Enabled = true;
                btntxtAc_Code.Enabled = true;
                txtInv1.Enabled = true;
                txtInv2.Enabled = true;
                txtInv3.Enabled = true;
                txtInv4.Enabled = true;
                txtInv5.Enabled = true;
                txtInv6.Enabled = true;
                txtInv7.Enabled = true;
                txtInv8.Enabled = true;
                txtInv9.Enabled = true;
                txtInv10.Enabled = true;
                txtQty.Enabled = true;
                txtRate.Enabled = true;
                txtValue.Enabled = true;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                btntxtPart_no.Enabled = true;
                btnGo.Enabled = true;
                btnBack.Enabled = false;
            }
            #region Always check this
            #endregion
        }
        catch
        {
        }
    }
    #endregion
    #region [showLastRecord]
    private void showLastRecord()
    {
        try
        {
            string qry = string.Empty;
            qry = "select max(Doc_No) as Doc_No from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        hdnf.Value = dt.Rows[0]["Doc_No"].ToString();
                        qry = getDisplayQuery();
                        bool recordExist = this.fetchRecord(qry);
                        if (recordExist == true)
                        {
                            btnEdit.Focus();
                        }
                        else                     //new code
                        {
                            btnEdit.Enabled = false;
                            btnDelete.Enabled = false;
                        }
                    }
                }
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [enableDisableNavigateButtons]
    private void enableDisableNavigateButtons()
    {
        #region enable disable previous next buttons
        //int RecordCount = 0;
        //string query = "";
        //query = "select count(*) from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "' and Year_Code=" + Convert.ToInt32(Session["year"]).ToString();
        //string cnt = clsCommon.getString(query);
        //if (cnt != string.Empty)
        //{
        //    RecordCount = Convert.ToInt32(cnt);
        //}
        //if (RecordCount != 0 && RecordCount == 1)
        //{
        //    btnFirst.Enabled = true;
        //    btnPrevious.Enabled = false;
        //    btnNext.Enabled = false;
        //    btnLast.Enabled = false;
        //}
        //else if (RecordCount != 0 && RecordCount > 1)
        //{
        //    btnFirst.Enabled = true;
        //    btnPrevious.Enabled = false;
        //    btnNext.Enabled = false;
        //    btnLast.Enabled = true;
        //}
        //if (txtDoc_No.Text != string.Empty)
        //{
        //    #region check for next or previous record exist or not
        //    query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No>" + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Convert.ToInt32(Session["year"]).ToString() + " ORDER BY Doc_No asc  ";
        //    string strDoc_No = clsCommon.getString(query);
        //    if (strDoc_No != string.Empty)
        //    {
        //        btnNext.Enabled = true;
        //        btnLast.Enabled = true;
        //    }
        //    else
        //    {
        //        btnNext.Enabled = false;
        //        btnLast.Enabled = false;
        //    }
        //    query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No<" + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Convert.ToInt32(Session["year"]).ToString() + " ORDER BY Doc_No desc  ";
        //    if (strDoc_No != string.Empty)
        //    {
        //        btnPrevious.Enabled = true;
        //        btnFirst.Enabled = true;
        //    }
        //    else
        //    {
        //        btnPrevious.Enabled = false;
        //        btnFirst.Enabled = false;
        //    }
        //}
        //    #endregion
        #endregion
        #region enable disable previous next buttons
        int RecordCount = 0;
        string query = "";
        // query = "select count(*) from " + tblHead + " where   Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) " and Sub_Type='" + drpSub_Type.SelectedValue + "' and Year_Code='" + Convert.ToInt32(Session["year"].ToString());

        query = "select count(*) from " + tblHead + " where  Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "' ";
        string cnt = clsCommon.getString(query);
        if (cnt != string.Empty)
        {
            RecordCount = Convert.ToInt32(cnt);
        }

        if (RecordCount != 0 && RecordCount == 1)
        {
            btnFirst.Enabled = true;
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
        }
        else if (RecordCount != 0 && RecordCount > 1)
        {
            btnFirst.Enabled = true;
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = true;
        }
        if (txtDoc_No.Text != string.Empty)
        {
            #region check for next or previous record exist or not

            query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No>" + Convert.ToInt32(hdnf.Value) +
                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                "and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY Doc_No asc  ";
            string strDoc_No = clsCommon.getString(query);
            if (strDoc_No != string.Empty)
            {
                //next record exist
                btnNext.Enabled = true;
                btnLast.Enabled = true;
            }
            else
            {
                //next record does not exist
                btnNext.Enabled = false;
                btnLast.Enabled = false;
            }


            query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No<" + int.Parse(hdnf.Value) +
                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                 "and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "  ORDER BY Doc_No desc  ";
            strDoc_No = clsCommon.getString(query);
            if (strDoc_No != string.Empty)
            {
                //previous record exist
                btnPrevious.Enabled = true;
                btnFirst.Enabled = true;
            }
            else
            {
                btnPrevious.Enabled = false;
                btnFirst.Enabled = false;
            }
            #endregion
        }
        #endregion
    }
    #endregion

    #region [First]
    protected void btnFirst_Click(object sender, EventArgs e)
    {

        try
        {
            string query = "";
            query = "select Doc_No from " + tblHead + " where Doc_No=(select MIN(Doc_No) from " + tblHead + " where Company_Code='"
                + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ")";
            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
        }
    }
    #endregion
    #region [Previous]
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDoc_No.Text != string.Empty)
            {
                string query = "";
                query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No< " + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY Doc_No desc  ";
                hdnf.Value = clsCommon.getString(query);
                navigateRecord();
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [Next]
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDoc_No.Text != string.Empty)
            {
                string query = "";
                query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No> " + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + " ORDER BY Doc_No asc  ";
                hdnf.Value = clsCommon.getString(query);
                navigateRecord();
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [Last]
    protected void btnLast_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "";
            query = "select Doc_No from " + tblHead + " where Doc_No=(select MAX(Doc_No) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + ")";
            hdnf.Value = clsCommon.getString(query);
            navigateRecord();
        }
        catch
        {
        }
    }
    #endregion

    #region [btnAddNew Click]
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string check = txtEdit_Doc_No.Text;
        if (check == string.Empty)
        {
            clsButtonNavigation.enableDisable("A");
            ViewState["mode"] = null;
            ViewState["mode"] = "I";
            this.makeEmptyForm("A");

            string qry = "select ISNULL(max(Doc_No),0) as Doc_No from " + tblHead +
        " where  Year_Code=" + Convert.ToString(Session["year"]).ToString()
                   + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"]).ToString();

            setFocusControl(txtDoc_No);
            pnlPopupDetails.Style["display"] = "none";
            Int32 Doc_No = Convert.ToInt32(clsCommon.getString(qry));
            if (Doc_No != 0)
            {
                int doc_no = Doc_No + 1;
                Doc_No = doc_no;
            }
            else
            {
                Doc_No = 1;
            }
            txtDoc_No.Text = Convert.ToString(Doc_No);
            setFocusControl(txtDoc_Date);
        }
        else
        {
            btnCancel_Click(this, new EventArgs());
        }
    }
    #endregion

    #region [btnEdit_Click]
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = null;
        ViewState["mode"] = "U";
        clsButtonNavigation.enableDisable("E");
        pnlgrdDetail.Enabled = true;
        this.makeEmptyForm("E");
        txtDoc_No.Enabled = false;
        setFocusControl(txtDoc_Date);
    }
    #endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                string str = string.Empty;
                DataSet xml_ds = new DataSet();
                if (str == string.Empty)
                {
                    string currentDoc_No = txtDoc_No.Text;
                    DataSet ds = new DataSet();
                    string strrev = "";
                    using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                    {
                        XElement root = new XElement("ROOT");
                        XElement child1 = new XElement("Head");
                        int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));
                        int Year_Code = Convert.ToInt32(Convert.ToInt32(Session["year"].ToString()));
                        string strRev = string.Empty;
                        child1.SetAttributeValue("Doc_No", txtDoc_No.Text);
                        child1.SetAttributeValue("Company_Code", Company_Code);
                        child1.SetAttributeValue("Year_Code", Year_Code);
                        child1.SetAttributeValue("Tran_Type", "");
                        root.Add(child1);
                        string XMLReport = root.ToString();
                        XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
                        XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
                        string spname = "SP_Export_Head";
                        string xmlfile = XMLReport;
                        string op = "";
                        string returnmaxno = "";
                        int flag = 10;
                        xml_ds = clsDAL.xmlExecuteDMLQrySP(spname, xmlfile, ref op, flag, ref returnmaxno);
                        strrev = op;





                    }
                    string query = "";
                    if (strrev == "-3")
                    {
                        DataTable dt_dalete = xml_ds.Tables[0];
                        string HDNF_VALUE = "";
                        if (dt_dalete.Rows.Count > 0)
                        {
                            HDNF_VALUE = dt_dalete.Rows[0]["Doc_No"].ToString();
                        }
                        hdnf.Value = HDNF_VALUE;

                        if (hdnf.Value == string.Empty)
                        {
                            DataTable dt_dalete_two = xml_ds.Tables[1];
                            if (dt_dalete_two.Rows.Count > 0)
                            {
                                hdnf.Value = dt_dalete_two.Rows[0]["Doc_No"].ToString();
                            }
                        }
                        if (hdnf.Value != string.Empty)
                        {
                            query = getDisplayQuery();
                            bool recordExist = this.fetchRecord(query);
                            this.makeEmptyForm("S");
                            clsButtonNavigation.enableDisable("S");
                        }
                        else
                        {
                            this.makeEmptyForm("N");
                            clsButtonNavigation.enableDisable("N");
                            btnEdit.Enabled = false;
                            btnDelete.Enabled = false;
                        }
                    }
                    this.enableDisableNavigateButtons();
                }
                else
                {
                    lblMsg.Text = "Cannot delete this Group , it is in use";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [btnCancel_Click]
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (hdnf.Value != string.Empty)
        {
            string query = getDisplayQuery();
            bool recordExist = this.fetchRecord(query);
        }
        else
        {
            this.showLastRecord();
        }
        string qry = clsCommon.getString("select count(Doc_No) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'");
        if (qry != "0")
        {
            clsButtonNavigation.enableDisable("S");
            this.makeEmptyForm("S");
            this.enableDisableNavigateButtons();
        }
        else
        {
            clsButtonNavigation.enableDisable("N");
            this.makeEmptyForm("N");
            this.enableDisableNavigateButtons();
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }
    }
    #endregion
    #region [fetchrecord]
    private bool fetchRecord(string qry)
    {
        try
        {
            bool recordExist = false;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        Label lblCreated = (Label)Master.FindControl("MasterlblCreatedBy");
                        Label lblModified = (Label)Master.FindControl("MasterlblModifiedBy");
                        if (lblCreated != null)
                        {
                            lblCreated.Text = "Created By: " + dt.Rows[0]["Created_By"].ToString();
                        }
                        if (lblModified != null)
                        {
                            lblModified.Text = "Modified By: " + dt.Rows[0]["Modified_By"].ToString();
                        }
                        Label lblCreatedDate = (Label)Master.FindControl("MasterlblCreatedDate");
                        Label lblModifiedDate = (Label)Master.FindControl("MasterlblModifiedDate");
                        if (lblCreatedDate != null)
                        {
                            if (dt.Rows[0]["Created_Date"].ToString() == string.Empty)
                            {
                                lblCreatedDate.Text = "";
                            }
                            else
                            {
                                lblCreatedDate.Text = "Created Date" + dt.Rows[0]["Created_Date"].ToString();
                            }
                        }
                        if (lblModifiedDate != null)
                        {
                            if (dt.Rows[0]["Modified_Date"].ToString() == string.Empty)
                            {
                                lblModifiedDate.Text = "";
                            }
                            else
                            {
                                lblModifiedDate.Text = "Modified Date" + dt.Rows[0]["Modified_Date"].ToString();
                            }
                        }

                        txtDoc_No.Text = dt.Rows[0]["Doc_No"].ToString();
                        txtDoc_Date.Text = dt.Rows[0]["Doc_Date"].ToString();
                        txtAc_Code.Text = dt.Rows[0]["Ac_Code"].ToString();
                        lblAc_Code_Name.Text = dt.Rows[0]["Ac_Name_E"].ToString();
                        txtInv1.Text = dt.Rows[0]["Inv1"].ToString();
                        txtInv2.Text = dt.Rows[0]["Inv2"].ToString();
                        txtInv3.Text = dt.Rows[0]["Inv3"].ToString();
                        txtInv4.Text = dt.Rows[0]["Inv4"].ToString();
                        txtInv5.Text = dt.Rows[0]["Inv5"].ToString();
                        txtInv6.Text = dt.Rows[0]["Inv6"].ToString();
                        txtInv7.Text = dt.Rows[0]["Inv7"].ToString();
                        txtInv8.Text = dt.Rows[0]["Inv8"].ToString();
                        txtInv9.Text = dt.Rows[0]["Inv9"].ToString();
                        txtInv10.Text = dt.Rows[0]["Inv10"].ToString();
                        lblExport_Amount.Text = dt.Rows[0]["Export_Amount"].ToString();
                        lblExport_Qty.Text = dt.Rows[0]["Export_Qty"].ToString();
                        lblInvoice_Qty.Text = dt.Rows[0]["Invoice_Qty"].ToString();
                        lblDifference.Text = dt.Rows[0]["Difference"].ToString();
                        lblInvoice_Value.Text = dt.Rows[0]["Invoice_Value"].ToString();
                        lblUnique_ID.Text = dt.Rows[0]["UpdatedDoc_No"].ToString();

                        recordExist = true;
                        lblMsg.Text = "";
                        #region Details grid
                        qry = "select distinct Detail_Id as Detail_Id,item_code as [Item Code],System_Name_E as [Item Name],Inv_Qty as Qty," +
                          " packing, Inv_Value as Value,Invoice_No as Doc_No from qryInvoice_Export where Company_Code='" + Convert.ToInt32(Session["Company_Code"]).ToString() +
                            "' and Year_Code=" + Convert.ToInt32(Session["year"]).ToString() + " And Doc_No='" + txtDoc_No.Text + "'";

                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                                    dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        dt.Rows[i]["rowAction"] = "N";
                                        dt.Rows[i]["SrNo"] = i + 1;
                                    }
                                    grdGenerate.DataSource = dt;
                                    grdGenerate.DataBind();
                                    ViewState["currentTableNew"] = dt;
                                }
                                else
                                {
                                    grdGenerate.DataSource = null;
                                    grdGenerate.DataBind();
                                    ViewState["currentTableNew"] = null;
                                }
                            }
                            else
                            {
                                grdGenerate.DataSource = null;
                                grdGenerate.DataBind();
                                ViewState["currentTableNew"] = null;
                            }
                        }
                        else
                        {
                            grdGenerate.DataSource = null;
                            grdGenerate.DataBind();
                            ViewState["currentTableNew"] = null;
                        }
                        #endregion
                        #region Details
                        qry = "select distinct ExL_Detail_ID as Detail_Id,ExL_item_code as [Item Code],ExL_Item_Name" +
                          " as [Item Name],ExL_Export_Qty as Qty,Export_Rate as Rate,ExL_Export_Amount as Value from qryInvoice_Export where Company_Code='"
                          + Convert.ToInt32(Session["Company_Code"]).ToString() + "' and Year_Code=" + Convert.ToInt32(Session["year"]).ToString()
                            + " And Doc_No='" + txtDoc_No.Text + "'";
                        ds = clsDAL.SimpleQuery(qry);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                                    dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        dt.Rows[i]["rowAction"] = "N";
                                        dt.Rows[i]["SrNo"] = i + 1;
                                    }
                                    grdDetail.DataSource = dt;
                                    grdDetail.DataBind();
                                    ViewState["currentTable"] = dt;
                                }
                                else
                                {
                                    grdDetail.DataSource = null;
                                    grdDetail.DataBind();
                                    ViewState["currentTable"] = null;
                                }
                            }
                            else
                            {
                                grdDetail.DataSource = null;
                                grdDetail.DataBind();
                                ViewState["currentTable"] = null;
                            }
                        }
                        else
                        {
                            grdDetail.DataSource = null;
                            grdDetail.DataBind();
                            ViewState["currentTable"] = null;
                        }
                        #endregion
                        pnlgrdDetail.Enabled = false;
                        pnlgrdGenerate.Enabled = false;


                    }
                }
            }
            hdnf.Value = txtDoc_No.Text;
            this.enableDisableNavigateButtons();
            return recordExist;
        }
        catch
        {
            return false;
        }
    }
    #endregion
    #region csCalculations
    private void csCalculations()
    {
        try
        {
            if (strTextBox == "txtEdit_Doc_No")
            {
                setFocusControl(txtEdit_Doc_No);
            }
            if (strTextBox == "txtDoc_No")
            {
                setFocusControl(txtDoc_No);
            }
            if (strTextBox == "txtDoc_Date")
            {
                //try
                //{
                //    string dt = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                //    //if (clsCommon.isValidDateforOp(dt) == true || dt == "")
                //    if (dt == "")
                //    {
                //        setFocusControl(txtDoc_Date);
                //    }
                //    else
                //    {
                //        txtDoc_Date.Text = "";
                //        setFocusControl(txtDoc_Date);
                //    }
                //}
                //catch
                //{
                //    txtDoc_Date.Text = "";
                //    setFocusControl(txtDoc_Date);
                //}

                try
                {
                    string dt = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    if (clsCommon.isValidDateforOp(dt) == true || dt == "")
                    //if (dt == "")
                    {
                        setFocusControl(txtAc_Code);
                    }
                    else
                    {
                        //txtDoc_Date.Text = "";
                        //setFocusControl(txtDoc_Date);
                    }
                }
                catch
                {
                    txtDoc_Date.Text = "";
                    setFocusControl(txtDoc_Date);
                }
            }
            if (strTextBox == "txtAc_Code")
            {
                string acname = "";
                if (txtAc_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtAc_Code.Text);
                    if (a == false)
                    {
                        btntxtAc_Code_Click(this, new EventArgs());
                    }
                    else
                    {
                        acname = clsCommon.getString("select Ac_Name_E from  qrymstaccountmaster where Ac_Code=" + txtAc_Code.Text
                            + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                        if (acname != string.Empty)
                        {
                            lblAc_Code_Name.Text = acname;
                            setFocusControl(txtInv1);

                        }
                        else
                        {
                            txtAc_Code.Text = string.Empty;
                            lblAc_Code_Name.Text = acname;
                            setFocusControl(txtAc_Code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtInv1);
                }
            }
            if (strTextBox == "txtInv1")
            {
                setFocusControl(txtInv1);
            }
            if (strTextBox == "txtInv2")
            {
                setFocusControl(txtInv2);
            }
            if (strTextBox == "txtInv3")
            {
                setFocusControl(txtInv3);
            }
            if (strTextBox == "txtInv4")
            {
                setFocusControl(txtInv4);
            }
            if (strTextBox == "txtInv5")
            {
                setFocusControl(txtInv5);
            }
            if (strTextBox == "txtInv6")
            {
                setFocusControl(txtInv6);
            }
            if (strTextBox == "txtInv7")
            {
                setFocusControl(txtInv7);
            }
            if (strTextBox == "txtInv8")
            {
                setFocusControl(txtInv8);
            }
            if (strTextBox == "txtInv9")
            {
                setFocusControl(txtInv9);
            }
            if (strTextBox == "txtInv10")
            {
                setFocusControl(txtInv10);
            }
            if (strTextBox == "txtQty")
            {
                //setFocusControl(txtQty);
            }
            if (strTextBox == "txtRate")
            {
                setFocusControl(txtValue);
            }
            if (strTextBox == "txtValue")
            {
                setFocusControl(btnAdddetails);
            }

            if (strTextBox == "txtPart_no")
            {
                string itemname = "";
                if (txtPart_no.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtPart_no.Text);
                    if (a == false)
                    {
                        btntxtPart_no_Click(this, new EventArgs());
                    }
                    else
                    {
                        itemname = clsCommon.getString("select System_Name_E from " + SystemMasterTable + " where System_Code=" + txtPart_no.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='I'");
                        if (itemname != string.Empty)
                        {

                            lblPart_no.Text = itemname;

                            setFocusControl(txtQty);


                        }
                        else
                        {
                            txtPart_no.Text = string.Empty;
                            lblPart_no.Text = itemname;

                            setFocusControl(txtPart_no);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtPart_no);
                }
            }



            double qty = 0.00;
            //double rate = 0.00;
            double value = 0.00;

            qty = txtQty.Text != string.Empty ? Convert.ToDouble(txtQty.Text) : 0;
            double rate = txtRate.Text != string.Empty ? Convert.ToDouble(txtRate.Text) : 0;
            value = qty * rate;
            txtValue.Text = value.ToString();

            #region grdcalculation
            double InvoiceValue = 0.00;
            double InvoiceQty = 0.00;
            double ExportQty = 0.00;
            double NetValue = 0.00;
            if (grdGenerate.Rows.Count > 0)
            {
                for (int i = 0; i < grdGenerate.Rows.Count; i++)
                {
                    //if (grdGenerate.Rows[i].Cells[Rowaction].Text != "R" && grdGenerate.Rows[i].Cells[Rowaction].Text != "D")
                    //{
                    double GridInvoiceValue = Convert.ToDouble(grdGenerate.Rows[i].Cells[7].Text.Trim());
                    InvoiceValue = InvoiceValue + GridInvoiceValue;

                    double GridExportQty = Convert.ToDouble(grdGenerate.Rows[i].Cells[5].Text.Trim());
                    ExportQty = ExportQty + GridExportQty;

                    //}
                }
                lblExport_Amount.Text = InvoiceValue.ToString();
                lblExport_Qty.Text = ExportQty.ToString();
            }

            if (grdDetail.Rows.Count > 0)
            {
                for (int i = 0; i < grdDetail.Rows.Count; i++)
                {
                    if (grdDetail.Rows[i].Cells[8].Text != "R" && grdDetail.Rows[i].Cells[8].Text != "D")
                    {
                        double GridInvoiceQty = Convert.ToDouble(grdDetail.Rows[i].Cells[5].Text.Trim());
                        InvoiceQty = InvoiceQty + GridInvoiceQty;

                        double GridNetValue = Convert.ToDouble(grdDetail.Rows[i].Cells[7].Text.Trim());
                        NetValue = NetValue + GridNetValue;

                    }
                }
                lblInvoice_Qty.Text = InvoiceQty.ToString();
                lblDifference.Text = (ExportQty - InvoiceQty).ToString();
                lblInvoice_Value.Text = NetValue.ToString();
            }
            #endregion
        }
        catch
        {
        }
    }
    #endregion
    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            string qryDisplay = " select * from " + qryCommon + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Doc_No=" + hdnf.Value + "  and Year_Code=" + Convert.ToInt32(Session["year"].ToString());
            return qryDisplay;
        }
        catch
        {
            return "";
        }
    }
    #endregion
    #region navigateRecord
    private void navigateRecord()
    {
        try
        {
            if (hdnf.Value != string.Empty)
            {
                ViewState["mode"] = "U";
                txtDoc_No.Text = hdnf.Value;
                string query = getDisplayQuery();
                clsButtonNavigation.enableDisable("N");
                bool recordExist = this.fetchRecord(query);
                if (recordExist == true)
                {
                    btnEdit.Enabled = true;
                    btnEdit.Focus();
                }
                this.enableDisableNavigateButtons();
                this.makeEmptyForm("S");
            }
            else
            {
                showLastRecord();
            }
        }
        catch
        {

        }
    }
    #endregion

    #region [btnOpenDetailsPopup_Click]
    protected void btnOpenDetailsPopup_Click(object sender, EventArgs e)
    {
        btnAdddetails.Text = "ADD";
        pnlPopupDetails.Style["display"] = "block";
    }
    #endregion

    #region [btnAdddetails_Click]
    protected void btnAdddetails_Click(object sender, EventArgs e)
    {
        try
        {
            int rowIndex = 1;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt = new DataTable();
            if (ViewState["currentTable"] != null)
            {
                dt = (DataTable)ViewState["currentTable"];
                if (dt.Rows[0]["Detail_ID"].ToString().Trim() != "")
                {
                    if (btnAdddetails.Text == "ADD")
                    {
                        dr = dt.NewRow();
                        #region calculate rowindex
                        int maxIndex = 0;
                        int[] index = new int[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            index[i] = Convert.ToInt32(dt.Rows[i]["Detail_ID"].ToString());
                        }
                        if (index.Length > 0)
                        {
                            for (int i = 0; i < index.Length; i++)
                            {
                                if (index[i] > maxIndex)
                                {
                                    maxIndex = index[i];
                                }
                            }
                            rowIndex = maxIndex + 1;
                        }
                        else
                        {
                            rowIndex = maxIndex;          //1
                        }
                        #endregion
                        //     rowIndex = dt.Rows.Count + 1;
                        dr["Detail_ID"] = rowIndex;     //auto
                        dr["rowAction"] = "A";
                        dr["SrNo"] = 0;
                    }
                    else
                    {
                        //update row
                        int n = Convert.ToInt32(lblNo.Text);
                        rowIndex = Convert.ToInt32(lblID.Text);   //auto no
                        dr = (DataRow)dt.Rows[n - 1];
                        dr["Detail_ID"] = rowIndex;
                        dr["SrNo"] = 0;
                        #region decide whether actual row is updating or virtual [rowAction]
                        string id = clsCommon.getString("select Detail_ID from " + tblDetails + " where Doc_No='" + txtDoc_No.Text + "' and Detail_ID=" + lblID.Text + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()));
                        if (id != string.Empty)
                        {
                            dr["rowAction"] = "U";   //actual row
                        }
                        else
                        {
                            dr["rowAction"] = "A";    //virtual row
                        }
                        #endregion
                    }
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add((new DataColumn("Item Code", typeof(int))));
                    dt.Columns.Add((new DataColumn("Item Name", typeof(string))));
                    #region [Write here columns]
                    dt.Columns.Add((new DataColumn("Qty", typeof(int))));
                    dt.Columns.Add((new DataColumn("Rate", typeof(double))));
                    dt.Columns.Add((new DataColumn("Value", typeof(double))));
                    #endregion
                    dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                    dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                    dr = dt.NewRow();
                    dr["Detail_ID"] = rowIndex;
                    dr["rowAction"] = "A";
                    dr["SrNo"] = 0;
                }
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add((new DataColumn("Item Code", typeof(int))));
                dt.Columns.Add((new DataColumn("Item Name", typeof(string))));
                #region [Write here columns]
                dt.Columns.Add((new DataColumn("Qty", typeof(int))));
                dt.Columns.Add((new DataColumn("Rate", typeof(double))));
                dt.Columns.Add((new DataColumn("Value", typeof(double))));
                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                dr = dt.NewRow();
                dr["Detail_ID"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]
            dr["Item Code"] = txtPart_no.Text;
            dr["Item Name"] = lblPart_no.Text;
            dr["Qty"] = txtQty.Text;
            dr["Rate"] = txtRate.Text;
            dr["Value"] = txtValue.Text;
            #endregion
            if (btnAdddetails.Text == "ADD")
            {
                dt.Rows.Add(dr);
            }
            #region set sr no
            DataRow drr = null;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    drr = (DataRow)dt.Rows[i];
                    drr["SrNo"] = i + 1;
                }
            }
            #endregion
            grdDetail.DataSource = dt;
            grdDetail.DataBind();
            ViewState["currentTable"] = dt;
            if (btnAdddetails.Text == "ADD")
            {
                pnlPopupDetails.Style["display"] = "none";
                setFocusControl(txtQty);
            }
            else
            {
                pnlPopupDetails.Style["display"] = "none";
                setFocusControl(txtRate);
                btnOpenDetailsPopup.Focus();
            }
            // Empty Code->
            csCalculations();
            txtQty.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtValue.Text = string.Empty;
            txtPart_no.Text = string.Empty;
            lblPart_no.Text = string.Empty;
            btnAdddetails.Text = "ADD";
        }
        catch
        {
        }
    }
    #endregion

    #region [btnClosedetails_Click]
    protected void btnClosedetails_Click(object sender, EventArgs e)
    {
        lblNo.Text = string.Empty;
        lblID.Text = string.Empty;
        txtQty.Text = string.Empty;
        txtRate.Text = string.Empty;
        txtValue.Text = string.Empty;
        btnAdddetails.Text = "ADD";
        setFocusControl(txtQty);
        pnlPopupDetails.Style["display"] = "none";
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gvrow)
    {
        lblNo.Text = Server.HtmlDecode(gvrow.Cells[Srno].Text);//srno row id;
        lblID.Text = Server.HtmlDecode(gvrow.Cells[2].Text);//Detail id;
        txtPart_no.Text = Server.HtmlDecode(gvrow.Cells[Item_Code].Text);
        lblPart_no.Text = Server.HtmlDecode(gvrow.Cells[4].Text);
        txtQty.Text = Server.HtmlDecode(gvrow.Cells[Qty].Text);
        txtRate.Text = Server.HtmlDecode(gvrow.Cells[Rate].Text);
        txtValue.Text = Server.HtmlDecode(gvrow.Cells[Value].Text);
        txtPart_no.Enabled = false;
        btntxtPart_no.Enabled = false;
        txtQty.Enabled = false;

    }
    #endregion

    #region [DeleteDetailsRow]
    private void DeleteDetailsRow(GridViewRow gridViewRow, string action)
    {
        try
        {
            int rowIndex = gridViewRow.RowIndex;
            if (ViewState["currentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["currentTable"];
                int ID = Convert.ToInt32(dt.Rows[rowIndex]["Item Code"].ToString());
                string IDExisting = clsCommon.getString("select item_code from " + tblDetails + " where Doc_No='" + hdnf.Value + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                if (IDExisting != string.Empty)
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "D";// rowAction Index add 
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "D";            //D=Delete from table
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "N";// Add rowaction id
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "N";
                    }
                }
                else
                {
                    if (action == "Delete")
                    {
                        gridViewRow.Style["background-color"] = "#64BB7F";
                        gridViewRow.ForeColor = System.Drawing.Color.White;
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "R";       // add row action R=Only remove fro grid
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "R";
                    }
                    if (action == "Open")
                    {
                        gridViewRow.Style["background-color"] = "#fff5ee";
                        gridViewRow.ForeColor = System.Drawing.Color.Gray;
                        grdDetail.Rows[rowIndex].Cells[Rowaction].Text = "A";
                        DataRow dr = dt.Rows[rowIndex];
                        dr["rowAction"] = "A";
                    }
                }
                ViewState["currentTable"] = dt;
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [grdDetail_RowDataBound]
    protected void grdDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            // if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            // e.Row.Cells[2].Width = new Unit("120px");
            e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
            e.Row.Cells[1].ControlStyle.Width = new Unit("40px");
            e.Row.Cells[1].Visible = true;
            e.Row.Cells[2].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[Rowaction].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[Srno].ControlStyle.Width = new Unit("70px");
            //--------------------------------------------------

            e.Row.Cells[Item_Code].ControlStyle.Width = new Unit("80px");
            e.Row.Cells[Item_Code].Style["overflow"] = "hidden";
            e.Row.Cells[Item_Code].HorizontalAlign = HorizontalAlign.Center;

            e.Row.Cells[4].ControlStyle.Width = new Unit("200px");
            e.Row.Cells[4].Style["overflow"] = "hidden";
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;

            e.Row.Cells[Qty].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Qty].Style["overflow"] = "hidden";
            e.Row.Cells[Qty].HorizontalAlign = HorizontalAlign.Center;
            //--------------------------------------------------
            e.Row.Cells[Rate].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Rate].Style["overflow"] = "hidden";
            e.Row.Cells[Rate].HorizontalAlign = HorizontalAlign.Center;
            //--------------------------------------------------
            e.Row.Cells[Value].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Value].Style["overflow"] = "hidden";
            e.Row.Cells[Value].HorizontalAlign = HorizontalAlign.Center;
            //--------------------------------------------------
            //     e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            //    e.Row.Cells[0].Style["overflow" ] = "hiden";
            //    e.Row.Cells[0].Visible =true;
            //}
        }
        catch
        {
        }
    }
    #endregion
    #region [grdPopup_RowDataBound]
    protected void grdPopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string v = hdnfClosePopup.Value;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;

        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            if (v == "txtDoc_No")
            {
                e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
                e.Row.Cells[1].ControlStyle.Width = new Unit("100px");
                e.Row.Cells[2].ControlStyle.Width = new Unit("100px");
                //e.Row.Cells[3].ControlStyle.Width = new Unit("100px");

            }
            if (v == "txtAc_Code" || v == "txtPart_no")
            {
                e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
                e.Row.Cells[1].ControlStyle.Width = new Unit("200px");
                e.Row.Cells[2].ControlStyle.Width = new Unit("100px");
            }
            if (v == "txtInv1" || v == "txtInv2" || v == "txtInv3" || v == "txtInv4" || v == "txtInv5" || v ==
                "txtInv5" || v == "txtInv6" || v == "txtInv7" || v == "txtInv8" || v == "txtInv9" || v == "txtInv10")
            {
                e.Row.Cells[0].ControlStyle.Width = new Unit("100px");
                e.Row.Cells[1].ControlStyle.Width = new Unit("150px");


            }
        }
    }
    #endregion
    #region [grdPopup_PageIndexChanging]
    protected void grdPopup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPopup.PageIndex = e.NewPageIndex;
        this.btnSearch_Click(sender, e);
    }
    #endregion
    #region [grdPopup_RowCreated]
    protected void grdPopup_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow &&
            (e.Row.RowState == DataControlRowState.Normal ||
           e.Row.RowState == DataControlRowState.Alternate))
            {
                e.Row.TabIndex = -1;
                e.Row.Attributes["onclick"] = string.Format("javascript:SelectRow(this, {0});", e.Row.RowIndex);
                e.Row.Attributes["onkeydown"] = "javascript:return SelectSibling(event);";
                e.Row.Attributes["onselectstart"] = "javascript:return false;";
                // e.Row.Attributes["onkeyup"] = "javascript:return selectRow(event);";
            }
        }
        catch
        {
            throw;
        }
    }
    #endregion
    #region [RowCommand]
    protected void grdDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int rowindex = row.RowIndex;
            if (e.CommandArgument == "lnk")
            {
                switch (e.CommandName)
                {
                    case "EditRecord":
                        if (grdDetail.Rows[rowindex].Cells[Rowaction].Text != "D" && grdDetail.Rows[rowindex].Cells[Rowaction].Text != "R")//add row action id
                        {
                            pnlPopupDetails.Style["display"] = "none";
                            this.showDetailsRow(grdDetail.Rows[rowindex]);
                            btnAdddetails.Text = "UPDATE";
                            setFocusControl(txtRate);
                        }
                        break;
                    case "DeleteRecord":
                        string action = "";
                        LinkButton lnkDelete = (LinkButton)e.CommandSource;
                        if (lnkDelete.Text == "Delete")
                        {
                            action = "Delete";
                            lnkDelete.Text = "Open";
                        }
                        else
                        {
                            action = "Open";
                            lnkDelete.Text = "Delete";
                        }
                        this.DeleteDetailsRow(grdDetail.Rows[rowindex], action);
                        break;
                }
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [txtEdit_Doc_No_TextChanged]
    protected void txtEdit_Doc_No_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtEdit_Doc_No.Text;
        //strTextBox = "txtEdit_Doc_No";
        //csCalculations();
        try
        {
            bool a = clsCommon.isStringIsNumeric(txtEdit_Doc_No.Text);
            if (a == false)
            {
                searchString = txtEdit_Doc_No.Text;
                strTextBox = "txtEditDoc_No";
                pnlPopup.Style["display"] = "block";

                hdnfClosePopup.Value = "txtEdit_Doc_No";
                btnSearch_Click(this, new EventArgs());
            }
            else
            {
                txtDoc_No.Enabled = false;
                hdnf.Value = txtEdit_Doc_No.Text;
                string qry1 = getDisplayQuery();
                fetchRecord(qry1);
                setFocusControl(txtEdit_Doc_No);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion
    #region [txtDoc_No_TextChanged]
    protected void txtDoc_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDoc_No.Text;
        strTextBox = "txtDoc_No";
        csCalculations();
    }
    #endregion
    #region [txtDoc_Date_TextChanged]
    protected void txtDoc_Date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDoc_Date.Text;
        strTextBox = "txtDoc_Date";
        csCalculations();
    }
    #endregion
    #region [txtAc_Code_TextChanged]
    protected void txtAc_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAc_Code.Text;
        strTextBox = "txtAc_Code";
        csCalculations();
    }
    #endregion
    #region [btntxtAc_Code_Click]
    protected void btntxtAc_Code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtAc_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    #region [txtInv1_TextChanged]
    protected void txtInv1_TextChanged(object sender, EventArgs e)
    {
        searchString = txtInv1.Text;
        strTextBox = "txtInv1";
        csCalculations();


    }
    #endregion

    #region [txtInv2_TextChanged]
    protected void txtInv2_TextChanged(object sender, EventArgs e)
    {
        searchString = txtInv2.Text;
        strTextBox = "txtInv2";
        csCalculations();
    }
    #endregion
    #region [txtInv3_TextChanged]
    protected void txtInv3_TextChanged(object sender, EventArgs e)
    {
        searchString = txtInv3.Text;
        strTextBox = "txtInv3";
        csCalculations();
    }
    #endregion
    #region [txtInv4_TextChanged]
    protected void txtInv4_TextChanged(object sender, EventArgs e)
    {
        searchString = txtInv4.Text;
        strTextBox = "txtInv4";
        csCalculations();
    }
    #endregion
    #region [txtInv5_TextChanged]
    protected void txtInv5_TextChanged(object sender, EventArgs e)
    {
        searchString = txtInv5.Text;
        strTextBox = "txtInv5";
        csCalculations();
    }
    #endregion
    #region [txtInv6_TextChanged]
    protected void txtInv6_TextChanged(object sender, EventArgs e)
    {
        searchString = txtInv6.Text;
        strTextBox = "txtInv6";
        csCalculations();
    }
    #endregion
    #region [txtInv7_TextChanged]
    protected void txtInv7_TextChanged(object sender, EventArgs e)
    {
        searchString = txtInv7.Text;
        strTextBox = "txtInv7";
        csCalculations();
    }
    #endregion
    #region [txtInv8_TextChanged]
    protected void txtInv8_TextChanged(object sender, EventArgs e)
    {
        searchString = txtInv8.Text;
        strTextBox = "txtInv8";
        csCalculations();
    }
    #endregion
    #region [txtInv9_TextChanged]
    protected void txtInv9_TextChanged(object sender, EventArgs e)
    {
        searchString = txtInv9.Text;
        strTextBox = "txtInv9";
        csCalculations();
    }
    #endregion
    #region [txtInv10_TextChanged]
    protected void txtInv10_TextChanged(object sender, EventArgs e)
    {
        searchString = txtInv10.Text;
        strTextBox = "txtInv10";
        csCalculations();
    }
    #endregion
    #region [btntxtDoc_No_Click]
    protected void btntxtDoc_No_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtDoc_No";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    #region [txtPart_no_TextChanged]
    protected void txtPart_no_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPart_no.Text;
        strTextBox = "txtPart_no";
        csCalculations();
    }
    #endregion

    #region [btntxtPart_no_Click]
    protected void btntxtPart_no_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtPart_no";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion

    #region [txtQty_TextChanged]
    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        searchString = txtQty.Text;
        strTextBox = "txtQty";
        csCalculations();
    }
    #endregion


    #region [txtRate_TextChanged]
    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRate.Text;
        strTextBox = "txtRate";
        csCalculations();
        setFocusControl(txtValue);
    }
    #endregion


    #region [txtValue_TextChanged]
    protected void txtValue_TextChanged(object sender, EventArgs e)
    {
        searchString = txtValue.Text;
        strTextBox = "txtValue";
        csCalculations();
    }
    #endregion

    #region [btnSearch_Click]
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (searchString != string.Empty)
            {
                txtSearchText.Text = searchString;
            }
            else
            {
                txtSearchText.Text = txtSearchText.Text;
            }
            if (hdnfClosePopup.Value == "txtDoc_No" || hdnfClosePopup.Value == "txtEditDoc_No")
            {
                if (btntxtDoc_No.Text == "Change No")
                {
                    pnlPopup.Style["display"] = "none";
                    txtDoc_No.Text = string.Empty;
                    txtDoc_No.Enabled = true;

                    btnSave.Enabled = false;
                    setFocusControl(txtDoc_No);
                    hdnfClosePopup.Value = "Close";
                }
                if (btntxtDoc_No.Text == "Choose No")
                {
                    lblPopupHead.Text = "--Select DOC No--";
                    

                    string qry = "select distinct Doc_No,Ac_Code,Ac_Name_E from " + qryCommon + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                        + " and ( Doc_No like '%" + txtSearchText.Text + "%' or Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' ) order by Doc_No";
                    this.showPopup(qry);
                }
            }

            if (hdnfClosePopup.Value == "txtAc_Code")
            {
                lblPopupHead.Text = "--Select Ship To Code--";
                string qry = "select Ac_Code,Ac_Name_E,cityname from qrymstaccountmaster where Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or cityname like '%" + txtSearchText.Text + "%' ) order by Ac_Name_E";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtInv1" || hdnfClosePopup.Value == "txtInv2" || hdnfClosePopup.Value == "txtInv3" || hdnfClosePopup.Value == "txtInv4" || hdnfClosePopup.Value == "txtInv5"
                || hdnfClosePopup.Value == "txtInv6" || hdnfClosePopup.Value == "txtInv7" || hdnfClosePopup.Value == "txtInv8" || hdnfClosePopup.Value == "txtInv9" || hdnfClosePopup.Value == "txtInv10")
            {
                

                string qry = " SELECT Doc_No,convert(varchar(10),Doc_Date,103) as Doc_Date  FROM nt_1_sugarsale WHERE  " +
                    " Doc_No NOT IN(SELECT Invoice_No FROM Export_InvLine where  year_code=" + Convert.ToInt32(Session["year"].ToString())
                    + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + ") and year_code=" + Convert.ToInt32(Session["year"].ToString())
                   + " and company_code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Ac_Code='" + txtAc_Code.Text + "' and ( Doc_No like '%" + txtSearchText.Text + "%' or Doc_Date like '%" + txtSearchText.Text + "%' ) order by Doc_No desc";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtPart_no")
            {
                lblPopupHead.Text = "--Select Item--";
                string qry = "select System_Code,System_Name_E as Item_Name from " + SystemMasterTable + " where System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString() + "and ( System_Code like '%" + txtSearchText.Text + "%' or System_Name_E like '%" + txtSearchText.Text + "%')");
                this.showPopup(qry);
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [Popup Button Code]
    protected void showPopup(string qry)
    {
        try
        {
            setFocusControl(txtSearchText);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        grdPopup.DataSource = dt;
                        grdPopup.DataBind();
                        hdHelpPageCount.Value = grdPopup.PageCount.ToString();
                        pnlPopup.Style["display"] = "block";
                    }
                    else
                    {
                        grdPopup.DataSource = null;
                        grdPopup.DataBind();
                        hdHelpPageCount.Value = "0";
                        pnlPopup.Style["display"] = "block";
                    }
                }
            }
        }
        catch
        {
        }
    }
    #endregion

    #region [imgBtnClose_Click]
    protected void imgBtnClose_Click(object sender, EventArgs e)
    {
        try
        {
            hdnfClosePopup.Value = "Close";
            pnlPopup.Style["display"] = "none";
            txtSearchText.Text = string.Empty;
            grdPopup.DataSource = null;
            grdPopup.DataBind();
        }
        catch
        {
        }
    }
    #endregion

    #region [setFocusControl]
    private void setFocusControl(WebControl wc)
    {
        objAsp = wc;
        System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(wc);
    }
    #endregion
    #region [txtSearchText_TextChanged]
    protected void txtSearchText_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (hdnfClosePopup.Value == "Close")
            {
                txtSearchText.Text = string.Empty;
                pnlPopup.Style["display"] = "none";
                grdPopup.DataSource = null;
                grdPopup.DataBind();
                if (objAsp != null)
                    System.Web.UI.ScriptManager.GetCurrent(this).SetFocus(objAsp);
            }
            else
            {
                pnlPopup.Style["display"] = "block";
                searchString = txtSearchText.Text;
                strTextBox = hdnfClosePopup.Value;
                setFocusControl(btnSearch);
            }
        }
        catch
        {
        }
    }
    #endregion
    #region [btnSave_Click]
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region [Validation Part]
        csCalculations();
        bool isValidated = true;
        //        if textbox is date then if condition will be like this if(clsCommon.isValidDate(txtDoc_Date.Text==true))

        if (Convert.ToDouble(lblDifference.Text) == 0)
        {
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('plz check Difference Amount!!!!!');", true);
            return;
        }
        if (txtDoc_Date.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtDoc_Date);
            return;
        }
        if (txtAc_Code.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtAc_Code);
            return;
        }
        /*  In Grid At Least One Record is required
            int count = 0;
              if (grdDetail.Rows.Count == 0)
          {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),"","alert('Please Enter Details!!!!             isValidated = false;
          setFocusControl(btnOpenDetailsPopup); 
          return;  
          }
           if (grdDetail.Rows.Count >= 1) 
          for (int i = 0; i < grdDetail.Rows.Count; i++)
          {
           if (grdDetail.Rows[i].Cells[10].Text == "D")
          {
          count++; 
          }
          }
            if (grdDetail.Rows.Count == count)
          {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),"","alert('Please Enter Minumun One Details!!!!          isValidated = false; 
            setFocusControl(btnOpenDetailsPopup); 
          return;
         } 
          }
          */


        if (grdDetail.Rows.Count > 0)
        {
            for (int i = 0; i < grdDetail.Rows.Count; i++)
            {
                if (grdDetail.Rows[i].Cells[6].Text != "0" && grdDetail.Rows[i].Cells[7].Text != "0")
                {
                    isValidated = true;
                }
                else
                {
                    string partno = grdDetail.Rows[i].Cells[3].Text;
                    setFocusControl(txtDoc_Date);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Enter Part Rate of Part No=" + partno + "');", true);
                    return;
                }
            }

        }
        #endregion

        #region -Head part declearation

        XElement root = new XElement("ROOT");
        XElement child1 = new XElement("Head");
        int Company_Code = Convert.ToInt32(Convert.ToInt32(Session["Company_Code"].ToString()));
        int Year_Code = Convert.ToInt32(Convert.ToInt32(Session["year"].ToString()));
        int Branch_Code = Convert.ToInt32(Convert.ToInt32(Session["Branch_Code"].ToString()));
        string Created_By = Session["user"].ToString();
        string Modified_By = Session["user"].ToString();
        string Created_Date = DateTime.Now.ToString("yyyy/MM/dd");
        string Modified_Date = DateTime.Now.ToString("yyyy/MM/dd");
        string retValue = string.Empty;
        string strRev = string.Empty;
        #endregion-End of Head part declearation
        #region Save Head Part
        //child1.SetAttributeValue("Edit_Doc_No", txtEdit_Doc_No.Text != string.Empty ? txtEdit_Doc_No.Text : "0");
        //child1.SetAttributeValue("Doc_No", txtDoc_No.Text != string.Empty ? txtDoc_No.Text : "0");

        child1.SetAttributeValue("Doc_Date", DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));
        child1.SetAttributeValue("Ac_Code", txtAc_Code.Text != string.Empty ? txtAc_Code.Text : "0");
        child1.SetAttributeValue("Inv1", txtInv1.Text != string.Empty ? txtInv1.Text : "0");
        child1.SetAttributeValue("Inv2", txtInv2.Text != string.Empty ? txtInv2.Text : "0");
        child1.SetAttributeValue("Inv3", txtInv3.Text != string.Empty ? txtInv3.Text : "0");
        child1.SetAttributeValue("Export_Qty", lblExport_Qty.Text != string.Empty ? lblExport_Qty.Text : "0");
        child1.SetAttributeValue("Invoice_Qty", lblInvoice_Qty.Text != string.Empty ? lblInvoice_Qty.Text : "0");
        child1.SetAttributeValue("Difference", lblDifference.Text != string.Empty ? lblDifference.Text : "0");
        child1.SetAttributeValue("Export_Amount", lblExport_Amount.Text != string.Empty ? lblExport_Amount.Text : "0");
        child1.SetAttributeValue("Inv4", txtInv4.Text != string.Empty ? txtInv4.Text : "0");
        child1.SetAttributeValue("Inv5", txtInv5.Text != string.Empty ? txtInv5.Text : "0");
        child1.SetAttributeValue("Inv6", txtInv6.Text != string.Empty ? txtInv6.Text : "0");
        child1.SetAttributeValue("Inv7", txtInv7.Text != string.Empty ? txtInv7.Text : "0");
        child1.SetAttributeValue("Inv8", txtInv8.Text != string.Empty ? txtInv8.Text : "0");
        child1.SetAttributeValue("Inv9", txtInv9.Text != string.Empty ? txtInv9.Text : "0");
        child1.SetAttributeValue("Inv10", txtInv10.Text != string.Empty ? txtInv10.Text : "0");
        child1.SetAttributeValue("Company_Code", Company_Code);
        child1.SetAttributeValue("Year_Code", Year_Code);
        child1.SetAttributeValue("Branch_Code", Branch_Code);
        child1.SetAttributeValue("Invoice_Value", lblInvoice_Value.Text != string.Empty ? lblInvoice_Value.Text : "0");
        child1.SetAttributeValue("UpdatedDoc_No", lblUnique_ID.Text);
        if (btnSave.Text != "Save")
        {
            child1.SetAttributeValue("Modified_By", Modified_By);
            child1.SetAttributeValue("Modified_Date", Modified_Date);
            child1.SetAttributeValue("Doc_No", txtDoc_No.Text != string.Empty ? txtDoc_No.Text : "0");

        }
        else
        {
            child1.SetAttributeValue("Created_By", Created_By);
            child1.SetAttributeValue("Created_Date", Created_Date);
        }
        root.Add(child1);
        #endregion-End of Head part Save

        #region save Head Master
        #region --------------------  Details --------------------
        for (int i = 0; i < grdDetail.Rows.Count; i++)
        {
            string CheckingFlag = string.Empty;
            XElement child2 = new XElement("DetailsResult");
            Int32 Detail_Id = Convert.ToInt32(grdDetail.Rows[i].Cells[2].Text);
            if (btnSave.Text != "Save")
            {
                if (grdDetail.Rows[i].Cells[Rowaction].Text == "A")//RowAction Value
                {
                    CheckingFlag = "1";
                }
                else if (grdDetail.Rows[i].Cells[Rowaction].Text == "U" || grdDetail.Rows[i].Cells[Rowaction].Text == "N")
                {
                    CheckingFlag = "2";
                }
                else
                {
                    CheckingFlag = "3";
                }
            }
            child2.SetAttributeValue("Doc_No", txtDoc_No.Text);
            child2.SetAttributeValue("Company_Code", Company_Code);
            child2.SetAttributeValue("Year_Code", Year_Code);
            child2.SetAttributeValue("Branch_Code", Branch_Code);
            child2.SetAttributeValue("Detail_Id", Server.HtmlDecode(grdDetail.Rows[i].Cells[2].Text));
            child2.SetAttributeValue("item_code", Server.HtmlDecode(grdDetail.Rows[i].Cells[Item_Code].Text));
            child2.SetAttributeValue("Export_Qty", Server.HtmlDecode(grdDetail.Rows[i].Cells[Qty].Text));
            child2.SetAttributeValue("Export_Rate", Server.HtmlDecode(grdDetail.Rows[i].Cells[Rate].Text));
            child2.SetAttributeValue("Export_Amount", Server.HtmlDecode(grdDetail.Rows[i].Cells[Value].Text));
            if (btnSave.Text != "Save")
            {
                child2.SetAttributeValue("Flag", CheckingFlag);
            }
            child1.Add(child2);
        }

        for (int i = 0; i < grdGenerate.Rows.Count; i++)
        {
            int Detail_Id = 0;
            int Code = 1;
            int Description = 2;
            int Strength = 3;
            int Print = 4;
            string CheckingFlag = string.Empty;
            XElement child3 = new XElement("Details");
            Detail_Id = Convert.ToInt32(grdGenerate.Rows[i].Cells[2].Text);
            if (btnSave.Text != "Save")
            {
                if (grdGenerate.Rows[i].Cells[9].Text == "A")//RowAction Value
                {
                    CheckingFlag = "1";
                }
                else if (grdGenerate.Rows[i].Cells[9].Text == "U" || grdGenerate.Rows[i].Cells[9].Text == "N")
                {
                    CheckingFlag = "2";
                }
                else
                {
                    CheckingFlag = "3";
                }
            }

            child3.SetAttributeValue("Doc_No", txtDoc_No.Text);
            child3.SetAttributeValue("Company_Code", Company_Code);
            child3.SetAttributeValue("Year_Code", Year_Code);
            child3.SetAttributeValue("Branch_Code", Branch_Code);

            child3.SetAttributeValue("Detail_Id", grdGenerate.Rows[i].Cells[2].Text);
            child3.SetAttributeValue("item_code", grdGenerate.Rows[i].Cells[3].Text);
            child3.SetAttributeValue("Inv_Qty", grdGenerate.Rows[i].Cells[5].Text);
            child3.SetAttributeValue("packing", Server.HtmlDecode(grdGenerate.Rows[i].Cells[6].Text));
            child3.SetAttributeValue("Inv_Value", Server.HtmlDecode(grdGenerate.Rows[i].Cells[7].Text));
            child3.SetAttributeValue("Invoice_No", Server.HtmlDecode(grdGenerate.Rows[i].Cells[8].Text));

            if (btnSave.Text != "Save")
            {
                child3.SetAttributeValue("Flag", CheckingFlag);

            }
            root.Add(child3);

        }

        #endregion
        string XMLReport = root.ToString();
        XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
        XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
        DataSet xml_ds = new DataSet();
        string spname = "SP_Export_Head";
        string xmlfile = XMLReport;
        string op = "";
        string returnmaxno = "";
        int flag;
        if (btnSave.Text == "Save")
        {
            #region[Insert]
            flag = 1;
            xml_ds = clsDAL.xmlExecuteDMLQrySP(spname, xmlfile, ref op, flag, ref returnmaxno);
            #endregion
        }
        else
        {
            #region[Update]
            flag = 6;
            xml_ds = clsDAL.xmlExecuteDMLQrySP(spname, xmlfile, ref op, flag, ref returnmaxno);
            #endregion
        }
        txtDoc_No.Text = returnmaxno;
        hdnf.Value = txtDoc_No.Text;
        retValue = op;

        if (retValue == "-1")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Successfully Added! No=" + returnmaxno + "');", true);
        }
        if (retValue == "-2" || retValue == "-3")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Successfully Updated! No=" + returnmaxno + "');", true);
        }
        clsButtonNavigation.enableDisable("S");
        this.enableDisableNavigateButtons();
        this.makeEmptyForm("S");
        qry = getDisplayQuery();
        this.fetchRecord(qry);
        #endregion
    }
    #endregion
    protected void grdGenerate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //try
        //{
        //    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
        //    int rowindex = row.RowIndex;
        //    if (e.CommandArgument == "lnk")
        //    {
        //        switch (e.CommandName)
        //        {
        //            case "EditRecord":
        //                if (grdGenerate.Rows[rowindex].Cells[RowactionResult].Text != "D" && grdGenerate.Rows[rowindex].Cells[RowactionResult].Text != "R")//add row action id
        //                {
        //                    pnlPopupDetails.Style["display"] = "none";
        //                    this.showDetailsRowResult(grdGenerate.Rows[rowindex]);
        //                    btnAdddetailsResult.Text = "UPDATE";
        //                    setFocusControl(txtScript_CodeResult);
        //                }
        //                break;
        //            case "DeleteRecord":
        //                string action = "";
        //                LinkButton lnkDeleteRessult = (LinkButton)e.CommandSource;
        //                if (lnkDeleteRessult.Text == "Delete")
        //                {
        //                    action = "Delete";
        //                    lnkDeleteRessult.Text = "Open";
        //                }
        //                else
        //                {
        //                    action = "Open";
        //                    lnkDeleteRessult.Text = "Delete";
        //                }
        //                this.DeleteDetailsRowResult(grdGenerate.Rows[rowindex], action);
        //                break;
        //        }
        //    }
        //}
        //catch
        //{
        //}
    }

    #region [grdGenerate_RowDataBound]
    protected void grdGenerate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //try
        //{
        //    // if (e.Row.RowType == DataControlRowType.DataRow)
        //    //{
        //    // e.Row.Cells[2].Width = new Unit("120px");
        //    e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
        //    e.Row.Cells[1].ControlStyle.Width = new Unit("40px");
        //    e.Row.Cells[2].ControlStyle.Width = new Unit("70px");
        //    e.Row.Cells[RowactionResult].ControlStyle.Width = new Unit("70px");
        //    e.Row.Cells[SrnoResult].ControlStyle.Width = new Unit("70px");
        //    //--------------------------------------------------
        //    e.Row.Cells[Sauda_TypeResult].ControlStyle.Width = new Unit("80px");
        //    e.Row.Cells[Sauda_TypeResult].Style["overflow"] = "hidden";
        //    e.Row.Cells[Sauda_TypeResult].HorizontalAlign = HorizontalAlign.Left;
        //    //--------------------------------------------------
        //    e.Row.Cells[Lot_SizeResult].ControlStyle.Width = new Unit("90px");
        //    e.Row.Cells[Lot_SizeResult].Style["overflow"] = "hidden";
        //    e.Row.Cells[Lot_SizeResult].HorizontalAlign = HorizontalAlign.Left;
        //    //--------------------------------------------------
        //    e.Row.Cells[Buy_QtyResult].ControlStyle.Width = new Unit("90px");
        //    e.Row.Cells[Buy_QtyResult].Style["overflow"] = "hidden";
        //    e.Row.Cells[Buy_QtyResult].HorizontalAlign = HorizontalAlign.Left;
        //    //--------------------------------------------------
        //    e.Row.Cells[Buy_RateResult].ControlStyle.Width = new Unit("90px");
        //    e.Row.Cells[Buy_RateResult].Style["overflow"] = "hidden";
        //    e.Row.Cells[Buy_RateResult].HorizontalAlign = HorizontalAlign.Left;
        //    //--------------------------------------------------
        //    e.Row.Cells[BuyStrike_PriceResult].ControlStyle.Width = new Unit("90px");
        //    e.Row.Cells[BuyStrike_PriceResult].Style["overflow"] = "hidden";
        //    e.Row.Cells[BuyStrike_PriceResult].HorizontalAlign = HorizontalAlign.Left;
        //    //--------------------------------------------------
        //    e.Row.Cells[Buy_ValueResult].ControlStyle.Width = new Unit("120px");
        //    e.Row.Cells[Buy_ValueResult].Style["overflow"] = "hidden";
        //    e.Row.Cells[Buy_ValueResult].HorizontalAlign = HorizontalAlign.Left;
        //    //--------------------------------------------------
        //    e.Row.Cells[Sale_QtyResult].ControlStyle.Width = new Unit("90px");
        //    e.Row.Cells[Sale_QtyResult].Style["overflow"] = "hidden";
        //    e.Row.Cells[Sale_QtyResult].HorizontalAlign = HorizontalAlign.Left;
        //    //--------------------------------------------------
        //    e.Row.Cells[Sale_RateResult].ControlStyle.Width = new Unit("90px");
        //    e.Row.Cells[Sale_RateResult].Style["overflow"] = "hidden";
        //    e.Row.Cells[Sale_RateResult].HorizontalAlign = HorizontalAlign.Left;
        //    //--------------------------------------------------
        //    e.Row.Cells[SaleStrike_PriceResult].ControlStyle.Width = new Unit("90px");
        //    e.Row.Cells[SaleStrike_PriceResult].Style["overflow"] = "hidden";
        //    e.Row.Cells[SaleStrike_PriceResult].HorizontalAlign = HorizontalAlign.Left;
        //    //--------------------------------------------------
        //    e.Row.Cells[Sale_ValueResult].ControlStyle.Width = new Unit("120px");
        //    e.Row.Cells[Sale_ValueResult].Style["overflow"] = "hidden";
        //    e.Row.Cells[Sale_ValueResult].HorizontalAlign = HorizontalAlign.Left;
        //    //--------------------------------------------------
        //    e.Row.Cells[Script_CodeResult].ControlStyle.Width = new Unit("50px");
        //    e.Row.Cells[Script_CodeResult].Style["overflow"] = "hidden";
        //    e.Row.Cells[Script_CodeResult].HorizontalAlign = HorizontalAlign.Left;

        //    e.Row.Cells[Script_Code_nameResult].ControlStyle.Width = new Unit("120px");
        //    e.Row.Cells[Script_Code_nameResult].Style["overflow"] = "hidden";
        //    e.Row.Cells[Script_Code_nameResult].HorizontalAlign = HorizontalAlign.Left;

        //    e.Row.Cells[Expiry_DateResult].ControlStyle.Width = new Unit("50px");
        //    e.Row.Cells[Expiry_DateResult].Style["overflow"] = "hidden";
        //    e.Row.Cells[Expiry_DateResult].HorizontalAlign = HorizontalAlign.Left;

        //    e.Row.Cells[Closing_Rate].ControlStyle.Width = new Unit("50px");
        //    e.Row.Cells[Closing_Rate].Style["overflow"] = "hidden";
        //    e.Row.Cells[Closing_Rate].HorizontalAlign = HorizontalAlign.Left;


        //    e.Row.Cells[Profit].ControlStyle.Width = new Unit("50px");
        //    e.Row.Cells[Profit].Style["overflow"] = "hidden";
        //    e.Row.Cells[Profit].HorizontalAlign = HorizontalAlign.Left;

        //    e.Row.Cells[Loss].ControlStyle.Width = new Unit("50px");
        //    e.Row.Cells[Loss].Style["overflow"] = "hidden";
        //    e.Row.Cells[Loss].HorizontalAlign = HorizontalAlign.Left;

        //    e.Row.Cells[Brokrage].ControlStyle.Width = new Unit("50px");
        //    e.Row.Cells[Brokrage].Style["overflow"] = "hidden";
        //    e.Row.Cells[Brokrage].HorizontalAlign = HorizontalAlign.Left;


        //    e.Row.Cells[STT].ControlStyle.Width = new Unit("50px");
        //    e.Row.Cells[STT].Style["overflow"] = "hidden";
        //    e.Row.Cells[STT].HorizontalAlign = HorizontalAlign.Left;

        //    e.Row.Cells[CGST].ControlStyle.Width = new Unit("50px");
        //    e.Row.Cells[CGST].Style["overflow"] = "hidden";
        //    e.Row.Cells[CGST].HorizontalAlign = HorizontalAlign.Left;

        //    e.Row.Cells[SGST].ControlStyle.Width = new Unit("50px");
        //    e.Row.Cells[SGST].Style["overflow"] = "hidden";
        //    e.Row.Cells[SGST].HorizontalAlign = HorizontalAlign.Left;

        //    e.Row.Cells[IGST].ControlStyle.Width = new Unit("50px");
        //    e.Row.Cells[IGST].Style["overflow"] = "hidden";
        //    e.Row.Cells[IGST].HorizontalAlign = HorizontalAlign.Left;
        //    //--------------------------------------------------
        //    //     e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
        //    //    e.Row.Cells[0].Style["overflow" ] = "hiden";
        //    //    e.Row.Cells[0].Visible =true;
        //    //}
        //    if (e.Row.Cells[Buy_QtyResult].Text == e.Row.Cells[Sale_QtyResult].Text)
        //    {
        //        e.Row.Cells[0].Enabled = false;
        //    }
        //    e.Row.Cells[1].Visible = false;
        //}
        //catch
        //{
        //}

        e.Row.Cells[0].ControlStyle.Width = new Unit("30px");
        e.Row.Cells[1].ControlStyle.Width = new Unit("40px");
        e.Row.Cells[2].ControlStyle.Width = new Unit("30px");
        e.Row.Cells[3].ControlStyle.Width = new Unit("50px");
        e.Row.Cells[4].ControlStyle.Width = new Unit("150px");
        e.Row.Cells[4].Style["overflow"] = "hidden";
        e.Row.Cells[5].ControlStyle.Width = new Unit("40px");
        e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[6].ControlStyle.Width = new Unit("70px");
        e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[7].ControlStyle.Width = new Unit("70px");
        e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[8].ControlStyle.Width = new Unit("70px");
        e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[9].ControlStyle.Width = new Unit("20px");
        e.Row.Cells[10].ControlStyle.Width = new Unit("20px");
        e.Row.Cells[0].Visible = true;
        e.Row.Cells[1].Visible = true;

    }
    #endregion

    #region [btnGO_Click]
    protected void btnGO_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string Doc_No = string.Empty;
        if (txtInv1.Text != string.Empty)
        {
            Doc_No = txtInv1.Text + ',';
        }
        if (txtInv2.Text != string.Empty)
        {
            Doc_No = Doc_No + txtInv2.Text + ',';
        }
        if (txtInv3.Text != string.Empty)
        {
            Doc_No = Doc_No + txtInv3.Text + ',';
        }
        if (txtInv4.Text != string.Empty)
        {
            Doc_No = Doc_No + txtInv4.Text + ',';
        }
        if (txtInv5.Text != string.Empty)
        {
            Doc_No = Doc_No + txtInv5.Text + ',';
        }
        if (txtInv6.Text != string.Empty)
        {
            Doc_No = Doc_No + txtInv6.Text + ',';
        }
        if (txtInv7.Text != string.Empty)
        {
            Doc_No = Doc_No + txtInv7.Text + ',';
        }
        if (txtInv8.Text != string.Empty)
        {
            Doc_No = Doc_No + txtInv8.Text + ',';
        }
        if (txtInv9.Text != string.Empty)
        {
            Doc_No = Doc_No + txtInv9.Text + ',';
        }
        if (txtInv10.Text != string.Empty)
        {
            Doc_No = Doc_No + txtInv10.Text + ',';
        }
        Doc_No = Doc_No.TrimEnd(',');

        #region grdGenerate
        if (ViewState["currentTableNew"] != null)
        {
            dt = (DataTable)ViewState["currentTableNew"];
            for (int i = 0; i < grdGenerate.Rows.Count; i++)
            {
                DataView view = new DataView(dt);
                DataTable distinctValues = view.ToTable(true, "Doc_No");
                string TypeGrid = Server.HtmlDecode(grdGenerate.Rows[i].Cells[8].Text);
                TypeGrid = TypeGrid.Trim();

                string NewDoc_No;
                NewDoc_No = TypeGrid.ToString();
                Doc_No = Doc_No.Replace(NewDoc_No, "0");

                if (TypeGrid == "0")
                {
                    TypeGrid = TypeGrid.Replace("0", "");
                }
                string chktype = TypeGrid;
                if (chktype != string.Empty)
                {
                    DataRow[] result = distinctValues.Select("Doc_No='" + chktype + "'");

                    if (result.Length > 0)
                    {
                        //string xxx = Convert.ToString(result[0][0]);
                        //xxx = xxx.Trim();
                        //if (chktype == xxx)
                        //{
                        //}
                        //NewDoc_No = (chktype.Length + 1);
                        //Doc_No = Doc_No.Substring(NewDoc_No);

                        continue;

                    }

                }
            }

            int count = grdGenerate.Rows.Count;
            count = count + 1;
           
            //qry = "select  '' as Detail_Id, Part_No as [Part No],Part_Name_E as [Part Name],Qty,Weight as [Net Kg],Value as Value,Doc_No from qryGstInvoice where Company_Code='"
            //    + Session["Company_Code"].ToString() + "' And Doc_No in(" + Doc_No + ") and Year_Code='" + Session["year"].ToString() + "' ";
            qry = "select ''as Detail_Id, item_code as [Item Code],itemname as [Item Name],Quantal as Qty,packing,item_Amount as Value,Doc_No " +
                  "from qrysaledetail where Company_Code='" + Session["Company_Code"].ToString() + "' And Doc_No in(" + Doc_No + ") and Year_Code='" + Session["year"].ToString() + "'";

            ds = clsDAL.SimpleQuery(qry);
            DataTable dtCopyc = new DataTable();
            DataTable dtCopycn = new DataTable();
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        dtCopyc = (DataTable)ViewState["currentTableNew"];
                        dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                        dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
                        //DataRow dr1 = null;
                        int rowIndex = 1;
                        int maxIndex = 0;
                        int[] index = new int[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            index[i] = dt.Rows[i]["Detail_ID"].ToString() != string.Empty ? Convert.ToInt32(dt.Rows[i]["Detail_ID"].ToString()) : 0;
                        }
                        if (index.Length > 0)
                        {
                            for (int i = 0; i < index.Length; i++)
                            {
                                if (index[i] > maxIndex)
                                {
                                    maxIndex = index[i];

                                    rowIndex = maxIndex + 1;
                                    dt.Rows[i]["Detail_ID"] = rowIndex;
                                }
                                maxIndex = dtCopyc.Rows.Count;
                                maxIndex += 1;
                                rowIndex = maxIndex;
                                dt.Rows[i]["Detail_ID"] = rowIndex;
                                dt.Rows[i]["rowAction"] = "A";
                                dt.Rows[i]["SrNo"] = dtCopyc.Rows.Count + 1;
                                DataRow dr1 = dtCopyc.NewRow();

                                dr1["Detail_ID"] = dt.Rows[i]["Detail_ID"].ToString();
                                dr1["Item Code"] = dt.Rows[i]["Item Code"].ToString();
                                dr1["Item Name"] = dt.Rows[i]["Item Name"].ToString();
                                dr1["Qty"] = dt.Rows[i]["Qty"].ToString();
                                dr1["packing"] = dt.Rows[i]["packing"].ToString();
                                dr1["Value"] = dt.Rows[i]["Value"].ToString();
                                dr1["Doc_No"] = dt.Rows[i]["Doc_No"].ToString();
                                dr1["rowAction"] = dt.Rows[i]["rowAction"].ToString();
                                dr1["SrNo"] = dt.Rows[i]["SrNo"].ToString();
                                dtCopyc.Rows.Add(dr1);
                            }

                        }
                        else
                        {
                            rowIndex = maxIndex;          //1
                        }


                        grdGenerate.DataSource = dtCopyc;
                        grdGenerate.DataBind();
                        ViewState["currentTableNew"] = dtCopyc;
                    }
                    else
                    {
                        grdGenerate.DataSource = null;
                        grdGenerate.DataBind();
                        ViewState["currentTableNew"] = null;
                    }
                }
                else
                {
                    grdGenerate.DataSource = null;
                    grdGenerate.DataBind();

                    ViewState["currentTableNew"] = null;
                }
            }
            else
            {
                grdGenerate.DataSource = null;
                grdGenerate.DataBind();
                ViewState["currentTableNew"] = null;
            }
            #region GridNo 2 insert
            qry = "select ''as Detail_Id,item_code as [Item Code],itemname as [Item Name],sum(Quantal) as Qty,'0' as Rate,'0'Value from qrysaledetail where Company_Code='"
           + Session["Company_Code"].ToString() + "' And Doc_No in(" + Doc_No + ") and Year_Code='" + Session["year"].ToString() + "' group by item_code,itemname ";
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        dtCopycn = (DataTable)ViewState["currentTable"];
                        dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                        dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
                        //DataRow dr1 = null;
                        int rowIndex = 1;
                        int maxIndex = 0;
                        int[] index = new int[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            index[i] = dt.Rows[i]["Detail_ID"].ToString() != string.Empty ? Convert.ToInt32(dt.Rows[i]["Detail_ID"].ToString()) : 0;
                        }
                        if (index.Length > 0)
                        {
                            for (int i = 0; i < index.Length; i++)
                            {
                                if (index[i] > maxIndex)
                                {
                                    maxIndex = index[i];

                                    rowIndex = maxIndex + 1;
                                    dt.Rows[i]["Detail_ID"] = rowIndex;
                                }
                                maxIndex = dtCopycn.Rows.Count;
                                maxIndex += 1;
                                rowIndex = maxIndex;
                                dt.Rows[i]["Detail_ID"] = rowIndex;
                                dt.Rows[i]["rowAction"] = "A";
                                dt.Rows[i]["SrNo"] = dtCopycn.Rows.Count + 1;
                                DataRow dr1 = dtCopycn.NewRow();

                                dr1["Detail_Id"] = dt.Rows[i]["Detail_Id"].ToString();
                                dr1["Item Code"] = dt.Rows[i]["Item Code"].ToString();
                                dr1["Item Name"] = dt.Rows[i]["Item Name"].ToString();
                                dr1["Qty"] = dt.Rows[i]["Qty"].ToString();
                                dr1["Rate"] = dt.Rows[i]["Rate"].ToString();
                                dr1["Value"] = dt.Rows[i]["Value"].ToString();

                                dr1["rowAction"] = dt.Rows[i]["rowAction"].ToString();
                                dr1["SrNo"] = dt.Rows[i]["SrNo"].ToString();
                                dtCopycn.Rows.Add(dr1);
                            }

                        }
                        else
                        {
                            rowIndex = maxIndex;          //1
                        }


                        grdDetail.DataSource = dtCopycn;
                        grdDetail.DataBind();
                        ViewState["currentTable"] = dtCopycn; ;
                    }
                    else
                    {
                        grdDetail.DataSource = null;
                        grdDetail.DataBind();
                        ViewState["currentTable"] = null;
                    }
                }
                else
                {
                    grdDetail.DataSource = null;
                    grdDetail.DataBind();
                    ViewState["currentTable"] = null;
                }
            }
            else
            {
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                ViewState["currentTable"] = null;
            }
            #endregion
        }
        else
        {
            //qry = "select ''as Detail_Id, Part_No as [Part No],Part_Name_E as [Part Name],Qty,Weight as [Net Kg],Value as Value,Doc_No from qryGstInvoice where Company_Code='"
            //    + Session["Company_Code"].ToString() + "' And Doc_No in(" + Doc_No + ") and Year_Code='" + Session["year"].ToString() + "'";
            qry = "select ''as Detail_Id, item_code as [Item Code],itemname as [Item Name],Quantal as Qty,packing,item_Amount as Value,Doc_No " +
                   " from qrysaledetail where Company_Code='" + Session["Company_Code"].ToString() + "' And Doc_No in(" + Doc_No + ") and Year_Code='" + Session["year"].ToString() + "' ";
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                        dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
                        DataRow dr = null;
                        int rowIndex = 1;
                        int maxIndex = 0;
                        int[] index = new int[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            index[i] = dt.Rows[i]["Detail_ID"].ToString() != string.Empty ? Convert.ToInt32(dt.Rows[i]["Detail_ID"].ToString()) : 0;
                        }
                        if (index.Length > 0)
                        {
                            for (int i = 0; i < index.Length; i++)
                            {
                                if (index[i] > maxIndex)
                                {
                                    maxIndex = index[i];

                                    rowIndex = maxIndex + 1;
                                    dt.Rows[i]["Detail_ID"] = rowIndex;
                                }
                                maxIndex += 1;
                                rowIndex = maxIndex;
                                dt.Rows[i]["Detail_ID"] = rowIndex;
                                dt.Rows[i]["rowAction"] = "A";
                                dt.Rows[i]["SrNo"] = i + 1;
                            }

                        }
                        else
                        {
                            rowIndex = maxIndex;          //1
                        }


                        grdGenerate.DataSource = dt;
                        grdGenerate.DataBind();
                        ViewState["currentTableNew"] = dt;
                    }
                    else
                    {
                        grdGenerate.DataSource = null;
                        grdGenerate.DataBind();
                        ViewState["currentTableNew"] = null;
                    }
                }
                else
                {
                    grdGenerate.DataSource = null;
                    grdGenerate.DataBind();

                    ViewState["currentTableNew"] = null;
                }
            }
            else
            {
                grdGenerate.DataSource = null;
                grdGenerate.DataBind();
                ViewState["currentTableNew"] = null;
            }

            #region GridNo 2 update
            qry = "select ''as Detail_Id,item_code as [Item Code],itemname as [Item Name],sum(Quantal) as Qty,'0' as Rate,'0'Value from qrysaledetail where Company_Code='"
            + Session["Company_Code"].ToString() + "' And Doc_No in(" + Doc_No + ") and Year_Code='" + Session["year"].ToString() + "' group by item_code,itemname ";
            ds = clsDAL.SimpleQuery(qry);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                        dt.Columns.Add(new DataColumn("SrNo", typeof(int)));
                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    dt.Rows[i]["rowAction"] = "N";
                        //    dt.Rows[i]["SrNo"] = i + 1;
                        //}


                        int rowIndex = 1;
                        int maxIndex = 0;
                        int[] index = new int[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            index[i] = dt.Rows[i]["Detail_ID"].ToString() != string.Empty ? Convert.ToInt32(dt.Rows[i]["Detail_ID"].ToString()) : 0;
                        }
                        if (index.Length > 0)
                        {
                            for (int i = 0; i < index.Length; i++)
                            {
                                if (index[i] > maxIndex)
                                {
                                    maxIndex = index[i];

                                    rowIndex = maxIndex + 1;
                                    dt.Rows[i]["Detail_ID"] = rowIndex;
                                }
                                maxIndex += 1;
                                rowIndex = maxIndex;
                                dt.Rows[i]["Detail_ID"] = rowIndex;
                                dt.Rows[i]["rowAction"] = "N";
                                dt.Rows[i]["SrNo"] = i + 1;
                            }

                        }
                        else
                        {
                            rowIndex = maxIndex;          //1
                        }


                        grdDetail.DataSource = dt;
                        grdDetail.DataBind();
                        ViewState["currentTable"] = dt;
                    }
                    else
                    {
                        grdDetail.DataSource = null;
                        grdDetail.DataBind();
                        ViewState["currentTable"] = null;
                    }
                }
                else
                {
                    grdDetail.DataSource = null;
                    grdDetail.DataBind();
                    ViewState["currentTable"] = null;
                }
            }
            else
            {
                grdDetail.DataSource = null;
                grdDetail.DataBind();
                ViewState["currentTable"] = null;
            }
            #endregion
        }
        #endregion

        
        csCalculations();

    }
    #endregion
    #region calculation
    private void Calculation()
    {
        double qty = 0.00;
        double rate = 0.00;
        double value = 0.00;
        qty = Convert.ToDouble(txtQty.Text);
        rate = Convert.ToDouble(txtRate.Text);
        value = qty * rate;
        txtValue.Text = value.ToString();
    }
    #endregion
}

