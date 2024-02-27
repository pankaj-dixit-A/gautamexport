using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
public partial class pgeExport_Invoice : System.Web.UI.Page
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
    int Detail_ID = 2;
    string Container_No = string.Empty;
    int Details = 4;
    int Item_Code = 5;
    int Qty = 7;
    int Weight = 8;
    int Net_Wt = 9;
    int Gross_Wt = 10;
    int Item_Rate = 11;
    int Item_Value = 12;
    int Rowaction = 13;
    int Srno = 14;
    string qryAccountList = string.Empty;
    string SystemMasterTable = string.Empty;
    #endregion
    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "Export_InvHead";
            tblDetails = "Export_InvDetail";
            qryCommon = "qryExportInvoice";
            user = Session["user"].ToString();
            pnlPopup.Style["display"] = "none";
            qryAccountList = "qrymstaccountmaster";
            SystemMasterTable = tblPrefix + "SystemMaster";
            string Doc = Request.QueryString["Doc_No"];
            string Action = Request.QueryString["Action"];

            if (!Page.IsPostBack)
            {
                isAuthenticate = Security.Authenticate(tblPrefix, user);
                string User_Type = clsCommon.getString("Select User_Type from tblUser WHERE User_Name='" + user + "'");
                if (isAuthenticate == "1" || User_Type == "A")
                {
                    if (Action == "1")
                    {
                        hdnf.Value = Doc;

                        pnlPopup.Style["display"] = "none";
                        ViewState["currentTable"] = null;
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
                btnCarryForword.Enabled = false;
                btnClosedetails.Enabled = false;
                btnOpenDetailsPopup.Enabled = false;
                grdDetail.DataSource = null; grdDetail.DataBind();
                ViewState["currentTable"] = null;
                // ViewState["currentTable"] = null;
                btnSave.Text = "Save";
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtOrder_No.Enabled = false;
                txtShip_To.Enabled = false;
                txtConsign.Enabled = false;
                txtFinal_Dest.Enabled = false;
                txtPer_Carriage.Enabled = false;
                txtPlace_Of_Receipt.Enabled = false;
                txtExportDelaration.Enabled = false;
                txtcountryorigin.Enabled = false;
                txtMAEQ.Enabled = false;
                drpDrawBack.Enabled = false;
                drpCurrencytype.Enabled = false;
                txtGstRate.Enabled = false;
                txtVessel.Enabled = false;
                txtPort_Of_Loading.Enabled = false;
                txtPort_Of_Discharage.Enabled = false;
                txtFinal_Destination.Enabled = false;
                txtTerms.Enabled = false;
                txtCategory.Enabled = false;
                txtNo_Of_Box.Enabled = false;
                txtToo.Enabled = false;
                txtRITC_Code.Enabled = false;
                txtIE_Code.Enabled = false;
                txtNet_Wt_Incr.Enabled = false;
                txtKind_Attention.Enabled = false;
                txtTele_No.Enabled = false;
                txtEuro_Rate.Enabled = false;
                txtOur_Inv_Rmk.Enabled = false;
                txtAmount_In_Rs.Enabled = false;
                txtLUT_No.Enabled = false;
                txtBox_No.Enabled = false;
                txtBox_Size.Enabled = false;
                txtPart_no.Enabled = false;
                btntxtPart_no.Enabled = false;
                txtQty.Enabled = false;
                txtWeight.Enabled = false;
                txtNet_Wt.Enabled = false;
                txtGross_Wt.Enabled = false;
                txtItem_Rate.Enabled = false;
                txtItem_Value.Enabled = false;

                txtShipTo_Code.Enabled = false;
                btntxtShipToCode.Enabled = false;
                txtConsign_code.Enabled = false;
                btntxtConsigncode.Enabled = false;
                txtConsign.Enabled = false;
                drpDrawBack.Enabled = false;
                drpCurrencytype.Enabled = false;
                txtGstRate.Enabled = false;
                btntxtGstRate.Enabled = false;
                btnBack.Enabled = true;
                setFocusControl(btnAdd);
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
                txtEditDoc_No.Enabled = false;
                btnCarryForword.Enabled = true;
                txtDoc_No.Enabled = false;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                ViewState["currentTable"] = null;
                grdDetail.DataSource = null; grdDetail.DataBind();
                //ViewState["currentTable"] = null;
                txtBox_No.Enabled = true;
                txtBox_Size.Enabled = true;
                txtPart_no.Enabled = true;
                lblPart_no.Text = string.Empty;
                btntxtPart_no.Enabled = true;
                txtShipTo_Code.Enabled = true;
                btntxtShipToCode.Enabled = true;
                txtConsign_code.Enabled = true;
                txtConsign.Enabled = true;
                btntxtConsigncode.Enabled = true;
                drpDrawBack.Enabled = true;
                drpCurrencytype.Enabled = true;
                txtGstRate.Enabled = true;
                btntxtGstRate.Enabled = true;
                txtQty.Enabled = true;
                txtWeight.Enabled = true;
                txtNet_Wt.Enabled = true;
                txtGross_Wt.Enabled = true;
                txtItem_Rate.Enabled = true;
                txtItem_Value.Enabled = true;
                txtDoc_Date.Enabled = true;
                txtDoc_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                txtOrder_No.Enabled = true;
                txtShip_To.Enabled = true;
                txtConsign.Enabled = true;
                txtFinal_Dest.Enabled = true;
                txtPer_Carriage.Enabled = true;
                txtPlace_Of_Receipt.Enabled = true;
                txtExportDelaration.Enabled = true;
                txtcountryorigin.Enabled = true;
                txtMAEQ.Enabled = true;
                txtVessel.Enabled = true;
                txtPort_Of_Loading.Enabled = true;
                txtPort_Of_Discharage.Enabled = true;
                txtFinal_Destination.Enabled = true;
                txtTerms.Enabled = true;
                txtCategory.Enabled = true;
                txtNo_Of_Box.Enabled = true;
                txtToo.Enabled = true;
                txtRITC_Code.Enabled = true;
                txtIE_Code.Enabled = true;
                txtNet_Wt_Incr.Enabled = true;
                txtKind_Attention.Enabled = true;
                txtTele_No.Enabled = true;
                txtEuro_Rate.Enabled = true;
                txtOur_Inv_Rmk.Enabled = true;
                txtAmount_In_Rs.Enabled = true;
                txtLUT_No.Enabled = true;

                btnBack.Enabled = false;
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
                btnCarryForword.Enabled = false;
                btntxtDoc_No.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtOrder_No.Enabled = false;
                txtShip_To.Enabled = false;
                txtConsign.Enabled = false;
                txtFinal_Dest.Enabled = false;
                txtPer_Carriage.Enabled = false;
                txtPlace_Of_Receipt.Enabled = false;
                txtExportDelaration.Enabled = false;
                txtcountryorigin.Enabled = false;
                txtMAEQ.Enabled = false;
                txtVessel.Enabled = false;
                txtPort_Of_Loading.Enabled = false;
                txtPort_Of_Discharage.Enabled = false;
                txtFinal_Destination.Enabled = false;
                txtTerms.Enabled = false;
                txtCategory.Enabled = false;
                txtNo_Of_Box.Enabled = false;
                txtToo.Enabled = false;
                txtRITC_Code.Enabled = false;
                txtIE_Code.Enabled = false;
                txtNet_Wt_Incr.Enabled = false;
                txtKind_Attention.Enabled = false;
                txtTele_No.Enabled = false;
                txtEuro_Rate.Enabled = false;
                txtOur_Inv_Rmk.Enabled = false;
                txtAmount_In_Rs.Enabled = false;
                txtLUT_No.Enabled = false;
                txtBox_No.Enabled = false;
                txtBox_Size.Enabled = false;
                txtPart_no.Enabled = false;
                btntxtPart_no.Enabled = false;
                txtShipTo_Code.Enabled = false;
                btntxtShipToCode.Enabled = false;
                btntxtConsigncode.Enabled = false;
                txtConsign_code.Enabled = false;
                drpDrawBack.Enabled = false;
                drpCurrencytype.Enabled = false;
                txtGstRate.Enabled = false;
                btntxtGstRate.Enabled = false;
                txtConsign.Enabled = false;
                txtQty.Enabled = false;
                txtWeight.Enabled = false;
                txtNet_Wt.Enabled = false;
                txtGross_Wt.Enabled = false;
                txtItem_Rate.Enabled = false;
                txtItem_Value.Enabled = false;
                txtBox_No.Text = string.Empty;
                txtBox_Size.Text = string.Empty;
                txtPart_no.Text = string.Empty;
                btntxtPart_no.Enabled = false;
                txtQty.Text = string.Empty;
                txtWeight.Text = string.Empty;
                txtNet_Wt.Text = string.Empty;
                txtGross_Wt.Text = string.Empty;
                txtItem_Rate.Text = string.Empty;
                txtItem_Value.Text = string.Empty;
                btnAdddetails.Text = "ADD";
                btnAdddetails.Enabled = false;
                btnClosedetails.Enabled = false;
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
                btnCarryForword.Enabled = false;
                btntxtDoc_No.Enabled = true;
                txtEditDoc_No.Enabled = false;
                lblMsg.Text = string.Empty;
                txtDoc_Date.Enabled = true;
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                txtOrder_No.Enabled = true;
                txtShip_To.Enabled = true;
                txtConsign.Enabled = true;
                txtFinal_Dest.Enabled = true;
                txtPer_Carriage.Enabled = true;
                txtPlace_Of_Receipt.Enabled = true;
                txtExportDelaration.Enabled = true;
                txtcountryorigin.Enabled = true;
                txtMAEQ.Enabled = true;
                txtVessel.Enabled = true;
                txtPort_Of_Loading.Enabled = true;
                txtPort_Of_Discharage.Enabled = true;
                txtFinal_Destination.Enabled = true;
                txtTerms.Enabled = true;
                txtCategory.Enabled = true;
                txtNo_Of_Box.Enabled = true;
                txtToo.Enabled = true;
                txtRITC_Code.Enabled = true;
                txtIE_Code.Enabled = true;
                txtNet_Wt_Incr.Enabled = true;
                txtKind_Attention.Enabled = true;
                txtTele_No.Enabled = true;
                txtEuro_Rate.Enabled = true;
                txtOur_Inv_Rmk.Enabled = true;
                txtAmount_In_Rs.Enabled = true;
                txtLUT_No.Enabled = true;
                txtBox_No.Enabled = true;
                txtBox_Size.Enabled = true;
                txtPart_no.Enabled = true;
                btntxtPart_no.Enabled = true;
                txtQty.Enabled = true;
                txtWeight.Enabled = true;
                txtNet_Wt.Enabled = true;
                txtGross_Wt.Enabled = true;
                txtItem_Rate.Enabled = true;
                txtItem_Value.Enabled = true;
                btnAdddetails.Enabled = true;
                btnClosedetails.Enabled = true;
                btnBack.Enabled = false;
                txtShipTo_Code.Enabled = true;
                btntxtShipToCode.Enabled = true;
                btntxtConsigncode.Enabled = true;
                txtConsign_code.Enabled = true;
                txtConsign.Enabled = true;
                drpDrawBack.Enabled = true;
                drpCurrencytype.Enabled = true;
                txtGstRate.Enabled = true;
                btntxtGstRate.Enabled = true;
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
            qry = "select max(Doc_No) as Doc_No from " + tblHead + " where  Year_Code='" + Convert.ToInt32(Session["year"].ToString()) +
                "' and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
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
        //query = "select count(*) from " + tblHead + " where Company_Code='" + Session["Company_Code"].ToString() + "'";
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
        //    query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No>" + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY Doc_No asc  ";
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
        //    query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No<" + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY Doc_No desc  ";
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
        //}
        //    #endregion
        #endregion

        #region enable disable previous next buttons
        int RecordCount = 0;
        string query = "";
        query = "select count(*) from " + tblHead + " where   Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());


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
                " ORDER BY Doc_No asc  ";
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
                " ORDER BY Doc_No desc  ";
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
            query = "select Doc_No from " + tblHead + " where Doc_No=(select MIN(Doc_No) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "')";
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
                query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No< " + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY Doc_No desc  ";
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
                query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No> " + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY Doc_No asc  ";
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
            query = "select Doc_No from " + tblHead + " where Doc_No=(select MAX(Doc_No) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "')";
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
        string check = txtEditDoc_No.Text;
        if (check == string.Empty)
        {
            clsButtonNavigation.enableDisable("A");
            ViewState["mode"] = null;
            ViewState["mode"] = "I";
            this.makeEmptyForm("A");
            pnlPopupDetails.Style["display"] = "none";
            pnlgrdDetail.Enabled = true;

            string qry = "select isnull(max(Doc_No),0) as Doc_No from " + tblHead +
          " where  Year_Code=" + Convert.ToString(Session["year"]).ToString()
                     + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"]).ToString();
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
            txtExportDelaration.Text = clsCommon.getString("select ExportDelaration from tblvoucherheadaddress where  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " ");
            txtDoc_No.Text = Convert.ToString(Doc_No);
            setFocusControl(txtDoc_Date);
        }
        else
        {
            btnCancel_Click(this, new EventArgs());
        }

    }
    #endregion

    #region [btnCarryForword_Click]
    protected void btnCarryForword_Click(object sender, EventArgs e)
    {


        //string carrydata = "select * from " + tblHead + " where Doc_No=(select MAX(Doc_No) from " + tblHead
        //    + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString())
        //    + "' and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "')";

        Int32 MaxNo = Convert.ToInt32(clsDAL.GetString("select  isnull(MAX(Doc_No),0) from " + tblHead
          + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString())
          + "' and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "'"));

        string carrydata = "select * from " + tblHead + " where Doc_No='" + MaxNo + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString())
         + "' and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "'";



        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        ds = clsDAL.SimpleQuery(carrydata);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {

                    //   txtDoc_No.Text = dt.Rows[0]["Doc_No"].ToString();

                    txtDoc_Date.Text = dt.Rows[0]["Doc_Date"].ToString();
                    //txtOrder_No.Text = dt.Rows[0]["Order_No"].ToString();
                    txtShip_To.Text = dt.Rows[0]["Ship_To"].ToString();
                    txtConsign.Text = dt.Rows[0]["Consign"].ToString();
                    txtFinal_Dest.Text = dt.Rows[0]["Final_Dest"].ToString();
                    txtPer_Carriage.Text = dt.Rows[0]["Per_Carriage"].ToString();
                    txtPlace_Of_Receipt.Text = dt.Rows[0]["Place_Of_Receipt"].ToString();
                    txtVessel.Text = dt.Rows[0]["Vessel"].ToString();
                    txtPort_Of_Loading.Text = dt.Rows[0]["Port_Of_Loading"].ToString();
                    txtPort_Of_Discharage.Text = dt.Rows[0]["Port_Of_Discharage"].ToString();
                    txtFinal_Destination.Text = dt.Rows[0]["Final_Destination"].ToString();
                    txtTerms.Text = dt.Rows[0]["Terms"].ToString();
                    txtCategory.Text = dt.Rows[0]["Category"].ToString();
                    //txtNo_Of_Box.Text = dt.Rows[0]["No_Of_Box"].ToString();
                    txtToo.Text = dt.Rows[0]["Too"].ToString();
                    txtRITC_Code.Text = dt.Rows[0]["RITC_Code"].ToString();
                    txtIE_Code.Text = dt.Rows[0]["IE_Code"].ToString();
                    txtNet_Wt_Incr.Text = dt.Rows[0]["Net_Wt_Incr"].ToString();
                    txtKind_Attention.Text = dt.Rows[0]["Kind_Attention"].ToString();
                    txtTele_No.Text = dt.Rows[0]["Tele_No"].ToString();
                    txtEuro_Rate.Text = dt.Rows[0]["Euro_Rate"].ToString();
                    txtOur_Inv_Rmk.Text = dt.Rows[0]["Our_Inv_Rmk"].ToString();
                    //txtAmount_In_Rs.Text = dt.Rows[0]["Amount_In_Rs"].ToString();
                    txtLUT_No.Text = dt.Rows[0]["LUT_No"].ToString();
                    txtShipTo_Code.Text = dt.Rows[0]["ShipTo_Code"].ToString();
                    txtConsign_code.Text = dt.Rows[0]["Consign_Code"].ToString();
                    hdnfshipto.Value = dt.Rows[0]["ShipTo_Id"].ToString();
                    hdnfconsign.Value = dt.Rows[0]["Consign_ID"].ToString();
                    txtExportDelaration.Text = dt.Rows[0]["ExportDelaration"].ToString();
                    txtcountryorigin.Text = dt.Rows[0]["countryoforigin"].ToString();
                    txtMAEQ.Text = dt.Rows[0]["MAEQ"].ToString();
                    string DrawBack = dt.Rows[0]["DrawBack"].ToString();
                    drpDrawBack.Text = DrawBack;
                    txtGstRate.Text = dt.Rows[0]["GstRateCode"].ToString();
                   // lblGstRateName.Text = dt.Rows[0]["GST_Name"].ToString();
                    string Currencytype = dt.Rows[0]["Current_Type"].ToString();
                    drpCurrencytype.SelectedValue = Currencytype;
                }
            }
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
        setFocusControl(txtDoc_No);
    }
    #endregion

    #region [btnDelete_Click]
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdconfirm.Value == "Yes")
            {
                //int userid = Convert.ToInt32(Session["User_Id"].ToString());
                //string pagevalidation = clsCommon.getString("Select Permission from tblUser_Detail where Tran_Type ='EI' and User_Id=" + userid + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"]).ToString());
                //if (pagevalidation == "Y")
                //{

                //}
                //else
                //{
                //    //setFocusControl(txtDoc_Date);
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('You are Not Authorize Person to Delete Record please Contact Admistrator!!!!!');", true);
                //    return;
                //}
                string str = string.Empty;
                if (str == string.Empty)
                {
                    string currentDoc_No = txtDoc_No.Text;
                    DataSet ds = new DataSet();
                    string strrev = "";
                    using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                    {
                        obj.flag = 3;
                        obj.tableName = tblHead;
                        obj.columnNm = "Doc_No=" + currentDoc_No + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                            + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                        obj.values = "none";
                        ds = obj.insertAccountMaster(ref strrev);
                    }
                    using (clsUniversalInsertUpdateDelete obj = new clsUniversalInsertUpdateDelete())
                    {
                        obj.flag = 3;
                        obj.tableName = tblDetails;
                        obj.columnNm = "Doc_No=" + currentDoc_No + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                            + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                        obj.values = "none";
                        ds = obj.insertAccountMaster(ref strrev);
                    }
                    string query = "";
                    if (strrev == "-3")
                    {
                        query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No>" + Convert.ToInt32(currentDoc_No) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY Doc_No asc  ";
                        hdnf.Value = clsCommon.getString(query);
                        if (hdnf.Value == string.Empty)
                        {
                            query = "SELECT top 1 [Doc_No] from " + tblHead + " where Doc_No<" + Convert.ToInt32(currentDoc_No) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY Doc_No desc  ";
                            hdnf.Value = clsCommon.getString(query);
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
                                lblModifiedDate.Text = "";
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
                        txtOrder_No.Text = dt.Rows[0]["Order_No"].ToString();
                        txtShip_To.Text = dt.Rows[0]["Ship_To"].ToString();
                        txtConsign.Text = dt.Rows[0]["Consign"].ToString();
                        txtFinal_Dest.Text = dt.Rows[0]["Final_Dest"].ToString();
                        txtPer_Carriage.Text = dt.Rows[0]["Per_Carriage"].ToString();
                        txtPlace_Of_Receipt.Text = dt.Rows[0]["Place_Of_Receipt"].ToString();
                        txtVessel.Text = dt.Rows[0]["Vessel"].ToString();
                        txtPort_Of_Loading.Text = dt.Rows[0]["Port_Of_Loading"].ToString();
                        txtPort_Of_Discharage.Text = dt.Rows[0]["Port_Of_Discharage"].ToString();
                        txtFinal_Destination.Text = dt.Rows[0]["Final_Destination"].ToString();
                        txtTerms.Text = dt.Rows[0]["Terms"].ToString();
                        txtCategory.Text = dt.Rows[0]["Category"].ToString();
                        txtNo_Of_Box.Text = dt.Rows[0]["No_Of_Box"].ToString();
                        txtToo.Text = dt.Rows[0]["Too"].ToString();
                        txtRITC_Code.Text = dt.Rows[0]["RITC_Code"].ToString();
                        txtIE_Code.Text = dt.Rows[0]["IE_Code"].ToString();
                        txtNet_Wt_Incr.Text = dt.Rows[0]["Net_Wt_Incr"].ToString();
                        txtKind_Attention.Text = dt.Rows[0]["Kind_Attention"].ToString();
                        txtTele_No.Text = dt.Rows[0]["Tele_No"].ToString();
                        txtEuro_Rate.Text = dt.Rows[0]["Euro_Rate"].ToString();
                        txtOur_Inv_Rmk.Text = dt.Rows[0]["Our_Inv_Rmk"].ToString();
                        txtAmount_In_Rs.Text = dt.Rows[0]["Amount_In_Rs"].ToString();
                        txtLUT_No.Text = dt.Rows[0]["LUT_No"].ToString();
                        //txtCurrent_type.Text = dt.Rows[0]["Current_Type"].ToString();
                        string Currencytype = dt.Rows[0]["Current_Type"].ToString();
                        drpCurrencytype.SelectedValue = Currencytype;
                        txtCurrencyInWord.Text = dt.Rows[0]["Current_In_Word"].ToString();
                        txtShipTo_Code.Text = dt.Rows[0]["ShipTo_Code"].ToString();
                        txtConsign_code.Text = dt.Rows[0]["Consign_Code"].ToString();
                        hdnfshipto.Value = dt.Rows[0]["ShipTo_Id"].ToString();
                        hdnfconsign.Value = dt.Rows[0]["Consign_ID"].ToString();
                        txtExportDelaration.Text = dt.Rows[0]["ExportDelaration"].ToString();
                        txtcountryorigin.Text = dt.Rows[0]["countryoforigin"].ToString();
                        txtMAEQ.Text = dt.Rows[0]["MAEQ"].ToString();
                        string DrawBack = dt.Rows[0]["DrawBack"].ToString();
                        drpDrawBack.SelectedValue = DrawBack;
                        txtGstRate.Text = dt.Rows[0]["GstRateCode"].ToString();
                        lblGstRateName.Text = dt.Rows[0]["GST_Name"].ToString();
                        recordExist = true;
                        lblMsg.Text = "";
                        #region Details
                        qry = "select [Detail_ID] ,[Container_No],[Details],[Item_Code],[System_Name_E] as Item_Name,[Qty] ,[Weight] ,[Net_Wt] ,[Gross_Wt]," +
                        "[Item_Rate],[Item_Value]  from " + qryCommon + "  where Detail_ID is not null and Company_Code='"
                            + Session["Company_Code"].ToString() + "' And Doc_No='" + txtDoc_No.Text
                            + "' and Year_Code='" + Session["year"].ToString() + "' order by Container_No ";
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
            if (strTextBox == "txtEditDoc_No")
            {
                setFocusControl(txtEditDoc_No);
            }
            if (strTextBox == "txtDoc_No")
            {
                setFocusControl(txtDoc_No);
            }
            if (strTextBox == "txtDoc_Date")
            {
                try
                {
                    string dt = DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("dd/MM/yyyy");
                    if (clsCommon.isValidDateforOp(dt) == true)
                    {
                        setFocusControl(txtFinal_Dest);
                    }
                    else
                    {
                        txtDoc_Date.Text = "";
                        setFocusControl(txtDoc_Date);
                    }
                }
                catch
                {
                    txtDoc_Date.Text = "";
                    setFocusControl(txtDoc_Date);
                }
            }
            if (strTextBox == "txtOrder_No")
            {
                setFocusControl(txtOrder_No);
            }
            if (strTextBox == "txtShip_To")
            {
                setFocusControl(txtShip_To);
            }
            if (strTextBox == "txtConsign")
            {
                setFocusControl(txtConsign);
            }
            if (strTextBox == "txtFinal_Dest")
            {
                setFocusControl(txtFinal_Dest);
            }
            if (strTextBox == "txtPer_Carriage")
            {
                setFocusControl(txtPer_Carriage);
            }
            if (strTextBox == "txtPlace_Of_Receipt")
            {
                setFocusControl(txtPlace_Of_Receipt);
            }
            if (strTextBox == "txtVessel")
            {
                setFocusControl(txtVessel);
            }
            if (strTextBox == "txtPort_Of_Loading")
            {
                setFocusControl(txtPort_Of_Loading);
            }
            if (strTextBox == "txtPort_Of_Discharage")
            {
                setFocusControl(txtPort_Of_Discharage);
            }
            if (strTextBox == "txtFinal_Destination")
            {
                setFocusControl(txtFinal_Destination);
            }
            if (strTextBox == "txtTerms")
            {
                setFocusControl(txtTerms);
            }
            if (strTextBox == "txtCategory")
            {
                setFocusControl(txtCategory);
            }
            if (strTextBox == "txtNo_Of_Box")
            {
                setFocusControl(txtNo_Of_Box);
            }
            if (strTextBox == "txtToo")
            {
                setFocusControl(txtToo);
            }
            if (strTextBox == "txtRITC_Code")
            {
                setFocusControl(txtRITC_Code);
            }
            if (strTextBox == "txtIE_Code")
            {
                setFocusControl(txtIE_Code);
            }
            if (strTextBox == "txtNet_Wt_Incr")
            {
                setFocusControl(txtNet_Wt_Incr);
            }
            if (strTextBox == "txtKind_Attention")
            {
                setFocusControl(txtKind_Attention);
            }
            if (strTextBox == "txtTele_No")
            {
                setFocusControl(txtTele_No);
            }
            if (strTextBox == "txtEuro_Rate")
            {
                setFocusControl(txtEuro_Rate);
            }
            if (strTextBox == "txtOur_Inv_Rmk")
            {
                setFocusControl(txtOur_Inv_Rmk);
            }
            if (strTextBox == "txtAmount_In_Rs")
            {
                setFocusControl(txtAmount_In_Rs);
            }
            if (strTextBox == "txtLUT_No")
            {
                setFocusControl(txtLUT_No);
            }
            if (strTextBox == "txtBox_No")
            {
                Int32 boxno = txtBox_No.Text != string.Empty ? Convert.ToInt32(txtBox_No.Text) : 0;
                Int32 totalBox = txtNo_Of_Box.Text != string.Empty ? Convert.ToInt32(txtNo_Of_Box.Text) : 0;

                if (boxno <= totalBox && boxno >= 1)
                {
                    setFocusControl(txtBox_Size);
                }
                else
                {
                    setFocusControl(txtBox_No);
                    return;
                }
                // setFocusControl(txtBox_No);
            }
            if (strTextBox == "txtBox_Size")
            {



                setFocusControl(txtBox_Size);
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


                // setFocusControl(txtPart_No);
            }
            if (strTextBox == "txtQty")
            {
                setFocusControl(txtWeight);
            }
            if (strTextBox == "txtWeight")
            {
                setFocusControl(txtItem_Rate);
            }
            if (strTextBox == "txtNet_Wt")
            {
                setFocusControl(txtNet_Wt);
            }
            if (strTextBox == "txtGross_Wt")
            {
                setFocusControl(txtGross_Wt);
            }
            if (strTextBox == "txtItem_Rate")
            {
                setFocusControl(btnAdddetails);
            }
            if (strTextBox == "txtItem_Value")
            {
                setFocusControl(btnAdddetails);
            }
            if (strTextBox == "txtShipTo_Code")
            {
                string acname = "";
                if (txtShipTo_Code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtShipTo_Code.Text);
                    if (a == false)
                    {
                        btntxtShipToCode_Click(this, new EventArgs());
                    }
                    else
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        ds = clsDAL.SimpleQuery("select Ac_Name_E,Address_E,CityName,Pincode,Ie_Code from " + qryAccountList + " where Ac_Code=" + txtShipTo_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    acname = dt.Rows[0]["Ac_Name_E"].ToString();
                                    acname = acname + dt.Rows[0]["Address_E"].ToString();
                                    acname = acname + dt.Rows[0]["CityName"].ToString();
                                    acname = acname + dt.Rows[0]["Pincode"].ToString();
                                    hdnfshipto.Value = clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + txtShipTo_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                    txtShip_To.Text = acname;
                                    txtIE_Code.Text = dt.Rows[0]["Ie_Code"].ToString();
                                    setFocusControl(txtShip_To);
                                }
                            }
                        }

                        else
                        {
                            txtShipTo_Code.Text = string.Empty;
                            txtShip_To.Text = acname;
                            setFocusControl(txtShipTo_Code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtShipTo_Code);
                }
            }
            if (strTextBox == "txtConsign_code")
            {
                string acname = "";
                if (txtConsign_code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtConsign_code.Text);
                    if (a == false)
                    {
                        btntxtConsigncode_Click(this, new EventArgs());
                    }
                    else
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        ds = clsDAL.SimpleQuery("select Ac_Name_E,Address_E,CityName,Pincode,Ie_Code from " + qryAccountList + " where Ac_Code=" + txtConsign_code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    acname = dt.Rows[0]["Ac_Name_E"].ToString();
                                    acname = acname + dt.Rows[0]["Address_E"].ToString();
                                    acname = acname + dt.Rows[0]["CityName"].ToString();
                                    acname = acname + dt.Rows[0]["Pincode"].ToString();
                                    hdnfconsign.Value = clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + txtConsign_code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                    txtConsign.Text = acname;

                                    setFocusControl(txtConsign);
                                }
                            }
                        }

                        else
                        {
                            txtConsign_code.Text = string.Empty;
                            txtConsign.Text = acname;
                            setFocusControl(txtConsign_code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtConsign_code);
                }
            }
            if (strTextBox == "txtGstRate")
            {
                string gstratename = "";
                if (txtGstRate.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtGstRate.Text);
                    if (a == false)
                    {
                        btntxtGstRate_Click(this, new EventArgs());
                    }
                    else
                    {
                        gstratename = clsCommon.getString("select GST_Name from " + tblPrefix + "GSTRateMaster  where Doc_No=" + txtGstRate.Text + "");
                        if (gstratename != string.Empty && gstratename != "0")
                        {
                            DataSet ds1 = clsDAL.SimpleQuery("select Doc_no,GST_Name,Rate,IGST,SGST,CGST from nt_1_gstratemaster where doc_no=" + txtGstRate.Text + " and company_code=" + Session["Company_Code"].ToString() + "");
                            if (ds1 != null)
                            {
                                if (ds1.Tables[0].Rows.Count > 0)
                                {
                                    Session["GSTRate"] = ds1.Tables[0].Rows[0]["Rate"].ToString();
                                    Session["igstrate"] = ds1.Tables[0].Rows[0]["IGST"].ToString();
                                    Session["sgstrate"] = ds1.Tables[0].Rows[0]["SGST"].ToString();
                                    Session["cgstrate"] = ds1.Tables[0].Rows[0]["CGST"].ToString();
                                }
                            }
                            lblGstRateName.Text = gstratename;

                            //GSTCalculations();
                            setFocusControl(txtTele_No);
                        }
                        else
                        {
                            txtGstRate.Text = string.Empty;
                            lblGstRateName.Text = gstratename;
                            setFocusControl(txtGstRate);
                        }
                    }
                }
                else
                {
                    lblGstRateName.Text = "";
                    setFocusControl(txtGstRate);
                }
                //  return;
            }
            #region[calculation]
            Int32 qty = txtQty.Text != string.Empty ? Convert.ToInt32(txtQty.Text) : 0;
            double weight = Math.Round((txtWeight.Text != string.Empty ? Convert.ToDouble(txtWeight.Text) : 0), 3);
            double netwt = 0.000;

            netwt = Math.Round((qty * weight), 3);
            txtNet_Wt.Text = netwt.ToString();

            double grosswt = 0.000;
            double totalboxwt = txtNet_Wt_Incr.Text != string.Empty ? Convert.ToDouble(txtNet_Wt_Incr.Text) : 0;

            if (btnAdddetails.Text != "UPDATE")
            {
                grosswt = Math.Round((netwt + totalboxwt), 0);

                txtGross_Wt.Text = grosswt.ToString();
            }

            double itemrate = Math.Round((txtItem_Rate.Text != string.Empty ? Convert.ToDouble(txtItem_Rate.Text) : 0), 2);
            double itemvalue = Math.Round((qty * itemrate), 2);
            txtItem_Value.Text = itemvalue.ToString();




            #endregion

        }
        catch
        {
        }
    }
    #endregion

    private void calculation()
    {
        #region[calculation]
        Int32 qty = txtQty.Text != string.Empty ? Convert.ToInt32(txtQty.Text) : 0;
        double weight = Math.Round((txtWeight.Text != string.Empty ? Convert.ToDouble(txtWeight.Text) : 0), 3);
        double netwt = 0.000;

        netwt = Math.Round((qty * weight), 3);
        txtNet_Wt.Text = netwt.ToString();
        #region grosswt
        //double grosswt = 0.000;
        //double totalboxwt = txtNet_Wt_Incr.Text != string.Empty ? Convert.ToDouble(txtNet_Wt_Incr.Text) : 0;
        //int box = 0;
        //int boxno = Convert.ToInt32(txtBox_No.Text);
        //if (btnAdddetails.Text == "ADD")
        //{
        //    if (grdDetail.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < grdDetail.Rows.Count; i++)
        //        {
        //            box = Convert.ToInt32(grdDetail.Rows[i].Cells[3].Text);
        //            if (box != boxno)
        //            {
        //                grosswt = Math.Round((netwt + totalboxwt), 0);
        //            }
        //            else
        //            {
        //                grosswt = Math.Round((netwt), 0);
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        grosswt = Math.Round((netwt + totalboxwt), 0);

        //    }
        //    txtGross_Wt.Text = grosswt.ToString();
        #endregion
        if (btnAdddetails.Text == "ADD")
        {
            double itemrate = Math.Round((txtItem_Rate.Text != string.Empty ? Convert.ToDouble(txtItem_Rate.Text) : 0), 2);
            double itemvalue = Math.Round((qty * itemrate), 2);
            txtItem_Value.Text = itemvalue.ToString();
        }





        #endregion
    }

    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            string qryDisplay = " select * from " + qryCommon + " where Year_Code='" + Convert.ToInt32(Session["year"].ToString()) +
                "' and  Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Doc_No=" + hdnf.Value;
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
        calculation();
        //int NoBox;
        //Int32.TryParse(txtBox_No.Text, out NoBox);
        //if (NoBox == 0)
        //{

        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please enter correct Box No.! ');", true);

        //    setFocusControl(txtBox_No);
        //    return;
        //}
        int Qty;
        Int32.TryParse(txtQty.Text, out Qty);
        if (Qty == 0)
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please enter correct Qty.! ');", true);

            setFocusControl(txtQty);
            return;
        }
        double Weight;
        double.TryParse(txtWeight.Text, out Weight);
        if (Weight == 0)
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please enter correct Weight! ');", true);

            setFocusControl(txtWeight);
            return;
        }
        double ItemRate;
        double.TryParse(txtItem_Rate.Text, out ItemRate);
        if (ItemRate == 0)
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please enter correct Item Rate! ');", true);

            setFocusControl(txtItem_Rate);
            return;
        }

        try
        {
            //int boxno;
            //int.TryParse(txtBox_No.Text, out boxno);
            //if (boxno == 0)
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please enter correct box no! ');", true);
            //    setFocusControl(txtBox_No);
            //    return;
            //}
            int partno;
            int.TryParse(txtPart_no.Text, out partno);
            if (partno == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please enter correct partno! ');", true);
                setFocusControl(txtPart_no);
                return;
            }
            int qty;
            int.TryParse(txtQty.Text, out qty);
            if (qty == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please enter correct qty! ');", true);
                setFocusControl(txtQty);
                return;
            }
            double weight;
            double.TryParse(txtWeight.Text, out weight);
            if (weight == 0)
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please enter correct amount!);", true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please enter correct weight! ');", true);

                setFocusControl(txtWeight);
                return;
            }
            double itemrate;
            double.TryParse(txtItem_Rate.Text, out itemrate);
            if (itemrate == 0)
            {
                // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please enter correct amount!);", true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please enter correct item rate! ');", true);

                setFocusControl(txtItem_Rate);
                return;
            }


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
                        string id = clsCommon.getString("select Detail_ID from " + tblDetails + " where Detail_ID='" + lblID.Text + "' and Doc_No=" + txtDoc_No.Text + " " +
                            " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) + "");
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
                    dt.Columns.Add((new DataColumn("Detail_ID", typeof(int))));
                    #region [Write here columns]
                    dt.Columns.Add((new DataColumn("Container_No", typeof(string))));
                    dt.Columns.Add((new DataColumn("Details", typeof(string))));
                    dt.Columns.Add((new DataColumn("Item_Code", typeof(int))));
                    dt.Columns.Add((new DataColumn("Item_Name", typeof(string))));
                    dt.Columns.Add((new DataColumn("Qty", typeof(int))));
                    dt.Columns.Add((new DataColumn("Weight", typeof(double))));
                    dt.Columns.Add((new DataColumn("Net_Wt", typeof(double))));
                    dt.Columns.Add((new DataColumn("Gross_Wt", typeof(double))));
                    dt.Columns.Add((new DataColumn("Item_Rate", typeof(double))));
                    dt.Columns.Add((new DataColumn("Item_Value", typeof(double))));
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
                dt.Columns.Add((new DataColumn("Detail_ID", typeof(int))));
                #region [Write here columns]
                dt.Columns.Add((new DataColumn("Container_No", typeof(string))));
                dt.Columns.Add((new DataColumn("Details", typeof(string))));
                dt.Columns.Add((new DataColumn("Item_Code", typeof(int))));
                dt.Columns.Add((new DataColumn("Item_Name", typeof(string))));
                dt.Columns.Add((new DataColumn("Qty", typeof(int))));
                dt.Columns.Add((new DataColumn("Weight", typeof(double))));
                dt.Columns.Add((new DataColumn("Net_Wt", typeof(double))));
                dt.Columns.Add((new DataColumn("Gross_Wt", typeof(double))));
                dt.Columns.Add((new DataColumn("Item_Rate", typeof(double))));
                dt.Columns.Add((new DataColumn("Item_Value", typeof(double))));
                #endregion
                dt.Columns.Add(new DataColumn("rowAction", typeof(string)));
                dt.Columns.Add((new DataColumn("SrNo", typeof(int))));
                dr = dt.NewRow();
                dr["Detail_ID"] = rowIndex;
                dr["rowAction"] = "A";
                dr["SrNo"] = 0;
            }
            #region [ Set values to dr]

            #region set sr no

            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {

            //        // int boxno1 = Convert.ToInt32(grdDetail.Rows[i].Cells[3].Text);
            //        int boxno1 = Convert.ToInt32(dt.Rows[i]["Box_No"].ToString());

            //        //txtConsign.Text = dt.Rows[0]["Consign"].ToString();
            //        //drr1 = (DataRow)dt.Rows[i];
            //        //drr["SrNo"] = i + 1;
            //    }
            //}
            //else
            //{
            //    txtBox_No.Text = "1";
            //}
            #endregion


            if (txtBox_No.Text != string.Empty)
            {
                dr["Container_No"] = txtBox_No.Text;
            }
            else
            {
                setFocusControl(txtBox_No);
                return;
            }
            dr["Details"] = txtBox_Size.Text;
            if (txtPart_no.Text != string.Empty)
            {
                dr["Item_Code"] = txtPart_no.Text;
            }
            else
            {
                setFocusControl(txtPart_no);
                return;
            }
            dr["Item_Name"] = lblPart_no.Text;

            if (txtQty.Text != string.Empty)
            {
                dr["Qty"] = txtQty.Text;
            }
            else
            {
                setFocusControl(txtQty);
                return;
            }
            if (txtWeight.Text != string.Empty)
            {
                dr["Weight"] = txtWeight.Text;
            }
            else
            {
                setFocusControl(txtWeight);
                return;
            }

            dr["Net_Wt"] = txtNet_Wt.Text;
            dr["Gross_Wt"] = txtGross_Wt.Text;

            if (txtItem_Rate.Text != string.Empty)
            {
                dr["Item_Rate"] = txtItem_Rate.Text;
            }
            else
            {
                setFocusControl(txtItem_Rate);
                return;
            }

            dr["Item_Value"] = txtItem_Value.Text;
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
                setFocusControl(txtBox_No);
            }
            else
            {
                pnlPopupDetails.Style["display"] = "none";
                setFocusControl(btnAdddetails);
                btnOpenDetailsPopup.Focus();
            }
            // Empty Code->
            txtBox_No.Text = string.Empty;
            //txtBox_Size.Text = string.Empty;
            //txtPart_no.Text = string.Empty;
            //lblPart_no.Text = string.Empty;
            txtQty.Text = string.Empty;
            //txtWeight.Text = string.Empty;
            txtNet_Wt.Text = string.Empty;
            txtGross_Wt.Text = string.Empty;
            //txtItem_Rate.Text = string.Empty;
            txtItem_Value.Text = string.Empty;
            btnAdddetails.Text = "ADD";
            setFocusControl(txtBox_No);
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
        txtBox_No.Text = string.Empty;
        txtBox_Size.Text = string.Empty;
        txtPart_no.Text = string.Empty;
        txtQty.Text = string.Empty;
        txtWeight.Text = string.Empty;
        txtNet_Wt.Text = string.Empty;
        txtGross_Wt.Text = string.Empty;
        txtItem_Rate.Text = string.Empty;
        txtItem_Value.Text = string.Empty;
        btnAdddetails.Text = "ADD";
        setFocusControl(txtBox_No);
        pnlPopupDetails.Style["display"] = "none";
    }
    #endregion

    #region [showDetailsRow]
    private void showDetailsRow(GridViewRow gvrow)
    {
        lblNo.Text = Server.HtmlDecode(gvrow.Cells[Srno].Text);//srno row id;
        lblID.Text = Server.HtmlDecode(gvrow.Cells[2].Text);//Detail id;
        txtBox_No.Text = Server.HtmlDecode(gvrow.Cells[3].Text);
        txtBox_Size.Text = Server.HtmlDecode(gvrow.Cells[Details].Text);
        txtPart_no.Text = Server.HtmlDecode(gvrow.Cells[Item_Code].Text);
        lblPart_no.Text = Server.HtmlDecode(gvrow.Cells[6].Text);
        txtQty.Text = Server.HtmlDecode(gvrow.Cells[Qty].Text);
        txtWeight.Text = Server.HtmlDecode(gvrow.Cells[Weight].Text);
        txtNet_Wt.Text = Server.HtmlDecode(gvrow.Cells[Net_Wt].Text);
        txtGross_Wt.Text = Server.HtmlDecode(gvrow.Cells[Gross_Wt].Text);
        txtItem_Rate.Text = Server.HtmlDecode(gvrow.Cells[Item_Rate].Text);
        txtItem_Value.Text = Server.HtmlDecode(gvrow.Cells[Item_Value].Text);
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
                int ID = Convert.ToInt32(dt.Rows[rowIndex]["Detail_ID"].ToString());
                string IDExisting = clsCommon.getString("select Detail_ID from " + tblDetails + " where Doc_NO='" + hdnf.Value + "' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
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
            e.Row.Cells[2].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[Rowaction].ControlStyle.Width = new Unit("70px");
            e.Row.Cells[Srno].ControlStyle.Width = new Unit("70px");
            //--------------------------------------------------
            e.Row.Cells[3].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[3].Style["overflow"] = "hidden";
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Details].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Details].Style["overflow"] = "hidden";
            e.Row.Cells[Details].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Item_Code].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Item_Code].Style["overflow"] = "hidden";
            e.Row.Cells[Item_Code].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[6].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[6].Style["overflow"] = "hidden";
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Qty].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Qty].Style["overflow"] = "hidden";
            e.Row.Cells[Qty].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Weight].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Weight].Style["overflow"] = "hidden";
            e.Row.Cells[Weight].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Net_Wt].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Net_Wt].Style["overflow"] = "hidden";
            e.Row.Cells[Net_Wt].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Gross_Wt].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Gross_Wt].Style["overflow"] = "hidden";
            e.Row.Cells[Gross_Wt].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Item_Rate].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Item_Rate].Style["overflow"] = "hidden";
            e.Row.Cells[Item_Rate].HorizontalAlign = HorizontalAlign.Left;
            //--------------------------------------------------
            e.Row.Cells[Item_Value].ControlStyle.Width = new Unit("120px");
            e.Row.Cells[Item_Value].Style["overflow"] = "hidden";
            e.Row.Cells[Item_Value].HorizontalAlign = HorizontalAlign.Left;
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
            //e.Row.Cells[0].Width = new Unit("120px");
            //e.Row.Cells[0].ControlStyle.Width = new Unit("120px");
            //e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            //    e.Row.Cells[0].Style["overflow" ] = "hidden";
            //    e.Row.Cells[0].Visible =true;

        }
        if (e.Row.RowType != DataControlRowType.Pager)
        {
            if (v == "txtPart_no")
            {
                e.Row.Cells[0].ControlStyle.Width = new Unit("100px");
                e.Row.Cells[0].Style["overflow"] = "hidden";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].ControlStyle.Width = new Unit("700px");
                e.Row.Cells[1].Style["overflow"] = "hidden";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;

            }
            if (v == "txtShipTo_Code")
            {
                e.Row.Cells[0].ControlStyle.Width = new Unit("100px");
                e.Row.Cells[0].Style["overflow"] = "hidden";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].ControlStyle.Width = new Unit("500px");
                e.Row.Cells[1].Style["overflow"] = "hidden";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].ControlStyle.Width = new Unit("700px");
                e.Row.Cells[2].Style["overflow"] = "hidden";
                e.Row.Cells[2].ControlStyle.Width = new Unit("100px");
                e.Row.Cells[2].ControlStyle.Width = new Unit("50px");

            }
            if (v == "txtConsign_code")
            {
                e.Row.Cells[0].ControlStyle.Width = new Unit("100px");
                e.Row.Cells[0].Style["overflow"] = "hidden";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].ControlStyle.Width = new Unit("500px");
                e.Row.Cells[1].Style["overflow"] = "hidden";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[2].ControlStyle.Width = new Unit("700px");
                e.Row.Cells[2].Style["overflow"] = "hidden";
                e.Row.Cells[2].ControlStyle.Width = new Unit("100px");
                e.Row.Cells[2].ControlStyle.Width = new Unit("50px");

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
                            setFocusControl(txtBox_No);
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
    #region [txtEditDoc_No_TextChanged]
    protected void txtEditDoc_No_TextChanged(object sender, EventArgs e)
    {
        //searchString = txtEditDoc_No.Text;
        //strTextBox = "txtEditDoc_No";
        //csCalculations();
        try
        {
            bool a = clsCommon.isStringIsNumeric(txtEditDoc_No.Text);
            if (a == false)
            {
                searchString = txtEditDoc_No.Text;
                strTextBox = "txtEditDoc_No";
                pnlPopup.Style["display"] = "block";
                hdnfClosePopup.Value = "txtEditDoc_No";
                btnSearch_Click(this, new EventArgs());
            }
            else
            {
                hdnf.Value = txtEditDoc_No.Text;
                string qry1 = getDisplayQuery();
                fetchRecord(qry1);
                setFocusControl(txtEditDoc_No);
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
        //searchString = txtDoc_No.Text;
        //strTextBox = "txtDoc_No";
        //csCalculations();
        #region code
        try
        {
            int n;
            bool isNumeric = int.TryParse(txtDoc_No.Text, out n);

            if (isNumeric == true)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string txtValue = "";
                if (txtDoc_No.Text != string.Empty)
                {
                    txtValue = txtDoc_No.Text;

                    string qry = "select * from " + tblHead + " where  Doc_No='" + txtValue + "' " +
                        "  and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'";

                    ds = clsDAL.SimpleQuery(qry);
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            dt = ds.Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                //Record Found
                                hdnf.Value = dt.Rows[0]["Doc_No"].ToString();

                                if (ViewState["mode"] != null)
                                {
                                    if (ViewState["mode"].ToString() == "I")
                                    {
                                        lblMsg.Text = "** Doc No (" + txtValue + ") Already Exist";
                                        lblMsg.ForeColor = System.Drawing.Color.Red;
                                        this.getMaxCode();
                                        //txtDoc_no.Enabled = false;

                                        btnSave.Enabled = true;   //IMP                                       
                                        setFocusControl(txtDoc_Date);
                                    }

                                    if (ViewState["mode"].ToString() == "U")
                                    {
                                        //fetch record
                                        qry = getDisplayQuery();
                                        bool recordExist = this.fetchRecord(qry);
                                        if (recordExist == true)
                                        {
                                            txtDoc_No.Enabled = false;
                                            setFocusControl(txtDoc_Date);

                                            hdnf.Value = txtDoc_No.Text;
                                            txtEditDoc_No.Text = string.Empty;
                                        }
                                    }
                                }
                            }
                            else   //Record Not Found
                            {
                                if (ViewState["mode"].ToString() == "I")  //Insert Mode
                                {
                                    lblMsg.Text = "";
                                    setFocusControl(txtDoc_Date);
                                    txtDoc_No.Enabled = false;
                                    btnSave.Enabled = true;   //IMP
                                }
                                if (ViewState["mode"].ToString() == "U")
                                {
                                    this.makeEmptyForm("E");
                                    lblMsg.Text = "** Record Not Found";
                                    lblMsg.ForeColor = System.Drawing.Color.Red;
                                    txtDoc_No.Text = string.Empty;
                                    setFocusControl(txtDoc_No);
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblMsg.Text = string.Empty;
                    setFocusControl(txtDoc_No);
                }
            }
            else
            {
                this.makeEmptyForm("A");
                lblMsg.Text = "Doc No is numeric";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                clsButtonNavigation.enableDisable("E");
                txtDoc_No.Text = string.Empty;
                setFocusControl(txtDoc_No);
            }
        }
        catch
        {

        }
        #endregion
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
    #region [txtDoc_Date_TextChanged]
    protected void txtDoc_Date_TextChanged(object sender, EventArgs e)
    {
        searchString = txtDoc_Date.Text;
        strTextBox = "txtDoc_Date";
        csCalculations();
    }
    #endregion
    #region [txtOrder_No_TextChanged]
    protected void txtOrder_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtOrder_No.Text;
        strTextBox = "txtOrder_No";
        csCalculations();
    }
    #endregion
    #region [txtShip_To_TextChanged]
    protected void txtShip_To_TextChanged(object sender, EventArgs e)
    {
        searchString = txtShip_To.Text;
        strTextBox = "txtShip_To";
        csCalculations();
    }
    #endregion
    #region [txtConsign_TextChanged]
    protected void txtConsign_TextChanged(object sender, EventArgs e)
    {
        searchString = txtConsign.Text;
        strTextBox = "txtConsign";
        csCalculations();
    }
    #endregion
    #region [txtFinal_Dest_TextChanged]
    protected void txtFinal_Dest_TextChanged(object sender, EventArgs e)
    {
        searchString = txtFinal_Dest.Text;
        strTextBox = "txtFinal_Dest";
        csCalculations();
    }
    #endregion
    #region [txtPer_Carriage_TextChanged]
    protected void txtPer_Carriage_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPer_Carriage.Text;
        strTextBox = "txtPer_Carriage";
        csCalculations();
    }
    #endregion
    #region [txtPlace_Of_Receipt_TextChanged]
    protected void txtPlace_Of_Receipt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPlace_Of_Receipt.Text;
        strTextBox = "txtPlace_Of_Receipt";
        csCalculations();
    }
    #endregion
    #region [txtVessel_TextChanged]
    protected void txtVessel_TextChanged(object sender, EventArgs e)
    {
        searchString = txtVessel.Text;
        strTextBox = "txtVessel";
        csCalculations();
    }
    #endregion
    #region [txtPort_Of_Loading_TextChanged]
    protected void txtPort_Of_Loading_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPort_Of_Loading.Text;
        strTextBox = "txtPort_Of_Loading";
        csCalculations();
    }
    #endregion
    #region [txtPort_Of_Discharage_TextChanged]
    protected void txtPort_Of_Discharage_TextChanged(object sender, EventArgs e)
    {
        searchString = txtPort_Of_Discharage.Text;
        strTextBox = "txtPort_Of_Discharage";
        csCalculations();
    }
    #endregion
    #region [txtFinal_Destination_TextChanged]
    protected void txtFinal_Destination_TextChanged(object sender, EventArgs e)
    {
        searchString = txtFinal_Destination.Text;
        strTextBox = "txtFinal_Destination";
        csCalculations();
    }
    #endregion
    #region [txtTerms_TextChanged]
    protected void txtTerms_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTerms.Text;
        strTextBox = "txtTerms";
        csCalculations();
    }
    #endregion
    #region [txtCategory_TextChanged]
    protected void txtCategory_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCategory.Text;
        strTextBox = "txtCategory";
        csCalculations();
    }
    #endregion
    #region [txtNo_Of_Box_TextChanged]
    protected void txtNo_Of_Box_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNo_Of_Box.Text;
        strTextBox = "txtNo_Of_Box";
        csCalculations();
    }
    #endregion
    #region [txtToo_TextChanged]
    protected void txtToo_TextChanged(object sender, EventArgs e)
    {
        searchString = txtToo.Text;
        strTextBox = "txtToo";
        csCalculations();
    }
    #endregion
    #region [txtRITC_Code_TextChanged]
    protected void txtRITC_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRITC_Code.Text;
        strTextBox = "txtRITC_Code";
        csCalculations();
    }
    #endregion
    #region [txtIE_Code_TextChanged]
    protected void txtIE_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtIE_Code.Text;
        strTextBox = "txtIE_Code";
        csCalculations();
    }
    #endregion
    #region [txtNet_Wt_Incr_TextChanged]
    protected void txtNet_Wt_Incr_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNet_Wt_Incr.Text;
        strTextBox = "txtNet_Wt_Incr";
        csCalculations();
    }
    #endregion
    #region [txtKind_Attention_TextChanged]
    protected void txtKind_Attention_TextChanged(object sender, EventArgs e)
    {
        searchString = txtKind_Attention.Text;
        strTextBox = "txtKind_Attention";
        csCalculations();
    }
    #endregion
    #region [txtTele_No_TextChanged]
    protected void txtTele_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTele_No.Text;
        strTextBox = "txtTele_No";
        csCalculations();
    }
    #endregion
    #region [txtEuro_Rate_TextChanged]
    protected void txtEuro_Rate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtEuro_Rate.Text;
        strTextBox = "txtEuro_Rate";
        csCalculations();
    }
    #endregion
    #region [txtOur_Inv_Rmk_TextChanged]
    protected void txtOur_Inv_Rmk_TextChanged(object sender, EventArgs e)
    {
        searchString = txtOur_Inv_Rmk.Text;
        strTextBox = "txtOur_Inv_Rmk";
        csCalculations();
    }
    #endregion
    #region [txtAmount_In_Rs_TextChanged]
    protected void txtAmount_In_Rs_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAmount_In_Rs.Text;
        strTextBox = "txtAmount_In_Rs";
        csCalculations();
    }
    #endregion

    //protected void txtCurrent_type_TextChanged(object sender, EventArgs e)
    //{
    //    searchString = txtCurrent_type.Text;
    //    strTextBox = "txtCurrent_type";
    //    csCalculations();
    //}
    protected void txtCurrencyInWord_TextChanged(object sender, EventArgs e)
    {
        searchString = txtCurrencyInWord.Text;
        strTextBox = "txtCurrencyInWord";
        csCalculations();
    }
    #region [txtLUT_No_TextChanged]
    protected void txtLUT_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtLUT_No.Text;
        strTextBox = "txtLUT_No";
        csCalculations();
    }
    #endregion

    #region [txtBox_No_TextChanged]
    protected void txtBox_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBox_No.Text;
        strTextBox = "txtBox_No";
        csCalculations();
    }
    #endregion


    #region [txtBox_Size_TextChanged]
    protected void txtBox_Size_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBox_Size.Text;
        strTextBox = "txtBox_Size";
        csCalculations();
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


    #region [txtWeight_TextChanged]
    protected void txtWeight_TextChanged(object sender, EventArgs e)
    {
        searchString = txtWeight.Text;
        strTextBox = "txtWeight";
        csCalculations();
    }
    #endregion


    #region [txtNet_Wt_TextChanged]
    protected void txtNet_Wt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNet_Wt.Text;
        strTextBox = "txtNet_Wt";
        csCalculations();
    }
    #endregion


    #region [txtGross_Wt_TextChanged]
    protected void txtGross_Wt_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGross_Wt.Text;
        strTextBox = "txtGross_Wt";
        csCalculations();
    }
    #endregion


    #region [txtItem_Rate_TextChanged]
    protected void txtItem_Rate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtItem_Rate.Text;
        strTextBox = "txtItem_Rate";
        csCalculations();
    }
    #endregion


    #region [txtItem_Value_TextChanged]
    protected void txtItem_Value_TextChanged(object sender, EventArgs e)
    {
        searchString = txtItem_Value.Text;
        strTextBox = "txtItem_Value";
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
                    //string qry = "select doc_no,doc_date,PartyName,PartyCity from " + qryCommon + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                    //    " and Year_Code=" + Convert.ToInt32(Session["year"].ToString()) +
                    //    " and (doc_no like '%" + txtSearchText.Text + "%' or doc_date like '%" + txtSearchText.Text + "%' or PartyName like '%" + txtSearchText.Text + "%' or PartyCity like '%" + txtSearchText.Text + "%')";

                    string qry = " select Doc_No from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                        + " and (doc_no like '%" + txtSearchText.Text + "%') order by Doc_No";
                    this.showPopup(qry);
                }
            }

            if (hdnfClosePopup.Value == "txtShipTo_Code")
            {
                lblPopupHead.Text = "--Select Ship To Code--";
                string qry = "select Ac_Code,Ac_Name_E,Address_E,CityName,Pincode from " + qryAccountList + " where Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%' or Address_E like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);

            }
            if (hdnfClosePopup.Value == "txtConsign_code")
            {
                lblPopupHead.Text = "--Select Ship To Code--";
                string qry = "select Ac_Code,Ac_Name_E,Address_E,CityName,Pincode from " + qryAccountList + " where Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%' or Address_E like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);

            }
            if (hdnfClosePopup.Value == "txtPart_no")
            {
                lblPopupHead.Text = "--Select Item--";

                qry = "select distinct System_Code,System_Name_E from nt_1_systemmaster where System_Type='I' and  Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " " +
                  " and (System_Code like '%" + txtSearchText.Text + "%' or System_Name_E like '%" + txtSearchText.Text + "%')";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtGstRate")
            {
                lblPopupHead.Text = "--Select Mill--";
                string qry = "select Doc_no,GST_Name,Rate,IGST,SGST,CGST from nt_1_gstratemaster where " +
                    "  (Doc_No like '%" + txtSearchText.Text + "%' or GST_Name like '%" + txtSearchText.Text + "%')";
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
        btnSave.Enabled = true;
        #region [Validation Part]
        if (txtNo_Of_Box.Text != string.Empty)
        {
        }
        else
        {
            setFocusControl(txtNo_Of_Box);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Check No Box!!!');", true);
            return;
        }
        string Post_date = Session["Post_Date"].ToString();
        if (Convert.ToDateTime(txtDoc_Date.Text) >= Convert.ToDateTime(Post_date))
        {
            // isValidated = true;
        }
        else
        {
            setFocusControl(txtDoc_Date);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Post Date Error!!!!!');", true);
            return;
        }
        #region box validation
        //DataTable dt = (DataTable)ViewState["currentTable"];
        //dt.DefaultView.Sort = "Container_No asc";


        //DataView view = new DataView(dt);
        //DataTable distinctValues = view.ToTable(true, "Container_No");
        //Int32 noofbox = Convert.ToInt32(txtNo_Of_Box.Text);
        //Int32 count = (distinctValues.Rows.Count);
        //string comparison = "";
        //if (noofbox < count)
        //{
        //    comparison = "less than";
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Box size is Less than of No of Box size');", true);
        //    return;
        //}
        //else
        //{
        //    if (count == noofbox)
        //    {
        //        comparison = "equal to";
        //    }
        //    else
        //    {
        //        comparison = "greater than";
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Box size is Greater than of No of Box size');", true);
        //        return;
        //    }
        //}
       
        //for (int i = 1; i <= Convert.ToInt32(txtNo_Of_Box.Text); i++)
        //{

        //    DataRow[] result = distinctValues.Select("Container_No='" + i + "'");

          

        //    string xxx = Convert.ToString(result[0][0]);
        //    xxx = xxx.Trim();
        //    if (Convert.ToString(i) == xxx)
        //    {

        //    }
        //    else
        //    {
        //        return;
        //    }
           
        //}

        //bool ifExist = false;
        //foreach (DataRow dr in dt.Rows)
        //{
        //    if (dr["Container_No"].ToString() == txtNo_Of_Box.Text.Trim())
        //    {
        //        ifExist = true;
        //    }
        //}

        //if (!ifExist)
        //{
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Number of Boxes Mismatch');", true);
        //    return;
        //}
        //else
        //{
        //    //this.lblErrorMessage.Visible = true;
        //}
        #endregion


        bool isValidated = true;
        //        if textbox is date then if condition will be like this if(clsCommon.isValidDate(txtDoc_Date.Text==true))
        if (txtDoc_No.Text != string.Empty)
        {
            isValidated = true;
        }
        else
        {
            isValidated = false;
            setFocusControl(txtDoc_No);
            return;
        }
        int NoBox;
        Int32.TryParse(txtNo_Of_Box.Text, out NoBox);
        if (NoBox == 0)
        {
            // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please enter correct amount!);", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please enter correct No Of Box! ');", true);

            setFocusControl(txtNo_Of_Box);
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

        //child1.SetAttributeValue("Doc_No", txtDoc_No.Text != string.Empty ? txtDoc_No.Text : "0");
        child1.SetAttributeValue("Doc_Date", DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));
        child1.SetAttributeValue("Order_No", txtOrder_No.Text);
        child1.SetAttributeValue("Ship_To", txtShip_To.Text);
        child1.SetAttributeValue("Consign", txtConsign.Text);
        child1.SetAttributeValue("Final_Dest", txtFinal_Dest.Text);
        child1.SetAttributeValue("Per_Carriage", txtPer_Carriage.Text);
        child1.SetAttributeValue("Place_Of_Receipt", txtPlace_Of_Receipt.Text);
        child1.SetAttributeValue("Vessel", txtVessel.Text);
        child1.SetAttributeValue("Port_Of_Loading", txtPort_Of_Loading.Text);
        child1.SetAttributeValue("Port_Of_Discharage", txtPort_Of_Discharage.Text);
        child1.SetAttributeValue("Final_Destination", txtFinal_Destination.Text);
        child1.SetAttributeValue("Terms", txtTerms.Text);
        child1.SetAttributeValue("Category", txtCategory.Text);
        child1.SetAttributeValue("No_Of_Box", txtNo_Of_Box.Text != string.Empty ? txtNo_Of_Box.Text : "0");
        child1.SetAttributeValue("Too", txtToo.Text);
        child1.SetAttributeValue("RITC_Code", txtRITC_Code.Text);
        child1.SetAttributeValue("IE_Code", txtIE_Code.Text);
        child1.SetAttributeValue("Net_Wt_Incr", txtNet_Wt_Incr.Text != string.Empty ? txtNet_Wt_Incr.Text : "0.00");
        child1.SetAttributeValue("Kind_Attention", txtKind_Attention.Text);
        child1.SetAttributeValue("Tele_No", txtTele_No.Text);
        child1.SetAttributeValue("Euro_Rate", txtEuro_Rate.Text != string.Empty ? txtEuro_Rate.Text : "0.00");
        child1.SetAttributeValue("Our_Inv_Rmk", txtOur_Inv_Rmk.Text);
        child1.SetAttributeValue("Amount_In_Rs", txtAmount_In_Rs.Text != string.Empty ? txtAmount_In_Rs.Text : "0.00");
        child1.SetAttributeValue("LUT_No", txtLUT_No.Text);
        string Currencytype = drpCurrencytype.SelectedValue;
        child1.SetAttributeValue("Current_Type", Currencytype);
        child1.SetAttributeValue("Current_In_Word", txtCurrencyInWord.Text);
        child1.SetAttributeValue("Company_Code", Company_Code);
        child1.SetAttributeValue("Year_Code", Year_Code);
        child1.SetAttributeValue("Branch_Code", Branch_Code);
        child1.SetAttributeValue("ShipTo_Code", txtShipTo_Code.Text != string.Empty ? txtShipTo_Code.Text : "0");
        child1.SetAttributeValue("Consign_Code", txtConsign_code.Text != string.Empty ? txtConsign_code.Text : "0");
        child1.SetAttributeValue("ShipTo_Id", hdnfshipto.Value);
        child1.SetAttributeValue("Consign_ID", hdnfconsign.Value);
        child1.SetAttributeValue("ExportDelaration", txtExportDelaration.Text);
        child1.SetAttributeValue("countryoforigin", txtcountryorigin.Text);
        child1.SetAttributeValue("MAEQ", txtMAEQ.Text);
        string DrawBack = drpDrawBack.SelectedValue;
        child1.SetAttributeValue("DrawBack", DrawBack);
        child1.SetAttributeValue("GstRateCode", txtGstRate.Text);

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
            XElement child2 = new XElement("Details");
            Int32 Detail_ID = Convert.ToInt32(grdDetail.Rows[i].Cells[2].Text);
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
            child2.SetAttributeValue("Doc_NO", txtDoc_No.Text);
            child2.SetAttributeValue("Company_Code", Company_Code);
            child2.SetAttributeValue("Year_Code", Year_Code);
            child2.SetAttributeValue("Branch_Code", Branch_Code);

            child2.SetAttributeValue("Container_No", grdDetail.Rows[i].Cells[3].Text);
            child2.SetAttributeValue("Details", grdDetail.Rows[i].Cells[Details].Text);
            child2.SetAttributeValue("Item_Code", grdDetail.Rows[i].Cells[Item_Code].Text);
            child2.SetAttributeValue("Qty", grdDetail.Rows[i].Cells[Qty].Text);
            child2.SetAttributeValue("Weight", grdDetail.Rows[i].Cells[Weight].Text);
            child2.SetAttributeValue("Net_Wt", grdDetail.Rows[i].Cells[Net_Wt].Text);
            child2.SetAttributeValue("Gross_Wt", grdDetail.Rows[i].Cells[Gross_Wt].Text);
            child2.SetAttributeValue("Item_Rate", grdDetail.Rows[i].Cells[Item_Rate].Text);
            child2.SetAttributeValue("Item_Value", grdDetail.Rows[i].Cells[Item_Value].Text);
            child2.SetAttributeValue("Detail_ID", grdDetail.Rows[i].Cells[2].Text);
            if (btnSave.Text != "Save")
            {
                child2.SetAttributeValue("Flag", CheckingFlag);

            }
            child1.Add(child2);
        }
        #endregion
        string XMLReport = root.ToString();
        XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
        XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
        DataSet xml_ds = new DataSet();
        string spname = "SP_Export_InvHead";
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

    protected void txtShipTo_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtShipTo_Code.Text;
        strTextBox = "txtShipTo_Code";
        csCalculations();
    }
    protected void btntxtShipToCode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtShipTo_Code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    protected void txtConsign_code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtConsign_code.Text;
        strTextBox = "txtConsign_code";
        csCalculations();
    }
    protected void btntxtConsigncode_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtConsign_code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #region [txtcountryorigin_TextChanged]
    protected void txtcountryorigin_TextChanged(object sender, EventArgs e)
    {
        searchString = txtcountryorigin.Text;
        strTextBox = "txtcountryorigin";
        csCalculations();
    }
    #endregion
    #region [txtMAEQ_TextChanged]
    protected void txtMAEQ_TextChanged(object sender, EventArgs e)
    {
        searchString = txtMAEQ.Text;
        strTextBox = "txtMAEQ";
        csCalculations();
    }
    #endregion
    #region [drpCurrencytype_SelectedIndexChanged]
    protected void drpCurrencytype_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion
    #region [txtGstRate_TextChanged]
    protected void txtGstRate_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGstRate.Text;
        strTextBox = "txtGstRate";
        csCalculations();
    }
    #endregion
    #region [btntxtGstRate_Click]
    protected void btntxtGstRate_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtGstRate";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion
    #region [drpType_SelectedIndexChanged]
    protected void drpDrawBack_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion
}

