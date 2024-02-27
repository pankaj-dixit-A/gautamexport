using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;

public partial class Sugar_Export_pgeExportInvoice : System.Web.UI.Page
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
    string qryAccountList = string.Empty;
    string SystemMasterTable = string.Empty;
    string plantName = string.Empty;
    string plantCode = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tblPrefix = Session["tblPrefix"].ToString();
            tblHead = "NT_1_Export_Head";
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
    #region [getMaxCode]
    private void getMaxCode()
    {
        try
        {
            DataSet ds = null;
            using (clsGetMaxCode obj = new clsGetMaxCode())
            {
                obj.tableName = tblHead + " where ";
                obj.code = "Inv_No";
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

                btnCarryForword.Enabled = false;

                ViewState["currentTable"] = null;

                btnSave.Text = "Save";
                btntxtDoc_No.Text = "Choose No";
                btntxtDoc_No.Enabled = false;
                txtEditDoc_No.Enabled = true;
                lblMsg.Text = string.Empty;
                txtDoc_Date.Enabled = false;
                CalendarExtenderDatetxtDoc_Date.Enabled = false;
                txtIECCode.Enabled = false;
                txtAc_Code.Enabled = false;
                btntxtAc_Code.Enabled = false;
                txtBuyer_code.Enabled = false;
                btntxtBuyer_code.Enabled = false;
                btnsalebill_No.Enabled = false;
                txtAc_Address.Enabled = false;
                txtBuyer_Address.Enabled = false;
                txtNotify_Party1.Enabled = false;
                txtNotify_Party2.Enabled = false;
                txtcountryorigin.Enabled = false;
                txtFinal_Destination.Enabled = false;
                txtPer_Carriage.Enabled = false;
                txtPlace_Of_Receipt.Enabled = false;
                txtVessel.Enabled = false;
                txtPort_Of_Loading.Enabled = false;
                txtTerms.Enabled = false;
                txtMAEQ.Enabled = false;
                txtGSTNo.Enabled = false;
                txtMarksAndNos.Enabled = false;
                txtNo_Of_bags.Enabled = false;
                drpCurrencytype.Enabled = false;
                drpCurrencytype.Enabled = false;
                txtGstRate.Enabled = false;
                txtParticular.Enabled = false;
                txtQty.Enabled = false;
                txtRate_Foregin.Enabled = false;
                txtAmount.Enabled = false;
                txtSupplier_Detail.Enabled = false;
                txtManufacturer_Detail.Enabled = false;
                txtGstRate.Enabled = false;
                btntxtGstRate.Enabled = false;
                txtBags_Perkg.Enabled = false;
                txtNet_Wt_Incr.Enabled = false;
                txtNet_Wt.Enabled = false;
                txtGross_Wt.Enabled = false;
                txtExportDelaration.Enabled = false;
                txtPort_of_Discharge.Enabled = false;
                txtCountryFinalDestination.Enabled = false;
                txtManufacturer_code.Enabled = false;
                btntxtManufacturer_code.Enabled = false;
                txtSubsidies.Enabled = false;
                txtHSN.Enabled = false;
                txtITEM_CODE.Enabled = false;
                btntxtITEM_CODE.Enabled = false;
                LBLITEMNAME.Text = "";
                txtNotifyPartyCode1.Enabled = false;
                btntxtNotifyPartyCode1.Enabled = false;
                txtNotifyPartyCode2.Enabled = false;
                btntxtNotifyPartyCode2.Enabled = false;
                drpReportType.Enabled = false;

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
                txtDoc_Date.Enabled = true;
                txtDoc_Date.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                CalendarExtenderDatetxtDoc_Date.Enabled = true;
                txtIECCode.Enabled = true;
                txtAc_Code.Enabled = true;
                btntxtAc_Code.Enabled = true;
                txtBuyer_code.Enabled = true;
                btntxtBuyer_code.Enabled = true;
                btnsalebill_No.Enabled = true;
                txtAc_Address.Enabled = true;
                txtBuyer_Address.Enabled = true;
                txtNotify_Party1.Enabled = true;
                txtNotify_Party2.Enabled = true;
                txtcountryorigin.Enabled = true;
                txtFinal_Destination.Enabled = true;
                txtPer_Carriage.Enabled = true;
                txtPlace_Of_Receipt.Enabled = true;
                txtVessel.Enabled = true;
                txtPort_Of_Loading.Enabled = true;
                txtTerms.Enabled = true;
                txtMAEQ.Enabled = true;
                txtGSTNo.Enabled = true;
                txtMarksAndNos.Enabled = true;
                txtNo_Of_bags.Enabled = true;
                drpCurrencytype.Enabled = true;
                drpCurrencytype.Enabled = true;
                txtGstRate.Enabled = true;
                txtParticular.Enabled = true;
                txtQty.Enabled = true;
                txtRate_Foregin.Enabled = true;
                txtAmount.Enabled = true;
                txtSupplier_Detail.Enabled = true;
                txtManufacturer_Detail.Enabled = true;
                txtGstRate.Enabled = true;
                btntxtGstRate.Enabled = true;
                txtBags_Perkg.Enabled = true;
                txtNet_Wt_Incr.Enabled = true;
                txtNet_Wt.Enabled = true;
                txtGross_Wt.Enabled = true;
                txtExportDelaration.Enabled = true;
                txtPort_of_Discharge.Enabled = true;
                txtCountryFinalDestination.Enabled = true;
                txtManufacturer_code.Enabled = true;
                btntxtManufacturer_code.Enabled = true;
                txtSubsidies.Enabled = true;
                txtHSN.Enabled = true;
                txtNotifyPartyCode1.Enabled = true;
                btntxtNotifyPartyCode1.Enabled = true;
                txtNotifyPartyCode2.Enabled = true;
                btntxtNotifyPartyCode2.Enabled = true;
                drpReportType.Enabled = true;
                drpReportType.SelectedValue = "Qntl";
                txtITEM_CODE.Enabled = true;
                btntxtITEM_CODE.Enabled = true;
                LBLITEMNAME.Text = "";
                btnBack.Enabled = false;
                lblsalebill_Id.Text = "";
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
                txtIECCode.Enabled = false;
                txtAc_Code.Enabled = false;
                btntxtAc_Code.Enabled = false;
                txtBuyer_code.Enabled = false;
                btntxtBuyer_code.Enabled = false;
                btnsalebill_No.Enabled = false;
                txtAc_Address.Enabled = false;
                txtBuyer_Address.Enabled = false;
                txtNotify_Party1.Enabled = false;
                txtNotify_Party2.Enabled = false;
                txtcountryorigin.Enabled = false;
                txtFinal_Destination.Enabled = false;
                txtPer_Carriage.Enabled = false;
                txtPlace_Of_Receipt.Enabled = false;
                txtVessel.Enabled = false;
                txtPort_Of_Loading.Enabled = false;
                txtTerms.Enabled = false;
                txtMAEQ.Enabled = false;
                txtGSTNo.Enabled = false;
                txtMarksAndNos.Enabled = false;
                txtNo_Of_bags.Enabled = false;
                drpCurrencytype.Enabled = false;
                drpCurrencytype.Enabled = false;
                txtGstRate.Enabled = false;
                txtParticular.Enabled = false;
                txtQty.Enabled = false;
                txtRate_Foregin.Enabled = false;
                txtAmount.Enabled = false;
                txtSupplier_Detail.Enabled = false;
                txtManufacturer_Detail.Enabled = false;
                txtGstRate.Enabled = false;
                btntxtGstRate.Enabled = false;
                txtBags_Perkg.Enabled = false;
                txtNet_Wt_Incr.Enabled = false;
                txtNet_Wt.Enabled = false;
                txtGross_Wt.Enabled = false;
                txtExportDelaration.Enabled = false;
                txtPort_of_Discharge.Enabled = false;
                txtCountryFinalDestination.Enabled = false;
                txtManufacturer_code.Enabled = false;
                btntxtManufacturer_code.Enabled = false;
                txtSubsidies.Enabled = false;
                txtHSN.Enabled = false;
                txtNotifyPartyCode1.Enabled = false;
                btntxtNotifyPartyCode1.Enabled = false;
                txtNotifyPartyCode2.Enabled = false;
                btntxtNotifyPartyCode2.Enabled = false;
                drpReportType.Enabled = false;
                txtITEM_CODE.Enabled = false;
                btntxtITEM_CODE.Enabled = false;
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
                txtIECCode.Enabled = true;
                txtAc_Code.Enabled = true;
                btntxtAc_Code.Enabled = true;
                txtBuyer_code.Enabled = true;
                btntxtBuyer_code.Enabled = true;
                btnsalebill_No.Enabled = true;
                txtAc_Address.Enabled = true;
                txtBuyer_Address.Enabled = true;
                txtNotify_Party1.Enabled = true;
                txtNotify_Party2.Enabled = true;
                txtcountryorigin.Enabled = true;
                txtFinal_Destination.Enabled = true;
                txtPer_Carriage.Enabled = true;
                txtPlace_Of_Receipt.Enabled = true;
                txtVessel.Enabled = true;
                txtPort_Of_Loading.Enabled = true;
                txtTerms.Enabled = true;
                txtMAEQ.Enabled = true;
                txtGSTNo.Enabled = true;
                txtMarksAndNos.Enabled = true;
                txtNo_Of_bags.Enabled = true;
                drpCurrencytype.Enabled = true;
                drpCurrencytype.Enabled = true;
                txtGstRate.Enabled = true;
                txtParticular.Enabled = true;
                txtQty.Enabled = true;
                txtRate_Foregin.Enabled = true;
                txtAmount.Enabled = true;
                txtSupplier_Detail.Enabled = true;
                txtManufacturer_Detail.Enabled = true;
                txtGstRate.Enabled = true;
                btntxtGstRate.Enabled = true;
                txtBags_Perkg.Enabled = true;
                txtNet_Wt_Incr.Enabled = true;
                txtNet_Wt.Enabled = true;
                txtGross_Wt.Enabled = true;
                txtExportDelaration.Enabled = true;
                txtPort_of_Discharge.Enabled = true;
                txtCountryFinalDestination.Enabled = true;
                txtManufacturer_code.Enabled = true;
                btntxtManufacturer_code.Enabled = true;
                txtSubsidies.Enabled = true;
                txtHSN.Enabled = true;
                txtNotifyPartyCode1.Enabled = true;
                btntxtNotifyPartyCode1.Enabled = true;
                txtNotifyPartyCode2.Enabled = true;
                btntxtNotifyPartyCode2.Enabled = true;
                drpReportType.Enabled = true;
                txtITEM_CODE.Enabled = true;
                btntxtITEM_CODE.Enabled = true;
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
            qry = "select max(Inv_No) as Inv_No from " + tblHead + " where  Year_Code='" + Convert.ToInt32(Session["year"].ToString()) +
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
                        hdnf.Value = dt.Rows[0]["Inv_No"].ToString();
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

            query = "SELECT top 1 [Inv_No] from " + tblHead + " where Inv_No>" + Convert.ToInt32(hdnf.Value) +
                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                " ORDER BY Inv_No asc  ";
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


            query = "SELECT top 1 [Inv_No] from " + tblHead + " where Inv_No<" + int.Parse(hdnf.Value) +
                " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) +
                " ORDER BY Inv_No desc  ";
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
            query = "select Inv_No from " + tblHead + " where Inv_No=(select MIN(Inv_No) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "')";
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
                query = "SELECT top 1 [Inv_No] from " + tblHead + " where Inv_No< " + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY Inv_No desc  ";
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
                query = "SELECT top 1 [Inv_No] from " + tblHead + " where Inv_No> " + Convert.ToInt32(hdnf.Value) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY Inv_No asc  ";
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
            query = "select Inv_No from " + tblHead + " where Inv_No=(select MAX(Inv_No) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "')";
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


            string qry = "select isnull(max(Inv_No),0) as Inv_No from " + tblHead +
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
            txttitleNarration.Text = "SUPPLY MEANT FOR EXPORT PAYMENT OF INTEGRATED TAX";
            txtDoc_No.Text = Convert.ToString(Doc_No);
            string IECCode = Session["IECCode"].ToString();
            txtIECCode.Text = IECCode;
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

        Int32 MaxNo = Convert.ToInt32(clsDAL.GetString("select  isnull(MAX(Inv_No),0) from " + tblHead
          + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString())
          + "' and Year_Code='" + Convert.ToInt32(Session["year"].ToString()) + "'"));

        string carrydata = "select * from " + tblHead + " where Inv_No='" + MaxNo + "' and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString())
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
                    txtDoc_Date.Text = dt.Rows[0]["Inv_Date"].ToString();
                    txtIECCode.Text = dt.Rows[0]["IEC_Code_No"].ToString();
                    txtAc_Code.Text = dt.Rows[0]["Ac_Code"].ToString();
                    txtAc_Address.Text = dt.Rows[0]["Ac_Address"].ToString();
                    txtBuyer_code.Text = dt.Rows[0]["Buyer_code"].ToString();
                    txtBuyer_Address.Text = dt.Rows[0]["Buyer_Address"].ToString();
                    txtNotify_Party1.Text = dt.Rows[0]["Notify_Party1"].ToString();
                    txtNotify_Party2.Text = dt.Rows[0]["Notify_Party2"].ToString();
                    txtcountryorigin.Text = dt.Rows[0]["County_Goods"].ToString();
                    txtFinal_Destination.Text = dt.Rows[0]["Final_Distination"].ToString();
                    txtPer_Carriage.Text = dt.Rows[0]["Prefereed_Carriage"].ToString();
                    txtPlace_Of_Receipt.Text = dt.Rows[0]["Place_Precarriage"].ToString();
                    txtVessel.Text = dt.Rows[0]["vessel_No"].ToString();
                    txtPort_Of_Loading.Text = dt.Rows[0]["Port_Of_Landing"].ToString();
                    txtTerms.Text = dt.Rows[0]["Terms_Payment"].ToString();
                    txtMAEQ.Text = dt.Rows[0]["MAEQ_Holdder"].ToString();
                    txtGSTNo.Text = dt.Rows[0]["MAEQ_Gst_No"].ToString();
                    txtMarksAndNos.Text = dt.Rows[0]["Marks_And_No"].ToString();
                    txtNo_Of_bags.Text = dt.Rows[0]["Bags"].ToString();
                    string Currencytype = dt.Rows[0]["Foregin_Type"].ToString();
                    drpCurrencytype.SelectedValue = Currencytype;
                    txtParticular.Text = dt.Rows[0]["Particular"].ToString();
                    txtQty.Text = dt.Rows[0]["Qnty_MT"].ToString();
                    txtRate_Foregin.Text = dt.Rows[0]["Rate_Foregin"].ToString();
                    txtAmount.Text = dt.Rows[0]["Amt_Foregin"].ToString();
                    txtSupplier_Detail.Text = dt.Rows[0]["Supplier_Detail"].ToString();
                    txtManufacturer_Detail.Text = dt.Rows[0]["Manufacturer_Detail"].ToString();
                    txtGstRate.Text = dt.Rows[0]["Gst_Code"].ToString();
                    txtBags_Perkg.Text = dt.Rows[0]["Bagsper_Kg"].ToString();
                    txtNet_Wt_Incr.Text = dt.Rows[0]["Increaseby"].ToString();
                    txtNet_Wt.Text = dt.Rows[0]["Net_Wt"].ToString();
                    txtGross_Wt.Text = dt.Rows[0]["Gross_Wt"].ToString();
                    txtExportDelaration.Text = dt.Rows[0]["ExportDelaration"].ToString();
                    txtCountryFinalDestination.Text = dt.Rows[0]["CountryFinalDestination"].ToString();
                    txtPort_of_Discharge.Text = dt.Rows[0]["Port_of_Discharge"].ToString();
                    txtManufacturer_code.Text = dt.Rows[0]["Manufacturer_Code"].ToString();
                    txtSubsidies.Text = dt.Rows[0]["Subsidies"].ToString();
                    txtHSN.Text = dt.Rows[0]["HSN"].ToString();
                    txtNotifyPartyCode1.Text = dt.Rows[0]["Notify_PartyCode1"].ToString();
                    txtNotifyPartyCode2.Text = dt.Rows[0]["Notify_PartyCode2"].ToString();
                    string ReportType = dt.Rows[0]["ReportType"].ToString();
                    txtITEM_CODE.Text = dt.Rows[0]["Item_Code"].ToString();
                    //LBLITEMNAME.Text = dt.Rows[0]["System_Name_E"].ToString();
                    drpReportType.SelectedValue = ReportType;

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
                        obj.columnNm = "Inv_No=" + currentDoc_No + " and Year_Code=" + Convert.ToInt32(Session["year"].ToString())
                            + " and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
                        obj.values = "none";
                        ds = obj.insertAccountMaster(ref strrev);
                    }

                    string query = "";
                    if (strrev == "-3")
                    {
                        query = "SELECT top 1 [Inv_No] from " + tblHead + " where Inv_No>" + Convert.ToInt32(currentDoc_No) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY Inv_No asc  ";
                        hdnf.Value = clsCommon.getString(query);
                        if (hdnf.Value == string.Empty)
                        {
                            query = "SELECT top 1 [Inv_No] from " + tblHead + " where Inv_No<" + Convert.ToInt32(currentDoc_No) + " and Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' ORDER BY Inv_No desc  ";
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
        string qry = clsCommon.getString("select count(Inv_No) from " + tblHead + " where Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "'");
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


                        txtDoc_No.Text = dt.Rows[0]["Inv_No"].ToString();

                        txtDoc_Date.Text = dt.Rows[0]["Inv_Date"].ToString();
                        txtIECCode.Text = dt.Rows[0]["IEC_Code_No"].ToString();
                        txtAc_Code.Text = dt.Rows[0]["Ac_Code"].ToString();
                        txtAc_Address.Text = dt.Rows[0]["Ac_Address"].ToString();
                        txtBuyer_code.Text = dt.Rows[0]["Buyer_code"].ToString();
                        txtBuyer_Address.Text = dt.Rows[0]["Buyer_Address"].ToString();
                        txtNotify_Party1.Text = dt.Rows[0]["Notify_Party1"].ToString();
                        txtNotify_Party2.Text = dt.Rows[0]["Notify_Party2"].ToString();
                        txtcountryorigin.Text = dt.Rows[0]["County_Goods"].ToString();
                        txtFinal_Destination.Text = dt.Rows[0]["Final_Distination"].ToString();
                        txtPer_Carriage.Text = dt.Rows[0]["Prefereed_Carriage"].ToString();
                        txtPlace_Of_Receipt.Text = dt.Rows[0]["Place_Precarriage"].ToString();
                        txtVessel.Text = dt.Rows[0]["vessel_No"].ToString();
                        txtPort_Of_Loading.Text = dt.Rows[0]["Port_Of_Landing"].ToString();
                        txtTerms.Text = dt.Rows[0]["Terms_Payment"].ToString();
                        txtMAEQ.Text = dt.Rows[0]["MAEQ_Holdder"].ToString();
                        txtGSTNo.Text = dt.Rows[0]["MAEQ_Gst_No"].ToString();
                        txtMarksAndNos.Text = dt.Rows[0]["Marks_And_No"].ToString();
                        txtNo_Of_bags.Text = dt.Rows[0]["Bags"].ToString();
                        string Currencytype = dt.Rows[0]["Foregin_Type"].ToString();
                        drpCurrencytype.SelectedValue = Currencytype;
                        txtParticular.Text = dt.Rows[0]["Particular"].ToString();
                        txtQty.Text = dt.Rows[0]["Qnty_MT"].ToString();
                        txtRate_Foregin.Text = dt.Rows[0]["Rate_Foregin"].ToString();
                        txtAmount.Text = dt.Rows[0]["Amt_Foregin"].ToString();
                        txtSupplier_Detail.Text = dt.Rows[0]["Supplier_Detail"].ToString();
                        txtManufacturer_Detail.Text = dt.Rows[0]["Manufacturer_Detail"].ToString();
                        txtGstRate.Text = dt.Rows[0]["Gst_Code"].ToString();
                        lblGstRateName.Text = dt.Rows[0]["GST_Name"].ToString();
                        txtBags_Perkg.Text = dt.Rows[0]["Bagsper_Kg"].ToString();
                        txtNet_Wt_Incr.Text = dt.Rows[0]["Increaseby"].ToString();
                        txtNet_Wt.Text = dt.Rows[0]["Net_Wt"].ToString();
                        txtGross_Wt.Text = dt.Rows[0]["Gross_Wt"].ToString();
                        txtExportDelaration.Text = dt.Rows[0]["ExportDelaration"].ToString();
                        txtCountryFinalDestination.Text = dt.Rows[0]["CountryFinalDestination"].ToString();
                        txtPort_of_Discharge.Text = dt.Rows[0]["Port_of_Discharge"].ToString();
                        txtManufacturer_code.Text = dt.Rows[0]["Manufacturer_Code"].ToString();
                        txtSubsidies.Text = dt.Rows[0]["Subsidies"].ToString();
                        txtHSN.Text = dt.Rows[0]["HSN"].ToString();
                        txtNotifyPartyCode1.Text = dt.Rows[0]["Notify_PartyCode1"].ToString();
                        txtNotifyPartyCode2.Text = dt.Rows[0]["Notify_PartyCode2"].ToString();
                        string ReportType = dt.Rows[0]["ReportType"].ToString();
                        txtITEM_CODE.Text = dt.Rows[0]["Item_Code"].ToString();
                        LBLITEMNAME.Text = dt.Rows[0]["System_Name_E"].ToString();
                        txtplantName.Text = dt.Rows[0]["plantName"].ToString();
                        txtplantCode.Text = dt.Rows[0]["plantCode"].ToString();
                        txttitleNarration.Text = dt.Rows[0]["titleNarration"].ToString();
                        txtNarration.Text = dt.Rows[0]["Narration"].ToString();
                        txtsalebill_No.Text = dt.Rows[0]["salebill_No"].ToString();
                        lblsalebill_Id.Text = dt.Rows[0]["sale_id"].ToString();
                        drpReportType.SelectedValue = ReportType;
                        //hdnfshipto.Value = dt.Rows[0]["ShipTo_Id"].ToString();
                        //hdnfconsign.Value = dt.Rows[0]["Consign_ID"].ToString();


                        recordExist = true;
                        lblMsg.Text = "";

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
                        setFocusControl(txtIECCode);
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
            if (strTextBox == "txtIECCode")
            {
                setFocusControl(txtAc_Code);
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
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        ds = clsDAL.SimpleQuery("select Ac_Name_E,Address_E,CityName,Pincode,Ie_Code from " + qryAccountList + " where Ac_Code=" + txtAc_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
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
                                    hdnfshipto.Value = clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + txtAc_Code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                    txtAc_Address.Text = acname;
                                    //txtIE_Code.Text = dt.Rows[0]["Ie_Code"].ToString();
                                    setFocusControl(txtAc_Address);
                                }
                            }
                        }

                        else
                        {
                            txtAc_Code.Text = string.Empty;
                            txtAc_Address.Text = acname;
                            setFocusControl(txtAc_Code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtAc_Code);
                }
            }
            if (strTextBox == "txtBuyer_code")
            {
                string acname = "";
                if (txtBuyer_code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtBuyer_code.Text);
                    if (a == false)
                    {
                        btntxtBuyer_code_Click(this, new EventArgs());
                    }
                    else
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        ds = clsDAL.SimpleQuery("select Ac_Name_E,Address_E,CityName,Pincode,Ie_Code from " + qryAccountList + " where Ac_Code=" + txtBuyer_code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
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
                                    // hdnfconsign.Value = clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + txtConsign_code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                    txtBuyer_Address.Text = acname;

                                    setFocusControl(txtBuyer_Address);
                                }
                            }
                        }

                        else
                        {
                            txtBuyer_code.Text = string.Empty;
                            txtBuyer_Address.Text = acname;
                            setFocusControl(txtBuyer_code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtBuyer_code);
                }
            }

            if (strTextBox == "txtAc_Address")
            {
                setFocusControl(txtBuyer_code);
            }
            if (strTextBox == "txtBuyer_Address")
            {
                setFocusControl(txtNotifyPartyCode1);
            }

            if (strTextBox == "txtsalebill_No")
            {
                string saleid = "";
                if (txtsalebill_No.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtsalebill_No.Text);
                    if (a == false)
                    {
                        btnsalebill_No_Click(this, new EventArgs());
                    }
                    else
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        ds = clsDAL.SimpleQuery("select doc_no,doc_date,saleid,Quantal,packing,bags from qrysaleheaddetail where doc_no=" + txtsalebill_No.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["Year"].ToString()) + "");
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    saleid = dt.Rows[0]["saleid"].ToString();
                                    lblsalebill_Id.Text = saleid;
                                    txtQty.Text = dt.Rows[0]["Quantal"].ToString();
                                    txtBags_Perkg.Text = dt.Rows[0]["packing"].ToString();
                                    txtNo_Of_bags.Text = dt.Rows[0]["bags"].ToString();


                                    setFocusControl(txtAc_Code);
                                    calculation();
                                }
                            }
                        }

                        else
                        {
                            txtsalebill_No.Text = string.Empty;
                            lblsalebill_Id.Text = saleid;
                            setFocusControl(txtsalebill_No);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtBuyer_code);
                }
            }
            if (strTextBox == "txtNotifyPartyCode1")
            {
                string acname = "";
                if (txtNotifyPartyCode1.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtNotifyPartyCode1.Text);
                    if (a == false)
                    {
                        btntxtNotifyPartyCode1_Click(this, new EventArgs());
                    }
                    else
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        ds = clsDAL.SimpleQuery("select Ac_Name_E,Address_E,CityName,Pincode from " + qryAccountList + " where Ac_Code=" + txtNotifyPartyCode1.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
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
                                    // hdnfconsign.Value = clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + txtConsign_code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                    txtNotify_Party1.Text = acname;

                                    setFocusControl(txtNotify_Party1);
                                }
                            }
                        }

                        else
                        {
                            txtNotifyPartyCode1.Text = string.Empty;
                            txtNotify_Party1.Text = acname;
                            setFocusControl(txtNotifyPartyCode1);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtNotify_Party1);
                }
            }
            if (strTextBox == "txtNotify_Party1")
            {
                setFocusControl(txtNotifyPartyCode2);
            }
            if (strTextBox == "txtNotifyPartyCode2")
            {
                string acname = "";
                if (txtNotifyPartyCode2.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtNotifyPartyCode2.Text);
                    if (a == false)
                    {
                        btntxtNotifyPartyCode2_Click(this, new EventArgs());
                    }
                    else
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        ds = clsDAL.SimpleQuery("select Ac_Name_E,Address_E,CityName,Pincode from " + qryAccountList + " where Ac_Code=" + txtNotifyPartyCode2.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
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
                                    // hdnfconsign.Value = clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + txtConsign_code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                    txtNotify_Party1.Text = acname;

                                    setFocusControl(txtNotify_Party2);
                                }
                            }
                        }

                        else
                        {
                            txtNotifyPartyCode2.Text = string.Empty;
                            txtNotify_Party2.Text = acname;
                            setFocusControl(txtNotifyPartyCode2);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtNotifyPartyCode2);
                }
            }
            if (strTextBox == "txtNotify_Party2")
            {
                setFocusControl(txtcountryorigin);
            }
            if (strTextBox == "txtcountryorigin")
            {
                setFocusControl(txtFinal_Destination);
            }
            if (strTextBox == "txtFinal_Destination")
            {
                setFocusControl(txtPer_Carriage);
            }
            if (strTextBox == "txtPer_Carriage")
            {
                setFocusControl(txtPlace_Of_Receipt);
            }
            if (strTextBox == "txtPlace_Of_Receipt")
            {
                setFocusControl(txtVessel);
            }
            if (strTextBox == "txtVessel")
            {
                setFocusControl(txtPort_Of_Loading);
            }
            if (strTextBox == "txtPort_Of_Loading")
            {
                setFocusControl(txtTerms);
            }
            if (strTextBox == "txtTerms")
            {
                setFocusControl(txtMAEQ);
            }
            if (strTextBox == "txtMAEQ")
            {
                setFocusControl(txtGSTNo);
            }
            if (strTextBox == "txtGSTNo")
            {
                setFocusControl(txtMarksAndNos);
            }
            if (strTextBox == "txtMarksAndNos")
            {
                setFocusControl(txtNo_Of_bags);
            }
            if (strTextBox == "txtNo_Of_bags")
            {
                setFocusControl(drpCurrencytype);
            }
            if (strTextBox == "drpCurrencytype")
            {
                setFocusControl(txtParticular);
            }
            if (strTextBox == "txtParticular")
            {
                setFocusControl(txtQty);
            }
            if (strTextBox == "txtQty")
            {
                setFocusControl(txtRate_Foregin);
            }
            if (strTextBox == "txtRate_Foregin")
            {
                setFocusControl(txtAmount);
            }
            if (strTextBox == "txtAmount")
            {
                setFocusControl(txtSupplier_Detail);
            }
            if (strTextBox == "txtSupplier_Detail")
            {
                setFocusControl(txtManufacturer_Detail);
            }
            if (strTextBox == "txtManufacturer_Detail")
            {
                setFocusControl(txtGstRate);
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
                        gstratename = clsCommon.getString("select GST_Name from " + tblPrefix + "GSTRateMaster  where Doc_no=" + txtGstRate.Text + "");
                        if (gstratename != string.Empty && gstratename != "0")
                        {
                            DataSet ds1 = clsDAL.SimpleQuery("select Doc_no,GST_Name,Rate,IGST,SGST,CGST from nt_1_gstratemaster where Doc_No=" + txtGstRate.Text + " and company_code=" + Session["Company_Code"].ToString() + "");
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
                            setFocusControl(txtBags_Perkg);
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
            if (strTextBox == "txtBags_Perkg")
            {
                setFocusControl(txtNet_Wt_Incr);
            }
            if (strTextBox == "txtNet_Wt_Incr")
            {
                setFocusControl(txtNet_Wt);

            }
            if (strTextBox == "txtNet_Wt")
            {
                setFocusControl(txtGross_Wt);
            }
            if (strTextBox == "txtGross_Wt")
            {
                setFocusControl(txtRate_Foregin);
            }
            if (strTextBox == "txtManufacturer_code")
            {
                string acname = "";
                if (txtManufacturer_code.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtManufacturer_code.Text);
                    if (a == false)
                    {
                        btntxtManufacturer_code_Click(this, new EventArgs());
                    }
                    else
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        ds = clsDAL.SimpleQuery("select Ac_Name_E,Address_E,CityName,Pincode,plantName,plantCode from " + qryAccountList + " where Ac_Code=" + txtManufacturer_code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "");
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
                                    plantName = dt.Rows[0]["plantName"].ToString();
                                    plantCode = dt.Rows[0]["plantCode"].ToString();
                                    // hdnfconsign.Value = clsCommon.getString("select accoid from " + qryAccountList + " where Ac_Code=" + txtConsign_code.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()));
                                    txtManufacturer_Detail.Text = acname;
                                    txtplantName.Text = plantName;
                                    txtplantCode.Text = plantCode;

                                    setFocusControl(txtManufacturer_code);
                                }
                            }
                        }

                        else
                        {
                            txtManufacturer_code.Text = string.Empty;
                            txtManufacturer_Detail.Text = acname;
                            setFocusControl(txtManufacturer_code);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtManufacturer_code);
                }
            }
            if (strTextBox == "txtITEM_CODE")
            {
                string itemname = "";
                if (txtITEM_CODE.Text != string.Empty)
                {
                    bool a = clsCommon.isStringIsNumeric(txtITEM_CODE.Text);
                    if (a == false)
                    {
                        btntxtITEM_CODE_Click(this, new EventArgs());
                    }
                    else
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        ds = clsDAL.SimpleQuery("select System_Name_E,HSN from " + SystemMasterTable + " where System_Code=" + txtITEM_CODE.Text + "  and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and System_Type='I'");
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];
                                if (dt.Rows.Count > 0)
                                {
                                    LBLITEMNAME.Text = dt.Rows[0]["System_Name_E"].ToString();
                                    txtHSN.Text = dt.Rows[0]["HSN"].ToString();
                                    setFocusControl(txtHSN);
                                }
                            }
                        }
                        else
                        {
                            txtITEM_CODE.Text = string.Empty;
                            LBLITEMNAME.Text = itemname;
                            setFocusControl(txtITEM_CODE);
                        }
                    }
                }
                else
                {
                    setFocusControl(txtITEM_CODE);
                }
            }
            if (strTextBox == "drpReportType")
            {
                setFocusControl(btnSave);
            }

      

        }
        catch
        {
        }
    }
    #endregion

    private void calculation()
    {

        //Int32 qty = txtQty.Text != string.Empty ? Convert.ToInt32(txtQty.Text) : 0;
        //double weight = Math.Round((txtWeight.Text != string.Empty ? Convert.ToDouble(txtWeight.Text) : 0), 3);
        //double netwt = 0.000;
        #region[calculation]
        double bagperkg = txtBags_Perkg.Text != string.Empty ? Convert.ToDouble(txtBags_Perkg.Text) : 0;
        double qty = txtQty.Text != string.Empty ? Convert.ToDouble(txtQty.Text) : 0;
        double rate = Math.Round((txtRate_Foregin.Text != string.Empty ? Convert.ToDouble(txtRate_Foregin.Text) : 0), 2);
        //double No_Of_bags = txtNo_Of_bags.Text != string.Empty ? Convert.ToDouble(txtNo_Of_bags.Text) : 0;
        //No_Of_bags = Math.Round((100 / bagperkg) * (10 * qty), 2);
        // txtNo_Of_bags.Text = No_Of_bags.ToString();

        double amt = Math.Round((qty * rate), 2);
        double NoOfBags = txtNo_Of_bags.Text != string.Empty ? Convert.ToDouble(txtNo_Of_bags.Text) : 0;
        double NetWtIncr = txtNet_Wt_Incr.Text != string.Empty ? Convert.ToDouble(txtNet_Wt_Incr.Text) : 0;
        double grosswtnew = txtGross_Wt.Text != string.Empty ? Convert.ToDouble(txtGross_Wt.Text) : 0;
        if (drpReportType.SelectedValue == "MT")
        {
            //double QtlRate = rate * 10;
            //double Quantl = qty / 10;
            //double grosswt1 = NoOfBags * NetWtIncr;
            //double grosswt2 = grosswt1 / 1000;
            //double grosswt = qty + grosswt2;
            //txtAmount.Text = amt.ToString();
            //txtNet_Wt.Text = qty.ToString();
            //txtGross_Wt.Text = grosswt.ToString();
            //txtQty.Text = Quantl.ToString();
            //txtRate_Foregin.Text = QtlRate.ToString();
            double Quantl = qty / 10;
            txtQty.Text = Quantl.ToString();
            txtNet_Wt.Text = Quantl.ToString();
            double bagperkg2 = bagperkg + NetWtIncr;
            double grosswt = Math.Round(bagperkg2 * NoOfBags / 1000, 3);
            txtGross_Wt.Text = grosswt.ToString();
            txtAmount.Text = amt.ToString();

        }
        else
        {
            //double QtlRate = rate / 10;
            //double Quantl = qty * 10;
            //double grosswt1 = NoOfBags * NetWtIncr;
            //double grosswt2 = grosswt1 / 100;
            //double grosswt = qty + grosswt2 * 10;
            //double net_wt = qty * 10;
            //txtAmount.Text = amt.ToString();
            //txtNet_Wt.Text = net_wt.ToString();
            //txtGross_Wt.Text = grosswt.ToString();
            //txtQty.Text = Quantl.ToString();
            //txtRate_Foregin.Text = QtlRate.ToString();
            //double QtlRate = rate / 10;
            //double Quantl = qty * 10; 
            //txtQty.Text = Quantl.ToString();
            //txtRate_Foregin.Text = QtlRate.ToString();
            //double grosswt2 = txtGross_Wt.Text != string.Empty ? Convert.ToDouble(txtGross_Wt.Text) : 0;
            //double grosswt = grosswt2 * 10;
            //txtGross_Wt.Text = grosswt.ToString();
            double net_wt = qty;
            txtAmount.Text = amt.ToString();
            txtNet_Wt.Text = net_wt.ToString();
            double bagperkg2 = bagperkg + NetWtIncr;
            double grosswt = Math.Round(bagperkg2 * NoOfBags / 100, 3);
            txtGross_Wt.Text = grosswt.ToString();
        }





        #endregion
    }

    #region getDisplayQuery
    private string getDisplayQuery()
    {
        try
        {
            string qryDisplay = " select * from " + qryCommon + " where Year_Code='" + Convert.ToInt32(Session["year"].ToString()) +
                "' and  Company_Code='" + Convert.ToInt32(Session["Company_Code"].ToString()) + "' and Inv_No=" + hdnf.Value;
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

            if (v == "txtAc_Code")
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
            if (v == "txtBuyer_code")
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

                    string qry = "select * from " + tblHead + " where  Inv_No='" + txtValue + "' " +
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
                                hdnf.Value = dt.Rows[0]["Inv_No"].ToString();

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
    #region [txtIECCode_TextChanged]
    protected void txtIECCode_TextChanged(object sender, EventArgs e)
    {
        searchString = txtIECCode.Text;
        strTextBox = "txtIECCode";
        csCalculations();
    }
    #endregion
    #region [txtAC_Code_TextChanged]
    protected void txtAC_Code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAc_Code.Text;
        strTextBox = "txtAc_Code";
        csCalculations();
    }
    #endregion
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
    #region [txtAc_Address_TextChanged]
    protected void txtAc_Address_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAc_Address.Text;
        strTextBox = "txtAc_Address";
        csCalculations();
    }
    #endregion
    #region [txtBuyer_code_TextChanged]
    protected void txtBuyer_code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBuyer_code.Text;
        strTextBox = "txtBuyer_code";
        csCalculations();
    }
    #endregion
    protected void btntxtBuyer_code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtBuyer_code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #region [txtBuyer_Address_TextChanged]
    protected void txtBuyer_Address_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBuyer_Address.Text;
        strTextBox = "txtBuyer_Address";
        csCalculations();
    }
    #endregion
    #region [txtNotify_Party1_TextChanged]
    protected void txtNotify_Party1_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNotify_Party1.Text;
        strTextBox = "txtNotify_Party1";
        csCalculations();
    }
    #endregion
    #region [txtNotify_Party2_TextChanged]
    protected void txtNotify_Party2_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNotify_Party2.Text;
        strTextBox = "txtNotify_Party2";
        csCalculations();
    }
    #endregion
    #region [txtcountryorigin_TextChanged]
    protected void txtcountryorigin_TextChanged(object sender, EventArgs e)
    {
        searchString = txtcountryorigin.Text;
        strTextBox = "txtcountryorigin";
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
    #region [txtTerms_TextChanged]
    protected void txtTerms_TextChanged(object sender, EventArgs e)
    {
        searchString = txtTerms.Text;
        strTextBox = "txtTerms";
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
    #region [txtGSTNo_TextChanged]
    protected void txtGSTNo_TextChanged(object sender, EventArgs e)
    {
        searchString = txtGSTNo.Text;
        strTextBox = "txtGSTNo";
        csCalculations();
    }
    #endregion
    #region [txtMarksAndNos_TextChanged]
    protected void txtMarksAndNos_TextChanged(object sender, EventArgs e)
    {
        searchString = txtMarksAndNos.Text;
        strTextBox = "txtMarksAndNos";
        csCalculations();
    }
    #endregion
    #region [txtNo_Of_bags_TextChanged]
    protected void txtNo_Of_bags_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNo_Of_bags.Text;
        strTextBox = "txtNo_Of_bags";
        csCalculations();
    }
    #endregion
    #region [drpCurrencytype_SelectedIndexChanged]
    protected void drpCurrencytype_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion
    #region [txtParticular_TextChanged]
    protected void txtParticular_TextChanged(object sender, EventArgs e)
    {
        searchString = txtParticular.Text;
        strTextBox = "txtParticular";
        csCalculations();
    }
    #endregion
    #region [txtQty_TextChanged]
    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        searchString = txtQty.Text;
        strTextBox = "txtQty";
        csCalculations();
        calculation();
    }
    #endregion
    #region [txtRate_Foregin_TextChanged]
    protected void txtRate_Foregin_TextChanged(object sender, EventArgs e)
    {
        searchString = txtRate_Foregin.Text;
        strTextBox = "txtRate_Foregin";
        csCalculations();
        calculation();
    }
    #endregion
    #region [txtAmount_TextChanged]
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        searchString = txtAmount.Text;
        strTextBox = "txtAmount";
        csCalculations();
    }
    #endregion
    #region [txtSupplier_Detail_TextChanged]
    protected void txtSupplier_Detail_TextChanged(object sender, EventArgs e)
    {
        searchString = txtSupplier_Detail.Text;
        strTextBox = "txtSupplier_Detail";
        csCalculations();
    }
    #endregion
    #region [txtManufacturer_Detail_TextChanged]
    protected void txtManufacturer_Detail_TextChanged(object sender, EventArgs e)
    {
        searchString = txtManufacturer_Detail.Text;
        strTextBox = "txtManufacturer_Detail";
        csCalculations();
    }
    #endregion


    protected void txtBags_Perkg_TextChanged(object sender, EventArgs e)
    {
        searchString = txtBags_Perkg.Text;
        strTextBox = "txtBags_Perkg";
        csCalculations();
    }
    #region [txtNet_Wt_Incr_TextChanged]
    protected void txtNet_Wt_Incr_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNet_Wt_Incr.Text;
        strTextBox = "txtNet_Wt_Incr";
        csCalculations();
        calculation();
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

                    string qry = " select Inv_No from " + tblHead + " where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString())
                        + " and (Inv_No like '%" + txtSearchText.Text + "%') order by Inv_No";
                    this.showPopup(qry);
                }
            }

            if (hdnfClosePopup.Value == "txtAc_Code")
            {
                lblPopupHead.Text = "--Select Ship To Code--";
                string qry = "select Ac_Code,Ac_Name_E,Address_E,CityName,Pincode from " + qryAccountList + " where Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%' or Address_E like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);

            }
            if (hdnfClosePopup.Value == "txtBuyer_code")
            {
                lblPopupHead.Text = "--Select Ship To Code--";
                string qry = "select Ac_Code,Ac_Name_E,Address_E,CityName,Pincode from " + qryAccountList + " where Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%' or Address_E like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);
                searchString = "";
            }

            if (hdnfClosePopup.Value == "txtsalebill_No")
            {
                lblPopupHead.Text = "--Select Ship To Code--";
                string qry = "select doc_no,doc_date,saleid from qrysaleheaddetail where Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + " and Year_Code=" + Convert.ToInt32(Session["Year"].ToString()) + "  and ( doc_no like '%" + txtSearchText.Text + "%' or doc_date like '%" + txtSearchText.Text + "%' or saleid like '%" + txtSearchText.Text + "%') order by doc_no";
                this.showPopup(qry);

            }

            if (hdnfClosePopup.Value == "txtGstRate")
            {
                lblPopupHead.Text = "--Select Mill--";
                string qry = "select Doc_no,GST_Name,Rate,IGST,SGST,CGST from nt_1_gstratemaster where " +
                    "  (Doc_no like '%" + txtSearchText.Text + "%' or GST_Name like '%" + txtSearchText.Text + "%')";
                this.showPopup(qry);
            }
            if (hdnfClosePopup.Value == "txtManufacturer_code")
            {
                lblPopupHead.Text = "--Select Ship To Code--";
                string qry = "select Ac_Code,Ac_Name_E,Address_E,CityName from " + qryAccountList + " where Ac_type='M' and Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%' or Address_E like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);

            }
            if (hdnfClosePopup.Value == "txtNotifyPartyCode1")
            {
                lblPopupHead.Text = "--Select Ship To Code--";
                string qry = "select Ac_Code,Ac_Name_E,Address_E,CityName from " + qryAccountList + " where Ac_type='M' and Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%' or Address_E like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);

            }
            if (hdnfClosePopup.Value == "txtNotifyPartyCode2")
            {
                lblPopupHead.Text = "--Select Ship To Code--";
                string qry = "select Ac_Code,Ac_Name_E,Address_E,CityName from " + qryAccountList + " where Ac_type='M' and Locked=0 and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString()) + "  and ( Ac_Code like '%" + txtSearchText.Text + "%' or Ac_Name_E like '%" + txtSearchText.Text + "%' or CityName like '%" + txtSearchText.Text + "%' or Address_E like '%" + txtSearchText.Text + "%') order by Ac_Name_E";
                this.showPopup(qry);

            }
            if (hdnfClosePopup.Value == "txtITEM_CODE")
            {
                lblPopupHead.Text = "--Select Item--";
                string qry = "select System_Code,System_Name_E as Item_Name from " + SystemMasterTable + " where System_Type='I' and Company_Code=" + Convert.ToInt32(Session["Company_Code"].ToString());
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
        bool isValidated = true;

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
        child1.SetAttributeValue("Inv_Date", DateTime.Parse(txtDoc_Date.Text, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")).ToString("yyyy/MM/dd"));
        child1.SetAttributeValue("IEC_Code_No", txtIECCode.Text);
        child1.SetAttributeValue("Ac_Code", txtAc_Code.Text != string.Empty ? txtAc_Code.Text : "0");
        child1.SetAttributeValue("Ac_Address", txtAc_Address.Text);
        child1.SetAttributeValue("Buyer_code", txtBuyer_code.Text != string.Empty ? txtBuyer_code.Text : "0");
        child1.SetAttributeValue("Buyer_Address", txtBuyer_Address.Text);
        child1.SetAttributeValue("Notify_Party1", txtNotify_Party1.Text);
        child1.SetAttributeValue("Notify_Party2", txtNotify_Party2.Text);
        child1.SetAttributeValue("County_Goods", txtcountryorigin.Text);
        child1.SetAttributeValue("Final_Distination", txtFinal_Destination.Text);
        child1.SetAttributeValue("Prefereed_Carriage", txtPer_Carriage.Text);
        child1.SetAttributeValue("Place_Precarriage", txtPlace_Of_Receipt.Text);
        child1.SetAttributeValue("vessel_No", txtVessel.Text);
        child1.SetAttributeValue("Port_Of_Landing", txtPort_Of_Loading.Text);
        child1.SetAttributeValue("Terms_Payment", txtTerms.Text);
        child1.SetAttributeValue("MAEQ_Holdder", txtMAEQ.Text);
        child1.SetAttributeValue("MAEQ_Gst_No", txtGSTNo.Text);
        child1.SetAttributeValue("Marks_And_No", txtMarksAndNos.Text != string.Empty ? txtMarksAndNos.Text : "0.00");
        child1.SetAttributeValue("Bags", txtNo_Of_bags.Text);
        string Currencytype = drpCurrencytype.SelectedValue;
        child1.SetAttributeValue("Foregin_Type", Currencytype);
        child1.SetAttributeValue("Particular", txtParticular.Text);
        child1.SetAttributeValue("Qnty_MT", txtQty.Text != string.Empty ? txtQty.Text : "0.00");
        child1.SetAttributeValue("Rate_Foregin", txtRate_Foregin.Text != string.Empty ? txtRate_Foregin.Text : "0.00");
        child1.SetAttributeValue("Amt_Foregin", txtAmount.Text != string.Empty ? txtAmount.Text : "0.00");
        child1.SetAttributeValue("Supplier_Detail", txtSupplier_Detail.Text);
        child1.SetAttributeValue("Manufacturer_Detail", txtManufacturer_Detail.Text);
        child1.SetAttributeValue("plantName", txtplantName.Text != string.Empty ? txtplantName.Text : "0.00");
        child1.SetAttributeValue("plantCode", txtplantCode.Text != string.Empty ? txtplantCode.Text : "0.00");
        child1.SetAttributeValue("titleNarration", txttitleNarration.Text != string.Empty ? txttitleNarration.Text : "0.00");
        child1.SetAttributeValue("Narration", txtNarration.Text != string.Empty ? txtNarration.Text : "0.00");
        child1.SetAttributeValue("Gst_Code", txtGstRate.Text);
        child1.SetAttributeValue("Bagsper_Kg", txtBags_Perkg.Text != string.Empty ? txtBags_Perkg.Text : "0");
        child1.SetAttributeValue("Increaseby", txtNet_Wt_Incr.Text != string.Empty ? txtNet_Wt_Incr.Text : "0");
        child1.SetAttributeValue("Net_Wt", txtNet_Wt.Text != string.Empty ? txtNet_Wt.Text : "0");
        child1.SetAttributeValue("Gross_Wt", txtGross_Wt.Text != string.Empty ? txtGross_Wt.Text : "0");
        child1.SetAttributeValue("Company_Code", Company_Code);
        child1.SetAttributeValue("Year_Code", Year_Code);
        child1.SetAttributeValue("ExportDelaration", txtExportDelaration.Text);
        child1.SetAttributeValue("CountryFinalDestination", txtCountryFinalDestination.Text);
        child1.SetAttributeValue("Port_of_Discharge", txtPort_of_Discharge.Text);
        child1.SetAttributeValue("Manufacturer_Code", txtManufacturer_code.Text != string.Empty ? txtManufacturer_code.Text : "0");
        child1.SetAttributeValue("Subsidies", txtSubsidies.Text);
        child1.SetAttributeValue("HSN", txtHSN.Text);
        child1.SetAttributeValue("Notify_PartyCode1", txtNotifyPartyCode1.Text != string.Empty ? txtNotifyPartyCode1.Text : "0");
        child1.SetAttributeValue("Notify_PartyCode2", txtNotifyPartyCode2.Text != string.Empty ? txtNotifyPartyCode2.Text : "0");
        string ReportType = drpReportType.SelectedValue;
        child1.SetAttributeValue("ReportType", ReportType);
        child1.SetAttributeValue("salebill_No", txtsalebill_No.Text != string.Empty ? txtsalebill_No.Text : "0");
        child1.SetAttributeValue("sale_id", lblsalebill_Id.Text != string.Empty ? lblsalebill_Id.Text : "0");
        if (btnSave.Text != "Save")
        {
            child1.SetAttributeValue("Modified_By", Modified_By);

            child1.SetAttributeValue("Inv_No", txtDoc_No.Text != string.Empty ? txtDoc_No.Text : "0");

        }
        else
        {
            child1.SetAttributeValue("Created_By", Created_By);

        }
        root.Add(child1);
        #endregion-End of Head part Save

        #region save Head Master

        string XMLReport = root.ToString();
        XDocument xDoc = XDocument.Parse(XMLReport, LoadOptions.None);
        XMLReport = xDoc.ToString(SaveOptions.DisableFormatting);
        DataSet xml_ds = new DataSet();
        string spname = "SP_ExportInv";
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
            flag = 2;
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
    #region [txtManufacturer_code_TextChanged]
    protected void txtManufacturer_code_TextChanged(object sender, EventArgs e)
    {
        searchString = txtManufacturer_code.Text;
        strTextBox = "txtManufacturer_code";
        csCalculations();
    }
    #endregion
    protected void btntxtManufacturer_code_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtManufacturer_code";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }

    #region [txtNotifyPartyCode1_TextChanged]
    protected void txtNotifyPartyCode1_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNotifyPartyCode1.Text;
        strTextBox = "txtNotifyPartyCode1";
        csCalculations();
    }
    #endregion
    protected void btntxtNotifyPartyCode1_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtNotifyPartyCode1";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }

    #region [txtNotifyPartyCode2_TextChanged]
    protected void txtNotifyPartyCode2_TextChanged(object sender, EventArgs e)
    {
        searchString = txtNotifyPartyCode2.Text;
        strTextBox = "txtNotifyPartyCode2";
        csCalculations();
    }
    #endregion
    protected void btntxtNotifyPartyCode2_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtNotifyPartyCode2";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #region [drpReportType_SelectedIndexChanged]
    protected void drpReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        csCalculations();
        calculation();
    }
    #endregion
    #region [txtITEM_CODE_TextChanged]
    protected void txtITEM_CODE_TextChanged(object sender, EventArgs e)
    {
        searchString = txtITEM_CODE.Text;
        strTextBox = "txtITEM_CODE";
        csCalculations();
    }
    #endregion

    #region [btntxtITEM_CODE_Click]
    protected void btntxtITEM_CODE_Click(object sender, EventArgs e)
    {
        try
        {
            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtITEM_CODE";
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion


    #region [txtsalebill_No_TextChanged]
    protected void txtsalebill_No_TextChanged(object sender, EventArgs e)
    {
        searchString = txtsalebill_No.Text;
        strTextBox = "txtsalebill_No";
        csCalculations();
    }
    #endregion

    #region [btnsalebill_No_Click]
    protected void btnsalebill_No_Click(object sender, EventArgs e)
    {
        try
        {

            pnlPopup.Style["display"] = "block";
            hdnfClosePopup.Value = "txtsalebill_No"; 
            btnSearch_Click(sender, e);
        }
        catch
        {
        }
    }
    #endregion


}